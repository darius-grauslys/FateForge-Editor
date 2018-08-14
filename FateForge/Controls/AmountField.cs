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
using System.Xml.XPath;
using System.Xml;
using System.Xml.Schema;
using FateForge.DataTypes;

namespace FateForge
{
    public partial class AmountField : UserControl, IXmlSerializable
    {
        public EventHandler AmountChanged;
        public decimal Amount { get => numericUpDown1.Value; set => numericUpDown1.Value = value; }
        public string ValueName { get => label1.Text; set => label1.Text = value; }

        public AmountField()
        {
            InitializeComponent();

            ValueName = "Amount";

            numericUpDown1.ValueChanged += (s, o) => { AmountChanged?.Invoke(s, o); };
        }

        public AmountField(string _valueName="Amount")
        {
            InitializeComponent();

            ValueName = _valueName;

            numericUpDown1.ValueChanged += (s, o) => { AmountChanged?.Invoke(s, o); };
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            reader.MoveToContent();
            ValueName = reader.GetAttribute("ValueName");
            Amount = Decimal.Parse(reader.GetAttribute("Amount"));
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString("ValueName", ValueName);
            writer.WriteAttributeString("Amount", Amount.ToString());
        }
    }
}
