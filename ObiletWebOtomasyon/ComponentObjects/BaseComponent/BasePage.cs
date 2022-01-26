using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;
namespace ObiletWebOtomasyon.ComponentObjects.BaseComponent
{
    public abstract class BasePage
    {
        protected BasePage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }
        IWebDriver driver;
        public void ClearAndSenKeys(IWebElement element, string value)
        {
            ScrollToElement(element);
            element.Click();
            element.Clear();
            element.SendKeys(value);
            
        }
        public void ScrollToElement(IWebElement element)
        {
            Actions actions = new Actions(driver);
            actions.MoveToElement(element);
            actions.Perform();
        }
        public IWebElement ExistsElement(string XPath)
        {
            IWebElement element;
            try { element = driver.FindElement(By.XPath(XPath)); }
            catch (NoSuchElementException e) { element = null; }
            //catch (StaleElementReferenceException e) { element = null; }
            return element;
        }
    }
}
