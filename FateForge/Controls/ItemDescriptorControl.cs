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
using FateForge.DataTypes;

namespace FateForge
{
    /// <summary>
    /// Used in the Item Editor window. Describes items.
    /// Important to note this is for comparing items.
    /// </summary>
    public partial class ItemDescriptorControl : UserControl
    {
        private Item _item;

        private Panel _panel2 = new Panel();
        private Button _newEntryPanel2 = new Button();
        private Panel _panel3 = new Panel();
        private Button _newEntryPanel3 = new Button();

        private TableLayoutPanel _table_1 = new TableLayoutPanel();
        private TableLayoutPanel _table_1_left = new TableLayoutPanel();
        private TableLayoutPanel _table_1_right = new TableLayoutPanel();

        private StringValueField _sf_Name = new StringValueField("Item Name");
        private AmountField _af_Id = new AmountField("Id");
        private AmountField _af_Data = new AmountField("Data");

        public Item Item { get => _item; set => _item = value; }

        public ItemDescriptorControl(Item _item)
        {
            InitializeComponent();

            this._item = _item;

            #region REGION init
            panel1.ControlAdded += (s, o) => { CollapseManager.ResizeChilds(panel1); };

            _newEntryPanel2.Text = "Add Lore Line";
            _newEntryPanel2.Dock = DockStyle.Fill;
            _newEntryPanel3.Text = "Add Enchantment";
            _newEntryPanel3.Dock = DockStyle.Fill;

            _panel2.ForeColor = Color.FromKnownColor(KnownColor.ControlLightLight);
            _panel3.ForeColor = Color.FromKnownColor(KnownColor.ControlLightLight);

            _panel2.AutoScroll = true;
            _panel2.Dock = DockStyle.Fill;
            _panel3.AutoScroll = true;
            _panel3.Dock = DockStyle.Fill;

            _panel2.ControlAdded += (s, o) => { CollapseManager.ResizeChilds(_panel2); };
            _panel3.ControlAdded += (s, o) => { CollapseManager.ResizeChilds(_panel3); };
            _panel2.ControlRemoved += (s, o) => { CollapseManager.ResizeChilds(_panel2); };
            _panel3.ControlRemoved += (s, o) => { CollapseManager.ResizeChilds(_panel3); };

            _newEntryPanel2.Click += _newEntryPanel2_Click;
            _newEntryPanel3.Click += _newEntryPanel3_Click;



            _table_1.RowCount = 1;
            _table_1.ColumnCount = 2;

            _table_1.Width = panel1.Width - 30;
            _table_1.Height = panel1.Height - 70;
            _table_1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, ((_table_1.Width - 12) / 2)));
            _table_1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, ((_table_1.Width - 12) / 2)));
            _table_1.BackColor = Color.FromKnownColor(KnownColor.ControlDark);

            _table_1.Controls.Add(_table_1_left, 0, 0);
            _table_1.Controls.Add(_table_1_right, 1, 0);


            _table_1_left.RowCount = 2;
            _table_1_left.ColumnCount = 1;
            _table_1_left.Controls.Add(_newEntryPanel2, 0, 0);
            _table_1_left.Controls.Add(_panel2, 1, 0);
            _table_1_left.RowStyles.Add(new RowStyle(SizeType.Absolute, 42));
            _table_1_left.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            _table_1_left.Dock = DockStyle.Fill;

            _table_1_right.RowCount = 2;
            _table_1_right.ColumnCount = 1;
            _table_1_right.Controls.Add(_newEntryPanel3, 0, 0);
            _table_1_right.Controls.Add(_panel3, 1, 0);
            _table_1_right.RowStyles.Add(new RowStyle(SizeType.Absolute, 42));
            _table_1_right.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            _table_1_right.Dock = DockStyle.Fill;



            panel1.Controls.Add(_sf_Name);
            panel1.Controls.Add(_af_Id);
            panel1.Controls.Add(_af_Data);

            panel1.Controls.Add(_table_1);

            _sf_Name.TextFieldChanged += (s, o) => { _item.Name = _sf_Name.TextField; };
            _af_Id.AmountChanged += (s, o) => { _item.Id = (int)_af_Id.Amount; };
            _af_Data.AmountChanged += (s, o) => { _item.Data = (int)_af_Data.Amount; };
            #endregion

            Resize += ItemDescriptorControl_Resize;
        }

        private void ItemDescriptorControl_Resize(object sender, EventArgs e)
        {
            panel1.Width = Width - 8;
            panel1.Height = Height - 30;

            _table_1.Width = panel1.Width - 30;
            _table_1.Height = panel1.Height - 70;

            _table_1.ColumnStyles.Clear();

            if (!(Width < 12))
            {
                _table_1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, ((_table_1.Width - 12) / 2)));
                _table_1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, ((_table_1.Width - 12) / 2)));
            }
        }

        /// <summary>
        /// Add a new deletion field for enchants.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _newEntryPanel3_Click(object sender, EventArgs e)
        {
            ComboField _cf = new ComboField("Type", ComboBoxValues.ComboBoxDictionary["Enchantment Type"].ToArray());
            StringValueField _sf = new StringValueField("Val");

            DeletionFieldContainer _df = new DeletionFieldContainer(
                new List<Control>()
                {
                    _cf,
                    _sf
                }
                );

            _df.Resize += (s, o) =>
            {
                _cf.Width = _df.Width - 50;
                _sf.Width = _df.Width - 50;
            };

            _sf.TextFieldChanged += (s, a) => { _item.Enchants[_cf.SelectedCombo] = _sf.TextField; };

            _panel3.Controls.Add(_df);
        }

        /// <summary>
        /// Add a new deletion field for lore.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _newEntryPanel2_Click(object sender, EventArgs e)
        {
            StringValueField _sf = new StringValueField("Lore_" + _panel2.Controls.Count);

            DeletionFieldContainer _df = new DeletionFieldContainer(
                new List<Control>()
                {
                    _sf
                }
                );

            _df.Resize += (s, o) => 
            {
                _sf.Width = _df.Width - 50;
            };

            _panel2.Controls.Add(_df);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Parent.Controls.Remove(this);
        }
    }
}
