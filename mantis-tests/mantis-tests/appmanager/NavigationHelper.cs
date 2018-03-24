using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace mantis_tests
{

    public class NavigationHalper : HelperBase
    {
        public NavigationHalper(ApplicationManager manager) : base(manager) { }

        public void OpenMainPage()
        {
            if (driver.Url != "http://localhost/mantisbt-2.12.0/login_page.php")
            {
                manager.Driver.Url = "http://localhost/mantisbt-2.12.0/login_page.php";
            }
        }

        public void OpenManageOverviewPage()
        {
            if (!IsElementPresent(By.XPath("//div[@id='sidebar']/ul/li[@class='active']/a[contains(@href,'manage_overview_page.php')]")))
            {
                driver.FindElement(By.XPath("//div[@id='sidebar']/ul/li[@class='active']/a[contains(@href,'manage_overview_page.php')]")).Click();
            }
        }

        public void GoToManageProjPage()
        {
            OpenManageOverviewPage();
            if (!IsElementPresent(By.XPath("//div[@id='main-container']//li[@class='active']/a[contains(@href,'manage_proj_page.php')]")))
            {
                driver.FindElement(By.XPath("//div[@id='main-container']//li[@class='active']/a[contains(@href,'manage_proj_page.php')]")).Click();
            }
        }
    }
}