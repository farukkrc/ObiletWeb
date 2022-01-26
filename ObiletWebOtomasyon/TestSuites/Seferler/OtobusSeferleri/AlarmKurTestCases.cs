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

    class AlarmKurTestCases : BaseUITestCase
    {
        public AlarmKur Alarm;
        public OtobusBiletiBul BiletBul;
        public HızlıFiltreler hizliFiltre;
        [SetUp]
        public void GoToCreateAccountPage()
        {
            Alarm = new AlarmKur(driver);
            BiletBul = new OtobusBiletiBul(driver);
            hizliFiltre = new HızlıFiltreler(driver);
        }


        [TestCase]
        public void AlarmKurmaButonu()
        {
            BiletBul.SeferArama();
            CustomElementWait.WaitForLoad(driver);
            hizliFiltre.Bekle();
            CustomElementWait.WaitForLoad(driver);
            Alarm.AlarmKurma();
            CustomElementWait.WaitForLoad(driver);
  
        }
  

        [TearDown]
        public void After()
        {
            AfterMethod(TestContext.CurrentContext);

        }
    }
}
