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

    class CanliDestek : BasePage
    {
        private IWebDriver driver; //web driver
        private WebDriverWait wait;// web driveri belirlenen olay gerçekleşene kadar istenilen süre kadar bekletmeyi sağlayan nesne
        private int timeoutWaitSecond = Convert.ToInt32(ConfigurationManager.AppSettings["TimeoutWaitSecond"]);
        public CanliDestek(IWebDriver driver) : base(driver)
        {//her sayfadaki test driveri ilgili sayfada kullanılmasını sağlayan constructor
            this.driver = driver;
            PageFactory.InitElements(driver, this);//Bu yapı tanımlanarak [FindsBy] ek açıklaması ile ana sayfadaki web elementleri bulunabilir.
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutWaitSecond));//web driver istenilen durum gerçekleşene kadar 10 sn bekleyecek
        }


        public void CanliDestek_Pop_up()
        {
            // Canlı Destek pop-up gelene kadar bekleme
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(110));
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("support-toggle")));
            CustomElementWait.WaitForLoad(driver);
            driver.FindElement(By.Id("support-toggle")).Click();
            CustomElementWait.WaitForLoad(driver);

            // canlı destek Widget
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//iframe[@id='webWidget']")));
            Thread.Sleep(3000);
            // Canlı destek pop-up açıldığını bir element ile doğruluyor.
            Assert.IsTrue(driver.FindElements(By.XPath("//button[contains(text(),'Canlı destek başlat')]")).Count > 0);
            Thread.Sleep(TimeSpan.FromSeconds(1));
            driver.FindElement(By.XPath("//button[contains(text(),'Canlı destek başlat')]")).Click();
            Thread.Sleep(TimeSpan.FromSeconds(2));
            
        }

        public void Name()
        {
          
            IWebElement ad = driver.FindElement(By.Name("name"));
            ad.Click();
            ad.SendKeys("Faruk KIRCI");
            Thread.Sleep(TimeSpan.FromSeconds(1));
        }

        public void Email()
        {
            IWebElement email = driver.FindElement(By.Name("email"));
            email.Click();
            email.SendKeys("faruk.kirci@obilet.com");
            Thread.Sleep(TimeSpan.FromSeconds(1));
        }

        public void Phone()
        {
            IWebElement Phone = driver.FindElement(By.Name("phone"));
            Phone.Click();
            Phone.SendKeys("5538596190");
            Thread.Sleep(TimeSpan.FromSeconds(1));
        }

        public void Message()
        {
            IWebElement Mesaj = driver.FindElement(By.Name("message"));
            Mesaj.Click();
            Mesaj.SendKeys("Text");
            Thread.Sleep(TimeSpan.FromSeconds(1));
        }

        public void Baglan()
        {
            driver.FindElement(By.XPath("//button[contains(text(),'Canlı destek başlat')]")).Click();
            Thread.Sleep(TimeSpan.FromSeconds(2));
        }

        public void CanliDestekSonlandir()
        {
            // Canlı destek sohbet açıldığı bir element ile doğruluyoruz.
            Assert.IsTrue(driver.FindElements(By.XPath("//img[@alt='avatar']")).Count > 0);

            driver.FindElement(By.CssSelector("button[aria-label='Canlı desteği sonlandır']")).Click();
            Thread.Sleep(TimeSpan.FromSeconds(2));
            driver.FindElement(By.XPath("//button[contains(text(),'Sonlandır')]")).Click();
            Thread.Sleep(TimeSpan.FromSeconds(2));
            driver.FindElement(By.XPath("//button[@aria-label='Pencere öğesini simge durumuna küçült']")).Click();
            Thread.Sleep(TimeSpan.FromSeconds(2));
        }
    }
}
