using FateForge.Managers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FateForge.DataTypes;

namespace FateForge
{
    public partial class ItemEditor : Form
    {
        public ItemEditor()
        {
            InitializeComponent();

            FormClosing += ItemEditor_FormClosing;
            panel1.ControlAdded += (s, o) => { CollapseManager.ResizeChilds(panel1); };
            panel1.ControlRemoved += (s, o) => { CollapseManager.ResizeChilds(panel1); };
        }

        /// <summary>
        /// Prevent the form from disposing on close.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Item _item = new Item(0, 0, "NewItem" + (ItemManager.Count + 1));
            Label _itemLabel = new Label() { Text = _item.Name };
            
            ItemManager.AddItem(_item);

            ItemDescriptorControl _idc = new ItemDescriptorControl(_item);
            _idc.Dock = DockStyle.Fill;

            DeletionFieldContainer _df = new DeletionFieldContainer(new List<Control>() { _itemLabel }, true);

            _df.Tag = _idc;
            _item.FieldsUpdated += (s, o) => 
            {
                _itemLabel.Text = _item.Name;
            };

            _df.Clicked += (s, o) => { panel2.Controls.Clear(); panel2.Controls.Add((ItemDescriptorControl)_df.Tag); };
            _df.Deletion += (s, o) => { ItemManager.RemoveItem(((ItemDescriptorControl)_df.Tag).Item); };

            panel1.Controls.Add(_df);
        }

        private void ItemEditor_Load(object sender, EventArgs e)
        {

        }
    }
}
