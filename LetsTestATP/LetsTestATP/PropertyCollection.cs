using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using log4net;
using log4net.Config;
using OpenQA.Selenium.Support.UI;
using System.Configuration;
using OpenQA.Selenium.Support.Events;
using LetsTestATP.Tests;

namespace LetsTestATP
{
    class PropertyCollection
    {
        public static IWebDriver driver { get; set; }
        public static String strApplicationURL { get; set; }
        public static String strBrowser { get; set; }
        public static String strChromeDriverPath { get; set; }
        public static String strIEDriverPath { get; set; }
        public static String strExportPath { get; set; }

        public static Boolean isAlertPresent()
        {
            try
            {
                PropertyCollection.driver.SwitchTo().Alert();
                return true;
            }   // try 
            catch (NoAlertPresentException Ex)
            {
                return false;
            }   // catch 
        }   // isAlertPresent()

        public static void AlertPresent_ClickOK()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();

                alert.Accept();
            }   // try 
            catch (NoAlertPresentException Ex)
            {

            }   // catch 
        }   // isAlertPresent()

        public static void WaitForPageLoadComplete()
        {
            IWait<IWebDriver> wait = new OpenQA.Selenium.Support.UI.WebDriverWait(PropertyCollection.driver, TimeSpan.FromSeconds(30));
            wait.Until(driver1 => ((IJavaScriptExecutor)PropertyCollection.driver).ExecuteScript("return document.readyState").Equals("complete"));
        }

        public static void firingDriver_TakeScreenshotOnException(object sender, WebDriverExceptionEventArgs e)
        {

            String strScreenshotsPath = ConfigurationManager.AppSettings["ScreenshotsPath"];
            Screenshot ss = ((ITakesScreenshot)PropertyCollection.driver).GetScreenshot();
            String strFilePath = strScreenshotsPath + SmokeTests.strSnapshot + "_" + DateTime.Now.ToString("ddMMyy_HHmmss") + ".png";
            ss.SaveAsFile(strFilePath, System.Drawing.Imaging.ImageFormat.Png);
        }

        public static Boolean isElementPresent(By by)
        {
            try
            {
                PropertyCollection.driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException e)
            {
                return false;
            }
        }

        public static Boolean isWebElementPresent(IWebElement we)
        {
            try
            {
                if (we.Displayed)
                    return true;
                else
                {
                    return false;
                }
            }
            catch (NoSuchElementException e)
            {
                return false;
            }
        }
    }

}
