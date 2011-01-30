namespace Knowledge.Prospector.UIEnvironment.Plugins
{
	partial class LocalDocumentsProviderOptionsPlugin
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
			System.Windows.Forms.Button cmdBrowseFolder;
			this.lblFilter = new System.Windows.Forms.Label();
			this.txtFilter = new System.Windows.Forms.TextBox();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.txtFolder = new System.Windows.Forms.TextBox();
			this.lblFolder = new System.Windows.Forms.Label();
			this.cmdOk = new System.Windows.Forms.Button();
			cmdBrowseFolder = new System.Windows.Forms.Button();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// cmdBrowseFolder
			// 
			cmdBrowseFolder.Dock = System.Windows.Forms.DockStyle.Fill;
			cmdBrowseFolder.Location = new System.Drawing.Point(616, 3);
			cmdBrowseFolder.Name = "cmdBrowseFolder";
			cmdBrowseFolder.Size = new System.Drawing.Size(114, 20);
			cmdBrowseFolder.TabIndex = 24;
			cmdBrowseFolder.Text = "Browse";
			cmdBrowseFolder.Click += new System.EventHandler(this.cmdBrowseFolder_Click);
			// 
			// lblFilter
			// 
			this.lblFilter.AutoSize = true;
			this.lblFilter.Location = new System.Drawing.Point(3, 26);
			this.lblFilter.Name = "lblFilter";
			this.lblFilter.Size = new System.Drawing.Size(29, 13);
			this.lblFilter.TabIndex = 39;
			this.lblFilter.Text = "Filter";
			// 
			// txtFilter
			// 
			this.txtFilter.AllowDrop = true;
			this.txtFilter.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtFilter.Location = new System.Drawing.Point(123, 29);
			this.txtFilter.Name = "txtFilter";
			this.txtFilter.Size = new System.Drawing.Size(487, 20);
			this.txtFilter.TabIndex = 40;
			this.txtFilter.Text = "*.txt";
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel1.ColumnCount = 3;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
			this.tableLayoutPanel1.Controls.Add(this.cmdCancel, 2, 2);
			this.tableLayoutPanel1.Controls.Add(this.txtFilter, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.txtFolder, 1, 0);
			this.tableLayoutPanel1.Controls.Add(cmdBrowseFolder, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.lblFolder, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.lblFilter, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.cmdOk, 1, 2);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(733, 79);
			this.tableLayoutPanel1.TabIndex = 42;
			// 
			// cmdCancel
			// 
			this.cmdCancel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cmdCancel.Location = new System.Drawing.Point(616, 55);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(114, 23);
			this.cmdCancel.TabIndex = 1;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// txtFolder
			// 
			this.txtFolder.AllowDrop = true;
			this.txtFolder.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtFolder.Location = new System.Drawing.Point(123, 3);
			this.txtFolder.Name = "txtFolder";
			this.txtFolder.Size = new System.Drawing.Size(487, 20);
			this.txtFolder.TabIndex = 23;
			// 
			// lblFolder
			// 
			this.lblFolder.AutoSize = true;
			this.lblFolder.Location = new System.Drawing.Point(3, 0);
			this.lblFolder.Name = "lblFolder";
			this.lblFolder.Size = new System.Drawing.Size(36, 13);
			this.lblFolder.TabIndex = 33;
			this.lblFolder.Text = "Folder";
			this.lblFolder.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// cmdOk
			// 
			this.cmdOk.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cmdOk.Location = new System.Drawing.Point(123, 55);
			this.cmdOk.Name = "cmdOk";
			this.cmdOk.Size = new System.Drawing.Size(487, 23);
			this.cmdOk.TabIndex = 41;
			this.cmdOk.Text = "OK";
			this.cmdOk.UseVisualStyleBackColor = true;
			this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
			// 
			// LocalDocumentsProviderOptionsPlugin
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(733, 79);
			this.Controls.Add(this.tableLayoutPanel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "LocalDocumentsProviderOptionsPlugin";
			this.Text = "LocalDocumentsProviderOptions";
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label lblFilter;
		private System.Windows.Forms.TextBox txtFilter;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.TextBox txtFolder;
		private System.Windows.Forms.Label lblFolder;
		private System.Windows.Forms.Button cmdCancel;
		private System.Windows.Forms.Button cmdOk;
	}
}