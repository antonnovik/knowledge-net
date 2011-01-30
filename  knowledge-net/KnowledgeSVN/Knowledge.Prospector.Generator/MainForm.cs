using System;
using System.IO;
using System.Windows.Forms;

namespace Knowledge.Prospector.Generator
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
		}

		public static string DataPath
		{
			get
			{
				string result = Application.StartupPath;
				result = result.Replace(@"\bin\Debug", string.Empty);
				return result + @"\Data";
			}
		}

		private string OpenFile(string initPath)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.InitialDirectory = initPath;
			if (ofd.ShowDialog().ToString() == "OK")
			{
				return ofd.FileName;
			}
			return string.Empty;
		}

		string[] FileNames;
		private void bBrowseInputFile_Click(object sender, EventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Multiselect = true;
			ofd.InitialDirectory = DataPath + @"\Sources";
			if (ofd.ShowDialog().ToString() == "OK")
			{
				FileNames = ofd.FileNames;

				if (FileNames != null && FileNames.Length == 1)
				{
					tbInputFile.Text = FileNames[0];
					tbOutputFile.Text = FileNames[0].Replace("Sources", "Results");
				}
			}
		}

		private void bBrowseOutputFile_Click(object sender, EventArgs e)
		{
			string fileName = OpenFile(DataPath + @"\Results");
			if (fileName != string.Empty)
				tbOutputFile.Text = fileName;
		}

		private void FormGenerator_Load(object sender, EventArgs e)
		{
			tbInputFile.Text = DataPath + @"\Sources\PartOfSpeeches.txt";
			tbOutputFile.Text = DataPath + @"\Results\Generated.txt";
		}

		private void bGenerate_Click(object sender, EventArgs e)
		{
			if (FileNames != null && FileNames.Length != 0)
			{
				StreamWriter sw = new StreamWriter(tbOutputFile.Text);

				sw.WriteLine("using System.Collections.Generic;");
				sw.WriteLine("using System;");
				
				foreach (string filename in FileNames)
				{
					PartOfSpeechGenerator gen = new PartOfSpeechGenerator();
					gen.Generate(filename, sw);
				}
				MessageBox.Show("Generated");

				sw.Close();
			}
		}


	}
}