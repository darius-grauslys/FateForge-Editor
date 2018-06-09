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
    public partial class ConvoEditor : UserControl
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

        private void button1_Click(object sender, EventArgs e)
        {
            TreeNode subNode = new TreeNode("option_" + _representativeNode.Nodes.Count);

            subNode.Tag = new ConvoEditor(_formRef, _representativeNode, subNode);
            _representativeNode.Nodes.Add(subNode);

            Button _inspect = new Button() { Text = "Inspect Reply" };
            _inspect.Tag = subNode;
            _inspect.Click += _inspect_Click;
            Button _condEvent = new Button() { Text = "Attach Condition and Event" };
            StringValueField _sf = new StringValueField("Text");

            DeletionFieldContainer  _df = new DeletionFieldContainer(new List<Control>() {_sf, _inspect, _condEvent });
            _condEvent.Click += (s, ee) => { _condEvent_Click(_condEvent, _df); };

            panel1.Controls.Add(_df);
        }

        /// <summary>
        /// Replace button with objectiveField. Then replace objField with button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _condEvent_Click(object sender, DeletionFieldContainer field)
        {
            ObjectiveField _objf = new ObjectiveField(true);
            _objf.Tag = sender;
            _objf.Resize += (s,e)=> { ((IIndependentResize)field).IndependentResize(); };
            field.RemoveControl((Control)sender);
            field.AddControl(_objf);
            _objf.ParentChanged += (s, e) => {
                field.RemoveControl(_objf);
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
