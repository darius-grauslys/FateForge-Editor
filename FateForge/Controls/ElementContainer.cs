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
    public partial class ElementContainer : UserControl
    {
        public ElementContainer()
        {
            InitializeComponent();
            SizeChanged += ElementContainer_SizeChanged;

            flowLayoutPanel1.HorizontalScroll.Maximum = 0;
            flowLayoutPanel1.AutoScroll = false;
            flowLayoutPanel1.VerticalScroll.Visible = false;
            flowLayoutPanel1.AutoScroll = true;
        }

        private void ElementContainer_SizeChanged(object sender, EventArgs e)
        {
            if (Width > Parent.Width - 8)
                Width = Parent.Width - 8;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Parent.Controls.Remove(this);
        }

        private void newElementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Add(new ElementContainer());
            UpdateSize();
        }

        public void UpdateSize()
        {
            Width = Parent.Width - 8;
            Height = (Parent.Height/((Parent.Controls.Count>flowLayoutPanel1.Controls.Count)
                ? Parent.Controls.Count-flowLayoutPanel1.Controls.Count

                : 1
                
                ))-8;
            //foreach (ElementContainer e in flowLayoutPanel1.Controls)
            //    e.UpdateSize();
        }
    }
}
