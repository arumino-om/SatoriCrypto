using LibSatoriCrypto;
using LibSatoriCrypto.Interface;

namespace SatoriCrypto
{
    public partial class OldMainForm : Form
    {
        private string[] superencryptionModeDescriptions = new string[]
        {
            // 方式: なし
            "多重暗号化を行いません。そのままパスワードとバイト文字をXORします。\r\n" +
            "多重暗号化をなしに設定すると、簡単に解析される可能性があります。",

            // 方式: PxF2Key
            "PxF2Keyの正式名称: Password Xor Filename To Key\r\n" +
            "パスワードと暗号化時点でのファイル名をXORしたものをキーとし、そのキーとバイト文字をXORします。\r\n" +
            "正しいファイル名とパスワードが分からなければ復号化ができません。多少は強度があると考えられます。\r\n" +
            "復号化する場合は、事前に正しいファイル名に変更してから復号化してください。その際、末尾の .sef は無視されます。"
        };
        private OpenFileDialog openFileDialog = new();

        public OldMainForm()
        {
            InitializeComponent();
        }

        private bool passwordValid()
        {
            return !string.IsNullOrEmpty(passwordTextBox.Text);
        }

        private bool pathValid(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                // null か、空文字列は不正とする
                return false;
            }

            // 使えないファイル名を含んでいないかチェック
            if (System.Text.RegularExpressions.Regex.IsMatch(path
                                           , @"(^|\\|/)(CON|PRN|AUX|NUL|CLOCK\$|COM[0-9]|LPT[0-9])(\.|\\|/|$)"
                                           , System.Text.RegularExpressions.RegexOptions.IgnoreCase))
            {
                // 使えないファイル名がある
                return false;
            }

            if (!File.Exists(path)) return false;

            return true;
        }

        private void 暗号化E9ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            superencryptionModeComboBox.SelectedIndex = 0;
        }

        private void selectEncryptTargetFileButton_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "暗号化するファイルを選択してください。";
        }

        private void superencryptionModeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            superencryptionModeDescriptionTextBox.Text = superencryptionModeDescriptions[superencryptionModeComboBox.SelectedIndex];
        }

        private void selectEncryptFileButton_Click(object sender, EventArgs e)
        {
            openFileDialog.Title = "暗号化するファイルを選択してください。";
            if (openFileDialog.ShowDialog() == DialogResult.OK) encryptFileTextBox.Text = openFileDialog.FileName;
        }

        private void selectDecryptFileButton_Click(object sender, EventArgs e)
        {
            openFileDialog.Title = "復号化するファイルを選択してください。";
            if (openFileDialog.ShowDialog() == DialogResult.OK) decryptFileTextBox.Text = openFileDialog.FileName;
        }

        private async void encryptButton_Click(object sender, EventArgs e)
        {
            Enabled = false;
            if (!passwordValid() || !pathValid(encryptFileTextBox.Text))
            {
                Enabled = true;
                MessageBox.Show("どこかが正しくありません。設定を確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string newFileName = encryptFileTextBox.Text + ".sef";

            if (File.Exists(newFileName))
            {
                var dialogResult = MessageBox.Show($"すでに以下のファイルは存在するようです。上書きしますか？\n\n{newFileName}", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult != DialogResult.Yes)
                {
                    Enabled = true;
                    return;
                }
            }

            await LibSatoriCrypto.SatoriCrypto.EncryptFileAsync(encryptFileTextBox.Text, newFileName
                , passwordTextBox.Text, (LibSatoriCrypto.SuperencryptionMode)superencryptionModeComboBox.SelectedIndex, new CryptoProgress());
            MessageBox.Show($"以下のパスに暗号化されたファイルを生成しました。\n\n{newFileName}", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Enabled = true;
        }

        private async void decryptButton_Click(object sender, EventArgs e)
        {
            Enabled = false;
            if (!passwordValid() || !pathValid(decryptFileTextBox.Text))
            {
                Enabled = true;
                MessageBox.Show("どこかが正しくありません。設定を確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string newFileName = decryptFileTextBox.Text;
            if (newFileName.EndsWith(".sef")) newFileName = newFileName[0..^4];
            else newFileName += ".sdf";

            if (File.Exists(newFileName))
            {
                var dialogResult = MessageBox.Show($"すでに以下のファイルは存在するようです。上書きしますか？\n\n{newFileName}", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult != DialogResult.Yes)
                {
                    Enabled = true;
                    return;
                }
            }

            await LibSatoriCrypto.SatoriCrypto.DecryptFileAsync(decryptFileTextBox.Text, newFileName, passwordTextBox.Text, new CryptoProgress());
            MessageBox.Show($"以下のパスに復号化されたファイルを生成しました。\n\n{newFileName}", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Enabled = true;
        }

        private void openNewFormToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var newform = new MainForm();
            newform.Show();
        }
    }
}