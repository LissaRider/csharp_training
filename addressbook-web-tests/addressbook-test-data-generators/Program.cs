using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAddressbookTests;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Excel = Microsoft.Office.Interop.Excel;

namespace addressbook_test_data_generators
{
    class Program
    {
        static void Main(string[] args)
        {
            string dataType = args[0];
            int count = Convert.ToInt32(args[1]);
            StreamWriter writer = new StreamWriter(args[2]);
            //string filename = args[1];
            string format = args[3];

            //if (format == "excel")
            //{
            //    writeGroupsToExcelFile(groups, filename);
            //}
            //else

            if (dataType == "group")
            {
                List<GroupData> groups = GenerateGroupDataTemplate(count);
            //    StreamWriter writer = new StreamWriter(filename);
                if (format == "csv")
                {
                    writeGroupsToCsvFile(groups, writer);
                }
                else if (format == "xml")
                {
                    writeGroupsToXmlFile(groups, writer);
                }
                else if (format == "json")
                {
                    writeGroupsToJsonFile(groups, writer);
                }
                else
                {
                    Console.Out.Write("WARNING! Unrecognized file format: " + format + ".");
                }
            }
            else if (dataType == "contact")
            {
                List<ContactData> contacts = GenerateContactDataTemplate(count);

                if (format == "csv")
                {
                    writeContactsToCsvFile(contacts, writer);
                }
                else if (format == "xml")
                {
                    writeContactsToXmlFile(contacts, writer);
                }
                else if (format == "json")
                {
                    writeContactsToJsonFile(contacts, writer);
                }
                else
                {
                    Console.Out.Write("WARNING! Unrecognized file format: " + format + ".");
                }
            }
            else
            {
                Console.Out.Write("WARNING! Unrecognized data type: " + dataType + ".");
            }
            writer.Close();
        }

        // Group data template creation
        public static List<GroupData> GenerateGroupDataTemplate(int count)
        {
            List<GroupData> groups = new List<GroupData>();
            for (int i = 0; i < count; i++)
            {
                groups.Add(new GroupData(TestBase.GenerateRandomString(35))
                {
                    Header = TestBase.GenerateRandomString(400),
                    Footer = TestBase.GenerateRandomString(400)
                });
            }
            return groups;
        }

        // Contact data template creation
        public static List<ContactData> GenerateContactDataTemplate(int count)
        {
            List<ContactData> contacts = new List<ContactData>();
            for (int i = 0; i < count; i++)
            {
                contacts.Add(new ContactData(TestBase.GenerateRandomString(35), TestBase.GenerateRandomString(35))
                {
                    Middlename = TestBase.GenerateRandomString(15),
                    Nickname = TestBase.GenerateRandomString(35),
                    Title = TestBase.GenerateRandomString(35),
                    Company = TestBase.GenerateRandomString(35),
                    Address = TestBase.GenerateRandomString(175),
                    Home = TestBase.GenerateRandomString(35),
                    Mobile = TestBase.GenerateRandomString(35),
                    Work = TestBase.GenerateRandomString(35),
                    Fax = TestBase.GenerateRandomString(35),
                    Email = TestBase.GenerateRandomString(35),
                    Email2 = TestBase.GenerateRandomString(35),
                    Email3 = TestBase.GenerateRandomString(35),
                    Homepage = TestBase.GenerateRandomString(35),
                    Address2 = TestBase.GenerateRandomString(175),
                    Phone2 = TestBase.GenerateRandomString(35),
                    Notes = TestBase.GenerateRandomString(175)
                });
            }
            return contacts;
        }

        //Csv file for groups (addressbook-test-data-generators.exe group 5 groups.csv csv)
        static void writeGroupsToCsvFile(List<GroupData> groups, StreamWriter writer)
        {
            foreach (GroupData group in groups)
            {
                writer.WriteLine(String.Format("${0},${1},${2}",
                    group.Name, group.Header, group.Footer));
            }
        }

        //Csv file for contacts (addressbook-test-data-generators.exe contact 5 contacts.csv csv)
        static void writeContactsToCsvFile(List<ContactData> contacts, StreamWriter writer)
        {
            foreach (ContactData contact in contacts)
            {
                writer.WriteLine(String.Format("${0},${1},${2},${3},${4},${5},${6},${7},${8},${9},${10},${11},${12},${13},${14},${15},${16},${17}",
                contact.Firstname, contact.Lastname,
                contact.Middlename, contact.Nickname,
                contact.Title, contact.Company,
                contact.Address, contact.Home,
                contact.Mobile, contact.Work,
                contact.Fax, contact.Email,
                contact.Email2, contact.Email3,
                contact.Homepage, contact.Address2,
                contact.Phone2, contact.Notes));
            }
        }

        // Xml file for groups (addressbook-test-data-generators.exe group 5 groups.xml xml)
        static void writeGroupsToXmlFile(List<GroupData> groups, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<GroupData>)).Serialize(writer, groups);
        }

        // Xml file for contacts (addressbook-test-data-generators.exe contact 5 contacts.xml xml)
        static void writeContactsToXmlFile(List<ContactData> contacts, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<ContactData>)).Serialize(writer, contacts);
        }

        // Json file for groups (addressbook-test-data-generators.exe group 5 groups.json json)
        static void writeGroupsToJsonFile(List<GroupData> groups, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(groups, Newtonsoft.Json.Formatting.Indented));
        }

        // Json file for contacts (addressbook-test-data-generators.exe contact 5 contacts.json json)
        static void writeContactsToJsonFile(List<ContactData> contacts, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(contacts, Newtonsoft.Json.Formatting.Indented));
        }

        // EXCEL file for groups (addressbook-test-data-generators.exe 5 groups.xlsx excel)
        //static void writeGroupsToExcelFile(List<GroupData> groups, string filename)
        //{
        //    Excel.Application app = new Excel.Application();
        //    //app.Visible = true;
        //    Excel.Workbook wb = app.Workbooks.Add();
        //    Excel.Worksheet sheet = wb.ActiveSheet;
              //sheet.Cells[1, 1] = "test";
        //    int row = 1;
        //    foreach (GroupData group in groups)
        //    {
        //        sheet.Cells[row, 1] = group.Name;
        //       sheet.Cells[row, 2] = group.Header;
        //        sheet.Cells[row, 3] = group.Footer;
        //        row++;
        //    }
        //    string fullPath = Path.Combine(Directory.GetCurrentDirectory(), filename);
        //    File.Delete(fullPath);
        //    wb.SaveAs(fullPath);
        //    wb.Close();
        //    //app.Visible = false;
        //}
    }
}