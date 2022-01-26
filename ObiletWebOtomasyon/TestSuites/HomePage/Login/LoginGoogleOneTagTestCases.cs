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
using OpenQA.Selenium.Support.PageObjects;

namespace ObiletWebOtomasyon.TestSuites.HomePage.Login
{

    [TestFixture]
    [AllureEpic("AllureEpic")] // kullanıma göre özelleştirilebilir
    [AllureFeature("APITests")] // kullanıma göre özelleştirilebilir
    [AllureParentSuite("AllureParentSuite")] // kullanıma göre özelleştirilebilir
    [AllureSuite("AllureSuite")] // kullanıma göre özelleştirilebilir
    [AllureTag("AllureTag", "Get")] // kullanıma göre özelleştirilebilir
    [AllureSeverity]

    class LoginGoogleOneTagTestCases : BaseUITestCase
    {
        public HomePagegoogleOneTag GoogleTag;
        [SetUp]
        public void GoToCreateAccountPage()
        {
            GoogleTag = new HomePagegoogleOneTag(driver);

        }

        [TestCase]
        public void GoogleOneTagHesap()
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("window.open();");
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            driver.Navigate().GoToUrl("https://accounts.google.com/");
            Thread.Sleep(2000);
            IWebElement Email = driver.FindElement(By.Id("identifierId"));
            Email.Click();
            Thread.Sleep(1000);
            Email.SendKeys("obilettest@gmail.com" + Keys.Enter);
            Thread.Sleep(2000);
            IWebElement Password = driver.FindElement(By.Name("password"));
            Password.Click();
            Thread.Sleep(1000);
            Password.SendKeys("Fk123456" + Keys.Enter);
            //IWebElement Loginbutton = driver.FindElement(By.XPath("//*[@id='passwordNext']/div/button/div[1]"));
            //Loginbutton.Click();
            Thread.Sleep(6000);
            driver.Close();
            driver.SwitchTo().Window(driver.WindowHandles.First());
            driver.Navigate().Refresh();
            Thread.Sleep(2000);
            // google one tap sign in iframe olarak çalışıyor Bu yüzden İFrame buluyoruz.
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//*[@id='credential_picker_container']/iframe")));
            driver.FindElement(By.Id("continue-as")).Click();
            Thread.Sleep(5000);
            CustomElementWait.WaitForLoad(driver);

        }

        [TearDown]
        public void After()
        {
            AfterMethod(TestContext.CurrentContext);


        }
    }
}
