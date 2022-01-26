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

    class HızlıFiltrelerTestCases : BaseUITestCase
    {
        public HızlıFiltreler hizliFiltre;
        public OtobusBiletiBul BiletBul;
        [SetUp]
        public void GoToCreateAccountPage()
        {
            hizliFiltre = new HızlıFiltreler(driver);
            BiletBul = new OtobusBiletiBul(driver);
        }

        [TestCase]
        public void HizliFiltrelerButonları()
        {
            BiletBul.SeferArama();
            CustomElementWait.WaitForLoad(driver);
            hizliFiltre.Bekle();
            CustomElementWait.WaitForLoad(driver);
            hizliFiltre.HizliFiltre();
            CustomElementWait.WaitForLoad(driver);
        }


        [TearDown]
        public void After()
        {
            AfterMethod(TestContext.CurrentContext);

        }

    }
}
