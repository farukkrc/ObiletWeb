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

namespace ObiletWebOtomasyon.ComponentObjects.SeferListeleme
{


    class İletisimBilgileri : BasePage
    {
        private IWebDriver driver; //web driver
        private WebDriverWait wait;// web driveri belirlenen olay gerçekleşene kadar istenilen süre kadar bekletmeyi sağlayan nesne
        private int timeoutWaitSecond = Convert.ToInt32(ConfigurationManager.AppSettings["TimeoutWaitSecond"]);
        public İletisimBilgileri(IWebDriver driver) : base(driver)
        {//her sayfadaki test driveri ilgili sayfada kullanılmasını sağlayan constructor
            this.driver = driver;
            PageFactory.InitElements(driver, this);//Bu yapı tanımlanarak [FindsBy] ek açıklaması ile ana sayfadaki web elementleri bulunabilir.
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutWaitSecond));//web driver istenilen durum gerçekleşene kadar 10 sn bekleyecek
        }


        // iletişim Bilgileri yazısı ekranda görüntülendiği doğrulanıyor.
        private By iletisimBasligi = By.XPath("//label[contains(text(),'İletişim Bilgileri')]");
        private IWebElement İletisimBilgileriYazisi => driver.FindElement(iletisimBasligi);
        public string İletisim => İletisimBilgileriYazisi.Text;


        public void İletisimEmail(string Email)
        {
            string id = "email";
            IWebElement firstName = driver.FindElement(By.Id(id));
            //firstName.SendKeys("faruk.kirci@obilet.com");
            //firstName.SendKeys(Keys.Enter);
            //var AccName = driver.FindElement(By.Id("email")).GetAttribute("value");
            //Assert.AreEqual("faruk.kirci@obilet.com", firstName.GetAttribute("value"));
            ClearAndSenKeys(firstName, Email);
        }

        public void İletisimPhone(string Telefon)
        {
            string id = "phone";
            IWebElement phone = driver.FindElement(By.Id(id));
            //phone.SendKeys("5538596190");
            //phone.SendKeys(Keys.Enter);
            //var phonevalue = driver.FindElement(By.Id("phone")).GetAttribute("value");
            //Assert.AreEqual("5538596190", phone.GetAttribute("value"));
            ClearAndSenKeys(phone, Telefon);
        }

        public void UcretsizSMS()
        {
            Thread.Sleep(TimeSpan.FromSeconds(2));
            driver.FindElement(By.Id("subscription-optin")).Click();
            Thread.Sleep(TimeSpan.FromSeconds(2));
            driver.FindElement(By.LinkText("burada")).Click();
            CustomElementWait.WaitForLoad(driver);

            var element = driver.FindElement(By.LinkText("obilet.com/kisisel-verilerin-korunmasi"));
            Actions actions = new Actions(driver);
            actions.MoveToElement(element).Perform();

            driver.FindElement(By.LinkText("obilet.com/kisisel-verilerin-korunmasi")).Click();
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            CustomElementWait.WaitForLoad(driver);
            IJavaScriptExecutor JS = driver as IJavaScriptExecutor;
            CustomElementWait.WaitForLoad(driver);
            JS.ExecuteScript("window.scroll(0, 1000);");
            CustomElementWait.WaitForLoad(driver);
            JS.ExecuteScript("window.scroll(500, 0);");
            CustomElementWait.WaitForLoad(driver);
            driver.Close();
            driver.SwitchTo().Window(driver.WindowHandles.First());
            CustomElementWait.WaitForLoad(driver);
            driver.FindElement(By.CssSelector("button[class='close']")).Click();
            CustomElementWait.WaitForLoad(driver);
            driver.FindElement(By.Id("subscription-optin")).Click();
            CustomElementWait.WaitForLoad(driver);
        }

        //Yolcu Bilgileri input Hata mesajlarının ID lerini çıkarıyoruz.
        #region Hata mesajları Xpathleri
        private const string EmailContactNullID = "//label[@id='email-error']";
        [FindsBy(How = How.XPath, Using = EmailContactNullID)]
        private IWebElement EmailContactNullText;

        private const string invalidEmailAddresID = "//label[normalize-space()='Geçersiz e-posta adresi.']";
        [FindsBy(How = How.XPath, Using = invalidEmailAddresID)]
        private IWebElement invalidEmailAddresText;

        private const string PhoneContactNullID = "//label[@id='phone-error']";
        [FindsBy(How = How.XPath, Using = PhoneContactNullID)]
        private IWebElement PhoneContactNullText;

        private const string invalidPhoneAddresID = "//label[contains(text(),'Geçersiz telefon numarası.')]";
        [FindsBy(How = How.XPath, Using = invalidPhoneAddresID)]
        private IWebElement invalidPhoneAddresText;
        #endregion

        #region Hata mesajları
        // hata mesajlarını göstermek için enum bir public oluşturuyoruz
        public enum ErrorMessages
        {
            EpostaBosBirakilamaz, GecersizEpostaAdresi, TelefonBosBirakilamaz, GecersizTelefonNumarasi
        }

        public string returnErrorMessage(ErrorMessages errorMessage)
        {
            switch (errorMessage)
            {
                case ErrorMessages.EpostaBosBirakilamaz:
                    CustomElementWait.WaitUntilElementVisible(driver, By.XPath(EmailContactNullID));
                    return EmailContactNullText.Text;

                case ErrorMessages.GecersizEpostaAdresi:
                    CustomElementWait.WaitUntilElementVisible(driver, By.XPath(invalidEmailAddresID));
                    return invalidEmailAddresText.Text;

                case ErrorMessages.TelefonBosBirakilamaz:
                    CustomElementWait.WaitUntilElementVisible(driver, By.XPath(PhoneContactNullID));
                    return PhoneContactNullText.Text;

                case ErrorMessages.GecersizTelefonNumarasi:
                    CustomElementWait.WaitUntilElementVisible(driver, By.XPath(invalidPhoneAddresID));
                    return invalidPhoneAddresText.Text;

                default:
                    return "";
            }
        }
        #endregion


    }
}

