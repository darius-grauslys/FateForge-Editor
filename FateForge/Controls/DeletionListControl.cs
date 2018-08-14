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
using FateForge.Managers.IO;

namespace FateForge
{
    public partial class DeletionListControl : UserControl, IXmlSerializable
    {
        private List<Type> _subFields = new List<Type>();
        private List<List<object>> _args = new List<List<object>>();
        private bool _checkAllClicks;

        /// <summary>
        /// A dynamic list of deletion controls.
        /// </summary>
        /// <param name="_args">The arguments for the dynamic controls.</param>
        /// <param name="_checkAllClicks"></param>
        public DeletionListControl(bool _checkAllClicks = false)
        {
            InitializeComponent();
            this._checkAllClicks = _checkAllClicks;
            panel1.ControlAdded += (s, o) => { CollapseManager.ResizeChilds(panel1); };
            panel1.ControlRemoved += (s, o) => { CollapseManager.ResizeChilds(panel1); };
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            throw new NotImplementedException();
        }

        public void WriteXml(XmlWriter writer)
        {
            ExportManager.ExportControlList(writer, panel1.Controls);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<Control> _controls = new List<Control>();
            for (int i = 0; i < _subFields.Count; i++)
            {
                Control child = (Control)Activator.CreateInstance(_subFields[i], _args[i].ToArray());
                _controls.Add(child);
            }

            DeletionFieldContainer _df = new DeletionFieldContainer(_checkAllClicks, _controls.ToArray());

            panel1.Controls.Add(_df);
        }
    }
}
