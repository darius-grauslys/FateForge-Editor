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
using FateForge.DataTypes;

namespace FateForge
{
    public partial class ComboField : UserControl, IXmlSerializable, IYamlExportable, IReferenceTableEntry
    {
        private string _comboFieldType = "";

        /// <summary>
        /// The combo selected by the user.
        /// </summary>
        public string SelectedCombo { get => comboBox1.Text; private set => comboBox1.Text = value; }
        private string ComboFieldType { get => _comboFieldType; set => _comboFieldType = value; }

        public event EventHandler SelectionChanged { add => comboBox1.TextChanged += value; remove => comboBox1.TextChanged -= value; }

        public ComboField(string _valueName = "Type", string _comboFieldType="" )
        {
            InitializeComponent();

            label1.Text = _valueName;
            if (ComboBoxValues.ComboBoxDictionary.ContainsKey(_comboFieldType))
                comboBox1.Items.AddRange(ComboBoxValues.ComboBoxDictionary[_comboFieldType].ToArray());

            ComboFieldType = _comboFieldType;
        }

        public void SetSelections(List<string> _selections)
        {
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(_selections.ToArray());
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

        public string GetYamlString_For_Objective()
        {
            return SelectedCombo;
        }

        public string GetYamlString_For_Event()
        {
            return SelectedCombo;
        }

        public string GetYamlString_For_Condition()
        {
            return SelectedCombo;
        }

        public string GetYamlString_As_Objective(int indexOf)
        {
            throw new NotImplementedException();
        }

        public string GetYamlString_As_Event(int indexOf)
        {
            throw new NotImplementedException();
        }

        public string GetYamlString_As_Condition(int indexOf)
        {
            throw new NotImplementedException();
        }

        public void Reference()
        {
            if (ComboFieldType == "Objective_Answer Type")
                ObjectiveManager.GetObjective(SelectedCombo)?.Reference();
        }
    }
}
