using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FateForge.DataTypes;

namespace FateForge.Managers
{
    public static class ReferenceTable
    {
        private static List<IReferenceTableEntry> _referenceItems = new List<IReferenceTableEntry>();
        private static List<ObjectiveField> _referencedObjectives = new List<ObjectiveField>();
        private static List<EventField> _referencedEvents = new List<EventField>();
        private static List<ConditionField> _referencedConditions = new List<ConditionField>();
        private static List<TreeNode> _referencedConvos = new List<TreeNode>();
        private static List<Item> _referencedItems = new List<Item>();
        //Mob
        //Npc
        //Location
        //Compass

        private static List<IReferenceTableEntry> ReferenceEntries { get => _referenceItems; set => _referenceItems = value; }
        private static List<ObjectiveField> ReferencedObjectives { get => _referencedObjectives; set => _referencedObjectives = value; }
        private static List<EventField> ReferencedEvents { get => _referencedEvents; set => _referencedEvents = value; }
        private static List<ConditionField> ReferencedConditions { get => _referencedConditions; set => _referencedConditions = value; }
        private static List<TreeNode> ReferencedConvos { get => _referencedConvos; set => _referencedConvos = value; }
        private static List<Item> ReferencedItems { get => _referencedItems; set => _referencedItems = value; }

        public static List<ObjectiveField> ReferencedObjectiveList => ReferencedObjectives.ToList();
        public static List<EventField> ReferencedEventList => ReferencedEvents.ToList();
        public static List<ConditionField> ReferencedConditionList => ReferencedConditions.ToList();
        public static List<TreeNode> ReferencedConvoList => ReferencedConvos.ToList();
        public static List<Item> ReferenedItemList => ReferencedItems.ToList();
        

        public static void ClearReferenceTable()
        {
            ReferencedObjectives.Clear();
            ReferencedEvents.Clear();
            ReferencedConditions.Clear();
        }

        #region Single Add
        public static void AddReference(IReferenceTableEntry _ref)
        {
            if (_ref == null)
                return;
            if (ReferenceEntries.Contains(_ref))
                return;
            ReferenceEntries.Add(_ref);
            if (_ref is ObjectiveField)
                AddReference(_ref as ObjectiveField);
            if (_ref is EventField)
                AddReference(_ref as EventField);
            if (_ref is ConditionField)
                AddReference(_ref as ConditionField);
            if (_ref is TreeNode)
                AddReference(_ref as TreeNode);
            if (_ref is Item)
                AddReference(_ref as Item);
        }

        private static void AddReference(ObjectiveField _objRef)
        {
            ReferencedObjectives.Add(_objRef);
        }

        private static void AddReference(EventField _eventRef)
        {
            ReferencedEvents.Add(_eventRef);
        }

        private static void AddReference(ConditionField _condRef)
        {
            ReferencedConditions.Add(_condRef);
        }

        private static void AddReference(TreeNode _convoRef)
        {
            ReferencedConvos.Add(_convoRef);
        }

        private static void AddReference(Item _itemRef)
        {
            ReferencedItems.Add(_itemRef);
        }
        #endregion

        #region Range Add

        public static void AddRangeOfReferences(List<IReferenceTableEntry> _refs)
        {
            foreach (IReferenceTableEntry _ref in _refs)
                AddReference(_ref);
        }

        #endregion
    }
}
