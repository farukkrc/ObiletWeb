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

    class LoginUyeGirisTestCases : BaseUITestCase
    {
        public HomePageUyeGiris Giris;

        [SetUp]
        public void GoToCreateAccountPage()
        {
            Giris = new HomePageUyeGiris(driver);
        }

        [TestCase("faruk@test.com", "1234")]//test case data 
        [Order(1)]
        public void EksikSifreGirisi(string email, string password)
        {
            Giris.Button();
            Thread.Sleep(1000);
            Giris.Login(email, password);
            string errorMessage = Giris.returnErrorMessage(HomePageUyeGiris.ErrorMessages.EksikSifreHatali);
            Assert.AreEqual("Şifreniz en az 6 karakter olmalıdır", errorMessage, "Şifre 6 karakterden kısa iken beklenen hata mesajı gösterilmedi.");
            driver.Navigate().Refresh();
        }

        [TestCase]
        [Order(2)]
        public void HataliEmailAdresi()
        {
            Giris.Button();
            Thread.Sleep(1000);
            string email = "Test@test";
            string password = "123456";
            Giris.Login(email, password);
            string errorMessage = Giris.returnErrorMessage(HomePageUyeGiris.ErrorMessages.EmailHatali);
            Assert.AreEqual("Geçersiz E-mail adresi", errorMessage, "Geçersiz E-mail adresi kısa iken beklenen hata mesajı gösterilmedi.");
            driver.Navigate().Refresh();
        }

        [TestCase("Faruk@hotmail.com", "")]
        [Order(3)]
        public void ParolaBosBirakilmasi(string email, string password)
        {
            Giris.Button();
            Giris.Login(email, password);
            string errorMessage = Giris.returnErrorMessage(HomePageUyeGiris.ErrorMessages.ParolaBosBirakilamaz);
            Assert.AreEqual("Şifre boş bırakılamaz", errorMessage, "Şifre boş bırakıldığında beklenen hata mesaji gösterilmelidir.");
            driver.Navigate().Refresh();
        }

        [TestCase("", "123456789")]
        [Order(4)]
        public void EmailAdresiBosBirakilmasi(string email, string password)
        {
            Giris.Button();
            Giris.Login(email, password);
            string errorMessage = Giris.returnErrorMessage(HomePageUyeGiris.ErrorMessages.EMailBosBirakilamaz);
            Assert.AreEqual("E-mail adresi boş bırakılamaz", errorMessage, "E-posta adresi boş bırakıldığında beklenen hata mesajı gösterilmedi.");
            driver.Navigate().Refresh();
        }

        [TestCase("","")]
        [Order(5)]
        public void EmailveParolaBosBirakilmasi(string email,string password)
        {
            Giris.Button();
            Giris.Login(email, password);
            string errorMessage = Giris.returnErrorMessage(HomePageUyeGiris.ErrorMessages.EMailBosBirakilamaz);
            string ErrorMessage = Giris.returnErrorMessage(HomePageUyeGiris.ErrorMessages.ParolaBosBirakilamaz);
            Assert.AreEqual("E-mail adresi boş bırakılamaz", errorMessage, "E-posta adresi boş bırakıldığında beklenen hata mesajı gösterilmedi.");
            Assert.AreEqual("Şifre boş bırakılamaz", ErrorMessage, "Şifre boş bırakıldığında beklenen hata mesaji gösterilmelidir.");
            driver.Navigate().Refresh();
        }

        [TestCase]
        [Order(6)]
        public void KayitliOlmayanEpostaileGiris()
        {
            Giris.Button();
            Thread.Sleep(1000);
            string email = "Farukomertest@hotmail.com";
            string password = "123456";
            Giris.Login(email, password);
            string errorMessage = Giris.returnErrorMessage(HomePageUyeGiris.ErrorMessages.KullanıcıAdıveSifre);
            Assert.AreEqual("Kullanıcı adı ve şifrenizi kontrol ederek tekrar deneyiniz.", errorMessage, "Kayıtlı olmayan Eposta bilgileriyle sisteme giriş yapılması beklenen hata mesajı gösterilmedi.");
            driver.Navigate().Refresh();
        }

        [TestCase("faruk1987@hotmail.com","123456")]
        [Order(7)]
        public void BasariliLoginGirisi(string email, string password)
        {
            Giris.Button();
            Giris.Login(email, password);
            
        }

        [TearDown]
        public void After()
        {
            AfterMethod(TestContext.CurrentContext);

        }
    }
}

