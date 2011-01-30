#define RunBuildingKnowledgeInNewThread7

using System;
using System.Configuration;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using Knowledge.Prospector.Common.Settings;
using Knowledge.Prospector.Common.Utilites;
using Knowledge.Prospector.Data.Collections;
using Knowledge.Prospector.Dictionaries;
using Knowledge.Prospector.IO;
using Knowledge.Prospector.IO.Articles;
using Knowledge.Prospector.IO.Documents;
using Knowledge.Prospector.IO.Documents.Providers;
using Knowledge.Prospector.Templates;
using Knowledge.Prospector.Templates.Handlers;
using Knowledge.Prospector.UIEnvironment.Resources;

namespace Knowledge.Prospector.UIEnvironment
{
	public partial class KnowledgeBuilder : Form
	{
		private ProviderConfiguration<IOntology> _OnologyConfig = ConfigurationManager.GetSection("ontologies") as ProviderConfiguration<IOntology>;
		private ProviderConfiguration<IDocumentProvider> _DocumentProviderConfig = ConfigurationManager.GetSection("documentProviders") as ProviderConfiguration<IDocumentProvider>;
		
		public KnowledgeBuilder()
		{
			InitializeComponent();
		}

		private void Main_Load(object sender, EventArgs e)
		{
			txtDocumentsProviderSettings.Text = UISettings.Default.DocumentsProviderSettings;

			txtOutputFileName.Text = UISettings.Default.OutputOntologySettings;
			txtLoadFromDumpFileName.Text = UISettings.Default.LoadFromDumpFileName;
			txtAddToDumpFileName.Text = UISettings.Default.SaveToDumpFileName;

			cbOutputOntologyProvider.Items.AddRange(_OnologyConfig.Providers.ToArray());
			cbOutputOntologyProvider.SelectedIndex = _OnologyConfig.GetIndex(UISettings.Default.OutputOntologyProviderName);

			cbDocumentProviderProvider.Items.AddRange(_DocumentProviderConfig.Providers.ToArray());
			cbDocumentProviderProvider.SelectedIndex = _DocumentProviderConfig.GetIndex(UISettings.Default.DocumentsProviderProviderName);
		}

		private void cbLoadFromDump_CheckedChanged(object sender, EventArgs e)
		{
			cmdBrowseLoadFromDumpFileName.Visible = cbLoadFromDump.Checked;
			txtLoadFromDumpFileName.Visible = cbLoadFromDump.Checked;
		}

		private void cbAddToDump_CheckedChanged(object sender, EventArgs e)
		{
			cmdBrowseAddToDumpFileName.Visible = cbAddToDump.Checked;
			txtAddToDumpFileName.Visible = cbAddToDump.Checked;
		}

