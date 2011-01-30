namespace Knowledge.Prospector.UIEnvironment
{
	partial class OptionsEditor
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
			System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("General");
			System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Russian");
			System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Xml Dictionary", new System.Windows.Forms.TreeNode[] {
            treeNode2});
			System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Russian");
			System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Mrd Dictionary", new System.Windows.Forms.TreeNode[] {
            treeNode4});
			System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Dictionary", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode3,
            treeNode5});
			System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Templates");
			System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Semantic Analysis", new System.Windows.Forms.TreeNode[] {
            treeNode7});
			this.bResetOptions = new System.Windows.Forms.Button();
			this.bSaveOptions = new System.Windows.Forms.Button();
			this.panelDictionaryManagerOptions = new System.Windows.Forms.Panel();
			this.clbDictionaries = new System.Windows.Forms.CheckedListBox();
			this.treeViewOptions = new System.Windows.Forms.TreeView();
			this.panelDictionaryManagerOptions.SuspendLayout();
			this.SuspendLayout();
			// 
			// bResetOptions
			// 
			this.bResetOptions.Location = new System.Drawing.Point(90, 335);
			this.bResetOptions.Name = "bResetOptions";
			this.bResetOptions.Size = new System.Drawing.Size(75, 23);
			this.bResetOptions.TabIndex = 7;
			this.bResetOptions.Text = "Reset";
			this.bResetOptions.Click += new System.EventHandler(this.bResetOptions_Click);
			// 
			// bSaveOptions
			// 
			this.bSaveOptions.Location = new System.Drawing.Point(8, 336);
			this.bSaveOptions.Name = "bSaveOptions";
			this.bSaveOptions.Size = new System.Drawing.Size(75, 23);
			this.bSaveOptions.TabIndex = 6;
			this.bSaveOptions.Text = "Save";
			this.bSaveOptions.Click += new System.EventHandler(this.bSaveOptions_Click);
			// 
			// panelDictionaryManagerOptions
			// 
			this.panelDictionaryManagerOptions.Controls.Add(this.clbDictionaries);
			this.panelDictionaryManagerOptions.Dock = System.Windows.Forms.DockStyle.Right;
			this.panelDictionaryManagerOptions.Location = new System.Drawing.Point(184, 0);
			this.panelDictionaryManagerOptions.Name = "panelDictionaryManagerOptions";
			this.panelDictionaryManagerOptions.Size = new System.Drawing.Size(442, 368);
			this.panelDictionaryManagerOptions.TabIndex = 5;
			this.panelDictionaryManagerOptions.Visible = false;
			// 
			// clbDictionaries
			// 
			this.clbDictionaries.FormattingEnabled = true;
			this.clbDictionaries.Location = new System.Drawing.Point(3, 37);
			this.clbDictionaries.Name = "clbDictionaries";
			this.clbDictionaries.Size = new System.Drawing.Size(436, 327);
			this.clbDictionaries.TabIndex = 0;
			// 
			// treeViewOptions
			// 
			this.treeViewOptions.HideSelection = false;
			this.treeViewOptions.Location = new System.Drawing.Point(2, 3);
			this.treeViewOptions.Name = "treeViewOptions";
			treeNode1.Name = "NodeDictionaryGeneral";
			treeNode1.Text = "General";
			treeNode2.Name = "NodeDictionaryXmlRussian";
			treeNode2.Text = "Russian";
			treeNode3.Name = "NodeDictionaryXml";
			treeNode3.Text = "Xml Dictionary";
			treeNode4.Name = "NodeDictionaryMrdRussian";
			treeNode4.Text = "Russian";
			treeNode5.Name = "NodeDictionaryMrd";
			treeNode5.Text = "Mrd Dictionary";
			treeNode6.Name = "NodeDictionary";
			treeNode6.Text = "Dictionary";
			treeNode7.Name = "Templates";
			treeNode7.Text = "Templates";
			treeNode8.Name = "SemanticAnalysis";
			treeNode8.Text = "Semantic Analysis";
			this.treeViewOptions.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode6,
            treeNode8});
			this.treeViewOptions.Size = new System.Drawing.Size(180, 327);
			this.treeViewOptions.TabIndex = 4;
			this.treeViewOptions.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewOptions_AfterSelect);
			// 
			// OptionsEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(626, 368);
			this.Controls.Add(this.bResetOptions);
			this.Controls.Add(this.bSaveOptions);
			this.Controls.Add(this.panelDictionaryManagerOptions);
			this.Controls.Add(this.treeViewOptions);
			this.Name = "OptionsEditor";
			this.Text = "OptionsEditor";
			this.panelDictionaryManagerOptions.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button bResetOptions;
		private System.Windows.Forms.Button bSaveOptions;
		private System.Windows.Forms.Panel panelDictionaryManagerOptions;
		private System.Windows.Forms.CheckedListBox clbDictionaries;
		private System.Windows.Forms.TreeView treeViewOptions;
	}
}