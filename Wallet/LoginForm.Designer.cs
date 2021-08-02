
namespace Wallet
{
	partial class LoginForm
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
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
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Miners here");
			System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Transactions here");
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.labelLogo = new System.Windows.Forms.Label();
			this.textBoxUsername = new System.Windows.Forms.TextBox();
			this.textBoxPassword = new System.Windows.Forms.TextBox();
			this.buttonLogin = new System.Windows.Forms.Button();
			this.labelIsLogged = new System.Windows.Forms.Label();
			this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
			this.labelBalance = new System.Windows.Forms.Label();
			this.treeViewMiners = new System.Windows.Forms.TreeView();
			this.treeViewTransactions = new System.Windows.Forms.TreeView();
			this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
			this.labelSendDuco = new System.Windows.Forms.Label();
			this.textBoxRecepientDuco = new System.Windows.Forms.TextBox();
			this.numericUpDownDuco = new System.Windows.Forms.NumericUpDown();
			this.buttonSendDuco = new System.Windows.Forms.Button();
			this.timerUpdater = new System.Windows.Forms.Timer(this.components);
			this.notifyTransactions = new System.Windows.Forms.NotifyIcon(this.components);
			this.tableLayoutPanel1.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			this.flowLayoutPanel2.SuspendLayout();
			this.flowLayoutPanel3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownDuco)).BeginInit();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel2, 1, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(812, 505);
			this.tableLayoutPanel1.TabIndex = 1;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Controls.Add(this.labelLogo);
			this.flowLayoutPanel1.Controls.Add(this.textBoxUsername);
			this.flowLayoutPanel1.Controls.Add(this.textBoxPassword);
			this.flowLayoutPanel1.Controls.Add(this.buttonLogin);
			this.flowLayoutPanel1.Controls.Add(this.labelIsLogged);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(4, 4);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(236, 497);
			this.flowLayoutPanel1.TabIndex = 1;
			// 
			// labelLogo
			// 
			this.labelLogo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelLogo.Font = new System.Drawing.Font("Segoe UI", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.labelLogo.Location = new System.Drawing.Point(3, 0);
			this.labelLogo.Name = "labelLogo";
			this.labelLogo.Size = new System.Drawing.Size(231, 137);
			this.labelLogo.TabIndex = 0;
			this.labelLogo.Text = "DuCoWallet";
			// 
			// textBoxUsername
			// 
			this.textBoxUsername.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxUsername.Location = new System.Drawing.Point(3, 140);
			this.textBoxUsername.Name = "textBoxUsername";
			this.textBoxUsername.PlaceholderText = "Username";
			this.textBoxUsername.Size = new System.Drawing.Size(231, 23);
			this.textBoxUsername.TabIndex = 0;
			// 
			// textBoxPassword
			// 
			this.textBoxPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxPassword.Location = new System.Drawing.Point(3, 169);
			this.textBoxPassword.Name = "textBoxPassword";
			this.textBoxPassword.PlaceholderText = "Password";
			this.textBoxPassword.Size = new System.Drawing.Size(231, 23);
			this.textBoxPassword.TabIndex = 1;
			// 
			// buttonLogin
			// 
			this.buttonLogin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonLogin.AutoSize = true;
			this.buttonLogin.Location = new System.Drawing.Point(3, 198);
			this.buttonLogin.Name = "buttonLogin";
			this.buttonLogin.Size = new System.Drawing.Size(231, 25);
			this.buttonLogin.TabIndex = 2;
			this.buttonLogin.Text = "Login";
			this.buttonLogin.UseVisualStyleBackColor = true;
			this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
			// 
			// labelIsLogged
			// 
			this.labelIsLogged.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.labelIsLogged.AutoSize = true;
			this.labelIsLogged.Location = new System.Drawing.Point(3, 226);
			this.labelIsLogged.Name = "labelIsLogged";
			this.labelIsLogged.Size = new System.Drawing.Size(231, 15);
			this.labelIsLogged.TabIndex = 3;
			this.labelIsLogged.Text = "Not logged";
			this.labelIsLogged.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// flowLayoutPanel2
			// 
			this.flowLayoutPanel2.Controls.Add(this.labelBalance);
			this.flowLayoutPanel2.Controls.Add(this.treeViewMiners);
			this.flowLayoutPanel2.Controls.Add(this.treeViewTransactions);
			this.flowLayoutPanel2.Controls.Add(this.flowLayoutPanel3);
			this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.flowLayoutPanel2.Location = new System.Drawing.Point(247, 4);
			this.flowLayoutPanel2.Name = "flowLayoutPanel2";
			this.flowLayoutPanel2.Size = new System.Drawing.Size(561, 497);
			this.flowLayoutPanel2.TabIndex = 2;
			// 
			// labelBalance
			// 
			this.labelBalance.AutoSize = true;
			this.labelBalance.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			this.labelBalance.Location = new System.Drawing.Point(3, 0);
			this.labelBalance.Name = "labelBalance";
			this.labelBalance.Size = new System.Drawing.Size(0, 32);
			this.labelBalance.TabIndex = 0;
			// 
			// treeViewMiners
			// 
			this.treeViewMiners.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeViewMiners.Location = new System.Drawing.Point(3, 35);
			this.treeViewMiners.MinimumSize = new System.Drawing.Size(200, 200);
			this.treeViewMiners.Name = "treeViewMiners";
			treeNode1.Name = "Node0";
			treeNode1.Text = "Miners here";
			this.treeViewMiners.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
			this.treeViewMiners.Size = new System.Drawing.Size(500, 200);
			this.treeViewMiners.TabIndex = 1;
			// 
			// treeViewTransactions
			// 
			this.treeViewTransactions.Location = new System.Drawing.Point(3, 241);
			this.treeViewTransactions.MinimumSize = new System.Drawing.Size(500, 4);
			this.treeViewTransactions.Name = "treeViewTransactions";
			treeNode2.Name = "Node0";
			treeNode2.Text = "Transactions here";
			this.treeViewTransactions.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2});
			this.treeViewTransactions.Size = new System.Drawing.Size(500, 97);
			this.treeViewTransactions.TabIndex = 4;
			// 
			// flowLayoutPanel3
			// 
			this.flowLayoutPanel3.Controls.Add(this.labelSendDuco);
			this.flowLayoutPanel3.Controls.Add(this.textBoxRecepientDuco);
			this.flowLayoutPanel3.Controls.Add(this.numericUpDownDuco);
			this.flowLayoutPanel3.Controls.Add(this.buttonSendDuco);
			this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel3.Enabled = false;
			this.flowLayoutPanel3.Location = new System.Drawing.Point(3, 344);
			this.flowLayoutPanel3.Name = "flowLayoutPanel3";
			this.flowLayoutPanel3.Size = new System.Drawing.Size(500, 100);
			this.flowLayoutPanel3.TabIndex = 3;
			// 
			// labelSendDuco
			// 
			this.labelSendDuco.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelSendDuco.AutoSize = true;
			this.labelSendDuco.Location = new System.Drawing.Point(3, 0);
			this.labelSendDuco.MinimumSize = new System.Drawing.Size(400, 0);
			this.labelSendDuco.Name = "labelSendDuco";
			this.labelSendDuco.Size = new System.Drawing.Size(400, 15);
			this.labelSendDuco.TabIndex = 3;
			this.labelSendDuco.Text = "Send DUCO coins to";
			// 
			// textBoxRecepientDuco
			// 
			this.textBoxRecepientDuco.Location = new System.Drawing.Point(3, 18);
			this.textBoxRecepientDuco.Name = "textBoxRecepientDuco";
			this.textBoxRecepientDuco.PlaceholderText = "Recepient";
			this.textBoxRecepientDuco.Size = new System.Drawing.Size(168, 23);
			this.textBoxRecepientDuco.TabIndex = 0;
			// 
			// numericUpDownDuco
			// 
			this.numericUpDownDuco.DecimalPlaces = 5;
			this.numericUpDownDuco.Location = new System.Drawing.Point(177, 18);
			this.numericUpDownDuco.Name = "numericUpDownDuco";
			this.numericUpDownDuco.Size = new System.Drawing.Size(120, 23);
			this.numericUpDownDuco.TabIndex = 1;
			// 
			// buttonSendDuco
			// 
			this.buttonSendDuco.Location = new System.Drawing.Point(303, 18);
			this.buttonSendDuco.Name = "buttonSendDuco";
			this.buttonSendDuco.Size = new System.Drawing.Size(167, 23);
			this.buttonSendDuco.TabIndex = 2;
			this.buttonSendDuco.Text = "Send DUCO";
			this.buttonSendDuco.UseVisualStyleBackColor = true;
			this.buttonSendDuco.Click += new System.EventHandler(this.buttonSendDuco_Click);
			// 
			// timerUpdater
			// 
			this.timerUpdater.Interval = 30000;
			// 
			// notifyTransactions
			// 
			this.notifyTransactions.BalloonTipText = "Received transaction";
			this.notifyTransactions.BalloonTipTitle = "DuCoWallet";
			this.notifyTransactions.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyTransactions.Icon")));
			this.notifyTransactions.Text = "notifyIcon1";
			this.notifyTransactions.Visible = true;
			// 
			// LoginForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(812, 465);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "LoginForm";
			this.Text = "DuCoWallet";
			this.tableLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.PerformLayout();
			this.flowLayoutPanel2.ResumeLayout(false);
			this.flowLayoutPanel2.PerformLayout();
			this.flowLayoutPanel3.ResumeLayout(false);
			this.flowLayoutPanel3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownDuco)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.TextBox textBoxUsername;
		private System.Windows.Forms.TextBox textBoxPassword;
		private System.Windows.Forms.Button buttonLogin;
		private System.Windows.Forms.Label labelIsLogged;
		private System.Windows.Forms.Label labelLogo;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
		private System.Windows.Forms.Label labelBalance;
		private System.Windows.Forms.TreeView treeViewMiners;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
		private System.Windows.Forms.Label labelSendDuco;
		private System.Windows.Forms.TextBox textBoxRecepientDuco;
		private System.Windows.Forms.NumericUpDown numericUpDownDuco;
		private System.Windows.Forms.Button buttonSendDuco;
		private System.Windows.Forms.TreeView treeViewTransactions;
		private System.Windows.Forms.Timer timerUpdater;
		private System.Windows.Forms.NotifyIcon notifyTransactions;
	}
}

