using System;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Configuration;

namespace ObiletWebOtomasyon.Base.Helpers
{
    class CustomScreenShoot
    {
        public static void TakeScreenshot(IWebDriver driver, TestContext result)
        {
            try
            {
                string ssName = string.Format("{0}{1}-({2}).{3}", result.Result.Outcome.Status, result.Test.MethodName, DateTime.Now.ToLongDateString(), ScreenshotImageFormat.Png.ToString());
                //Take the screenshot
                Screenshot image = ((ITakesScreenshot)driver).GetScreenshot();
                image.SaveAsFile(ConfigurationManager.AppSettings["ScrenShootsPath"] + @"\" + ssName, ScreenshotImageFormat.Png);

            }
            catch (Exception)
            {
                //loglama yapılabilir
                throw;
            }
        }
    }
}
