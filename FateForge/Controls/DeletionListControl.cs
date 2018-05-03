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

namespace FateForge
{
    public partial class DeletionListControl : UserControl
    {
        private List<Type> _subFields = new List<Type>();
        private List<List<object>> _args = new List<List<object>>();
        private bool _checkAllClicks;

        /// <summary>
        /// A dynamic list of deletion controls.
        /// </summary>
        /// <param name="_subFields">The controls for each deletion field.</param>
        /// <param name="_args">The arguments for the dynamic controls.</param>
        /// <param name="_checkAllClicks"></param>
        public DeletionListControl(List<Type> _subFields, List<List<object>> _args, bool _checkAllClicks = false)
        {
            InitializeComponent();
            this._checkAllClicks = _checkAllClicks;
            panel1.ControlAdded += (s, o) => { CollapseManager.ResizeChilds(panel1); };
            panel1.ControlRemoved += (s, o) => { CollapseManager.ResizeChilds(panel1); };
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<Control> _controls = new List<Control>();
            for (int i = 0; i < _subFields.Count; i++)
            {
                Control child = (Control)Activator.CreateInstance(_subFields[i], _args[i].ToArray());
                _controls.Add(child);
            }

            DeletionFieldContainer _df = new DeletionFieldContainer(_controls, _checkAllClicks);

            panel1.Controls.Add(_df);
        }
    }
}
