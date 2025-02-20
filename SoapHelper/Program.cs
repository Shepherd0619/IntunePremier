namespace SoapHelper
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            MessageBox.Show("By using this program, you agree that you are responsible for any data leak, loss or corruption that may occur. \n\n" +
                "We are also not responsible for how data was processed by LLM.\n\n" +
                "Use at your own risk.", 
                "Disclaimer", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            ApplicationConfiguration.Initialize();
            Application.Run(new StartUpForm());
        }
    }
}