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

namespace ObiletWebOtomasyon.ComponentObjects.OdemeEkrani
{

    class OdemeBilgileri : BasePage
    {
        private IWebDriver driver; //web driver
        private WebDriverWait wait;// web driveri belirlenen olay gerçekleşene kadar istenilen süre kadar bekletmeyi sağlayan nesne
        private int timeoutWaitSecond = Convert.ToInt32(ConfigurationManager.AppSettings["TimeoutWaitSecond"]);
        public OdemeBilgileri(IWebDriver driver) : base(driver)
        {//her sayfadaki test driveri ilgili sayfada kullanılmasını sağlayan constructor
            this.driver = driver;
            PageFactory.InitElements(driver, this);//Bu yapı tanımlanarak [FindsBy] ek açıklaması ile ana sayfadaki web elementleri bulunabilir.
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutWaitSecond));//web driver istenilen durum gerçekleşene kadar 10 sn bekleyecek
        }

        private By YolcuBasligi = By.XPath("//label[normalize-space()='Ödeme Bilgileri']");
        private IWebElement YolcuBilgisi => driver.FindElement(YolcuBasligi);
        public string OdemeBilgileriYazisi => YolcuBilgisi.Text;

        public void KartNumarasi()
        {
            IWebElement Kart = driver.FindElement(By.Id("card-number"));
            Kart.Click();
            Kart.Clear();
            Kart.Click();
        }

        public void KartNumarasiBos()
        {
            IWebElement KartBosBirakilmasi = driver.FindElement(By.Id("card-number"));
            KartBosBirakilmasi.Click();
            KartBosBirakilmasi.SendKeys("");
        }

        public void SonKullanmaTarihi()
        {
            IWebElement SonTarih = driver.FindElement(By.Id("card-expiration"));
            SonTarih.Click();
            SonTarih.Clear();
            SonTarih.Click();
        }

        public void SonKullanmaTarihiBos()
        {
            IWebElement SonTarihBosBirakilmasi = driver.FindElement(By.Id("card-expiration"));
            SonTarihBosBirakilmasi.Click();
            SonTarihBosBirakilmasi.SendKeys("");
        }

        public void CVC2()
        {
            IWebElement CVC2Alani = driver.FindElement(By.Id("card-csc"));
            CVC2Alani.Click();
            CVC2Alani.Clear();
            CVC2Alani.Click();
        }

        public void CVC2Bosbirakilmasi()
        {
            IWebElement CVC2AlaniBos = driver.FindElement(By.Id("card-csc"));
            CVC2AlaniBos.Click();
            CVC2AlaniBos.SendKeys("");
        }

        public void CVC2Nedir()
        {
            driver.FindElement(By.XPath("//button[normalize-space()='CVC2 Nedir?']")).Click();
            CustomElementWait.WaitForLoad(driver);
            driver.FindElement(By.XPath("//button[@class='close']")).Click();
            CustomElementWait.WaitForLoad(driver);
        }

        public void SozlesmeBox()
        {
            IWebElement box = driver.FindElement(By.CssSelector("ob-checkbox[id='contract-optin'] div[class='box']"));
            box.Click();
        }

        public void Onbilgilendirme()
        {
            Thread.Sleep(2);
            driver.FindElement(By.LinkText("Ön Bilgilendirme Formu'nu")).Click();
            Thread.Sleep(2);
            CustomElementWait.WaitForLoad(driver);

            // elementi üzerinde gelinir
            var element = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//h2[normalize-space()='5. CAYMA HAKKI']")));
            Actions hoverAction = new Actions(driver);
            hoverAction.MoveToElement(element).Perform();
            CustomElementWait.WaitForLoad(driver);

            var elements = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//strong[contains(text(),'7. YETKİLİ MAHKEME')]")));
            Actions Action = new Actions(driver);
            hoverAction.MoveToElement(elements).Perform();
            CustomElementWait.WaitForLoad(driver);

            // Ön bilgilendirme formu X ile kapatması
            driver.FindElement(By.CssSelector("button[class='close']")).Click();

        }

        public void MesafeliSatisSozlesmesi()
        {
            
            driver.FindElement(By.LinkText("Mesafeli Satış Sözleşmesi'ni")).Click();
            CustomElementWait.WaitForLoad(driver);

            // elementi üzerinde gelinir
            var element = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#A3 > h2")));
            Actions hoverAction = new Actions(driver);
            hoverAction.MoveToElement(element).Perform();
            CustomElementWait.WaitForLoad(driver);

            var elements = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("section[id='A5'] h2")));
            Actions Action = new Actions(driver);
            hoverAction.MoveToElement(elements).Perform();
            CustomElementWait.WaitForLoad(driver);

            driver.FindElement(By.CssSelector("button[class='close']")).Click();
        }

        public void MasterPassKullanimKosullari()
        {
            IWebElement KullanimKosullari = driver.FindElement(By.Id("card-store-terms"));
            KullanimKosullari.Click();
            CustomElementWait.WaitForLoad(driver);
            driver.FindElement(By.CssSelector("button[class='close']")).Click();
            CustomElementWait.WaitForLoad(driver);
        }

        public void MasterPassNedir()
        {
            IWebElement masterNedir = driver.FindElement(By.Id("card-store-details"));
            masterNedir.Click();
            CustomElementWait.WaitForLoad(driver);
            driver.FindElement(By.CssSelector("button[class='close']")).Click();
            CustomElementWait.WaitForLoad(driver);
        }

        public void GuvenliOdemeYap ()
        {
            IWebElement Odemeyap = driver.FindElement(By.Id("pay"));
            Odemeyap.Click();
            CustomElementWait.WaitForLoad(driver);
            //Thread.Sleep(TimeSpan.FromSeconds(timeoutWaitSecond));
        }

        public void MasterPassCheckbox()
        {
            IWebElement MasterPass = driver.FindElement(By.CssSelector("ob-checkbox[id='card-store-register-optin'] div[class='box']"));
            MasterPass.Click();
            CustomElementWait.WaitForLoad(driver);
            CustomElementWait.WaitUntilElementVisible(driver, By.CssSelector("div[class='ob-modal success pop card-store-validation-modal card-store-modal open']"));
            CustomElementWait.WaitForLoad(driver);
            IWebElement ExitButton = driver.FindElement(By.XPath("//button[contains(text(),'İptal')]"));
            ExitButton.Click();
            CustomElementWait.WaitForLoad(driver);
        }



        //Kart Bilgileri input Hata mesajlarının ID lerini çıkarıyoruz.
        #region Hata mesajları Xpathleri
        private const string CardnumberNullError = "//label[@id='card-number-error']";
        [FindsBy(How = How.XPath, Using = CardnumberNullError)]
        private IWebElement CardnumberNullErrorText;

        private const string invalidcardnumberXpath = "//label[contains(text(),'Geçersiz banka/kredi kartı numarası.')]";
        [FindsBy(How = How.XPath, Using = invalidcardnumberXpath)]
        private IWebElement invalidcardnumberXpathText;

        private const string PushdateNullError = "//label[@id='card-expiration-error']";
        [FindsBy(How = How.XPath, Using = PushdateNullError)]
        private IWebElement PushdateNullErrorText;

        private const string invalidPushDateXpath = "//label[normalize-space()='Geçersiz son kullanma tarihi.']";
        [FindsBy(How = How.XPath, Using = invalidPushDateXpath)]
        private IWebElement invalidPushDateXpathText;

        private const string CVC2NullError = "//label[@id='card-csc-error']";
        [FindsBy(How = How.XPath, Using = CVC2NullError)]
        private IWebElement CVC2NullErrorText;

        private const string invalidCVC2Xpath = "//label[normalize-space()='Geçersiz CVC2.']";
        [FindsBy(How = How.XPath, Using = invalidCVC2Xpath)]
        private IWebElement invalidCVC2XpathText;
        #endregion


        #region Hata mesajları
        // hata mesajlarını göstermek için enum bir public oluşturuyoruz
        public enum ErrorMessages
        {
            KartNumarasiBosBiralimaz, KartNumarasiGecersiz, SonKullanimTarihiBosBirakilamaz, SonKullanimTarihiGecersiz, CVC2BosBirakilamaz, CVC2Gecersiz
        }

        public string returnErrorMessage(ErrorMessages errorMessage)
        {
            switch (errorMessage)
            {
                case ErrorMessages.KartNumarasiBosBiralimaz:
                    CustomElementWait.WaitUntilElementVisible(driver, By.XPath(CardnumberNullError));
                    return CardnumberNullErrorText.Text;

                case ErrorMessages.KartNumarasiGecersiz:
                    CustomElementWait.WaitUntilElementVisible(driver, By.XPath(invalidcardnumberXpath));
                    return invalidcardnumberXpathText.Text;

                case ErrorMessages.SonKullanimTarihiBosBirakilamaz:
                    CustomElementWait.WaitUntilElementVisible(driver, By.XPath(PushdateNullError));
                    return PushdateNullErrorText.Text;

                case ErrorMessages.SonKullanimTarihiGecersiz:
                    CustomElementWait.WaitUntilElementVisible(driver, By.XPath(invalidPushDateXpath));
                    return invalidPushDateXpathText.Text;

                case ErrorMessages.CVC2BosBirakilamaz:
                    CustomElementWait.WaitUntilElementVisible(driver, By.XPath(CVC2NullError));
                    return CVC2NullErrorText.Text;

                case ErrorMessages.CVC2Gecersiz:
                    CustomElementWait.WaitUntilElementVisible(driver, By.XPath(invalidCVC2Xpath));
                    return invalidCVC2XpathText.Text;

                default:
                    return "";
            }
        }
        #endregion
    }
}