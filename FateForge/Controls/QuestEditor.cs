using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using FateForge.Managers;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Schema;
using FateForge.Managers.IO;

namespace FateForge
{
    public partial class QuestEditor : UserControl, IXmlSerializable
    {
        ElementContainer _baseElementContainer = new ElementContainer();

        public string QuestName { get => questNameTextbox.Text; private set => questNameTextbox.Text = value; }
        public string Description { get => richTextBox1.Text; private set => richTextBox1.Text = value; }

        public QuestEditor()
        {
            InitializeComponent();

            questNameTextbox.TextChanged += QuestTitle_TextChanged;
            Resize += QuestEditor_Resize;

            //ObjectiveField _objf = new ObjectiveField();

            //panel2.Controls.Add(_objf);

            //_objf.Resize += QuestEditor_Resize;
            panel2.ControlRemoved += QuestEditor_Resize;

            questNameTextbox.Text = QuestManager.GetNewQuestName();

            QuestManager.AddNewQuest(this);
            //panel2.ControlAdded += Panel2_ControlAdded;
        }

        private void Panel2_ControlAdded(object sender, ControlEventArgs e)
        {
            panel2.Controls[panel2.Controls.Count - 1].Resize += QuestEditor_Resize;
        }

        private void QuestEditor_Resize(object sender, EventArgs e)
        {
            int positionY = 0;
            foreach (Control c in panel2.Controls)
            {
                c.Size = new Size(panel2.Width - 12, c.Height);
                c.Location = new Point(0, positionY - panel2.VerticalScroll.Value);
                positionY += 2 + (c.Height);
            }
        }

        private void QuestTitle_TextChanged(object sender, EventArgs e)
        {
            if (QuestManager.IsOtherNameExisting(this))
            {
                MessageBox.Show("This quest name is already in use! Please change to avoid issues.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            UpdateParentTagName();
            QuestManager.UpdateQuests();
        }

        public void UpdateParentTagName()
        {
            if (Parent is TabPage)
            {
                PropertyInfo pInfo = typeof(TabPage).GetProperty("Text");
                pInfo.SetValue(((TabPage)Parent), Convert.ChangeType(questNameTextbox.Text, pInfo.PropertyType), null);
            }
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            Control newControl = new Control();

            switch (comboBox1.Text)
            {
                case "Condition":
                    newControl = new ConditionField();
                    break;
                case "Event":
                    newControl = new EventField();
                    break;
                case "Objective":
                    newControl = new ObjectiveField();
                    newControl.Size = new Size(panel2.Width-12, (int)(Form1.DEFAULT_NEST_SIZE * 2.5f));
                    break;
                default:
                    return;
            }
            newControl.Location = new Point(0, (Form1.DEFAULT_NEST_SIZE * panel2.Controls.Count) + (2 * panel2.Controls.Count));
            newControl.Resize += QuestEditor_Resize;
            panel2.Controls.Add(newControl);
            QuestEditor_Resize(null,null);
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            QuestName = reader.GetAttribute("QuestName");
            reader.ReadStartElement();
            Description = reader.GetAttribute("DescriptionText");
            reader.ReadStartElement();
            panel2.Controls.AddRange(ImportManager.GetControlListFromXml(this, reader.ReadSubtree()).ToArray());
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString("QuestName", QuestName);
            writer.WriteStartElement("Description");
            writer.WriteStartAttribute("DescriptionText", richTextBox1.Text);
            writer.WriteEndAttribute();
            writer.WriteEndElement();

            writer.WriteStartElement("QuestContents");
            ExportManager.ExportControlList(writer, panel2.Controls);
            writer.WriteEndElement();
        }
    }
}
