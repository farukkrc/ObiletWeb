using NUnit.Allure.Attributes;
using NUnit.Framework;
using ObiletWebOtomasyon.Base;
using ObiletWebOtomasyon.Common.Generator;
using ObiletWebOtomasyon.ComponentObjects.HomePage;
using ObiletWebOtomasyon.ComponentObjects.OdemeEkrani;
using OpenQA.Selenium;
using System.Linq;
using System.Threading;
using ObiletWebOtomasyon.Common;
using OpenQA.Selenium.Support;
using System;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
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

    class YolcuBilgileriTestCases : BaseUITestCase
    {
        public YolcuBilgileri OdemeYolcuBilgileri;
        public OtobusBiletiBul BiletBul;
        public TekliKoltukSecimi TekliKoltuk;
        public İletisimBilgileri OdemeiletisimEkrani;

        [SetUp]
        public void GoToCreateAccountPage()
        {
            OdemeYolcuBilgileri = new YolcuBilgileri(driver);
            BiletBul = new OtobusBiletiBul(driver);
            TekliKoltuk = new TekliKoltukSecimi(driver);
            OdemeiletisimEkrani = new İletisimBilgileri(driver);
        
        }

        [Order(1)]
        [TestCase("", "")]
        public void YolcuBilgileriBosBirakilmasi(string AdSoyad, string TC)
        {
            BiletBul.SeferArama();
            CustomElementWait.WaitForLoad(driver);
            TekliKoltuk.Tekli_Tekli_Koltuk_Secimi();
            CustomElementWait.WaitForLoad(driver);
            var mesaj = "Yolcu Bilgileri";
            Assert.AreEqual(mesaj, OdemeYolcuBilgileri.YolcuBilgileriYazisi);
            CustomElementWait.WaitForLoad(driver);
            driver.FindElement(By.Id("pay")).Click();
            OdemeYolcuBilgileri.YolcuAdSoyad(AdSoyad);
            OdemeYolcuBilgileri.TcKimlik(TC);
            CustomElementWait.WaitForLoad(driver);
            string errorMessage = OdemeYolcuBilgileri.returnErrorMessage(YolcuBilgileri.ErrorMessages.AdSoyadBosBirakilamaz);
            string ErrorMessage = OdemeYolcuBilgileri.returnErrorMessage(YolcuBilgileri.ErrorMessages.TCkimlikBosBirakilamaz);
            Assert.AreEqual("Boş bırakılamaz.", errorMessage, "AdSoyad boş bırakıldığında beklenen hata mesajı gösterilmedi.");
            Assert.AreEqual("Boş bırakılamaz.", ErrorMessage, "TC boş bırakıldığında beklenen hata mesaji gösterilmelidir.");
            CustomElementWait.WaitForLoad(driver);
            
        }

        [Order(2)]
        [TestCase("fa", "")]
        public void YolcuBilgilerUcKarakterolmali(string AdSoyad, string TC)
        {
            // Tek Tek caseler çalışıcaksa bunu açalım
            /*
            BiletBul.SeferArama();
            CustomElementWait.WaitForLoad(driver);
            TekliKoltuk.Tekli_Tekli_Koltuk_Secimi();
            CustomElementWait.WaitForLoad(driver);
            var mesaj = "Yolcu Bilgileri";
            Assert.AreEqual(mesaj, Yolcu.YolcuBilgileriYazisi);
            CustomElementWait.WaitForLoad(driver);
            driver.FindElement(By.Id("pay")).Click();
            */

            CustomElementWait.WaitForLoad(driver);
            OdemeYolcuBilgileri.YolcuAdSoyad(AdSoyad);
            OdemeYolcuBilgileri.TcKimlik(TC);
            CustomElementWait.WaitForLoad(driver);
            string errorMessage = OdemeYolcuBilgileri.returnErrorMessage(YolcuBilgileri.ErrorMessages.YolcuİsmiUcKarektericermelidir);
            string ErrorMessage = OdemeYolcuBilgileri.returnErrorMessage(YolcuBilgileri.ErrorMessages.TCkimlikBosBirakilamaz);
            Assert.AreEqual("Yolcu ismi en az 3 karakter içermelidir.", errorMessage, "Yolcu ismi en az 3 karakter az ise beklenen hata mesajı gösterilmedi.");
            Assert.AreEqual("Boş bırakılamaz.", ErrorMessage, "TC boş bırakıldığında beklenen hata mesaji gösterilmelidir.");
            CustomElementWait.WaitForLoad(driver);
            
        }

        [Order(3)]
        [TestCase("fa1907", "342342")]
        public void YolcuBilgilerUcharfİccermeli(string AdSoyad, string TC)
        {
            //Tek Tek caseler çalışıcaksa bunu açalım
            /*
            BiletBul.SeferArama();
            CustomElementWait.WaitForLoad(driver);
            TekliKoltuk.Tekli_Tekli_Koltuk_Secimi();
            CustomElementWait.WaitForLoad(driver);
            var mesaj = "Yolcu Bilgileri";
            Assert.AreEqual(mesaj, Yolcu.YolcuBilgileriYazisi);
            CustomElementWait.WaitForLoad(driver);
            driver.FindElement(By.Id("pay")).Click();
            */


            CustomElementWait.WaitForLoad(driver);
            OdemeYolcuBilgileri.YolcuAdSoyad(AdSoyad);
            OdemeYolcuBilgileri.TcKimlik(TC);
            CustomElementWait.WaitForLoad(driver);
            string errorMessage = OdemeYolcuBilgileri.returnErrorMessage(YolcuBilgileri.ErrorMessages.YolcuİsmiUcharficermelidir);
            string ErrorMessage = OdemeYolcuBilgileri.returnErrorMessage(YolcuBilgileri.ErrorMessages.TCkimlikNumarsiGecersiz);
            Assert.AreEqual("Yolcu ismi en az 3 harf içermelidir.", errorMessage, "Yolcu ismi en az 3 harf içermelidir az ise beklenen hata mesajı gösterilmedi.");
            Assert.AreEqual("Geçersiz T.C. kimlik numarası.", ErrorMessage, "Geçersiz T.C. kimlik numarası bırakıldığında beklenen hata mesaji gösterilmelidir.");
            CustomElementWait.WaitForLoad(driver);
        }

        [Order(4)]
        [TestCase("faruk1907", "342342")]
        public void YolcuBilgilerGecersizKarakter(string AdSoyad, string TC)
        {
            // Tek Tek caseler çalışıcaksa bunu açalım
            /*
            BiletBul.SeferArama();
            CustomElementWait.WaitForLoad(driver);
            TekliKoltuk.Tekli_Tekli_Koltuk_Secimi();
            CustomElementWait.WaitForLoad(driver);
            var mesaj = "Yolcu Bilgileri";
            Assert.AreEqual(mesaj, Yolcu.YolcuBilgileriYazisi);
            CustomElementWait.WaitForLoad(driver);
            driver.FindElement(By.Id("pay")).Click();
            */

            CustomElementWait.WaitForLoad(driver);
            OdemeYolcuBilgileri.YolcuAdSoyad(AdSoyad);
            OdemeYolcuBilgileri.TcKimlik(TC);
            CustomElementWait.WaitForLoad(driver);
            string errorMessage = OdemeYolcuBilgileri.returnErrorMessage(YolcuBilgileri.ErrorMessages.YolcuİsmiGecersizKaraktericermektedir);
            string ErrorMessage = OdemeYolcuBilgileri.returnErrorMessage(YolcuBilgileri.ErrorMessages.TCkimlikNumarsiGecersiz);
            Assert.AreEqual("Yolcu ismi geçersiz karakterler içermektedir.", errorMessage, "Yolcu ismi geçersiz karakterler içermektedir beklenen hata mesajı gösterilmedi.");
            Assert.AreEqual("Geçersiz T.C. kimlik numarası.", ErrorMessage, "Geçersiz T.C. kimlik numarası bırakıldığında beklenen hata mesaji gösterilmelidir.");
            CustomElementWait.WaitForLoad(driver);
        }

        [Order(5)]
        [TestCase]
        public void YolcuBilgileriBasarili()
        {
            //Tek Tek caseler çalışıcaksa bunu açalım
            /*
            BiletBul.SeferArama();
            CustomElementWait.WaitForLoad(driver);
            TekliKoltuk.Tekli_Tekli_Koltuk_Secimi();
            CustomElementWait.WaitForLoad(driver);
            var mesaj = "Yolcu Bilgileri";
            Assert.AreEqual(mesaj, Yolcu.YolcuBilgileriYazisi);
            */
            CustomElementWait.WaitForLoad(driver);
            string AdSoyad = "Faruk Kırcı";
            string TC = "13127215844";
            CustomElementWait.WaitForLoad(driver);
            OdemeYolcuBilgileri.YolcuAdSoyad(AdSoyad);
            var AccName = driver.FindElement(By.XPath("//input[@placeholder='Doldurulması zorunludur.']")).GetAttribute("value");
            Assert.AreEqual("Faruk Kırcı", AdSoyad);
            OdemeYolcuBilgileri.TcKimlik(TC);
            var phonevalue = driver.FindElement(By.XPath("//input[contains(@placeholder,'Karayolu Taşıma Yönetmeliğince gereklidir.')]")).GetAttribute("value");
            Assert.AreEqual("13127215844", TC);
            CustomElementWait.WaitForLoad(driver);
            OdemeYolcuBilgileri.HesKodu();
            CustomElementWait.WaitForLoad(driver);
            OdemeYolcuBilgileri.SeyahatSigortasi();
            CustomElementWait.WaitForLoad(driver);

        }

        [Order(6)]
        [TestCase]
        public void YabanciYolcu()
        {
            //Tek Tek caseler çalışıcaksa bunu açalım
            /*
            BiletBul.SeferArama();
            CustomElementWait.WaitForLoad(driver);
            TekliKoltuk.Tekli_Tekli_Koltuk_Secimi();
            CustomElementWait.WaitForLoad(driver);
            var mesaj = "Yolcu Bilgileri";
            Assert.AreEqual(mesaj, Yolcu.YolcuBilgileriYazisi);
            */
            CustomElementWait.WaitForLoad(driver);
            string AdSoyad = "Faruk Kırcı";
            string TC = "13127215844";
            CustomElementWait.WaitForLoad(driver);
            OdemeYolcuBilgileri.YolcuAdSoyad(AdSoyad);
            var AccName = driver.FindElement(By.XPath("//input[@placeholder='Doldurulması zorunludur.']")).GetAttribute("value");
            Assert.AreEqual("Faruk Kırcı", AdSoyad);
            OdemeYolcuBilgileri.TcKimlik(TC);
            var phonevalue = driver.FindElement(By.XPath("//input[contains(@placeholder,'Karayolu Taşıma Yönetmeliğince gereklidir.')]")).GetAttribute("value");
            Assert.AreEqual("13127215844", TC);
            CustomElementWait.WaitForLoad(driver);
            OdemeYolcuBilgileri.TcVatandasidegilim();
            CustomElementWait.WaitForLoad(driver);
            OdemeYolcuBilgileri.UyrukAndPasaport();
            CustomElementWait.WaitForLoad(driver);
            OdemeYolcuBilgileri.HesKodu();
            CustomElementWait.WaitForLoad(driver);
        }

        [TearDown]
        public void After()
        {
            AfterMethod(TestContext.CurrentContext);
        }
    }
}
