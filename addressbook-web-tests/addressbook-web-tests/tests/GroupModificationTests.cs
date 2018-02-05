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
            app.Groups.Modify(1, NewData);
        }
    }
}