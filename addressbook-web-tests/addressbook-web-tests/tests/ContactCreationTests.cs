using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.IO;
using System.Collections.Generic;
using NUnit.Framework;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Excel = Microsoft.Office.Interop.Excel;

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

        public static IEnumerable<ContactData> ContactDataFromCsvFile()
        {
           List<ContactData> contacts = new List<ContactData>();
           string[] lines = File.ReadAllLines(@"contacts.csv");
           foreach (string l in lines)
           {
               string[] parts = l.Split(',');
               contacts.Add(new ContactData(parts[0], parts[1])
                {      
                    Middlename = parts[2],
                    Nickname = parts[3],
                    Title = parts[4],
                    Company = parts[5],
                    Address = parts[6],
                    Home = parts[7],
                    Mobile = parts[8],
                    Work = parts[9],
                    Fax = parts[10],
                    Email = parts[11],
                    Email2 = parts[12],
                    Email3 = parts[13],
                    Homepage = parts[14],
                    Address2 = parts[15],
                    Phone2 = parts[16],
                    Notes = parts[17]
                });
            }
            return contacts;
        }

        public static IEnumerable<ContactData> ContactDataFromXmlFile()
        {
            return (List<ContactData>)
                new XmlSerializer(typeof(List<ContactData>))
                .Deserialize(new StreamReader(@"contacts.xml"));
        }

        public static IEnumerable<ContactData> ContactDataFromJsonFile()
        {
            return JsonConvert.DeserializeObject<List<ContactData>>(
                File.ReadAllText(@"contacts.json"));
        }

        [Test, TestCaseSource("ContactDataFromJsonFile")]

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