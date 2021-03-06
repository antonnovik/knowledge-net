// Configure log4net using the .config file
[assembly: log4net.Config.XmlConfigurator(Watch = true)]
// This will cause log4net to look for a configuration file
// called devenv.exe.config in the appropriate base
// directory. The config file will be watched for changes.

namespace Knowledge.Editor.AddIn
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Windows.Forms;

    using Microsoft.VisualStudio.CommandBars;
    using Extensibility;
    using EnvDTE;
    using EnvDTE80;

    using log4net;

    using Knowledge.Editor.Controller;
    // TODO: get rid of the variable "addinControl"
    using Knowledge.Editor.View;

    /// <summary>The object for implementing an Add-in.</summary>
    /// <seealso class='IDTExtensibility2' />
    public partial class Connect : Object, IDTExtensibility2, IDTCommandTarget
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Connect));

        private DTE2 app;
        private AddIn addin;
        private Window addinWindow;
        private KnowledgeControl control;
        KnowledgeController controller;

        public Connect()
        {
        }

        /// <summary>
        /// Implements the OnConnection method of the IDTExtensibility2
        /// interface. Receives notification that the Add-in is being loaded.
        /// </summary>
        /// <param term='app'>Root object of the host application.</param>
        /// <param term='mode'>Describes how the Add-in is being loaded.</param>
        /// <param term='addin'>Object representing this Add-in.</param>
        /// <seealso class='IDTExtensibility2' />
        public void OnConnection(object appObject, ext_ConnectMode mode,
            object addinObject, ref Array custom)
        {
            Command command = null;

            log.Debug("on connection ...");

            app = (DTE2)appObject;
            addin = (AddIn)addinObject;

            if (mode == ext_ConnectMode.ext_cm_AfterStartup ||
                mode == ext_ConnectMode.ext_cm_Startup)
            {
                Array contextGUIDS = new object[] { };
                Commands2 commands = (Commands2)app.Commands;

                // Try to retrieve the command, just in case it was already created
                try{
                    log.Debug(" try to retrieve the command, just in case it was already created ...");
                    command = app.Commands.Item(addin.ProgID + "." + "KnowledgeNETFramework", 1);
                }catch(Exception e){
                }
                log.Debug(" command: "+command);

                try
                {
                    // Add the command if it does not exists
                    log.Debug(" add the command if it does not exists");
                    if (command == null)
                    {
                        command = commands.AddNamedCommand2(
                        addin,
                        "KnowledgeNETFramework",
                        "KnowledgeNETFramework",
                        "Executes the command for KnowledgeNETFramework",
                        true,
                        2946,
                        ref contextGUIDS,
                        (int)vsCommandStatus.vsCommandStatusSupported + (int)vsCommandStatus.vsCommandStatusEnabled,
                        (int)vsCommandStyle.vsCommandStylePictAndText,
                        vsCommandControlType.vsCommandControlTypeButton
                        );
                    }
                    log.Debug(" command: " + command);

                    String toolsMenuName;
                    try
                    {
                        //If you would like to move the command to a different menu, change the word "Tools" to the 
                        //  English version of the menu. This code will take the culture, append on the name of the menu
                        //  then add the command to that menu. You can find a list of all the top-level menus in the file
                        //  CommandBar.resx.
                        System.Resources.ResourceManager resourceManager = new System.Resources.ResourceManager("AspectNETFramework.CommandBar", System.Reflection.Assembly.GetExecutingAssembly());
                        System.Threading.Thread thread = System.Threading.Thread.CurrentThread;
                        System.Globalization.CultureInfo cultureInfo = thread.CurrentCulture;
                        toolsMenuName = resourceManager.GetString(String.Concat(cultureInfo.TwoLetterISOLanguageName, "Tools"));
                    }
                    catch (Exception e)
                    {
                        // We tried to find a localized version of the word Tools, but one was not found.
                        // Default to the en-US word, which may work for the current culture.
                        toolsMenuName = "Tools";
                    }
                    log.Debug(" toolsMenuName: " + toolsMenuName);

                    // Retrieve some built-in command bars
                    Microsoft.VisualStudio.CommandBars.CommandBar menuBarCommandBar;
                    CommandBarControl toolsControl;
                    CommandBarPopup toolsPopup;
                    CommandBarControl commandBarControl;

                    //Place the command on the tools menu.
                    //Find the MenuBar command bar, which is the top-level command bar holding all the main menu items:
                    menuBarCommandBar = ((CommandBars)app.CommandBars)["MenuBar"];
                    //Find the Tools command bar on the MenuBar command bar:
                    toolsControl = menuBarCommandBar.Controls[toolsMenuName];
                    toolsPopup = (CommandBarPopup)toolsControl;
                    //Find the appropriate command bar on the MenuBar command bar:
                    commandBarControl = (CommandBarControl)command.AddControl(toolsPopup.CommandBar, 1);

                    // Add a button to the built-in "Tools" menu
/*                    
                    log.Debug(" Add a button to the built-in 'Tools' menu");
                    CommandBarControl cb = (CommandBarControl)command.AddControl(toolsControl, 1);
                    cb.Caption = "KnowledgeNETFramework";
                    cb.Enabled = true;
*/
                }
                catch (Exception e)
                {
                    log.Debug("connection failed.", e);
                }
            }
        }

        /// <summary>Implements the OnDisconnection method of the IDTExtensibility2 interface. Receives notification that the Add-in is being unloaded.</summary>
        /// <param term='disconnectMode'>Describes how the Add-in is being unloaded.</param>
        /// <param term='custom'>Array of parameters that are host application specific.</param>
        /// <seealso class='IDTExtensibility2' />
        public void OnDisconnection(ext_DisconnectMode disconnectMode, ref Array custom)
        {
            if (control != null)
            {
                // TODO
                //addinControl.ExtendedClose();
                control.Hide();
            }
        }

        /// <summary>Implements the OnAddInsUpdate method of the IDTExtensibility2 interface. Receives notification when the collection of Add-ins has changed.</summary>
        /// <param term='custom'>Array of parameters that are host application specific.</param>
        /// <seealso class='IDTExtensibility2' />		
        public void OnAddInsUpdate(ref Array custom)
        {
        }

        /// <summary>Implements the OnStartupComplete method of the
        /// IDTExtensibility2 interface. Receives notification that
        /// the host application has completed loading.</summary>
        /// <param term='custom'>Array of parameters that are host application specific.</param>
        /// <seealso class='IDTExtensibility2' />
        public void OnStartupComplete(ref Array custom)
        {
            log.Debug("on startup complete ...");
            showAddin();
        }

        /// <summary>Implements the OnBeginShutdown method of the IDTExtensibility2 interface. Receives notification that the host application is being unloaded.</summary>
        /// <param term='custom'>Array of parameters that are host application specific.</param>
        /// <seealso class='IDTExtensibility2' />
        public void OnBeginShutdown(ref Array custom)
        {
            if (addinWindow != null)
            {
                addinWindow.Visible = false;
                addinWindow.Close(vsSaveChanges.vsSaveChangesPrompt);
            }
        }

        /// <summary>Implements the QueryStatus method of the IDTCommandTarget interface. This is called when the command's availability is updated</summary>
        /// <param term='commandName'>The name of the command to determine state for.</param>
        /// <param term='neededText'>Text that is needed for the command.</param>
        /// <param term='status'>The state of the command in the user interface.</param>
        /// <param term='commandText'>Text requested by the neededText parameter.</param>
        /// <seealso class='Exec' />
        public void QueryStatus(string commandName, vsCommandStatusTextWanted neededText, ref vsCommandStatus status, ref object commandText)
        {
            log.Debug("query status: " + commandName);

            if (neededText == vsCommandStatusTextWanted.vsCommandStatusTextWantedNone)
            {
                if (commandName == "KnowledgeNETFramework.Connect.KnowledgeNETFramework")
                {
                    status = (vsCommandStatus)vsCommandStatus.vsCommandStatusSupported | vsCommandStatus.vsCommandStatusEnabled;
                }
            }
        }

        /// <summary>Implements the Exec method of the IDTCommandTarget interface.
        /// This is called when the command is invoked.</summary>
        /// <param term='commandName'>The name of the command to execute.</param>
        /// <param term='executeOption'>Describes how the command should be run.</param>
        /// <param term='varIn'>Parameters passed from the caller to the command handler.</param>
        /// <param term='varOut'>Parameters passed from the command handler to the caller.</param>
        /// <param term='handled'>Informs the caller if the command was handled or not.</param>
        /// <seealso class='Exec' />
        public void Exec(string commandName, vsCommandExecOption executeOption, ref object varIn, ref object varOut, ref bool handled)
        {
            log.Debug("exec: "+commandName);

            handled = false;
            if (executeOption == vsCommandExecOption.vsCommandExecOptionDoDefault)
            {
                if (commandName == "Knowledge.Editor.AddIn.Connect.KnowledgeNETFramework")
                {
                    handled = showAddin();
                }
            }
        }

        private bool showAddin()
        {
            log.Debug("show addin... ");

            if (addinWindow != null)
            {
                if (addinWindow.Visible)
                {
                    log.Debug(" return");
                    return false;
                }
            }

            try
            {
                // addinWindow stuff

                //string guid = "{453A83DA-6EA6-42b7-A681-83A6A4A9E5A4}";
                string guid = "{48FC1ED2-1C5B-47e5-A83A-A6D33B316624}";
                object tempdoc = null;
                Assembly asm = Assembly.GetExecutingAssembly();

                addinWindow = ((Windows2)app.Windows).CreateToolWindow2(
                    addin,
                    asm.Location,
                    "Knowledge.Editor.View.KnowledgeControl",
                    "Knowledge.NET Framework",
                    guid,
                    ref tempdoc
                );

                log.Debug(" addinWindow: "+addinWindow);
                addinWindow.Visible = true;
                addinWindow.Activate();
                // TODO
                //addinWindow.SetTabPicture(Knowledge.Editor.CommandBar.an_ico.GetHbitmap());
                control = (Knowledge.Editor.View.KnowledgeControl)tempdoc;

                // outputWindowPane stuff

                OutputWindow outputWindow = (OutputWindow)app.Windows.Item(Constants.vsWindowKindOutput).Object;
                OutputWindowPane outputWindowPane = null;
                if (outputWindow.OutputWindowPanes != null)
                {
                    for (int i = 1; i <= outputWindow.OutputWindowPanes.Count; i++)
                    {
                        if (outputWindow.OutputWindowPanes.Item(i).Name == "Knowledge.NET Framework Console")
                        {
                            outputWindowPane = outputWindow.OutputWindowPanes.Item(i);
                            break;
                        }
                    }
                }

                if (outputWindowPane == null) outputWindowPane = outputWindow.OutputWindowPanes.Add("Knowledge.NET Framework Console");

                // TODO: refactoring required
                Environment environment = new Environment();
                environment.KnowledgeNetFrameworkBaseDir = asm.Location.Substring(0, asm.Location.LastIndexOf("\\") + 1);
                environment.ApplicationObject = app;
                environment.OutputWindowPane = outputWindowPane;

                controller = KnowledgeController.instance(environment,
                                                          control);
                
                // on vs startup open event is not coming for some reason
                // TODO: get rid of this
                controller.opened();
            }
            catch (Exception e)
            {
                log.Debug("showing tool window failed.", e);
                MessageBox.Show(e.ToString());
            }

            return true;
        }
    }
}