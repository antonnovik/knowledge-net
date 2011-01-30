using System;
using System.Collections.Generic;
using System.Text;

namespace Knowledge.Prospector.Common.Settings
{
	public class PluginClosedEventArgs : EventArgs
	{
		private bool _OK;
		public bool OK
		{
			get { return _OK; }
			set { _OK = value; }
		}
		
		public PluginClosedEventArgs(bool _OK)
		{
			this._OK = _OK;
		}
	}
}
