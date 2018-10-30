using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FateForge.Managers
{
    public static class ObjectiveManager
    {
        private static readonly string _rootString = "obj_";
        private static Dictionary<string, ObjectiveField> _objectiveFields = new Dictionary<string, ObjectiveField>();
        private static int _unnamedCount = 0;
        
        public static int UnnamedCount { get => _unnamedCount; private set => _unnamedCount = value; }
        private static Dictionary<string, ObjectiveField> ObjectiveFields { get => _objectiveFields; set => _objectiveFields = value; }
        public static List<string> ObjectiveNames { get => ObjectiveFields.Keys.ToList(); }

        private static string RootString => _rootString;

        public static void AddObjective(ObjectiveField _obj)
        {
            _obj.ObjectiveName = GetUniqueName();
            _obj.OldName = _obj.ObjectiveName;
            AddSafe(_obj);
        }

        public static void RemoveObjective(ObjectiveField _obj)
        {
            if (ObjectiveFields.ContainsKey(_obj.ObjectiveName))
                ObjectiveFields.Remove(_obj.ObjectiveName);
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

        public static ObjectiveField GetObjective(string name)
        {
            return ((ObjectiveFields.ContainsKey(name)) ? ObjectiveFields[name] : null);
        }

        public static void Rename(ObjectiveField _obj)
        {
            if (_obj.ObjectiveName.Length >= RootString.Length)
            {
                if (_obj.ObjectiveName.Substring(0, RootString.Length) == RootString)
                    throw new ArgumentException();
            }
            if (ObjectiveFields.ContainsKey(_obj.OldName) && _obj.OldName.Length >= RootString.Length)
                if (_obj.OldName.Substring(0, RootString.Length) == RootString)
                    UnnamedCount--;
            ObjectiveFields.Remove(_obj.OldName);
            AddSafe(_obj);
        }

        private static void AddSafe(ObjectiveField _obj)
        {
            if (ObjectiveFields.ContainsKey(_obj.ObjectiveName))
            {
                MessageBox.Show("Objective name is already in use! New name given.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _obj.ObjectiveName = GetUniqueName();
            }
            ObjectiveFields.Add(_obj.ObjectiveName, _obj);
        }
    }
}
