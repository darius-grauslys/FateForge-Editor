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
    public partial class ItemField : UserControl
    {
        public ItemField()
        {
            InitializeComponent();

            tableLayoutPanel2.SetRow(new AmountField(),1);
            ItemManager.UpdatedList += (s, o) =>
            {
                comboBox1.Items.Clear();
                comboBox1.Items.AddRange(ItemManager.GetComboValues().ToArray());
            };
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1.ItemEditor.Show();
        }
    }
}
