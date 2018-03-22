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
    public class ProjectHelper : HelperBase
    {
        public ProjectHelper(ApplicationManager manager) : base(manager)
        {
        }

        public void Create(ProjectData project)
        {
            manager.Navigator.OpenManageOverviewPage();
            manager.Navigator.GoToManageProjPage();
            InitProjectCreation();
            FillProjectForm(project);
            SubmitProjectCreation();
        }

        // Project creation methods
        public void InitProjectCreation()
        {
            driver.FindElement(
                By.XPath("//form[@action='manage_proj_create_page.php']/fieldset/input")).Click();
        }

        public void FillProjectForm(ProjectData project)
        {
            Type(By.Id("project-name"), project.Name);
            Type(By.Id("project-description"), project.Description);
        }

        public void SubmitProjectCreation()
        {
            driver.FindElement(
                By.XPath("//div[contains(@class,'widget-toolbox')]/input")).Click();
            driver.FindElement(By.XPath("//a[contains(@href,'manage_proj_page.php')]")).Click();
        }

        // Verification
        public List<ProjectData> GetProjectsList()
        {
            List<ProjectData> list = new List<ProjectData>();
            manager.Navigator.GoToManageProjPage();
            ICollection<IWebElement> elements = driver.FindElements(By.XPath("//div[@class='table-responsive']/table"))[0]
                .FindElements(By.XPath("//tbody/tr"));
            foreach (IWebElement element in elements)
            {
                list.Add(new ProjectData()
                {
                    Name = element.FindElements(By.CssSelector("td"))[0].Text,
                    Description = element.FindElements(By.CssSelector("td"))[4].Text
                });
            }
            return list;
        }

        public int GetProjectCount()
        {
            manager.Navigator.GoToManageProjPage();
            return driver.FindElements(By.XPath("//div[@class='table-responsive']/table"))[0]
                .FindElements(By.XPath("//tbody/tr")).Count();
        }
        /*
        public void VerifySameProjectPresence(ProjectData project)
        {
            manager.Navigator.GoToManageProjPage();

            if (IsElementPresent(By.XPath("//table[1]/tbody/tr/td[1]/a[.='" + project.Name + "']")))
            {
                Remove(project);
            }
        }
        */
    }
}
