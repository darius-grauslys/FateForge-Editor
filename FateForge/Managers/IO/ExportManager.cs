using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;
using FateForge.DataTypes;
using System.Xml;
using static System.Windows.Forms.Control;
using FateForge.Utility;

namespace FateForge.Managers.IO
{
    public static class ExportManager
    {
        public delegate void ExportDataFunction(StreamWriter s,  object o);

        private static XmlSerializer _serializer;
        private static XmlSerializer Serializer { get => _serializer; set => _serializer = value; }
        

        private static Dictionary<Type, ExportDataFunction> _exportDataFunctions = new Dictionary<Type, ExportDataFunction>()
        {
            { typeof(Item), ExportItemData }
        };

        #region ExportBetonStructure

        private static void ExportSerializiable(StreamWriter s, Control c)
        {
            XmlWriter xw = XmlWriter.Create(s);
            xw.WriteStartElement(c.Name);
            ((IXmlSerializable)c).WriteXml(xw);
            xw.WriteEndElement();
            xw.Flush();
            xw.Close();
        }
        
        #endregion

        #region ExportFateforgeData

        /// <summary>
        /// All functions responsible for each data piece in the editor. (Items, NPCs, Locations, Jornals, etc)
        /// </summary>
        public static Dictionary<Type, ExportDataFunction> ExportDataFunctions { get => _exportDataFunctions; set => _exportDataFunctions = value; }

        private static void ExportItemData( StreamWriter s, object i)
        {
            throw new NotFiniteNumberException();
        }

        #endregion

        public static void SerializeGenericBetonStructure(StreamWriter s, Control c)
        {
            if (c is IXmlSerializable)
            {
                ExportSerializiable(s, c);
            }
        }

        public static void ExportControlList(XmlWriter writer, ControlCollection list)
        {
            foreach (Control c in list)
            {
                if (c is IXmlSerializable)
                {
                    writer.WriteStartElement(c.Name);
                    (c as IXmlSerializable).WriteXml(writer);
                    writer.WriteEndElement();
                }
            }
        }

        private static void ExportDataYAML(QuestEditor q, string workingDirectory)
        {
            TextWriter tw = File.CreateText(workingDirectory + "\\items.yaml");
            ExportItemsYAML(q, new YamlWriter(tw));
            
        }

        public static void ExportQuestYAML(QuestEditor q, string workingDirectory)
        {
            //We are clearing all references made by any prior quest export.
            ReferenceTable.ClearReferenceTable();
            //This will reference EVERYTHING utilized by the quest.
            q.Reference();

            string workingDir_q = workingDirectory + "\\" + q.QuestName;
            Directory.CreateDirectory(workingDir_q);
            ExportDataYAML(q, workingDir_q);
            ExportConvoHeadsYAML(workingDirectory + "\\conversations");
        }

        private static void ExportObjectiveYAML(QuestEditor q, YamlWriter yw)
        {
            List<ObjectiveField> objs = ReferenceTable.ReferencedObjectiveList;

            foreach (ObjectiveField obj in objs)
            {
                yw.WriteFullScalar(obj.Name, obj.GetYamlString_As_Objective(objs.IndexOf(obj)));
            }
            yw.Close();
        }

        public static void ExportJournalEntriesYAML()
        {
            JournalEntryManager
        }

        /// <summary>
        /// Creates a YAML config for each convo made by the user.
        /// </summary>
        /// <param name="convoNodes">The convos to config up</param>
        /// <param name="workingDirectory">The directory to work in.</param>
        public static void ExportConvoHeadsYAML(string workingDirectory)
        {
            List<TreeNode> convoNodes = ReferenceTable.ReferencedConvoList;
            TextWriter tw;
            YamlWriter yw;

            foreach (TreeNode convo in convoNodes)
            {
                tw = File.CreateText(workingDirectory + "\\" + convo.Text + ".yml");
                yw = new YamlWriter(tw);
                ConvoEditor ce = convo.Tag as ConvoEditor;
                yw.WriteFullScalar("quester:",ce.QuesterName);

                yw.WriteScalarName("NPC_options:");
                yw.IndentationLevel++;
                RecursiveWriteConvo_NPC(convo, yw, convo.Text);
                yw.IndentationLevel--;
                yw.WriteScalarName("player_options:");
                yw.IndentationLevel++;
                //RecursiveReadConvo_PLAYER(convo, yw);
            }
        }

        /// <summary>
        /// Recursively writes all NPC option statements.
        /// </summary>
        /// <param name="subNode">Node operating on.</param>
        /// <param name="yw">Writer</param>
        /// <param name="accumText">The accumulating text for unique pointers.</param>
        public static void RecursiveWriteConvo_NPC(TreeNode subNode, YamlWriter yw, string accumText)
        {
            //I do not write conditions and or event fields here because it is over redundant.
            //Handle that shit in the player options.

            ConvoEditor ce = subNode.Tag as ConvoEditor;
            yw.WriteScalarName(accumText + subNode.Text + "_npc");
            yw.IndentationLevel++;
            yw.WriteFullScalar("text:", ce.NPC_Text);

            string pointers = "";

            foreach (TreeNode nestedNode in subNode.Nodes)
                pointers += ", " + accumText + nestedNode.Text + "_player";
            if (pointers.Length > 2)
                pointers = pointers.Substring(2,pointers.Length);

            yw.WriteFullScalar("pointers:", pointers);
            yw.IndentationLevel--;

            foreach (TreeNode nestedNode in subNode.Nodes)
                RecursiveWriteConvo_NPC(nestedNode, yw, accumText + nestedNode.Text);
        }

