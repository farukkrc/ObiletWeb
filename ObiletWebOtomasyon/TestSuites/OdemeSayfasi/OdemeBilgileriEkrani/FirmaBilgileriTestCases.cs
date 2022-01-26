using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using System.Threading;
using ObiletWebOtomasyon.Base;
using ObiletWebOtomasyon.Common.Generator;
using ObiletWebOtomasyon.ComponentObjects.HomePage;
using OpenQA.Selenium;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using ObiletWebOtomasyon.Common;
using ObiletWebOtomasyon.ComponentObjects.SeferListeleme;
using ObiletWebOtomasyon.ComponentObjects.OdemeEkrani;

namespace ObiletWebOtomasyon.TestSuites.OdemeSayfasi.OdemeBilgileriEkrani
{
    [TestFixture]
    [AllureEpic("AllureEpic")] // kullanıma göre özelleştirilebilir
    [AllureFeature("APITests")] // kullanıma göre özelleştirilebilir
    [AllureParentSuite("AllureParentSuite")] // kullanıma göre özelleştirilebilir
    [AllureSuite("AllureSuite")] // kullanıma göre özelleştirilebilir
    [AllureTag("AllureTag", "Get")] // kullanıma göre özelleştirilebilir
    [AllureSeverity]

    class FirmaBilgileriTestCases : BaseUITestCase
    {
        public FirmaBilgileri Firma;
        public OtobusBiletiBul BiletBul;
        public TekliKoltukSecimi TekKoltuk;
        public OdemeBilgileri Odeme;


        [SetUp]
        public void GoToCreateAccountPage()
        {
            Firma = new FirmaBilgileri(driver);
            BiletBul = new OtobusBiletiBul(driver);
            TekKoltuk = new TekliKoltukSecimi(driver);
            Odeme = new OdemeBilgileri(driver);

        }

        [TestCase]
        [Order(1)]
        public void FirmaBiletBilgileri()
        {

            BiletBul.SeferArama();
            TekKoltuk.Tekli_Tekli_Koltuk_Secimi();
            Firma.Kalkis();
            Firma.Varis();
            Firma.HareketZamanı();
            Firma.Koltuk();
            Firma.İptalandDegisim();
        }

        [TearDown]
        public void After()
        {
            AfterMethod(TestContext.CurrentContext);

        }

    }
}
