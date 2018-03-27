using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.White;
using TestStack.White.UIItems;
using TestStack.White.UIItems.WindowItems;


namespace addressbook_tests_white
{
    public class ApplicationManager
    {
        public static string WINTITLE = "Free Address Book";

        private GroupHelper groupHelper;
        private ContactHelper contactHelper;

        public ApplicationManager()
        {
            Application app = Application.Launch(@"C:\Program Files (x86)\GAS Softwares\Free Address Book\AddressBook.exe");
            MainWindow = app.GetWindow(WINTITLE);

            groupHelper = new GroupHelper(this);
            contactHelper = new ContactHelper(this);
        }

        public Window MainWindow { get; private set; }

        public void Stop()
        {
            MainWindow.Get<Button>("uxExitAddressButton").Click();
        }

        public GroupHelper Groups
        {
            get
            {
                return groupHelper;
            }
        }

        public ContactHelper Contacts
        {
            get
            {
                return contactHelper;
            }
        }
    }
}