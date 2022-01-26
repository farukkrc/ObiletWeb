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

namespace ObiletWebOtomasyon.ComponentObjects.MyTickets
{

    class OtobusBiletlerim : BasePage
    {
        private IWebDriver driver; //web driver
        private WebDriverWait wait;// web driveri belirlenen olay gerçekleşene kadar istenilen süre kadar bekletmeyi sağlayan nesne
        private int timeoutWaitSecond = Convert.ToInt32(ConfigurationManager.AppSettings["TimeoutWaitSecond"]);
        public OtobusBiletlerim(IWebDriver driver) : base(driver)
        {//her sayfadaki test driveri ilgili sayfada kullanılmasını sağlayan constructor
            this.driver = driver;
            PageFactory.InitElements(driver, this);//Bu yapı tanımlanarak [FindsBy] ek açıklaması ile ana sayfadaki web elementleri bulunabilir.
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutWaitSecond));//web driver istenilen durum gerçekleşene kadar 10 sn bekleyecek
        }


        public void Biletlerim()
        {
            driver.FindElement(By.LinkText("Biletlerim")).Click();
        }

        //Firma logosunun olup olmadığını kontrol ediyor.
        public void TurizmFirmasi()
        {
            Assert.IsTrue(driver.FindElements(By.ClassName("partner-logo")).Count > 0);      
        }


    }
}


