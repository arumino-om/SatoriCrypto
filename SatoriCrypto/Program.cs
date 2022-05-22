using System.Runtime.InteropServices;

namespace SatoriCrypto
{
    internal static class Program
    {
        static bool RequiredOldMainForm = false;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            ApplicationConfiguration.Initialize();

            AnalyzeArgs(args);

            if (RequiredOldMainForm) Application.Run(new OldMainForm());            
            else Application.Run(new MainForm());
        }

        static void ShowHelp()
        {
            Console.WriteLine("Help");
        }

        static void AnalyzeArgs(string[] args)
        {
            foreach (var arg in args)
            {
                if (arg == "--oldform") RequiredOldMainForm = true;
            }
        }
    }
}