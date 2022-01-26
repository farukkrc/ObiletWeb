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

    class OtobusBiletiBulTestCases : BaseUITestCase
    {
        public OtobusBiletiBul BiletBul;
        [SetUp]
        public void GoToCreateAccountPage()
        {
            BiletBul = new OtobusBiletiBul(driver);
        }

        [TestCase] // Otobus Bileti Bul Case
        public void OtobusBiletiArama()
        {
            BiletBul.SeferArama();
        }

        [TestCase] // Sefer listeleme ekranında Arama düzenleme
        public void SeferListelemeAramayiDuzenleTestCases()
        {
            //BiletBul.SeferArama();          // caseleri tek tek çalıştıracaksan bunu aç
            CustomElementWait.WaitForLoad(driver);
            Thread.Sleep(5000);
            BiletBul.SeferListelemeNereden();
            CustomElementWait.WaitForLoad(driver);
            BiletBul.SeferListelemeNereye();
            CustomElementWait.WaitForLoad(driver);
            BiletBul.YondegistirmeButonu();
            CustomElementWait.WaitForLoad(driver);
            BiletBul.YenidenArama();
            CustomElementWait.WaitForLoad(driver);
            Thread.Sleep(2000);
            BiletBul.TakvimTarihsecimi();
            CustomElementWait.WaitForLoad(driver);
        }

        [TearDown]
        public void After()
        {
            AfterMethod(TestContext.CurrentContext);

        }
    }
}
