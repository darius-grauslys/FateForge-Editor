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

namespace FateForge
{
    public partial class StringValueField : UserControl, IXmlSerializable
    {
        /// <summary>
        /// Event Handler for when the string value text is changed.
        /// </summary>
        public EventHandler TextFieldChanged;
        /// <summary>
        /// The value of the string field.
        /// </summary>
        public string TextField { get => textBox1.Text; internal set => textBox1.Text = value; }

        public event EventHandler ValueChanged { add => textBox1.TextChanged += value; remove => textBox1.TextChanged -= value; }

        public StringValueField(string _valueName="String Val")
        {
            InitializeComponent();

            label1.Text = _valueName;

            textBox1.TextChanged += (s, a) => 
            {
                TextFieldChanged?.Invoke(s, a);
            };
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            TextField = reader.GetAttribute("TextField");
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString("TextField", TextField);
        }
    }
}
