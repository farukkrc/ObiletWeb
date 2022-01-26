using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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
using OpenQA.Selenium.Interactions;

namespace ObiletWebOtomasyon.TestSuites.OdemeSayfasi.OdemeBilgileriEkrani
{

    [TestFixture]
    [AllureEpic("AllureEpic")] // kullanıma göre özelleştirilebilir
    [AllureFeature("APITests")] // kullanıma göre özelleştirilebilir
    [AllureParentSuite("AllureParentSuite")] // kullanıma göre özelleştirilebilir
    [AllureSuite("AllureSuite")] // kullanıma göre özelleştirilebilir
    [AllureTag("AllureTag", "Get")] // kullanıma göre özelleştirilebilir
    [AllureSeverity]

    class OdemeBilgileriTestCases : BaseUITestCase
    {
        public OdemeBilgileri Odeme;
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
            Odeme = new OdemeBilgileri(driver);
            OdemeiletisimEkrani = new İletisimBilgileri(driver);
        }

        [TestCase]
        [Order(1)]
        public void OdemeBilgilerininBosBirakilmasi()
        {
            BiletBul.SeferArama();
            CustomElementWait.WaitForLoad(driver);
            TekliKoltuk.Tekli_Tekli_Koltuk_Secimi();
            CustomElementWait.WaitForLoad(driver);
            var mesaj = "Ödeme Bilgileri";
            Assert.AreEqual(mesaj, Odeme.OdemeBilgileriYazisi);
            CustomElementWait.WaitForLoad(driver);
            driver.FindElement(By.Id("pay")).Click();
            Odeme.KartNumarasiBos();
            Odeme.SonKullanmaTarihiBos();
            Odeme.CVC2Bosbirakilmasi();
            CustomElementWait.WaitForLoad(driver);
            string errorMessage = Odeme.returnErrorMessage(ComponentObjects.OdemeEkrani.OdemeBilgileri.ErrorMessages.KartNumarasiBosBiralimaz);
            string ErrorMessage = Odeme.returnErrorMessage(ComponentObjects.OdemeEkrani.OdemeBilgileri.ErrorMessages.SonKullanimTarihiBosBirakilamaz);
            string EerrorMessage = Odeme.returnErrorMessage(ComponentObjects.OdemeEkrani.OdemeBilgileri.ErrorMessages.CVC2BosBirakilamaz);
            Assert.AreEqual("Boş bırakılamaz.", errorMessage, "KArt Numarasi boş bırakıldığında beklenen hata mesajı gösterilmedi.");
            Assert.AreEqual("Boş bırakılamaz.", ErrorMessage, "Son Kullanma Tarihi boş bırakıldığında beklenen hata mesaji gösterilmelidir.");
            Assert.AreEqual("Boş bırakılamaz.", EerrorMessage, "CVC2 alanı boş bırakıldığında beklenen hata mesaji gösterilmelidir.");
            CustomElementWait.WaitForLoad(driver);
        }

        [TestCase]
        [Order(2)]
        public void OdemeBilgilerininGecersizOlmasi()
        {
            //Tek Tek caseler çalışıcaksa bunu açalım

            //BiletBul.SeferArama();
            //CustomElementWait.WaitForLoad(driver);
            //TekliKoltuk.Tekli_Tekli_Koltuk_Secimi();
            //CustomElementWait.WaitForLoad(driver);
            //var mesaj = "Ödeme Bilgileri";
            //Assert.AreEqual(mesaj, Odeme.OdemeBilgileriYazisi);
            //CustomElementWait.WaitForLoad(driver);
            //driver.FindElement(By.Id("pay")).Click();


            CustomElementWait.WaitForLoad(driver);
            Odeme.KartNumarasi();
            CustomElementWait.WaitForLoad(driver);
            Actions action = new Actions(driver);
            action.SendKeys(Keys.Control + "4444 5555 8888 9999").Build().Perform();
            CustomElementWait.WaitForLoad(driver);

            Odeme.SonKullanmaTarihi();
            CustomElementWait.WaitForLoad(driver);
            Actions actions = new Actions(driver);
            actions.SendKeys(Keys.Control + "00 99").Build().Perform();
            CustomElementWait.WaitForLoad(driver);

            Odeme.CVC2();
            CustomElementWait.WaitForLoad(driver);
            Actions actio = new Actions(driver);
            actio.SendKeys(Keys.Control + "88").Build().Perform();
            CustomElementWait.WaitForLoad(driver);

            CustomElementWait.WaitForLoad(driver);
            string errorMessage = Odeme.returnErrorMessage(ComponentObjects.OdemeEkrani.OdemeBilgileri.ErrorMessages.KartNumarasiGecersiz);
            string ErrorMessage = Odeme.returnErrorMessage(ComponentObjects.OdemeEkrani.OdemeBilgileri.ErrorMessages.SonKullanimTarihiGecersiz);
            string EerrorMessage = Odeme.returnErrorMessage(ComponentObjects.OdemeEkrani.OdemeBilgileri.ErrorMessages.CVC2Gecersiz);
            Assert.AreEqual("Geçersiz banka/kredi kartı numarası.", errorMessage, "Geçersiz banka/kredi kartı geçersiz giriş yapıldığında beklenen hata mesajı gösterilmedi.");
            Assert.AreEqual("Geçersiz son kullanma tarihi.", ErrorMessage, "Geçersiz son kullanma tarihi geçersiz giriş yapıldığında beklenen hata mesaji gösterilmelidir.");
            Assert.AreEqual("Geçersiz CVC2.", EerrorMessage, "Geçersiz CVC2.geçersiz giriş yapıldığında beklenen hata mesaji gösterilmelidir.");
            CustomElementWait.WaitForLoad(driver);
        }

