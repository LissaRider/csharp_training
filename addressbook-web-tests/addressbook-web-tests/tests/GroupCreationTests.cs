﻿using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.IO;
using System.Collections.Generic;
using NUnit.Framework;
using System.Xml;
using System.Linq;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Excel = Microsoft.Office.Interop.Excel;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupCreationTests : ContactTestBase
    {
        public static IEnumerable<GroupData> RandomGroupDataProvider()
        {
            List<GroupData> groups = new List<GroupData>();

            for (int i = 0; i < 5; i++)
            {
                groups.Add(new GroupData(GenerateRandomString(35))
                {
                    Header = GenerateRandomString(400),
                    Footer = GenerateRandomString(400)
                });
            }
            return groups;
        }

        public static IEnumerable<GroupData> GroupDataFromCsvFile()
        {
            List<GroupData> groups = new List<GroupData>();
            string[] lines = File.ReadAllLines(@"groups.csv");
            foreach (string l in lines)
            {
                string[] parts = l.Split(',');
                groups.Add(new GroupData(parts[0])
                {
                    Header = parts[1],
                    Footer = parts[2]
                });
            }
            return groups;
        }

        public static IEnumerable<GroupData> GroupDataFromXmlFile()
        {
            return (List<GroupData>)
                new XmlSerializer(typeof(List<GroupData>))
                .Deserialize(new StreamReader(@"groups.xml"));
        }

        public static IEnumerable<GroupData> GroupDataFromJsonFile()
        {
            return JsonConvert.DeserializeObject<List<GroupData>>(
                File.ReadAllText(@"groups.json"));
        }

        public static IEnumerable<GroupData> GroupDataFromExcelFile()
        {
            List<GroupData> groups = new List<GroupData>();
            Excel.Application app = new Excel.Application();
            Excel.Workbook wbook = app.Workbooks.Open(Path.Combine(Directory.GetCurrentDirectory(), @"groups.xlsx"));
            Excel.Worksheet wsheet = wbook.Sheets[1];
            Excel.Range range = wsheet.UsedRange;
            for (int i = 1; i <= range.Rows.Count; i++)
            {
                groups.Add(new GroupData()
                {
                    Name = range.Cells[i, 1].Value,
                    Header = range.Cells[i, 2].Value,
                    Footer = range.Cells[i, 3].Value,
                });
            }
            wbook.Close();
            app.Visible = false;
            app.Quit();
            return groups;
        }

        [Test, TestCaseSource("GroupDataFromExcelFile")]

        public void GroupCreationTest(GroupData group)
        {           
            //List<GroupData> oldGroups = app.Groups.GetGroupsList();
            List<GroupData> oldGroups = GroupData.GetAll();

            app.Groups.Create(group);

            Assert.AreEqual(oldGroups.Count + 1, app.Groups.GetGroupCount());

            //List<GroupData> newGroups = app.Groups.GetGroupsList();
            List<GroupData> newGroups = GroupData.GetAll();
            oldGroups.Add(group);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }

        [Test]
        public void BadNameGroupCreationTest()
        {
            GroupData group = new GroupData("Lissa's Group");
            group.Header = "";
            group.Footer = "";

            //List<GroupData> oldGroups = app.Groups.GetGroupsList();
            List<GroupData> oldGroups = GroupData.GetAll();

            app.Groups.Create(group);

            Assert.AreEqual(oldGroups.Count + 1, app.Groups.GetGroupCount());

            //List<GroupData> newGroups = app.Groups.GetGroupsList();
            List<GroupData> newGroups = GroupData.GetAll();
            oldGroups.Add(group);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }

        [Test]
        public void TestDBConnectivity_G0()
        {
            DateTime start = DateTime.Now;
            List<GroupData> fromUi = app.Groups.GetGroupsList();
            DateTime end = DateTime.Now;
            System.Console.Out.WriteLine(end.Subtract(start));

            start = DateTime.Now;
            List<GroupData> fromDb = GroupData.GetAll();
            end = DateTime.Now;
            System.Console.Out.WriteLine(end.Subtract(start));
        }

        [Test]
        public void TestDBConnectivity_G1()
        {
            foreach(ContactData contact in GroupData.GetAll()[0].GetContacts());
            {
                System.Console.Out.WriteLine();
            }
        }

        [Test]
        public void TestDBConnectivity_G2()
        {
            foreach (ContactData contact in ContactData.GetAll())
            {
                System.Console.Out.WriteLine(contact.Deprecated);
            }
        }
    }
}