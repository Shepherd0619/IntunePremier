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

        public async Task<bool> ShowOpenMDMFileDiag()
        {
            try
            {
                openFileDialog1.ShowDialog(this);

                if (!File.Exists(openFileDialog1.FileName))
                    return false;

                if (analysisWindows.Values.Contains(openFileDialog1.FileName))
                    return false;

                var util = await CreateAndExtractMDMFileUtilAsync(openFileDialog1.FileName);
                await ShowMDMAnalysisWindowAsync(util, openFileDialog1.FileName);

                UpdateRecentDiagFiles(openFileDialog1.FileName);
                this.Hide();
                openFileDialog1.FileName = string.Empty;

                return true;
            }
            catch (Exception ex)
            {
                // Handle the exception appropriately (e.g., log it or show a message to the user)
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public async Task<bool> OpenMDMDiagByPath(string path)
        {
            try
            {
                if (!File.Exists(path))
                    return false;

                if (analysisWindows.Values.Contains(path))
                    return false;

                var util = await CreateAndExtractMDMFileUtilAsync(path);
                await ShowMDMAnalysisWindowAsync(util, path);

                UpdateRecentDiagFiles(path);

                this.Hide();

                return true;
            }
            catch (Exception ex)
            {
                // Handle the exception appropriately
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private async Task<MDMFileUtil> CreateAndExtractMDMFileUtilAsync(string filePath)
        {
            MDMFileUtil util;

            if (!CustomDiagUtil.IsThisACustomDiag(filePath))
            {
                util = new MDMFileUtil(filePath);
            }
            else
            {
                util = new CustomDiagUtil(filePath);
            }

            await util.Extract();
            return util;
        }

        private async Task ShowMDMAnalysisWindowAsync(MDMFileUtil util, string path)
        {
            var mainForm = new MDMAnalysisWindow
            {
                CurrentDiagFile = util,
                // Initialize other properties if necessary
            };
            mainForm.Show();

            analysisWindows.Add(mainForm, path);
        }

        private void UpdateRecentDiagFiles(string filePath)
        {
            if (!Program.Settings.RecentDiagFiles.Contains(filePath))
            {
                Program.Settings.RecentDiagFiles.Insert(0, filePath);
            }
            else
            {
                Program.Settings.RecentDiagFiles.Remove(filePath);
                Program.Settings.RecentDiagFiles.Insert(0, filePath);
            }
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

        private async void RecentMDMDiagList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var path = (string)RecentMDMDiagList.Items[RecentMDMDiagList.SelectedIndex];

            if (string.IsNullOrEmpty(path)) return;

            if (!await OpenMDMDiagByPath(path))
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
