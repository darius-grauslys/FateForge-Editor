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
    public partial class ObjectiveField : UserControl
    {
        public ObjectiveField()
        {
            InitializeComponent();

            Resize += ObjectiveField_Resize;
            panel2.ControlRemoved += (s, o) => { PanelResize(panel2); };
            panel2.ControlAdded += (s, o) => { PanelResize(panel2); };
            panel3.ControlRemoved += (s, o) => { PanelResize(panel3); };
            panel3.ControlAdded += (s, o) => { PanelResize(panel3); };
            panel4.ControlRemoved += (s, o) => { PanelResize(panel4, 20); };
            panel4.ControlAdded += (s, o) => { PanelResize(panel4, 20); };
        }

        private void PanelResize(Panel panel, int offset=12)
        {
            CollapseManager.ResizeChilds(panel, offset);
        }

        private void ObjectiveField_Resize(object sender, EventArgs e)
        {
            //CollapseManager.IndependentResize(this, 12, panel2, panel3, panel4);

            CollapseManager.ResizeChilds(panel2);
            CollapseManager.ResizeChilds(panel3);
            CollapseManager.ResizeChilds(panel4, 20);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "-")
            {
                button2.Text = "o";
                tableLayoutPanel1.RowStyles[1] = new RowStyle(SizeType.Percent, 0);
                Height = 46;
                tableLayoutPanel2.RowStyles[1] = new RowStyle(SizeType.Percent, 0);
            }
            else
            {
                button2.Text = "-";
                tableLayoutPanel1.RowStyles[1] = new RowStyle(SizeType.Percent, 31.97f);
                Height = (int)(Form1.DEFAULT_NEST_SIZE * 2.5);
                tableLayoutPanel2.RowStyles[1] = new RowStyle(SizeType.Percent, 100);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (button6.Text == "-")
            {
                button6.Text = "o";
                tableLayoutPanel8.ColumnStyles[0].Width = 40;
                tableLayoutPanel3.ColumnStyles[0] = new ColumnStyle(SizeType.Absolute, 80);
            }
            else
            {
                button6.Text = "-";
                tableLayoutPanel8.ColumnStyles[0].Width = 125;
                tableLayoutPanel3.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 28.57f);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (button4.Text == "-")
            {
                button4.Text = "o";
                tableLayoutPanel5.RowStyles[1] = new RowStyle(SizeType.Absolute, 0);
            }
            else
            {
                button4.Text = "-";
                tableLayoutPanel5.RowStyles[1] = new RowStyle(SizeType.Percent, 100);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            EventField eventField = new EventField();

            //if (panel3.Height < 250)
            //{
            //    Button escapeButton = new Button();
            //    escapeButton.Text = ("[Open in New Window]");
            //    escapeButton.Dock = DockStyle.Fill;
            //    eventField.Dock = DockStyle.Fill;
            //    escapeButton.Tag = eventField;
            //    escapeButton.Click += EscapeButton_Click;
            //    panel3.Controls.Add(escapeButton);
            //    return;
            //}
            
            eventField.Size = new Size(panel3.Width-12, panel3.Height);

            int positionY = 0;
            foreach (Control c in panel3.Controls)
                positionY += c.Height + 2 - panel3.VerticalScroll.Value;

            eventField.Location = new Point(2, positionY);
            eventField.Resize += ObjectiveField_Resize;
            panel3.Controls.Add(eventField);
        }

        private void EscapeButton_Click(object sender, EventArgs e)
        {
            if (!(sender is Control))
                return;
            WindowedEditor escapeEditor = new WindowedEditor(((Control)sender).Parent, (Control)((Control)sender).Tag);
            escapeEditor.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (button3.Text == "-")
            {
                button3.Text = "o";
                tableLayoutPanel10.RowStyles[1].Height = 0;
                tableLayoutPanel1.RowStyles[1] = new RowStyle(SizeType.Absolute, 42);
            }
            else
            {
                button3.Text = "-";
                tableLayoutPanel10.RowStyles[1].Height = 100;
                tableLayoutPanel1.RowStyles[1] = new RowStyle(SizeType.Percent, 31.97f);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Parent.Controls.Remove(this);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ConditionField conditionField = new ConditionField();

            conditionField.Size = new Size(panel4.Width - 20, Form1.DEFAULT_NEST_SIZE/4);

            int positionY = 0;
            foreach (Control c in panel4.Controls)
                positionY += c.Height + 2 - panel4.VerticalScroll.Value;

            conditionField.Location = new Point(2, positionY);
            conditionField.Resize += ObjectiveField_Resize;
            panel4.Controls.Add(conditionField);
        }
    }
}