        /// <summary>
        /// Recursively writes all player conversation statements.
        /// </summary>
        /// <param name="subNode"></param>
        /// <param name="yw"></param>
        public static void RecursiveWriteConvo_PLAYER(TreeNode subNode, YamlWriter yw, string accumText)
        {
            ConvoEditor ce = subNode.Tag as ConvoEditor;

            List<ConvoPlayerResponse> responses = ce.GetResponses();

            foreach (ConvoPlayerResponse response in responses)
            {
                yw.WriteScalarName(accumText + response.ResponseUniqueName + "_player");
                yw.IndentationLevel++;
                yw.WriteFullScalar("text", response.PlayerResponse);
                
                if (ObjectiveManager.ObjectiveNames.Contains(response.ObjectiveFieldRef))
                {
                    ObjectiveField objRef = ObjectiveManager.GetObjective(response.ObjectiveFieldRef);

                    objRef.ReferenceChildsOnly();

                    List<ConditionField> conditions = objRef.ReferencedConditionFields;
                    List<EventField> events = objRef.ReferencedEventFields;

                    string conditionYaml = "";
                    string eventYaml = "";
                    
                    foreach (ConditionField cf in conditions)
                        conditionYaml += ", " + cf.UniqueIdentifier;
                    if (conditionYaml.Length >= 2)
                        conditionYaml = conditionYaml.Substring(2, conditionYaml.Length);

                    foreach (EventField ef in events)
                        eventYaml += ", " + ef.UniqueIdentifier;
                    if (eventYaml.Length >= 2)
                        eventYaml = eventYaml.Substring(2, eventYaml.Length);

                    yw.WriteFullScalar("event", eventYaml);
                    yw.WriteFullScalar("condition", conditionYaml);
                }

                string pointers = accumText + response.SubNode.Text + "_npc";
                if (pointers.Length > 2)
                    pointers = pointers.Substring(2, pointers.Length);

                yw.WriteFullScalar("pointers", pointers);
                yw.IndentationLevel--;
            }
        }

        public static void ExportConvoNPC_YAML(TreeNode subNode, YamlWriter yw)
        {
            ConvoEditor ce = subNode.Tag as ConvoEditor;
            yw.WriteFullScalar("quester", ce.QuesterName);
            string startingOptions = "";
            foreach (TreeNode node in subNode.Nodes)
                startingOptions += ", " + node.Text;
            if (startingOptions.Length >= 2)
                startingOptions = startingOptions.Substring(2, startingOptions.Length);
            yw.WriteFullScalar("first", startingOptions);
            if (ce.StayStill)
                yw.WriteFullScalar("stop", "'true'");
            if (ce.FinalEvents.Count != 0)
            {
                string finalEvents = "";
                foreach (EventField ef in ce.FinalEvents)
                {
                    //ef ref
                    finalEvents += ", " + ef.UniqueIdentifier;
                }
                if (finalEvents.Length >= 2)
                    finalEvents = finalEvents.Substring(2, finalEvents.Length);
                yw.WriteFullScalar("final_events", finalEvents);
            }

            foreach (TreeNode node in subNode.Nodes)
                RecursiveWriteConvo_NPC(node, yw, "");

            foreach (TreeNode node in subNode.Nodes)
                RecursiveWriteConvo_PLAYER(node, yw, "");
        }

        public static void ExportItemsYAML(QuestEditor q, YamlWriter yw)
        {
            List<Item> items = ReferenceTable.ReferenedItemList;

            foreach (Item item in items)
            {
                string yaml = "";
                string lore = "";
                string enchant = "";
                foreach (string str in item.Lore)
                    lore += ";" + str;
                lore = lore.Substring(1,lore.Length);

                if (item.Name == "")
                    item.Name = items.IndexOf(item) + "_INVALID_NAME";
                
                yaml += (item.Type != "") ? item.Type.ToUpper() + " " : "STICK" ;
                yaml += item.Name + " ";
                yaml += lore + " ";
                if (item.Enchants.Count != 0)
                {
                    foreach (KeyValuePair<string, string> pair in item.Enchants)
                    {
                        enchant += String.Format(
                            ";{0}:{1}",
                            pair.Key,
                            pair.Value
                            );
                    }
                    enchant = enchant.Substring(1, enchant.Length);
                    yaml += String.Format("enchant:{0} ", enchant);
                }
                yw.WriteFullScalar(item.Alias, yaml);
            }
            yw.Close();
        }
    }
}
