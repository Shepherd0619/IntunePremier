using AutopilotHelper.Models;
using AutopilotHelper.Utilities;
using Newtonsoft.Json;
using System.Diagnostics;

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

        private async void MDMAnalysisWindow_Load(object sender, EventArgs e)
        {
            var autopilotProfile = _autopilotUtil.GetLocalAutopilotProfileStatus();
            if (autopilotProfile == null)
            {
                AutopilotProfileStatusTextBox.Text = "ERROR: Autopilot Profile is missing!";
            }
            else
            {
                AutopilotProfileStatusTextBox.Text = _autopilotUtil.GetLocalAutopilotProfileStatus()?.ToString()
                    .Replace("\n", Environment.NewLine);
            }
            _autopilotUtil.GetCloudSessionHostRecords();

            try
            {
                InitializeESPTabPage(_autopilotUtil.GetAutopilotSettingsFromRegistry());
            }
            catch(Exception ex)
            {
                // Do nothing
            }

            autopilotDiagTextBox1.Text = _autopilotUtil.GetGeneralDiagnosticsReport();

            await ProcessedPoliciesWebView.EnsureCoreWebView2Async();

            ProcessedPoliciesWebView.NavigateToString(_autopilotUtil.GetHtmlFormattedProcessedPolicies());

            await ProcessedAppsWebView.EnsureCoreWebView2Async();
        }

        public void InitializeESPTabPage(AutopilotSettings settings)
        {
            devicePreparationCheckedListBox.Items.Clear();
            deviceSetupCheckedListBox.Items.Clear();
            accountSetupCheckedListBox.Items.Clear();

            try
            {
                var esp = _autopilotUtil.GetAutopilotSettingsFromRegistry();

                try
                {
                    var devicePreparation = JsonConvert.DeserializeObject<DevicePreparationCategory>(esp.DevicePreparation.Status);
                    foreach (var item in devicePreparation.GetType().GetProperties())
                    {
                        if (item.PropertyType != typeof(Subcategory)) continue;

                        var category = item.GetValue(devicePreparation) as Subcategory;

                        devicePreparationCheckedListBox.Items.Add(item.Name, 
                            category.SubcategoryState == "succeeded");
                    }
                }
                catch (Exception ex)
                {
                    // Do nothing
                }

                try
                {
                    var deviceSetup = JsonConvert.DeserializeObject<DeviceSetupCategory>(esp.DeviceSetup.Status);
                    foreach (var item in deviceSetup.GetType().GetProperties())
                    {
                        if (item.PropertyType != typeof(Subcategory)) continue;

                        var category = item.GetValue(deviceSetup) as Subcategory;

                        deviceSetupCheckedListBox.Items.Add($"{item.Name} - {category.SubcategoryStatusText}",
                            category.SubcategoryState == "succeeded");
                    }
                }
                catch (Exception ex)
                {
                    // Do nothing
                }

                try
                {
                    var accountSetup = JsonConvert.DeserializeObject<AccountSetupCategory>(esp.AccountSetup.Status);
                    foreach (var item in accountSetup.GetType().GetProperties())
                    {
                        if (item.PropertyType != typeof(Subcategory)) continue;

                        var category = item.GetValue(accountSetup) as Subcategory;

                        accountSetupCheckedListBox.Items.Add(item.Name,
                            category.SubcategoryState == "succeeded");
                    }
                }
                catch (Exception ex)
                {
                    // Do nothing
                }

                devicePreparationCheckedListBox.ItemCheck += ESPTabPageCheckedListBox_ItemCheck;
                deviceSetupCheckedListBox.ItemCheck += ESPTabPageCheckedListBox_ItemCheck;
                accountSetupCheckedListBox.ItemCheck += ESPTabPageCheckedListBox_ItemCheck;
            }
            catch (Exception ex)
            {
                // Do nothing
            }
        }

        private void MDMAnalysisWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            Directory.Delete(_diagFile?.TmpWorkspacePath, true);
            StartUpForm.Instance.analysisWindows.Remove(this);

            if (StartUpForm.Instance.analysisWindows.Count <= 0)
                Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var about = new AboutBox();
            about.ShowDialog();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!StartUpForm.Instance.ShowOpenMDMFileDiag()) return;

            this.Close();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void openWorkspaceFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = _diagFile.TmpWorkspacePath;
            startInfo.UseShellExecute = true;

            Process process = Process.Start(startInfo);
        }

        private void ESPTabPageCheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            // 阻止用户更改项目状态
            if (e.NewValue == CheckState.Checked)
            {
                e.NewValue = CheckState.Unchecked;
                return;
            }

            if (e.NewValue == CheckState.Unchecked)
            {
                e.NewValue = CheckState.Checked;
                return;
            }
        }
    }
}
