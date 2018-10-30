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
    public partial class NPCEditor : UserControl
    {
        private NPC _npc;
        private TreeNode _repNode;
        private TreeNode _subNodeId;
        private TreeNode _subNodeX;
        private TreeNode _subNodeY;
        private TreeNode _subNodeZ;
        private TreeNode _subNodeWorld;

        public NPC Npc { get => _npc; private set => _npc = value; }
        public TreeNode RepNode { get => _repNode; private set => _repNode = value; }
        private TreeNode SubNodeId { get => _subNodeId; set => _subNodeId = value; }
        private TreeNode SubNodeX { get => _subNodeX; set => _subNodeX = value; }
        private TreeNode SubNodeY { get => _subNodeY; set => _subNodeY = value; }
        private TreeNode SubNodeZ { get => _subNodeZ; set => _subNodeZ = value; }
        private TreeNode SubNodeWorld { get => _subNodeWorld; set => _subNodeWorld = value; }

        public NPCEditor(TreeNode _repNode, NPC _npc)
        {
            InitializeComponent();

            Npc = _npc;
            RepNode = _repNode;

            SubNodeId = new TreeNode();
            SubNodeX = new TreeNode();
            SubNodeY = new TreeNode();
            SubNodeZ = new TreeNode();
            SubNodeWorld = new TreeNode();

            RepNode.Nodes.AddRange(new TreeNode[] { SubNodeId, SubNodeX, SubNodeY, SubNodeZ, SubNodeWorld });

            StringValueField _sf = new StringValueField("Name");
            _sf.TextFieldChanged += (s, e) => 
            {
                if (_sf.TextField == Npc.Name)
                {
                    return;
                }
                try
                {
                    NPCManager.Rename(Npc, _sf.TextField);
                }
                catch (ArgumentException)
                {
                    MessageBox.Show("\"npc_\" suffix is used by the editor only! Pick a different name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                _sf.TextField = Npc.Name;
                RepNode.Text = Npc.Name;
            };
            _sf.TextField = Npc.Name;
            panel1.Controls.Add(_sf);

            AmountField _af = new AmountField("ID");
            _af.AmountChanged += (s, e) => { Npc.Id = (int)_af.Amount; SubNodeId.Text = "id: " + _af.Amount.ToString(); };
            _af.Amount = Npc.Id;
            panel3.Controls.Add(_af);

            LocationField _lf = new LocationField(false, false, false);
            _lf.XCoordChanged += (s, e) => { Npc.X = _lf.X; SubNodeX.Text = "x: " + _lf.X.ToString(); };
            _lf.YCoordChanged += (s, e) => { Npc.Y = _lf.Y; SubNodeY.Text = "y: " + _lf.Y.ToString(); };
            _lf.ZCoordChanged += (s, e) => { Npc.Z = _lf.Z; SubNodeZ.Text = "z: " + _lf.Z.ToString(); };
            _lf.WorldChanged += (s, e) => { Npc.World = _lf.World; SubNodeWorld.Text = "world: " + _lf.World; };
            _lf.X = Npc.X;
            _lf.Y = Npc.Y;
            _lf.Z = Npc.Z;
            _lf.World = Npc.World;
            panel2.Controls.Add(_lf);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Parent.Controls.Remove(this);
            Dispose();
        }
    }
}
