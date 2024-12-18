﻿using AutopilotHelper.Models;
using AutopilotHelper.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutopilotHelper
{
    public partial class MDMAnalysisWindow : Form
    {
        public MDMFileUtil? CurrentDiagFile
        {
            get
            {
                return _diagFile;
            }

            set
            {
                _diagFile = value;

                if (value == null) return;

                _autopilotUtil = new(value);
            }
        }
        private MDMFileUtil? _diagFile;

        public AutopilotUtil AutopilotUtil => _autopilotUtil;
        private AutopilotUtil _autopilotUtil;

        public MDMAnalysisWindow()
        {
            InitializeComponent();
        }

        private void MDMAnalysisWindow_Load(object sender, EventArgs e)
        {
            AutopilotProfileStatusTextBox.Text = _autopilotUtil.GetLocalAutopilotProfileStatus()?.ToString()
                .Replace("\n", Environment.NewLine);
            _autopilotUtil.GetCloudSessionHostRecords();
        }

        private void MDMAnalysisWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
