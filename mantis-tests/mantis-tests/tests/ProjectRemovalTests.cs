using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace mantis_tests
{
    [TestFixture]
    public class ProjectRemovalTests : AuthTestBase
    {
        [Test]
        public void ProjectRemovalTest()
        {
            ProjectData project = new ProjectData()
            {
                Name = "Project",
            };

            app.Projects.VerifyProjectPresence(account, project);

            //List<ProjectData> oldProjects = app.Projects.GetProjectsList();
            List<ProjectData> oldProjects = app.Projects.GetProjectsList(account);
            ProjectData toBeRemoved = oldProjects[0];

            app.Projects.Remove(toBeRemoved);

            Assert.AreEqual(oldProjects.Count - 1, app.Projects.GetProjectCount());

            //List<ProjectData> newProjects = app.Projects.GetProjectsList();
            List<ProjectData> newProjects = app.Projects.GetProjectsList(account);
            oldProjects.RemoveAt(0);

            Assert.AreEqual(oldProjects, newProjects);
        }
    }
}