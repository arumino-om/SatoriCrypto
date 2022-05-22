using LibSatoriCrypto;
using LibSatoriCrypto.Interface;

namespace SatoriCrypto
{
    public partial class OldMainForm : Form
    {
        private string[] superencryptionModeDescriptions = new string[]
        {
            // ����: �Ȃ�
            "���d�Í������s���܂���B���̂܂܃p�X���[�h�ƃo�C�g������XOR���܂��B\r\n" +
            "���d�Í������Ȃ��ɐݒ肷��ƁA�ȒP�ɉ�͂����\��������܂��B",

            // ����: PxF2Key
            "PxF2Key�̐�������: Password Xor Filename To Key\r\n" +
            "�p�X���[�h�ƈÍ������_�ł̃t�@�C������XOR�������̂��L�[�Ƃ��A���̃L�[�ƃo�C�g������XOR���܂��B\r\n" +
            "�������t�@�C�����ƃp�X���[�h��������Ȃ���Ε��������ł��܂���B�����͋��x������ƍl�����܂��B\r\n" +
            "����������ꍇ�́A���O�ɐ������t�@�C�����ɕύX���Ă��畜�������Ă��������B���̍ہA������ .sef �͖�������܂��B"
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
                // null ���A�󕶎���͕s���Ƃ���
                return false;
            }

            // �g���Ȃ��t�@�C�������܂�ł��Ȃ����`�F�b�N
            if (System.Text.RegularExpressions.Regex.IsMatch(path
                                           , @"(^|\\|/)(CON|PRN|AUX|NUL|CLOCK\$|COM[0-9]|LPT[0-9])(\.|\\|/|$)"
                                           , System.Text.RegularExpressions.RegexOptions.IgnoreCase))
            {
                // �g���Ȃ��t�@�C����������
                return false;
            }

            if (!File.Exists(path)) return false;

            return true;
        }

        private void �Í���E9ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            superencryptionModeComboBox.SelectedIndex = 0;
        }

        private void selectEncryptTargetFileButton_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "�Í�������t�@�C����I�����Ă��������B";
        }

        private void superencryptionModeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            superencryptionModeDescriptionTextBox.Text = superencryptionModeDescriptions[superencryptionModeComboBox.SelectedIndex];
        }

        private void selectEncryptFileButton_Click(object sender, EventArgs e)
        {
            openFileDialog.Title = "�Í�������t�@�C����I�����Ă��������B";
            if (openFileDialog.ShowDialog() == DialogResult.OK) encryptFileTextBox.Text = openFileDialog.FileName;
        }

        private void selectDecryptFileButton_Click(object sender, EventArgs e)
        {
            openFileDialog.Title = "����������t�@�C����I�����Ă��������B";
            if (openFileDialog.ShowDialog() == DialogResult.OK) decryptFileTextBox.Text = openFileDialog.FileName;
        }

        private async void encryptButton_Click(object sender, EventArgs e)
        {
            Enabled = false;
            if (!passwordValid() || !pathValid(encryptFileTextBox.Text))
            {
                Enabled = true;
                MessageBox.Show("�ǂ���������������܂���B�ݒ���m�F���Ă��������B", "�G���[", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string newFileName = encryptFileTextBox.Text + ".sef";

            if (File.Exists(newFileName))
            {
                var dialogResult = MessageBox.Show($"���łɈȉ��̃t�@�C���͑��݂���悤�ł��B�㏑�����܂����H\n\n{newFileName}", "�m�F", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult != DialogResult.Yes)
                {
                    Enabled = true;
                    return;
                }
            }

            await LibSatoriCrypto.SatoriCrypto.EncryptFileAsync(encryptFileTextBox.Text, newFileName
                , passwordTextBox.Text, (LibSatoriCrypto.SuperencryptionMode)superencryptionModeComboBox.SelectedIndex, new CryptoProgress());
            MessageBox.Show($"�ȉ��̃p�X�ɈÍ������ꂽ�t�@�C���𐶐����܂����B\n\n{newFileName}", "����", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Enabled = true;
        }

        private async void decryptButton_Click(object sender, EventArgs e)
        {
            Enabled = false;
            if (!passwordValid() || !pathValid(decryptFileTextBox.Text))
            {
                Enabled = true;
                MessageBox.Show("�ǂ���������������܂���B�ݒ���m�F���Ă��������B", "�G���[", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string newFileName = decryptFileTextBox.Text;
            if (newFileName.EndsWith(".sef")) newFileName = newFileName[0..^4];
            else newFileName += ".sdf";

            if (File.Exists(newFileName))
            {
                var dialogResult = MessageBox.Show($"���łɈȉ��̃t�@�C���͑��݂���悤�ł��B�㏑�����܂����H\n\n{newFileName}", "�m�F", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult != DialogResult.Yes)
                {
                    Enabled = true;
                    return;
                }
            }

            await LibSatoriCrypto.SatoriCrypto.DecryptFileAsync(decryptFileTextBox.Text, newFileName, passwordTextBox.Text, new CryptoProgress());
            MessageBox.Show($"�ȉ��̃p�X�ɕ��������ꂽ�t�@�C���𐶐����܂����B\n\n{newFileName}", "����", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Enabled = true;
        }

        private void openNewFormToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var newform = new MainForm();
            newform.Show();
        }
    }
}