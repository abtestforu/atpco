using log4net;
using log4net.Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsTestATP.PageLib
{
    class LoginPage
    {
        public static readonly ILog logger = LogManager.GetLogger(typeof(LoginPage));
        public LoginPage()
        {
            PageFactory.InitElements(PropertyCollection.driver, this);

            log4net.Config.XmlConfigurator.Configure();
            BasicConfigurator.Configure();
        }

        [FindsBy(How = How.Id, Using = "TxtUserName")]
        public IWebElement TxtUsername { get; set; }

        [FindsBy(How = How.Id, Using = "TxtPassword")]
        public IWebElement TxtPassword { get; set; }

        [FindsBy(How = How.Id, Using = "BtnLogin")]
        public IWebElement BtnLogin { get; set; }

        //[FindsBy(How = How.LinkText, Using = "Dashboard")]
        //public IWebElement LnkDashboard { get; set; }


        [FindsBy(How = How.Id, Using = "DdlUserProfile")]
        public IWebElement btnUserProfile { get; set; }

        [FindsBy(How = How.LinkText, Using = "LogOut")]
        public IWebElement LnkLogOut { get; set; }

        [FindsBy(How = How.Id, Using = "alertMessage")]
        public IWebElement msgalertMessage { get; set; }

        public static String strUsername_Admin, strPassword_Admin, strUsername_CertCoordinator, strPassword_CertCoordinator, strUsername_LicenseeMaster, strPassword_LicenseeMaster;

        public void ReadLogindata()
        {
            String strDataSheetPath = ConfigurationManager.AppSettings["DataSheetPath"];
            ExcelLib.PopulateInCollection(@"" + strDataSheetPath + "LoginInfo.xls");

            for (int i = 1; i < 10; i++)
            {
                if (ExcelLib.ReadData(i, "Role") == ConfigurationManager.AppSettings["Role"])
                {
                    strUsername_Admin = ExcelLib.ReadData(1, "Username");
                    strPassword_Admin = ExcelLib.ReadData(1, "Password");
                    break;
                }
            }
            strUsername_CertCoordinator = ExcelLib.ReadData(2, "Username");
            strPassword_CertCoordinator = ExcelLib.ReadData(2, "Password");
            strUsername_LicenseeMaster = ExcelLib.ReadData(4, "Username");
            strPassword_LicenseeMaster = ExcelLib.ReadData(4, "Password");
        }

        public void LoginSuccessful(String strUsername, String strPassword)
        {
           
            HomePage objHomePage = new HomePage();

            objHomePage.LnkCustomerCenterLogin.Click();
            new WebDriverWait(PropertyCollection.driver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementToBeClickable(TxtUsername));
            Assert.AreEqual(true, PropertyCollection.isWebElementPresent(TxtUsername));
            Assert.AreEqual(true, PropertyCollection.isWebElementPresent(TxtPassword));
            TxtUsername.SendKeys(strUsername);
            TxtPassword.SendKeys(strPassword);

            BtnLogin.Click();
            new WebDriverWait(PropertyCollection.driver, TimeSpan.FromSeconds(20)).Until(ExpectedConditions.ElementToBeClickable(objHomePage.LnkHome));
            objHomePage.LnkHome.Click();
        }

        public void LoginFailed()
        {
            HomePage objMainPage = new HomePage();
            objMainPage.LnkCustomerCenterLogin.Click();
            new WebDriverWait(PropertyCollection.driver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementToBeClickable(TxtUsername));

            TxtUsername.SendKeys("abc");
            TxtPassword.SendKeys("abc");

            BtnLogin.Click();
            new WebDriverWait(PropertyCollection.driver, TimeSpan.FromSeconds(20)).Until(ExpectedConditions.ElementToBeClickable(msgalertMessage));
            Assert.IsTrue(msgalertMessage.Displayed);

        }

        public void ClickDashboard()
        {
            try
            {

                HomePage objHomePage = new HomePage();
                if (PropertyCollection.isWebElementPresent(objHomePage.LnkHome))
                {
                    objHomePage.LnkHome.Click();
                    PropertyCollection.WaitForPageLoadComplete();
                    Assert.AreEqual("ATPCO", PropertyCollection.driver.Title);
                }
            }
            catch (Exception ex)
            {
                PropertyCollection.driver.Navigate().GoToUrl(PropertyCollection.strApplicationURL);
                PropertyCollection.driver.Manage().Window.Maximize();


                logger.Debug(System.Reflection.MethodBase.GetCurrentMethod().Name + " - Failed");

            }
        }
    }
}
