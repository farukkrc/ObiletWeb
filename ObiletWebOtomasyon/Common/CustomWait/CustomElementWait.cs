using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ObiletWebOtomasyon.Common
{
    class CustomElementWait
    {
        private static int TIMEOUT_WAIT_SECONDS = Convert.ToInt32(ConfigurationManager.AppSettings["TimeoutWaitSecond"]);

        public static void WaitUntilElementFind(IWebDriver driver, By path)
        {
            try
            {
                /*IWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3))
                IWebElement element = wait.Until(driver => driver.FindElement(By.Name("q"))); */
                IWebElement myDynamicElement = (new WebDriverWait(driver, TimeSpan.FromSeconds(TIMEOUT_WAIT_SECONDS)))
                    .Until(drv => drv.FindElement(path));
            }
            catch (TimeoutException e)
            {
                // CAN DO : log
                throw e;
            }
        }

        // Görüne ne kadar bekle
        public static void WaitUntilElementVisible(IWebDriver driver, By path)
        {
            try
            {
                IWebElement myDynamicElementVisible = (new WebDriverWait(driver, TimeSpan.FromSeconds(TIMEOUT_WAIT_SECONDS)))
                        .Until(ExpectedConditions.ElementIsVisible(path));
            }
            catch (TimeoutException e)
            {
                // TODO : log
                throw e;
            }
        }
        // Element görüne ne kadar bekle
        public static void WaitUntilElementVisibilityOfAllElment(IWebDriver driver, By path)
        {
            try
            {
                ReadOnlyCollection<IWebElement> myDynamicElementVisible = (new WebDriverWait(driver, TimeSpan.FromSeconds(TIMEOUT_WAIT_SECONDS)))
                        .Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(path));
            }
            catch (TimeoutException e)
            {
                // TODO : log
                throw e;
            }
        }

        // Element tıklanabilir olana kadar bekle
        public static void WaitUntilElementClickable(IWebDriver driver, IWebElement element)
        {
            try
            {
                IWebElement myDynamicElementClickable = (new WebDriverWait(driver, TimeSpan.FromSeconds(TIMEOUT_WAIT_SECONDS)))
                        .Until(ExpectedConditions.ElementToBeClickable(element));
            }
            catch (TimeoutException e)
            {
                // TODO : log
                throw e;
            }
        }

        internal static void WaitUntilElementClickable(IWebElement webElement)
        {
            throw new NotImplementedException();
        }

        //Görünmez Olana Kadar Bekle
        public static void WaitUntilElementInvisible(IWebDriver driver, By path)
        {
            try
            {
                Boolean myDynamicElementInVisible = (new WebDriverWait(driver, TimeSpan.FromSeconds(TIMEOUT_WAIT_SECONDS)))
                        .Until(ExpectedConditions.InvisibilityOfElementLocated(path));
            }
            catch (TimeoutException e)
            {
                // TODO : log
                throw e;
            }
        }
        // Eleman Var Olana Kadar Bekle
        public static void WaitUntilElementExists(IWebDriver driver, By path)
        {
            try
            {
                IWebElement myDynamicElementExists = (new WebDriverWait(driver, TimeSpan.FromSeconds(TIMEOUT_WAIT_SECONDS)))
                        .Until(ExpectedConditions.ElementExists(path));

            }
            catch (TimeoutException e)
            {
                // TODO : log
                throw e;
            }
        }
        // Bulunan Tüm Elemanların Bulunmasını Bekle
        public static void WaitUntilElementPresenceOfAllElementsLocatedBy(IWebDriver driver, By path)
        {
            try
            {
                ReadOnlyCollection<IWebElement> myDynamicElementLoad = (new WebDriverWait(driver, TimeSpan.FromSeconds(TIMEOUT_WAIT_SECONDS)))
                        .Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(path));
            }
            catch (TimeoutException e)
            {
                // TODO : log
                throw e;
            }
        }
        //Yüklemeyi Bekle
        public static void WaitForLoad(IWebDriver driver)
        {
            try
            {
                int delay;
                delay = TIMEOUT_WAIT_SECONDS;
                while (delay > 0)
                {
                    WaitForAjax(driver);
                    var documentReady = (bool)(driver as IJavaScriptExecutor)
                        .ExecuteScript("return document.readyState").ToString().Equals("complete");

                    if (documentReady) break;
                    Thread.Sleep(1000);
                    delay--;
                }
            }
            catch (ThreadInterruptedException)
            {
                throw; // TODO : log
            }
        }

        public static void WaitForAjax(IWebDriver driver)
        {
            try
            {
                int delay = 0;
                delay = TIMEOUT_WAIT_SECONDS;
                while (delay > 0)
                {
                    var ajaxIsComplete = (bool)(driver as IJavaScriptExecutor)
                         .ExecuteScript("return jQuery.active").ToString().Equals("0");

                    Thread.Sleep(1000);
                    delay--;
                    if (ajaxIsComplete) return;
                }
            }
            catch (ThreadInterruptedException)
            {
                throw; // TODO : log
            }
        }
    }
}
