using NUnit.Allure.Attributes;
using NUnit.Framework;
using System.Threading;
using ObiletWebOtomasyon.Base;
using ObiletWebOtomasyon.Common.Generator;
using ObiletWebOtomasyon.ComponentObjects.HomePage;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support;
using System;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using ObiletWebOtomasyon.Common;
using ObiletWebOtomasyon.ComponentObjects.SeferListeleme;

namespace ObiletWebOtomasyon.TestSuites.Seferler.OtobusSeferleri
{

    [TestFixture]
    [AllureEpic("AllureEpic")] // kullanıma göre özelleştirilebilir
    [AllureFeature("APITests")] // kullanıma göre özelleştirilebilir
    [AllureParentSuite("AllureParentSuite")] // kullanıma göre özelleştirilebilir
    [AllureSuite("AllureSuite")] // kullanıma göre özelleştirilebilir
    [AllureTag("AllureTag", "Get")] // kullanıma göre özelleştirilebilir
    [AllureSeverity]

    class SeferBulunamamasiTestCases : BaseUITestCase
    {
        public SeferBulunamamasi OtobusSeferiBulunmamasi;
        public HızlıFiltreler hizliFiltre;
        [SetUp]
        public void GoToCreateAccountPage()
        {
            OtobusSeferiBulunmamasi = new SeferBulunamamasi(driver);
            hizliFiltre = new HızlıFiltreler(driver);
        }

        [TestCase]
        [Order(1)]
        public void AlternatifRotaBulma()
        {

            CustomElementWait.WaitForLoad(driver);
            OtobusSeferiBulunmamasi.OtobusBiletiBulma();
            CustomElementWait.WaitForLoad(driver);
            OtobusSeferiBulunmamasi.AppStore();
            CustomElementWait.WaitForLoad(driver);
            OtobusSeferiBulunmamasi.GooglePlay();
            CustomElementWait.WaitForLoad(driver);
           // OtobusSeferiBulunmamasi.AlternatifRota();
           // CustomElementWait.WaitForLoad(driver);
            OtobusSeferiBulunmamasi.AlternatifUcakSeferleri();
            CustomElementWait.WaitForLoad(driver);

         
        }

        [TestCase("", "")]
        [Order(2)]
        public void Mail_Phone_EnAzBir_Adress_Girin(string email, string password)
        {

            CustomElementWait.WaitForLoad(driver);
            OtobusSeferiBulunmamasi.AlternatifRotaAlarm(email, password);
            CustomElementWait.WaitForLoad(driver);
            string errorMessage = OtobusSeferiBulunmamasi.returnErrorMessage(SeferBulunamamasi.ErrorMessages.EnAzbirEpostaGirin);
            string ErrorMessage = OtobusSeferiBulunmamasi.returnErrorMessage(SeferBulunamamasi.ErrorMessages.EnazbirTelefonGirin);
            Assert.AreEqual("En az bir iletişim adresi giriniz.", errorMessage, "E-posta adresi boş bırakıldığında beklenen hata mesajı gösterilmedi.");
            Assert.AreEqual("En az bir iletişim adresi giriniz.", ErrorMessage, "Telefon Numarası boş bırakıldığında beklenen hata mesaji gösterilmelidir.");
            driver.Navigate().Refresh();
        }

        [TestCase]
        [Order(3)]
        public void Mail_Phone_Gecersiz()
        {
            CustomElementWait.WaitForLoad(driver);
            string email = "faruk.kirci";
            string password = "45255";
            OtobusSeferiBulunmamasi.AlternatifRotaAlarm(email, password);
            CustomElementWait.WaitForLoad(driver);
            string errorMessage = OtobusSeferiBulunmamasi.returnErrorMessage(SeferBulunamamasi.ErrorMessages.GecersizEpostaAdresi);
            string ErrorMessage = OtobusSeferiBulunmamasi.returnErrorMessage(SeferBulunamamasi.ErrorMessages.GecersizTelefonNumarasi);
            Assert.AreEqual("Geçersiz e-posta adresi.", errorMessage, "E-posta adresi Geçersiz girildiğinde beklenen hata mesajı gösterilmedi.");
            Assert.AreEqual("Geçersiz telefon numarası.", ErrorMessage, "Telefon Numarası Geçersiz girildiğinde beklenen hata mesaji gösterilmelidir.");
            driver.Navigate().Refresh();
        }

        [TestCase("faruk.kirci@obilet.com", "45255")]
        [Order(4)]
        public void Phone_Gecersiz(string email, string password)
        {
            CustomElementWait.WaitForLoad(driver);
            OtobusSeferiBulunmamasi.AlternatifRotaAlarm(email, password);
            CustomElementWait.WaitForLoad(driver);
            string ErrorMessage = OtobusSeferiBulunmamasi.returnErrorMessage(SeferBulunamamasi.ErrorMessages.GecersizTelefonNumarasi);
            Assert.AreEqual("Geçersiz telefon numarası.", ErrorMessage, "Telefon Numarası Geçersiz girildiğinde beklenen hata mesaji gösterilmelidir.");
            driver.Navigate().Refresh();
        }

        [TestCase("faruk.kirci", "5538596190")]
        [Order(5)]
        public void Mail_Gecersiz(string email, string password)
        {
            CustomElementWait.WaitForLoad(driver);
            OtobusSeferiBulunmamasi.AlternatifRotaAlarm(email, password);
            CustomElementWait.WaitForLoad(driver);
            string errorMessage = OtobusSeferiBulunmamasi.returnErrorMessage(SeferBulunamamasi.ErrorMessages.GecersizEpostaAdresi);
            Assert.AreEqual("Geçersiz e-posta adresi.", errorMessage, "E-posta adresi Geçersiz girildiğinde beklenen hata mesajı gösterilmedi.");
            driver.Navigate().Refresh();
        }

        [TestCase("faruk.kirci@hotmail.com", "5538596190")]
        [Order(6)]
        public void Basarili_Alarm_Kurma(string email, string password)
        {
            CustomElementWait.WaitForLoad(driver);
            OtobusSeferiBulunmamasi.AlternatifRotaAlarm(email, password);
            CustomElementWait.WaitForLoad(driver);

        }



        [TearDown]
        public void After()
        {
            AfterMethod(TestContext.CurrentContext);

        }
    }
}
