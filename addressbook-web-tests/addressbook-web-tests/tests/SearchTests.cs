using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class SearchTests : AuthTestBase
    {
        [Test]
        public void TestSearch()
        {
            System.Console.Out.Write(app.Contacts.GetNumberOfSearchResults());
        }

        [Test]
        public void ComparisonSearchResultsAndContactStringsTest()
        {
            int fromSearch = app.Contacts.GetNumberOfSearchResults();
            int fromTable = app.Contacts.GetContactCount();

            Assert.AreEqual(fromSearch, fromTable);
        }
    }
}
