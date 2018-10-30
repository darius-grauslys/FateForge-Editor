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
    public partial class EventField : UserControl, IIndependentResize, IXmlSerializable, IReferenceTableEntry
    {
        private bool _isMin = false;
        private Size _initSize = new Size(0,0);
        private string _uniqueIdentifier;

        public string SelectedValue { get => comboBox1.Text; set => comboBox1.Text = value; }
        public string UniqueIdentifier { get => _uniqueIdentifier; internal set => _uniqueIdentifier = value; }

        public EventField()
        {
            InitializeComponent();

            _initSize = MinimumSize;

            comboBox1.TextChanged += ComboBox1_SelectedValueChanged;

            comboBox1.Items.AddRange(FieldUpdateManager.EventFieldHandlers.Keys.ToArray());

            panel1.ControlAdded += (s, o) => { CollapseManager.ResizeChilds(panel1); IndependentResize();  };
            panel1.ControlRemoved += (s, o) => { CollapseManager.ResizeChilds(panel1); IndependentResize(); };

            EventFieldManager.AddEvent(this);
        }

        private void ComboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                FieldUpdateManager.EventFieldHandlers[comboBox1.Text](panel1);
                IndependentResize();
            }
            catch
            { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IndependentCollapse(CollapseManager.FlipCollapseButton_Check((Button)sender));
            //_isMin = !_isMin;

            //if (button1.Text == "-")
            //{
            //    button1.Text = "o";
            //    MinimumSize = new Size(Width, 36);
            //    Height = 36;
            //}
            //else
            //{
            //    button1.Text = "-";
            //    MinimumSize = _initSize;
            //    IndependentResize();
            //}

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Parent.Controls.Remove(this);
        }

        public void IndependentResize()
        {
            int height = 40;

            CollapseManager.WrapIndependentSize(this, panel1);

            //if (!_isMin)
            //    CollapseManager.IndependentResize(this, panel1);
        }

        public void IndependentCollapse(bool collapseState)
        {
            List<IIndependentResize> resizers = CollapseManager.ScanForResizers(panel1.Controls);
            CollapseManager.IndependentCollapse(collapseState, this, new List<Panel> { panel1 }, 36, resizers.ToArray());
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
            reader.ReadStartElement();
            SelectedValue = reader.GetAttribute("SelectedValue");
            panel1.Controls.Clear();
            panel1.Controls.AddRange(ImportManager.GetControlListFromXml(this, reader).ToArray());
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString("SelectedValue", SelectedValue);
            ExportManager.ExportControlList(writer, panel1.Controls);
        }

        public void Reference()
        {
            ReferenceTable.AddReference(this);
            foreach (IReferenceTableEntry reference in panel1.Controls)
                reference.Reference();
        }
    }
}
