namespace SatoriCrypto.Panels
{
    partial class EncryptPanel
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.superencryptionModeComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.encryptTargetFileTextBox = new System.Windows.Forms.TextBox();
            this.encryptTargetChoiceButton = new System.Windows.Forms.Button();
            this.progressLabel = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.encryptButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // superencryptionModeComboBox
            // 
            this.superencryptionModeComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.superencryptionModeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.superencryptionModeComboBox.FormattingEnabled = true;
            this.superencryptionModeComboBox.Items.AddRange(new object[] {
            "XOR",
            "Password XOR Filename to Key",
            "AES",
            "Password XOR AES"});
            this.superencryptionModeComboBox.Location = new System.Drawing.Point(102, 0);
            this.superencryptionModeComboBox.Name = "superencryptionModeComboBox";
            this.superencryptionModeComboBox.Size = new System.Drawing.Size(268, 23);
            this.superencryptionModeComboBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "多重暗号化方法";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "暗号化するファイル";
            // 
            // encryptTargetFileTextBox
            // 
            this.encryptTargetFileTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.encryptTargetFileTextBox.Location = new System.Drawing.Point(102, 29);
            this.encryptTargetFileTextBox.Name = "encryptTargetFileTextBox";
            this.encryptTargetFileTextBox.Size = new System.Drawing.Size(239, 23);
            this.encryptTargetFileTextBox.TabIndex = 3;
            // 
            // encryptTargetChoiceButton
            // 
            this.encryptTargetChoiceButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.encryptTargetChoiceButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.encryptTargetChoiceButton.Location = new System.Drawing.Point(342, 28);
            this.encryptTargetChoiceButton.Name = "encryptTargetChoiceButton";
            this.encryptTargetChoiceButton.Size = new System.Drawing.Size(29, 25);
            this.encryptTargetChoiceButton.TabIndex = 4;
            this.encryptTargetChoiceButton.Text = "…";
            this.encryptTargetChoiceButton.UseVisualStyleBackColor = true;
            this.encryptTargetChoiceButton.Click += new System.EventHandler(this.encryptTargetChoiceButton_Click);
            // 
            // progressLabel
            // 
            this.progressLabel.AutoSize = true;
            this.progressLabel.Location = new System.Drawing.Point(0, 71);
            this.progressLabel.Name = "progressLabel";
            this.progressLabel.Size = new System.Drawing.Size(24, 15);
            this.progressLabel.TabIndex = 5;
            this.progressLabel.Text = "0/0";
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(0, 89);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(370, 23);
            this.progressBar.TabIndex = 6;
            // 
            // encryptButton
            // 
            this.encryptButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.encryptButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.encryptButton.Location = new System.Drawing.Point(-1, 118);
            this.encryptButton.Name = "encryptButton";
            this.encryptButton.Size = new System.Drawing.Size(372, 40);
            this.encryptButton.TabIndex = 7;
            this.encryptButton.Text = "暗号化";
            this.encryptButton.UseVisualStyleBackColor = true;
            this.encryptButton.Click += new System.EventHandler(this.encryptButton_Click);
            // 
            // EncryptPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.encryptButton);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.progressLabel);
            this.Controls.Add(this.encryptTargetChoiceButton);
            this.Controls.Add(this.encryptTargetFileTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.superencryptionModeComboBox);
            this.Name = "EncryptPanel";
            this.Size = new System.Drawing.Size(370, 280);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComboBox superencryptionModeComboBox;
        private Label label1;
        private Label label2;
        private TextBox encryptTargetFileTextBox;
        private Button encryptTargetChoiceButton;
        private Label progressLabel;
        private ProgressBar progressBar;
        private Button encryptButton;
    }
}
