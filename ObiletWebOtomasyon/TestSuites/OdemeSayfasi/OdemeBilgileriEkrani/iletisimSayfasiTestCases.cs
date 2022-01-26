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

namespace ObiletWebOtomasyon.TestSuites.OdemeSayfasi.OdemeBilgileriEkrani
{

    [TestFixture]
    [AllureEpic("AllureEpic")] // kullanıma göre özelleştirilebilir
    [AllureFeature("APITests")] // kullanıma göre özelleştirilebilir
    [AllureParentSuite("AllureParentSuite")] // kullanıma göre özelleştirilebilir
    [AllureSuite("AllureSuite")] // kullanıma göre özelleştirilebilir
    [AllureTag("AllureTag", "Get")] // kullanıma göre özelleştirilebilir
    [AllureSeverity]

    class iletisimSayfasiTestCases : BaseUITestCase
    {
        public İletisimBilgileri OdemeiletisimEkrani;
        public OtobusBiletiBul BiletBul;
        public TekliKoltukSecimi TekKoltuk;

        [SetUp]
        public void GoToCreateAccountPage()
        {

            BiletBul = new OtobusBiletiBul(driver);
            OdemeiletisimEkrani = new İletisimBilgileri(driver);
            TekKoltuk = new TekliKoltukSecimi(driver);

        }



        [TestCase("", "")]
        [Order(1)]
        public void İletisim_Bilgilerinin_Bos_Birakilmasi(string Email, string Telefon)
        {

            BiletBul.SeferArama();
            CustomElementWait.WaitForLoad(driver);
            TekKoltuk.Tekli_Tekli_Koltuk_Secimi();
            CustomElementWait.WaitForLoad(driver);
            var mesaj = "İletişim Bilgileri";
            Assert.AreEqual(mesaj, OdemeiletisimEkrani.İletisim);
            CustomElementWait.WaitForLoad(driver);
            driver.FindElement(By.Id("pay")).Click();
            CustomElementWait.WaitForLoad(driver);
            OdemeiletisimEkrani.İletisimEmail(Email);
            OdemeiletisimEkrani.İletisimPhone(Telefon);
            CustomElementWait.WaitForLoad(driver);
            string errorMessage = OdemeiletisimEkrani.returnErrorMessage(İletisimBilgileri.ErrorMessages.EpostaBosBirakilamaz);
            string ErrorMessage = OdemeiletisimEkrani.returnErrorMessage(İletisimBilgileri.ErrorMessages.TelefonBosBirakilamaz);
            Assert.AreEqual("Boş bırakılamaz.", errorMessage, "E-posta adresi boş bırakıldığında beklenen hata mesajı gösterilmedi.");
            Assert.AreEqual("Boş bırakılamaz.", ErrorMessage, "Telefon Numarası boş bırakıldığında beklenen hata mesaji gösterilmelidir.");
            CustomElementWait.WaitForLoad(driver);
        }

        [TestCase("obilet", "123456")]
        [Order(2)]
        public void İletisim_Bilgilerinin_Gecersiz(string Email, string Telefon)
        {
            //Tek Tek caseler çalışıcaksa bunu açalım
            /*
            BiletBul.SeferArama();
            CustomElementWait.WaitForLoad(driver);
            TekKoltuk.Tekli_Tekli_Koltuk_Secimi();
            CustomElementWait.WaitForLoad(driver);
            var mesaj = "İletişim Bilgileri";
            Assert.AreEqual(mesaj, OdemeiletisimEkrani.İletisim);
            CustomElementWait.WaitForLoad(driver);
            driver.FindElement(By.Id("pay")).Click();
            */

            CustomElementWait.WaitForLoad(driver);
            OdemeiletisimEkrani.İletisimEmail(Email);
            OdemeiletisimEkrani.İletisimPhone(Telefon);
            driver.FindElement(By.Id("form")).Click();
            //CustomElementWait.WaitForLoad(driver);
            //driver.FindElement(By.Id("pay")).Click();
            CustomElementWait.WaitForLoad(driver);
            string errorMessage = OdemeiletisimEkrani.returnErrorMessage(İletisimBilgileri.ErrorMessages.GecersizEpostaAdresi);
            string ErrorMessage = OdemeiletisimEkrani.returnErrorMessage(İletisimBilgileri.ErrorMessages.GecersizTelefonNumarasi);
            Assert.AreEqual("Geçersiz e-posta adresi.", errorMessage, "E-posta adresi geçersiz ise beklenen hata mesajı gösterilmedi.");
            Assert.AreEqual("Geçersiz telefon numarası.", ErrorMessage, "Telefon Numarası geçersiz ise beklenen hata mesaji gösterilmelidir.");
            CustomElementWait.WaitForLoad(driver);
        }

        [TestCase("faruk.kirci@obilet.com", "5538596190")]
        [Order(3)]
        public void İletisim_Gecerli(string Email, string Telefon)
        {
            // Tek Tek caseler çalışıcaksa bunu açalım
            /*
            BiletBul.SeferArama();
            CustomElementWait.WaitForLoad(driver);
            TekKoltuk.Tekli_Tekli_Koltuk_Secimi();
            CustomElementWait.WaitForLoad(driver);
            var mesaj = "İletişim Bilgileri";
            Assert.AreEqual(mesaj, OdemeiletisimEkrani.İletisim);
            */
            CustomElementWait.WaitForLoad(driver);
            OdemeiletisimEkrani.İletisimEmail(Email);
            var Namevalue = driver.FindElement(By.Id("email")).GetAttribute("value");
            Assert.AreEqual("faruk.kirci@obilet.com", Email);
            OdemeiletisimEkrani.İletisimPhone(Telefon);
            var phonevalue = driver.FindElement(By.Id("phone")).GetAttribute("value");
            Assert.AreEqual("5538596190", Telefon);
            CustomElementWait.WaitForLoad(driver);
            OdemeiletisimEkrani.UcretsizSMS();
            CustomElementWait.WaitForLoad(driver);
        }

        [TearDown]
        public void After()
        {
            AfterMethod(TestContext.CurrentContext);

        }

    }
}
