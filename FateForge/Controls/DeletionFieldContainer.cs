﻿using System;
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
    public partial class DeletionFieldContainer : UserControl
    {
        public EventHandler Deletion;
        public EventHandler Clicked;

        private bool _testAllClicks = false;

        public DeletionFieldContainer(List<Control> _addedControls, bool _testAllClicks = false)
        {
            InitializeComponent();

            this._testAllClicks = _testAllClicks;

            panel1.ControlAdded += Panel1_ControlAdded;
            foreach (Control c in _addedControls)
            {
                panel1.Controls.Add(c);
                if (_testAllClicks)
                    c.Click += (s, o) => { Clicked(s, o); };

            }

            if (_testAllClicks)
            {
                panel1.Click += (s, o) => { Clicked?.Invoke(s, o); };
                Click += (s, o) => { Clicked?.Invoke(s, o); };
            }
        }

        private void Panel1_ControlAdded(object sender, ControlEventArgs e)
        {
            int _newHeight = 0;
            foreach (Control c in panel1.Controls)
            {
                c.Location = new Point(0, _newHeight);
                _newHeight += c.Height + 4;
            }

            Size = new Size(216, _newHeight);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Deletion?.Invoke(this, new EventArgs());
            Parent.Controls.Remove(this);
        }
    }
}