using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactRemovalTests : ContactTestBase
    {
        [Test]

        public void ContactRemovalTest()
        {
            app.Contacts.VerifyContactPresence();

            //List<ContactData> oldContacts = app.Contacts.GetContactsList();
            List<ContactData> oldContacts = ContactData.GetAll();
            ContactData toBeRemoved = oldContacts[0];

            app.Contacts.Remove(toBeRemoved);

            Assert.AreEqual(oldContacts.Count - 1, app.Contacts.GetContactCount());

            //List<ContactData> newContacts = app.Contacts.GetContactsList();
            List<ContactData> newContacts = ContactData.GetAll();

            oldContacts.RemoveAt(0);
            Assert.AreEqual(oldContacts, newContacts);

            foreach (ContactData contact in newContacts)
            {
                Assert.AreNotEqual(contact.Id, toBeRemoved.Id);
            }
        }
    }
}