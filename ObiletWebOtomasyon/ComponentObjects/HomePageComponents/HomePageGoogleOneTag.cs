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
using System;

namespace ObiletWebOtomasyon.ComponentObjects.HomePageComponents
{
    class HomePagegoogleOneTag : BasePage
    {
        private IWebDriver driver; //web driver
        private WebDriverWait wait;// web driveri belirlenen olay gerçekleşene kadar istenilen süre kadar bekletmeyi sağlayan nesne
        private int timeoutWaitSecond = Convert.ToInt32(ConfigurationManager.AppSettings["TimeoutWaitSecond"]);
        public HomePagegoogleOneTag(IWebDriver driver) : base(driver)
        {//her sayfadaki test driveri ilgili sayfada kullanılmasını sağlayan constructor
            this.driver = driver;
            PageFactory.InitElements(driver, this);//Bu yapı tanımlanarak [FindsBy] ek açıklaması ile ana sayfadaki web elementleri bulunabilir.
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutWaitSecond));//web driver istenilen durum gerçekleşene kadar 10 sn bekleyecek

        }

        


    }

}
