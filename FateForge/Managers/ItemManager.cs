using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FateForge.DataTypes;

namespace FateForge.Managers
{
    /// <summary>
    /// Manages all items described by the Item Editor.
    /// </summary>
    public static class ItemManager
    {
        public static EventHandler UpdatedList;

        private static List<Item> _items = new List<Item>();
        private static List<string> _comboValues = new List<string>();

        public static int Count { get => Items.Count; }
        private static List<Item> Items { get => _items; }
        private static List<string> ComboValues { get => _comboValues; }

        public static void AddItem(Item _item)
        {
            Items.Add(_item);
            AddComboValue(_item.Name);
            UpdatedList?.Invoke(new object(), new EventArgs());

            _item.FieldsUpdated += (s, o) => 
            {
                MutateComboValue((Item)s, Items.IndexOf((Item)s));
                UpdatedList?.Invoke(new object(), new EventArgs());
            };
        }

        public static void RemoveItem(Item _item)
        {
            RemoveComboValue(Items.IndexOf(_item));
            Items.Remove(_item);
            UpdatedList?.Invoke(new object(), new EventArgs());
        }

        public static void RemoveItem(int index)
        {
            try
            {
                RemoveComboValue(index);
                Items.RemoveAt(index);
                UpdatedList?.Invoke(new object(), new EventArgs());
            }
            catch (IndexOutOfRangeException e)
            {
                throw e;
            }
        }

        private static void AddComboValue(string s)
        {
            //Incase "Epic Sword of Asgard" comes in many forms or something.
            //Example. Perhaps a quest results in a player getting one of two
            //versions of the sword. (Evil/Bad whatever) But they both use the
            //same name. The Editor must differentiate the two, and does so here.
            if (ComboValues.Contains(s))
                AddComboValue(s + "_1");
            else
                ComboValues.Add(s);
        }

        private static void InsertComboValue(int index, string s)
        {
            //Same deal as AddComboValue
            if (ComboValues.Contains(s))
                InsertComboValue(index, s + "_1");
            else
                ComboValues.Insert(index, s);
        }

        private static void RemoveComboValue(int index)
        {
            //Has to be removed by index since _comboValues do not
            //match by the _items actual names.
            ComboValues.RemoveAt(index);
        }

        /// <summary>
        /// This is called when an item is updated. (Name mainly)
        /// </summary>
        /// <param name="item"></param>
        /// <param name="index"></param>
        private static void MutateComboValue(Item item, int index)
        {
            RemoveComboValue(index);
            InsertComboValue(index, item.Name);
        }

        /// <summary>
        /// Returns a reference safe list.
        /// </summary>
        /// <returns></returns>
        public static List<string> GetComboValues()
        {
            return _comboValues.ToList();
        }
    }
}
