using LetsTestATP.PageLib;
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

namespace LetsTestATP.Tests
{
    [TestClass]
    class SanityTests
    {
        public static readonly log4net.ILog logger = log4net.LogManager.GetLogger(typeof(SanityTests));
        public static String strApplicationURL;
        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            FirefoxProfile profile = new FirefoxProfile();

            profile.SetPreference("browser.download.manager.alertOnEXEOpen", false);
            profile.SetPreference("browser.download.manager.closeWhenDone", true);
            profile.SetPreference("browser.download.manager.focusWhenStarting", false);
            profile.SetPreference("browser.download.dir", "D:\\EclipseWS\\Repo001\\LetsTestATP\\Downloads");
            profile.SetPreference("browser.helperApps.neverAsk.saveToDisk", "application/octet-stream");

            var firingDriver = new EventFiringWebDriver(new FirefoxDriver(profile));

            firingDriver.ExceptionThrown += PropertyCollection.firingDriver_TakeScreenshotOnException;
            PropertyCollection.driver = firingDriver;

            PropertyCollection.strApplicationURL = ConfigurationManager.AppSettings["ApplicationURL_PRO"];
            PropertyCollection.driver.Navigate().GoToUrl(PropertyCollection.strApplicationURL);
            PropertyCollection.driver.Manage().Window.Maximize();
            PropertyCollection.driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));

            LoginPage objLoginPage = new LoginPage();
            objLoginPage.ReadLogindata();

            log4net.Config.XmlConfigurator.Configure();
            BasicConfigurator.Configure();
        }

        [TestInitialize]
        public void Initialise()
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
