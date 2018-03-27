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

        public void Create(AccountData account, ProjectData projectData)
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            Mantis.ProjectData project = new Mantis.ProjectData();
            project.name = projectData.Name;
            client.mc_project_add(account.Name, account.Password, project);
        }

        private void Remove(AccountData account, string projectId)
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            client.mc_project_delete(account.Name, account.Password, projectId);
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
            ICollection<IWebElement> elements = driver.FindElements(By.CssSelector(".table"))[0]
                .FindElements(By.CssSelector("tbody>tr"));
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
        
        public List<ProjectData> GetProjectsList(AccountData account)
        {
            List<ProjectData> list = new List<ProjectData>();

            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            Mantis.ProjectData[] projects = client.mc_projects_get_user_accessible(account.Name, account.Password);
            foreach (Mantis.ProjectData project in projects)
            {
                list.Add(new ProjectData()
                {
                    Name = project.name,
                    Description = project.description
                });
            }
            return list;
        }

        public int GetProjectCount()
        {
            manager.Navigator.GoToManageProjPage();
            return driver.FindElements(By.CssSelector(".table"))[0].FindElements(By.CssSelector("tbody>tr")).Count();
        }
        
        public void VerifyProjectPresence(ProjectData project)
        {
            manager.Navigator.GoToManageProjPage();

            if (!IsElementPresent(By.XPath("//a[contains(@href,'manage_proj_edit_page.php?project_id')]")))
            {
                Create(project);
            }
        }        

        public void VerifyProjectPresence(AccountData account, ProjectData project)
        {
            if (GetProjectsList(account).Count == 0)
            {
                Create(account, project);
            }
        }
        
        public void VerifySameProjectPresence(ProjectData project)
        {
            manager.Navigator.GoToManageProjPage();
            if (IsElementPresent(By.XPath("//table[1]/tbody/tr/td[1]/a[.='" + project.Name + "']")))
            {
                Remove(project);
            }
        }        

        public void VerifySameProjectPresence(AccountData account, ProjectData project)
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            string projectId = client.mc_project_get_id_from_name(account.Name, account.Password, project.Name);

            if (projectId != null && projectId != "0")
            {
                Remove(account, projectId);
            }
        }
    }
}