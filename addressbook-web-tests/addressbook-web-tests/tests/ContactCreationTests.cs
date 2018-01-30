﻿using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : TestBase
    {
        [Test]
        public void ContactCreationTest()
        {
            OpenHomePage();
            Login(new AccountData("admin", "secret"));
            InitNewContactCreation();
            ContactData contact = new ContactData("Vasilisa", "Smirnova");
            contact.Middlename = "Sergeevna";
            contact.Nickname = "Lissa Rider";
            contact.Photo = "C:\\Users\vsmirnova\\Desktop\\fb36mdIniM8.jpg";
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
            contact.New_group = "[none]";
            contact.Address2 = "Moscow, Chertanovskay street";
            contact.Phone2 = "8965444-444-4";
            contact.Notes = "Smth about me";
            FillContactForm(contact);
            SubmitContactCreation();
            GoToHomePage();
            Logout();
        }
    }
}