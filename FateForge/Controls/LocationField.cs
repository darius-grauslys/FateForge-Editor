using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FateForge
{
    public partial class LocationField : UserControl
    {
        private bool _justHeight;
        private bool _justWorld;

        public LocationField(bool _justHeight=false, bool _justWorld=false)
        {
            InitializeComponent();
            this._justHeight = _justHeight || _justWorld;
            this._justWorld = _justWorld;

            if (this._justHeight)
            {
                xCoord.Enabled = false;
                zCoord.Enabled = false;
            }
            if (_justWorld)
            {
                yCoord.Enabled = false;
            }
        }
    }
}
