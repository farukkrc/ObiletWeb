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

    class TekliKoltukSecimi : BasePage
    {
        private IWebDriver driver; //web driver
        private WebDriverWait wait;// web driveri belirlenen olay gerçekleşene kadar istenilen süre kadar bekletmeyi sağlayan nesne
        private int timeoutWaitSecond = Convert.ToInt32(ConfigurationManager.AppSettings["TimeoutWaitSecond"]);
        public TekliKoltukSecimi(IWebDriver driver) : base(driver)
        {//her sayfadaki test driveri ilgili sayfada kullanılmasını sağlayan constructor
            this.driver = driver;
            PageFactory.InitElements(driver, this);//Bu yapı tanımlanarak [FindsBy] ek açıklaması ile ana sayfadaki web elementleri bulunabilir.
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutWaitSecond));//web driver istenilen durum gerçekleşene kadar 10 sn bekleyecek
        }

        #region Koltuk şemasında Dolu koltuk tıklanması
        public void DoluKoltukSecimi()
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

                var DoluKoltukList = Sefer.FindElements(By.CssSelector(@"a[obilet\:available=no]"));

                if (!DoluKoltukList.Any())
                {
                    continue;
                }
                else
                {
                    DoluKoltukList[0].Click();
                    CustomElementWait.WaitForLoad(driver);
                    var PopUp = driver.FindElement(By.XPath("//div[contains(@class, 'ob-modal error pop open')]"));
                    Assert.IsTrue(PopUp != null);

                    PopUp.FindElement(By.XPath("//div[2]/div[1]/button")).Click();
                    CustomElementWait.WaitForLoad(driver);
                    IWebElement close = driver.FindElement(By.XPath("//button[@class='close'][normalize-space()='Kapat']"));
                    close.Click();
                    CustomElementWait.WaitForLoad(driver);
                    break;
                }
            }
        }
        #endregion

        #region Dörtlü koltuk seçimi
        public void Tekli_Dort_Koltuk_Secimi()
        {
            IList<IWebElement> SeferListe = driver.FindElements(By.XPath("//li[contains(@class, 'item journey') and starts-with(@id, 'journey-')]"));
            if (!SeferListe.Any()) return;
            foreach (var Sefer in SeferListe)
            {
                Sefer.Click();


                CustomElementWait.WaitForLoad(driver);
                var openedSefer = driver.FindElement(By.XPath("//li[starts-with(@id, '" + Sefer.GetAttribute("id") + "')]"));
                CustomElementWait.WaitForLoad(driver);
                var hasOpenClass = openedSefer.GetAttribute("class").Contains("open");
                CustomElementWait.WaitForLoad(driver);
                // Assert.IsTrue(hasOpenClass);

                var BosKoltukList = openedSefer.FindElements(By.XPath("//*[contains(@class, 'available active single-seat')]"));

                if (BosKoltukList.Count >= 4)
                {
                    int koltukNo = 0;

                    if (koltukNo < 4)
                    {
                        koltukSec(BosKoltukList, koltukNo, 1);
                        koltukNo++;

                        koltukSec(BosKoltukList, koltukNo, 2);
                        koltukNo++;

                        koltukSec(BosKoltukList, koltukNo, 1);
                        koltukNo++;

                        koltukSec(BosKoltukList, koltukNo, 2);
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

        #region Üçlü koltuk seçimi
        public void Tekli_Uclu_Koltuk_Secimi()
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

                if (BosKoltukList.Count >= 3)
                {
                    int koltukNo = 0;

                    if (koltukNo < 3)
                    {
                        koltukSec(BosKoltukList, koltukNo, 1);
                        koltukNo++;

                        koltukSec(BosKoltukList, koltukNo, 2);
                        koltukNo++;

                        koltukSec(BosKoltukList, koltukNo, 1);
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

        #region İkili koltuk seçimi
        public void Tekli_iki_Koltuk_Secimi()
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

                if (BosKoltukList.Count >= 2)
                {
                    int koltukNo = 0;

                    if (koltukNo < 2)
                    {
                        koltukSec(BosKoltukList, koltukNo, 1);
                        koltukNo++;

                        koltukSec(BosKoltukList, koltukNo, 2);
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

        #region Tekli koltuk seçimi
        public void Tekli_Tekli_Koltuk_Secimi()
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

                if (BosKoltukList.Count >= 1)
                {
                    int koltukNo = 0;

                    if (koltukNo < 1)
                    {
                        koltukSec(BosKoltukList, koltukNo, 1);
                        koltukNo++;
                    }

                    IWebElement Button = driver.FindElement(By.XPath("//span[contains(text(),'Onayla ve Devam Et')]"));
                    Button.Click();
                    CustomElementWait.WaitForLoad(driver);
                    //Ödeme sayfasında bir web element ile sayfanın açıldığı 
                    //doğrulanıyor
                    Assert.IsTrue(driver.FindElements(By.ClassName("passengers-contact-info-label")).Count > 0);
                    break;
                }
                else
                {
                        Seferkapatma(Sefer);
                }
            }
        }
        #endregion

        #region Beşli koltuk seçimi
        public void Tekli_Bes_Koltuk_Secimi()
        {
            IList<IWebElement> SeferListe = driver.FindElements(By.XPath("//li[contains(@class, 'item journey') and starts-with(@id, 'journey-')]"));
            if (!SeferListe.Any()) return;
            foreach (var Sefer in SeferListe)
            {
                Sefer.Click();


                CustomElementWait.WaitForLoad(driver);
                var openedSefer = driver.FindElement(By.XPath("//li[starts-with(@id, '" + Sefer.GetAttribute("id") + "')]"));
                CustomElementWait.WaitForLoad(driver);
                var hasOpenClass = openedSefer.GetAttribute("class").Contains("open");
                CustomElementWait.WaitForLoad(driver);
                // Assert.IsTrue(hasOpenClass);

                var BosKoltukList = openedSefer.FindElements(By.XPath("//*[contains(@class, 'available active single-seat')]"));

                if (BosKoltukList.Count >= 5)
                {
                    int koltukNo = 0;


                    if (koltukNo < 5)
                    {
                        koltukSec(BosKoltukList, koltukNo, 1);
                        koltukNo++;

                        koltukSec(BosKoltukList, koltukNo, 2);
                        koltukNo++;

                        koltukSec(BosKoltukList, koltukNo, 1);
                        koltukNo++;

                        koltukSec(BosKoltukList, koltukNo, 2);
                        koltukNo++;

                        koltukSec(BosKoltukList, koltukNo, 2);
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
        public void koltukSec(ReadOnlyCollection<IWebElement> BosKoltukList, int koltukNo, int erkekMi)
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
                // Otobüs koltukları tek sefer 4 koltuk seçilir.
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
