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
    public static class EventFieldManager
    {
        private static Dictionary<string, FieldUpdateHandler> _fieldHandlers = new Dictionary<string, FieldUpdateHandler>()
        {
            {"Cancel Quest", FieldUpdate_CancelQuest},
            {"Clear", FieldUpdate_Clear},
            {"Chest Clear", FieldUpdate_ChestClear},
            {"Chest Give", FieldUpdate_ChestGive},
            {"Conversation", FieldUpdate_Conversation},
            {"Command", FieldUpdate_Command},
            {"Compass", FieldUpdate_Compass},
            {"Damage", FieldUpdate_Damage},
            {"Door", FieldUpdate_Door},
            {"Explosion", FieldUpdate_Explosion},
            {"Give", FieldUpdate_Give},
            {"Give Journal", FieldUpdate_GiveJournal},
            {"If", FieldUpdate_If},
            {"Journal", FieldUpdate_Journal},
            {"Kill", FieldUpdate_Kill},
            {"Lever", FieldUpdate_Lever},
            {"Lightning", FieldUpdate_Lightning},
            {"Message", FieldUpdate_Message },
            {"Objective", FieldUpdate_Objective},
            {"Party", FieldUpdate_Party},
            {"Point", FieldUpdate_Point},
            {"Potion Effect", FieldUpdate_PotionEffect},
            {"Scoreboard", FieldUpdate_Scoreboard},
            {"Set Block", FieldUpdate_SetBlock},
            {"Spawn", FieldUpdate_Spawn},
            {"Sudo", FieldUpdate_Sudo},
            {"Tag", FieldUpdate_Tag},
            {"Take Items", FieldUpdate_TakeItems},
            {"Teleport", FieldUpdate_Teleport},
            {"Time", FieldUpdate_Time},
            {"Variable", Field_UpdateVariable},
            {"Weather", FieldUPdate_Weather},
        };

        private static void FieldUpdate_CancelQuest(Control control)
        {

        }
        private static void FieldUpdate_Clear(Control control)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Handler for the ChestClear field.
        /// </summary>
        /// <param name="control"></param>
        private static void FieldUpdate_ChestClear(Control control)
        {
            LocationField _lf = new LocationField();
            control.Controls.Clear();
            control.Controls.Add(_lf);
        }
        private static void FieldUpdate_ChestGive(Control control)
        {
            throw new NotImplementedException();
        }
        private static void FieldUpdate_Conversation(Control control)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Handler for updating the Command field.
        /// </summary>
        /// <param name="control"></param>
        private static void FieldUpdate_Command(Control control)
        {
            StringValueField _sf = new StringValueField("Command");
            control.Controls.Clear();
            control.Controls.Add(_sf);
        }
        private static void FieldUpdate_Compass(Control control)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Handler for the Damage field.
        /// </summary>
        /// <param name="control"></param>
        private static void FieldUpdate_Damage(Control control)
        {
            AmountField _af = new AmountField();
            control.Controls.Clear();
            control.Controls.Add(_af);
        }

        /// <summary>
        /// Handler for the Door field.
        /// </summary>
        /// <param name="control"></param>
        private static void FieldUpdate_Door(Control control)
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
        private static void FieldUpdate_Explosion(Control control)
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
        private static void FieldUpdate_Give(Control control)
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
        private static void FieldUpdate_If(Control control)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Handler for updating the Journal field.
        /// </summary>
        /// <param name="control"></param>
        private static void FieldUpdate_Journal(Control control)
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
        private static void FieldUpdate_Kill(Control control)
        {
            control.Controls.Clear();
        }

        /// <summary>
        /// Handler for the Lever field.
        /// </summary>
        /// <param name="control"></param>
        private static void FieldUpdate_Lever(Control control)
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
        private static void FieldUpdate_Lightning(Control control)
        {
            LocationField _lf = new LocationField();
            control.Controls.Clear();
            control.Controls.Add(_lf);
        }

        /// <summary>
        /// Handler for updating the Message field.
        /// </summary>
        /// <param name="control"></param>
        private static void FieldUpdate_Message(Control control)
        {
            StringValueField _sf = new StringValueField("Message");
            control.Controls.Clear();
            control.Controls.Add(_sf);
        }

        /// <summary>
        /// The handler for updating a field for a Objective Event.
        /// </summary>
        private static void FieldUpdate_Objective(Control control)
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
        private static void FieldUpdate_Party(Control control)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Handler for updating the Point field.
        /// </summary>
        /// <param name="control"></param>
        private static void FieldUpdate_Point(Control control)
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
        private static void FieldUpdate_PotionEffect(Control control)
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
        private static void FieldUpdate_Scoreboard(Control control)
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
        private static void FieldUpdate_SetBlock(Control control)
        {
            ComboField _cf = new ComboField("Block Type", ComboBoxValues.ComboBoxDictionary["Block Type"].ToArray());
            LocationField _lf = new LocationField();
            control.Controls.Clear();
            control.Controls.Add(_cf);
            control.Controls.Add(_lf);
        }
        private static void FieldUpdate_Spawn(Control control)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Handler for the Sudo field.
        /// </summary>
        /// <param name="control"></param>
        private static void FieldUpdate_Sudo(Control control)
        {
            StringValueField _sf = new StringValueField("Command");
            control.Controls.Clear();
            control.Controls.Add(_sf);
        }

        /// <summary>
        /// Handler for updating the Tag field.
        /// </summary>
        /// <param name="control"></param>
        private static void FieldUpdate_Tag(Control control)
        {
            ComboField _cf = new ComboField("Mutate Type", ComboBoxValues.ComboBoxDictionary["AddDel_Answer Type"].ToArray());
            StringValueField _sf = new StringValueField("Operation");
            control.Controls.Clear();
            control.Controls.Add(_cf);
            control.Controls.Add(_sf);
        }
        private static void FieldUpdate_TakeItems(Control control)
        {
            throw new NotImplementedException();
        }
        
        /// <summary>
        /// Handler for updating the Teleport field.
        /// </summary>
        /// <param name="control"></param>
        private static void FieldUpdate_Teleport(Control control)
        {
            LocationField _lf = new LocationField();
            control.Controls.Clear();
            control.Controls.Add(_lf);
        }

        /// <summary>
        /// Handler for the Time field.
        /// </summary>
        /// <param name="control"></param>
        private static void FieldUpdate_Time(Control control)
        {
            StringValueField _sf = new StringValueField("Operation");
            control.Controls.Clear();
            control.Controls.Add(_sf);
        }
        private static void Field_UpdateVariable(Control control)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Handler for the Weather field.
        /// </summary>
        /// <param name="control"></param>
        private static void FieldUPdate_Weather(Control control)
        {
            ComboField _cf = new ComboField("Weather Type", ComboBoxValues.ComboBoxDictionary["Weather Type"].ToArray());
            control.Controls.Clear();
            control.Controls.Add(_cf);
        }

        /// <summary>
        /// The methods used for updating fields of particular event types.
        /// </summary>
        public static Dictionary<string, FieldUpdateHandler> FieldHandlers { get => _fieldHandlers;}

        /// <summary>
        /// Creates FieldUpdate lexicon.
        /// </summary>
        public static void Initalize()
        {

        }
    }
}
