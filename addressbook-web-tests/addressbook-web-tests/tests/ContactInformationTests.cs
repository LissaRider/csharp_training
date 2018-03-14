using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactInformationTests : AuthTestBase
    {
        [Test]
        public void TestContactInformation()
        {
            app.Contacts.VerifyContactPresence();            

            ContactData fromTable = app.Contacts.GetContactInformationFromTable(0);
            ContactData fromForm = app.Contacts.GetContactInformationFromEditForm(0);

            //verification

            Assert.AreEqual(fromTable, fromForm);
            Assert.AreEqual(fromTable.Address, fromForm.Address);
            Assert.AreEqual(fromTable.AllPhones, fromForm.AllPhones);
            Assert.AreEqual(fromTable.AllEmails, fromForm.AllEmails);
        }

        [Test]
        public void TestContactInformationDetails()
        {
            app.Contacts.VerifyContactPresence();

            ContactData fromDetailsForm = app.Contacts.GetContactInformationFromDetailsForm(0);
            ContactData fromEditForm = app.Contacts.GetContactInformationFromEditForm(0);

            //verification

            Assert.AreEqual(fromEditForm.AllDetails, fromDetailsForm.AllDetails);
        }
    }
}
