using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsTestATP.PageLib
{
    class HomePage
    {
        public HomePage()
        {
            PageFactory.InitElements(PropertyCollection.driver, this);
        }

        [FindsBy(How = How.LinkText, Using = "Home")]
        public IWebElement LnkHome { get; set; }

        [FindsBy(How = How.LinkText, Using = "Sitemap")]
        public IWebElement LnkSiteMap { get; set; }

        [FindsBy(How = How.LinkText, Using = "Customer Center Log In")]
        public IWebElement LnkCustomerCenterLogin { get; set; }

        [FindsBy(How = How.Id, Using = "edit-search-block-form--2")]
        public IWebElement SearchBox { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='logo']/img")]
        public IWebElement ATPCOIMG { get; set; }

        [FindsBy(How = How.LinkText, Using = "Corporate")]
        public IWebElement LnkCorporate { get; set; }

        [FindsBy(How = How.LinkText, Using = "Products")]
        public IWebElement LnkProducts { get; set; }

        [FindsBy(How = How.LinkText, Using = "Events")]
        public IWebElement LnkEvents { get; set; }

        [FindsBy(How = How.LinkText, Using = "Career")]
        public IWebElement LnkCareer { get; set; }

        [FindsBy(How = How.LinkText, Using = "Training")]
        public IWebElement LnkTraining { get; set; }

        [FindsBy(How = How.LinkText, Using = "Contact")]
        public IWebElement LnkContact { get; set; }

        [FindsBy(How = How.LinkText, Using = "System")]
        public IWebElement LnkSystem { get; set; }

        internal void VerifyHomePage()
        {
            {
                /* buffer to hold your errors */
                StringBuilder errorBuffer = new StringBuilder();
                try
                {
                    LnkHome.Click();
                    PropertyCollection.WaitForPageLoadComplete();
                    Assert.AreEqual("About ATPCO | ATPCO", PropertyCollection.driver.Title);
                }
                catch (Exception ex) { errorBuffer.Append(ex.Message + "\n"); }

            }
        }
    }
}
