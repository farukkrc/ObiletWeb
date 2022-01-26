using NUnit.Allure.Attributes;
using NUnit.Framework;
using ObiletWebOtomasyon.Base;
using ObiletWebOtomasyon.Common.Generator;
using ObiletWebOtomasyon.ComponentObjects.HomePage;
using OpenQA.Selenium;
using System.Linq;
using System.Threading;

namespace ObiletWebOtomasyon.TestSuites.HomePage.Login
{

    [TestFixture]
    [AllureEpic("AllureEpic")] // kullanıma göre özelleştirilebilir
    [AllureFeature("APITests")] // kullanıma göre özelleştirilebilir
    [AllureParentSuite("AllureParentSuite")] // kullanıma göre özelleştirilebilir
    [AllureSuite("AllureSuite")] // kullanıma göre özelleştirilebilir
    [AllureTag("AllureTag", "Get")] // kullanıma göre özelleştirilebilir
    [AllureSeverity]

    class LoginUyeOlTestCases : BaseUITestCase
    {
        public HomePageUyeOl homePage;
        [SetUp]
        public void GoToCreateAccountPage()
        {
            homePage = new HomePageUyeOl(driver);

        }
        [TestCase]//test case data 
        [Order(1)]
        public void EksikParolaBilgisiGirisiyapilmasi()
        {

            homePage.navigateLoginPage();
            Thread.Sleep(1000);
            homePage.navigateLoginPageUyeOl();
            string email = "Test@test.com";
            string password = "12345";
            homePage.Login(email, password);
            string errorMessage = homePage.returnErrorMessage(HomePageUyeOl.ErrorMessages.SifreAltikarakterliolmalı);
            Assert.AreEqual("Şifreniz en az 6 karakter olmalıdır", errorMessage, "Şifre 6 karakterden kısa iken beklenen hata mesajı gösterilmedi.");
            driver.Navigate().Refresh();
        }

        [TestCase("test@test", "123456")]
        [Order(2)]
        public void GecersizEmailAdresiGirisiYapilmasi(string email, string password)
        {
            homePage.navigateLoginPage();
            Thread.Sleep(1000);
            homePage.navigateLoginPageUyeOl();
            homePage.Login(email, password);
            string errorMessage = homePage.returnErrorMessage(HomePageUyeOl.ErrorMessages.GecersizMailAdresi);
            Assert.AreEqual("Geçersiz E-mail adresi", errorMessage, "Geçersiz E-mail adresi kısa iken beklenen hata mesajı gösterilmedi.");
            driver.Navigate().Refresh();
        }

        [TestCase("test@test.com", "")]//test case data 
        [Order(3)]
        public void ParolaBilgisininBosBirakilmasi(string email, string password)
        {
            homePage.navigateLoginPage();
            Thread.Sleep(2000);
            homePage.navigateLoginPageUyeOl();
            homePage.Login(email, password);
            string errorMessage = homePage.returnErrorMessage(HomePageUyeOl.ErrorMessages.SifreBosBirakilamaz);
            Assert.AreEqual("Şifre boş bırakılamaz", errorMessage, "Şifre boş bırakıldığında beklenen hata mesajı gösterilmedi.");
            driver.Navigate().Refresh();
        }

        [TestCase("", "123456")]
        [Order(4)]
        public void EPostaAdresininBosBirakilmasi(string email, string password)
        {
            homePage.navigateLoginPage();
            Thread.Sleep(2000);
            homePage.navigateLoginPageUyeOl();
            homePage.Login(email, password);
            string errorMessage = homePage.returnErrorMessage(HomePageUyeOl.ErrorMessages.MailBosBirakilamaz);
            Assert.AreEqual("E-posta adresi boş bırakılamaz", errorMessage, "E-posta adresi boş bırakıldığında beklenen hata mesajı gösterilmedi.");
            driver.Navigate().Refresh();
        }

        [TestCase("","")]
        [Order(5)]
        public void EmailParolaBosBirakilmasi(string email,string password)
        {
            homePage.navigateLoginPage();
            Thread.Sleep(2000);
            homePage.navigateLoginPageUyeOl();
            homePage.Login(email, password);
            string errorMessage = homePage.returnErrorMessage(HomePageUyeOl.ErrorMessages.MailBosBirakilamaz);
            string ErrorMessage = homePage.returnErrorMessage(HomePageUyeOl.ErrorMessages.SifreBosBirakilamaz);
            Assert.AreEqual("E-posta adresi boş bırakılamaz", errorMessage, "E-posta adresi boş bırakıldığında beklenen hata mesajı gösterilmedi.");
            Assert.AreEqual("Şifre boş bırakılamaz", ErrorMessage, "Şifre boş bırakıldığında beklenen hata mesaji gösterilmelidir.");
            driver.Navigate().Refresh();
        }

        [Test]
        [Order(6)]
        public void RandomMailveParolaBilgileriLoginolunmasi()
        {
            homePage.navigateLoginPage();
            Thread.Sleep(2000);
            homePage.navigateLoginPageUyeOl();
            string email = RandomDataGenerator.RandomMail(10) + "@test.com";
            string password = RandomDataGenerator.RandomPassword(10);
            driver.FindElement(By.Id("contract-checkbox")).Click();
            Thread.Sleep(1000);
            homePage.KVKK();
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            driver.Close();
            driver.SwitchTo().Window(driver.WindowHandles.First());
            Thread.Sleep(3000);
            homePage.Gizlilik();
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            driver.Close();
            driver.SwitchTo().Window(driver.WindowHandles.First());
            Thread.Sleep(3000);
            driver.FindElement(By.Id("contract-checkbox")).Click();
            Thread.Sleep(1000);
            homePage.Login(email, password);
        }

        [TearDown]
        public void After()
        {
            AfterMethod(TestContext.CurrentContext);
        }
    }
}
