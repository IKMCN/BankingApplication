namespace BankingApp.WinFormClient
{
    partial class AccountsForm
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
            dgvAccounts = new DataGridView();
            btnLoadAccounts = new Button();
            txtAccountType = new TextBox();
            txtInitialBalance = new TextBox();
            btnCreateAccount = new Button();
            label1 = new Label();
            lblMessage = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvAccounts).BeginInit();
            SuspendLayout();
            // 
            // dgvAccounts
            // 
            dgvAccounts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAccounts.Location = new Point(12, 67);
            dgvAccounts.Name = "dgvAccounts";
            dgvAccounts.RowHeadersWidth = 62;
            dgvAccounts.Size = new Size(745, 145);
            dgvAccounts.TabIndex = 0;
            // 
            // btnLoadAccounts
            // 
            btnLoadAccounts.Location = new Point(12, 12);
            btnLoadAccounts.Name = "btnLoadAccounts";
            btnLoadAccounts.Size = new Size(154, 40);
            btnLoadAccounts.TabIndex = 1;
            btnLoadAccounts.Text = "LoadAccounts";
            btnLoadAccounts.UseVisualStyleBackColor = true;
            btnLoadAccounts.Click += btnLoadAccounts_Click;
            // 
            // txtAccountType
            // 
            txtAccountType.Location = new Point(181, 242);
            txtAccountType.Name = "txtAccountType";
            txtAccountType.Size = new Size(168, 31);
            txtAccountType.TabIndex = 2;
            // 
            // txtInitialBalance
            // 
            txtInitialBalance.Location = new Point(389, 242);
            txtInitialBalance.Name = "txtInitialBalance";
            txtInitialBalance.Size = new Size(171, 31);
            txtInitialBalance.TabIndex = 3;
            // 
            // btnCreateAccount
            // 
            btnCreateAccount.Location = new Point(12, 235);
            btnCreateAccount.Name = "btnCreateAccount";
            btnCreateAccount.Size = new Size(154, 38);
            btnCreateAccount.TabIndex = 4;
            btnCreateAccount.Text = "CreateAccount";
            btnCreateAccount.UseVisualStyleBackColor = true;
            btnCreateAccount.Click += btnCreateAccount_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 309);
            label1.Name = "label1";
            label1.Size = new Size(82, 25);
            label1.TabIndex = 5;
            label1.Text = "Message";
            // 
            // lblMessage
            // 
            lblMessage.AutoSize = true;
            lblMessage.Location = new Point(100, 309);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(16, 25);
            lblMessage.TabIndex = 6;
            lblMessage.Text = ":";
            // 
            // AccountsForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(771, 450);
            Controls.Add(lblMessage);
            Controls.Add(label1);
            Controls.Add(btnCreateAccount);
            Controls.Add(txtInitialBalance);
            Controls.Add(txtAccountType);
            Controls.Add(btnLoadAccounts);
            Controls.Add(dgvAccounts);
            Name = "AccountsForm";
            Text = "AccountsForm";
            ((System.ComponentModel.ISupportInitialize)dgvAccounts).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvAccounts;
        private Button btnLoadAccounts;
        private TextBox txtAccountType;
        private TextBox txtInitialBalance;
        private Button btnCreateAccount;
        private Label label1;
        private Label lblMessage;
    }
}