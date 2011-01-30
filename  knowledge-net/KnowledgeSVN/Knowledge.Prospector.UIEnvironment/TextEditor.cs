using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Knowledge.Prospector.UIEnvironment
{
	public partial class TextEditor : Form
	{
		public string OpenedFileName = string.Empty;
		public TextEditor()
		{
			InitializeComponent();
		}

		internal bool Open(string FileName)
		{
			StreamReader sr = new StreamReader(FileName);
			Editor.Text = sr.ReadToEnd();
			OpenedFileName = FileName;
			sr.Close();
			return true;
		}

		internal void Save()
		{
			StreamWriter sw = new StreamWriter(OpenedFileName);
			sw.Write(Editor.Text);
			sw.Close();
		}
	}
}