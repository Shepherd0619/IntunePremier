﻿using AutopilotHelper.Models;
using AutopilotHelper.Utilities;
using DarkModeForms;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

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

        private string autopilotDiagText;

        private readonly List<ListViewItem> cspListViewItems = new();

        private bool isLoadingCSP = false;

        public MDMAnalysisWindow()
        {
            InitializeComponent();

            var dm = new DarkModeCS(this)
            {
                ColorMode = DarkModeCS.DisplayMode.SystemDefault
            };
        }

        private void MDMAnalysisWindow_Load(object sender, EventArgs e)
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

            try
            {
                InitializeESPTabPage(_autopilotUtil.GetAutopilotSettingsFromRegistry());
            }
            catch (Exception ex)
            {
                // Do nothing
            }

            isLoadingCSP = true;
            cspStripStatusLabel.Text = "Opening registry...";
            cspBackgroundWorker.RunWorkerAsync();
            autopilotDiagTextBox1.Text = "Please wait as we are generating report for you......";
            autopilotDiagBackgrounWorker.RunWorkerAsync();
            //autopilotDiagTextBox1.Text = _autopilotUtil.GetGeneralDiagnosticsReport();
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
            this.Hide();

            cspBackgroundWorker.CancelAsync();
            autopilotDiagBackgrounWorker.CancelAsync();

            while (cspBackgroundWorker.IsBusy || autopilotDiagBackgrounWorker.IsBusy)
            {
                Application.DoEvents();
            }

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

        private async void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!await StartUpForm.Instance.ShowOpenMDMFileDiag()) return;

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

        private void eventViewerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new EventViewerForm(_diagFile);
            form.Show(this);
        }

        private void registryViewerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new RegViewerForm(_autopilotUtil.Reg);
            form.Show(this);
        }

        private void policiesSearchBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue != 13) return;

            if (isLoadingCSP) return;

            ListViewItem selected;

            if (policiesListView.SelectedItems.Count > 0)
            {
                selected = policiesListView.SelectedItems[0];
            }
            else
            {
                selected = null;
            }

            var keyword = policiesSearchBox.Text;

            ListViewUtil.FindNextKeyword(keyword, 1, policies_downCheckBox.Checked, policies_CaseSensitiveCheckBox.Checked, policiesListView);

            if (policiesListView.SelectedItems.Count <= 0 || selected == policiesListView.SelectedItems[0])
            {
                ListViewUtil.FindNextKeyword(keyword, 2, policies_downCheckBox.Checked, policies_CaseSensitiveCheckBox.Checked, policiesListView, true);
            }
        }

        private void policiesListView_Click(object sender, EventArgs e)
        {
            policies_tabControl.Visible = true;

            var item = policiesListView.Items[policiesListView.SelectedIndices[0]];

            var sb = new StringBuilder();
            sb.AppendLine($"ID: {item.SubItems[0].Text}");
            sb.AppendLine();
            sb.AppendLine($"NodeUri: {item.SubItems[1].Text}");
            sb.AppendLine();
            sb.AppendLine($"ExpectedValue: {item.SubItems[2].Text}");

            policiesDetailTextBox.Text = sb.ToString();
        }

        private void policies_detailTab_HideButton_Click(object sender, EventArgs e)
        {
            policies_tabControl.Visible = false;
        }

        private void cspBackgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            if(cspBackgroundWorker.CancellationPending) { e.Cancel = true; return; }

            if (_autopilotUtil.NodeCaches == null)
                _autopilotUtil.GetProcessedPolicies();

            cspBackgroundWorker.ReportProgress(-1);
            for (int i = 0; i < _autopilotUtil.NodeCaches.Count; i++)
            {
                if (cspBackgroundWorker.CancellationPending) { e.Cancel = true; return; }
                cspBackgroundWorker.ReportProgress(i);
                Thread.Sleep(10);
            }
        }

        private void cspBackgroundWorker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == -1)
            {
                policiesListView.Items.Clear();
                return;
            }

            var item = new ListViewItem();
            item.Text = _autopilotUtil.NodeCaches[e.ProgressPercentage].id.ToString();
            item.SubItems.Add(_autopilotUtil.NodeCaches[e.ProgressPercentage].NodeUri);
            item.SubItems.Add(_autopilotUtil.NodeCaches[e.ProgressPercentage].ExpectedValue);

            cspListViewItems.Add(item);

            cspStripProgressBar.Maximum = _autopilotUtil.NodeCaches.Count - 1;
            cspStripProgressBar.Value = e.ProgressPercentage;
            cspStripStatusLabel.Text = $"Loading CSPs... ({e.ProgressPercentage + 1}/{_autopilotUtil.NodeCaches.Count})";
        }

        private void cspBackgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            cspListViewItems.ForEach(item => policiesListView.Items.Add(item));
            cspStripStatusLabel.Text = "CSPs loaded.";
            isLoadingCSP = false;
        }

        private void autopilotDiagBackgrounWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            if (autopilotDiagBackgrounWorker.CancellationPending)
            {
                e.Cancel = true;
                return;
            }

            autopilotDiagText = _autopilotUtil.GetGeneralDiagnosticsReport();
        }

        private void autopilotDiagBackgrounWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            autopilotDiagTextBox1.Text = autopilotDiagText;
        }
    }
}
