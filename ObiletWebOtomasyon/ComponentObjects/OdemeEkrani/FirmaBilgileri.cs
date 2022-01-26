using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Configuration;
using ObiletWebOtomasyon.Base;
using ObiletWebOtomasyon.Common;
using ObiletWebOtomasyon.ComponentObjects.BaseComponent;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;
using ObiletWebOtomasyon.Model;
using System.Collections.ObjectModel;
using OpenQA.Selenium.Interactions;
using System.Text;
using System.Threading.Tasks;

namespace ObiletWebOtomasyon.ComponentObjects.OdemeEkrani
{

    class FirmaBilgileri : BasePage
    {
        private IWebDriver driver; //web driver
        private WebDriverWait wait;// web driveri belirlenen olay gerçekleşene kadar istenilen süre kadar bekletmeyi sağlayan nesne
        private int timeoutWaitSecond = Convert.ToInt32(ConfigurationManager.AppSettings["TimeoutWaitSecond"]);
        public FirmaBilgileri(IWebDriver driver) : base(driver)
        {//her sayfadaki test driveri ilgili sayfada kullanılmasını sağlayan constructor
            this.driver = driver;
            PageFactory.InitElements(driver, this);//Bu yapı tanımlanarak [FindsBy] ek açıklaması ile ana sayfadaki web elementleri bulunabilir.
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutWaitSecond));//web driver istenilen durum gerçekleşene kadar 10 sn bekleyecek
        }

        public void Kalkis()
        {
            //Kalkış metni ile aynı metni İçerir doğruluyor.
            Assert.IsTrue(driver.FindElement(By.XPath("//th[contains(text(),'Kalkış')]")).Text.Contains("Kalkış"));
            CustomElementWait.WaitForLoad(driver);
        }

        public void Varis()
        {
            // Varış metni ile aynı metin Eşittir doğruluyor.
            Assert.IsTrue(driver.FindElement(By.XPath("//th[contains(text(),'Varış')]")).Text.Equals("Varış"));
            CustomElementWait.WaitForLoad(driver);
        }

        public void HareketZamanı()
        {
            // metnin eşit olup olmadığını kontrol ediyor.
            var time = driver.FindElement(By.XPath("//th[contains(text(),'Hareket Zamanı')]"));
            Assert.IsTrue(time.Text.Equals("Hareket Zamanı"));
        }

        public void Koltuk()
        {
            // metnin eşit olup olmadığını kontrol ediyor.
            var Koltuk = driver.FindElement(By.XPath("//th[normalize-space()='Koltuk']"));
            Assert.IsTrue(Koltuk.Text.Equals("Koltuk"));
        }

        public void İptalandDegisim()
        {
            // metnin içerip içermediğini kontrol ediyor.
            var end = driver.FindElement(By.XPath("//th[contains(text(),'İptal')]"));
            Assert.IsTrue(end.Text.Contains("İptal"));
        }

    }
}