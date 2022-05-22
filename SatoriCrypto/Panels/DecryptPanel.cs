using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using LibSatoriCrypto;
using LibSatoriCrypto.Interface;

namespace SatoriCrypto.Panels
{
    public partial class DecryptPanel : UserControl
    {
        private MainForm parentForm;
        public DecryptPanel(MainForm parentForm)
        {
            InitializeComponent();
            this.parentForm = parentForm;
        }

        private async void decryptButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(decryptTargetFileTextBox.Text))
            {
                MessageBox.Show("復号化するファイルが選択されていません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!File.Exists(decryptTargetFileTextBox.Text))
            {
                MessageBox.Show($"{decryptTargetFileTextBox.Text}\nは存在しません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!LibSatoriCrypto.SatoriCrypto.IsValidSEF(decryptTargetFileTextBox.Text))
            {
                MessageBox.Show($"{decryptTargetFileTextBox.Text}\nは SEFファイル ではありません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var progress = new CryptoProgress()
            {
                ProgressBar = progressBar1,
                ProgressLabel = progressLabel,
                ParentControl = this
            };

            var newFileName = Path.GetFileName(decryptTargetFileTextBox.Text).EndsWith(".sef")
                ? decryptTargetFileTextBox.Text[0..^4] : decryptTargetFileTextBox.Text;

            if (File.Exists(newFileName))
            {
                var result = MessageBox.Show($"以下のファイルは存在するようです。上書きしますか？\n{newFileName}", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.No) return;
            }

            ChangeEnabled(false);
            await LibSatoriCrypto.SatoriCrypto.DecryptFileAsync(decryptTargetFileTextBox.Text, newFileName, MainForm.Password, progress);
            ChangeEnabled(true);

            MessageBox.Show($"以下のファイルに出力しました。\n{newFileName}", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void decryptTargetChoiceButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new();
            openFileDialog.Title = "復号化するファイルを選択してください。";
            if (openFileDialog.ShowDialog() == DialogResult.OK) decryptTargetFileTextBox.Text = openFileDialog.FileName;
        }

        private void ChangeEnabled(bool enabled)
        {
            decryptTargetFileTextBox.Enabled = enabled;
            decryptTargetChoiceButton.Enabled = enabled;
            decryptButton.Enabled = enabled;
            parentForm.opComboBox.Enabled = enabled;
        }
    }
}
