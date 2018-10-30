using FateForge.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FateForge.Managers
{
    public static class NPCManager
    {
        public delegate void NpcAddedEvent(NPC _npc);

        private static int _unnamedCount = 0;
        private static readonly string _rootString = "npc_";
        private static Dictionary<string, NPC> _npcs = new Dictionary<string, NPC>();
        private static Dictionary<string, int> _recurringNames = new Dictionary<string, int>();

        private static Dictionary<string, NPC> Npcs { get => _npcs; set => _npcs = value; }
        public static List<string> NpcNames => Npcs.Keys.ToList();
        public static List<NPC> NpcList => Npcs.Select((k) => k.Value).ToList();
        private static Dictionary<string, int> RecurringNames { get => _recurringNames; set => _recurringNames = value; }

        public static int UnnamedCount { get => _unnamedCount; private set => _unnamedCount = value; }

        public static string RootString => _rootString;
        

        public static event NpcAddedEvent NpcAdded;

        public static void AddNPC(NPC _npc)
        {
            if (_npc.Name == "")
                _npc.Name = GetUniqueName();
            AddConsiderRecurring(_npc);
            NpcAdded(_npc);
        }
        
        public static void RemoveObjective(NPC _npc)
        {
            if (Npcs.ContainsKey(_npc.Name))
                Npcs.Remove(_npc.Name);
            else
                return;
        }

        public static string GetUniqueName()
        {
            UnnamedCount++;
            return RootString + UnnamedCount;
        }

        public static NPC GetObjective(string name)
        {
            return ((Npcs.ContainsKey(name)) ? Npcs[name] : null);
        }

        public static void Rename(NPC _npc, string newName)
        {
            if (newName.Length >= RootString.Length)
            {
                if (newName.Substring(0, RootString.Length-1) == RootString)
                    throw new ArgumentException();
            }
            if (Npcs.ContainsKey(_npc.Name))
                Npcs[_npc.Name].Name = newName;
            else
            {
                _npc.Name = newName;
                AddConsiderRecurring(_npc);
            }
        }

        private static void AddConsiderRecurring(NPC _npc)
        {
            if (Npcs.ContainsKey(_npc.Name))
            {
                if (RecurringNames.ContainsKey(_npc.Name))
                    RecurringNames[_npc.Name]++;
                else
                    RecurringNames.Add(_npc.Name, 1);
                _npc.Name += RecurringNames[_npc.Name];
            }
            Npcs.Add(_npc.Name, _npc);
        }
    }
}
