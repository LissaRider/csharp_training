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
        [Test]

        public void ContactCreationTest()
        {
            ContactData contact = new ContactData("Vasilisa", "Smirnova");
            contact.Middlename = "Sergeevna";
            contact.Nickname = "Lissa Rider";
            //contact.Photo = "C:\\Users\vsmirnova\\Desktop\\fb36mdIniM8.jpg";
            contact.Title = "QA Engineer";
            contact.Company = "ABS";
            contact.Address = "Moscow, Lokomotivny Proezd";
            contact.Home = "8 (495) 111-11-11";
            contact.Mobile = "+7(910)4923238";
            contact.Work = "84993333333";
            contact.Fax = "8-499-333-33-34";
            contact.Email = "lissarider@gmail.com";
            contact.Email2 = "lisaniel.lisaniel@gmail.com";
            contact.Email3 = "lisanie@mail.ru";
            contact.Homepage = "https://vk.com/lissarider";
            contact.Bday = "3";
            contact.Bmonth = "January";
            contact.Byear = "1986";
            contact.Aday = "3";
            contact.Amonth = "January";
            contact.Ayear = "2036";
            //contact.New_group = "[none]";
            contact.Address2 = "Moscow, Chertanovskay street";
            contact.Phone2 = "8965444-444-4";
            contact.Notes = "Smth about me";

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