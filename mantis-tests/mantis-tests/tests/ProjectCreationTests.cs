using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;

namespace mantis_tests
{
    [TestFixture]
    public class ProjectCreationTests : AuthTestBase
    {
        [Test]
        public void ProjectCreationTest()
        {
            ProjectData project = new ProjectData()
            {
                Name = "New Project1",
                Description = "New Project1 Description"
            };

            //app.Projects.VerifySameProjectPresence(project);
            app.Projects.VerifySameProjectPresence(account, project);

            //List<ProjectData> oldProjects = app.Projects.GetProjectsList();
            List<ProjectData> oldProjects = app.Projects.GetProjectsList(account);

            app.Projects.Create(project);

            Assert.AreEqual(oldProjects.Count + 1, app.Projects.GetProjectCount());

            //List<ProjectData> newProjects = app.Projects.GetProjectsList();
            List<ProjectData> newProjects = app.Projects.GetProjectsList(account);
            oldProjects.Add(project);
            oldProjects.Sort();
            newProjects.Sort();

            Assert.AreEqual(oldProjects, newProjects);
        }
    }
}