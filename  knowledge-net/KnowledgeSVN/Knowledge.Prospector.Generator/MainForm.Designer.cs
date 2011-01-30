namespace Knowledge.Prospector.Generator
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
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPagePOS = new System.Windows.Forms.TabPage();
			this.bBrowseOutputFile = new System.Windows.Forms.Button();
			this.tbOutputFile = new System.Windows.Forms.TextBox();
			this.bBrowseInputFile = new System.Windows.Forms.Button();
			this.tbInputFile = new System.Windows.Forms.TextBox();
			this.tabPageGC = new System.Windows.Forms.TabPage();
			this.bGenerate = new System.Windows.Forms.Button();
			this.tabControl1.SuspendLayout();
			this.tabPagePOS.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPagePOS);
			this.tabControl1.Controls.Add(this.tabPageGC);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(656, 247);
			this.tabControl1.TabIndex = 0;
			// 
			// tabPagePOS
			// 
			this.tabPagePOS.Controls.Add(this.bGenerate);
			this.tabPagePOS.Controls.Add(this.bBrowseOutputFile);
			this.tabPagePOS.Controls.Add(this.tbOutputFile);
			this.tabPagePOS.Controls.Add(this.bBrowseInputFile);
			this.tabPagePOS.Controls.Add(this.tbInputFile);
			this.tabPagePOS.Location = new System.Drawing.Point(4, 22);
			this.tabPagePOS.Name = "tabPagePOS";
			this.tabPagePOS.Padding = new System.Windows.Forms.Padding(3);
			this.tabPagePOS.Size = new System.Drawing.Size(648, 221);
			this.tabPagePOS.TabIndex = 0;
			this.tabPagePOS.Text = "PartOfSpeeches";
			// 
			// bBrowseOutputFile
			// 
			this.bBrowseOutputFile.Location = new System.Drawing.Point(436, 39);
			this.bBrowseOutputFile.Name = "bBrowseOutputFile";
			this.bBrowseOutputFile.Size = new System.Drawing.Size(75, 20);
			this.bBrowseOutputFile.TabIndex = 23;
			this.bBrowseOutputFile.Text = "Browse";
			this.bBrowseOutputFile.Click += new System.EventHandler(this.bBrowseOutputFile_Click);
			// 
			// tbOutputFile
			// 
			this.tbOutputFile.Location = new System.Drawing.Point(8, 38);
			this.tbOutputFile.Name = "tbOutputFile";
			this.tbOutputFile.Size = new System.Drawing.Size(422, 20);
			this.tbOutputFile.TabIndex = 22;
			// 
			// bBrowseInputFile
			// 
			this.bBrowseInputFile.Location = new System.Drawing.Point(436, 12);
			this.bBrowseInputFile.Name = "bBrowseInputFile";
			this.bBrowseInputFile.Size = new System.Drawing.Size(75, 20);
			this.bBrowseInputFile.TabIndex = 21;
			this.bBrowseInputFile.Text = "Browse";
			this.bBrowseInputFile.Click += new System.EventHandler(this.bBrowseInputFile_Click);
			// 
			// tbInputFile
			// 
			this.tbInputFile.Location = new System.Drawing.Point(8, 12);
			this.tbInputFile.Name = "tbInputFile";
			this.tbInputFile.Size = new System.Drawing.Size(422, 20);
			this.tbInputFile.TabIndex = 20;
			// 
			// tabPageGC
			// 
			this.tabPageGC.Location = new System.Drawing.Point(4, 22);
			this.tabPageGC.Name = "tabPageGC";
			this.tabPageGC.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageGC.Size = new System.Drawing.Size(648, 221);
			this.tabPageGC.TabIndex = 1;
			this.tabPageGC.Text = "GrammaticCategories";
			// 
			// bGenerate
			// 
			this.bGenerate.Location = new System.Drawing.Point(9, 65);
			this.bGenerate.Name = "bGenerate";
			this.bGenerate.Size = new System.Drawing.Size(75, 23);
			this.bGenerate.TabIndex = 24;
			this.bGenerate.Text = "Generate";
			this.bGenerate.Click += new System.EventHandler(this.bGenerate_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(656, 247);
			this.Controls.Add(this.tabControl1);
			this.Name = "MainForm";
			this.Text = "Generator";
			this.Load += new System.EventHandler(this.FormGenerator_Load);
			this.tabControl1.ResumeLayout(false);
			this.tabPagePOS.ResumeLayout(false);
			this.tabPagePOS.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPagePOS;
		private System.Windows.Forms.TabPage tabPageGC;
		private System.Windows.Forms.Button bBrowseOutputFile;
		private System.Windows.Forms.TextBox tbOutputFile;
		private System.Windows.Forms.Button bBrowseInputFile;
		private System.Windows.Forms.TextBox tbInputFile;
		private System.Windows.Forms.Button bGenerate;
	}
}

