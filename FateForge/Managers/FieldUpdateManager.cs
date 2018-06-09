using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FateForge.Managers
{
    public delegate void FieldUpdateHandler(Control control);

    /// <summary>
    /// Holds the event type lexicon, and updates fields based on event type.
    /// </summary>
    public static class FieldUpdateManager
    {
        private static Dictionary<string, FieldUpdateHandler> _eventFieldHandlers = new Dictionary<string, FieldUpdateHandler>()
        {
            {"Cancel Quest", EventFieldUpdate_CancelQuest},
            {"Clear", EventFieldUpdate_Clear},
            {"Chest Clear", EventFieldUpdate_ChestClear},
            {"Chest Give", EventFieldUpdate_ChestGive},
            {"Conversation", EventFieldUpdate_Conversation},
            {"Command", EventFieldUpdate_Command},
            {"Compass", EventFieldUpdate_Compass},
            {"Damage", EventFieldUpdate_Damage},
            {"Door", EventFieldUpdate_Door},
            {"Explosion", EventFieldUpdate_Explosion},
            {"Give", EventFieldUpdate_Give},
            {"Give Journal", FieldUpdate_GiveJournal},
            {"If", EventFieldUpdate_If},
            {"Journal", EventFieldUpdate_Journal},
            {"Kill", EventFieldUpdate_Kill},
            {"Lever", EventFieldUpdate_Lever},
            {"Lightning", EventFieldUpdate_Lightning},
            {"Message", EventFieldUpdate_Message },
            {"Objective", EventFieldUpdate_Objective},
            {"Party", EventFieldUpdate_Party},
            {"Point", EventFieldUpdate_Point},
            {"Potion Effect", EventFieldUpdate_PotionEffect},
            {"Scoreboard", EventFieldUpdate_Scoreboard},
            {"Set Block", EventFieldUpdate_SetBlock},
            {"Spawn", EventFieldUpdate_Spawn},
            {"Sudo", EventFieldUpdate_Sudo},
            {"Tag", EventFieldUpdate_Tag},
            {"Take Items", EventFieldUpdate_TakeItems},
            {"Teleport", EventFieldUpdate_Teleport},
            {"Time", EventFieldUpdate_Time},
            {"Variable", EventFieldUpdate_Variable},
            {"Weather", EventFieldUpdate_Weather},
        };
        private static Dictionary<string, FieldUpdateHandler> _objectiveFieldHandlers = new Dictionary<string, FieldUpdateHandler>()
        {
            {"Location", ObjectiveFieldUpdate_Location},
            {"Block", ObjectiveFieldUpdate_Block },
            {"Mob Kill", ObjectiveFieldUpdate_MobKill },
            {"Action", ObjectiveFieldUpdate_Action },
            {"Die", ObjectiveFieldUpdate_Die },
            {"Crafting", ObjectiveFieldUpdate_Craft },
            {"Smelting", ObjectiveFieldUpdate_Smelt },
            {"Taming", ObjectiveFieldUpdate_Tame },
            {"Delay", ObjectiveFieldUpdate_Delay },
            {"Arrow Shooting", ObjectiveFieldUpdate_Arrow },
            {"Experience", ObjectiveFieldUpdate_Experience },
            {"Step Pressure Plate", ObjectiveFieldUpdate_Step },
            {"Logout", ObjectiveFieldUpdate_Logout },
            {"Password", ObjectiveFieldUpdate_Password },
            {"Fishing", ObjectiveFieldUpdate_Fish },
            {"Shearing", ObjectiveFieldUpdate_Shear },
            {"Enchant Item", ObjectiveFieldUpdate_Enchant },
            {"Put Item in Chest", ObjectiveFieldUpdate_ChestPut },
            {"Potion Brewing", ObjectiveFieldUpdate_Potion },
            {"Consume", ObjectiveFieldUpdate_Consume },
            {"Variable", ObjectiveFieldUpdate_Variable },
            {"Kill", ObjectiveFieldUpdate_Kill },
            {"Breed", ObjectiveFieldUpdate_Breed }
        };

        #region Objective Control Updates
        public static void ObjectiveFieldUpdate_Location(Control control)
        {
            LocationField _lf = new LocationField();
            control.Controls.Clear();
            control.Controls.Add(_lf);
        }
        public static void ObjectiveFieldUpdate_Block(Control control)
        {
            
        }
        public static void ObjectiveFieldUpdate_MobKill(Control control)
        {

        }
        public static void ObjectiveFieldUpdate_Action(Control control)
        {
            ComboField _cf = new ComboField("Click", new object[] { "left", "right", "any" });
            //TODO: Block combo
            //OPTIONAL:
            LocationField _lf = new LocationField();
            AmountField _af = new AmountField("Radius");
        }
        public static void ObjectiveFieldUpdate_Die(Control control)
        {
            //OPTIONAL
            LocationField _lf = new LocationField();
        }
        public static void ObjectiveFieldUpdate_Craft(Control control)
        {
            ComboField _cf = new ComboField("Item", ComboBoxValues.ComboBoxDictionary["Item Type"].ToArray());
            AmountField _af = new AmountField();
            control.Controls.Clear();
            control.Controls.Add(_cf);
            control.Controls.Add(_af);
        }
        public static void ObjectiveFieldUpdate_Smelt(Control control)
        {
            ComboField _cf = new ComboField("Item", ComboBoxValues.ComboBoxDictionary["Item Type"].ToArray());
            AmountField _af = new AmountField();
            control.Controls.Clear();
            control.Controls.Add(_cf);
            control.Controls.Add(_af);
        }
        public static void ObjectiveFieldUpdate_Tame(Control control)
        {
            ComboField _cf = new ComboField("Animal", ComboBoxValues.ComboBoxDictionary["Mob_Tameable Type"].ToArray());
            AmountField _af = new AmountField();
            control.Controls.Clear();
            control.Controls.Add(_cf);
            control.Controls.Add(_af);
        }
        public static void ObjectiveFieldUpdate_Delay(Control control)
        {
            AmountField _af = new AmountField("Duration");
            control.Controls.Clear();
            control.Controls.Add(_af);
        }
        public static void ObjectiveFieldUpdate_Arrow(Control control)
        {
            LocationField _lf = new LocationField();
            AmountField _af = new AmountField("Radius");
            control.Controls.Clear();
            control.Controls.Add(_lf);
            control.Controls.Add(_af);
        }
        public static void ObjectiveFieldUpdate_Experience(Control control)
        {
            AmountField _af = new AmountField();
            control.Controls.Clear();
            control.Controls.Add(_af);
        }
        public static void ObjectiveFieldUpdate_Step(Control control)
        {
            LocationField _lf = new LocationField();
            control.Controls.Clear();
            control.Controls.Add(_lf);
        }
        public static void ObjectiveFieldUpdate_Logout(Control control)
        {
            control.Controls.Clear();
        }
        public static void ObjectiveFieldUpdate_Password(Control control)
        {
            StringValueField _sf = new StringValueField();
            ComboField _cf = new ComboField("Ignore Case?", ComboBoxValues.ComboBoxDictionary["YN_Answer Type"].ToArray());
            control.Controls.Clear();
            control.Controls.Add(_sf);
            control.Controls.Add(_cf);
        }
        public static void ObjectiveFieldUpdate_Fish(Control control)
        {
            ComboField _cf = new ComboField("Item", ComboBoxValues.ComboBoxDictionary["Item Type"].ToArray());
            AmountField _af = new AmountField();
            ComboField _cf1 = new ComboField("Notify?", ComboBoxValues.ComboBoxDictionary["YN_Answer Type"].ToArray());
            control.Controls.Clear();
            control.Controls.Add(_cf);
            control.Controls.Add(_af);
            control.Controls.Add(_cf1);
        }
        public static void ObjectiveFieldUpdate_Shear(Control control)
        {
            AmountField _af = new AmountField();
            StringValueField _sf = new StringValueField("Name");
            StringValueField _sf1 = new StringValueField("Color");
            control.Controls.Clear();
            control.Controls.Add(_af);
            control.Controls.Add(_sf);
            control.Controls.Add(_sf1);
        }
        public static void ObjectiveFieldUpdate_Enchant(Control control)
        {
            //ComboField _cf = new ComboField("Item", ComboBoxValues.ComboBoxDictionary["Item Type"].ToArray());
            ItemField _if = new ItemField();
            ComboField _cf1 = new ComboField("Enchantment", ComboBoxValues.ComboBoxDictionary["Enchantment Type"].ToArray());
            AmountField _af = new AmountField("Level");
            control.Controls.Clear();
            //control.Controls.Add(_cf);
            control.Controls.Add(_if);
            control.Controls.Add(_cf1);
            control.Controls.Add(_af);
        }
        public static void ObjectiveFieldUpdate_ChestPut(Control control)
        {
            //TODO: Implement the List control.
        }
        public static void ObjectiveFieldUpdate_Potion(Control control)
        {
            ItemField _if = new ItemField();
            AmountField _af = new AmountField();
            ComboField _cf = new ComboField("Notify?", ComboBoxValues.ComboBoxDictionary["YN_Answer Type"].ToArray());
            control.Controls.Clear();
            control.Controls.Add(_if);
            control.Controls.Add(_af);
            control.Controls.Add(_cf);
        }
        public static void ObjectiveFieldUpdate_Consume(Control control)
        {
            ItemField _if = new ItemField();
            control.Controls.Clear();
            control.Controls.Add(_if);
        }
        public static void ObjectiveFieldUpdate_Variable(Control control)
        {
            //IMPLEMENT VARIABLES LATER
        }
        public static void ObjectiveFieldUpdate_Kill(Control control)
        {
            //IMPLEMENT LATER
        }
        public static void ObjectiveFieldUpdate_Breed(Control control)
        {
            //IMPLEMENT MOB TYPE
        }
        #endregion

        #region Event Control updates
        private static void EventFieldUpdate_CancelQuest(Control control)
        {

        }
        private static void EventFieldUpdate_Clear(Control control)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Handler for the ChestClear field.
        /// </summary>
        /// <param name="control"></param>
        private static void EventFieldUpdate_ChestClear(Control control)
        {
            LocationField _lf = new LocationField();
            control.Controls.Clear();
            control.Controls.Add(_lf);
        }
        private static void EventFieldUpdate_ChestGive(Control control)
        {
            throw new NotImplementedException();
        }
        private static void EventFieldUpdate_Conversation(Control control)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Handler for updating the Command field.
        /// </summary>
        /// <param name="control"></param>
        private static void EventFieldUpdate_Command(Control control)
        {
            StringValueField _sf = new StringValueField("Command");
            control.Controls.Clear();
            control.Controls.Add(_sf);
        }
        private static void EventFieldUpdate_Compass(Control control)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Handler for the Damage field.
        /// </summary>
        /// <param name="control"></param>
        private static void EventFieldUpdate_Damage(Control control)
        {
            AmountField _af = new AmountField();
            control.Controls.Clear();
            control.Controls.Add(_af);
        }

        /// <summary>
        /// Handler for the Door field.
        /// </summary>
        /// <param name="control"></param>
        private static void EventFieldUpdate_Door(Control control)
        {
            LocationField _lf = new LocationField();
            ComboField _cf = new ComboField("State", ComboBoxValues.ComboBoxDictionary["Lever_Answer Type"].ToArray());
            control.Controls.Clear();
            control.Controls.Add(_lf);
            control.Controls.Add(_cf);
        }

        /// <summary>
        /// Handler for the Explosion field.
        /// </summary>
        /// <param name="control"></param>
        private static void EventFieldUpdate_Explosion(Control control)
        {
            AmountField _af = new AmountField("Fire?");
            AmountField _af_1 = new AmountField("Grief?");
            AmountField _af_2 = new AmountField("Power");
            LocationField _lf = new LocationField();
            control.Controls.Clear();
            control.Controls.Add(_af);
            control.Controls.Add(_af_1);
            control.Controls.Add(_af_2);
            control.Controls.Add(_lf);
        }
        private static void EventFieldUpdate_Give(Control control)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Handler for the GiveJournal field.
        /// </summary>
        /// <param name="control"></param>
        private static void FieldUpdate_GiveJournal(Control control)
        {
            control.Controls.Clear();
        }
        private static void EventFieldUpdate_If(Control control)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Handler for updating the Journal field.
        /// </summary>
        /// <param name="control"></param>
        private static void EventFieldUpdate_Journal(Control control)
        {
            ComboField _cf = new ComboField("Mutate Type", ComboBoxValues.ComboBoxDictionary["AddDel_Answer Type"].ToArray());
            StringValueField _sf = new StringValueField("Entry Name");
            control.Controls.Clear();
            control.Controls.Add(_cf);
            control.Controls.Add(_sf);
        }

        /// <summary>
        /// Handler for the Kill field.
        /// </summary>
        /// <param name="control"></param>
        private static void EventFieldUpdate_Kill(Control control)
        {
            control.Controls.Clear();
        }

        /// <summary>
        /// Handler for the Lever field.
        /// </summary>
        /// <param name="control"></param>
        private static void EventFieldUpdate_Lever(Control control)
        {
            LocationField _lf = new LocationField();
            ComboField _cf = new ComboField("State", ComboBoxValues.ComboBoxDictionary["Lever_Answer Type"].ToArray());
            control.Controls.Clear();
            control.Controls.Add(_lf);
            control.Controls.Add(_cf);
        }

        /// <summary>
        /// Handler for the Lightning field.
        /// </summary>
        /// <param name="control"></param>
        private static void EventFieldUpdate_Lightning(Control control)
        {
            LocationField _lf = new LocationField();
            control.Controls.Clear();
            control.Controls.Add(_lf);
        }

        /// <summary>
        /// Handler for updating the Message field.
        /// </summary>
        /// <param name="control"></param>
        private static void EventFieldUpdate_Message(Control control)
        {
            StringValueField _sf = new StringValueField("Message");
            control.Controls.Clear();
            control.Controls.Add(_sf);
        }

        /// <summary>
        /// The handler for updating a field for a Objective Event.
        /// </summary>
        private static void EventFieldUpdate_Objective(Control control)
        {
            ObjectiveField objField = new ObjectiveField();

            control.Resize += (s, a) =>
            {
                objField.Size = new System.Drawing.Size(control.Width - 18, (control.Height / control.Controls.Count) - 4);
                int index = control.Controls.IndexOf(objField);
                int offset = 0;
                for (int i = 0; i < index; i++)
                    offset += control.Controls[i].Height + 4;
                objField.Location = new System.Drawing.Point(0, offset);
            };

            control.Controls.Clear();
            control.Controls.Add(objField);
        }
        private static void EventFieldUpdate_Party(Control control)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Handler for updating the Point field.
        /// </summary>
        /// <param name="control"></param>
        private static void EventFieldUpdate_Point(Control control)
        {
            StringValueField _sf = new StringValueField("Point Name");
            AmountField _af = new AmountField();
            control.Controls.Clear();
            control.Controls.Add(_sf);
            control.Controls.Add(_af);
        }

        /// <summary>
        /// Handler for the PotionEffect field.
        /// </summary>
        /// <param name="control"></param>
        private static void EventFieldUpdate_PotionEffect(Control control)
        {
            ComboField _cf = new ComboField("Potion Type", ComboBoxValues.ComboBoxDictionary["Potion Type"].ToArray());
            AmountField _af = new AmountField("Duration");
            AmountField _af_1 = new AmountField("Power");
            ComboField _cf_1 = new ComboField("Ambient?", ComboBoxValues.ComboBoxDictionary["YN_Answer_Type"].ToArray());
            control.Controls.Clear();
            control.Controls.Add(_cf);
            control.Controls.Add(_af);
            control.Controls.Add(_af_1);
            control.Controls.Add(_cf_1);
        }

        /// <summary>
        /// Handler for the Scoreboard field.
        /// </summary>
        /// <param name="control"></param>
        private static void EventFieldUpdate_Scoreboard(Control control)
        {
            StringValueField _sf = new StringValueField("Point Type");
            StringValueField _sf_1 = new StringValueField("Operation");
            control.Controls.Clear();
            control.Controls.Add(_sf);
            control.Controls.Add(_sf_1);
        }
        
        /// <summary>
        /// Handler for the SetBlock field.
        /// </summary>
        /// <param name="control"></param>
        private static void EventFieldUpdate_SetBlock(Control control)
        {
            ComboField _cf = new ComboField("Block Type", ComboBoxValues.ComboBoxDictionary["Block Type"].ToArray());
            LocationField _lf = new LocationField();
            control.Controls.Clear();
            control.Controls.Add(_cf);
            control.Controls.Add(_lf);
        }
        private static void EventFieldUpdate_Spawn(Control control)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Handler for the Sudo field.
        /// </summary>
        /// <param name="control"></param>
        private static void EventFieldUpdate_Sudo(Control control)
        {
            StringValueField _sf = new StringValueField("Command");
            control.Controls.Clear();
            control.Controls.Add(_sf);
        }

        /// <summary>
        /// Handler for updating the Tag field.
        /// </summary>
        /// <param name="control"></param>
        private static void EventFieldUpdate_Tag(Control control)
        {
            ComboField _cf = new ComboField("Mutate Type", ComboBoxValues.ComboBoxDictionary["AddDel_Answer Type"].ToArray());
            StringValueField _sf = new StringValueField("Operation");
            control.Controls.Clear();
            control.Controls.Add(_cf);
            control.Controls.Add(_sf);
        }
        private static void EventFieldUpdate_TakeItems(Control control)
        {
            throw new NotImplementedException();
        }
        
        /// <summary>
        /// Handler for updating the Teleport field.
        /// </summary>
        /// <param name="control"></param>
        private static void EventFieldUpdate_Teleport(Control control)
        {
            LocationField _lf = new LocationField();
            control.Controls.Clear();
            control.Controls.Add(_lf);
        }

        /// <summary>
        /// Handler for the Time field.
        /// </summary>
        /// <param name="control"></param>
        private static void EventFieldUpdate_Time(Control control)
        {
            StringValueField _sf = new StringValueField("Operation");
            control.Controls.Clear();
            control.Controls.Add(_sf);
        }
        private static void EventFieldUpdate_Variable(Control control)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Handler for the Weather field.
        /// </summary>
        /// <param name="control"></param>
        private static void EventFieldUpdate_Weather(Control control)
        {
            ComboField _cf = new ComboField("Weather Type", ComboBoxValues.ComboBoxDictionary["Weather Type"].ToArray());
            control.Controls.Clear();
            control.Controls.Add(_cf);
        }
        #endregion
        
        /// <summary>
        /// The methods used for updating fields of particular event types.
        /// </summary>
        public static Dictionary<string, FieldUpdateHandler> EventFieldHandlers { get => _eventFieldHandlers;}
        public static Dictionary<string, FieldUpdateHandler> ObjectiveFieldHandlers { get => _objectiveFieldHandlers; set => _objectiveFieldHandlers = value; }

        /// <summary>
        /// Creates FieldUpdate lexicon.
        /// </summary>
        public static void Initalize()
        {

        }
    }
}
