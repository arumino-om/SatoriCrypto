namespace SatoriCrypto.Panels
{
    partial class DecryptPanel
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
            this.label2 = new System.Windows.Forms.Label();
            this.decryptTargetFileTextBox = new System.Windows.Forms.TextBox();
            this.decryptTargetChoiceButton = new System.Windows.Forms.Button();
            this.progressLabel = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.decryptButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "復号化するファイル";
            // 
            // decryptTargetFileTextBox
            // 
            this.decryptTargetFileTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.decryptTargetFileTextBox.Location = new System.Drawing.Point(102, 0);
            this.decryptTargetFileTextBox.Name = "decryptTargetFileTextBox";
            this.decryptTargetFileTextBox.Size = new System.Drawing.Size(239, 23);
            this.decryptTargetFileTextBox.TabIndex = 3;
            // 
            // decryptTargetChoiceButton
            // 
            this.decryptTargetChoiceButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.decryptTargetChoiceButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.decryptTargetChoiceButton.Location = new System.Drawing.Point(342, -1);
            this.decryptTargetChoiceButton.Name = "decryptTargetChoiceButton";
            this.decryptTargetChoiceButton.Size = new System.Drawing.Size(29, 25);
            this.decryptTargetChoiceButton.TabIndex = 4;
            this.decryptTargetChoiceButton.Text = "…";
            this.decryptTargetChoiceButton.UseVisualStyleBackColor = true;
            this.decryptTargetChoiceButton.Click += new System.EventHandler(this.decryptTargetChoiceButton_Click);
            // 
            // progressLabel
            // 
            this.progressLabel.AutoSize = true;
            this.progressLabel.Location = new System.Drawing.Point(0, 35);
            this.progressLabel.Name = "progressLabel";
            this.progressLabel.Size = new System.Drawing.Size(24, 15);
            this.progressLabel.TabIndex = 5;
            this.progressLabel.Text = "0/0";
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(0, 53);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(370, 23);
            this.progressBar1.TabIndex = 6;
            // 
            // decryptButton
            // 
            this.decryptButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.decryptButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.decryptButton.Location = new System.Drawing.Point(-1, 82);
            this.decryptButton.Name = "decryptButton";
            this.decryptButton.Size = new System.Drawing.Size(372, 40);
            this.decryptButton.TabIndex = 8;
            this.decryptButton.Text = "復号化";
            this.decryptButton.UseVisualStyleBackColor = true;
            this.decryptButton.Click += new System.EventHandler(this.decryptButton_Click);
            // 
            // DecryptPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.decryptButton);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.progressLabel);
            this.Controls.Add(this.decryptTargetChoiceButton);
            this.Controls.Add(this.decryptTargetFileTextBox);
            this.Controls.Add(this.label2);
            this.Name = "DecryptPanel";
            this.Size = new System.Drawing.Size(370, 280);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Label label2;
        private TextBox decryptTargetFileTextBox;
        private Button decryptTargetChoiceButton;
        private Label progressLabel;
        private ProgressBar progressBar1;
        private Button decryptButton;
    }
}
