﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Avangarde.KeyboardExtender.Externals;
using Avangarde.KeyboardExtender.Project;

namespace Avangarde.KeyboardExtender
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            ProjectManager.Instance.RunMainLogic();
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            
        }
    }
}