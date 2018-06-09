using FateForge.Managers;
using FateForge.DataTypes;
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
    public partial class Form1 : Form
    {
        public static readonly int DEFAULT_NEST_SIZE = 485;

        private static ItemEditor _itemEditor = new ItemEditor();

        public static ItemEditor ItemEditor { get => _itemEditor; }

        private int _questCount = 0;
        private string _questTitleRoot = "Quest ";
        private QuestEditor _newEditor;
        private TreeNode _activeNode;

        public Form1()
        {
            InitializeComponent();
            NewPage();
            FieldUpdateManager.Initalize();

            panel1.ControlAdded += (s, e) => CollapseManager.ResizeChilds(panel1);
            panel1.ControlRemoved += (s, e) => CollapseManager.ResizeChilds(panel1);
            panelConvoEditor.ControlAdded += (s, e) => CollapseManager.ResizeChilds(panelConvoEditor);
            panelConvoEditor.ControlRemoved += (s, e) => CollapseManager.ResizeChilds(panelConvoEditor);

            treeView1.NodeMouseClick += (s, e) => ChangeConvoNodeFocus(e.Node);

            Resize += (s, e) => {
                CollapseManager.ResizeChilds(panelConvoEditor);
            };
        }

        private void newQuestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(0);
            NewPage();
        }

        private void NewPage()
        {
            _questCount++;
            _newEditor = new QuestEditor();
            _newEditor.Dock = DockStyle.Fill;
            TabPage newPage = new TabPage();
            newPage.BackColor = Color.Transparent;
            newPage.Text = _questTitleRoot + _questCount;
            newPage.Controls.Add(_newEditor);
            questControl.TabPages.Add(newPage);
        }

        private void itemEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ItemEditor.Show();
        }

        public void ChangeConvoNodeFocus(TreeNode node)
        {
            tabControl1.SelectTab(1);
            _activeNode = node;

            panelConvoEditor.Controls.Clear();
            panelConvoEditor.Controls.Add(((ConvoEditor)node.Tag));
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

        private void newItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(1);
        }

        private void newJournalEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(2);
        }

        private void newItemToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(3);
            button1_Click(this, null);
        }

        private void newCompassLocationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(4);
        }

        private void newMobToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(5);
        }

        private void newNPCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(6);
        }

        private void newConvoButton_Click(object sender, EventArgs e)
        {
            TreeNode node = new TreeNode("NewConvo" + treeView1.Nodes.Count);

            node.Tag = new ConvoEditor(this, null, node) { Dock = DockStyle.Fill };

            treeView1.Nodes.Add(node);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                ChangeConvoNodeFocus(_activeNode.Parent);
            }
            catch { }
        }
    }
}
