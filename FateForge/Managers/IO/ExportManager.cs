using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;
using FateForge.DataTypes;
using System.Xml;
using static System.Windows.Forms.Control;

namespace FateForge.Managers.IO
{
    public static class ExportManager
    {
        public delegate void ExportDataFunction(StreamWriter s,  object o);

        private static XmlSerializer _serializer;
        private static XmlSerializer Serializer { get => _serializer; set => _serializer = value; }
        

        private static Dictionary<Type, ExportDataFunction> _exportDataFunctions = new Dictionary<Type, ExportDataFunction>()
        {
            { typeof(Item), ExportItemData }
        };

        #region ExportBetonStructure

        private static void ExportSerializiable(StreamWriter s, Control c)
        {
            XmlWriter xw = XmlWriter.Create(s);
            xw.WriteStartElement(c.Name);
            ((IXmlSerializable)c).WriteXml(xw);
            xw.WriteEndElement();
            xw.Flush();
            xw.Close();
        }
        
        #endregion

        #region ExportFateforgeData

        /// <summary>
        /// All functions responsible for each data piece in the editor. (Items, NPCs, Locations, Jornals, etc)
        /// </summary>
        public static Dictionary<Type, ExportDataFunction> ExportDataFunctions { get => _exportDataFunctions; set => _exportDataFunctions = value; }

        private static void ExportItemData( StreamWriter s, object i)
        {
            throw new NotFiniteNumberException();
        }

        #endregion

        public static void SerializeGenericBetonStructure(StreamWriter s, Control c)
        {
            if (c is IXmlSerializable)
            {
                ExportSerializiable(s, c);
            }
        }

        public static void ExportControlList(XmlWriter writer, ControlCollection list)
        {
            foreach (Control c in list)
            {
                if (c is IXmlSerializable)
                {
                    writer.WriteStartElement(c.Name);
                    (c as IXmlSerializable).WriteXml(writer);
                    writer.WriteEndElement();
                }
            }
        }
    }
}
