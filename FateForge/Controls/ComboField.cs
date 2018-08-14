using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Schema;
using FateForge.Managers;

namespace FateForge
{
    public partial class ComboField : UserControl, IXmlSerializable
    {
        private string _comboFieldType = "";

        /// <summary>
        /// The combo selected by the user.
        /// </summary>
        public string SelectedCombo { get => comboBox1.Text; private set => comboBox1.Text = value; }
        private string ComboFieldType { get => _comboFieldType; set => _comboFieldType = value; }

        public ComboField(string _valueName = "Type", string _comboFieldType="" )
        {
            InitializeComponent();

            label1.Text = _valueName;
            if (ComboBoxValues.ComboBoxDictionary.ContainsKey(_comboFieldType))
                comboBox1.Items.AddRange(ComboBoxValues.ComboBoxDictionary[_comboFieldType].ToArray());

            ComboFieldType = _comboFieldType;
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            reader.MoveToContent();
            ComboFieldType = reader.GetAttribute("ComboFieldType");
            SelectedCombo = reader.GetAttribute("SelectedCombo");
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString("ComboFieldType", ComboFieldType);
            writer.WriteAttributeString("SelectedCombo", SelectedCombo);
        }
    }
}
