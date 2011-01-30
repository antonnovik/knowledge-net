using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

using log4net;

using EnvDTE;
using EnvDTE80;

using Knowledge.Editor.Model;
using Knowledge.Editor.Controller;

/*
 * The class is part of implementation of pattern
 * Observer.
 */
namespace Knowledge.Editor.View
{
    public interface IObserver {
        void update(ISubject subject);
    }

    public class KnowledgeCsView : IObserver { 

        private static readonly ILog log = LogManager.GetLogger(typeof(KnowledgeCsView));

        KnowledgeController controller;
        KnowledgeModel model;

        public KnowledgeCsView(KnowledgeController controller,
                                   KnowledgeModel model) {

            log.Debug(" cs observer ctor ... ");

            this.controller = controller;
            this.model = model;

        }

        public void update(ISubject subject) { 
            
            log.Debug(" c# view updating (converting)... ");

            try
            {
                controller.environment.OutputWindowPane.Clear();
                controller.environment.OutputWindowPane.Activate();

                if (!File.Exists(controller.environment.KnowledgeNetFrameworkBaseDir +
                                 "Knowledge.CSharp.Converter.exe"))
                {
                    string error = "\nCan not find Knowledge.CSharp.Converter.exe utility in " +
                                   controller.environment.KnowledgeNetFrameworkBaseDir +
                                   "\nKnowledge converting aborted";

                    log.Debug(error);
                    messageToOutputPane(error);
                    return;
                }

                // TODO: replace project name + debug/release
                messageToOutputPane("\n------ Converter started: Project: Knowledge12,"+
                                    "Configuration: Debug Any CPU ------");

                int succeeded = 0;
                int failed = 0;
                int skipped = 0;

                foreach (Project project in controller.environment.ApplicationObject.Solution.Projects)
                {
                    string projectFileName = project.FileName;
                    log.Debug("    project = " + projectFileName);
                    foreach (ProjectItem item in project.ProjectItems)
                    {
                        string itemFileName = item.Name;
                        log.Debug("      item =" + itemFileName);

                        if (projectFileName.EndsWith("csproj") && itemFileName.EndsWith(".expert"))
                        {

                            // TODO: prItem.Document.FullName is null !!! for some reason
                            // so RESTRICTION: file inside other dirs will not be found

                            // TODO: ->Anton: System.Exit() replace to return is the file is not found
                            // TODO: ->Anton: flush() should be added, otherwise looks like parser is not ready for this twice actions

                            string workdir = projectFileName.Substring(0, projectFileName.LastIndexOf("\\"));
                            log.Debug(" converting file : " + workdir + "\\" + itemFileName);
                            
                            ExpComp.convert(workdir + "\\" + itemFileName);

                            // Output pane + add file to project
                            // TODO: prItem.Document.FullName is null !!! for some reason
                            if (File.Exists(workdir + "\\" + itemFileName + "_.cs"))
                            {
                                messageToOutputPane("========== Convert: 1 succeeded, 0 failed, 0 skipped ==========");
                                project.ProjectItems.AddFromFile(workdir + "\\" + itemFileName + "_.cs");
                                succeeded++;
                            }
                            else
                            {
                                failed++;
                            }
                        }
                        else
                        {
                            skipped++;
                        }
                    }
                }
                string message = "========== Convert: " + succeeded +
                                 " succeeded, " + failed +
                                 " failed, " + skipped +
                                 " skipped ==========";

                log.Debug(message);
                messageToOutputPane(message);
            }
            catch (Exception e)
            {
                log.Debug(" converting failed. "+e);
            }
        }

        private void messageToOutputPane(String text) {

            controller.environment.OutputWindowPane.OutputString(text + "\n`");

        }
    }
 
    public class KnowledgeNetView : IObserver{

        private static readonly ILog log = LogManager.GetLogger(typeof(KnowledgeNetView));

        KnowledgeController controller;
        KnowledgeModel model;

        public KnowledgeNetView(KnowledgeController controller,
                                    KnowledgeModel model) {

            log.Debug(" net observer ctor ... ");

            this.controller = controller;
            this.model = model;

        }

        // TODO: ask Anton to implement this
        public void update(ISubject subject){

        }

    }
}
