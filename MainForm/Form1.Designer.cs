namespace MainForm
{
    partial class Form1
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
            this.lblShowPwd = new System.Windows.Forms.Label();
            this.pwdMeter = new CustomControls.PasswordMeter();
            this.pwdTxtBox = new CustomControls.PasswordTextBox();
            this.SuspendLayout();
            // 
            // lblShowPwd
            // 
            this.lblShowPwd.AutoSize = true;
            this.lblShowPwd.Location = new System.Drawing.Point(118, 45);
            this.lblShowPwd.Name = "lblShowPwd";
            this.lblShowPwd.Size = new System.Drawing.Size(0, 13);
            this.lblShowPwd.TabIndex = 3;
            // 
            // pwdMeter
            // 
            this.pwdMeter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pwdMeter.Location = new System.Drawing.Point(162, 15);
            this.pwdMeter.Name = "pwdMeter";
            this.pwdMeter.Size = new System.Drawing.Size(138, 25);
            this.pwdMeter.TabIndex = 4;
            // 
            // pwdTxtBox
            // 
            this.pwdTxtBox.Location = new System.Drawing.Point(12, 18);
            this.pwdTxtBox.Name = "pwdTxtBox";
            this.pwdTxtBox.Size = new System.Drawing.Size(138, 20);
            this.pwdTxtBox.TabIndex = 0;
            this.pwdTxtBox.TextChanged += new System.EventHandler(this.pwdTxtBox_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(311, 60);
            this.Controls.Add(this.pwdMeter);
            this.Controls.Add(this.lblShowPwd);
            this.Controls.Add(this.pwdTxtBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CustomControls.PasswordTextBox pwdTxtBox;
        private System.Windows.Forms.Label lblShowPwd;
        private CustomControls.PasswordMeter pwdMeter;
    }
}

