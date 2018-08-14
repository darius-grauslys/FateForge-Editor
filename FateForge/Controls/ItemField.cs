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

namespace FateForge
{
    public partial class ItemField : UserControl, IXmlSerializable
    {
        public string SelectedValue { get => comboBox1.Text; private set => comboBox1.Text = value; }

        public ItemField()
        {
            InitializeComponent();

            tableLayoutPanel2.SetRow(new AmountField(),1);
            ItemManager.UpdatedList += (s, o) =>
            {
                comboBox1.Items.Clear();
                comboBox1.Items.AddRange(ItemManager.GetComboValues().ToArray());
            };
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            SelectedValue = reader.GetAttribute("SelectedValue");
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString("SelectedValue", SelectedValue);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ((Form1)Form1.ActiveForm).SwitchTab(TabPageEnum.Item);
        }
    }
}
