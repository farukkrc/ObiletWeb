using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Configuration;
using ObiletWebOtomasyon.Base;
using ObiletWebOtomasyon.Common;
using ObiletWebOtomasyon.ComponentObjects.BaseComponent;
using System.Threading;

namespace ObiletWebOtomasyon.ComponentObjects.SeferListeleme
{
    class OtobusBiletiBul : BasePage
    {
        private IWebDriver driver; //web driver
        private WebDriverWait wait;// web driveri belirlenen olay gerçekleşene kadar istenilen süre kadar bekletmeyi sağlayan nesne
        private int timeoutWaitSecond = Convert.ToInt32(ConfigurationManager.AppSettings["TimeoutWaitSecond"]);
        public OtobusBiletiBul(IWebDriver driver) : base(driver)
        {//her sayfadaki test driveri ilgili sayfada kullanılmasını sağlayan constructor
            this.driver = driver;
            PageFactory.InitElements(driver, this);//Bu yapı tanımlanarak [FindsBy] ek açıklaması ile ana sayfadaki web elementleri bulunabilir.
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutWaitSecond));//web driver istenilen durum gerçekleşene kadar 10 sn bekleyecek
        }

        // Anasayfa Otobüs Bileti Bul element ve Fonksiyonları tanımlıyoruz
        #region SearchModalFactory
        public void SeferArama()
        {

            CustomElementWait.WaitForLoad(driver);
            Thread.Sleep(2000);
            IWebElement Nereden = driver.FindElement(By.Id("origin"));
            Nereden.Click();
            CustomElementWait.WaitForLoad(driver);
            IWebElement NeredeInput = driver.FindElement(By.Id("origin-input"));
            NeredeInput.Click();
            CustomElementWait.WaitForLoad(driver);
            NeredeInput.SendKeys("istanbul Anadolu");
            CustomElementWait.WaitForLoad(driver);
            NeredeInput.SendKeys(Keys.Enter);
            CustomElementWait.WaitForLoad(driver);
            IWebElement Nereye = driver.FindElement(By.Id("destination"));
            Nereye.Click();
            CustomElementWait.WaitForLoad(driver);
            IWebElement NereyeInput = driver.FindElement(By.Id("destination-input"));
            NereyeInput.Click();
            CustomElementWait.WaitForLoad(driver);
            NereyeInput.SendKeys("Ankara");
            CustomElementWait.WaitForLoad(driver);
            NereyeInput.SendKeys(Keys.Enter);
            CustomElementWait.WaitForLoad(driver);
            IWebElement Bugun = driver.FindElement(By.Id("today"));
            Bugun.Click();
            CustomElementWait.WaitForLoad(driver);
            IWebElement Yarin = driver.FindElement(By.Id("tomorrow"));
            Yarin.Click();
            CustomElementWait.WaitForLoad(driver);
            CustomElementWait.WaitForLoad(driver);
            IWebElement Search = driver.FindElement(By.Id("search-button"));
            Search.Click();
            CustomElementWait.WaitForLoad(driver);
        }
        #endregion

        #region SeferListeleme Ekranında Nereden-Nereye Elementleri
        private const string SeferModal = "//div[contains(@class,'container')]";
        [FindsBy(How = How.XPath, Using = SeferModal)]
        private IWebElement SeferlistModal;
        [FindsBy(How = How.Id, Using = "origin")]
        private IWebElement Nereden;
        [FindsBy(How = How.XPath, Using = "//*[@id='origin']/div/ul/li[4]")]
        private IWebElement Listbox;
        [FindsBy(How = How.XPath, Using = "//*[@id='origin']/div/ul/li[6]")]
        private IWebElement ListboxTwo;
        [FindsBy(How = How.XPath, Using = "//*[@id='origin']/div/ul/li[1]")]
        private IWebElement ListboxThree;

        [FindsBy(How = How.Id, Using = "destination")]
        private IWebElement Nereye;
        [FindsBy(How = How.CssSelector, Using = "#destination > div > ul > li:nth-child(4)")]
        private IWebElement NereyeList;
        [FindsBy(How = How.CssSelector, Using = "#destination > div > ul > li:nth-child(5)")]
        private IWebElement NereyeListTwo;
        [FindsBy(How = How.CssSelector, Using = "#destination > div > ul > li:nth-child(7)")]
        private IWebElement NereyeListThree;
        #endregion

        #region Sefer Listeleme Nereden seçimleri fonksiyonları
        public void SeferListelemeNereden()
        {

            //Thread.Sleep(3000);
            CustomElementWait.WaitForLoad(driver);
            CustomElementWait.WaitUntilElementVisible(driver, By.XPath(SeferModal));
            CustomElementWait.WaitForLoad(driver);
            CustomElementWait.WaitUntilElementClickable(driver, Nereden);
            Nereden.Click();
            CustomElementWait.WaitForLoad(driver);
            CustomElementWait.WaitUntilElementClickable(driver, Listbox);
            Listbox.Click();
            CustomElementWait.WaitForLoad(driver);
            CustomElementWait.WaitUntilElementClickable(driver, Nereden);
            Nereden.Click();
            CustomElementWait.WaitForLoad(driver);
            CustomElementWait.WaitUntilElementClickable(driver, ListboxTwo);
            ListboxTwo.Click();
            CustomElementWait.WaitForLoad(driver);
            CustomElementWait.WaitUntilElementClickable(driver, Nereden);
            Nereden.Click();
            CustomElementWait.WaitForLoad(driver);
            CustomElementWait.WaitUntilElementClickable(driver, ListboxThree);
            ListboxThree.Click();
            Thread.Sleep(3000);

        }
        #endregion

        #region Sefer Listeleme Nereye seçimleri fonksiyonları
        public void SeferListelemeNereye()
        {
            CustomElementWait.WaitForLoad(driver);
            CustomElementWait.WaitUntilElementClickable(driver, Nereye);
            Nereye.Click();
            CustomElementWait.WaitForLoad(driver);
            CustomElementWait.WaitUntilElementClickable(driver, NereyeList);
            NereyeList.Click();
            CustomElementWait.WaitForLoad(driver);
            CustomElementWait.WaitUntilElementClickable(driver, Nereye);
            Nereye.Click();
            CustomElementWait.WaitForLoad(driver);
            CustomElementWait.WaitUntilElementClickable(driver, NereyeListTwo);
            NereyeListTwo.Click();
            CustomElementWait.WaitForLoad(driver);
            CustomElementWait.WaitUntilElementClickable(driver, Nereye);
            Nereye.Click();
            CustomElementWait.WaitForLoad(driver);
            CustomElementWait.WaitUntilElementClickable(driver, NereyeListThree);
            NereyeListThree.Click();
            CustomElementWait.WaitForLoad(driver);
        }
        #endregion

        #region Yön değiştir butonunun elementleri
        [FindsBy(How = How.Id, Using = "swap")]
        private IWebElement YonButonu;
        #endregion

        #region Yön değiştirme butonunun fonksiyonu
        public void YondegistirmeButonu()
        {
            CustomElementWait.WaitUntilElementClickable(driver, YonButonu);
            YonButonu.Click();
            CustomElementWait.WaitForLoad(driver);
            CustomElementWait.WaitUntilElementClickable(driver, YonButonu);
            YonButonu.Click();
            CustomElementWait.WaitForLoad(driver);
        }
        #endregion

        #region Yeniden Ara butonunun Elementleri
        [FindsBy(How = How.CssSelector, Using = "#search > div > button.edit.button > span.search-text")]
        private IWebElement YenidenAra;
        #endregion

        #region Yeniden Ara butonunun Fonksiyonu
        public void YenidenArama()
        {
            CustomElementWait.WaitUntilElementClickable(driver, YenidenAra);
            YenidenAra.Click();
            CustomElementWait.WaitForLoad(driver);
        }
        #endregion

        #region Takvim Tarih seçim elementleri
        [FindsBy(How = How.CssSelector, Using = "#search > div > button.departure-nav.next.button.right")]
        private IWebElement takvim;
        #endregion

        #region Takvim Tarih seçim fonksiyonları
        public void TakvimTarihsecimi()
        {
            CustomElementWait.WaitForLoad(driver);
            CustomElementWait.WaitUntilElementClickable(driver, takvim);
            takvim.Click();
            CustomElementWait.WaitForLoad(driver);
        }
        #endregion


    }
}



