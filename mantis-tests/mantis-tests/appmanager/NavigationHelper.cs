using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace mantis_tests
{

    public class NavigationHelper : HelperBase
    {
        private string baseURL;

        public NavigationHelper(ApplicationManager manager, string baseURL) : base(manager)
        {
            this.baseURL = baseURL;
        }

        public void OpenMainPage()
        {
            if (driver.Url == baseURL + "/login_page.php")
            {
                return;
            }
            driver.Navigate().GoToUrl(baseURL + "/login_page.php");
        }

        public void GoToManageProjPage()
        {
            if (driver.Url == baseURL + "/manage_proj_page.php"
                && IsElementPresent(By.XPath("//form[@action='manage_proj_create_page.php']/fieldset/button")))
            {
                return;
            }
            OpenManageOverviewPage();
            OpenManageProjPage();
            //driver.Manage().Timeouts().ImplicitWait = new TimeSpan(40);
        }

        public void OpenManageOverviewPage()
        {
            driver.FindElement(
                By.XPath("//div[@id='sidebar']//a[contains(@href,'/manage_overview_page.php')]"))
                .Click();
        }

        public void OpenManageProjPage()
        {
            driver.FindElement(
                By.XPath("//div[@id='main-container']//a[contains(@href,'/manage_proj_page.php')]"))
                .Click();
        }
    }
}