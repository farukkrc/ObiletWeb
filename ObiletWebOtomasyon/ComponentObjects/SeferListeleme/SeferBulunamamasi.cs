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

    class SeferBulunamamasi : BasePage
    {
        private IWebDriver driver; //web driver
        private WebDriverWait wait;// web driveri belirlenen olay gerçekleşene kadar istenilen süre kadar bekletmeyi sağlayan nesne
        private int timeoutWaitSecond = Convert.ToInt32(ConfigurationManager.AppSettings["TimeoutWaitSecond"]);
        public SeferBulunamamasi(IWebDriver driver) : base(driver)
        {//her sayfadaki test driveri ilgili sayfada kullanılmasını sağlayan constructor
            this.driver = driver;
            PageFactory.InitElements(driver, this);//Bu yapı tanımlanarak [FindsBy] ek açıklaması ile ana sayfadaki web elementleri bulunabilir.
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutWaitSecond));//web driver istenilen durum gerçekleşene kadar 10 sn bekleyecek
        }

        public void OtobusBiletiBulma()
        {
            CustomElementWait.WaitForLoad(driver);
            Thread.Sleep(2000);
            IWebElement Nereden = driver.FindElement(By.Id("origin"));
            Nereden.Click();
            CustomElementWait.WaitForLoad(driver);
            //Modal olana kadar bekle
            IWebElement visible = driver.FindElement(By.ClassName("visible"));
            CustomElementWait.WaitUntilElementVisible(driver, By.ClassName("visible"));
            CustomElementWait.WaitForLoad(driver);
            IWebElement NeredeInput = driver.FindElement(By.Id("origin-input"));
            NeredeInput.Click();
            CustomElementWait.WaitForLoad(driver);
            NeredeInput.SendKeys("Erzincan");
            CustomElementWait.WaitForLoad(driver);
            Thread.Sleep(1500);
            NeredeInput.SendKeys(Keys.Enter);
            CustomElementWait.WaitForLoad(driver);
            IWebElement Nereye = driver.FindElement(By.Id("destination"));
            Nereye.Click();
            CustomElementWait.WaitForLoad(driver);
            //Modal olana kadar bekle
            CustomElementWait.WaitUntilElementVisible(driver, By.ClassName("visible"));
            CustomElementWait.WaitForLoad(driver);
            IWebElement NereyeInput = driver.FindElement(By.Id("destination-input"));
            NereyeInput.Click();
            CustomElementWait.WaitForLoad(driver);
            NereyeInput.SendKeys("Perşembe");
            CustomElementWait.WaitForLoad(driver);
            Thread.Sleep(1500);
            NereyeInput.SendKeys(Keys.Enter);
            CustomElementWait.WaitForLoad(driver);
            IWebElement Bugun = driver.FindElement(By.Id("today"));
            Bugun.Click();
            CustomElementWait.WaitForLoad(driver);
            IWebElement Yarin = driver.FindElement(By.Id("tomorrow"));
            Yarin.Click();
            CustomElementWait.WaitForLoad(driver);
            CustomElementWait.WaitForLoad(driver);
            IWebElement Search = driver.FindElement(By.Id("search-button"));
            Search.Click();
            CustomElementWait.WaitForLoad(driver);
        }

        public void AppStore()
        {
            CustomElementWait.WaitForLoad(driver);
            IWebElement AppstoreButton = driver.FindElement(By.XPath("//a[@data-event-label='Clicked on iOS on no journey']"));
            CustomElementWait.WaitForLoad(driver);
            AppstoreButton.Click();
            CustomElementWait.WaitForLoad(driver);
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            //CustomElementWait.WaitForLoad(driver);
            driver.Close();
            driver.SwitchTo().Window(driver.WindowHandles.First());
            CustomElementWait.WaitForLoad(driver);
        }

        public void GooglePlay()
        {
            IWebElement GooglePlayButton = driver.FindElement(By.XPath("//a[@data-event-label='Clicked on Android on no journey']"));
            CustomElementWait.WaitForLoad(driver);
            GooglePlayButton.Click();
            CustomElementWait.WaitForLoad(driver);
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            //CustomElementWait.WaitForLoad(driver);
            driver.Close();
            driver.SwitchTo().Window(driver.WindowHandles.First());
            CustomElementWait.WaitForLoad(driver);
        }

        public void AlternatifRota()
        {
            IWebElement incelebutton = driver.FindElement(By.XPath("//*[@id='list']/li[3]"));
            incelebutton.Click();
            CustomElementWait.WaitForLoad(driver);
            Thread.Sleep(TimeSpan.FromSeconds(3));
            driver.Navigate().Back();
            CustomElementWait.WaitForLoad(driver);
        }

        public void AlternatifUcakSeferleri()
        {
            driver.FindElement(By.XPath("//li[@class='item flight']")).Click();
            CustomElementWait.WaitForLoad(driver);
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            CustomElementWait.WaitForLoad(driver);
            driver.Close();
            driver.SwitchTo().Window(driver.WindowHandles.First());
            CustomElementWait.WaitForLoad(driver);
        }


        // Hata mesajlarının ID lerini çıkarıyoruz.
        #region Hata mesajları Xpathleri
        private const string ContactAddressXpath = "//label[@class='error' and @for='alert-email']";
        [FindsBy(How = How.XPath, Using = ContactAddressXpath)]
        private IWebElement ContactAddressNullText;

        private const string invalidEmailAddresXpath = "//label[@class='error' and @for='alert-email']";
        [FindsBy(How = How.XPath, Using = invalidEmailAddresXpath)]
        private IWebElement EmailAdressErrorText;

        private const string ContactPhoneXpath = "//label[@class='error' and @for='alert-phone']";
        [FindsBy(How = How.XPath, Using = ContactPhoneXpath)]
        private IWebElement ContactPhoneNullText;

        private const string invalidPhoneAddresXpath = "//label[@class='error' and @for='alert-phone']";
        [FindsBy(How = How.XPath, Using = invalidPhoneAddresXpath)]
        private IWebElement PhoneErrorText;
        #endregion

        #region alarm kur pop-up inputları
        [FindsBy(How = How.Id, Using = "alert-email")]
        private IWebElement EmailInput;

        [FindsBy(How = How.Id, Using = "alert-phone")]
        private IWebElement PasswordInput;

        [FindsBy(How = How.XPath, Using = "//button[contains(@data-action,'save')]")]
        private IWebElement AlarmbuttonKur;
        #endregion

        public void AlternatifRotaAlarm(string email, string password)
        {

            IList<IWebElement> alarmKur = driver.FindElements(By.XPath("//li[@class='item alert single']"));

            foreach (var kur in alarmKur)
            {
                kur.Click();

                IWebElement Alarms = driver.FindElement(By.CssSelector("button[data-event-category='Bus Journey List']"));
                Alarms.Click();

                // Alarm Pop-up açılıp Model olana kadar beklemesini sağlıyoruz.
                CustomElementWait.WaitUntilElementVisible(driver, By.XPath("//div[@class='body content']"));
                CustomElementWait.WaitForLoad(driver);
                var Yolcusayısı = new SelectElement(driver.FindElement(By.Id("alert-pax-count")));
                //value ile select listen yolcu sayısı seçimi yapıyor.
                Yolcusayısı.SelectByValue("2");
                // 2 numaranın seçildiğini doğruluyor
                Assert.IsTrue(Yolcusayısı.SelectedOption.Text.Equals("2"));

                IWebElement butonKadın = driver.FindElement(By.XPath("//ob-checkbox[@class='female checked']"));
                butonKadın.Click();
                var Dogrumu = butonKadın.GetAttribute("class").Contains("female");
                Assert.IsTrue(Dogrumu);
                driver.FindElement(By.XPath("//ob-checkbox[@class='male checked']")).Click();
                Thread.Sleep(TimeSpan.FromSeconds(1));
                driver.FindElement(By.ClassName("female")).Click();
                Thread.Sleep(TimeSpan.FromSeconds(1));
                driver.FindElement(By.ClassName("male")).Click();
                CustomElementWait.WaitForLoad(driver);
                ClearAndSenKeys(EmailInput, email);
                ClearAndSenKeys(PasswordInput, password);
                //IWebElement AlarmEmail = driver.FindElement(By.Id("alert-email"));
                //AlarmEmail.Click();
                //AlarmEmail.SendKeys("faruk.kirci@obilet.com");
                //CustomElementWait.WaitForLoad(driver);
                //IWebElement AlarmPhone = driver.FindElement(By.Id("alert-phone"));
                //AlarmPhone.Click();
                //AlarmPhone.SendKeys("05538596190");
                //CustomElementWait.WaitForLoad(driver);
                //driver.FindElement(By.XPath("//button[contains(@data-action,'save')]")).Click();
                CustomElementWait.WaitUntilElementClickable(driver, AlarmbuttonKur); // buton tıklanabilir olana kadar bekle
                AlarmbuttonKur.Click();
                CustomElementWait.WaitForLoad(driver);

            }
        }

        #region Hata mesajları
        // hata mesajlarını göstermek için enum bir public oluşturuyoruz
        public enum ErrorMessages
        {
            GecersizEpostaAdresi, GecersizTelefonNumarasi, EnAzbirEpostaGirin, EnazbirTelefonGirin
        }

        public string returnErrorMessage(ErrorMessages errorMessage)
        {
            switch (errorMessage)
            {
                case ErrorMessages.EnAzbirEpostaGirin:
                    CustomElementWait.WaitUntilElementVisible(driver, By.XPath(ContactAddressXpath));
                    return ContactAddressNullText.Text;

                case ErrorMessages.GecersizEpostaAdresi:
                    CustomElementWait.WaitUntilElementVisible(driver, By.XPath(invalidEmailAddresXpath));
                    return EmailAdressErrorText.Text;

                case ErrorMessages.EnazbirTelefonGirin:
                    CustomElementWait.WaitUntilElementVisible(driver, By.XPath(ContactPhoneXpath));
                    return ContactPhoneNullText.Text;

                case ErrorMessages.GecersizTelefonNumarasi:
                    CustomElementWait.WaitUntilElementVisible(driver, By.XPath(invalidPhoneAddresXpath));
                    return PhoneErrorText.Text;

                default:
                    return "";
            }
        }
        #endregion

    }
}
