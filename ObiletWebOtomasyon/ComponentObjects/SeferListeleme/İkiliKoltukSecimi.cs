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

    class İkiliKoltukSecimi : BasePage
    {
        private IWebDriver driver; //web driver
        private WebDriverWait wait;// web driveri belirlenen olay gerçekleşene kadar istenilen süre kadar bekletmeyi sağlayan nesne
        private int timeoutWaitSecond = Convert.ToInt32(ConfigurationManager.AppSettings["TimeoutWaitSecond"]);
        public İkiliKoltukSecimi(IWebDriver driver) : base(driver)
        {//her sayfadaki test driveri ilgili sayfada kullanılmasını sağlayan constructor
            this.driver = driver;
            PageFactory.InitElements(driver, this);//Bu yapı tanımlanarak [FindsBy] ek açıklaması ile ana sayfadaki web elementleri bulunabilir.
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutWaitSecond));//web driver istenilen durum gerçekleşene kadar 10 sn bekleyecek
        }

        #region Çift iki koltuk seçimi
        public void Cift_iki_Koltuk_Secimi()
        {
            IList<IWebElement> SeferListe = driver.FindElements(By.XPath("//li[contains(@class, 'item journey') and starts-with(@id, 'journey-')]"));
            if (!SeferListe.Any()) return;
            foreach (var Sefer in SeferListe)
            {
                Sefer.Click();

                CustomElementWait.WaitForLoad(driver);
                var openedSefer = driver.FindElement(By.XPath("//li[starts-with(@id, '" + Sefer.GetAttribute("id") + "')]"));
                var hasOpenClass = openedSefer.GetAttribute("class").Contains("open");
                Assert.IsTrue(hasOpenClass);

                var BosKoltukList = Sefer.FindElements(By.XPath("//*[contains(@class, 'available active not-single-seat')]"));

                if (BosKoltukList.Count >= 2)
                {
                    int koltukNo = 0;

                    if (koltukNo < 2)
                    {

                        KoltukSec(BosKoltukList, koltukNo, 2);
                        koltukNo++;

                        KoltukSec(BosKoltukList, koltukNo, 2);
                        koltukNo++;

                        Seferkapatma(Sefer);
                        break;
                    }
                    Seferkapatma(Sefer);
                }
                else
                {
                    Seferkapatma(Sefer);

                }
            }
        }
        #endregion

        #region Çift dört koltuk seçimi
        public void Cift_dört_Koltuk_Secimi()
        {
            IList<IWebElement> SeferListe = driver.FindElements(By.XPath("//li[contains(@class, 'item journey') and starts-with(@id, 'journey-')]"));
            if (!SeferListe.Any()) return;
            foreach (var Sefer in SeferListe)
            {
                Sefer.Click();

                CustomElementWait.WaitForLoad(driver);
                var openedSefer = driver.FindElement(By.XPath("//li[starts-with(@id, '" + Sefer.GetAttribute("id") + "')]"));
                var hasOpenClass = openedSefer.GetAttribute("class").Contains("open");
                Assert.IsTrue(hasOpenClass);

                var BosKoltukList = Sefer.FindElements(By.XPath("//*[contains(@class, 'available active not-single-seat')]"));

                if (BosKoltukList.Count >= 4)
                {
                    int koltukNo = 0;

                    if (koltukNo < 4)
                    {

                        KoltukSec(BosKoltukList, koltukNo, 2);
                        koltukNo++;

                        KoltukSec(BosKoltukList, koltukNo, 2);
                        koltukNo++;

                        KoltukSec(BosKoltukList, koltukNo, 1);
                        koltukNo++;

                        KoltukSec(BosKoltukList, koltukNo, 1);
                        koltukNo++;

                        Seferkapatma(Sefer);
                        break;
                    }
                    Seferkapatma(Sefer);
                }
                else
                {
                    Seferkapatma(Sefer);

                }
            }
        }
        #endregion 

        #region Koltuk seçimlerinin Metotları
        public void KoltukSec(ReadOnlyCollection<IWebElement> BosKoltukList, int koltukNo, int erkekMi)
        {
            BosKoltukList[koltukNo].Click();

            CustomElementWait.WaitForLoad(driver);

            var KadinErkekMod = driver.FindElements(By.XPath("//div[contains(@class, 'drop-content')]"));
            if (KadinErkekMod.Count > 0)
            {
                var KadinErkek = driver.FindElement(By.XPath("//div[contains(@class, 'drop-content')]"));
                Assert.IsTrue(KadinErkek != null);

                CustomElementWait.WaitForLoad(driver);
                KadinErkek.FindElement(By.XPath("//*[@id='main']/div[9]/div/button[" + erkekMi + "]")).Click();
                CustomElementWait.WaitForLoad(driver);
                Assert.IsTrue(KadinErkek != null);
            }
            else
            {
                CustomElementWait.WaitForLoad(driver);
                var PopUp = driver.FindElement(By.CssSelector("#main > div.ob-modal-overlay.pop.open > div"));
                Assert.IsTrue(PopUp != null);
                PopUp.FindElement(By.ClassName("red")).Click();
                CustomElementWait.WaitForLoad(driver);
            }

        }
        #endregion

        #region Seferleri Kapatma Metodu
        public void Seferkapatma(IWebElement baseElement)
        {
            /* IWebElement firma = baseElement.FindElement(By.ClassName("logo"));
             var firmaAdi = firma.GetAttribute("alt");*/

            IWebElement close = baseElement.FindElement(By.ClassName("close"));
            if (close != null && !string.IsNullOrEmpty(close.Text))
            {
                close.Click();
                CustomElementWait.WaitForLoad(driver);
            }
        }
        #endregion
    }
}
