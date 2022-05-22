using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SatoriCrypto.Panels
{
    public partial class EncryptPanel : UserControl
    {
        private MainForm parentForm;
        public EncryptPanel(MainForm parentForm)
        {
            InitializeComponent();
            this.parentForm = parentForm;
            superencryptionModeComboBox.SelectedIndex = 0;
        }

        private async void encryptButton_Click(object sender, EventArgs e)
        {
            string newFileName = encryptTargetFileTextBox.Text + ".sef";

            if (File.Exists(newFileName))
            {
                var dialogResult = MessageBox.Show($"すでに以下のファイルは存在するようです。上書きしますか？\n\n{newFileName}", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.No) return;
            }

            var progress = new CryptoProgress()
            {
                ProgressBar = progressBar,
                ProgressLabel = progressLabel,
                ParentControl = this
            };

            ChangeEnabled(false);
            await LibSatoriCrypto.SatoriCrypto.EncryptFileAsync(encryptTargetFileTextBox.Text, newFileName,
               MainForm.Password, (LibSatoriCrypto.SuperencryptionMode)superencryptionModeComboBox.SelectedIndex, progress);
            ChangeEnabled(true);

            MessageBox.Show($"以下のファイルに暗号化されたファイルを生成しました。\n\n{newFileName}", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void encryptTargetChoiceButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new();
            openFileDialog.Title = "暗号化するファイルを選択してください。";
            if (openFileDialog.ShowDialog() == DialogResult.OK) encryptTargetFileTextBox.Text = openFileDialog.FileName;
        }

        private void ChangeEnabled(bool enabled)
        {
            superencryptionModeComboBox.Enabled = enabled;
            encryptTargetFileTextBox.Enabled = enabled;
            encryptTargetChoiceButton.Enabled = enabled;
            encryptButton.Enabled = enabled;
            parentForm.opComboBox.Enabled = enabled;
        }
    }
}
