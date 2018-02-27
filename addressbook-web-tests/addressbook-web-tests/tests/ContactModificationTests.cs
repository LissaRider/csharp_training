using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : AuthTestBase
    {
        [Test]

        public void ContactModificationTest()
        {
            ContactData newData = new ContactData("V", "S");
            newData.Middlename = "S";
            newData.Nickname = "LissaRider";
            //NewData.Photo = "C:\\Users\vsmirnova\\Desktop\\fb36mdIniM8.jpg";
            newData.Title = "Tester";
            newData.Company = "ANBR";
            newData.Address = "Moscow, Lokomotivny Proezd, 201";
            newData.Home = "8(495)1111111";
            newData.Mobile = "+7(910) 492-32-38";
            newData.Work = "+74993333333";
            newData.Fax = "8-499-3333334";
            newData.Email = "lissarider1@gmail.com";
            newData.Email2 = "lisaniel.lisaniel1@gmail.com";
            newData.Email3 = "lisaniel1@mail.ru";
            newData.Homepage = "https://vk.com/lissa_rider";
            newData.Bday = "2";
            newData.Bmonth = "February";
            newData.Byear = "1984";
            newData.Aday = "2";
            newData.Amonth = "February";
            newData.Ayear = "2034";
            //newData.New_group = "[none]";
            newData.Address2 = "Moscow, Chertanovskay street, 33";
            newData.Phone2 = "8(965)444-444-4";
            newData.Notes = "Notes";

            app.Contacts.VerifyContactPresence();

            List<ContactData> oldContacts = app.Contacts.GetContactsList();
            ContactData oldData = oldContacts[0];

            app.Contacts.Modify(0, newData);

            Assert.AreEqual(oldContacts.Count, app.Contacts.GetContactCount());

            List<ContactData> newContacts = app.Contacts.GetContactsList();
            oldContacts[0].Firstname = newData.Firstname;
            oldContacts[0].Lastname = newData.Lastname;
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
            foreach (ContactData contact in newContacts)
            {
                if (contact.Id == oldData.Id)
                {
                    Assert.AreEqual(newData.Firstname + newData.Lastname, contact.Firstname + contact.Lastname);
                }
            }
        }
    }
}
