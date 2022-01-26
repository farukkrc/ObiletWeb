using NUnit.Allure.Attributes;
using NUnit.Framework;
using System.Threading;
using ObiletWebOtomasyon.Base;
using ObiletWebOtomasyon.Common.Generator;
using ObiletWebOtomasyon.ComponentObjects.HomePage;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support;
using ObiletWebOtomasyon.ComponentObjects.HomePageComponents;
using System;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using ObiletWebOtomasyon.Common;

namespace ObiletWebOtomasyon.TestSuites.HomePage.Login
{

    [TestFixture]
    [AllureEpic("AllureEpic")] // kullanıma göre özelleştirilebilir
    [AllureFeature("APITests")] // kullanıma göre özelleştirilebilir
    [AllureParentSuite("AllureParentSuite")] // kullanıma göre özelleştirilebilir
    [AllureSuite("AllureSuite")] // kullanıma göre özelleştirilebilir
    [AllureTag("AllureTag", "Get")] // kullanıma göre özelleştirilebilir
    [AllureSeverity]
    class LoginFacebookTestCases : BaseUITestCase
    {
        public HomePageFacebook Facebook;
        [SetUp]
        public void GoToCreateAccountPage()
        {
            Facebook = new HomePageFacebook(driver);

        }

        [Test] //test case data 
        public void FacebookGiris()
        {
            /*
            //IWebElement UyeGiris = driver.FindElement(By.XPath("//li[@class='login']"));
            //UyeGiris.Click();
            //Thread.Sleep(2000);
            //IWebElement UyeOl = driver.FindElement(By.XPath("//*[@id='login-form']/div[5]/a"));
            //UyeOl.Click();
            */

            // facebook Üye ol buton Xpath alıyoruz.
            var openWindow = driver.FindElement(By.XPath("//*[@id='register-form']/div[1]/div[1]/button"));

            // aktif pencerenin id'si alınyor
            var currentWindow = driver.CurrentWindowHandle;

            CustomElementWait.WaitUntilElementClickable(driver, openWindow); // buton tıklanabilir olana kadar bekle

            //Yeni pencere açacak buton tıklanyor
            openWindow.Click();

            //aktif tüm pencere id leri alınıyor
            var handels = driver.WindowHandles;

            //sayfada ki bir hata varsa  2 saniye kadar testi bekletiyoruz.
            Thread.Sleep(TimeSpan.FromSeconds(2));

            //açılan en son pencereye geçiş yapılıyor
            driver.SwitchTo().Window(handels.Last());

            IWebElement email = driver.FindElement(By.Id("email"));
            email.Click();
            Thread.Sleep(1000);
            email.SendKeys("obilettest@yandex.com");
            Thread.Sleep(1000);
            IWebElement password = driver.FindElement(By.Id("pass"));
            password.Click();
            Thread.Sleep(1000);
            password.SendKeys("Fk123456");
            Thread.Sleep(1000);
            IWebElement LoginButton = driver.FindElement(By.Id("loginbutton"));
            LoginButton.Click();
            Thread.Sleep(5000);
            //ana pencreye geri dönülüyor
            driver.SwitchTo().Window(currentWindow);
            Thread.Sleep(3000);
            CustomElementWait.WaitForLoad(driver);
        }
     }
}