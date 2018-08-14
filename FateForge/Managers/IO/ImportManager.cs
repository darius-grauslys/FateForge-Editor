using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace FateForge.Managers.IO
{
    public static class ImportManager
    {
        public delegate Control ImportFunction(Control parent, XmlReader r);

        private static Dictionary<string, ImportFunction> _importFunctions = new Dictionary<string, ImportFunction>()
        {
            { "AmountField", ImportAmountField },
            { "ComboField", ImportComboField },
            { "ConditionField", ImportConditionField },
            { "DeletionFieldContainer", ImportDeletionField },
            { "ControlListPanel", ImportList },
            { "EventField", ImportEventField },
            { "ItemField", ImportItemField },
            { "LocationField", ImportLocationField },
            { "ObjectiveField", ImportObjectiveField },
            { "QuestEditor", ImportQuest },
            { "StringValueField", ImportStringValueField }
        };

        /// <summary>
        /// All functions responsible for each editor control.
        /// </summary>
        private static Dictionary<string, ImportFunction> ImportFunctions { get => _importFunctions; set => _importFunctions = value; }

        private static Control ImportAmountField(Control parent, XmlReader r)
        {
            AmountField _af = new AmountField();
            _af.Parent = parent;
            _af.ReadXml(r);
            return _af;
        }

        private static Control ImportComboField(Control parent, XmlReader r)
        {
            ComboField _cf = new ComboField();
            _cf.Parent = parent;
            _cf.ReadXml(r);
            return _cf;
        }

        private static Control ImportConditionField(Control parent, XmlReader r)
        {
            ConditionField _cf = new ConditionField();
            _cf.Parent = parent;
            _cf.ReadXml(r);
            return _cf;
        }

        private static Control ImportDeletionField(Control parent, XmlReader r)
        {
            DeletionFieldContainer _df = new DeletionFieldContainer();
            _df.Parent = parent;
            _df.ReadXml(r);
            return _df;
        }

        private static Control ImportList(Control parent, XmlReader r)
        {
            DeletionListControl _dlc = new DeletionListControl();
            _dlc.Parent = parent;
            _dlc.ReadXml(r);
            return _dlc;
        }

        private static Control ImportEventField(Control parent, XmlReader r)
        {
            EventField _ef = new EventField();
            _ef.Parent = parent;
            _ef.ReadXml(r);
            return _ef;
        }

        private static Control ImportItemField(Control parent, XmlReader r)
        {
            ItemField _if = new ItemField();
            _if.Parent = parent;
            _if.ReadXml(r);
            return _if;
        }

        private static Control ImportLocationField(Control parent, XmlReader r)
        {
            LocationField _lf = new LocationField();
            _lf.Parent = parent;
            _lf.ReadXml(r);
            return _lf;
        }

        private static Control ImportObjectiveField(Control parent, XmlReader r)
        {
            ObjectiveField _of = new ObjectiveField();
            _of.Parent = parent;
            _of.ReadXml(r);
            return _of;
        }

        private static Control ImportQuest(Control parent, XmlReader r)
        {
            QuestEditor _qe = new QuestEditor();
            _qe.Parent = parent;
            _qe.ReadXml(r);
            return _qe;
        }

        private static Control ImportStringValueField(Control parent, XmlReader r)
        {
            StringValueField _sf = new StringValueField();
            _sf.Parent = parent;
            _sf.ReadXml(r);
            return _sf;
        }
        
        public static Control GetControlFromXML(Control parent, string XmlElementName, XmlReader r)
        {
            return ImportFunctions[XmlElementName](parent, r);
        }

        public static List<Control> GetControlListFromXml(Control parent, XmlReader r)
        {
            r.MoveToContent();
            List<Control> controls = new List<Control>();
            while (r.NodeType != XmlNodeType.EndElement)
            {
                r.ReadStartElement();
                if (r.NodeType == XmlNodeType.Element)
                    controls.Add(GetControlFromXML(parent, r.Name, r.ReadSubtree()));
                //else
                //    controls.Add(GetControlFromXML(parent, r.Name, r
            }
            r.ReadEndElement();
            return controls;
        }

        public static List<QuestEditor> DeserializeGenericBetonStructure(Control parent, StreamReader s)
        {
            List<QuestEditor> _qes = new List<QuestEditor>();
            XmlReader xr = XmlReader.Create(s);
            Control c;
            xr.MoveToContent();
            while (xr.NodeType != XmlNodeType.EndElement)
            {
                if (xr.Name == "xml")
                    continue;
                c = ImportFunctions[xr.Name](parent, xr);
                if (c is QuestEditor)
                    _qes.Add(c as QuestEditor);
            }
            xr.Close();
            return _qes;
        }
    }
}
