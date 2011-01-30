using System;
using System.Collections.Generic;
using System.Text;
using EnvDTE;
using EnvDTE80;

namespace Knowledge.Editor.AddIn
{
	public class Environment
	{
		private DTE2 app;
		private OutputWindowPane outputWindowPane;
		private string addinBaseDir;
		private string aNFWindowName = "Knowledge.NET Framework";

		public Environment()
		{
			app = null;
			outputWindowPane = null;
			addinBaseDir = null;
		}

		public DTE2 ApplicationObject
		{
			get
			{
				return app;
			}
			set
			{
				app = value;
			}
		}

		public OutputWindowPane OutputWindowPane
		{
			get
			{
				return outputWindowPane;
			}
			set
			{
				outputWindowPane = value;
			}
		}

		public string KnowledgeNetFrameworkBaseDir
		{
			get
			{
				return addinBaseDir;
			}
			set
			{
				addinBaseDir = value;
			}
		}

		public string ANFWindowName
		{
			get
			{
				return aNFWindowName;
			}
			set
			{
				aNFWindowName = value;
			}
		}

		public static bool isInArray(object[] array, object value)
		{
			try
			{
				foreach (object obj in array)
				{
					if (obj.ToString() == value.ToString())
						return true;
				}
			}
			catch (Exception)
			{
				return false;
			}
			return false;
		}

	}
}
