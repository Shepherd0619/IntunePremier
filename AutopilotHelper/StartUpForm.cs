using AutopilotHelper.Utilities;

namespace AutopilotHelper
{
    public partial class StartUpForm : Form
    {
        public static StartUpForm Instance => instance;
        private static StartUpForm instance;

        public readonly Dictionary<MDMAnalysisWindow, string> analysisWindows = new ();

        public StartUpForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            instance = this;
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

            Stream? fileStream = null;
            try
            {
                fileStream = openFileDialog1.OpenFile();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to open the file!\n\n{ex}", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

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

            this.Hide();
            openFileDialog1.FileName = string.Empty;

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
    }
}
