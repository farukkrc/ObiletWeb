using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Configuration;
using System.Threading;
using ObiletWebOtomasyon.Base;
using ObiletWebOtomasyon.Common;
using ObiletWebOtomasyon.ComponentObjects.BaseComponent;
using NUnit.Framework;

namespace ObiletWebOtomasyon.ComponentObjects.HomePage
{
    class HomePageUyeGiris : BasePage
    {
        private IWebDriver driver; //web driver
        private WebDriverWait wait;// web driveri belirlenen olay gerçekleşene kadar istenilen süre kadar bekletmeyi sağlayan nesne
        private int timeoutWaitSecond = Convert.ToInt32(ConfigurationManager.AppSettings["TimeoutWaitSecond"]);
        public HomePageUyeGiris(IWebDriver driver) : base(driver)
        {//her sayfadaki test driveri ilgili sayfada kullanılmasını sağlayan constructor
            this.driver = driver;
            PageFactory.InitElements(driver, this);//Bu yapı tanımlanarak [FindsBy] ek açıklaması ile ana sayfadaki web elementleri bulunabilir.
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutWaitSecond));//web driver istenilen durum gerçekleşene kadar 10 sn bekleyecek
        }

        [FindsBy(How = How.XPath, Using = "//li[@class='login']")]
        private IWebElement UyeGiris;

        #region UyeLoginModal
        private const string UyeloginModalXPath = "//div[contains(@class,'ob-auth-modal open')]";
        [FindsBy(How = How.XPath, Using = UyeloginModalXPath)]
        private IWebElement UyeloginModal;

        [FindsBy(How = How.XPath, Using = "//*[@id='login-form']/div[2]/div/input")]
        private IWebElement UyeEmailInput;

        [FindsBy(How = How.XPath, Using = "//*[@id='login-form']/div[3]/div/input")]
        private IWebElement UyePasswordInput;

        [FindsBy(How = How.XPath, Using = "//*[@id='login-form']/div[5]/button")]
        private IWebElement UyeLoginButton;
        #endregion

        // Hata mesajlarının ID lerini çıkarıyoruz.
        #region Hata mesajları
        //private const string passwordLengtXPath = "//*[@id='password-error']";
        [FindsBy(How = How.Id, Using = "password-error")]
        private IWebElement ParolaLengtError;

        //private const string invalidPasswordOrMailErrorTextXPath = "//*[@id='email-error']";
        [FindsBy(How = How.Id, Using = "username-error")]
        private IWebElement EMailError;

        [FindsBy(How = How.Id, Using = "password-error")]
        private IWebElement ParolaBosError;

        [FindsBy(How = How.Id, Using = "username-error")]
        private IWebElement EmailBosError;

        [FindsBy(How = How.XPath, Using = "//*[@id='login-form']/div[5]/div[1]")]
        private IWebElement UserandPasswordError;
        #endregion

        // Fonksiyonları tanımlıyoruz
        #region UyeModalInputları
        public void Login(string email, string password)
        {
            CustomElementWait.WaitUntilElementVisible(driver, By.XPath(UyeloginModalXPath)); // Modal olana kadar bekle
            ClearAndSenKeys(UyeEmailInput, email);
            ClearAndSenKeys(UyePasswordInput, password);
            CustomElementWait.WaitUntilElementClickable(driver, UyeLoginButton); // buton tıklanabilir olana kadar bekle
            UyeLoginButton.Click();
            CustomElementWait.WaitForLoad(driver);
        }
        #endregion

        #region Hata mesajları
        // hata mesajlarını göstermek için enum bir public oluşturuyoruz
        public enum ErrorMessages
        {
            EksikSifreHatali, EmailHatali, ParolaBosBirakilamaz, EMailBosBirakilamaz, KullanıcıAdıveSifre
        }

        public string returnErrorMessage(ErrorMessages errorMessage)
        {
            switch (errorMessage)
            {
                case ErrorMessages.EksikSifreHatali:
                    CustomElementWait.WaitUntilElementVisible(driver, By.Id("password-error"));
                    return ParolaLengtError.Text;

                case ErrorMessages.EmailHatali:
                    CustomElementWait.WaitUntilElementVisible(driver, By.Id("username-error"));
                    return EMailError.Text;

                case ErrorMessages.ParolaBosBirakilamaz:
                    CustomElementWait.WaitUntilElementVisible(driver, By.Id("password-error"));
                    return ParolaBosError.Text;

                case ErrorMessages.EMailBosBirakilamaz:
                    CustomElementWait.WaitUntilElementVisible(driver, By.Id("username-error"));
                    return EmailBosError.Text;

                case ErrorMessages.KullanıcıAdıveSifre:
                    CustomElementWait.WaitUntilElementVisible(driver, By.XPath("//*[@id='login-form']/div[5]/div[1]"));
                    return UserandPasswordError.Text;


                default:
                    return "";
            }
        }
        #endregion

        #region Tıklanabilir butonlar
        public void Button()   // element tıklanabilir olana kadar bekler. App config verilen süre kadar 10sn
        {
            CustomElementWait.WaitUntilElementClickable(driver, UyeGiris);
            UyeGiris.Click();
        }
        #endregion

    }
}
