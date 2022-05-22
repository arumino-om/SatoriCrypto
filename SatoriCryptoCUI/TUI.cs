using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatoriCryptoCUI
{
    public class TUI
    {
        public static bool YesNo(string question)
        {
            Console.WriteLine(question + " (yes/no)");
            string input = Console.ReadLine();
            if (input == "yes" || input == "y") return true;
            return false;
        }
    }
}
