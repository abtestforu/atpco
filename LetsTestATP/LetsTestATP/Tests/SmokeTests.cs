using LetsTestATP.PageLib;
using log4net.Config;
using log4net.Repository.Hierarchy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsTestATP.Tests
{
    public class SmokeTests
    {
        public static string strSnapshot { get; internal set; }
        public static readonly log4net.ILog logger = log4net.LogManager.GetLogger(typeof(SanityTests));
        public static String strApplicationURL;
        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {

            FirefoxDriverService service = FirefoxDriverService.CreateDefaultService(@"D:\EclipseWS\Repo001\LetsTestATP\Drivers\Firefox\", "geckodriver.exe");
            service.FirefoxBinaryPath = @"C:\Program Files (x86)\Mozilla Firefox\firefox.exe";
            PropertyCollection.driver = new FirefoxDriver(service);

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
        public void TC_Smoke_A_VerifySearch()
        {
            HomePage objMainPage = new HomePage();
            objMainPage.SearchBoxVerification();

            logger.Debug("***********ATP Smoke Test Execution**********");
            logger.Debug("Search Page verified Successfully");
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
