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

    class TekliveİkiliKoltukSecimiTestCases : BaseUITestCase
    {
        public TekliveİkiliKoltukSecimi TekliveİkiliKoltuk;
        public OtobusBiletiBul BiletBul;
        [SetUp]
        public void GoToCreateAccountPage()
        {
            TekliveİkiliKoltuk = new TekliveİkiliKoltukSecimi(driver);
            BiletBul = new OtobusBiletiBul(driver);
        }


        [TestCase]
        public void TekliveİkiliKoltukSecimi()
        {
            CustomElementWait.WaitForLoad(driver);
            BiletBul.SeferArama();
            CustomElementWait.WaitForLoad(driver);
            TekliveİkiliKoltuk.Tekli_ikili_Koltuk_Secimi();
            CustomElementWait.WaitForLoad(driver);
            TekliveİkiliKoltuk.Tekli_Uc_Cifli_iki_Koltuk_Secimi();
            CustomElementWait.WaitForLoad(driver);
            TekliveİkiliKoltuk.Tekli_iki_Cifli_iki_Koltuk_Secimi();
            CustomElementWait.WaitForLoad(driver);
        }




        [TearDown]
        public void After()
        {
            AfterMethod(TestContext.CurrentContext);

        }
    }
}
