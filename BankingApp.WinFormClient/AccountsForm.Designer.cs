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
            lblSelectedAccountId = new Label();
            lblSelectedAccountIdText = new Label();
            txtAmount = new TextBox();
            btnDeposit = new Button();
            btnWithdraw = new Button();
            lblTransactionMessage = new Label();
            lblTransactionMessageText = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvAccounts).BeginInit();
            SuspendLayout();
            // 
            // dgvAccounts
            // 
            dgvAccounts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAccounts.Location = new Point(12, 101);
            dgvAccounts.Name = "dgvAccounts";
            dgvAccounts.RowHeadersWidth = 62;
            dgvAccounts.Size = new Size(796, 174);
            dgvAccounts.TabIndex = 0;
            dgvAccounts.CellContentClick += dgvAccounts_CellContentClick;
            // 
            // btnLoadAccounts
            // 
            btnLoadAccounts.Location = new Point(12, 12);
            btnLoadAccounts.Name = "btnLoadAccounts";
            btnLoadAccounts.Size = new Size(154, 38);
            btnLoadAccounts.TabIndex = 1;
            btnLoadAccounts.Text = "LoadAccounts";
            btnLoadAccounts.UseVisualStyleBackColor = true;
            btnLoadAccounts.Click += btnLoadAccounts_Click;
            // 
            // txtAccountType
            // 
            txtAccountType.Location = new Point(341, 19);
            txtAccountType.Name = "txtAccountType";
            txtAccountType.Size = new Size(168, 31);
            txtAccountType.TabIndex = 2;
            // 
            // txtInitialBalance
            // 
            txtInitialBalance.Location = new Point(549, 19);
            txtInitialBalance.Name = "txtInitialBalance";
            txtInitialBalance.Size = new Size(171, 31);
            txtInitialBalance.TabIndex = 3;
            // 
            // btnCreateAccount
            // 
            btnCreateAccount.Location = new Point(172, 12);
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
            label1.Location = new Point(12, 60);
            label1.Name = "label1";
            label1.Size = new Size(152, 25);
            label1.TabIndex = 5;
            label1.Text = "Account Message";
            // 
            // lblMessage
            // 
            lblMessage.AutoSize = true;
            lblMessage.Location = new Point(190, 60);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(16, 25);
            lblMessage.TabIndex = 6;
            lblMessage.Text = ":";
            // 
            // lblSelectedAccountId
            // 
            lblSelectedAccountId.AutoSize = true;
            lblSelectedAccountId.Location = new Point(190, 297);
            lblSelectedAccountId.Name = "lblSelectedAccountId";
            lblSelectedAccountId.Size = new Size(16, 25);
            lblSelectedAccountId.TabIndex = 7;
            lblSelectedAccountId.Text = ":";
            // 
            // lblSelectedAccountIdText
            // 
            lblSelectedAccountIdText.AutoSize = true;
            lblSelectedAccountIdText.Location = new Point(12, 297);
            lblSelectedAccountIdText.Name = "lblSelectedAccountIdText";
            lblSelectedAccountIdText.Size = new Size(159, 25);
            lblSelectedAccountIdText.TabIndex = 8;
            lblSelectedAccountIdText.Text = "SelectedAccountId";
            // 
            // txtAmount
            // 
            txtAmount.Location = new Point(12, 392);
            txtAmount.Name = "txtAmount";
            txtAmount.Size = new Size(314, 31);
            txtAmount.TabIndex = 9;
            // 
            // btnDeposit
            // 
            btnDeposit.Location = new Point(12, 338);
            btnDeposit.Name = "btnDeposit";
            btnDeposit.Size = new Size(128, 38);
            btnDeposit.TabIndex = 10;
            btnDeposit.Text = "Deposit ";
            btnDeposit.UseVisualStyleBackColor = true;
            btnDeposit.Click += btnDeposit_Click;
            // 
            // btnWithdraw
            // 
            btnWithdraw.Location = new Point(157, 338);
            btnWithdraw.Name = "btnWithdraw";
            btnWithdraw.Size = new Size(169, 38);
            btnWithdraw.TabIndex = 11;
            btnWithdraw.Text = "Withdraw";
            btnWithdraw.UseVisualStyleBackColor = true;
            btnWithdraw.Click += btnWithdraw_Click;
            // 
            // lblTransactionMessage
            // 
            lblTransactionMessage.AutoSize = true;
            lblTransactionMessage.Location = new Point(190, 434);
            lblTransactionMessage.Name = "lblTransactionMessage";
            lblTransactionMessage.Size = new Size(16, 25);
            lblTransactionMessage.TabIndex = 12;
            lblTransactionMessage.Text = ":";
            // 
            // lblTransactionMessageText
            // 
            lblTransactionMessageText.AutoSize = true;
            lblTransactionMessageText.Location = new Point(12, 434);
            lblTransactionMessageText.Name = "lblTransactionMessageText";
            lblTransactionMessageText.Size = new Size(175, 25);
            lblTransactionMessageText.TabIndex = 13;
            lblTransactionMessageText.Text = "Transaction Message";
            // 
            // AccountsForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(833, 468);
            Controls.Add(lblTransactionMessageText);
            Controls.Add(lblTransactionMessage);
            Controls.Add(btnWithdraw);
            Controls.Add(btnDeposit);
            Controls.Add(txtAmount);
            Controls.Add(lblSelectedAccountIdText);
            Controls.Add(lblSelectedAccountId);
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
        private Label lblSelectedAccountId;
        private Label lblSelectedAccountIdText;
        private TextBox txtAmount;
        private Button btnDeposit;
        private Button btnWithdraw;
        private Label lblTransactionMessage;
        private Label lblTransactionMessageText;
    }
}