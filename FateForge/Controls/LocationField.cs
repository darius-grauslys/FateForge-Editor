using FateForge.DataTypes;
using System;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace FateForge
{
    public partial class LocationField : UserControl, IXmlSerializable, IYamlExportable
    {
        private bool _justHeight;
        private bool _justWorld;

        public event EventHandler XCoordChanged { add => xCoord.ValueChanged += value; remove => xCoord.ValueChanged -= value; }
        public event EventHandler YCoordChanged { add => yCoord.ValueChanged += value; remove => yCoord.ValueChanged -= value; }
        public event EventHandler ZCoordChanged { add => zCoord.ValueChanged += value; remove => zCoord.ValueChanged -= value; }
        public event EventHandler WorldChanged { add => worldName.TextChanged += value; remove => worldName.TextChanged -= value; }
        public event EventHandler RadiusChanged { add => numericRadius.ValueChanged += value; remove => numericRadius.ValueChanged -= value; }

        public LocationField(bool _justHeight=false, bool _justWorld=false, bool _enabledRadius=true)
        {
            InitializeComponent();
            JustHeight = _justHeight || _justWorld;
            JustWorld = _justWorld;

            numericRadius.Enabled = _enabledRadius;

            if (JustHeight)
            {
                xCoord.Enabled = false;
                zCoord.Enabled = false;
            }
            if (JustWorld)
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
        public decimal Radius { get => numericRadius.Value; set => numericRadius.Value = value; }

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
            Radius = Decimal.Parse(reader.GetAttribute("Radius"));
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString("X", X.ToString());
            writer.WriteAttributeString("Y", Y.ToString());
            writer.WriteAttributeString("Z", Z.ToString());
            writer.WriteAttributeString("World", World);
            writer.WriteAttributeString("Radius", Radius.ToString());
        }

        public string GetYamlString_For_Objective()
        {
            string yaml = "";
            
            if (Radius != 0)
                yaml += String.Format(
                    "{0};{1};{2};{3} {4}",
                    X,
                    Y,
                    Z,
                    World,
                    Radius
                    );
            else
                yaml += String.Format(
                    "{0};{1};{2};{3}",
                    X,
                    Y,
                    Z,
                    World
                    );

            return yaml;
        }

        public string GetYamlString_For_Event()
        {
            return GetYamlString_For_Objective();
        }

        public string GetYamlString_For_Condition()
        {
            throw new NotImplementedException();
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
    }
}