        [TestCase]
        [Order (3)]
        public void MasterPassKartEkleme()
        {
            //Tek Tek caseler çalışıcaksa bunu açalım
            //CustomElementWait.WaitForLoad(driver);
            //BiletBul.SeferArama();
            //CustomElementWait.WaitForLoad(driver);
            //TekliKoltuk.Tekli_Tekli_Koltuk_Secimi();
            string Email = "Faruk.kirci@obilet.com";
            string Telefon = "5538596190";
            OdemeiletisimEkrani.İletisimEmail(Email);
            OdemeiletisimEkrani.İletisimPhone(Telefon);
            CustomElementWait.WaitForLoad(driver);
            string AdSoyad = "Faruk Kırcı";
            string TC = "13127215844";
            OdemeYolcuBilgileri.YolcuAdSoyad(AdSoyad);
            OdemeYolcuBilgileri.TcKimlik(TC);
            CustomElementWait.WaitForLoad(driver);
            OdemeYolcuBilgileri.HesKodu();
            CustomElementWait.WaitForLoad(driver);

            Odeme.KartNumarasi();
            Actions action = new Actions(driver);
            action.SendKeys(Keys.Control + "4410 7771 0883 0182").Build().Perform();

            CustomElementWait.WaitForLoad(driver);
            Odeme.SonKullanmaTarihi();
            Actions actions = new Actions(driver);
            actions.SendKeys(Keys.Control + "01 25").Build().Perform();

            Odeme.CVC2();
            Actions actio = new Actions(driver);
            actio.SendKeys(Keys.Control + "049").Build().Perform();

            Odeme.MasterPassKullanimKosullari();
            Odeme.MasterPassNedir();
            Odeme.MasterPassCheckbox();
            CustomElementWait.WaitForLoad(driver);

            //Not
            /*
             MasterPass pop-up gelen sms ekrana giriş yapılamadığı için bu adım pop-up çıkana kadar devam ediyor
             Daha sonra nasıl devam edilecek konuşulup araştırılmalıdır.
             */

        }

        [TestCase]
        [Order(4)]
        public void OdemeBilgisiBasarili()
        {
            //Tek Tek caseler çalışıcaksa bunu açalım
            //CustomElementWait.WaitForLoad(driver);
            //BiletBul.SeferArama();
            //CustomElementWait.WaitForLoad(driver);
            //TekliKoltuk.Tekli_Tekli_Koltuk_Secimi();
            string Email = "Faruk.kirci@obilet.com";
            string Telefon = "5538596190";
            OdemeiletisimEkrani.İletisimEmail(Email);
            OdemeiletisimEkrani.İletisimPhone(Telefon);
            OdemeiletisimEkrani.UcretsizSMS();
            CustomElementWait.WaitForLoad(driver);
            string AdSoyad = "Faruk Kırcı";
            string TC = "13127215844";
            OdemeYolcuBilgileri.YolcuAdSoyad(AdSoyad);
            OdemeYolcuBilgileri.TcKimlik(TC);
            CustomElementWait.WaitForLoad(driver);
            OdemeYolcuBilgileri.HesKodu();
            CustomElementWait.WaitForLoad(driver);
            OdemeYolcuBilgileri.SeyahatSigortasi();

            Odeme.KartNumarasi();
            Actions action = new Actions(driver);
            action.SendKeys(Keys.Control + "4410 7772 3312 7058").Build().Perform();

            Odeme.SonKullanmaTarihi();
            Actions actions = new Actions(driver);
            actions.SendKeys(Keys.Control + "01 25").Build().Perform();

            Odeme.CVC2Nedir();
            Odeme.CVC2();
            Actions actio = new Actions(driver);
            actio.SendKeys(Keys.Control + "515").Build().Perform();
            Odeme.SozlesmeBox();
            Odeme.SozlesmeBox();
            Odeme.Onbilgilendirme();
            Odeme.MesafeliSatisSozlesmesi();
            Odeme.MasterPassKullanimKosullari();
            Odeme.MasterPassNedir();
            Odeme.GuvenliOdemeYap();

            //Not
            /*
             3D ekranına yönlendirdikten sonra gelen sms bilgileri girişi yapılamadığı için 3D ekranında case
             sonlandırılıyor. Daha sonra nasıl devam edilmesi için araştırılıp bilgi alınmalıdır.
             */
        }

        [TearDown]
        public void After()
        {
            AfterMethod(TestContext.CurrentContext);
        }
    }
}
