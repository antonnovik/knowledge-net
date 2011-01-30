using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Knowledge.Prospector.Common.Utilites;

namespace Knowledge.Prospector.UIEnvironment
{
	public partial class MainContainer : Form
	{
		private MdiClient ClientArea = null;
		private KnowledgeBuilder knowledgeBuilder;
		private OptionsEditor optionsEditor;

		public MainContainer()
		{
			PathUtils.DataPath = Path.GetFullPath(@"..\..\Data");

			InitializeComponent();
			foreach (Control ctl in this.Controls)
			{
				if (ctl is MdiClient)
				{
					ClientArea = ctl as MdiClient;
					break;
				}
			}

			knowledgeBuilderToolStripMenuItem.Checked = true;

			//this.ClientSize = this.Size;
		}

		private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void CutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// TODO: Use System.Windows.Forms.Clipboard to insert the selected text or images into the clipboard
		}

		private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// TODO: Use System.Windows.Forms.Clipboard to insert the selected text or images into the clipboard
		}

		private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// TODO: Use System.Windows.Forms.Clipboard.GetText() or System.Windows.Forms.GetData to retrieve information from the clipboard.
		}

		private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
		{
			toolStrip.Visible = toolBarToolStripMenuItem.Checked;
		}

		private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
		{
			statusStrip.Visible = statusBarToolStripMenuItem.Checked;
		}

		private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			LayoutMdi(MdiLayout.Cascade);
		}

		private void TileVerticleToolStripMenuItem_Click(object sender, EventArgs e)
		{
			LayoutMdi(MdiLayout.TileVertical);
		}

		private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
		{
			LayoutMdi(MdiLayout.TileHorizontal);
		}

		private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			LayoutMdi(MdiLayout.ArrangeIcons);
		}

		private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
		{
			foreach (Form childForm in MdiChildren)
			{
				childForm.Close();
			}
		}

		private void xmldictionaryToolStripMenuItem_Click(object sender, EventArgs e)
		{

		}

		private void templateToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.InitialDirectory = PathUtils.DataPath;
			openFileDialog.Filter = "Template Files (*.txt)|*.txt";
			if (DialogResult.OK == openFileDialog.ShowDialog(this))
			{
				string FileName = openFileDialog.FileName;
				TextEditor te = new TextEditor();
				if (te.Open(FileName))
				{
					te.MdiParent = this;
					te.Show();
					te.Size = ClientArea.Size- new Size(20,20);
				}
				else
				{
					te.Dispose();
				}
			}
		}

		private void saveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (ActiveMdiChild is TextEditor)
			{
				(ActiveMdiChild as TextEditor).Save();
			}
		}

		private void knowledgeBuilderToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (knowledgeBuilderToolStripMenuItem.Checked)
			{
				if (knowledgeBuilder == null)
				{
					knowledgeBuilder = new KnowledgeBuilder();
					knowledgeBuilder.MdiParent = this;
					knowledgeBuilder.FormClosed += new FormClosedEventHandler(knowledgeBuilder_FormClosed);
					knowledgeBuilder.Show();
				}
				else
				{
					knowledgeBuilder.Visible = true;
				}
			}
			else
			{
				if (knowledgeBuilder != null)
				{
					knowledgeBuilder.Dispose();
					knowledgeBuilder = null;
				}
			}
		}

		void knowledgeBuilder_FormClosed(object sender, FormClosedEventArgs e)
		{
			knowledgeBuilderToolStripMenuItem.Checked = false;
		}

		private void optionsToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			if (optionsToolStripMenuItem1.Checked)
			{
				if (optionsEditor == null)
				{
					optionsEditor = new OptionsEditor();
					optionsEditor.MdiParent = this;
					optionsEditor.FormClosed += new FormClosedEventHandler(optionsEditor_FormClosed);
					optionsEditor.Show();
				}
				else
				{
					optionsEditor.Visible = true;
				}
			}
			else
			{
				if (optionsEditor != null)
				{
					optionsEditor.Dispose();
					optionsEditor = null;
				}
			}
		}

		void optionsEditor_FormClosed(object sender, FormClosedEventArgs e)
		{
			optionsToolStripMenuItem1.Checked = false;			
		}
	}
}