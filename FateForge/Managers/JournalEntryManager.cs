using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FateForge.Managers
{
    public static class JournalEntryManager
    {
        private static Dictionary<string, TreeNode> _journalChapterEntries = new Dictionary<string, TreeNode>();
        private static readonly string _rootString = "chapter_";

        private static Dictionary<string, TreeNode> JournalChapterEntries { get => _journalChapterEntries; set => _journalChapterEntries = value; }
        public static List<string> GetJournalChapterNames => JournalChapterEntries.Keys.ToList();
        
        private static string RootString => _rootString;

        public static void AddObjective(ObjectiveField _obj)
        {
            _obj.ObjectiveName = GetUniqueName();
            _obj.OldName = _obj.ObjectiveName;
            AddSafe(_obj);
        }

        public static void RemoveObjective(ObjectiveField _obj)
        {
            if (JournalChapterEntries.ContainsKey(_obj.ObjectiveName))
                JournalChapterEntries.Remove(_obj.ObjectiveName);
            else
                return;
            if (_obj.ObjectiveName.Length >= 4 && _obj.ObjectiveName.Substring(0, 4) == "obj_")
                UnnamedCount--;
        }

        public static string GetUniqueName()
        {
            UnnamedCount++;
            return "obj_" + UnnamedCount;
        }

        public static TreeNode GetObjective(string name)
        {
            return ((JournalChapterEntries.ContainsKey(name)) ? JournalChapterEntries[name] : null);
        }

        public static void Rename(ObjectiveField _obj)
        {
            if (_obj.ObjectiveName.Length >= RootString.Length)
            {
                if (_obj.ObjectiveName.Substring(0, RootString.Length) == RootString)
                    throw new ArgumentException();
            }
            if (JournalChapterEntries.ContainsKey(_obj.OldName) && _obj.OldName.Length >= RootString.Length)
                if (_obj.OldName.Substring(0, RootString.Length) == RootString)
                    UnnamedCount--;
            JournalChapterEntries.Remove(_obj.OldName);
            AddSafe(_obj);
        }

        private static void AddSafe(ObjectiveField _obj)
        {
            if (JournalChapterEntries.ContainsKey(_obj.ObjectiveName))
            {
                MessageBox.Show("Objective name is already in use! New name given.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _obj.ObjectiveName = GetUniqueName();
            }
            JournalChapterEntries.Add(_obj.ObjectiveName, _obj);
        }
    }
}
