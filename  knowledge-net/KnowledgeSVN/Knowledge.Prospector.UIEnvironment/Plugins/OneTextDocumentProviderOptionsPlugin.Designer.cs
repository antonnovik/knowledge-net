namespace Knowledge.Prospector.UIEnvironment.Plugins
{
	partial class OneTextDocumentProviderOptionsPlugin
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
			System.Windows.Forms.Button cmdBrowseFileName;
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.txtFileName = new System.Windows.Forms.TextBox();
			this.lblFileName = new System.Windows.Forms.Label();
			this.cmdOk = new System.Windows.Forms.Button();
			cmdBrowseFileName = new System.Windows.Forms.Button();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel1.ColumnCount = 3;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
			this.tableLayoutPanel1.Controls.Add(this.cmdCancel, 2, 1);
			this.tableLayoutPanel1.Controls.Add(this.txtFileName, 1, 0);
			this.tableLayoutPanel1.Controls.Add(cmdBrowseFileName, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.lblFileName, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.cmdOk, 1, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(622, 55);
			this.tableLayoutPanel1.TabIndex = 43;
			// 
			// cmdCancel
			// 
			this.cmdCancel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cmdCancel.Location = new System.Drawing.Point(505, 29);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(114, 23);
			this.cmdCancel.TabIndex = 1;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// txtFileName
			// 
			this.txtFileName.AllowDrop = true;
			this.txtFileName.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtFileName.Location = new System.Drawing.Point(123, 3);
			this.txtFileName.Name = "txtFileName";
			this.txtFileName.Size = new System.Drawing.Size(376, 20);
			this.txtFileName.TabIndex = 23;
			// 
			// cmdBrowseFileName
			// 
			cmdBrowseFileName.Dock = System.Windows.Forms.DockStyle.Fill;
			cmdBrowseFileName.Location = new System.Drawing.Point(505, 3);
			cmdBrowseFileName.Name = "cmdBrowseFileName";
			cmdBrowseFileName.Size = new System.Drawing.Size(114, 20);
			cmdBrowseFileName.TabIndex = 24;
			cmdBrowseFileName.Text = "Browse";
			cmdBrowseFileName.Click += new System.EventHandler(this.cmdBrowseFileName_Click);
			// 
			// lblFileName
			// 
			this.lblFileName.AutoSize = true;
			this.lblFileName.Location = new System.Drawing.Point(3, 0);
			this.lblFileName.Name = "lblFileName";
			this.lblFileName.Size = new System.Drawing.Size(52, 13);
			this.lblFileName.TabIndex = 33;
			this.lblFileName.Text = "File name";
			this.lblFileName.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// cmdOk
			// 
			this.cmdOk.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cmdOk.Location = new System.Drawing.Point(123, 29);
			this.cmdOk.Name = "cmdOk";
			this.cmdOk.Size = new System.Drawing.Size(376, 23);
			this.cmdOk.TabIndex = 41;
			this.cmdOk.Text = "OK";
			this.cmdOk.UseVisualStyleBackColor = true;
			this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
			// 
			// OneTextDocumentProviderOptionsPlugin
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(622, 55);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "OneTextDocumentProviderOptionsPlugin";
			this.Text = "OneTextDocumentProviderOptionsPlugin";
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Button cmdCancel;
		private System.Windows.Forms.TextBox txtFileName;
		private System.Windows.Forms.Label lblFileName;
		private System.Windows.Forms.Button cmdOk;
	}
}