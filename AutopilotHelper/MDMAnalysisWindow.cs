using AutopilotHelper.Utilities;

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

            autopilotDiagTextBox1.Text = _autopilotUtil.GetGeneralDiagnosticsReport();

            await ProcessedPoliciesWebView.EnsureCoreWebView2Async();
            
            ProcessedPoliciesWebView.NavigateToString(_autopilotUtil.GetHtmlFormattedProcessedPolicies());
        }

        private void MDMAnalysisWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            Directory.Delete(_diagFile?.TmpWorkplacePath, true);
            Application.Exit();
        }
    }
}
