using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.White;
using TestStack.White.UIItems;
using TestStack.White.InputDevices;
using TestStack.White.WindowsAPI;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.TreeItems;
using TestStack.White.UIItems.TableItems;
using TestStack.White.UIItems.WindowItems;
using System.Windows.Automation;

namespace addressbook_tests_white
{
    public class ContactHelper : HelperBase
    {
        public static string CONTACTWINTITLE = "Contact Editor";

        public ContactHelper(ApplicationManager manager) : base(manager)
        {
        }

        public void Create(ContactData contactData)
        {
            Window dialogue = OpenContactsDialogue();

            TextBox firstnameField = dialogue.Get<TextBox>("ueFirstNameAddressTextBox");
            firstnameField.Enter(contactData.Firstname);
            TextBox lastnameField = dialogue.Get<TextBox>("ueLastNameAddressTextBox");
            lastnameField.Enter(contactData.Lastname);

            CloseContactsDialogue(dialogue);
        }

        public void Remove(int index)
        {
            Window mainWindow = manager.MainWindow;

            Table table = mainWindow.Get<Table>("uxAddressGrid");
            TableRows tableRows = table.Rows;
            tableRows[0].Select();
            mainWindow.Get<Button>("uxDeleteAddressButton").Click();
            Window questionWindow = mainWindow.ModalWindow("Question");
            questionWindow.Get<Button>(SearchCriteria.ByText("Yes")).Click();
        }

        public Window OpenContactsDialogue()
        {
            manager.MainWindow.Get<Button>("uxNewAddressButton").Click();
            return manager.MainWindow.ModalWindow(CONTACTWINTITLE);
        }

        public void CloseContactsDialogue(Window dialogue)
        {
            dialogue.Get<Button>("uxSaveAddressButton").Click();
        }

        //verification
        public List<ContactData> GetContactList()
        {
            List<ContactData> list = new List<ContactData>();
            Window mainWindow = manager.MainWindow;
            Table table = mainWindow.Get<Table>("uxAddressGrid");
            TableRows tableRows = table.Rows;

            foreach (TableRow item in tableRows)
            {
                list.Add(new ContactData()
                {
                    Firstname = item.Cells[0].Value as string,
                    Lastname = item.Cells[1].Value as string
                });
            }
            return list;
        }

        public int GetContactCount()
        {
            Window mainWindow = manager.MainWindow;

            Table table = mainWindow.Get<Table>("uxAddressGrid");
            TableRows tableRows = table.Rows;
            int count = tableRows.Count();
            return count;
        }

        public void VerifyContactPresence()
        {
            List<ContactData> contacts = GetContactList();
            if (contacts.Count == 0)
            {
                ContactData newContact = new ContactData()
                {
                    Firstname = "Lissa",
                    Lastname = "Rider"
                };
                Create(newContact);
            }
        }
    }
}