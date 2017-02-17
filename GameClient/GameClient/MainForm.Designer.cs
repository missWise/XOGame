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
            this.btnLogin = new System.Windows.Forms.Button();
            this.tbLogin = new System.Windows.Forms.TextBox();
            this.lb_hint = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(58, 62);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 23);
            this.btnLogin.TabIndex = 1;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // tbLogin
            // 
            this.tbLogin.Location = new System.Drawing.Point(32, 36);
            this.tbLogin.Name = "tbLogin";
            this.tbLogin.Size = new System.Drawing.Size(127, 20);
            this.tbLogin.TabIndex = 2;
            // 
            // lb_hint
            // 
            this.lb_hint.AutoSize = true;
            this.lb_hint.Location = new System.Drawing.Point(29, 20);
            this.lb_hint.Name = "lb_hint";
            this.lb_hint.Size = new System.Drawing.Size(81, 13);
            this.lb_hint.TabIndex = 3;
            this.lb_hint.Text = "Enter username";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(187, 113);
            this.Controls.Add(this.lb_hint);
            this.Controls.Add(this.tbLogin);
            this.Controls.Add(this.btnLogin);
            this.Name = "MainForm";
            this.Text = "Authorize";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.TextBox tbLogin;
        private System.Windows.Forms.Label lb_hint;
    }
}

