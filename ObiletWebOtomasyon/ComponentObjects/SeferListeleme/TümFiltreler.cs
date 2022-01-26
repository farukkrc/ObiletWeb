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

    class TümFiltreler : BasePage
    {
        private IWebDriver driver; //web driver
        private WebDriverWait wait;// web driveri belirlenen olay gerçekleşene kadar istenilen süre kadar bekletmeyi sağlayan nesne
        private int timeoutWaitSecond = Convert.ToInt32(ConfigurationManager.AppSettings["TimeoutWaitSecond"]);
        public TümFiltreler(IWebDriver driver) : base(driver)
        {//her sayfadaki test driveri ilgili sayfada kullanılmasını sağlayan constructor
            this.driver = driver;
            PageFactory.InitElements(driver, this);//Bu yapı tanımlanarak [FindsBy] ek açıklaması ile ana sayfadaki web elementleri bulunabilir.
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutWaitSecond));//web driver istenilen durum gerçekleşene kadar 10 sn bekleyecek
        }

        #region Tüm Filtrelerin Elementleri
        [FindsBy(How = How.CssSelector, Using = "#quick-filters-container > ul > li:nth-child(6) > button")]
        private IWebElement HizliFiltrelerButton;
        #endregion

        #region Tüm filtrelerin fonksiyonları
        public void TumFiltreler()
        {
            CustomElementWait.WaitUntilElementClickable(driver, HizliFiltrelerButton);
            HizliFiltrelerButton.Click();
            CustomElementWait.WaitForLoad(driver);
            HizliFiltrelerButton.Click();
            CustomElementWait.WaitForLoad(driver);
            HizliFiltrelerButton.Click();
            CustomElementWait.WaitForLoad(driver);
        }
        #endregion

        #region Firmaya göre elementler
        [FindsBy(How = How.CssSelector, Using = "#filters-bar > ob-dropdown:nth-child(1) > div.label")]
        private IWebElement Firmalar;
        #endregion

        #region Firmaya göre fonksiyonlar
        public void Firmayagore()
        {
            CustomElementWait.WaitUntilElementClickable(driver, Firmalar);
            Firmalar.Click();
            CustomElementWait.WaitForLoad(driver);
            // Firmaya Göre listesinden turizm firmaları seçiyoruz.
            System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> labels = driver.FindElements(By.XPath("//*[@id='filters-bar']/ob-dropdown[1]/div[2]/div/ul/li"));
            string[] partnerList = { "Metro", "Kamil", "Pamukkale", "Nilüfer", "Varan" };

            foreach (string partner in partnerList)
            {
                var hasSelectedPartners = labels.FirstOrDefault(x => x.Text.Contains(partner));
                if (hasSelectedPartners != null)
                {
                    hasSelectedPartners.Click();
                }
            }
        }
        #endregion

        #region Firmaya göre Kalkış noktası elementleri
        [FindsBy(How = How.CssSelector, Using = "#filters-bar > ob-dropdown:nth-child(2) > div.label")]
        private IWebElement TumKalkisNoktasi;
        #endregion

        #region Firmaya göre Kalkış noktası fonksiyonlar
        public void FirmaKalkisNoktasi()
        {
            CustomElementWait.WaitUntilElementClickable(driver, TumKalkisNoktasi);
            TumKalkisNoktasi.Click();
            CustomElementWait.WaitForLoad(driver);
            // Firmaya Göre listesinden Kalkiş noktalarını seçiyoruz.
            System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> labels = driver.FindElements(By.XPath("//*[@id='filters-bar']/ob-dropdown[2]//div/ul/li"));
            string[] originList = { "Ataşehir", "Kaynarca", "Samandıra" };

            foreach (string origin in originList)
            {
                var hasSelectedPartners = labels.FirstOrDefault(x => x.Text.Contains(origin));
                if (hasSelectedPartners != null)
                {
                    hasSelectedPartners.Click();
                }
            }
        }
        #endregion

        #region Firmaya göre Varış noktası elementleri
        [FindsBy(How = How.CssSelector, Using = "#filters-bar > ob-dropdown:nth-child(3) > div.label")]
        private IWebElement TumVarisnoktasi;
        #endregion

        #region Firmaya göre Varış noktası fonksiyonları
        public void FirmaVarisnoktasi()
        {
            CustomElementWait.WaitUntilElementClickable(driver, TumVarisnoktasi);
            TumVarisnoktasi.Click();
            CustomElementWait.WaitForLoad(driver);
            // Firmaya Göre listesinden Varış noktalarını seçiyoruz.
            System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> labels = driver.FindElements(By.XPath("//*[@id='filters-bar']/ob-dropdown[3]/div[2]/div/ul/li"));
            string[] destinationList = { "Evka", "İzmir" };

            foreach (var destination in destinationList)
            {
                var hasSelectedPartners = labels.FirstOrDefault(x => x.Text.Contains(destination));
                if (hasSelectedPartners != null)
                {
                    hasSelectedPartners.Click();
                }
            }
        }
        #endregion

        #region Firmaya göre Sefer Tipi elementleri
        [FindsBy(How = How.CssSelector, Using = "#filters-bar > ob-dropdown:nth-child(4) > div.label")]
        private IWebElement Tumsefertipi;
        #endregion

        #region Firmaya göre Sefer Tipi fonksiyonları
        public void FirmaSefertipi()
        {
            CustomElementWait.WaitUntilElementClickable(driver, Tumsefertipi);
            Tumsefertipi.Click();
            CustomElementWait.WaitForLoad(driver);
            // Firmaya Göre listesinden sefer tipi seçiyoruz.
            System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> labels = driver.FindElements(By.XPath("//*[@id='filters-bar']/ob-dropdown[4]/div[2]/div/ul/li"));
            string[] typeList = { "2+1", "1+1", "2+2" };
            foreach (var type in typeList)
            {
                var hasSelectedPartners = labels.FirstOrDefault(x => x.Text.Contains(type));
                if (hasSelectedPartners != null)
                {
                    hasSelectedPartners.Click();

                }

            }
            CustomElementWait.WaitForLoad(driver);
            Tumsefertipi.Click();
        }
        #endregion

    }
}