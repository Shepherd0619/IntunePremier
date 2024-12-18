using AutopilotHelper.Models;
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
        public MDMFileUtil? CurrentDiagFile;

        public MDMAnalysisWindow()
        {
            InitializeComponent();
        }

        private void MDMAnalysisWindow_Load(object sender, EventArgs e)
        {
            if (CurrentDiagFile != null)
            {
                EventViewerFile file = new(Path.Combine(CurrentDiagFile.TmpWorkplacePath, "microsoft-windows-shell-core-operational.evtx"));
            }
        }

        private void MDMAnalysisWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
