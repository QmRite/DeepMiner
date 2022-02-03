using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeepMiner
{
    static class Program
    {
       // internal static DeepMiner game;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var mainMenu = new MainMenu();
            mainMenu.Show();
            Application.Run();

        }

        public static string GetFileDirectory(string fileName)
        {
            return Path.Combine
                (new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName.ToString(),
                    fileName);
        }

    }
}
