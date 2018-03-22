using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace mantis_tests
{
    public class NavigationHelper : HelperBase
    {
        public NavigationHelper(ApplicationManager manager) : base(manager)
        {
        }

        public void OpenManageOverviewPage()
        {
            if (!IsElementPresent(By.XPath("//div[@id='sidebar']/ul/li[@class='active']/a" + "/span[contains(text(),'управление')]")))
            {
                driver.FindElement(By.XPath("//div[@id='sidebar']/ul/li/a" + "/span[contains(text(),'управление')]")).Click();
            }
        }

        public void GoToManageProjPage()
        {
            if (!IsElementPresent(By.LinkText("Управление проектами")))
            {
                driver.FindElement(By.LinkText("Управление проектами")).Click();
            }
        }

        public void OpenMainPage()
        {
            if (driver.Url != "http://localhost/mantisbt-2.2.0/login_page.php")
            {
                manager.Driver.Url = "http://localhost/mantisbt-2.2.0/login_page.php";
            }
        }
    }
}