		private void bBrowseLoadFromDumpFileName_Click(object sender, EventArgs e)
		{
			Utilities.OpenFile(txtLoadFromDumpFileName, PathUtils.DataPath + @"\Results\");
		}

		private void bBrowseAddToDumpFileName_Click(object sender, EventArgs e)
		{
			Utilities.OpenFile(txtAddToDumpFileName, PathUtils.DataPath + @"\Results\");
		}

		private void SaveUserSettings()
		{
			UISettings.Default.DocumentsProviderSettings = txtDocumentsProviderSettings.Text;
			UISettings.Default.OutputOntologySettings = txtOutputFileName.Text;
			UISettings.Default.LoadFromDumpFileName = txtLoadFromDumpFileName.Text;
			UISettings.Default.SaveToDumpFileName = txtAddToDumpFileName.Text;

			UISettings.Default.OutputOntologyProviderName = cbOutputOntologyProvider.SelectedIndex != -1
			                                                	?
			                                                _OnologyConfig.Providers[cbOutputOntologyProvider.SelectedIndex].Name
			                                                	: string.Empty;

			UISettings.Default.DocumentsProviderProviderName = cbDocumentProviderProvider.SelectedIndex != -1
			                                                  	?
			                                                  _DocumentProviderConfig.Providers[
			                                                  	cbDocumentProviderProvider.SelectedIndex].Name
			                                                  	: string.Empty;


			UISettings.Default.Save();
		}

		private void bBuildKnowledges_Click(object sender, EventArgs e)
		{
			SaveUserSettings();

#if RunBuildingKnowledgeInNewThread
			Thread t = new Thread(new ThreadStart(BuildKnowledges));
			t.Start();
//			t.Join();
#else
			BuildKnowledges();
#endif
		}

		private bool IsInited = false;

		public void BuildKnowledges()
		{
			#region UI
			this.Cursor = Cursors.WaitCursor;
			StatusLabel1.Text = Messages.InitializingDictionaries;
			#endregion

			if (!IsInited)
			{
				//Need to implement IsInited in new option handlers!
				ShortcutManager.Instance.Init(ConfigurationManager.GetSection("shortcutManager"));
				ClauseHolderManager.Instance.Init(ConfigurationManager.GetSection("clauseHolderManager"));
				WordHolderManager.Instance.Init(ConfigurationManager.GetSection("wordHolderManager"));
				EntityTemplateManager.GetInstance().Init(ConfigurationManager.GetSection("entityTemplateManager"));

				//Init dictionaries
				if (!DictionaryManager.GetInstance().IsInited)
				{
					DictionaryManager.GetInstance().Init((DictionaryManagerOptions)ConfigurationManager.GetSection("dictionaryManager"));
				}
				IsInited = true;
			}

			#region UI
			DateTime startDT = DateTime.Now;
			#endregion

			//Reading file
			#region UI
			StatusLabel1.Text = Messages.ReadingSource;
			#endregion

			Provider<IDocumentProvider> documentProviderProvider = _DocumentProviderConfig[cbDocumentProviderProvider.Text];
			IDocumentProvider documentProvider = documentProviderProvider.CreateProvidedClassInstance();
			documentProvider.Init(txtDocumentsProviderSettings.Text);

			//Graph
			EntityGraph entityGraph;

			if (cbLoadFromDump.Checked)
			{
				try
				{
					entityGraph = EntityGraph.LoadDump(txtLoadFromDumpFileName.Text);
				}
				catch
				{
					if (DialogResult.OK == MessageBox.Show("Ignore?", "Cannot load dump", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
						entityGraph = new EntityGraph();
					else
						return;
				}
			}
			else
				entityGraph = new EntityGraph();

			foreach (IDocument document in documentProvider)
			{
				foreach (IArticle article in document)
				{
					//Prospecting
					#region UI
					StatusLabel1.Text = Messages.Translating;
					#endregion
					IProspector prospector = ProspectorManager.Instance.GetProspector(article);
					prospector.Prospect(entityGraph, article);
				}

				document.Close();
			}
			documentProvider.Close();

			//Optimizing the graph
			#region UI
			StatusLabel1.Text = Messages.OptimizingGraph;
			#endregion
			EntityGraphOptimizer entityGraphOptimizer = new EntityGraphOptimizer(entityGraph);
			entityGraphOptimizer.OptimizeSubclassRelationship();
			entityGraphOptimizer.OptimizePropertyRelationships();

			//Saving Graph to file
			#region UI
			StatusLabel1.Text = Messages.SavingGraph;
			#endregion

			Provider<IOntology> outputOntologyProvider = _OnologyConfig[cbOutputOntologyProvider.Text];
			IOntology outputOntology = outputOntologyProvider.CreateProvidedClassInstance();
			outputOntology.SaveGraph(entityGraph, txtOutputFileName.Text);

			if(cbAddToDump.Checked)
				entityGraph.Dump(txtAddToDumpFileName.Text);

			#region UI
			DateTime stopDT = DateTime.Now;
			TimeSpan ts = stopDT - startDT;
			StatusLabel1.Text = string.Format(Messages.BuildingCompliteFormat, ts.TotalMilliseconds);
			StatusProgressBar1.Visible = false;
			this.Cursor = Cursors.Default;
			MessageBox.Show(string.Format("Done by {0} milliseconds", ts.TotalMilliseconds), "Knowledge Prospector", MessageBoxButtons.OK, MessageBoxIcon.Information);
			#endregion

			GC.Collect();
		}

		void translator_OnPartComplite()
		{
			StatusProgressBar1.PerformStep();
		}

		private void cmdBrowseOutputFile_Click(object sender, EventArgs e)
		{
			Utilities.OpenFile(txtOutputFileName, Path.Combine(PathUtils.DataPath, @"\Results\"));
		}
		
		protected Provider<IDocumentProvider> SelectedDocumentsProviderProvider
		{
			get { return _DocumentProviderConfig[cbDocumentProviderProvider.Text]; }
		}

		private void cmdEditDocumentsProviderOptions_Click(object sender, EventArgs e)
		{
			IProviderOptionsPlugin plugin = SelectedDocumentsProviderProvider.CreatePluginInstance();
			plugin.OnPluginClosed += new PluginClosedEventHandler(plugin_OnPluginClosed);
			plugin.ProviderOptions = txtDocumentsProviderSettings.Text;
			plugin.ShowPlugin();
		}

		void plugin_OnPluginClosed(IProviderOptionsPlugin sender, PluginClosedEventArgs e)
		{
			if(e.OK)
				txtDocumentsProviderSettings.Text = sender.ProviderOptions;

			sender.OnPluginClosed -= plugin_OnPluginClosed;
		}
	}
}