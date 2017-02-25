namespace GameClient
{
    partial class PasswordForm
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
            this.btnPass = new System.Windows.Forms.Button();
            this.tbPass = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnPass
            // 
            this.btnPass.Location = new System.Drawing.Point(52, 74);
            this.btnPass.Name = "btnPass";
            this.btnPass.Size = new System.Drawing.Size(98, 40);
            this.btnPass.TabIndex = 0;
            this.btnPass.Text = "Change password";
            this.btnPass.UseVisualStyleBackColor = true;
            this.btnPass.Click += new System.EventHandler(this.btnPass_Click);
            // 
            // tbPass
            // 
            this.tbPass.Location = new System.Drawing.Point(22, 39);
            this.tbPass.Name = "tbPass";
            this.tbPass.Size = new System.Drawing.Size(148, 20);
            this.tbPass.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(47, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Enter new password";
            // 
            // PasswordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(195, 136);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbPass);
            this.Controls.Add(this.btnPass);
            this.Name = "PasswordForm";
            this.Text = "Password";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPass;
        private System.Windows.Forms.TextBox tbPass;
        private System.Windows.Forms.Label label1;
    }
}