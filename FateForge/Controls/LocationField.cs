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
    public partial class LocationField : UserControl, IXmlSerializable
    {
        private bool _justHeight;
        private bool _justWorld;

        public LocationField(bool _justHeight=false, bool _justWorld=false)
        {
            InitializeComponent();
            this.JustHeight = _justHeight || _justWorld;
            this.JustWorld = _justWorld;

            if (this.JustHeight)
            {
                xCoord.Enabled = false;
                zCoord.Enabled = false;
            }
            if (_justWorld)
            {
                yCoord.Enabled = false;
            }
        }

        private bool JustHeight { get => _justHeight; set => _justHeight = value; }
        private bool JustWorld { get => _justWorld; set => _justWorld = value; }

        public decimal X { get => xCoord.Value; set => xCoord.Value = value;  }
        public decimal Y { get => yCoord.Value; set => yCoord.Value = value; }
        public decimal Z { get => zCoord.Value; set => zCoord.Value = value; }
        public string World { get => worldName.Text; set => worldName.Text = value; }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            reader.MoveToContent();
            X = Decimal.Parse( reader.GetAttribute("X") );
            Y = Decimal.Parse( reader.GetAttribute("Y") );
            Z = Decimal.Parse( reader.GetAttribute("Z") );
            World = reader.GetAttribute("World");
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString("X", X.ToString());
            writer.WriteAttributeString("Y", Y.ToString());
            writer.WriteAttributeString("Z", Z.ToString());
            writer.WriteAttributeString("World", World);
        }
    }
}
