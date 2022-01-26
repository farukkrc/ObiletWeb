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

    class HızlıFiltreler : BasePage
    {
        private IWebDriver driver; //web driver
        private WebDriverWait wait;// web driveri belirlenen olay gerçekleşene kadar istenilen süre kadar bekletmeyi sağlayan nesne
        private int timeoutWaitSecond = Convert.ToInt32(ConfigurationManager.AppSettings["TimeoutWaitSecond"]);
        public HızlıFiltreler(IWebDriver driver) : base(driver)
        {//her sayfadaki test driveri ilgili sayfada kullanılmasını sağlayan constructor
            this.driver = driver;
            PageFactory.InitElements(driver, this);//Bu yapı tanımlanarak [FindsBy] ek açıklaması ile ana sayfadaki web elementleri bulunabilir.
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutWaitSecond));//web driver istenilen durum gerçekleşene kadar 10 sn bekleyecek
        }


        #region Sayfa'da belirtilen öğe gelene kadar bekleme
        private const string SearchModal = "search";
        [FindsBy(How = How.Id, Using = SearchModal)]
        private IWebElement loginModal;
        
        public void Bekle()
        {
            CustomElementWait.WaitUntilElementVisible(driver, By.Id(SearchModal)); // Modal olana kadar bekle
        }
        #endregion


        #region Hızlı Filtreler Elementleri
        [FindsBy(How = How.CssSelector, Using = "#quick-filters-container > ul > li:nth-child(1) > button")]
        private IWebElement Sabah;
        [FindsBy(How = How.CssSelector, Using = "#quick-filters-container > ul > li:nth-child(2) > button")]
        private IWebElement Oglen;
        [FindsBy(How = How.CssSelector, Using = "#quick-filters-container > ul > li:nth-child(3) > button")]
        private IWebElement Aksam;
        [FindsBy(How = How.CssSelector, Using = "#quick-filters-container > ul > li:nth-child(4) > button")]
        private IWebElement BaglayanGece;
        [FindsBy(How = How.CssSelector, Using = "#quick-filters-container > ul > li:nth-child(5) > button")]
        private IWebElement ikiartibir;
        // Tüm seferleri görmek için tıklayın butonu
        [FindsBy(How = How.CssSelector, Using = "#list > li.item.single.link > button > strong")]
        private IWebElement TumSeferler;
        #endregion

        #region Hızlı Filtrelerinin Fonksiyonları
        public void HizliFiltre()
        {
            // Sefer listesinde Takvim kısmındaki tarih bilgisini alıyoruz.
            var journeyDate = DateTime.Parse(Convert.ToString(driver.FindElements(By.CssSelector("#departure"))[0].GetAttribute("data-date"))).Date;

            var nonFilteredJourneyList = GetDisplayingJourneys(journeyDate);
            //var journeyDate = DateTime.Now.Date;

            //Bu kısımda butonlar tek tek seçilip kaldırıyoruz
            // Sabah butonu
            var buttonPathSabah = "#quick-filters-container > ul > li:nth-child(1) > button";
            var MorningTimeStart = new TimeSpan(05, 0, 0);
            var MorningTimeEnd = new TimeSpan(11, 0, 0);
            var morningFilterLimitStart = journeyDate + MorningTimeStart;
            var morningFilterLimitEnd = journeyDate + MorningTimeEnd;

            Control(nonFilteredJourneyList, journeyDate, buttonPathSabah, morningFilterLimitStart, morningFilterLimitEnd, true);

            // Öğlen butonu
            var buttonPathOglen = "#quick-filters-container > ul > li:nth-child(2) > button";
            var NoonTimeStart = new TimeSpan(11, 0, 0);
            var NoonTimeEnd = new TimeSpan(17, 0, 0);
            var NoonFilterLimitStart = journeyDate + NoonTimeStart;
            var NoonFilterLimitEnd = journeyDate + NoonTimeEnd;

            Control(nonFilteredJourneyList, journeyDate, buttonPathOglen, NoonFilterLimitStart, NoonFilterLimitEnd, true);
            // Akşam butonu
            var buttonPathaksam = "#quick-filters-container > ul > li:nth-child(3) > button";
            var NightTimeStart = new TimeSpan(17, 0, 0);
            var NightTimeEnd = new TimeSpan(22, 0, 0);
            var NightFilterLimitStart = journeyDate + NightTimeStart;
            var NightFilterLimitEnd = journeyDate + NightTimeEnd;

            Control(nonFilteredJourneyList, journeyDate, buttonPathaksam, NightFilterLimitStart, NightFilterLimitEnd, true);
            // Bağlayan gece butonu
            var buttonPathGece = "#quick-filters-container > ul > li:nth-child(4) > button";
            var ConnectingTimeStart = new TimeSpan(22, 0, 0);
            var ConnectingTimeEnd = new TimeSpan(05, 00, 0);
            var ConnectingFilterLimitStart = journeyDate + ConnectingTimeStart;
            var ConnectingFilterLimitEnd = journeyDate.AddDays(1) + ConnectingTimeEnd;

            Control(nonFilteredJourneyList, journeyDate, buttonPathGece, ConnectingFilterLimitStart, ConnectingFilterLimitEnd, true);


            //Bu kısımda bütün butonlar tektek seçiliyor.
            // Sabah butonu
            var buttonPathSabah1 = "#quick-filters-container > ul > li:nth-child(1) > button";
            var morningTimeStart = new TimeSpan(05, 0, 0);
            var morningTimeEnd = new TimeSpan(11, 0, 0);
            var MorningFilterLimitStart = journeyDate + morningTimeStart;
            var MorningFilterLimitEnd = journeyDate + morningTimeEnd;

            Control(nonFilteredJourneyList, journeyDate, buttonPathSabah1, MorningFilterLimitStart, MorningFilterLimitEnd, false);
            // Öğlen butonu
            var buttonPathOglen1 = "#quick-filters-container > ul > li:nth-child(2) > button";
            var noonTmeStart = new TimeSpan(05, 0, 0);
            var noonTimeEnd = new TimeSpan(17, 0, 0);
            var noonFilterLimitStart = journeyDate + noonTmeStart;
            var noonFilterLimitEnd = journeyDate + noonTimeEnd;

            Control(nonFilteredJourneyList, journeyDate, buttonPathOglen1, noonFilterLimitStart, noonFilterLimitEnd, false);
            // Akşam butonu
            var buttonPathaksam1 = "#quick-filters-container > ul > li:nth-child(3) > button";
            var nightTimeStart = new TimeSpan(05, 0, 0);
            var nightTimeEnd = new TimeSpan(22, 0, 0);
            var nightFilterLimitStart = journeyDate + nightTimeStart;
            var nightFilterLimitEnd = journeyDate + nightTimeEnd;

            Control(nonFilteredJourneyList, journeyDate, buttonPathaksam1, nightFilterLimitStart, nightFilterLimitEnd, false);
            // Bağlayan Gece butonu
            var buttonPathGece1 = "#quick-filters-container > ul > li:nth-child(4) > button";
            //var connectingnightTimeStart = new TimeSpan(05, 0, 0); aynı gün için çalıştırma
            var connectingnightTimeStart = new TimeSpan(00, 0, 0);
            var connectingnightTimeEnd = new TimeSpan(05, 00, 0);
            var ConnectingNightFilterLimitStart = journeyDate + connectingnightTimeStart;
            var ConnectingNightFilterLimitEnd = journeyDate.AddDays(1) + connectingnightTimeEnd;

            Control(nonFilteredJourneyList, journeyDate, buttonPathGece1, ConnectingNightFilterLimitStart, ConnectingNightFilterLimitEnd, false);
        }

        private void Control(List<JourneyModel> nonFilteredJourneyList, DateTime journeyDate, string buttonPath, DateTime startTime, DateTime endTime, bool clickAgain)
        {
            //var nonFilteredJourneyList = GetDisplayingJourneys();

            CustomElementWait.WaitForLoad(driver);
            IWebElement button = driver.FindElement(By.CssSelector(buttonPath));
            button.Click();
            CustomElementWait.WaitForLoad(driver);

            // Selenium->Filtre (button click)
            var morningJourneys = GetDisplayingJourneys(journeyDate);

            // Test Case 1: nonFilteredjourneyList'ten tüm sabah olmayanlar filtrelendimi
            Assert.IsFalse(morningJourneys.Any(x => x.DepartureHour <= startTime && x.DepartureHour >= endTime));

            //// Test Case 2: nonFilteredjourneyList'den sadece sabah olanlar mı filtrelendi
            var morningJourneysCount = nonFilteredJourneyList.Where(x => x.DepartureHour >= startTime && x.DepartureHour <= endTime).Count();
            var filteredMorningJourneysCount = morningJourneys.Count;
            Assert.AreEqual(filteredMorningJourneysCount, morningJourneysCount);

            // true false kontrolleri
            if (clickAgain)
            {
                button.Click();
                CustomElementWait.WaitForLoad(driver);
            }
        }

        private List<JourneyModel> GetDisplayingJourneys(DateTime journeyDate)
        {
            var allJourneys = new List<JourneyModel>();

            IList<IWebElement> all = driver.FindElements(By.XPath("//li[contains(@class, 'item journey') and starts-with(@id, 'journey-')]"));

            foreach (var listItem in all)
            {

                IWebElement departureHour = listItem.FindElement(By.CssSelector("div.main.row > div.time.col > div.departure"));
                var isNextDay = listItem.GetAttribute("class").Contains("next-day");
                var departure = TimeSpan.Parse(departureHour.Text.ToString());
                var tempJourneyDate = journeyDate;

                if (isNextDay)
                {
                    tempJourneyDate = journeyDate.AddDays(1);
                }

                allJourneys.Add(
                    new JourneyModel()
                    {
                        DepartureHour = tempJourneyDate + departure,
                        //SeatCategory = "1+1"
                    }
                );
            }
            return allJourneys;
        }
        #endregion
    }
}
