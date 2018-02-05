using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    public class ContactHelper : HelperBase
    {
        public ContactHelper(ApplicationManager manager) : base(manager)
        {
        }
        
        public ContactHelper Create(ContactData contact)
        {
            manager.Navigator.GoToContactCreationPage();

            FillContactForm(contact);
            SubmitContactCreation();
            ReturnToHomePage();
            return this;
        }
        public ContactHelper Modify(int v1, int v2, ContactData newData)
        {
            manager.Navigator.GoToHomePage();

            InitContactModification(v1);
            FillContactForm(newData);
            SubmitContactModification(v2);
            ReturnToHomePage();
            return this;
        }



        public ContactHelper Remove(int v)
        {
            manager.Navigator.GoToHomePage();

            SelectContact(v);
            InitContactRemoval();
            SubmitContactRemoval();

            manager.Navigator.GoToHomePage();
            return this;
        }

        //Contact creation methods
        public ContactHelper FillContactForm(ContactData contact)
        {
            // Contact card
            Type(By.Name("firstname"), contact.Firstname);
            Type(By.Name("middlename"), contact.Middlename);
            Type(By.Name("lastname"), contact.Lastname);
            Type(By.Name("nickname"), contact.Nickname);

            // Photo
            //Type(By.Name("photo"), contact.Photo);
            //driver.FindElement(By.Name("delete_photo")).Click();

            // Job
            Type(By.Name("title"), contact.Title);
            Type(By.Name("company"), contact.Company);

            // Address
            Type(By.Name("address"), contact.Address);

            // Telephone
            Type(By.Name("home"), contact.Home);
            Type(By.Name("mobile"), contact.Mobile);
            Type(By.Name("work"), contact.Work);
            Type(By.Name("fax"), contact.Fax);

            // Email
            Type(By.Name("email"), contact.Email);
            Type(By.Name("email2"), contact.Email2);
            Type(By.Name("email3"), contact.Email3);

            // Internet
            Type(By.Name("homepage"), contact.Homepage);
            
            // Birthday
            new SelectElement(driver.FindElement(By.Name("bday"))).SelectByText(contact.Bday);
            new SelectElement(driver.FindElement(By.Name("bmonth"))).SelectByText(contact.Bmonth);
            Type(By.Name("byear"), contact.Byear);
            
            // Anniversary
            new SelectElement(driver.FindElement(By.Name("aday"))).SelectByText(contact.Aday);
            new SelectElement(driver.FindElement(By.Name("amonth"))).SelectByText(contact.Amonth);
            Type(By.Name("ayear"), contact.Ayear);

            // Contact group
            //new SelectElement(driver.FindElement(By.Name("new_group"))).SelectByText(contact.New_group);

            // Secondary
            Type(By.Name("address2"), contact.Address2);
            Type(By.Name("phone2"), contact.Phone2);
            Type(By.Name("notes"), contact.Notes);
            return this;
        }

        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
            return this;
        }


        //Contact modification methods
        public ContactHelper InitContactModification(int index1)
        {
            driver.FindElement(By.XPath("(//a[contains(@href,'edit.php?id')])[" + index1 + "]")).Click();
            return this;
        }

        public ContactHelper SubmitContactModification(int index2)
        {
            driver.FindElement(By.XPath("(//input[@name='update'])[" + index2 + "]")).Click();
            return this;
        }

        // Contact removal methods
        public ContactHelper SelectContact(int index)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + index + "]")).Click();
            return this;
        }

        public ContactHelper InitContactRemoval()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            return this;
        }

        public ContactHelper SubmitContactRemoval()
        {
            driver.SwitchTo().Alert().Accept();
            return this;
        }

        //Common
        public ContactHelper ReturnToHomePage()
        {
            driver.FindElement(By.LinkText("home page")).Click();
            return this;
        }

        // Verification
        public void VerifyContactPresence()
        {
            if (IsElementPresent(By.Name("entry")))
            {
                return;
            }

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

            Create(contact);
        }
    }
}