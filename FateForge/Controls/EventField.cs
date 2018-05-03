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
    public partial class EventField : UserControl, IIndependentResize
    {
        private bool _isMin = false;
        private Size _initSize = new Size(0,0);

        public EventField()
        {
            InitializeComponent();

            _initSize = MinimumSize;

            comboBox1.TextChanged += ComboBox1_SelectedValueChanged;
            panel1.ControlAdded += (s, o) => { CollapseManager.ResizeChilds(panel1); };
            panel1.ControlRemoved += (s, o) => { CollapseManager.ResizeChilds(panel1); };
        }

        private void ComboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                EventFieldManager.FieldHandlers[comboBox1.Text](panel1);
                IndependentResize();
            }
            catch
            { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _isMin = !_isMin;

            if (button1.Text == "-")
            {
                button1.Text = "o";
                MinimumSize = new Size(Width, 36);
                Height = 36;
            }
            else
            {
                button1.Text = "-";
                MinimumSize = _initSize;
                IndependentResize();
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Parent.Controls.Remove(this);
        }

        public void IndependentResize()
        {
            int height = 40;

            if (!_isMin)
            {
                CollapseManager.WrapIndependentSize(this, panel1);
            }

            //if (!_isMin)
            //    CollapseManager.IndependentResize(this, panel1);
        }
    }
}
