using System;
using System.IO;
using System.Windows.Forms;
using Knowledge.Prospector.Common.Utilites;

namespace Knowledge.Prospector.UIEnvironment
{
	public static class Utilities
	{
		public static string OpenFile(string initPath)
		{
			OpenFileDialog ofd = new OpenFileDialog();

			if (!string.IsNullOrEmpty(initPath))
				try
				{
					ofd.InitialDirectory = GetDirectory(initPath);
				}
				catch { }

			if (ofd.ShowDialog() == DialogResult.OK)
				return ofd.FileName;
			return string.Empty;
		}

		public static void OpenFile(TextBox tbFileName, string defaultInitPath)
		{
			string selectedFile;
			if (!string.IsNullOrEmpty(tbFileName.Text))
			{
				try
				{
					selectedFile = OpenFile(Path.GetDirectoryName(PathUtils.Decode(tbFileName.Text)));
				}
				catch
				{
					selectedFile = OpenFile(defaultInitPath);
				}
			}
			else
				selectedFile = OpenFile(defaultInitPath);

			if (!string.IsNullOrEmpty(selectedFile))
			{
				tbFileName.Text = PathUtils.Encode(selectedFile);
			}
		}
		
		private static string AppendSlash(string path)
		{
			return path.EndsWith("\\") ? path : path + "\\";
		}
		
		private static string GetDirectory(string path)
		{
			return Path.GetDirectoryName(PathUtils.Decode(AppendSlash(path)));
		}

		public static string OpenFolder(string initPath)
		{
			FolderBrowserDialog fbd = new FolderBrowserDialog();
			fbd.ShowNewFolderButton = false;
			fbd.RootFolder = Environment.SpecialFolder.MyComputer;

			if (!string.IsNullOrEmpty(initPath))
				try
				{
					fbd.SelectedPath = GetDirectory(initPath);
				}
				catch{}

			if (fbd.ShowDialog() == DialogResult.OK)
				return fbd.SelectedPath;
			return string.Empty;
		}

		public static void OpenFolder(TextBox tbFolderName, string defaultInitPath)
		{
			string selectedFolder;
			if (!string.IsNullOrEmpty(tbFolderName.Text))
			{
				try
				{
					selectedFolder = OpenFolder(GetDirectory(tbFolderName.Text));
				}
				catch
				{
					selectedFolder = OpenFolder(defaultInitPath);
				}
			}
			else
				selectedFolder = OpenFolder(defaultInitPath);

			if (!string.IsNullOrEmpty(selectedFolder))
			{
				tbFolderName.Text = PathUtils.Encode(selectedFolder);
			}
		}
	}
}
