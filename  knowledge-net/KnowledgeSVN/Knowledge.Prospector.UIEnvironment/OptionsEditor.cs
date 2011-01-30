using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Knowledge.Prospector.Dictionaries;
using Knowledge.Prospector.Common.Settings;

namespace Knowledge.Prospector.UIEnvironment
{
	public partial class OptionsEditor : Form
	{
		public OptionsEditor()
		{
			InitializeComponent();
			clbDictionaries.BeginUpdate();
			//foreach (OneFileOptions xmlDictionaryOptions in MainContainer.options.OptionsOfDictionaryManager.XmlDictionariesOptions)
			//{
			//    clbDictionaries.Items.Add(xmlDictionaryOptions.FileName, true);
			//}
			clbDictionaries.EndUpdate();

		}

		private void bResetOptions_Click(object sender, EventArgs e)
		{
	//		MainContainer.options.Reset(MainContainer.DataPath);
		}

		private void bSaveOptions_Click(object sender, EventArgs e)
		{
	//		MainContainer.options.Save(MainContainer.DataPath + @"\Options.xml");
		}

		private void treeViewOptions_AfterSelect(object sender, TreeViewEventArgs e)
		{
			panelDictionaryManagerOptions.Visible = false;
			switch (e.Node.Name)
			{
				case "NodeDictionaryGeneral":
					panelDictionaryManagerOptions.Visible = true;
					break;
			}

		}
	}
}