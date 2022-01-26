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

    class TümFiltrelerTestCases : BaseUITestCase
    {
        public TümFiltreler  TumFiltre;
        public OtobusBiletiBul BiletBul;
        [SetUp]
        public void GoToCreateAccountPage()
        {
            TumFiltre = new TümFiltreler(driver);
            BiletBul = new OtobusBiletiBul(driver);
        }

        [TestCase]
        public void TümFiltreler()
        {
            BiletBul.SeferArama();
            CustomElementWait.WaitForLoad(driver);
            TumFiltre.TumFiltreler();
            CustomElementWait.WaitForLoad(driver);
            TumFiltre.Firmayagore();
            CustomElementWait.WaitForLoad(driver);
            TumFiltre.FirmaKalkisNoktasi();
            CustomElementWait.WaitForLoad(driver);
            TumFiltre.FirmaVarisnoktasi();
            CustomElementWait.WaitForLoad(driver);
            TumFiltre.FirmaSefertipi();
            CustomElementWait.WaitForLoad(driver);
        }


        [TearDown]
        public void After()
        {
            AfterMethod(TestContext.CurrentContext);

        }
    }
}
