using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FateForge
{
    public partial class ComboField : UserControl
    {
        /// <summary>
        /// The combo selected by the user.
        /// </summary>
        public string SelectedCombo { get => comboBox1.Text; }

        public ComboField(string _valueName = "Type", object[] _items = null)
        {
            InitializeComponent();

            if (_items == null)
                _items = new object[0];

            label1.Text = _valueName;
            comboBox1.Items.AddRange(_items);
        }
    }
}
