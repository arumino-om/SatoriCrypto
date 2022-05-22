using LibSatoriCrypto.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatoriCrypto
{
    public class CryptoProgress : ICryptoProgress
    {
        public int Max
        { 
            get { return ProgressBar.Maximum; }
            set
            {
                if (ParentControl.InvokeRequired)
                {
                    ParentControl.Invoke(() =>
                    {
                        ProgressBar.Maximum = value;
                        ProgressLabel.Text = $"{ProgressBar.Value}/{ProgressBar.Maximum} 完了…";
                    });
                }
                else
                {
                    ProgressBar.Maximum = value;
                    ProgressLabel.Text = $"{ProgressBar.Value}/{ProgressBar.Maximum} 完了…";
                }
            }
        }
        public int Now
        {
            get { return ProgressBar.Value; }
            set
            {
                if (ParentControl.InvokeRequired)
                {
                    ParentControl.Invoke(() =>
                    {
                        ProgressBar.Value = value;
                        ProgressLabel.Text = $"{ProgressBar.Value}/{ProgressBar.Maximum} 完了…";
                    });
                }
                else
                {
                    ProgressBar.Value = value;
                    ProgressLabel.Text = $"{ProgressBar.Value}/{ProgressBar.Maximum} 完了…";
                }

            }
        }
        public ProgressBar ProgressBar { get; init; }
        public Label ProgressLabel { get; init; }
        public Control ParentControl { get; init; }
    }
}
