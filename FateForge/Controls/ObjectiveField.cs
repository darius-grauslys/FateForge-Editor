using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FateForge.Managers;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Schema;
using FateForge.Managers.IO;
using FateForge.DataTypes;

namespace FateForge
{
    public partial class ObjectiveField : UserControl, IIndependentResize, IXmlSerializable, IYamlExportable, IReferenceTableEntry
    {
        private Size _initSize = new Size(0,0);
        private List<Panel> _panelPackage = new List<Panel>();
        private string _oldName;

        private List<EventField> _referencedEventFields = new List<EventField>();
        private List<ConditionField> _referencedConditionFields = new List<ConditionField>();

        public string SelectedValue { get => comboBox1.Text; private set => comboBox1.Text = value; }
        public string ObjectiveName { get => textBox1.Text; internal set => textBox1.Text = value; }
        public List<EventField> ReferencedEventFields { get => _referencedEventFields.ToList(); private set => _referencedEventFields = value; }
        public List<ConditionField> ReferencedConditionFields { get => _referencedConditionFields.ToList(); private set => _referencedConditionFields = value; }
        internal string OldName { get => _oldName; set => _oldName = value; }

        public ObjectiveField(bool _nameReadOnly=false)
        {
            InitializeComponent();

            //_panelPackage.Add(panel2);
            _panelPackage.Add(panel3);
            _panelPackage.Add(panel4);

            textBox1.ReadOnly = _nameReadOnly;
            _initSize = MinimumSize;

            Resize += ObjectiveField_Resize;
            panel2.ControlRemoved += (s, o) => { PanelResize(panel2); };
            panel2.ControlAdded += (s, o) => { PanelResize(panel2); };
            panel3.ControlRemoved += (s, o) => { PanelResize(panel3); IndependentResize(); };
            panel3.ControlAdded += (s, o) => { PanelResize(panel3); IndependentResize(); };
            panel4.ControlRemoved += (s, o) => { PanelResize(panel4, 20); };
            panel4.ControlAdded += (s, o) => { PanelResize(panel4, 20); };
            comboBox1.SelectedValueChanged += ComboBox1_SelectedValueChanged;

            ObjectiveManager.AddObjective(this);

            textBox1.TextChanged += TextBox1_TextChanged;
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == OldName)
                return;
            try
            {
                ObjectiveManager.Rename(this);
                OldName = textBox1.Text;
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Invalid name. Names starting with \"obj_\" is for the editor only.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Text = OldName;
            }
        }

