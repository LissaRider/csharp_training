using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : TestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            ContactData NewData = new ContactData("V", "S");
            NewData.Middlename = "S";
            NewData.Nickname = "LissaRider";
            //NewData.Photo = "C:\\Users\vsmirnova\\Desktop\\fb36mdIniM8.jpg";
            NewData.Title = "Tester";
            NewData.Company = "ANBR";
            NewData.Address = "Moscow, Lokomotivny Proezd, 201";
            NewData.Home = "8(495)1111111";
            NewData.Mobile = "+7(910) 492-32-38";
            NewData.Work = "+74993333333";
            NewData.Fax = "8-499-3333334";
            NewData.Email = "lissarider1@gmail.com";
            NewData.Email2 = "lisaniel.lisaniel1@gmail.com";
            NewData.Email3 = "lisaniel1@mail.ru";
            NewData.Homepage = "https://vk.com/lissa_rider";
            NewData.Bday = "2";
            NewData.Bmonth = "February";
            NewData.Byear = "1984";
            NewData.Aday = "2";
            NewData.Amonth = "February";
            NewData.Ayear = "2034";
            //NewData.New_group = "[none]";
            NewData.Address2 = "Moscow, Chertanovskay street, 33";
            NewData.Phone2 = "8(965)444-444-4";
            NewData.Notes = "Notes";

            app.Contacts.Modify(1, 1, NewData);
        }
    }
}
