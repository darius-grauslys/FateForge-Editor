using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FateForge
{
    public partial class WindowedEditor : Form
    {
        public WindowedEditor(Control parent, Control nestedEditor)
        {
            InitializeComponent();

            this.panel1.Controls.Add(nestedEditor);
            this.Text = GetTitle(parent) + "->" + nestedEditor.Name;
        }

        private void WindowedEditor_Load(object sender, EventArgs e)
        {

        }

        private string GetTitle(Control c)
        {
            string title = "";

            if (c is WindowedEditor)
                return c.Text;
            else if (!(c is Panel) && c.Name[0] == c.Name.ToUpper()[0])
            {
                title = c.Name;
                if (c.Parent != null)
                    return GetTitle(c.Parent) + "->" + title;
                return title;
            }
            else
            {
                if (c.Parent != null)
                    return GetTitle(c.Parent);
                else
                    return title;
            }
        }
    }
}
