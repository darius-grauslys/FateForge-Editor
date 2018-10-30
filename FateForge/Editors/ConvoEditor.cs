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

        public string QuesterName => comboBox1.Text;
        public bool StayStill => checkBox1.Checked;
        public ControlCollection FinalEvents => panelConvoFinalEvents.Controls;
        public string NPC_Text => textBox1.Text.Replace('\n', ' ');

        public event EventHandler ResponseNameChanged;

        public ConvoEditor(Form1 _formRef, TreeNode _parentNode, TreeNode _representativeNode)
        {
            InitializeComponent();

            this._formRef = _formRef;
            this._parentNode = _parentNode;
            this._representativeNode = _representativeNode;

            panel1.ControlAdded += (s, e) => CollapseManager.ResizeChilds(panel1);
            panel1.ControlRemoved += (s, e) => CollapseManager.ResizeChilds(panel1);
            panelConvoFinalEvents.ControlAdded += (s, e) => CollapseManager.ResizeChilds(panelConvoFinalEvents);
            panelConvoFinalEvents.ControlRemoved += (s, e) => CollapseManager.ResizeChilds(panelConvoFinalEvents);

            UpdateNPCPointers();
            NPCManager.NpcAdded += (n) => { UpdateNPCPointers(); };

            Resize += (s, e) => CollapseManager.ResizeChilds(panel1);
        }

        public void UpdateNPCPointers()
        {
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(NPCManager.NpcNames.ToArray());
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

        public List<ConvoPlayerResponse> GetResponses()
        {
            List<ConvoPlayerResponse> responses = new List<ConvoPlayerResponse>();

            foreach (DeletionFieldContainer df in panel1.Controls)
                responses.Add((ConvoPlayerResponse)df.Tag);

            return responses;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TreeNode subNode = new TreeNode("option_" + _representativeNode.Nodes.Count);
            ConvoPlayerResponse _response = new ConvoPlayerResponse("", "", "", subNode);

            textBox1.TextChanged += (s, e1) => { _response.ResponseUniqueName = textBox1.Text; };

            ConvoEditor _editor = new ConvoEditor(_formRef, _representativeNode, subNode) { Dock = DockStyle.Fill };
            _editor.tableLayoutPanel1.GetControlFromPosition(0,0).Controls.Clear();
            _editor.tableLayoutPanel1.RowStyles[0] = new RowStyle(SizeType.Absolute, 0);
            _representativeNode.Nodes.Add(subNode);

            subNode.Tag = _editor;

            Button _inspect = new Button() { Text = "Inspect Reply" };
            _inspect.Tag = subNode;
            _inspect.Click += _inspect_Click;
            Button _condEvent = new Button() { Text = "Attach Conditions and Events" };
            StringValueField _sf = new StringValueField("Text");

            _sf.ValueChanged += (s, e1) => { _response.PlayerResponse = _sf.TextField; };

            DeletionFieldContainer  _df = new DeletionFieldContainer(false, _sf, _inspect, _condEvent);
            _condEvent.Click += (s, ee) => { _condEvent_Click(_condEvent, _df, _response); };
            _df.Deletion += (s, e1) => { _representativeNode.Nodes.Remove(subNode); };

            _df.Tag = _response;

            panel1.Controls.Add(_df);
            _df.IndependentResize();

            _editor.Disposed += (s, e1) => { _df.ForceDeletion(); }; 
        }

        /// <summary>
        /// Replace button with objectiveField. Then replace objField with button.
        /// 
        /// Changed to just reference a quest and objective name.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _condEvent_Click(object sender, DeletionFieldContainer field, ConvoPlayerResponse _response)
        {
            //ObjectiveField _objf = new ObjectiveField(true);
            ComboField _sf2 = new ComboField("Objective");

            _sf2.SelectionChanged += (s, e) => { _response.ObjectiveFieldRef = _sf2.SelectedCombo; };

            _sf2.SetSelections(ObjectiveManager.ObjectiveNames);

            DeletionFieldContainer _df = new DeletionFieldContainer(false, _sf2);

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

        private void button2_Click(object sender, EventArgs e)
        {
            _formRef.RemoveConvo(_representativeNode);
            Parent.Controls.Remove(this);
            Dispose();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            EventField _ef = new EventField();
            _ef.Resize += (s, e1) => { CollapseManager.ResizeChilds(panelConvoFinalEvents); };
            panelConvoFinalEvents.Controls.Add(_ef);
        }
    }

    public class ConvoPlayerResponse
    {
        private string _objectiveFieldRef;
        private string _playerResponse;
        private string _responseUniqueName;
        private TreeNode _subNode;

        public string ObjectiveFieldRef { get => _objectiveFieldRef; set => _objectiveFieldRef = value; }
        public string PlayerResponse { get => _playerResponse; set => _playerResponse = value; }
        public string ResponseUniqueName { get => _responseUniqueName; set => _responseUniqueName = value; }
        public TreeNode SubNode { get => _subNode; set => _subNode = value; }

        public ConvoPlayerResponse(string _objectiveFieldRef, string _playerResponse, string _responseUniqueName, TreeNode _subNode)
        {
            ObjectiveFieldRef = _objectiveFieldRef;
            PlayerResponse = _playerResponse;
            ResponseUniqueName = _responseUniqueName;
            SubNode = _subNode;
        }
    }
}
