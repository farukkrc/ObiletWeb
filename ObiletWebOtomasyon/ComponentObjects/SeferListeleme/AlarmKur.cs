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

    class AlarmKur : BasePage
    {
        private IWebDriver driver; //web driver
        private WebDriverWait wait;// web driveri belirlenen olay gerçekleşene kadar istenilen süre kadar bekletmeyi sağlayan nesne
        private int timeoutWaitSecond = Convert.ToInt32(ConfigurationManager.AppSettings["TimeoutWaitSecond"]);
        public AlarmKur(IWebDriver driver) : base(driver)
        {//her sayfadaki test driveri ilgili sayfada kullanılmasını sağlayan constructor
            this.driver = driver;
            PageFactory.InitElements(driver, this);//Bu yapı tanımlanarak [FindsBy] ek açıklaması ile ana sayfadaki web elementleri bulunabilir.
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutWaitSecond));//web driver istenilen durum gerçekleşene kadar 10 sn bekleyecek
        }



        #region LoginModal
        //private const string alarpopbekleme = "//div[@class='body content']";
        //[FindsBy(How = How.XPath, Using = alarpopbekleme)]
        //private IWebElement loginModal;
        #endregion

        #region Alarm Kur
        public void AlarmKurma()
        {
            IList<IWebElement> alarmKur = driver.FindElements(By.XPath("//li[contains(@class, 'item journey  full ') and starts-with(@id, 'journey-')]"));

            foreach (var kur in alarmKur)
            {

                kur.Click();

                CustomElementWait.WaitForLoad(driver);
                var openedSefer = driver.FindElement(By.XPath("//li[starts-with(@id, '" + kur.GetAttribute("id") + "')]"));
                var hasOpenClass = openedSefer.GetAttribute("class").Contains("open");
                CustomElementWait.WaitForLoad(driver);

                //IWebElement alarmButtun = driver.FindElement(By.CssSelector("button[data-event-category='Bus Journey List']"));
                //alarmButtun.Click();

                //IWebElement close = driver.FindElement(By.XPath("//*[@class='item journey full open success']/div[1]/div[5]/button[2]"));
                //close.Click();
                //CustomElementWait.WaitForLoad(driver);
                IWebElement Alarms = driver.FindElement(By.ClassName("ready"));
                Alarms.Click();
               
          

                CustomElementWait.WaitUntilElementVisible(driver, By.XPath("//div[@class='body content']"));
                CustomElementWait.WaitForLoad(driver);
                IWebElement PopUpAlarms = driver.FindElement(By.XPath("//button[contains(text(),'Alarm Kur')]"));
                PopUpAlarms.Click();
                CustomElementWait.WaitForLoad(driver);

                IWebElement butonKadın = driver.FindElement(By.XPath("//ob-checkbox[@class='female checked']"));
                butonKadın.Click();
                var Dogrumu = butonKadın.GetAttribute("class").Contains("female");
                Assert.IsTrue(Dogrumu);
                CustomElementWait.WaitForLoad(driver);
                IWebElement Emailİnput = driver.FindElement(By.Id("alert-email"));
                Emailİnput.Click();
                Emailİnput.SendKeys("faruk.kirci@obilet.com");
                CustomElementWait.WaitForLoad(driver);
                IWebElement Phoneİnput = driver.FindElement(By.Id("alert-phone"));
                Phoneİnput.Click();
                Phoneİnput.SendKeys("5538596190");
                CustomElementWait.WaitForLoad(driver);
                PopUpAlarms.Click();
                IWebElement PopUpTamam = driver.FindElement(By.XPath("//div[@class='success']//button[@type='button']"));
                PopUpTamam.Click();
                CustomElementWait.WaitForLoad(driver);
                  // Bu kısma tekrar bakılacak alarm kuruldu ise başka sefere  yeni alarm kurma işlemi yapılması gereklidir.
                break;
            }

        }
        #endregion



    }
}
