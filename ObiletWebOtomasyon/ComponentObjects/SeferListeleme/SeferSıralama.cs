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

    class SeferSıralama : BasePage
    {
        private IWebDriver driver; //web driver
        private WebDriverWait wait;// web driveri belirlenen olay gerçekleşene kadar istenilen süre kadar bekletmeyi sağlayan nesne
        private int timeoutWaitSecond = Convert.ToInt32(ConfigurationManager.AppSettings["TimeoutWaitSecond"]);
        public SeferSıralama(IWebDriver driver) : base(driver)
        {//her sayfadaki test driveri ilgili sayfada kullanılmasını sağlayan constructor
            this.driver = driver;
            PageFactory.InitElements(driver, this);//Bu yapı tanımlanarak [FindsBy] ek açıklaması ile ana sayfadaki web elementleri bulunabilir.
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutWaitSecond));//web driver istenilen durum gerçekleşene kadar 10 sn bekleyecek
        }

        #region Sıralama Elementleri
        [FindsBy(How = How.Id, Using = "sorting")]
        private IWebElement Sirala;
        [FindsBy(How = How.CssSelector, Using = "#sorting > ul > li:nth-child(2) > button")]
        private IWebElement FıyataGore;
        [FindsBy(How = How.CssSelector, Using = "#sorting > ul > li:nth-child(1) > button")]
        private IWebElement KalkisSaati;
        #endregion

        #region Sıralama Fonksiyonları
        public void SiralamaFiltreleri()
        {
            CustomElementWait.WaitUntilElementClickable(driver, Sirala);
            Sirala.Click();
            CustomElementWait.WaitForLoad(driver);
            CustomElementWait.WaitUntilElementClickable(driver, FıyataGore);
            FıyataGore.Click();
            CustomElementWait.WaitForLoad(driver);
            CustomElementWait.WaitUntilElementClickable(driver, Sirala);
            Sirala.Click();
            CustomElementWait.WaitForLoad(driver);
            CustomElementWait.WaitUntilElementClickable(driver, KalkisSaati);
            KalkisSaati.Click();
            CustomElementWait.WaitForLoad(driver);

        }
        #endregion

    }
}

