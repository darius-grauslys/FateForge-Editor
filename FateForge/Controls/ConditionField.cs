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

namespace FateForge
{
    public partial class ConditionField : UserControl, IIndependentResize, IXmlSerializable
    {
        private int _privateSize = Form1.DEFAULT_NEST_SIZE/4;
        private Size _initSize = new Size(0,0);

        public string SelectedValue { get => comboBox2.Text; set => comboBox2.Text = value; }

        public ConditionField()
        {
            InitializeComponent();

            _initSize = MinimumSize;

            comboBox2.TextChanged += comboBox2_SelectedIndexChanged;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Parent.Controls.Remove(this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IndependentCollapse(CollapseManager.FlipCollapseButton_Check((Button)sender));
            //CollapseManager.ResizeChilds(panel1);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ConditionFieldManager.FieldHandlers[comboBox2.Text](panel1);
                //int height = 0;
                //foreach (Control c in panel1.Controls)
                //    height += c.Height + 2;
                //_privateSize = (Form1.DEFAULT_NEST_SIZE / 4) + height;
                //Height = _privateSize;
                CollapseManager.WrapIndependentSize(this, panel1, 84);
            }
            catch
            {

            }
        }

        public void IndependentResize()
        {
            CollapseManager.WrapIndependentSize(this, panel1, 84);
        }

        public void IndependentCollapse(bool collapseState)
        {
            List<IIndependentResize> resizers = CollapseManager.ScanForResizers(panel1.Controls);
            CollapseManager.IndependentCollapse(collapseState, this, new List<Panel> { panel1 }, 36, resizers.ToArray());
        }

        public int GetDesiredSize()
        {
            return _privateSize;
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
    }
}
