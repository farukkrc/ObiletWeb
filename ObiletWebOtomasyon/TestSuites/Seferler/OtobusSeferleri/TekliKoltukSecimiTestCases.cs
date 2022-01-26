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

    class TekliKoltukSecimiTestCases : BaseUITestCase
    {
        public TekliKoltukSecimi TekliKoltuk;
        public OtobusBiletiBul BiletBul;
        [SetUp]
        public void GoToCreateAccountPage()
        {
            TekliKoltuk = new TekliKoltukSecimi(driver);
            BiletBul = new OtobusBiletiBul(driver);
        }

        [TestCase]
        public void TekliKoltukSecimleri()
        {
            BiletBul.SeferArama();
            CustomElementWait.WaitForLoad(driver);
            TekliKoltuk.DoluKoltukSecimi();
            CustomElementWait.WaitForLoad(driver);
            TekliKoltuk.Tekli_Dort_Koltuk_Secimi();
            CustomElementWait.WaitForLoad(driver);
            TekliKoltuk.Tekli_Uclu_Koltuk_Secimi();
            CustomElementWait.WaitForLoad(driver);
            TekliKoltuk.Tekli_Bes_Koltuk_Secimi();
            CustomElementWait.WaitForLoad(driver);
            TekliKoltuk.Tekli_iki_Koltuk_Secimi();
            CustomElementWait.WaitForLoad(driver);
            TekliKoltuk.Tekli_Tekli_Koltuk_Secimi();
            CustomElementWait.WaitForLoad(driver);
        }


        [TearDown]
        public void After()
        {
            AfterMethod(TestContext.CurrentContext);

        }
    }
}
