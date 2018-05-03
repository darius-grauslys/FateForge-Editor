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
    public partial class ControlListPanel : UserControl
    {
        private string _elementNameRoot = "";
        private int _elementCount = 1;

        public ControlListPanel()
        {
            InitializeComponent();
        }

        public ControlListPanel(string _elementNameRoot)
        {
            InitializeComponent();
            this._elementNameRoot = _elementNameRoot;
            Dock = DockStyle.Fill;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ControlListElement newElement = new ControlListElement();
            newElement.ElementTextbox.Text = _elementNameRoot + _elementCount;
            Controls.Remove(buttonAdd);
            flowLayoutPanel1.Controls.Add(newElement);
            flowLayoutPanel1.Controls.Add(buttonAdd);
            _elementCount++;
        }

        
    }
}