        private void ComboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            FieldUpdateManager.ObjectiveFieldHandlers[comboBox1.Text](panel2);
        }

        private void PanelResize(Panel panel, int offset=12)
        {
            CollapseManager.ResizeChilds(panel, offset);
        }

        private void ObjectiveField_Resize(object sender, EventArgs e)
        {
            //IndependentResize();

            CollapseManager.ResizeChilds(panel2);
            CollapseManager.ResizeChilds(panel3);
            CollapseManager.ResizeChilds(panel4, 20);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            bool _collapseType = (b.Text == "-");
            
            CollapseManager.FlipCollapseButton(b, _collapseType);
            IndependentCollapse(_collapseType);
            

            //CollapseManager.Collapse(button1, this, _initSize, 60);

            //if (button2.Text == "-")
            //{
            //    button2.Text = "o";
            //    tableLayoutPanel1.RowStyles[1] = new RowStyle(SizeType.Percent, 0);
            //    Height = 46;
            //    tableLayoutPanel2.RowStyles[1] = new RowStyle(SizeType.Percent, 0);
            //}
            //else
            //{
            //    button2.Text = "-";
            //    tableLayoutPanel1.RowStyles[1] = new RowStyle(SizeType.Percent, 31.97f);
            //    Height = (int)(Form1.DEFAULT_NEST_SIZE * 2.5);
            //    tableLayoutPanel2.RowStyles[1] = new RowStyle(SizeType.Percent, 100);
            //}
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (button6.Text == "-")
            {
                button6.Text = "o";
                tableLayoutPanel8.ColumnStyles[0].Width = 40;
                tableLayoutPanel3.ColumnStyles[0] = new ColumnStyle(SizeType.Absolute, 80);
            }
            else
            {
                button6.Text = "-";
                tableLayoutPanel8.ColumnStyles[0].Width = 125;
                tableLayoutPanel3.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 28.57f);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (button4.Text == "-")
            {
                button4.Text = "o";
                tableLayoutPanel5.RowStyles[1] = new RowStyle(SizeType.Absolute, 0);
            }
            else
            {
                button4.Text = "-";
                tableLayoutPanel5.RowStyles[1] = new RowStyle(SizeType.Percent, 100);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //if (panel3.Height < 250)
            //{
            //    Button escapeButton = new Button();
            //    escapeButton.Text = ("[Open in New Window]");
            //    escapeButton.Dock = DockStyle.Fill;
            //    eventField.Dock = DockStyle.Fill;
            //    escapeButton.Tag = eventField;
            //    escapeButton.Click += EscapeButton_Click;
            //    panel3.Controls.Add(escapeButton);
            //    return;
            //}
            EventField _ef = new EventField();
            ReferencedEventFields.Add(_ef);
            AddNewEventField(_ef);
        }

        private void AddNewEventField(EventField _ef)
        {
            _ef.Size = new Size(panel3.Width - 12, panel3.Height);

            int positionY = 0;
            foreach (Control c in panel3.Controls)
                positionY += c.Height + 2 - panel3.VerticalScroll.Value;

            _ef.Location = new Point(2, positionY);
            _ef.Resize += ObjectiveField_Resize;
            _ef.Parent = panel3;
            panel3.Controls.Add(_ef);
        }

        private void EscapeButton_Click(object sender, EventArgs e)
        {
            if (!(sender is Control))
                return;
            WindowedEditor escapeEditor = new WindowedEditor(((Control)sender).Parent, (Control)((Control)sender).Tag);
            escapeEditor.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (button3.Text == "-")
            {
                button3.Text = "o";
                tableLayoutPanel10.RowStyles[1].Height = 0;
                tableLayoutPanel1.RowStyles[1] = new RowStyle(SizeType.Absolute, 42);
            }
            else
            {
                button3.Text = "-";
                tableLayoutPanel10.RowStyles[1].Height = 100;
                tableLayoutPanel1.RowStyles[1] = new RowStyle(SizeType.Percent, 31.97f);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Parent.Controls.Remove(this);
            ObjectiveManager.RemoveObjective(this);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ConditionField _cf = new ConditionField();
            ReferencedConditionFields.Add(_cf);
            AddNewConditionField(_cf);
        }

        private void AddNewConditionField(ConditionField _cf)
        {
            _cf.Size = new Size(panel4.Width - 20, Form1.DEFAULT_NEST_SIZE / 4);

            int positionY = 0;
            foreach (Control c in panel4.Controls)
                positionY += c.Height + 2 - panel4.VerticalScroll.Value;

            _cf.Location = new Point(2, positionY);
            _cf.Resize += ObjectiveField_Resize;
            _cf.Parent = panel4;
            panel4.Controls.Add(_cf);
        }

        public void IndependentResize()
        {
            CollapseManager.IndependentResize(this, _panelPackage);
        }

        public void IndependentCollapse(bool collapseState)
        {
            List<IIndependentResize> resizers = new List<IIndependentResize>();
            foreach (EventField ef in panel3.Controls)
                resizers.Add((IIndependentResize)ef);
            foreach (ConditionField cf in panel4.Controls)
                resizers.Add((IIndependentResize)cf);

            CollapseManager.IndependentCollapse(collapseState, this, _panelPackage, 40, resizers.ToArray());

            if (collapseState)
                tableLayoutPanel1.RowStyles[1].Height = 0;
            else
                tableLayoutPanel1.RowStyles[1].Height = 30;

            //CollapseManager.ResizeChilds;
        }

        public int GetDesiredSize()
        {
            return _initSize.Height;
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            reader.MoveToContent();
            SelectedValue = reader.GetAttribute("SelectedValue");
            ObjectiveName = reader.GetAttribute("ObjectiveName");
            panel2.Controls.Clear();
            reader.ReadStartElement();

            XmlReader inner;

            //reader.ReadStartElement();
            if (!reader.IsEmptyElement && reader.Name == "Conditions")
            {
                //reader.ReadStartElement();
                inner = reader.ReadSubtree();
                while (inner.Read())
                {
                    ConditionField _cf = new ConditionField();

                    _cf.ReadXml(inner.ReadSubtree());

                    AddNewConditionField(_cf);
                }
            }
            reader.ReadEndElement();
            //reader.ReadEndElement();
            //reader.ReadStartElement();
            if (!reader.IsEmptyElement && reader.Name == "Events")
            {
                //reader.ReadStartElement();
                inner = reader.ReadSubtree();
                while (inner.Read())
                {
                    EventField _ef = new EventField();

                    _ef.ReadXml(inner.ReadSubtree());

                    AddNewEventField(_ef);
                }
            }
            reader.ReadEndElement();
            //reader.ReadEndElement();
            //reader.ReadStartElement();
            if (!reader.IsEmptyElement && reader.Name == "Parameters")
            {
                //reader.ReadStartElement();
                panel2.Controls.AddRange(ImportManager.GetControlListFromXml(this, reader).ToArray());
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString("SelectedValue", SelectedValue);
            writer.WriteAttributeString("ObjectiveName", ObjectiveName);
            writer.WriteStartElement("Conditions");

            ExportManager.ExportControlList(writer, panel4.Controls);

            writer.WriteEndElement();
            writer.WriteStartElement("Events");

            ExportManager.ExportControlList(writer, panel3.Controls);

            writer.WriteEndElement();
            writer.WriteStartElement("Parameters");

            ExportManager.ExportControlList(writer, panel2.Controls);

            writer.WriteEndElement();
        }

        public string GetYamlString_For_Objective()
        {
            return "";
        }

        public string GetYamlString_For_Event()
        {
            return "";
        }

        public string GetYamlString_For_Condition()
        {
            return "";
        }

        public string GetYamlString_As_Objective(int indexOf)
        {
            //Important to know that we only write the scalar value. Not the full scalar.

            string yaml = "";
            string eventNameYaml = "";
            if (ObjectiveName == "")
                ObjectiveName = indexOf + "_INVALID_NAME";


            foreach (EventField e in panel4.Controls)
                eventNameYaml += ";" + e.UniqueIdentifier;
            eventNameYaml = eventNameYaml.Substring(1, eventNameYaml.Length);

            yaml = String.Format(FieldUpdateManager.ObjectiveYamlExport[SelectedValue](panel2.Controls.Cast<Control>().ToList()), eventNameYaml);

            return "";
        }

        public string GetYamlString_As_Event(int indexOf)
        {
            //DO NOT IMPLEMENT
            return "";
        }

        public string GetYamlString_As_Condition(int indexOf)
        {
            //DO NOT IMPLEMENT
            return "";
        }

        public void Reference()
        {
            ReferenceTable.AddReference(this);
            ReferenceChildsOnly();
        }

        public void ReferenceChildsOnly()
        {
            foreach (IReferenceTableEntry refe in panel2.Controls)
                refe.Reference();
            foreach (IReferenceTableEntry refe in panel3.Controls)
                refe.Reference();
            foreach (IReferenceTableEntry refe in panel4.Controls)
                refe.Reference();
        }
    }
}
