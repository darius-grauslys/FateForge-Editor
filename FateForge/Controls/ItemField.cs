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
using FateForge.DataTypes;

namespace FateForge
{
    public partial class ItemField : UserControl, IXmlSerializable, IReferenceTableEntry
    {
        private QuestEditor _questItemReferences;
        private Item _referencedItem;

        public string SelectedValue { get => comboBox1.Text; private set => comboBox1.Text = value; }
        private QuestEditor QuestItemReferences { get => _questItemReferences; set => _questItemReferences = value; }
        private Item ReferencedItem { get => _referencedItem; set => _referencedItem = value; }

        public ItemField()
        {
            InitializeComponent();

            _questItemReferences = ((Form1)(Form1.ActiveForm)).ActiveEditor;

            tableLayoutPanel2.SetRow(new AmountField(),1);
            comboBox1.Items.AddRange(ItemManager.GetComboValues().ToArray());
            ItemManager.UpdatedList += (s, o) =>
            {
                comboBox1.Items.Clear();
                comboBox1.Items.AddRange(ItemManager.GetComboValues().ToArray());
            };

            comboBox1.TextChanged += ComboBox1_TextChanged;
        }

        private void ComboBox1_TextChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
                return;
            //if (ReferencedItem != null)
            //    QuestItemReferences.RemoveItemFromReference(ReferencedItem);

            Item _item = ItemManager.GetItem(comboBox1.Text);
            if (_item != null)
            {
                //QuestItemReferences.AddItemToReference(_item);
                ReferencedItem = _referencedItem;
            }
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

        public void Reference()
        {
            ReferencedItem.Reference();
        }
    }
}
