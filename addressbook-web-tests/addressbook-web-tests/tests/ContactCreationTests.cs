using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : AuthTestBase
    {
        public static IEnumerable<ContactData> RandomContactDataProvider()
        {
            List<ContactData> contacts = new List<ContactData>();

            for (int i = 0; i < 5; i++)
            {
                contacts.Add(new ContactData(GenerateRandomString(35), GenerateRandomString(35))
                {
                    Middlename = GenerateRandomString(15),
                    Nickname = GenerateRandomString(35),
                    Title = GenerateRandomString(35),
                    Company = GenerateRandomString(35),
                    Address = GenerateRandomString(175),
                    Home = GenerateRandomString(35),
                    Mobile = GenerateRandomString(35),
                    Work = GenerateRandomString(35),
                    Fax = GenerateRandomString(35),
                    Email = GenerateRandomString(35),
                    Email2 = GenerateRandomString(35),
                    Email3 = GenerateRandomString(35),
                    Homepage = GenerateRandomString(35),
                    Address2 = GenerateRandomString(175),
                    Phone2 = GenerateRandomString(35),
                    Notes = GenerateRandomString(175)
                });
            }
            return contacts;
        }

        [Test, TestCaseSource("RandomContactDataProvider")]

        public void ContactCreationTest(ContactData contact)
        {
            List<ContactData> oldContacts = app.Contacts.GetContactsList();

            app.Contacts.Create(contact);

            Assert.AreEqual(oldContacts.Count + 1, app.Contacts.GetContactCount());

            List<ContactData> newContacts = app.Contacts.GetContactsList();
            oldContacts.Add(contact);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}