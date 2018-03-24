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
    public class ProjectManagementHelper : HelperBase
    {
        public ProjectManagementHelper(ApplicationManager manager) : base(manager)
        {
        }        

        public void Create(ProjectData project)
        {
            manager.Navigator.GoToManageProjPage();
            InitProjectCreation();
            FillProjectForm(project);
            SubmitProjectCreation();
        }

        public void Remove(ProjectData project)
        {
            manager.Navigator.GoToManageProjPage();
            OpenManageProjEditPage(project.Name);
            RemoveProject();
            SubmitProjectRemoval();
        }

        // Project creation methods
        public void InitProjectCreation()
        {
            driver.FindElement(By.XPath("//form[@action='manage_proj_create_page.php']/fieldset/button")).Click();
        }

        public void FillProjectForm(ProjectData project)
        {
            Type(By.Id("project-name"), project.Name);
            Type(By.Id("project-description"), project.Description);
        }

        public void SubmitProjectCreation()
        {
            driver.FindElement(By.XPath("//form[@id='manage-project-create-form']//div[contains(@class,'widget-toolbox')]/input")).Click();
            driver.FindElement(By.XPath("//a[contains(@href, 'manage_proj_page.php')]")).Click();
        }        

        // Project removal methods
        public void OpenManageProjEditPage(String name)
        {
            driver.FindElement(By.LinkText(name)).Click();
        }

        public void RemoveProject()
        {
            driver.FindElement(By.XPath("//form[@id='project-delete-form']/fieldset/input[contains(@class,'btn')]")).Click();
        }

        public void SubmitProjectRemoval()
        {
            driver.FindElement(By.XPath("//form[@class='center']/input[contains(@class,'btn')]")).Click();
        }        

        // Verification
        public List<ProjectData> GetProjectsList()
        {
            List<ProjectData> list = new List<ProjectData>();
            manager.Navigator.GoToManageProjPage();
            ICollection<IWebElement> elements = driver.FindElements(By.XPath("//table"))[0]
                .FindElements(By.XPath("//tbody/tr"));
            foreach (IWebElement element in elements)
            {
                list.Add(new ProjectData()
                {
                    Name = element.FindElements(By.XPath("//td"))[0].Text,
                    Description = element.FindElements(By.XPath("//td"))[4].Text
                });
            }
            return list;
        }

        public int GetProjectCount()
        {
            manager.Navigator.GoToManageProjPage();
            return driver.FindElements(By.XPath("//table"))[0]
                .FindElements(By.XPath("//tbody/tr"))
                .Count();
        }

        public void VerifyProjectPresence()
        {
            manager.Navigator.GoToManageProjPage();

            if (!IsElementPresent(By.XPath("//a[contains(@href,'manage_proj_edit_page.php?project_id')]")))
            {
                ProjectData project = new ProjectData()
                {
                    Name = "Test Project",
                    Description = "Test Project Description"
                };
                Create(project);
            }
        }

        public void VerifySameProjectPresence(ProjectData project)
        {
            manager.Navigator.GoToManageProjPage();

            if (IsElementPresent(By.XPath("//a[contains(text(),'" + project.Name + "') and contains(@href,'manage_proj_edit_page.php?project_id')]")))
            {
                Remove(project);
            }
        }
    }
}