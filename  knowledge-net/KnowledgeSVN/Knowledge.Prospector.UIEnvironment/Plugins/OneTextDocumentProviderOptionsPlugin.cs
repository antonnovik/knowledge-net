using System;
using System.Windows.Forms;
using System.Xml;
using Knowledge.Prospector.Common.Settings;
using Knowledge.Prospector.Common.Utilites;

namespace Knowledge.Prospector.UIEnvironment.Plugins
{
	public partial class OneTextDocumentProviderOptionsPlugin : Form, IProviderOptionsPlugin
	{
		private bool ok = false;
	
		private string CreateProviderOptions()
		{
			XmlDocument doc = new XmlDocument();
			XmlElement rootElement = doc.CreateElement("options");
			doc.AppendChild(rootElement);

			XmlElement child1 = doc.CreateElement("file");
			child1.AppendChild(doc.CreateTextNode(txtFileName.Text));
			rootElement.AppendChild(child1);

			return doc.OuterXml;
		}
		
		private void ParseProviderOptions(string options)
		{
			try
			{
				XmlNode xmlConfig = XmlUtils.GetNode(options);
				txtFileName.Text = xmlConfig.SelectSingleNode(".//file").InnerText;
			}catch{}
		}

		public OneTextDocumentProviderOptionsPlugin()
		{
			InitializeComponent();
		}

		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed(e);
			if (OnPluginClosed != null)
				OnPluginClosed(this, new PluginClosedEventArgs(ok));
		}

		#region IProviderOptionsPlugin Members

		public bool ShowPlugin()
		{
			ok = false;
			return ShowDialog() == DialogResult.OK;
		}

		public string ProviderOptions
		{
			get { return CreateProviderOptions(); }
			set { ParseProviderOptions(value); }
		}

		public event PluginClosedEventHandler OnPluginClosed;

		#endregion

		private void cmdBrowseFileName_Click(object sender, EventArgs e)
		{
			Utilities.OpenFile(txtFileName, PathUtils.DataPath);
		}

		private void cmdCancel_Click(object sender, EventArgs e)
		{
			ok = false;
			Close();
		}

		private void cmdOk_Click(object sender, EventArgs e)
		{
			CreateProviderOptions();
			ok = true;
			Close();
		}
	}
}