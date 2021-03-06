﻿using LetsTestATP.PageLib;
using log4net.Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using OpenQA.Selenium;

namespace LetsTestATP.Tests
{
    [TestClass]
    public class SanityTests
    {
        public static readonly log4net.ILog logger = log4net.LogManager.GetLogger(typeof(SanityTests));
        public static String strApplicationURL;
        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
 
            FirefoxDriverService service = FirefoxDriverService.CreateDefaultService(@"D:\EclipseWS\Repo001\LetsTestATP\Drivers\Firefox\", "geckodriver.exe");
            service.FirefoxBinaryPath = @"C:\Program Files (x86)\Mozilla Firefox\firefox.exe";
            PropertyCollection.driver = new FirefoxDriver(service);


            //FirefoxProfile profile = new FirefoxProfile();

            //profile.SetPreference("browser.download.manager.alertOnEXEOpen", false);
            //profile.SetPreference("browser.download.manager.closeWhenDone", true);
            //profile.SetPreference("browser.download.manager.focusWhenStarting", false);
            //profile.SetPreference("browser.download.dir", "D:\\EclipseWS\\Repo001\\LetsTestATP\\Downloads");
            //profile.SetPreference("browser.helperApps.neverAsk.saveToDisk", "application/octet-stream");
            //profile.SetPreference("browser.private.browsing.autostart", true);
            //var firingDriver = new EventFiringWebDriver(new FirefoxDriver(profile));
            //firingDriver.ExceptionThrown += PropertyCollection.firingDriver_TakeScreenshotOnException;
            //PropertyCollection.driver = firingDriver;

            PropertyCollection.strApplicationURL = ConfigurationManager.AppSettings["ApplicationURL_PRO"];
            PropertyCollection.driver.Navigate().GoToUrl(PropertyCollection.strApplicationURL);
            PropertyCollection.driver.Manage().Window.Maximize();
            PropertyCollection.driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));


            XmlConfigurator.Configure();
            BasicConfigurator.Configure();
        }
 
        [TestInitialize]
        public void Initialize()
        {

        }

        [TestMethod]
        public void TC_Sanity_A_VerifyHomePage()
        {
            HomePage objMainPage = new HomePage();
            objMainPage.VerifyHomePage();

            logger.Debug("***********ATP Sanity Test Execution**********");
            logger.Debug("Home Page verified Successfully");
        }
        [TestMethod]
        public void TC_Sanity_B_VerifyLogin()
        {
            LoginPage objLoginPage = new LoginPage();
            objLoginPage.ReadLogindata();
        }

        [TestCleanup]
        public void CleanUp()
        {
               
            PropertyCollection.driver.Navigate().GoToUrl(PropertyCollection.strApplicationURL);
            PropertyCollection.WaitForPageLoadComplete();
        }

        [ClassCleanup]
        public static void ClassCleanUp()
        {

            PropertyCollection.driver.Close();
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
    }
}
