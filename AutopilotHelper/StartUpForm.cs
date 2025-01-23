using AutopilotHelper.Utilities;
using DarkModeForms;
using System.Diagnostics;

namespace AutopilotHelper
{
    public partial class StartUpForm : Form
    {
        public static StartUpForm Instance => instance;
        private static StartUpForm instance;

        public readonly Dictionary<MDMAnalysisWindow, string> analysisWindows = new();

        public StartUpForm()
        {
            InitializeComponent();
            var dm = new DarkModeCS(this)
            {
                ColorMode = DarkModeCS.DisplayMode.SystemDefault
            };
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            instance = this;
            RecentMDMDiagList.Items.Clear();
            for (int i = 0; i < Program.Settings.RecentDiagFiles.Count; i++)
            {
                RecentMDMDiagList.Items.Add(Program.Settings.RecentDiagFiles[i]);
            }
        }

        private void OpenMDMDiagButton_Click(object sender, EventArgs e)
        {
            ShowOpenMDMFileDiag();
        }

        public bool ShowOpenMDMFileDiag()
        {
            openFileDialog1.ShowDialog(this);

            if (!File.Exists(openFileDialog1.FileName)) return false;

            if (analysisWindows.Values.Contains(openFileDialog1.FileName)) return false;

            MDMFileUtil util;
            try
            {
                util = new(openFileDialog1.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to extract the file!\n\n{ex}", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            var mainForm = new MDMAnalysisWindow();

            mainForm.CurrentDiagFile = util;
            mainForm.Show();
            analysisWindows.Add(mainForm, openFileDialog1.FileName);

            if (!Program.Settings.RecentDiagFiles.Contains(openFileDialog1.FileName))
                Program.Settings.RecentDiagFiles.Add(openFileDialog1.FileName);
            else
            {
                Program.Settings.RecentDiagFiles.Remove(openFileDialog1.FileName);
                Program.Settings.RecentDiagFiles.Insert(0, openFileDialog1.FileName);
            }

            this.Hide();
            openFileDialog1.FileName = string.Empty;

            return true;
        }

        public bool OpenMDMDiagByPath(string path)
        {
            if (!File.Exists(path)) return false;

            if (analysisWindows.Values.Contains(path)) return false;

            MDMFileUtil util;
            try
            {
                util = new(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to extract the file!\n\n{ex}", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            var mainForm = new MDMAnalysisWindow();

            mainForm.CurrentDiagFile = util;
            mainForm.Show();
            analysisWindows.Add(mainForm, path);

            if (!Program.Settings.RecentDiagFiles.Contains(path))
                Program.Settings.RecentDiagFiles.Add(path);
            else
            {
                Program.Settings.RecentDiagFiles.Remove(path);
                Program.Settings.RecentDiagFiles.Insert(0, path);
            }

            this.Hide();

            return true;
        }

        private void AboutMeButton_Click(object sender, EventArgs e)
        {
            var about = new AboutBox();
            about.ShowDialog();
        }

        private void StartUpForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            instance = null;
        }

        private void ClearRecentDiagListBtn_Click(object sender, EventArgs e)
        {
            Program.Settings.RecentDiagFiles.Clear();
            RecentMDMDiagList.Items.Clear();
        }

        private void RecentMDMDiagList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var path = (string)RecentMDMDiagList.Items[RecentMDMDiagList.SelectedIndex];

            if (string.IsNullOrEmpty(path)) return;

            if (!OpenMDMDiagByPath(path))
            {
                MessageBox.Show($"File no longer exists.\n\n{path}", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                RecentMDMDiagList.Items.RemoveAt(RecentMDMDiagList.SelectedIndex);
                Program.Settings.RecentDiagFiles.Remove(path);
            }
        }

        private void CollectMDMDiagButton_Click(object sender, EventArgs e)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "mdmdiagnosticstool.exe";
            startInfo.Arguments = "-area \"DeviceEnrollment;DeviceProvisioning;Autopilot\" -zip \"c:\\users\\public\\documents\\MDMDiagReport.zip\"";
            startInfo.UseShellExecute = true;

            Process process = Process.Start(startInfo);
            process.WaitForExit();

            if (File.Exists("c:\\users\\public\\documents\\MDMDiagReport.zip"))
            {
                startInfo = new ProcessStartInfo();
                startInfo.FileName = "c:\\users\\public\\documents\\";
                startInfo.UseShellExecute = true;
                process = Process.Start(startInfo);
            }
            else
            {
                MessageBox.Show("MDMDiagReport collect failed!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
