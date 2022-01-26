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

    class LoginSifremiUnuttumTestCases : BaseUITestCase
    {
        public HomePageSifremiUnuttum SifremiUnuttum;
        [SetUp]
        public void GoToCreateAccountPage()
        {
            SifremiUnuttum = new HomePageSifremiUnuttum(driver);

        }

        [TestCase] //test case data 
        public void SifrenizimiUnuttunuz()
        {
            SifremiUnuttum.ButtonTiklama();
            CustomElementWait.WaitForLoad(driver);
            SifremiUnuttum.ButtonTiklama2();
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            string email = "faruk1@hotmail.com";
            SifremiUnuttum.LoginClick(email);
            CustomElementWait.WaitForLoad(driver);
            //driver.FindElement(By.XPath("//*[@id='forgot-password-form']/div[4]/button")).Click();
            //Thread.Sleep(3000);
            //driver.Navigate().Refresh();

            /*
             * Bu alanda şifremi unuttum Captcha olduğu için geçemiyoruz.Alper konuşuldu bu alan zorun değil,
             * yazılmasına gerek duyulmadı daha sonra tekrar bakılıcak ama farklı bir yol izlenirse yazılacaktır.
             */

        }

        [TearDown]
        public void After()
        {
            AfterMethod(TestContext.CurrentContext);


        }
    }
}