namespace BankingApp.WinFormClient
{
    partial class LoginForm
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
            label4 = new Label();
            label2 = new Label();
            btnLogin = new Button();
            txtPassword = new TextBox();
            txtUsername = new TextBox();
            btnGoToRegister = new Button();
            lblMessage = new Label();
            label3 = new Label();
            SuspendLayout();
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(88, 120);
            label4.Name = "label4";
            label4.Size = new Size(87, 25);
            label4.TabIndex = 16;
            label4.Text = "Password";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(88, 68);
            label2.Name = "label2";
            label2.Size = new Size(91, 25);
            label2.TabIndex = 14;
            label2.Text = "Username";
            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(464, 62);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(138, 42);
            btnLogin.TabIndex = 13;
            btnLogin.Text = "Login";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(206, 114);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(224, 31);
            txtPassword.TabIndex = 12;
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(206, 62);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(224, 31);
            txtUsername.TabIndex = 10;
            // 
            // btnGoToRegister
            // 
            btnGoToRegister.Location = new Point(464, 114);
            btnGoToRegister.Name = "btnGoToRegister";
            btnGoToRegister.Size = new Size(138, 42);
            btnGoToRegister.TabIndex = 17;
            btnGoToRegister.Text = "GoToRegister";
            btnGoToRegister.UseVisualStyleBackColor = true;
            btnGoToRegister.Click += btnGoToRegister_Click;
            // 
            // lblMessage
            // 
            lblMessage.AutoSize = true;
            lblMessage.Location = new Point(88, 189);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(86, 25);
            lblMessage.TabIndex = 18;
            lblMessage.Text = "Message:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(206, 189);
            label3.Name = "label3";
            label3.Size = new Size(0, 25);
            label3.TabIndex = 19;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label3);
            Controls.Add(lblMessage);
            Controls.Add(btnGoToRegister);
            Controls.Add(label4);
            Controls.Add(label2);
            Controls.Add(btnLogin);
            Controls.Add(txtPassword);
            Controls.Add(txtUsername);
            Name = "LoginForm";
            Text = "LoginForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label4;
        private Label label2;
        private Button btnLogin;
        private TextBox txtPassword;
        private TextBox txtUsername;
        private Button btnGoToRegister;
        private Label lblMessage;
        private Label label3;
    }
}