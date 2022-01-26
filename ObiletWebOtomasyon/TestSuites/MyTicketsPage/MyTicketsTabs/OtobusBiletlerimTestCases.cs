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
using ObiletWebOtomasyon.ComponentObjects.MyTickets;

namespace ObiletWebOtomasyon.TestSuites.MyTicketsPage.MyTicketsTabs
{
    [TestFixture]
    [AllureEpic("AllureEpic")] // kullanıma göre özelleştirilebilir
    [AllureFeature("APITests")] // kullanıma göre özelleştirilebilir
    [AllureParentSuite("AllureParentSuite")] // kullanıma göre özelleştirilebilir
    [AllureSuite("AllureSuite")] // kullanıma göre özelleştirilebilir
    [AllureTag("AllureTag", "Get")] // kullanıma göre özelleştirilebilir
    [AllureSeverity]

    class OtobusBiletlerimTestCases : BaseUITestCase
    {
        public OtobusBiletlerim Bilet;
        public HomePageUyeGiris Giris;

        [SetUp]
        public void GoToCreateAccountPage()
        {

            Bilet = new OtobusBiletlerim(driver);
            Giris = new HomePageUyeGiris(driver);
           
        }

        [Test]
        [Order(1)]
        public void Biletlerim()
        {
            Giris.Button();
            string email = "faruk.kirci@obilet.com";
            Thread.Sleep(TimeSpan.FromSeconds(1));
            string password = "123456";
            Giris.Login(email, password);
            CustomElementWait.WaitForLoad(driver);
            Bilet.Biletlerim();
            CustomElementWait.WaitForLoad(driver);
            Bilet.TurizmFirmasi();
            CustomElementWait.WaitForLoad(driver);

        }



        [TearDown]
        public void After()
        {
            AfterMethod(TestContext.CurrentContext);

        }

    }
}
