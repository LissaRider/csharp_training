using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestStack.White;
using TestStack.White.UIItems;
using TestStack.White.InputDevices;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.TreeItems;
using TestStack.White.UIItems.WindowItems;
using TestStack.White.WindowsAPI;
using System.Windows.Automation;

namespace addressbook_tests_white
{
    public class GroupHelper : HelperBase
    {
        public static string GROUPWINTITLE = "Group editor";
        public static string DELETEGROUPWINTITLE = "Delete group";

        public GroupHelper(ApplicationManager manager) : base(manager)
        {
        }

        public void Create(GroupData group)
        {
            Window dialogue = OpenGroupsDialogue();
            InitGroupCreation(dialogue);
            TextBox textBox = (TextBox)dialogue.Get(SearchCriteria.ByControlType(ControlType.Edit));
            textBox.Enter(group.Name);
            Keyboard.Instance.PressSpecialKey(KeyboardInput.SpecialKeys.RETURN);

            CloseGroupsDialogue(dialogue);
        } 

        public void Remove(int index)
        {
            Window dialogue = OpenGroupsDialogue();

            Tree tree = dialogue.Get<Tree>("uxAddressTreeView");
            TreeNode root = tree.Nodes[0];
            root.Nodes[index].Select();
            Window submitGroupRemovalDialogue = InitGroupRemoval(dialogue);
            SubmitGroupRemoval(submitGroupRemovalDialogue);

            CloseGroupsDialogue(dialogue);
        }

        // Common methods for groups
        public Window OpenGroupsDialogue()
        {
            manager.MainWindow.Get<Button>("groupButton").Click();
            return manager.MainWindow.ModalWindow(GROUPWINTITLE);
        }

        public void CloseGroupsDialogue(Window dialogue)
        {
            dialogue.Get<Button>("uxCloseAddressButton").Click();
        }

        // Group creation methods
        public void InitGroupCreation(Window dialogue)
        {
            dialogue.Get<Button>("uxNewAddressButton").Click();
        }

        // Group removal methods
        public Window InitGroupRemoval(Window dialogue)
        {
            dialogue.Get<Button>("uxDeleteAddressButton").Click();
            return dialogue.ModalWindow(DELETEGROUPWINTITLE);
        }

        public void SubmitGroupRemoval(Window submitGroupRemovalDialogue)
        {
            submitGroupRemovalDialogue.Get<Button>("uxOKAddressButton").Click();
        }

        // Verification
        public List<GroupData> GetGroupsList()
        {
            List<GroupData> list = new List<GroupData>();
            Window dialogue = OpenGroupsDialogue();
            Tree tree = dialogue.Get<Tree>("uxAddressTreeView");
            TreeNode root = tree.Nodes[0];
            foreach (TreeNode item in root.Nodes)
            {
                list.Add(new GroupData()
                {
                    Name = item.Text
                });
            }
            CloseGroupsDialogue(dialogue);
            return list;
        }

        public int GetGroupCount()
        {
            Window dialogue = OpenGroupsDialogue();
            Tree tree = dialogue.Get<Tree>("uxAddressTreeView");
            TreeNode root = tree.Nodes[0];
            int count = root.Nodes.Count();
            CloseGroupsDialogue(dialogue);
            return count;
        }

        public void VerifyGroupPresence()
        {
            if (GetGroupCount() <= 1)
            {
                GroupData newGroup = new GroupData()
                {
                    Name = "GroupName"
                };
                Create(newGroup);
            }
        }
    }
}