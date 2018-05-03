using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FateForge.Managers
{
    public static class ConditionFieldManager
    {
        private static Dictionary<string, FieldUpdateHandler> _fieldHandlers = new Dictionary<string, FieldUpdateHandler>
        {
            { "Item in Chest", FieldUpdate_ChestItem},
            { "Scoreboard", FieldUpdate_ScoreBoard },
            { "World", FieldUpdate_World},
            { "Gamemode", FieldUpdate_Gamemode},
            { "Achievement", FieldUpdate_Achievement},
            { "Variable", FieldUpdate_Variable},
            { "Potion", FieldUpdate_Potion},
            { "Time", FieldUpdate_Time},
            { "Weather", FieldUpdate_Weather},
            { "Height", FieldUpdate_Height},
            { "Armor Rating", FieldUpdate_ArmorRating},
            { "Random", FieldUpdate_Random},
            { "Sneak", FieldUpdate_Sneak},
            { "Journal", FieldUpdate_Journal},
            { "Test for Block", FieldUpdate_TestforBlock},
            { "Empty Inventory Slots", FieldUpdate_EmptyInventory},
            { "Monsters in Area", FieldUpdate_MonstersInArea },
            { "Objective", FieldUpdate_Objective },
            { "Item in Inventory", FieldUpdate_ItemInInventory},
            { "Item in Hand", FieldUpdate_ItemInHand},
            { "Or", FieldUpdate_Or},
            { "And", FieldUpdate_Or},
            { "Health", FieldUpdate_Health},
            { "Permission", FieldUpdate_Permission },
            { "Point", FieldUpdate_Point },
            { "Experience", FieldUpdate_Experience},
            { "Armor", FieldUpdate_Armor},
            { "Location", FieldUpdate_Location}
        };

        public static Dictionary<string, FieldUpdateHandler> FieldHandlers { get => _fieldHandlers; set => _fieldHandlers = value; }

        public static void Initalize()
        {
            
        }

        private static void FieldUpdate_ChestItem(Control c)
        {
            c.Controls.Clear();

            LocationField _lf = new LocationField();
            ItemField _if = new ItemField();

            _if.Location = new System.Drawing.Point(0, _lf.Height + 2);

            c.Controls.Add(_lf);
            c.Controls.Add(_if);
        }
        private static void FieldUpdate_ScoreBoard(Control c)
        {
            c.Controls.Clear();

            StringValueField _sf1 = new StringValueField("Scoreboard");
            AmountField _af = new AmountField();

            _af.Location = new System.Drawing.Point(0, _sf1.Height + 2);

            c.Controls.Add(_sf1);
            c.Controls.Add(_af);
        }
        private static void FieldUpdate_World(Control c)
        {
            c.Controls.Clear();

            LocationField _lf = new LocationField(true, true);

            c.Controls.Add(_lf);
        }
        private static void FieldUpdate_Gamemode(Control c)
        {
            c.Controls.Clear();

            StringValueField _sf1 = new StringValueField("Gamemode");

            c.Controls.Add(_sf1);
        }
        private static void FieldUpdate_Achievement(Control c)
        {
            c.Controls.Clear();

            ComboField _cf = new ComboField("Achievement Type", ComboBoxValues.ComboBoxDictionary["Achievement Type"].ToArray());

            c.Controls.Add(_cf);
        }
        private static void FieldUpdate_Variable(Control c)
        {
            c.Controls.Clear();


        }
        private static void FieldUpdate_Potion(Control c)
        {
            c.Controls.Clear();

            ComboField _cf = new ComboField("Potion Type", ComboBoxValues.ComboBoxDictionary["Potion Type"].ToArray());

            c.Controls.Add(_cf);
        }
        private static void FieldUpdate_Time(Control c)
        {
            c.Controls.Clear();

            AmountField _af1 = new AmountField("Range 1");
            AmountField _af2 = new AmountField("Range 2");

            _af2.Location = new System.Drawing.Point(0, _af1.Height + 2);

            c.Controls.Add(_af1);
            c.Controls.Add(_af2);
        }
        private static void FieldUpdate_Weather(Control c)
        {
            c.Controls.Clear();

            ComboField _cf = new ComboField("Weather Type", ComboBoxValues.ComboBoxDictionary["Weather Type"].ToArray());

            c.Controls.Add(_cf);
        }
        private static void FieldUpdate_Height(Control c)
        {
            c.Controls.Clear();

            LocationField _lf = new LocationField(true);

            c.Controls.Add(_lf);
        }
        private static void FieldUpdate_Location(Control c)
        {
            c.Controls.Clear();

            LocationField _lf = new LocationField();

            c.Controls.Add(_lf);
        }
        private static void FieldUpdate_ArmorRating(Control c)
        {
            c.Controls.Clear();

            AmountField _af = new AmountField();

            c.Controls.Add(_af);
        }
        private static void FieldUpdate_Random(Control c)
        {
            c.Controls.Clear();

            AmountField _af1 = new AmountField("True");
            AmountField _af2 = new AmountField("Out of");

            _af2.Location = new System.Drawing.Point(0, _af1.Height + 2);

            c.Controls.Add(_af1);
            c.Controls.Add(_af2);
        }
        private static void FieldUpdate_Sneak(Control c)
        {
            c.Controls.Clear();
        }
        private static void FieldUpdate_Journal(Control c)
        {
            c.Controls.Clear();

            StringValueField _sf = new StringValueField("Entry");

            c.Controls.Add(_sf);
        }
        private static void FieldUpdate_TestforBlock(Control c)
        {
            c.Controls.Clear();

            LocationField _lf = new LocationField();
            ComboField _cf = new ComboField("Block Type", ComboBoxValues.ComboBoxDictionary["Block Type"].ToArray());

            _cf.Location = new System.Drawing.Point(0, _lf.Height + 2);

            c.Controls.Add(_lf);
            c.Controls.Add(_cf);
        }
        private static void FieldUpdate_EmptyInventory(Control c)
        {
            c.Controls.Clear();

            AmountField _af = new AmountField();

            c.Controls.Add(_af);
        }
        private static void FieldUpdate_MonstersInArea(Control c)
        {
            c.Controls.Clear();

            Panel _container = new Panel();
            _container.Size = new System.Drawing.Size(240,120);
            _container.AutoScroll = true;
            _container.BorderStyle = BorderStyle.Fixed3D;
            _container.ControlAdded += UpdateChildPositions;
            _container.ControlRemoved += UpdateChildPositions;
            Button _newEntry = new Button();
            _newEntry.Text = "Describe Mob";
            _newEntry.Click += (s, a) => 
            {
                _container.Controls.Add(
                    new DeletionFieldContainer(new List<Control>()
                    {
                        new ComboField("Mob Type", ComboBoxValues.ComboBoxDictionary["Mob Type"].ToArray()),
                        new StringValueField("Name"),
                        new AmountField()
                    })
                    );
            };

            _container.Location = new System.Drawing.Point(0, _newEntry.Height + 4);

            c.Controls.Add(_newEntry);
            c.Controls.Add(_container);

        }
        private static void FieldUpdate_Objective(Control c)
        {
            c.Controls.Clear();

            StringValueField _sf = new StringValueField("Obj Name");

            c.Controls.Add(_sf);

        }
        private static void FieldUpdate_ItemInInventory(Control c)
        {
            c.Controls.Clear();

            ItemField _if = new ItemField();

            c.Controls.Add(_if);
        }
        private static void FieldUpdate_ItemInHand(Control c)
        {
            c.Controls.Clear();

            ItemField _if = new ItemField();

            c.Controls.Add(_if);
        }
        private static void FieldUpdate_Or(Control c)
        {
            c.Controls.Clear();

            ConditionField _cf1 = new ConditionField();
            ConditionField _cf2 = new ConditionField();

            _cf2.Location = new System.Drawing.Point(0, _cf1.Height + 2);

            c.Controls.Add(_cf1);
            c.Controls.Add(_cf2);
        }
        private static void FieldUpdate_Health(Control c)
        {
            c.Controls.Clear();

            AmountField _af = new AmountField();

            c.Controls.Add(_af);
        }
        private static void FieldUpdate_Permission(Control c)
        {
            c.Controls.Clear();

            StringValueField _sf = new StringValueField("Permission");

            c.Controls.Add(_sf);
        }
        private static void FieldUpdate_Point(Control c)
        {
            c.Controls.Clear();

            StringValueField _sf = new StringValueField("Point Name");
            AmountField _af = new AmountField();

            _af.Location = new System.Drawing.Point(0, _sf.Height + 2);

            c.Controls.Add(_sf);
            c.Controls.Add(_af);
        }
        private static void FieldUpdate_Experience(Control c)
        {
            c.Controls.Clear();

            AmountField _af = new AmountField();
            CheckBox _skillAPI = new CheckBox();
            _skillAPI.Text = "SkillAPI";
            CheckBox _heroes = new CheckBox();
            _heroes.Text = "Heroes";

            _skillAPI.Location = new System.Drawing.Point(0, _af.Height + 2);
            _heroes.Location = new System.Drawing.Point(0, _af.Height + _skillAPI.Height + 2);

            c.Controls.Add(_af);
            c.Controls.Add(_skillAPI);
            c.Controls.Add(_heroes);
        }
        private static void FieldUpdate_Armor(Control c)
        {
            c.Controls.Clear();

            ItemField _if = new ItemField();

            c.Controls.Add(_if);
        }

        public static void UpdateChildPositions(object sender, EventArgs args)
        {
            if (!(sender is Panel))
                return;
            Panel _container = (Panel)sender;

            int _newHeight = 0;
            foreach (Control ca in _container.Controls)
            {
                ca.Location = new System.Drawing.Point(0, _newHeight - _container.VerticalScroll.Value);
                _newHeight += ca.Height + 4;
            }
        }
    }
}
