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
    public partial class AmountField : UserControl
    {
        public EventHandler AmountChanged;
        public decimal Amount { get => numericUpDown1.Value; }

        public AmountField(string _valueName="Amount")
        {
            InitializeComponent();

            label1.Text = _valueName;

            numericUpDown1.ValueChanged += (s, o) => { AmountChanged?.Invoke(s, o); };
        }
    }
}
