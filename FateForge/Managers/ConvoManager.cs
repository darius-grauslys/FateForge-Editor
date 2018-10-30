using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FateForge.Managers
{
    public static class ConvoManager : GenericManager
    {
        public delegate void ConvoNodeEvent(TreeNode _convo);

        private static int _unnamedCount;
        private static readonly string _nameRoot = "newConvoNode_";
        private static Dictionary<string, TreeNode> _rootConvoNodes = new Dictionary<string, TreeNode>();
        private static Dictionary<string, TreeNode> RootConvoNodes { get => _rootConvoNodes; set => _rootConvoNodes = value; }
        public static List<string> RootConvoNodeNames { get => RootConvoNodes.Keys.ToList(); }
        public static int UnnamedCount { get => _unnamedCount; private set => _unnamedCount = value; }

        private static string NameRoot => _nameRoot;

        public static event ConvoNodeEvent ConvoListUpdated;

        public static void AddConvoNode(TreeNode _convoNode)
        {
            _convoNode.Text = GetUniqueName();
            AddSafe(_convoNode);
            ConvoListUpdated?.Invoke(_convoNode);
            
        }

        public static void RemoveConvoNode(TreeNode _convoNode)
        {
            if (RootConvoNodes.ContainsKey(_convoNode.Text))
                RootConvoNodes.Remove(_convoNode.Text);
            else
                return;
            if (_convoNode.Text.Length >= 4 && _convoNode.Text.Substring(0, NameRoot.Length) == NameRoot)
                UnnamedCount--;
            ConvoListUpdated?.Invoke(_convoNode);
        }

        public static void RenameConvoNode(TreeNode _convoNode, string newName)
        {
            if (newName.Length >= NameRoot.Length)
            {
                if (newName.Substring(0, NameRoot.Length) == NameRoot)
                    throw new ArgumentException();
            }
            if (RootConvoNodes.ContainsKey(_convoNode.Text) && _convoNode.Text.Length >= NameRoot.Length)
                if(_convoNode.Text.Substring(0, NameRoot.Length) == NameRoot)
                    UnnamedCount--;
            RootConvoNodes.Remove(_convoNode.Text);
            _convoNode.Text = newName;
            AddSafe(_convoNode);
            ConvoListUpdated?.Invoke(_convoNode);
        }

        private static string GetUniqueName()
        {
            UnnamedCount++;
            return NameRoot + UnnamedCount;
        }

        private static void AddSafe(TreeNode _convoNode)
        {
            if (RootConvoNodes.ContainsKey(_convoNode.Text))
            {
                MessageBox.Show("Conversation name is already in use! New name given.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _convoNode.Text = GetUniqueName();
            }
            RootConvoNodes.Add(_convoNode.Text, _convoNode);
            ConvoListUpdated?.Invoke(_convoNode);
        }
    }
}
