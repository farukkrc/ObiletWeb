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
using OpenQA.Selenium.Chrome;

namespace ObiletWebOtomasyon.TestSuites.OdemeSayfasi.OdemeBilgileriEkrani
{
    [TestFixture]
    [AllureEpic("AllureEpic")] // kullanıma göre özelleştirilebilir
    [AllureFeature("APITests")] // kullanıma göre özelleştirilebilir
    [AllureParentSuite("AllureParentSuite")] // kullanıma göre özelleştirilebilir
    [AllureSuite("AllureSuite")] // kullanıma göre özelleştirilebilir
    [AllureTag("AllureTag", "Get")] // kullanıma göre özelleştirilebilir
    [AllureSeverity]

    class CanliDestekTestCases : BaseUITestCase
    {
        public CanliDestek Canli;
        public FirmaBilgileri Firma;
        public OtobusBiletiBul BiletBul;
        public TekliKoltukSecimi TekKoltuk;
        public OdemeBilgileri Odeme;
        

        [SetUp]
        public void GoToCreateAccountPage()
        {
            
            Canli = new CanliDestek(driver);
            Firma = new FirmaBilgileri(driver);
            BiletBul = new OtobusBiletiBul(driver);
            TekKoltuk = new TekliKoltukSecimi(driver);
            Odeme = new OdemeBilgileri(driver);
        }

        [Test]
        [Order(1)]
        public void CanliDestekButonu()
        {
            BiletBul.SeferArama();
            TekKoltuk.Tekli_Tekli_Koltuk_Secimi();
            Canli.CanliDestek_Pop_up();
            Canli.Name();
            Canli.Email();
            Canli.Phone();
            Canli.Message();
            Canli.Baglan();
            Canli.CanliDestekSonlandir();
        }

        [TearDown]
        public void After()
        {
            AfterMethod(TestContext.CurrentContext);

        }

    }
}
