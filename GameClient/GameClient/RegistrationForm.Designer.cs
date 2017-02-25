namespace GameClient
{
    partial class RegistrationForm
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
            this.lb_pass = new System.Windows.Forms.Label();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.btnReg = new System.Windows.Forms.Button();
            this.lb_hint = new System.Windows.Forms.Label();
            this.tbLogin = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbEmail = new System.Windows.Forms.TextBox();
            this.btnBack = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lb_pass
            // 
            this.lb_pass.AutoSize = true;
            this.lb_pass.Location = new System.Drawing.Point(76, 63);
            this.lb_pass.Name = "lb_pass";
            this.lb_pass.Size = new System.Drawing.Size(80, 13);
            this.lb_pass.TabIndex = 11;
            this.lb_pass.Text = "Enter password";
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(23, 79);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(196, 20);
            this.tbPassword.TabIndex = 10;
            // 
            // btnReg
            // 
            this.btnReg.Location = new System.Drawing.Point(23, 160);
            this.btnReg.Name = "btnReg";
            this.btnReg.Size = new System.Drawing.Size(196, 23);
            this.btnReg.TabIndex = 9;
            this.btnReg.Text = "Registration";
            this.btnReg.UseVisualStyleBackColor = true;
            this.btnReg.Click += new System.EventHandler(this.btnReg_Click);
            // 
            // lb_hint
            // 
            this.lb_hint.AutoSize = true;
            this.lb_hint.Location = new System.Drawing.Point(76, 21);
            this.lb_hint.Name = "lb_hint";
            this.lb_hint.Size = new System.Drawing.Size(81, 13);
            this.lb_hint.TabIndex = 8;
            this.lb_hint.Text = "Enter username";
            // 
            // tbLogin
            // 
            this.tbLogin.Location = new System.Drawing.Point(23, 37);
            this.tbLogin.Name = "tbLogin";
            this.tbLogin.Size = new System.Drawing.Size(196, 20);
            this.tbLogin.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(88, 109);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Enter email";
            // 
            // tbEmail
            // 
            this.tbEmail.Location = new System.Drawing.Point(23, 125);
            this.tbEmail.Name = "tbEmail";
            this.tbEmail.Size = new System.Drawing.Size(196, 20);
            this.tbEmail.TabIndex = 12;
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(23, 189);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(196, 23);
            this.btnBack.TabIndex = 14;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // RegistrationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(251, 224);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbEmail);
            this.Controls.Add(this.lb_pass);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.btnReg);
            this.Controls.Add(this.lb_hint);
            this.Controls.Add(this.tbLogin);
            this.Name = "RegistrationForm";
            this.Text = "Registration";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lb_pass;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Button btnReg;
        private System.Windows.Forms.Label lb_hint;
        private System.Windows.Forms.TextBox tbLogin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbEmail;
        private System.Windows.Forms.Button btnBack;
    }
}