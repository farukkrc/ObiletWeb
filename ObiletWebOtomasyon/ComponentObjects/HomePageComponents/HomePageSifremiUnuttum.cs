using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObiletWebOtomasyon.ComponentObjects.BaseComponent;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System.Configuration;
using System.Threading;
using ObiletWebOtomasyon.Base;
using ObiletWebOtomasyon.Common;
using NUnit.Framework;

namespace ObiletWebOtomasyon.ComponentObjects.HomePageComponents
{
    class HomePageSifremiUnuttum : BasePage
    {
        private IWebDriver driver; //web driver
        private WebDriverWait wait;// web driveri belirlenen olay gerçekleşene kadar istenilen süre kadar bekletmeyi sağlayan nesne
        private int timeoutWaitSecond = Convert.ToInt32(ConfigurationManager.AppSettings["TimeoutWaitSecond"]);
        public HomePageSifremiUnuttum(IWebDriver driver) : base(driver)
        {//her sayfadaki test driveri ilgili sayfada kullanılmasını sağlayan constructor
            this.driver = driver;
            PageFactory.InitElements(driver, this);//Bu yapı tanımlanarak [FindsBy] ek açıklaması ile ana sayfadaki web elementleri bulunabilir.
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutWaitSecond));//web driver istenilen durum gerçekleşene kadar 10 sn bekleyecek
        }

        // elementleri tanımlıyoruz önce
        #region UyeolandUyeGiriş 
        [FindsBy(How = How.XPath, Using = "//li[@class='login']")]
        private IWebElement LoginButton;

        [FindsBy(How = How.XPath, Using = "//*[@id='login-form']/div[4]/div[2]")]
        private IWebElement SifremiUnuttumButton;
        #endregion

        #region LoginInputlar
        private const string SifreModalXPath = "//div[contains(@id,'form')]";
        [FindsBy(How = How.XPath, Using = SifreModalXPath)]
        private IWebElement SifreModal;

        [FindsBy(How = How.XPath, Using = "//*[@id='forgot-password-form']/div[2]/input")]
        private IWebElement EmailText;

        [FindsBy(How = How.XPath, Using = "//div[contains(@class,'captcha')]")]
        private IWebElement Robot;

        [FindsBy(How = How.XPath, Using = "//*[@id='forgot-password-form']/div[4]/button")]
        private IWebElement Gönder;
        #endregion

        private const string EmailhatasiXPath = "//*[@id='email-error']";
        [FindsBy(How = How.XPath, Using = EmailhatasiXPath)]
        private IWebElement EmailhatasiXPathText;

        // Fonksiyonları tanımlıyoruz
        public void LoginClick(string email)
        {
            CustomElementWait.WaitUntilElementVisible(driver, By.XPath(SifreModalXPath)); // Modal olana kadar bekle
            ClearAndSenKeys(EmailText, email);
            CustomElementWait.WaitUntilElementClickable(driver, Robot);
            Robot.Click();
            //CustomElementWait.WaitUntilElementClickable(driver, Gönder); // buton tıklanabilir olana kadar bekle
            //Gönder.Click();
            CustomElementWait.WaitForLoad(driver);
        }

        public enum ErrorMessages
        {
            Emailhatali
        }

        public string returnErrorMessage(ErrorMessages errorMessage)
        {
            switch (errorMessage)
            {
                case ErrorMessages.Emailhatali:
                    CustomElementWait.WaitUntilElementVisible(driver, By.XPath(EmailhatasiXPath));
                    return EmailhatasiXPathText.Text;

                default:
                    return "";
            }
        }

        public void ButtonTiklama()  // element tıklanabilir olana kadar bekler. App config verilen süre kadar 10sn
        {
            CustomElementWait.WaitUntilElementClickable(driver, LoginButton);
            LoginButton.Click();
        }

        public void ButtonTiklama2()  // element tıklanabilir olana kadar bekler. App config verilen süre kadar 10sn
        {
            CustomElementWait.WaitUntilElementClickable(driver, SifremiUnuttumButton);
            SifremiUnuttumButton.Click();
        }
    }
}
