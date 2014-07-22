namespace Caladan.Addin.Views.WordPress
{
    partial class WordPressPublishDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.siteUrlText = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.postTitleText = new System.Windows.Forms.TextBox();
            this.draftCheckbox = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tagsText = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Site Url:";
            // 
            // siteUrlText
            // 
            this.siteUrlText.Location = new System.Drawing.Point(6, 40);
            this.siteUrlText.Name = "siteUrlText";
            this.siteUrlText.Size = new System.Drawing.Size(414, 20);
            this.siteUrlText.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Post Title:";
            // 
            // postTitleText
            // 
            this.postTitleText.Location = new System.Drawing.Point(6, 82);
            this.postTitleText.Name = "postTitleText";
            this.postTitleText.Size = new System.Drawing.Size(414, 20);
            this.postTitleText.TabIndex = 3;
            // 
            // draftCheckbox
            // 
            this.draftCheckbox.AutoSize = true;
            this.draftCheckbox.Location = new System.Drawing.Point(6, 109);
            this.draftCheckbox.Name = "draftCheckbox";
            this.draftCheckbox.Size = new System.Drawing.Size(98, 17);
            this.draftCheckbox.TabIndex = 4;
            this.draftCheckbox.Text = "Publish as draft";
            this.draftCheckbox.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tagsText);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.draftCheckbox);
            this.groupBox1.Controls.Add(this.siteUrlText);
            this.groupBox1.Controls.Add(this.postTitleText);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(430, 186);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "WordPress Settings";
            // 
            // tagsText
            // 
            this.tagsText.Location = new System.Drawing.Point(6, 151);
            this.tagsText.Name = "tagsText";
            this.tagsText.Size = new System.Drawing.Size(414, 20);
            this.tagsText.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 134);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Tags:";
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(264, 192);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 6;
            this.okButton.Text = "&OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.OkButtonClick);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(345, 192);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 7;
            this.cancelButton.Text = "&Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // WordPressPublishDialog
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(430, 225);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "WordPressPublishDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Publish";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox siteUrlText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox postTitleText;
        private System.Windows.Forms.CheckBox draftCheckbox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tagsText;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
    }
}