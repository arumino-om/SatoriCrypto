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
            Application.Run(new MainForm());
        }

        static void ShowHelp()
        {
            Console.WriteLine("Help");
        }
    }
}