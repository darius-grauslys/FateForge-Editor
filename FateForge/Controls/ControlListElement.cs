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
    public partial class ControlListElement : UserControl
    {

        public TextBox ElementTextbox { get
            {
                return textBox1;
            } }

        public ControlListElement()
        {
            InitializeComponent();

            this.Click += ControlListElement_Click;
        }

        private void ControlListElement_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            this.Parent.Controls.Remove(this);
        }
    }
}
