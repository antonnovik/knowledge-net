using System;
using System.Collections;
using System.Text;

using log4net;

using Knowledge.Library;
using Knowledge.Editor.Controller;

// TODO
using EnvDTE;
using EnvDTE80;

/*
 * The class is adapter for structure which holds
 * inner representation. The logic of its filling
 * is implemented inside Knowledge.Core project.
 */
namespace Knowledge.Editor.Model
{

    // TODO: ask Anton do this public (KnowledgeCrossCompiler.cs)
    public enum FrameTypes { instanceFrame, classFrame };

    public class KnowledgeAdapter
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(KnowledgeAdapter));

        // TODO: singleton?

        // so far only one file (with knowledge) is considered by system
        // TODO: perhaps it'll be useful for "navigation impl"

        private static string filepath;
        public bool isEmpty = true;

        public KnowledgeAdapter() {
            log.Debug(" data created. ");
        }

        public static string getCurrentFilepath() {
            return filepath;
        }

        // TODO: ask Anton on possibility of parsing of several files
        // TODO: make returning of IList<string>
        public static string getKnowledgeFilePath(Knowledge.Editor.AddIn.Environment environment)
        {

            log.Debug(" getting path of knowledge file");

            foreach (Project project in environment.ApplicationObject.Solution.Projects)
            {
                string projectFileName = project.FileName;
                log.Debug("    project = " + projectFileName);

                foreach (ProjectItem item in project.ProjectItems)
                {

                    string itemFileName = item.Name;
                    log.Debug("      item = " + itemFileName);

                    if (projectFileName.EndsWith("csproj") && itemFileName.EndsWith(".expert"))
                    {
                        // TODO: prItem.Document.FullName is null !!! for some reason
                        // SO -> RESTRICTION: file inside other dirs will not be found

                        // TODO: ->Anton: System.Exit() replace to return is the file is not found
                        // TODO: ->Anton: flush() should be added, otherwise looks
                        //                like parser is not ready for this twice actions

                        string workdir = projectFileName.Substring(0, projectFileName.LastIndexOf("\\"));
                        string filepath = (workdir + "\\" + itemFileName);
                        
                        log.Debug(" file - "+filepath);
                        return filepath;
                    }
                }
            }

            log.Debug(" no knowledge ");
            return null;
        }

        public void parse(Knowledge.Editor.AddIn.Environment environment)
        {

            log.Debug(" parsing model ... ");

            try
            {
                string filepath = KnowledgeAdapter.getKnowledgeFilePath(environment);

                ExpComp.Clear();
                Scanner scanner = new Scanner(filepath);
                ExpComp.parser = new Parser(scanner);
                ExpComp.parser.Parse();

                KnowledgeAdapter.filepath = filepath;
                isEmpty = false;

                log.Debug(" building model successed. ");

            }
            catch (Exception e)
            {
                log.Debug(" building model failed. " + e);
            }
        }

        // TODO: think out something else
        public IDictionary getFrames(){
            return (IDictionary)ExpComp.dataFrames;
        }

        public IDictionary getRules()
        {
            return (IDictionary)ExpComp.ruleFrames;
        }

        public static string getFrameName(Object obj) {
            if (obj.GetType() == typeof(DataFrame)) {
                return ((DataFrame)obj).iden;
            }
            if (obj.GetType() == typeof(RuleFrame))
            {
                return ((RuleFrame)obj).iden;
            }
            return null;
        }

        public static bool isClassFrame(Object obj) { 
            if (obj.GetType() != typeof(DataFrame)) {
                return false;
            }
            DataFrame frame = (DataFrame)obj;
            if (frame.frameType == DataFrame.FrameTypes.classFrame){
                return true;
            }
            return false;
        }

        public static bool isInstanceFrame(Object obj)
        {
            if (obj.GetType() != typeof(DataFrame))
            {
                return false;
            }
            DataFrame frame = (DataFrame)obj;
            if (frame.frameType == DataFrame.FrameTypes.instanceFrame)
            {
                return true;
            }
            return false;
        }

        public static bool isRuleFrame(Object obj)
        {
            if (obj.GetType() == typeof(RuleFrame))
            {
                return true;
            }
            return false;
        }

        public static bool isSlot(Object obj)
        {
            if (obj.GetType() == typeof(Slot))
            {
                return true;
            }
            return false;
        }

        public static string getSlotName(Object obj) {
            if (obj.GetType() != typeof(Slot))
            {
                return null;
            }
            return ((Slot)obj).iden;
        }

        public static IDictionary getOwnSlots(Object obj)
        { 
            if (obj.GetType() != typeof(DataFrame))
            {
                return null;
            }
            return (IDictionary)((DataFrame)obj).ownSlots;
        }

        public static IDictionary getInstanceSlots(Object obj)
        {
            if (obj.GetType() != typeof(DataFrame))
            {
                return null;
            }
            return (IDictionary)((DataFrame)obj).instanceSlots;
        }

    }

}
