using System;
using System.Xml;

namespace Knowledge.Prospector.Common.Settings
{
	public delegate void PluginClosedEventHandler(IProviderOptionsPlugin sender, PluginClosedEventArgs e);

	public interface IProviderOptionsPlugin
	{
		bool ShowPlugin();

		string ProviderOptions { get;set;}

		event PluginClosedEventHandler OnPluginClosed;
	}
}
