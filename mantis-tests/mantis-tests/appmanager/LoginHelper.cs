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
    public class LoginHelper : HelperBase
    {
        public LoginHelper(ApplicationManager manager) : base(manager)
        {
        }

        public void Login(AccountData account)
        {
            if (IsLoggedIn())
            {
                if (IsLoggedIn(account))
                {
                    return;
                }
                Logout();
            }
            manager.Navigator.OpenMainPage();
            Type(By.Id("username"), account.Name);
            driver.FindElement(By.CssSelector(".btn-success")).Click();
            Type(By.Id("password"), account.Password);
            driver.FindElement(By.CssSelector(".btn-success")).Click();
        }

        public void Logout()
        {
            if (IsLoggedIn())
            {
                driver.FindElement(By.CssSelector("span.user-info")).Click();
                driver.FindElement(By.LinkText("выход")).Click();
            }
        }

        public bool IsLoggedIn()
        {
            return IsElementPresent(By.XPath("//a[contains(@href,'/logout_page.php')]"));
        }

        public bool IsLoggedIn(AccountData account)
        {
            return IsLoggedIn()
                && GetLoggetUserName() == account.Name;
        }

        public string GetLoggetUserName()
        {
            String text = driver.FindElement(By.XPath("//a[contains(@href,'/logout_page.php')]")).Text;
            return text;
        }
    }
}
