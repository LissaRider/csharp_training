﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupModificationTests : AuthTestBase
    {
        [Test]

        public void GroupModificationTest()
        {
            GroupData NewData = new GroupData("Leadership1");
            NewData.Header = null;
            NewData.Footer = null;

            app.Groups.VerifyGroupPresence();

            List<GroupData> oldGroups = app.Groups.GetGroupsList();

            app.Groups.Modify(0, NewData);

            Assert.AreEqual(oldGroups.Count, app.Groups.GetGroupCount());

            List<GroupData> newGroups = app.Groups.GetGroupsList();
            oldGroups[0].Name = NewData.Name;
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }
    }
}