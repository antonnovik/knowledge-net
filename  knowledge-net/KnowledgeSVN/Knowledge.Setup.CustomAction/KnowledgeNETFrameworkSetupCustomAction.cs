using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.Windows.Forms;

using System.Xml.Serialization;
using System.IO;
using Microsoft.Win32;
using System.Threading;
using System.Diagnostics;

namespace KNFInstallerCustomAction
{
    [RunInstaller(true)]
    public class KnowledgeNETFrameworkSetupCustomAction : Installer
    {
        public KnowledgeNETFrameworkSetupCustomAction()
            :base()
        {
            InitializeComponent();
        }

        /*
                    // Check whether the "LogtoConsole" parameter has been set.
                    if (context.IsParameterTrue("LogtoConsole") == true)
                    {
                        // Display the message to the console and add it to the logfile.
                        context.LogMessage("The 'Install' method has been called");
                    }
        */

        // /dirs="[ProgramFilesFolder]\[ProductName]\?[AppDataFolder]\"
        public override void Commit(IDictionary state)
        {
            base.Commit(state);

            InstallContext context = this.Context;

            if (!context.Parameters.ContainsKey("dirs"))
            {
                MessageBox.Show("Registering Add-in failed. You'll have to copy add-in support files manually");
                return;
            }

            string installpath = context.Parameters["dirs"].Substring(0, context.Parameters["dirs"].IndexOf('?'));
            string appdatapath = context.Parameters["dirs"].Substring(context.Parameters["dirs"].IndexOf('?') + 1, context.Parameters["dirs"].Length - context.Parameters["dirs"].IndexOf('?') - 1);

            string addinlink = File.ReadAllText(installpath + "_KnowledgeNETFramework.AddIn");
            addinlink = addinlink.Replace("<Assembly></Assembly>", "<Assembly>" + installpath + "Knowledge.Editor.dll</Assembly>");

            if (!Directory.Exists(appdatapath + "Microsoft")) Directory.CreateDirectory(appdatapath + "Microsoft");
            if (!Directory.Exists(appdatapath + @"Microsoft\MSEnvShared")) Directory.CreateDirectory(appdatapath + @"Microsoft\MSEnvShared");
            if (!Directory.Exists(appdatapath + @"Microsoft\MSEnvShared\Addins")) Directory.CreateDirectory(appdatapath + @"Microsoft\MSEnvShared\Addins");
            if (File.Exists(appdatapath + @"Microsoft\MSEnvShared\Addins\KnowledgeNETFramework.AddIn"))
                File.Delete(appdatapath + @"Microsoft\MSEnvShared\Addins\KnowledgeNETFramework.AddIn");
            File.WriteAllText(appdatapath + @"Microsoft\MSEnvShared\Addins\KnowledgeNETFramework.AddIn", addinlink, System.Text.Encoding.Unicode);

            ManageTemplates(installpath, TemplatesActions.Copy);
        }

        public override void Uninstall(IDictionary savedState)
        {
            InstallContext context = this.Context;

            if (!context.Parameters.ContainsKey("dirs"))
            {
                MessageBox.Show("Unregistering Add-in failed. You'll have to delete add-in support files manually");
                return;
            }

            string installpath = context.Parameters["dirs"].Substring(0, context.Parameters["dirs"].IndexOf('?'));
            string appdatapath = context.Parameters["dirs"].Substring(context.Parameters["dirs"].IndexOf('?') + 1, context.Parameters["dirs"].Length - context.Parameters["dirs"].IndexOf('?') - 1);

            if (File.Exists(appdatapath + @"Microsoft\MSEnvShared\Addins\KnowledgeNETFramework.AddIn"))
                File.Delete(appdatapath + @"Microsoft\MSEnvShared\Addins\KnowledgeNETFramework.AddIn");

            ManageTemplates(installpath, TemplatesActions.Delete);
        }

        private enum TemplatesActions { Copy, Delete };

        private void ManageTemplates(string installpath, TemplatesActions action)
        {
            try
            {
                RegistryKey k = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\VisualStudio\8.0", false);
                string vsDir = (string)k.GetValue("InstallDir");
                string projectTemplatesDir = vsDir + @"ProjectTemplates\CSharp\Windows\1033";
                if (!Directory.Exists(projectTemplatesDir))
                {
                    if (action == TemplatesActions.Copy)
                        MessageBox.Show("Knowledge.NET project templates can not be copied.\n\"" + projectTemplatesDir + "\" not found.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else
                {
                    if (action == TemplatesActions.Copy)
                    {
                        if (File.Exists(projectTemplatesDir + @"\Knowledge.zip")) File.Delete(projectTemplatesDir + @"\Knowledge.zip");
                        File.Copy(installpath + @"\Knowledge.zip", projectTemplatesDir + @"\Knowledge.zip");
                    }
                    else
                    {
                        File.Delete(projectTemplatesDir + @"\AspectAttr.zip");
                    }

                    if (!File.Exists(vsDir + "devenv.exe")) return;

                    ProcessStartInfo pInfo = new ProcessStartInfo();
                    pInfo.WorkingDirectory = vsDir;
                    pInfo.FileName = vsDir + "devenv.exe";
                    pInfo.Arguments = "/setup";
                    pInfo.UseShellExecute = false;
                    pInfo.RedirectStandardOutput = false;
                    pInfo.RedirectStandardError = false;
                    pInfo.CreateNoWindow = true;

                    System.Diagnostics.Process devenvProcess = System.Diagnostics.Process.Start(pInfo);

                    devenvProcess.WaitForExit();
                }
            }
            catch (Exception e) { MessageBox.Show(e.Message); }
        }

        #region Component Designer generated code
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
        }

        #endregion
    }
}