namespace GameClient
{
    partial class MainForm
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
            this.tbLogin = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.lb_hint = new System.Windows.Forms.Label();
            this.btnReg = new System.Windows.Forms.Button();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.lb_pass = new System.Windows.Forms.Label();
            this.btnForgotPass = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbLogin
            // 
            this.tbLogin.Location = new System.Drawing.Point(32, 36);
            this.tbLogin.Name = "tbLogin";
            this.tbLogin.Size = new System.Drawing.Size(196, 20);
            this.tbLogin.TabIndex = 1;
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(32, 107);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(196, 23);
            this.btnLogin.TabIndex = 5;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // lb_hint
            // 
            this.lb_hint.AutoSize = true;
            this.lb_hint.Location = new System.Drawing.Point(85, 20);
            this.lb_hint.Name = "lb_hint";
            this.lb_hint.Size = new System.Drawing.Size(81, 13);
            this.lb_hint.TabIndex = 3;
            this.lb_hint.Text = "Enter username";
            // 
            // btnReg
            // 
            this.btnReg.Location = new System.Drawing.Point(32, 165);
            this.btnReg.Name = "btnReg";
            this.btnReg.Size = new System.Drawing.Size(196, 23);
            this.btnReg.TabIndex = 4;
            this.btnReg.Text = "Registration";
            this.btnReg.UseVisualStyleBackColor = true;
            this.btnReg.Click += new System.EventHandler(this.btnReg_Click);
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(32, 78);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(196, 20);
            this.tbPassword.TabIndex = 5;
            // 
            // lb_pass
            // 
            this.lb_pass.AutoSize = true;
            this.lb_pass.Location = new System.Drawing.Point(85, 62);
            this.lb_pass.Name = "lb_pass";
            this.lb_pass.Size = new System.Drawing.Size(80, 13);
            this.lb_pass.TabIndex = 6;
            this.lb_pass.Text = "Enter password";
            // 
            // btnForgotPass
            // 
            this.btnForgotPass.Location = new System.Drawing.Point(32, 136);
            this.btnForgotPass.Name = "btnForgotPass";
            this.btnForgotPass.Size = new System.Drawing.Size(196, 23);
            this.btnForgotPass.TabIndex = 7;
            this.btnForgotPass.Text = "Forgot password";
            this.btnForgotPass.UseVisualStyleBackColor = true;
            this.btnForgotPass.Click += new System.EventHandler(this.btnForgotPass_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(240, 203);
            this.Controls.Add(this.btnForgotPass);
            this.Controls.Add(this.lb_pass);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.btnReg);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.lb_hint);
            this.Controls.Add(this.tbLogin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "MainForm";
            this.Text = "Authorize";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox tbLogin;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label lb_hint;
        private System.Windows.Forms.Button btnReg;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Label lb_pass;
        private System.Windows.Forms.Button btnForgotPass;
    }
}

