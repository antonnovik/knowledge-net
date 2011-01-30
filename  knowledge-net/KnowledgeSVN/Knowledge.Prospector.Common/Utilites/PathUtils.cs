using System;
using System.Collections.Generic;
using System.Text;

namespace Knowledge.Prospector.Common.Utilites
{
	public static class PathUtils
	{
		private static string _DataPath;

		public static string DataPath
		{
			get { return _DataPath; }
			set { _DataPath = value; }
		}

		public static readonly string DataPathShortcut = "$data";
		
		public static string Decode(string path)
		{
			return path.Replace(DataPathShortcut, DataPath);
		}

		public static string Encode(string path)
		{
			if(!string.IsNullOrEmpty(path) && !string.IsNullOrEmpty(DataPath))
				return path.Replace(DataPath, DataPathShortcut);
			else
				return path;
		}
	}
}
