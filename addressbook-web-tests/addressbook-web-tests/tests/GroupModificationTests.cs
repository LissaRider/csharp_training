using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupModificationTests : TestBase
    {
        [Test]
        public void GroupModificationTest()
        {
            GroupData NewData = new GroupData("Leadership1");
            NewData.Header = "Leadership1";
            NewData.Footer = "ANBR";

            app.Groups.Modify(1, NewData);
        }
    }
}
