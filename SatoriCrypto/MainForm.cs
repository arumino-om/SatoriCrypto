using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SatoriCrypto
{
    public partial class MainForm : Form
    {
        public static string Password { get; private set; }
        public List<UserControl> panelManager = new List<UserControl>();

        public MainForm()
        {
            InitializeComponent();

            panelManager.Add(new Panels.EncryptPanel(this));
            panelManager.Add(new Panels.DecryptPanel(this));

            opComboBox.SelectedIndex = 0;
        }

        private void opComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            panel1.Controls.Add(panelManager[opComboBox.SelectedIndex]);
            panel1.Controls[0].Size = new Size(panel1.Width, panel1.Height);
        }

        private void MainForm_ClientSizeChanged(object sender, EventArgs e)
        {
            panelManager.ForEach((panel) =>
            {
                panel.Size = new Size(panel1.Width, panel1.Height);
            });
        }

        private void passwordTextBox_TextChanged(object sender, EventArgs e)
        {
            Password = passwordTextBox.Text;
        }
    }
}
