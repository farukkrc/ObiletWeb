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

namespace ObiletWebOtomasyon.ComponentObjects.SeferListeleme
{

    class UygunFiyatlıUcakSeferleri : BasePage
    {
        private IWebDriver driver; //web driver
        private WebDriverWait wait;// web driveri belirlenen olay gerçekleşene kadar istenilen süre kadar bekletmeyi sağlayan nesne
        private int timeoutWaitSecond = Convert.ToInt32(ConfigurationManager.AppSettings["TimeoutWaitSecond"]);
        public UygunFiyatlıUcakSeferleri(IWebDriver driver) : base(driver)
        {//her sayfadaki test driveri ilgili sayfada kullanılmasını sağlayan constructor
            this.driver = driver;
            PageFactory.InitElements(driver, this);//Bu yapı tanımlanarak [FindsBy] ek açıklaması ile ana sayfadaki web elementleri bulunabilir.
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutWaitSecond));//web driver istenilen durum gerçekleşene kadar 10 sn bekleyecek
        }

        public void UcakFiyat()
        {
            IWebElement UygunFiyat = driver.FindElement(By.XPath("//li[contains(@class, 'item flight')]"));
            UygunFiyat.Click();
            CustomElementWait.WaitForLoad(driver);
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            //sadece açılan yeni pencerede olan bir web element ile sayfanın açıldığı 
            //doğrulanıyor
           // Assert.IsTrue(driver.FindElements(By.Id("search-button")).Count > 0);
            IWebElement search = driver.FindElement(By.Id("search-button"));
            var dogrumu = search.GetAttribute("id").Contains("search-button");
            Thread.Sleep(2000);
            Assert.IsTrue(dogrumu);
            driver.Close();
            driver.SwitchTo().Window(driver.WindowHandles.First());
            CustomElementWait.WaitForLoad(driver);
            //ana pencereye geri dönüldüğünde sonradan açılan sayfada olan bir elementin
            //aktif olan sayfada olmadığını kontrol ederek doğrulanıyor
            Assert.IsFalse(driver.FindElements(By.Id("search-button")).Count > 0);
            CustomElementWait.WaitForLoad(driver);
        }
    }
}