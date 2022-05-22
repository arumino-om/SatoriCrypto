using LibSatoriCrypto.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatoriCryptoCUI
{
    public class CryptoProgress : ICryptoProgress
    {
        private int startX = 0;
        private int startY = 0;
        private int lastWritedLength = 0;
        private Translate tr = null;

        private int _max { get; set; }
        public int Max
        {
            get { return _max; }
            set
            {
                _max = value;
                WriteProgress();
            }
        }

        private int _now { get; set; }
        public int Now
        {
            get { return _now; }
            set
            {
                _now = value;
                WriteProgress();
            }
        }

        private void WriteProgress()
        {
            Console.SetCursorPosition(startX, startY);
            string writeText = tr.t("[Progress] {0}/{1} complete...", Now, Max);
            if (writeText.Length < lastWritedLength) writeText += new string(' ', lastWritedLength - writeText.Length);

            Console.WriteLine(writeText);

            lastWritedLength = writeText.Length;
        }

        public CryptoProgress(Translate tr)
        {
            (int left, int top) = Console.GetCursorPosition();
            startX = left;
            startY = top;
            this.tr = tr;
        }
    }
}
