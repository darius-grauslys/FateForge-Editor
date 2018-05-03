using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FateForge.Managers;

namespace FateForge.DataTypes
{
    public class Item
    {
        public EventHandler FieldsUpdated;

        private string _type = "air";
        private int _id = 0;
        private int _data = 0;
        private string _name = "";
        private List<string> _lore = new List<string>();
        private Dictionary<string, string> _enchants = new Dictionary<string, string>();

        public string Type
        {
            get => _type; set
            {
                _type = value;
                FieldsUpdated(this, new EventArgs());
            }
        }
        public int Id { get => _id; set
            {
                _id = value;
                FieldsUpdated(this, new EventArgs());
            } }
        public int Data { get => _data; set
            {
                _data = value;
                FieldsUpdated(this, new EventArgs());
            }
        }
        public string Name { get => _name; set
            {
                _name = value;
                FieldsUpdated(this, new EventArgs());
            }
        }
        public List<string> Lore { get => _lore; set
            {
                _lore = value;
                FieldsUpdated(this, new EventArgs());
            }
        }
        public Dictionary<string, string> Enchants { get => _enchants; set
            {
                _enchants = value;
                FieldsUpdated(this, new EventArgs());
            }
        }

        public Item(int _id=0, int _data=0, string _name="")
        {
            this._id = _id;
            this._data = _data;
            this._name = _name;

            foreach (string s in ComboBoxValues.ComboBoxDictionary["Enchantment Type"].ToArray())
                _enchants.Add(s,"!");
        }

        public void AddLoreLine(string _loreLine)
        {
            _lore.Add(_loreLine);
        }

        public void RemoveLoreLine(string _loreLine)
        {
            for (int i = 0; i < Lore.Count; i++)
                if (Lore[i] == _loreLine)
                {
                    Lore.RemoveAt(i);
                    FieldsUpdated(this, new EventArgs());
                }
        }

        /// <summary>
        /// Adds vals to dicitionary. Does not handle exceptions!
        /// </summary>
        /// <param name="_enchant"></param>
        /// <param name="_enchantVal"></param>
        public void AddEnchant(string _enchant, string _enchantVal)
        {
            //Only call FieldsUpdated if no exceptions occured.
            try
            {
                Enchants.Add(_enchant, _enchantVal);
                FieldsUpdated(this, new EventArgs());
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
