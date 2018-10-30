using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FateForge.Controls
{
    public partial class JournalEntry : UserControl
    {
        public string EntryName { get => textBox1.Text; internal set { textBox1.Text = value; EntryNameChanged?.Invoke(this, new EventArgs()); } }

        public event EventHandler EntryNameChanged;

        public JournalEntry()
        {
            InitializeComponent();
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            Parent.Controls.Remove(this);
            //TODO: Remove from reference list.
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            EntryNameChanged?.Invoke(sender, e);
        }
    }
}
