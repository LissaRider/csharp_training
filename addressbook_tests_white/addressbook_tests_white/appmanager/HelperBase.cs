using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestStack.White;
using TestStack.White.UIItems;
using TestStack.White.UIItems.WindowItems;

namespace addressbook_tests_white
{
    public class HelperBase
    {
        protected ApplicationManager manager;

        public HelperBase(ApplicationManager manager)
        {
            this.manager = manager;
        }
    }
}