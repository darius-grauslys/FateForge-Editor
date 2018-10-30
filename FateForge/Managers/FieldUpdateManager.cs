using FateForge.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FateForge.Managers
{
    public delegate void FieldUpdateHandler(Control control);
    public delegate string YamlExport(List<Control> exportingControls);

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
            {"Objective Nested", EventFieldUpdate_ObjectiveNested},
            {"Objective Reference", EventFieldUpdate_ObjectiveReferenced},
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
            {"Death", ObjectiveFieldUpdate_Die },
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

        private static Dictionary<string, YamlExport> _eventYamlExport = new Dictionary<string, YamlExport>()
        {
            {"Cancel Quest", EventFieldUpdate_CancelQuest_Export},
            {"Clear", EventFieldUpdate_Clear_Export},
            {"Chest Clear", EventFieldUpdate_ChestClear_Export},
            {"Chest Give", EventFieldUpdate_ChestGive_Export},
            {"Conversation", EventFieldUpdate_Conversation_Export},
            {"Command", EventFieldUpdate_Command_Export},
            {"Compass", EventFieldUpdate_Compass_Export},
            {"Damage", EventFieldUpdate_Damage_Export},
            {"Door", EventFieldUpdate_Door_Export},
            {"Explosion", EventFieldUpdate_Explosion_Export},
            {"Give", EventFieldUpdate_Give_Export},
            {"Give Journal", FieldUpdate_GiveJournal_Export},
            {"If", EventFieldUpdate_If_Export},
            {"Journal", EventFieldUpdate_Journal_Export},
            {"Kill", EventFieldUpdate_Kill_Export},
            {"Lever", EventFieldUpdate_Lever_Export},
            {"Lightning", EventFieldUpdate_Lightning_Export},
            {"Message", EventFieldUpdate_Message_Export},
            {"Objective Nested", EventFieldUpdate_ObjectiveNested_Export},
            {"Objective Referenced", EventFieldUpdate_ObjectiveReferenced_Export},
            {"Party", EventFieldUpdate_Party_Export},
            {"Point", EventFieldUpdate_Point_Export},
            {"Potion Effect", EventFieldUpdate_PotionEffect_Export},
            {"Scoreboard", EventFieldUpdate_Scoreboard_Export},
            {"Set Block", EventFieldUpdate_SetBlock_Export},
            {"Spawn", EventFieldUpdate_Spawn_Export},
            {"Sudo", EventFieldUpdate_Sudo_Export},
            {"Tag", EventFieldUpdate_Tag_Export},
            {"Take Items", EventFieldUpdate_TakeItems_Export},
            {"Teleport", EventFieldUpdate_Teleport_Export},
            {"Time", EventFieldUpdate_Time_Export},
            {"Variable", EventFieldUpdate_Variable_Export},
            {"Weather", EventFieldUpdate_Weather_Export},
        };

        private static Dictionary<string, YamlExport> _objectiveYamlExport = new Dictionary<string, YamlExport>()
        {
            {"Location", ObjectiveFieldUpdate_Location_Export},
            {"Block", ObjectiveFieldUpdate_Block_Export },
            {"Mob Kill", ObjectiveFieldUpdate_MobKill_Export },
            {"Action", ObjectiveFieldUpdate_Action_Export },
            {"Death", ObjectiveFieldUpdate_Die_Export },
            {"Crafting", ObjectiveFieldUpdate_Craft_Export },
            {"Smelting", ObjectiveFieldUpdate_Smelt_Export },
            {"Taming", ObjectiveFieldUpdate_Tame_Export },
            {"Delay", ObjectiveFieldUpdate_Delay_Export },
            {"Arrow Shooting", ObjectiveFieldUpdate_Arrow_Export },
            {"Experience", ObjectiveFieldUpdate_Experience_Export },
            {"Step Pressure Plate", ObjectiveFieldUpdate_Step_Export },
            {"Logout", ObjectiveFieldUpdate_Logout_Export },
            {"Password", ObjectiveFieldUpdate_Password_Export },
            {"Fishing", ObjectiveFieldUpdate_Fish_Export },
            {"Shearing", ObjectiveFieldUpdate_Shear_Export },
            {"Enchant Item", ObjectiveFieldUpdate_Enchant_Export },
            {"Put Item in Chest", ObjectiveFieldUpdate_ChestPut_Export },
            {"Potion Brewing", ObjectiveFieldUpdate_Potion_Export },
            {"Consume", ObjectiveFieldUpdate_Consume_Export },
            {"Variable", ObjectiveFieldUpdate_Variable_Export },
            {"Kill", ObjectiveFieldUpdate_Kill_Export },
            {"Breed", ObjectiveFieldUpdate_Breed_Export }
        };

        #region ObjectiveYamlExports
        private static string ObjectiveFieldUpdate_Breed_Export(List<Control> exportingControls)
        {
            string yaml = "consume ";

            ComboField _cf1 = (ComboField)exportingControls[0];
            AmountField _af = (AmountField)exportingControls[1];
            ComboField _cf2 = (ComboField)exportingControls[2];

            yaml += _cf1.SelectedCombo + " " + _af.Amount + ((_cf2.SelectedCombo == "yes") ? " notify" : "");

            yaml += " {0}";

            return yaml;
        }

        private static string ObjectiveFieldUpdate_Kill_Export(List<Control> exportingControls)
        {
            throw new NotImplementedException();
        }

        private static string ObjectiveFieldUpdate_Variable_Export(List<Control> exportingControls)
        {
            throw new NotImplementedException();
        }

        private static string ObjectiveFieldUpdate_Consume_Export(List<Control> exportingControls)
        {
            string yaml = "consume ";

            ItemField _if = (ItemField)exportingControls[0];

            yaml += _if.SelectedValue;

            yaml += " {0}";

            return yaml;
        }

        private static string ObjectiveFieldUpdate_Potion_Export(List<Control> exportingControls)
        {
            string yaml = "potion ";

            ItemField _if = (ItemField)exportingControls[0];
            AmountField _af = (AmountField)exportingControls[1];
            ComboField _cf = (ComboField)exportingControls[2];
            
            yaml += _if.SelectedValue + " " + _af.Amount + ((_cf.SelectedCombo == "yes" ) ? " notify" : "" );

            yaml += " {0}";

            return yaml;
        }

        private static string ObjectiveFieldUpdate_ChestPut_Export(List<Control> exportingControls)
        {
            string yaml = "chestput ";
            
            ComboField _cf = (ComboField)exportingControls[0];
            LocationField _lf = (LocationField)exportingControls[1];
            DeletionListControl _dlc = (DeletionListControl)exportingControls[2];
            _lf.Radius = 0;
            
            yaml += _lf.GetYamlString_For_Objective() + " ";

            foreach (DeletionFieldContainer df in _dlc.DeletionNodes)
            {
                AmountField _af = df.Contents[1] as AmountField;
                yaml += (df.Contents[0] as ItemField).SelectedValue + ((_af.Amount != 0) ? ":"+_af.Amount.ToString() : "" ) + ";";
            }
            yaml = yaml.Substring(0, yaml.Length - 1);

            if (_cf.SelectedCombo == "yes")
                yaml += " items-stay";

            yaml += " {0}";

            return yaml;
        }

        private static string ObjectiveFieldUpdate_Enchant_Export(List<Control> exportingControls)
        {
            string yaml = "shear ";
            
            ItemField _if = (ItemField)exportingControls[0];
            ComboField _cf1 = (ComboField)exportingControls[1];
            AmountField _af = (AmountField)exportingControls[2];

            yaml += _if.SelectedValue + " " + _cf1.SelectedCombo + ":" + _af.Amount;

            return yaml; 
        }

        private static string ObjectiveFieldUpdate_Shear_Export(List<Control> exportingControls)
        {
            string yaml = "shear ";
            
            AmountField _af = (AmountField)exportingControls[0];
            StringValueField _sf1 = (StringValueField)exportingControls[1];
            StringValueField _sf2 = (StringValueField)exportingControls[2];

            yaml += _af.Amount + ((_sf1.TextField != "") ? " name:" + _sf1.TextField : "") + ((_sf2.TextField != "") ? " color:" + _sf1.TextField : "");

            return yaml;
        }

        private static string ObjectiveFieldUpdate_Fish_Export(List<Control> exportingControls)
        {
            string yaml = "fish ";

            ComboField _cf = (ComboField)exportingControls[0];
            AmountField _af = (AmountField)exportingControls[1];
            ComboField _cf1 = (ComboField)exportingControls[2];

            yaml += _cf.SelectedCombo + " " + _af.Amount + " " + ((_cf1.SelectedCombo == "yes") ? "notify" : "");

            return yaml;
        }

        private static string ObjectiveFieldUpdate_Password_Export(List<Control> exportingControls)
        {
            string yaml = "arrow ";

            StringValueField _sf = (StringValueField)exportingControls[0];
            ComboField _cf = (ComboField)exportingControls[1];

            yaml += _sf.TextField + " " + ((_cf.SelectedCombo == "yes") ? "ignoreCase" : "");

            yaml += " {0}";

            return yaml;
        }

        private static string ObjectiveFieldUpdate_Logout_Export(List<Control> exportingControls)
        {
            return "logout {0}";
        }

        private static string ObjectiveFieldUpdate_Step_Export(List<Control> exportingControls)
        {
            string yaml = "step ";

            LocationField _lf = (LocationField)exportingControls[0];

            yaml += _lf.GetYamlString_For_Objective();

            yaml += " {0}";

            return yaml;
        }

        private static string ObjectiveFieldUpdate_Experience_Export(List<Control> exportingControls)
        {
            string yaml = "experience ";

            AmountField _af = (AmountField)exportingControls[0];

            yaml += _af.Amount.ToString();

            yaml += " {0}";

            return yaml;
        }

        private static string ObjectiveFieldUpdate_Arrow_Export(List<Control> exportingControls)
        {
            string yaml = "arrow ";

            LocationField _lf = (LocationField)exportingControls[0];

            yaml += _lf.GetYamlString_For_Objective();

            yaml += " {0}";

            return yaml;
        }

        private static string ObjectiveFieldUpdate_Delay_Export(List<Control> exportingControls)
        {
            string yaml = "delay ";
            
            AmountField _af = (AmountField)exportingControls[0];

            yaml += _af.Amount.ToString();

            yaml += " {0}";

            return yaml;
        }

        private static string ObjectiveFieldUpdate_Tame_Export(List<Control> exportingControls)
        {
            string yaml = "tame ";

            ComboField _cf = (ComboField)exportingControls[0];
            AmountField _af = (AmountField)exportingControls[1];

            yaml += _cf.SelectedCombo.ToUpper() + " " + _af.Amount.ToString();

            yaml += " {0}";

            return yaml;
        }

        private static string ObjectiveFieldUpdate_Smelt_Export(List<Control> exportingControls)
        {
            string yaml = "craft ";

            ComboField _cf = (ComboField)exportingControls[0];
            AmountField _af = (AmountField)exportingControls[1];

            yaml += _cf.SelectedCombo.ToUpper() + " " + _af.Amount.ToString();

            yaml += " {0}";

            return yaml;
        }

        private static string ObjectiveFieldUpdate_Craft_Export(List<Control> exportingControls)
        {
            string yaml = "craft ";

            ItemField _if = (ItemField)exportingControls[0];
            AmountField _af = (AmountField)exportingControls[1];

            yaml += _if.SelectedValue + " " + _af.Amount.ToString();

            yaml += " {0}";

            return yaml;
        }

        private static string ObjectiveFieldUpdate_Die_Export(List<Control> exportingControls)
        {
            string yaml = "die ";

            ComboField _cf = (ComboField)exportingControls[0];

            if (_cf.SelectedCombo == "yes")
            {
                LocationField _lf = (LocationField)exportingControls[1];
                yaml += "cancel";
                if (_lf.Y > -1)
                {
                    yaml += _lf.ToString();
                }
            }

            yaml += " {0}";

            return yaml;
        }

        private static string ObjectiveFieldUpdate_Action_Export(List<Control> exportingControls)
        {
            string yaml = "action ";

            LocationField _lf = (LocationField)exportingControls[3];

            yaml += ((IYamlExportable)exportingControls[0]).GetYamlString_For_Objective() +
                " " + ((IYamlExportable)exportingControls[1]).GetYamlString_For_Objective() +
                ":" + ((IYamlExportable)exportingControls[2]).GetYamlString_For_Objective() +
                " " + _lf.GetYamlString_For_Objective();

            yaml.Insert(yaml.Length - _lf.Radius.ToString().Length, "range:");

            yaml += " {0}";

            return yaml;
        }

        private static string ObjectiveFieldUpdate_MobKill_Export(List<Control> exportingControls)
        {
            throw new NotImplementedException();
        }

        private static string ObjectiveFieldUpdate_Block_Export(List<Control> exportingControls)
        {
            string yaml = "block ";
            yaml += ((IYamlExportable)exportingControls[0]).GetYamlString_For_Objective() +
                ":" + ((IYamlExportable)exportingControls[1]).GetYamlString_For_Objective() +
                " " + ((IYamlExportable)exportingControls[2]).GetYamlString_For_Objective() + 
                " {0} " + ((IYamlExportable)exportingControls[3]).GetYamlString_For_Objective();
            return yaml;
        }

        private static string ObjectiveFieldUpdate_Location_Export(List<Control> exportingControls)
        {
            string yaml = "location ";

            yaml += ((IYamlExportable)exportingControls[0]).GetYamlString_For_Objective();
            yaml += " {0}";
            return yaml;
        }
#endregion

        #region EventFieldYamlExports
        private static string EventFieldUpdate_Weather_Export(List<Control> exportingControls)
        {
            ComboField _cf = exportingControls[0] as ComboField;

            return "weather " + _cf.SelectedCombo;
        }

        private static string EventFieldUpdate_Variable_Export(List<Control> exportingControls)
        {
            throw new NotImplementedException();
        }

        private static string EventFieldUpdate_Time_Export(List<Control> exportingControls)
        {
            StringValueField _sf = exportingControls[0] as StringValueField;

            return "time " + _sf.TextField;
        }

        private static string EventFieldUpdate_Teleport_Export(List<Control> exportingControls)
        {
            LocationField _lf = exportingControls[0] as LocationField;
            _lf.Radius = 0;
            AmountField _af1 = exportingControls[1] as AmountField;
            AmountField _af2 = exportingControls[2] as AmountField;

            return "teleport " + _lf.GetYamlString_For_Event() + ";" + _af1.Amount.ToString() + ";" + _af2.Amount.ToString();
        }

        private static string EventFieldUpdate_TakeItems_Export(List<Control> exportingControls)
        {
            string yaml = "take ";
            
            DeletionListControl _dlc = (DeletionListControl)exportingControls[0];

            foreach (DeletionFieldContainer df in _dlc.DeletionNodes)
            {
                AmountField _af = df.Contents[1] as AmountField;
                yaml += (df.Contents[0] as ItemField).SelectedValue + ((_af.Amount != 0) ? ":" + _af.Amount.ToString() : "") + ";";
            }
            yaml = yaml.Substring(0, yaml.Length - 1);

            return yaml;
        }

        private static string EventFieldUpdate_Tag_Export(List<Control> exportingControls)
        {
            string yaml = "tag ";

            ComboField _cf = (ComboField)exportingControls[0];
            StringValueField _sf = (StringValueField)exportingControls[1];

            yaml += _cf.SelectedCombo + " " + _sf.TextField;

            return yaml;
        }

        private static string EventFieldUpdate_Sudo_Export(List<Control> exportingControls)
        {
            string yaml = "sudo ";
            
            StringValueField _sf = (StringValueField)exportingControls[0];

            yaml += _sf.TextField;

            return yaml;
        }

        private static string EventFieldUpdate_Spawn_Export(List<Control> exportingControls)
        {
            throw new NotImplementedException();
        }

        private static string EventFieldUpdate_SetBlock_Export(List<Control> exportingControls)
        {
            string yaml = "setblock ";

            ComboField _cf = (ComboField)exportingControls[0];
            LocationField _lf = (LocationField)exportingControls[1];
            _lf.Radius = 0;

            yaml += _cf.SelectedCombo + " " + _lf.GetYamlString_For_Event();

            return yaml;
        }

        private static string EventFieldUpdate_Scoreboard_Export(List<Control> exportingControls)
        {
            string yaml = "damage ";

            AmountField _af = (AmountField)exportingControls[0];
            
            yaml += _af.Amount.ToString();

            return yaml;
        }

        private static string EventFieldUpdate_PotionEffect_Export(List<Control> exportingControls)
        {
            string yaml = "effect ";

            ComboField _cf1 = (ComboField)exportingControls[0];
            AmountField _af1 = (AmountField)exportingControls[1];
            AmountField _af2 = (AmountField)exportingControls[2];
            ComboField _cf2 = (ComboField)exportingControls[3];

            yaml += _cf1.SelectedCombo + " " + _af1.Amount + " " + _af1.Amount + ((_cf2.SelectedCombo == "yes") ? "--ambient" : "");

            return yaml;
        }

        private static string EventFieldUpdate_Point_Export(List<Control> exportingControls)
        {
            string yaml = "effect ";

            StringValueField _sf1 = (StringValueField)exportingControls[0];
            StringValueField _sf2 = (StringValueField)exportingControls[1];

            yaml += _sf1.TextField + " " + _sf1.TextField;

            return yaml;
        }

        private static string EventFieldUpdate_Party_Export(List<Control> exportingControls)
        {
            throw new NotImplementedException();
        }

        private static string EventFieldUpdate_ObjectiveNested_Export(List<Control> exportingControls)
        {
            string yaml = "objective start ";

            ObjectiveField _of = (ObjectiveField)exportingControls[0];

            yaml += _of.ObjectiveName;

            return yaml;
        }

        private static string EventFieldUpdate_ObjectiveReferenced_Export(List<Control> exportingControls)
        {
            string yaml = "objective ";

            ComboField _cf1 = (ComboField)exportingControls[0];
            ComboField _cf2 = (ComboField)exportingControls[1];

            yaml += _cf2.SelectedCombo + " " + _cf1.SelectedCombo;

            return yaml;
        }

        private static string EventFieldUpdate_Message_Export(List<Control> exportingControls)
        {
            string yaml = "message ";

            StringValueField _sf = (StringValueField)exportingControls[0];

            yaml += _sf.TextField;

            return yaml;
        }

        private static string EventFieldUpdate_Lightning_Export(List<Control> exportingControls)
        {
            string yaml = "lightning ";

            LocationField _lf = (LocationField)exportingControls[0];

            yaml += _lf.GetYamlString_For_Event();

            return yaml;
        }

        private static string EventFieldUpdate_Lever_Export(List<Control> exportingControls)
        {
            string yaml = "lightning ";

            LocationField _lf = (LocationField)exportingControls[0];
            ComboField _cf = (ComboField)exportingControls[1];

            yaml += _lf.GetYamlString_For_Event() + " " + _cf.SelectedCombo;

            return yaml;
        }

        private static string EventFieldUpdate_Kill_Export(List<Control> exportingControls)
        {
            return "kill";
        }

        private static string EventFieldUpdate_Journal_Export(List<Control> exportingControls)
        {
            string yaml = "lightning ";
            
            ComboField _cf = (ComboField)exportingControls[0];
            StringValueField _lf = (StringValueField)exportingControls[1];

            //yaml += _lf.GetYamlString_For_Event() + " " + _cf.SelectedCombo;

            return yaml;
        }

        private static string EventFieldUpdate_If_Export(List<Control> exportingControls)
        {
            throw new NotImplementedException();
        }

        private static string FieldUpdate_GiveJournal_Export(List<Control> exportingControls)
        {
            throw new NotImplementedException();
        }

        private static string EventFieldUpdate_Give_Export(List<Control> exportingControls)
        {
            throw new NotImplementedException();
        }

        private static string EventFieldUpdate_Explosion_Export(List<Control> exportingControls)
        {
            throw new NotImplementedException();
        }

        private static string EventFieldUpdate_Door_Export(List<Control> exportingControls)
        {
            throw new NotImplementedException();
        }

        private static string EventFieldUpdate_Damage_Export(List<Control> exportingControls)
        {
            throw new NotImplementedException();
        }

        private static string EventFieldUpdate_Compass_Export(List<Control> exportingControls)
        {
            throw new NotImplementedException();
        }

        private static string EventFieldUpdate_Command_Export(List<Control> exportingControls)
        {
            throw new NotImplementedException();
        }

        private static string EventFieldUpdate_Conversation_Export(List<Control> exportingControls)
        {
            throw new NotImplementedException();
        }

        private static string EventFieldUpdate_ChestGive_Export(List<Control> exportingControls)
        {
            throw new NotImplementedException();
        }

        private static string EventFieldUpdate_ChestClear_Export(List<Control> exportingControls)
        {
            throw new NotImplementedException();
        }

        private static string EventFieldUpdate_Clear_Export(List<Control> exportingControls)
        {
            throw new NotImplementedException();
        }

        private static string EventFieldUpdate_CancelQuest_Export(List<Control> exportingControls)
        {
            throw new NotImplementedException();
        }
#endregion

        #region Objective Control Updates
        public static void ObjectiveFieldUpdate_Location(Control control)
        {
            LocationField _lf = new LocationField();
            control.Controls.Clear();
            control.Controls.Add(_lf);
        }
        public static void ObjectiveFieldUpdate_Block(Control control)
        {
            ComboField _cf1 = new ComboField("Block", "Block Type");
            AmountField _af1 = new AmountField("Data");
            AmountField _af2 = new AmountField("Amount");
            ComboField _cf2 = new ComboField("Notify?", "YN_Answer Type");
            control.Controls.Clear();
            control.Controls.AddRange(new Control[] { _cf1, _af1, _af2, _cf2 });
        }
        public static void ObjectiveFieldUpdate_MobKill(Control control)
        {

        }
        public static void ObjectiveFieldUpdate_Action(Control control)
        {
            ComboField _cf1 = new ComboField("Click", "Left_Right_Answer Type");
            ComboField _cf2 = new ComboField("Block", "Block Type");
            //OPTIONAL:
            LocationField _lf = new LocationField();
            _lf.Y = -1;
            AmountField _af = new AmountField("Radius");
            _af.Amount = -1;
            control.Controls.Clear();
            control.Controls.AddRange(new Control[] { _cf1, _cf2, _lf, _af });
        }
        public static void ObjectiveFieldUpdate_Die(Control control)
        {
            ComboField _cf = new ComboField("Cancel?", "YN_Answer Type");
            LocationField _lf = new LocationField();
            _lf.Y = -1;
            control.Controls.Clear();
            control.Controls.Add(_cf);
            control.Controls.Add(_lf);
        }
        public static void ObjectiveFieldUpdate_Craft(Control control)
        {
            ItemField _if = new ItemField();
            AmountField _af = new AmountField();
            control.Controls.Clear();
            control.Controls.Add(_if);
            control.Controls.Add(_af);
        }
        public static void ObjectiveFieldUpdate_Smelt(Control control)
        {
            ComboField _cf = new ComboField("Item", "Item Type");
            AmountField _af = new AmountField();
            control.Controls.Clear();
            control.Controls.Add(_cf);
            control.Controls.Add(_af);
        }
        public static void ObjectiveFieldUpdate_Tame(Control control)
        {
            ComboField _cf = new ComboField("Animal", "Mob_Tameable Type");
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
            ComboField _cf = new ComboField("Ignore Case?", "YN_Answer Type");
            control.Controls.Clear();
            control.Controls.Add(_sf);
            control.Controls.Add(_cf);
        }
        public static void ObjectiveFieldUpdate_Fish(Control control)
        {
            ComboField _cf = new ComboField("Item", "Item Type");
            AmountField _af = new AmountField();
            ComboField _cf1 = new ComboField("Notify?", "YN_Answer Type");
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
            ComboField _cf1 = new ComboField("Enchantment", "Enchantment Type");
            AmountField _af = new AmountField("Level");
            control.Controls.Clear();
            //control.Controls.Add(_cf);
            control.Controls.Add(_if);
            control.Controls.Add(_cf1);
            control.Controls.Add(_af);
        }
        public static void ObjectiveFieldUpdate_ChestPut(Control control)
        {
            ComboField _cf = new ComboField("Items Stay?", "YN_Answer Type");
            LocationField _lf = new LocationField();
            DeletionListControl _dlc = new DeletionListControl();
            _dlc.NewItemHandle += () => 
            {
                ItemField _if = new ItemField();
                AmountField _af = new AmountField();

                return new DeletionFieldContainer(false, _if, _af);
            };
            control.Controls.Clear();
            control.Controls.Add(_cf);
            control.Controls.Add(_lf);
            control.Controls.Add(_dlc);
        }
        public static void ObjectiveFieldUpdate_Potion(Control control)
        {
            ItemField _if = new ItemField();
            AmountField _af = new AmountField();
            ComboField _cf = new ComboField("Notify?", "YN_Answer Type");
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
            ComboField _cf1 = new ComboField("Mob", "Breedmob Type");
            AmountField _af = new AmountField();
            ComboField _cf2 = new ComboField("Notify?", "YN_Answer Type");
            control.Controls.Clear();
            control.Controls.Add(_cf1);
            control.Controls.Add(_af);
            control.Controls.Add(_cf2);
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
            LocationField _lf = new LocationField();
            DeletionListControl _dlc = new DeletionListControl();
            _dlc.NewItemHandle += () =>
            {
                ItemField _if = new ItemField();
                AmountField _af = new AmountField();

                return new DeletionFieldContainer(false, new Control[] { _if, _af });
            };
            control.Controls.Clear();
            control.Controls.Add(_lf);
            control.Controls.Add(_dlc);
        }
        private static void EventFieldUpdate_Conversation(Control control)
        {
            ComboField _cf = new ComboField("Conversation");
            control.Controls.Clear();
            ConvoManager.ConvoNodeEvent _handle = (n) => _cf.SetSelections(ConvoManager.RootConvoNodeNames);
            ConvoManager.ConvoListUpdated += _handle;
            _cf.Disposed += (s, e) => ConvoManager.ConvoListUpdated -= _handle;
            control.Controls.Add(_cf);
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
            ComboField _cf = new ComboField("State", "Lever_Answer Type");
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
            DeletionListControl _dlc = new DeletionListControl();
            _dlc.NewItemHandle += () =>
            {
                ItemField _if = new ItemField();
                AmountField _af = new AmountField();

                return new DeletionFieldContainer(false, new Control[] { _if, _af });
            };
            _dlc.Dock = DockStyle.Fill;
            control.Controls.Clear();
            control.Controls.Add(_dlc);
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
            ComboField _cf = new ComboField("Mutate Type", "AddDel_Answer Type");
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
            ComboField _cf = new ComboField("State", "Lever_Answer Type");
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
        private static void EventFieldUpdate_ObjectiveNested(Control control)
        {
            ObjectiveField objField = new ObjectiveField();

            control.Resize += (s, a) =>
            {
                int countBuffer = (control.Controls.Count != 0) ? control.Controls.Count : 1;

                objField.Size = new System.Drawing.Size(control.Width - 18, (control.Height / countBuffer) - 4);
                int index = control.Controls.IndexOf(objField);
                int offset = 0;
                for (int i = 0; i < index; i++)
                    offset += control.Controls[i].Height + 4;
                objField.Location = new System.Drawing.Point(0, offset);
            };

            control.Controls.Clear();
            control.Controls.Add(objField);
        }

        private static void EventFieldUpdate_ObjectiveReferenced(Control control)
        {
            ComboField _cf1 = new ComboField("ObjName");
            ComboField _cf2 = new ComboField("Action", "Objective_Answer Type");

            _cf1.SetSelections(ObjectiveManager.ObjectiveNames);

            control.Controls.Clear();
            control.Controls.Add(_cf1);
            control.Controls.Add(_cf2);
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
            StringValueField _sf1 = new StringValueField("Point Name");
            StringValueField _sf2 = new StringValueField("Operation");
            control.Controls.Clear();
            control.Controls.Add(_sf1);
            control.Controls.Add(_sf2);
        }

        /// <summary>
        /// Handler for the PotionEffect field.
        /// </summary>
        /// <param name="control"></param>
        private static void EventFieldUpdate_PotionEffect(Control control)
        {
            ComboField _cf = new ComboField("Potion Type", "Potion Type");
            AmountField _af = new AmountField("Duration");
            AmountField _af_1 = new AmountField("Power");
            ComboField _cf_1 = new ComboField("Ambient?", "YN_Answer_Type");
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
            ComboField _cf = new ComboField("Block Type", "Block Type");
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
            ComboField _cf = new ComboField("Mutate Type", "AddDel_Answer Type");
            StringValueField _sf = new StringValueField("Operation");
            control.Controls.Clear();
            control.Controls.Add(_cf);
            control.Controls.Add(_sf);
        }
        private static void EventFieldUpdate_TakeItems(Control control)
        {
            DeletionListControl _dlc = new DeletionListControl();
            _dlc.NewItemHandle += () => 
            {
                ItemField _if = new ItemField();
                AmountField _af = new AmountField();

                return new DeletionFieldContainer(false, new Control[] { _if, _af });
            };
            _dlc.Dock = DockStyle.Fill;
            control.Controls.Clear();
            control.Controls.Add(_dlc);
        }
        
        /// <summary>
        /// Handler for updating the Teleport field.
        /// </summary>
        /// <param name="control"></param>
        private static void EventFieldUpdate_Teleport(Control control)
        {
            LocationField _lf = new LocationField();
            AmountField _af1 = new AmountField("Yaw");
            AmountField _af2 = new AmountField("Pitch");
            control.Controls.Clear();
            control.Controls.Add(_lf);
            control.Controls.Add(_af1);
            control.Controls.Add(_af2);
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
            ComboField _cf = new ComboField("Weather Type", "Weather Type");
            control.Controls.Clear();
            control.Controls.Add(_cf);
        }
        #endregion
        
        /// <summary>
        /// The methods used for updating fields of particular event types.
        /// </summary>
        public static Dictionary<string, FieldUpdateHandler> EventFieldHandlers { get => _eventFieldHandlers;}
        public static Dictionary<string, FieldUpdateHandler> ObjectiveFieldHandlers { get => _objectiveFieldHandlers; set => _objectiveFieldHandlers = value; }
        public static Dictionary<string, YamlExport> EventYamlExport { get => _eventYamlExport; set => _eventYamlExport = value; }
        public static Dictionary<string, YamlExport> ObjectiveYamlExport { get => _objectiveYamlExport; set => _objectiveYamlExport = value; }

        /// <summary>
        /// Creates FieldUpdate lexicon.
        /// </summary>
        public static void Initalize()
        {

        }
    }
}
