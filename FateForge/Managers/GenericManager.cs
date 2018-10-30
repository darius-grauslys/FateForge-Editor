using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FateForge.Managers
{
    public static class GenericManager<T>
    {
        private static string _rootString = "";
        private static int _unnamedCount = 0;
        private static Dictionary<string, T> _labeledIndex = new Dictionary<string, T>();

        public static string RootString => _rootString;
        public static int UnnamedCount { get => _unnamedCount; set => _unnamedCount = value; }
        internal static Dictionary<string, T> LabeledIndex { get => _labeledIndex; set => _labeledIndex = value; }

        /// <summary>
        /// Event Handle for entries being added.
        /// </summary>
        public static event EventHandler LabeledEntriesUpdated;

        /// <summary>
        /// Gets all unique named entiries this manager posseses.
        /// </summary>
        public static List<string> EntryLabels => LabeledIndex.Keys.ToList();

        public static void AddNamelessEntry(T _entry)
        {

        }

        public static void AddNamedEntry(string _name, T _entry)
        {

        }

        public static void RemoveNamelessEntry(T _entry)
        {

        }

        public static void RemoveNamedEntry(string _name)
        {

        }

        private static string GetUniqueName()
        {
            UnnamedCount++;
            return RootString + UnnamedCount;
        }

        private static void AddSafe(T _entry)
        {
            if (LabeledIndex.ContainsKey(_convoNode.Text))
            {
                MessageBox.Show("Entry name is already in use! New name given.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _convoNode.Text = GetUniqueName();
            }
            LabeledIndex.Add(_convoNode.Text, _convoNode);
            LabeledEntriesUpdated?.Invoke(_entry);
        }
    }
}
