using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class ContactTestBase : AuthTestBase
    {
        [TearDown]
        public void ComapareContacUI_DB()
        {
            if (PERFORM_LONG_UI_CHECKS)
            {
                List<ContactData> contactsUI = app.Contacts.GetContactsList();
                List<ContactData> contactsDB = ContactData.GetAll();
                contactsUI.Sort();
                contactsDB.Sort();
                Assert.AreEqual(contactsUI, contactsDB);
            }
        }
    }
}