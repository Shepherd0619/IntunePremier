using AutopilotHelper.Utilities;

namespace AutopilotHelper
{
    public partial class StartUpForm : Form
    {
        public StartUpForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void OpenMDMDiagButton_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog(this);

            if (!File.Exists(openFileDialog1.FileName)) return;

            Stream? fileStream = null;
            try
            {
                fileStream = openFileDialog1.OpenFile();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to open the file!\n\n{ex}", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MDMFileUtil? util = new(fileStream, Path.GetExtension(openFileDialog1.FileName) == ".cab" ? true : false);
        }
    }
}
