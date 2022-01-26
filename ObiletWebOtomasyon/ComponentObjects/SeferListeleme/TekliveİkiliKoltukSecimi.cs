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

    class TekliveİkiliKoltukSecimi : BasePage
    {
        private IWebDriver driver; //web driver
        private WebDriverWait wait;// web driveri belirlenen olay gerçekleşene kadar istenilen süre kadar bekletmeyi sağlayan nesne
        private int timeoutWaitSecond = Convert.ToInt32(ConfigurationManager.AppSettings["TimeoutWaitSecond"]);
        public TekliveİkiliKoltukSecimi(IWebDriver driver) : base(driver)
        {//her sayfadaki test driveri ilgili sayfada kullanılmasını sağlayan constructor
            this.driver = driver;
            PageFactory.InitElements(driver, this);//Bu yapı tanımlanarak [FindsBy] ek açıklaması ile ana sayfadaki web elementleri bulunabilir.
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutWaitSecond));//web driver istenilen durum gerçekleşene kadar 10 sn bekleyecek
        }

        #region Tek ve ikili koltuk seçimi
        public void Tekli_ikili_Koltuk_Secimi()
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

                var BosKoltukList = Sefer.FindElements(By.XPath("//*[contains(@class, 'available active single-seat')]"));
                var BosKoltukListikili = Sefer.FindElements(By.XPath("//*[contains(@class, 'available active not-single-seat')]"));

                if (BosKoltukList.Count >= 1 & BosKoltukListikili.Count >=2)
                {
                    int ikilikoltukNo = 0;
                    int koltukNo = 0;


                    if (koltukNo < 1 & ikilikoltukNo < 2)
                    {
                        OnesingleSeat(BosKoltukList, koltukNo, 1);
                        koltukNo++;

                        Twonotsingleseat(BosKoltukListikili, ikilikoltukNo, 1);
                        ikilikoltukNo++;

                        Twonotsingleseat(BosKoltukListikili, ikilikoltukNo, 1);
                        ikilikoltukNo++;


                        Seferkapatma(Sefer);
                        break;
                        /*
                        IWebElement Button = driver.FindElement(By.XPath("//span[contains(text(),'Onayla ve Devam Et')]"));
                        Button.Click();
                        CustomElementWait.WaitForLoad(driver);
                        break;
                        */
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

        #region Üç Tekli iki Çiftli koltuk seçimi
        public void Tekli_Uc_Cifli_iki_Koltuk_Secimi()
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

                var BosKoltukList = Sefer.FindElements(By.XPath("//*[contains(@class, 'available active single-seat')]"));
                var BosKoltukListikili = Sefer.FindElements(By.XPath("//*[contains(@class, 'available active not-single-seat')]"));

                if (BosKoltukList.Count >= 3 & BosKoltukListikili.Count >= 2)
                {
                    int ikilikoltukNo = 0;
                    int koltukNo = 0;


                    if (koltukNo < 3 & ikilikoltukNo < 2)
                    {
                        Twonotsingleseat(BosKoltukListikili, ikilikoltukNo, 2);
                        ikilikoltukNo++;

                        Twonotsingleseat(BosKoltukListikili, ikilikoltukNo, 1);
                        ikilikoltukNo++;

                        OnesingleSeat(BosKoltukList, koltukNo, 1);
                        koltukNo++;

                        OnesingleSeat(BosKoltukList, koltukNo, 1);
                        koltukNo++;

                        OnesingleSeat(BosKoltukList, koltukNo, 1);
                        koltukNo++;

                        Seferkapatma(Sefer);
                        break;
                        /*
                        IWebElement Button = driver.FindElement(By.XPath("//span[contains(text(),'Onayla ve Devam Et')]"));
                        Button.Click();
                        CustomElementWait.WaitForLoad(driver);
                        break;
                        */
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

        #region İki Tekli iki Çiftli koltuk seçimi
        public void Tekli_iki_Cifli_iki_Koltuk_Secimi()
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

                var BosKoltukList = Sefer.FindElements(By.XPath("//*[contains(@class, 'available active single-seat')]"));
                var BosKoltukListikili = Sefer.FindElements(By.XPath("//*[contains(@class, 'available active not-single-seat')]"));

                if (BosKoltukList.Count >= 2 & BosKoltukListikili.Count >= 2)
                {
                    int ikilikoltukNo = 0;
                    int koltukNo = 0;


                    if (koltukNo < 2 & ikilikoltukNo < 2)
                    {
                        OnesingleSeat(BosKoltukList, koltukNo, 1);
                        koltukNo++;

                        OnesingleSeat(BosKoltukList, koltukNo, 1);
                        koltukNo++;

                        Twonotsingleseat(BosKoltukListikili, ikilikoltukNo, 2);
                        ikilikoltukNo++;

                        Twonotsingleseat(BosKoltukListikili, ikilikoltukNo, 1);
                        ikilikoltukNo++;
                    }

                    IWebElement Button = driver.FindElement(By.XPath("//span[contains(text(),'Onayla ve Devam Et')]"));
                    Button.Click();
                    CustomElementWait.WaitForLoad(driver);
                    // Ödeme sayfasına gittimi doğruluyor.
                    IWebElement search = driver.FindElement(By.ClassName("passengers-contact-info-label"));
                    var dogrumu = search.GetAttribute("Class").Contains("passengers-contact-info-label");
                    Thread.Sleep(TimeSpan.FromSeconds(2));
                    Assert.IsTrue(dogrumu);
                    break;
                }
                else
                {
                    Seferkapatma(Sefer);
                }

            }
        }
        #endregion

        #region Tekli Koltuk seçimlerinin Metotları
        public void OnesingleSeat(ReadOnlyCollection<IWebElement> BosKoltukList, int koltukNo, int erkekMi)
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

        #region ikili Koltuk seçimlerinin Metotları
        public void Twonotsingleseat(ReadOnlyCollection<IWebElement> BosKoltukListikili, int ikilikoltukNo, int erkekMi)
        {
            BosKoltukListikili[ikilikoltukNo].Click();

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