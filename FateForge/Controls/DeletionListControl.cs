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
using FateForge.DataTypes;

namespace FateForge
{
    public partial class DeletionListControl : UserControl, IXmlSerializable, IIndependentResize, IReferenceTableEntry
    {
        public delegate DeletionFieldContainer NewItemHandler();
        public NewItemHandler NewItemHandle;

        public ControlCollection DeletionNodes { get => panel1.Controls; }

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

            DeletionFieldContainer _df = NewItemHandle();

            Resize += (s, e1) => { _df.IndependentResize(); };

            panel1.Controls.Add(_df);
        }

        public void IndependentResize()
        {
            CollapseManager.IndependentResize(this, panel1);
        }

        public void IndependentCollapse(bool collapseState)
        {
            List<IIndependentResize> resizers = CollapseManager.ScanForResizers(panel1.Controls);
            CollapseManager.IndependentCollapse(collapseState, this, new List<Panel> { panel1 }, 36, resizers.ToArray());
        }

        public int GetDesiredSize()
        {
            return Parent.Width - 10;
        }

        public void Reference()
        {
            foreach (IReferenceTableEntry refe in panel1.Controls)
                refe.Reference();
        }
    }
}
