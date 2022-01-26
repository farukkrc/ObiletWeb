using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Configuration;
using ObiletWebOtomasyon.Base;
using ObiletWebOtomasyon.Common;
using ObiletWebOtomasyon.ComponentObjects.BaseComponent;
using System.Threading;

namespace ObiletWebOtomasyon.ComponentObjects.HomePage
{
    class HomePageUyeOl : BasePage
    {
        private IWebDriver driver; //web driver
        private WebDriverWait wait;// web driveri belirlenen olay gerçekleşene kadar istenilen süre kadar bekletmeyi sağlayan nesne
        private int timeoutWaitSecond = Convert.ToInt32(ConfigurationManager.AppSettings["TimeoutWaitSecond"]);
        public HomePageUyeOl(IWebDriver driver) : base(driver)
        {//her sayfadaki test driveri ilgili sayfada kullanılmasını sağlayan constructor
            this.driver = driver;
            PageFactory.InitElements(driver, this);//Bu yapı tanımlanarak [FindsBy] ek açıklaması ile ana sayfadaki web elementleri bulunabilir.
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutWaitSecond));//web driver istenilen durum gerçekleşene kadar 10 sn bekleyecek
        }

        // Elementleri tanımlıyoruz
        #region UyeolandUyeGiriş  
        [FindsBy(How = How.XPath, Using = "//li[@class='login']")]
        private IWebElement goUyeGiris;

        [FindsBy(How = How.XPath, Using = "//*[@id='login-form']/div[5]/a")]
        private IWebElement goUyeOL;
        #endregion

        #region LoginModal
        private const string loginModalXPath = "//div[contains(@class,'ob-auth-modal')]";
        [FindsBy(How = How.XPath, Using = loginModalXPath)]
        private IWebElement loginModal;

        [FindsBy(How = How.XPath, Using = "//*[@id='register-form']/div[2]/div/input")]
        private IWebElement EmailInput;

        [FindsBy(How = How.XPath, Using = "//*[@id='register-form']/div[3]/div/input")]
        private IWebElement PasswordInput;

        [FindsBy(How = How.XPath, Using = "//*[@id='register-form']/div[6]/button")]
        private IWebElement LoginButon;

        [FindsBy(How = How.XPath, Using = "//*[@id='register-form']/div[3]/button")]
        private IWebElement GozIconu;
        #endregion


        #region KVKK Gizlilik butonu
        [FindsBy(How = How.XPath, Using = "//*[@id='register-form']/div[4]/div/a[1]")]
        private IWebElement KVKKGizlilik;

        [FindsBy(How = How.XPath, Using = "//*[@id='register-form']/div[4]/div/a[2]")]
        private IWebElement GizlilikButtonu;
        #endregion

        // Hata mesajlarının ID lerini çıkarıyoruz.
        #region Hata mesajları Xpathleri
        private const string passwordLengtXPath = "//*[@id='password-error']";
        [FindsBy(How = How.XPath, Using = passwordLengtXPath)]
        private IWebElement PasswordLengthErrorText;

        private const string invalidPasswordOrMailErrorTextXPath = "//*[@id='email-error']";
        [FindsBy(How = How.XPath, Using = invalidPasswordOrMailErrorTextXPath)]
        private IWebElement invalidPasswordOrMailErrorText;

        private const string PasswordErrorXPath = "//*[@id='password-error']";
        [FindsBy(How = How.XPath, Using = PasswordErrorXPath)]
        private IWebElement PasswordErrorText;

        private const string MailErrorXPath = "//*[@id='email-error']";
        [FindsBy(How = How.XPath, Using = MailErrorXPath)]
        private IWebElement MailErrorText;
        #endregion

        // Fonksiyonları tanımlıyoruz
        #region LoginModalFactory
        public void Login(string email, string password)
        {
            CustomElementWait.WaitUntilElementVisible(driver, By.XPath(loginModalXPath)); // Modal olana kadar bekle
            ClearAndSenKeys(EmailInput, email);
            ClearAndSenKeys(PasswordInput, password);
            CustomElementWait.WaitUntilElementClickable(driver, GozIconu);
            GozIconu.Click();
            Thread.Sleep(2000);
            CustomElementWait.WaitUntilElementClickable(driver, LoginButon); // buton tıklanabilir olana kadar bekle
            LoginButon.Click();
            CustomElementWait.WaitForLoad(driver);
        }
        #endregion

        #region Hata mesajları
        // hata mesajlarını göstermek için enum bir public oluşturuyoruz
        public enum ErrorMessages
        {
            SifreAltikarakterliolmalı, GecersizMailAdresi, SifreBosBirakilamaz, MailBosBirakilamaz
        }

        public string returnErrorMessage(ErrorMessages errorMessage)
        {
            switch (errorMessage)
            {
                case ErrorMessages.GecersizMailAdresi:
                    CustomElementWait.WaitUntilElementVisible(driver, By.XPath(invalidPasswordOrMailErrorTextXPath));
                    return invalidPasswordOrMailErrorText.Text;

                case ErrorMessages.SifreAltikarakterliolmalı:
                    CustomElementWait.WaitUntilElementVisible(driver, By.XPath(passwordLengtXPath));
                    return PasswordLengthErrorText.Text;

                case ErrorMessages.SifreBosBirakilamaz:
                    CustomElementWait.WaitUntilElementVisible(driver, By.XPath(PasswordErrorXPath));
                    return PasswordErrorText.Text;

                case ErrorMessages.MailBosBirakilamaz:
                    CustomElementWait.WaitUntilElementVisible(driver, By.XPath(MailErrorXPath));
                    return MailErrorText.Text;

                default:
                    return "";
            }
        }
        #endregion


        // Login üye giriş ve Kvkk butonları ekliyoruz.
        #region Tıklanabilir butonlar
        public void navigateLoginPage()   // element tıklanabilir olana kadar bekler. App config verilen süre kadar 10sn
        {
            CustomElementWait.WaitUntilElementClickable(driver, goUyeGiris);
            goUyeGiris.Click();
        }

        public void navigateLoginPageUyeOl()   // element tıklanabilir olana kadar bekler. App config verilen süre kadar 10sn
        {
            CustomElementWait.WaitUntilElementClickable(driver, goUyeOL);
            goUyeOL.Click();
        }

        public void KVKK()
        {
            CustomElementWait.WaitUntilElementClickable(driver, KVKKGizlilik);
            KVKKGizlilik.Click();
        }

        public void Gizlilik()
        {
            CustomElementWait.WaitUntilElementClickable(driver, GizlilikButtonu);
            GizlilikButtonu.Click();
        }
        #endregion
    }
}