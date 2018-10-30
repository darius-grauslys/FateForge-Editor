using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FateForge.Controls;
using FateForge.Managers;

namespace FateForge
{
    public partial class JournalEditor : UserControl
    {
        private TreeNode _representativeNode;
        private Form1 _formRef;

        public TreeNode RepresentativeNode { get => _representativeNode; set => _representativeNode = value; }
        public Form1 FormRef { get => _formRef; set => _formRef = value; }

        public string JournalCollectionName { get => label1.Text; set => label1.Text = value; }

        public JournalEditor(Form1 _formRef, TreeNode _representativeNode)
        {
            InitializeComponent();

            RepresentativeNode = _representativeNode;
            FormRef = _formRef;

            panel1.ControlAdded += (s, e) => CollapseManager.ResizeChilds(panel1);
            panel1.ControlRemoved += (s, e) => CollapseManager.ResizeChilds(panel1);
            panel1.Resize += (s, e) => CollapseManager.ResizeChilds(panel1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TreeNode subNode = new TreeNode("subEntry" + RepresentativeNode.Nodes.Count);
            
            RepresentativeNode.Nodes.Add(subNode);

            Button _btn = new Button() { Text = "Inspect Entry" };
            _btn.Click += (s, e1) => { FormRef.ChangeJournalNodeFocus(subNode); };

            DeletionFieldContainer _df = new DeletionFieldContainer(false, _btn);
            _df.Resize += (s, e1) => { _btn.Width = _df.Width - 40; };
            JournalEntry _entry = new JournalEntry() { Dock = DockStyle.Fill };
            subNode.Tag = _entry;
            _entry.EntryName = subNode.Text;
            _entry.EntryNameChanged += (s, e1) => { subNode.Text = _entry.EntryName; };
            _df.Deletion += (s, e1) => 
            {
                RepresentativeNode.Nodes.Remove(subNode);
            };

            panel1.Controls.Add(_df);
            _df.IndependentResize();
        }
    }
}
