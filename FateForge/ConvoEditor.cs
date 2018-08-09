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
    public partial class ConvoEditor : UserControl, IIndependentResize
    {
        private TreeNode _parentNode;
        private TreeNode _representativeNode;
        private Form1 _formRef;

        public ConvoEditor(Form1 _formRef, TreeNode _parentNode, TreeNode _representativeNode)
        {
            InitializeComponent();

            this._formRef = _formRef;
            this._parentNode = _parentNode;
            this._representativeNode = _representativeNode;

            panel1.ControlAdded += (s, e) => CollapseManager.ResizeChilds(panel1);
            panel1.ControlRemoved += (s, e) => CollapseManager.ResizeChilds(panel1);

            Resize += (s, e) => CollapseManager.ResizeChilds(panel1);
        }
        
        public void IndependentCollapse(bool collapseState)
        {

        }

        public int GetDesiredSize()
        {
            return MinimumSize.Height;
        }

        public void IndependentResize()
        {
            if (Parent != null) CollapseManager.IndependentResize(this, panel1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TreeNode subNode = new TreeNode("option_" + _representativeNode.Nodes.Count);

            subNode.Tag = new ConvoEditor(_formRef, _representativeNode, subNode) { Dock = DockStyle.Fill };
            _representativeNode.Nodes.Add(subNode);

            Button _inspect = new Button() { Text = "Inspect Reply" };
            _inspect.Tag = subNode;
            _inspect.Click += _inspect_Click;
            Button _condEvent = new Button() { Text = "Attach Condition and Event" };
            StringValueField _sf = new StringValueField("Text");

            DeletionFieldContainer  _df = new DeletionFieldContainer(false, _sf, _inspect, _condEvent);
            _condEvent.Click += (s, ee) => { _condEvent_Click(_condEvent, _df); };
            _df.Deletion += (s, e1) => { _representativeNode.Nodes.Remove(subNode); };

            panel1.Controls.Add(_df);
            _df.IndependentResize();
        }

        /// <summary>
        /// Replace button with objectiveField. Then replace objField with button.
        /// 
        /// Changed to just reference a quest and objective name.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _condEvent_Click(object sender, DeletionFieldContainer field)
        {
            //ObjectiveField _objf = new ObjectiveField(true);
            StringValueField _sf1 = new StringValueField("Quest");
            StringValueField _sf2 = new StringValueField("Objective");

            DeletionFieldContainer _df = new DeletionFieldContainer(false, _sf1, _sf2);

            _df.Tag = sender;
            //_df.Resize += (s,e)=> { ((IIndependentResize)field).IndependentResize(); };
            field.RemoveControl((Control)sender);
            field.AddControl(_df);
            _df.Deletion += (s, e) => {
                field.AddControl((Control)sender);
            };
            CollapseManager.ResizeChilds(panel1);
        }

        /// <summary>
        /// Inspect response node. If none exists add one.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _inspect_Click(object sender, EventArgs e)
        {
            TreeNode subNode = (TreeNode)((Control)sender).Tag;
            
            _formRef.ChangeConvoNodeFocus(subNode);
        }
    }
}
