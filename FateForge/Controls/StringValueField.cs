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
    public partial class StringValueField : UserControl
    {
        /// <summary>
        /// Event Handler for when the string value text is changed.
        /// </summary>
        public EventHandler TextFieldChanged;
        /// <summary>
        /// The value of the string field.
        /// </summary>
        public string TextField { get => textBox1.Text; }

        public StringValueField(string _valueName="String Val")
        {
            InitializeComponent();

            label1.Text = _valueName;

            textBox1.TextChanged += (s, a) => 
            {
                TextFieldChanged?.Invoke(s, a);
            };
        }
    }
}
