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
using FateForge.Managers.IO;
using System.IO;

namespace FateForge
{
    public partial class Form1 : Form
    {
        public static readonly int DEFAULT_NEST_SIZE = 485;

        private static ItemEditor _itemEditor = new ItemEditor();
        private static SaveDialog _saveDialog = new SaveDialog();

        public static ItemEditor ItemEditor { get => _itemEditor; }
        private static SaveDialog SaveDialog { get => _saveDialog; set => _saveDialog = value; }


        private QuestEditor _activeEditor;

        public QuestEditor ActiveEditor { get => _activeEditor; set => _activeEditor = value; }

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

            activeNodeTextboxName.TextChanged += (s, e) =>
            {
                if (_activeNode != null)
                {
                    _activeNode.Text = activeNodeTextboxName.Text;
                }
            };

            treeView1.NodeMouseClick += (s, e) => ChangeConvoNodeFocus(e.Node);

            Resize += (s, e) => {
                CollapseManager.ResizeChilds(panelConvoEditor);
            };

            questControl.TabIndexChanged += QuestControl_TabIndexChanged;
        }

        private void QuestControl_TabIndexChanged(object sender, EventArgs e)
        {
            ActiveEditor = (QuestEditor)questControl.SelectedTab.Controls[0];
        }

        private void newQuestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(0);
            NewPage();
        }

        public void SwitchTab(TabPageEnum type)
        {
            tabControl1.SelectTab((int)type);
        }

        private void NewPage()
        {
            _newEditor = new QuestEditor();
            ActiveEditor = _newEditor;
            _newEditor.Dock = DockStyle.Fill;
            TabPage newPage = new TabPage();
            newPage.BackColor = Color.Transparent;
            newPage.Text = _newEditor.QuestName;
            newPage.Controls.Add(_newEditor);
            questControl.TabPages.Add(newPage);

            SaveDialog.UpdateQuestMenu();
        }

        private void NewPage(QuestEditor _q)
        {
            ActiveEditor = _q;
            _q.Dock = DockStyle.Fill;
            TabPage newPage = new TabPage();
            newPage.BackColor = Color.Transparent;
            newPage.Text = _q.QuestName;
            newPage.Controls.Add(_q);
            questControl.TabPages.Add(newPage);

            SaveDialog.UpdateQuestMenu();
        }

        private void itemEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(3);
        }

        public void ChangeConvoNodeFocus(TreeNode node)
        {
            tabControl1.SelectTab(1);
            _activeNode = node;

            activeNodeTextboxName.Text = _activeNode.Text;

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

            DeletionFieldContainer _df = new DeletionFieldContainer(true, _itemLabel);

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

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog _diag = new SaveFileDialog();
            _diag.DefaultExt = "xml";
            _diag.ShowDialog();
            if (Path.GetExtension(_diag.FileName) != ".xml")
            {
                MessageBox.Show("Selected file is not Xml!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ExportManager.SerializeGenericBetonStructure(new StreamWriter(_diag.FileName), ActiveEditor);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            SaveDialog.Show();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenFileDialog _diag = new OpenFileDialog();
            _diag.ShowDialog();
            if (Path.GetExtension(_diag.FileName) != ".xml")
            {
                MessageBox.Show("Selected file is not Xml!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            NewPage(ImportManager.DeserializeGenericBetonStructure(questControl.SelectedTab, new StreamReader(_diag.FileName))[0]);
        }
    }
}
