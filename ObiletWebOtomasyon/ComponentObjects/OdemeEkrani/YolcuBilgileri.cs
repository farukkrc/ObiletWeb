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

namespace ObiletWebOtomasyon.ComponentObjects.OdemeEkrani
{

    class YolcuBilgileri : BasePage
    {
        private IWebDriver driver; //web driver
        private WebDriverWait wait;// web driveri belirlenen olay gerçekleşene kadar istenilen süre kadar bekletmeyi sağlayan nesne
        private int timeoutWaitSecond = Convert.ToInt32(ConfigurationManager.AppSettings["TimeoutWaitSecond"]);
        public YolcuBilgileri(IWebDriver driver) : base(driver)
        {//her sayfadaki test driveri ilgili sayfada kullanılmasını sağlayan constructor
            this.driver = driver;
            PageFactory.InitElements(driver, this);//Bu yapı tanımlanarak [FindsBy] ek açıklaması ile ana sayfadaki web elementleri bulunabilir.
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutWaitSecond));//web driver istenilen durum gerçekleşene kadar 10 sn bekleyecek
        }

        private By YolcuBasligi = By.XPath("//label[normalize-space()='Yolcu Bilgileri']");
        private IWebElement YolcuBilgisi => driver.FindElement(YolcuBasligi);
        public string YolcuBilgileriYazisi => YolcuBilgisi.Text;

        public void YolcuAdSoyad(string AdSoyad)
        {
            string xpath = "//input[@placeholder='Doldurulması zorunludur.']";
            IWebElement firstName = driver.FindElement(By.XPath(xpath));
            ClearAndSenKeys(firstName, AdSoyad);
        }

        public void TcKimlik(string TC)
        {
            string xpath = "//input[contains(@placeholder,'Karayolu Taşıma Yönetmeliğince gereklidir.')]";
            IWebElement TCkimlik = driver.FindElement(By.XPath(xpath));
            ClearAndSenKeys(TCkimlik, TC);
        }

        public void TcVatandasidegilim()
        {
            IWebElement tcvatandasidegislim = driver.FindElement(By.XPath("//span[contains(text(),'T.C. vatandaşı değilim')]"));
            tcvatandasidegislim.Click();
        }

        public void UyrukAndPasaport()
        {

            // select list elementini buluyor ve bir select sınıfı oluşturuyor
            var listSelect = new SelectElement(driver.FindElement(By.XPath("//select[contains(@id,'nationality-')]")));
            
            //value ile select listen seçim yapıyor.
            listSelect.SelectByValue("US");

            // Amerika Birlesik Devletleri'in seçildiğini doğruluyor
            Assert.IsTrue(listSelect.SelectedOption.Text.Equals("Amerika Birlesik Devletleri"));

            // Pasaport numarası
            IWebElement Pasaport = driver.FindElement(By.XPath("//input[contains(@id,'passport-')]"));
            Pasaport.Click();
            Pasaport.SendKeys("U123456");
            Pasaport.SendKeys(Keys.Enter);
        }

        public void HesKodu()
        {
            driver.FindElement(By.XPath("//input[contains(@id,'hes-code-')]")).SendKeys("T2J5816318");
        }

        public void SeyahatSigortasi()
        {
            driver.FindElement(By.CssSelector("button[class='details-button no-wrap']")).Click();
            Thread.Sleep(TimeSpan.FromSeconds(1));
            CustomElementWait.WaitForLoad(driver);
            var element = driver.FindElement(By.PartialLinkText("yurtdışına aktarılacaktır."));
            Actions actions = new Actions(driver);
            actions.MoveToElement(element).Perform();
            CustomElementWait.WaitForLoad(driver);
            driver.FindElement(By.PartialLinkText("Ürün koşulları ve öz")).Click();
            CustomElementWait.WaitForLoad(driver);
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            driver.Close();
            driver.SwitchTo().Window(driver.WindowHandles.First());
            CustomElementWait.WaitForLoad(driver);
            driver.FindElement(By.LinkText("KVKK Aydınlatma Metni")).Click();
            CustomElementWait.WaitForLoad(driver);
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            driver.Close();
            driver.SwitchTo().Window(driver.WindowHandles.First());
            CustomElementWait.WaitForLoad(driver);
            driver.FindElement(By.LinkText("Mesafeli Satış Sözleşmesi Bilgilendirme Metni")).Click();
            CustomElementWait.WaitForLoad(driver);
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            driver.Close();
            driver.SwitchTo().Window(driver.WindowHandles.First());
            CustomElementWait.WaitForLoad(driver);
            driver.FindElement(By.LinkText("Sigorta Bilgilendirme Metni")).Click();
            CustomElementWait.WaitForLoad(driver);
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            driver.Close();
            driver.SwitchTo().Window(driver.WindowHandles.First());
            CustomElementWait.WaitForLoad(driver);
            driver.FindElement(By.XPath("//button[contains(@class,'close')]")).Click();
            CustomElementWait.WaitForLoad(driver);

            // Seyahat sigortası istiyorum Butonu //
            driver.FindElement(By.CssSelector("ob-checkbox[class='opt-in'] div[class='box']")).Click();
            CustomElementWait.WaitForLoad(driver);

            IWebElement Day = driver.FindElement(By.ClassName("day"));
            SelectElement select = new SelectElement(Day);
            //Listeden seçim eklemek için kullanılır
            select.SelectByValue("13");
            Thread.Sleep(2);
            IWebElement Month = driver.FindElement(By.ClassName("month"));
            SelectElement selectMonth = new SelectElement(Month);
            //Listeden seçim eklemek için kullanılır
            selectMonth.SelectByValue("5");
            Thread.Sleep(2);
            IWebElement Year = driver.FindElement(By.ClassName("year"));
            SelectElement selectYear = new SelectElement(Year);
            //Listeden seçim eklemek için kullanılır
            selectYear.SelectByValue("1987");
            Thread.Sleep(2);



        }


        //Yolcu Bilgileri input Hata mesajlarının ID lerini çıkarıyoruz.
        #region Hata mesajları Xpathleri
        private const string PassengerContactNull = "//label[contains(@id,'name-')]";
        [FindsBy(How = How.XPath, Using = PassengerContactNull)]
        private IWebElement PassengerContactNullText;

        private const string PassengerleastThreecharacters = "//label[normalize-space()='Yolcu ismi en az 3 karakter içermelidir.']";
        [FindsBy(How = How.XPath, Using = PassengerleastThreecharacters)]
        private IWebElement PassengerleastthreecharactersText;

        private const string PassengermustcontainThreeletters = "//label[normalize-space()='Yolcu ismi en az 3 harf içermelidir.']";
        [FindsBy(How = How.XPath, Using = PassengermustcontainThreeletters)]
        private IWebElement PassengermustcontainThreelettersText;

        private const string Passengernamecontainsinvalidcharacters = "//label[normalize-space()='Yolcu ismi geçersiz karakterler içermektedir.']";
        [FindsBy(How = How.XPath, Using = Passengernamecontainsinvalidcharacters)]
        private IWebElement PassengernamecontainsinvalidcharactersText;

        private const string TCNullXpath = "//label[contains(@id,'gov-id-')]";
        [FindsBy(How = How.XPath, Using = TCNullXpath)]
        private IWebElement TCNullText;

        private const string invalidTCnumberXpath = "//label[contains(text(),'Geçersiz T.C. kimlik numarası.')]";
        [FindsBy(How = How.XPath, Using = invalidTCnumberXpath)]
        private IWebElement invalidTCnumberText;
        #endregion

        #region Hata mesajları
        // hata mesajlarını göstermek için enum bir public oluşturuyoruz
        public enum ErrorMessages
        {
            AdSoyadBosBirakilamaz, YolcuİsmiUcKarektericermelidir, YolcuİsmiUcharficermelidir, YolcuİsmiGecersizKaraktericermektedir, TCkimlikBosBirakilamaz,TCkimlikNumarsiGecersiz
        }

        public string returnErrorMessage(ErrorMessages errorMessage)
        {
            switch (errorMessage)
            {
                case ErrorMessages.AdSoyadBosBirakilamaz:
                    CustomElementWait.WaitUntilElementVisible(driver, By.XPath(PassengerContactNull));
                    return PassengerContactNullText.Text;

                case ErrorMessages.YolcuİsmiUcKarektericermelidir:
                    CustomElementWait.WaitUntilElementVisible(driver, By.XPath(PassengerleastThreecharacters));
                    return PassengerleastthreecharactersText.Text;

                case ErrorMessages.YolcuİsmiUcharficermelidir:
                    CustomElementWait.WaitUntilElementVisible(driver, By.XPath(PassengermustcontainThreeletters));
                    return PassengermustcontainThreelettersText.Text;

                case ErrorMessages.YolcuİsmiGecersizKaraktericermektedir:
                    CustomElementWait.WaitUntilElementVisible(driver, By.XPath(Passengernamecontainsinvalidcharacters));
                    return PassengernamecontainsinvalidcharactersText.Text;

                case ErrorMessages.TCkimlikBosBirakilamaz:
                    CustomElementWait.WaitUntilElementVisible(driver, By.XPath(TCNullXpath));
                    return TCNullText.Text;

                case ErrorMessages.TCkimlikNumarsiGecersiz:
                    CustomElementWait.WaitUntilElementVisible(driver, By.XPath(invalidTCnumberXpath));
                    return invalidTCnumberText.Text;

                default:
                    return "";
            }
        }
        #endregion
    }
}