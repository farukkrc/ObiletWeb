using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using System;
using System.Configuration;
using ObiletWebOtomasyon.Base.Helpers;
using ObiletWebOtomasyon.Common;
using Allure.Commons;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using Logger = ObiletWebOtomasyon.Base.Helpers.Logger;
using static System.IO.Path;
using System.Diagnostics;

namespace ObiletWebOtomasyon.Base
{
    [AllureNUnit]
    [TestFixture]
    [Category("SeleniumTestProject")]
    [Author("Tester > Faruk KIRCI", "faruk.kirci@obilet.com")]
    [AllureDisplayIgnored]
    public abstract class BaseUITestCase
    {
        protected static IWebDriver driver;
        protected string AllureCleanUpType;
        Logger UITestLog = Logger.getInstance();
        string errorMessage;
        string browserProcessName = "";
        string gridURL = ConfigurationManager.AppSettings["ScrenShootsPath"];
        string homePageURL = ConfigurationManager.AppSettings["ObiletHomePageURL"];
        int timeoutWaitSecond = Convert.ToInt32(ConfigurationManager.AppSettings["TimeoutWaitSecond"]);
        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            var allureLogsPath = ConfigurationManager.AppSettings["AllureLogsPath"];//allure loglarının eklenecegi path i app.config bölümünden alır.
            Environment.CurrentDirectory = GetDirectoryName(allureLogsPath) ?? throw new InvalidOperationException();// allure path ini environment path e ekler

            AllureCleanUpType =
                 "false"; // allureCleanUpType : false (false > gelirse loglari silmeden ustune yazmaya devam eder!TTrue dersek silip yazar)

            if (TestContext.Parameters["allureCleanUpType"] != null)
                AllureCleanUpType = TestContext.Parameters["allureCleanUpType"];

            if (AllureCleanUpType.IndexOf("true", StringComparison.OrdinalIgnoreCase) != -1)
                AllureLifecycle.Instance.CleanupResultDirectory(); // NUnitConsole3 Params * 
            BeforeMethod(BrowserType.Chrome);//burdan seçtiğimiz browser a göre webdriver i ayaga kaldırıyoruz.
        }
        [OneTimeTearDown]
        public void RunAfterAnyTests()
        {
            //açılan browserlerin kapatılması 

            CloseDriver();
            #region BrowserKill
            Process[] chromeDriverProcesses = Process.GetProcessesByName(browserProcessName);
            foreach (var chromeDriverProcess in chromeDriverProcesses)
            {
                try
                {
                    chromeDriverProcess.Kill();
                }
                catch (Exception)
                {
                    continue;
                }
            }
            #endregion
        }

        public void AfterMethod(TestContext currentResult)
        {
            //log
            var status = currentResult.Result.Outcome.Status;
            var stackTrace = string.IsNullOrEmpty(currentResult.Result.StackTrace);
            var message = string.IsNullOrEmpty(currentResult.Result.Message);
            errorMessage = TestContext.CurrentContext.Result.Message;

            switch (status)
            {
                case TestStatus.Inconclusive:
                    CustomScreenShoot.TakeScreenshot(driver, currentResult); 
                    break;
                case TestStatus.Skipped:
                    break;
                case TestStatus.Passed:
                    break;
                case TestStatus.Warning:
                    CustomScreenShoot.TakeScreenshot(driver, currentResult); 
                    break;
                case TestStatus.Failed:
                    
                    //Test Case fail verince ilgili sayfanın screenshot u alınıp log düşülüyor.
                    UITestLog.ExceptionLog(driver.Url, errorMessage, status);
                    CustomScreenShoot.TakeScreenshot(driver, currentResult);  
                    break;

                default:

                    break;
            }
        }

        internal void SwitchToTab(int tabIndex)
        {
            driver.SwitchTo().Window(driver.WindowHandles[tabIndex]);
            CustomElementWait.WaitForLoad(driver);
        }

        public void CloseDriver()
        {
            if (driver != null)
            {
                driver.Close();
                driver.Quit();
            }
        }
        public enum BrowserType
        {
            Chrome,
            Firefox,
            InternetExplorer
        }
        public void BeforeMethod(BrowserType browser)
        {
            try
            {
                if (browser == BrowserType.Chrome)
                {//Chrome Driver in ayaga kaldırılması
                    browserProcessName = "chromedriver";
                    ChromeOptions options = new ChromeOptions();
                    options.AddArgument("--no-sandbox");
                    options.AddArgument("--test-type");
                    options.AddArgument("--enable-automation");
                    options.AddArgument("--window-size=1920,1080");
                    options.AddArgument("--enable-precise-memory-info");
                    options.SetLoggingPreference(LogType.Browser, LogLevel.All);
                    var capabilities = options.ToCapabilities();
                    // driver = new RemoteWebDriver(new Uri(GridURL), capabilities);
                    driver = new ChromeDriver(@"C:\Users\Ömer Faruk\Desktop\obilet-web-otomasyon\ObiletWebOtomasyon\Resources\Drivers", options);
                //}
                //else if (browser == BrowserType.Firefox)
                //{//Firefox Driver in ayaga kaldırılması
                //    browserProcessName = "geckodriver";
                //    FirefoxOptions option = new FirefoxOptions();
                //    var capabilities = new FirefoxOptions().ToCapabilities();

                //}
                //else if (browser == BrowserType.InternetExplorer)
                //{//İE Driver in ayaga kaldırılması
                //    browserProcessName = "IEDriverServer";
                //    InternetExplorerOptions options = new InternetExplorerOptions();
                //    options.IgnoreZoomLevel = true;
                //    options.RequireWindowFocus = true;
                //    options.EnableNativeEvents = false;
                //    options.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
                //    options.EnablePersistentHover = true;
                //    var capabilities = options.ToCapabilities();// Node da çalışacak Browser (Internet-Option / Internet-Profile)

                }
                driver.Manage().Cookies.DeleteAllCookies();
                driver.Manage().Window.Maximize();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(timeoutWaitSecond); // 20 saniye
                driver.Navigate().GoToUrl(homePageURL);
                //driver.Navigate().GoToUrl(homePageURL);

            }
            catch (Exception e)
            {
                errorMessage = TestContext.CurrentContext.Result.Message;
                UITestLog.ExceptionLog(errorMessage, "", e);
            }
        }
    }
}
