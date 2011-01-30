using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using EnvDTE;
using EnvDTE80;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;

using log4net;

using Knowledge.Editor.Model;
using Knowledge.Editor.Controller;

namespace Knowledge.Editor.View
{
    /*
     *
     */
    class TextEditorHelper
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(TextEditorHelper));

        public static void navigate(KnowledgeController controller, Object tag, String sourceFile)
        {
            try
            {
                log.Debug(" navigation ... ");

                string regExpression = getRegExpr(tag);
                if (regExpression == null) { return; }
                string actualString = getActualString(sourceFile, regExpression);
                if (actualString == null) { return; }

                Window textEditor = controller.environment.ApplicationObject.OpenFile(Constants.vsViewKindCode, sourceFile);
                TextSelection textSelection = (TextSelection)textEditor.Document.Selection;
                textSelection.StartOfDocument(false);
                textSelection.EndOfDocument(false);

                // TODO: textSelection.findPattern doesn't work for some reason
                if (textSelection.FindText(actualString, (int)vsFindOptions.vsFindOptionsFromStart)){
                    textSelection.SelectLine();
                }else{
                    log.Debug("log. actualString is not found in text file, actualString="+actualString);
                }

                textEditor.Visible = true;
                textEditor.Activate();
                // TODO: save of the document
            }
            catch (Exception e0)
            {
                MessageBox.Show(e0.Message);
            }
        }

        private static string getRegExpr(Object tag)
        {
            string pattern;
            string spaces = "\\s+";

            if (KnowledgeAdapter.isClassFrame(tag))
            {
                string name = KnowledgeAdapter.getFrameName(tag);
                pattern = "frame" + spaces + "class" + spaces + name;
            }
            else if (KnowledgeAdapter.isInstanceFrame(tag))
            {
                string name = KnowledgeAdapter.getFrameName(tag);
                pattern = "frame" + spaces + "instance" + spaces + name;
            }
            else if (KnowledgeAdapter.isRuleFrame(tag))
            {
                string name = KnowledgeAdapter.getFrameName(tag);
                pattern = "frame" + spaces + "ruleset" + spaces + name;
            }
            else 
            {
                pattern = null;
            }
            // TODO: slots

            log.Debug(" regular expression = "+pattern);

            return pattern;
        }

        /*
        public void update(ChangeEvent ce, string sourceFile)
        {
            try
            {
                // ADD for Frames is not implemented
                if (ce.getChangeID() == ChangeEvent.REMOVE &&
                    // it's frame knowledges
                    ce.getSource().getID() >= 10 &&
                    ce.getSource().getID() >= 20){
                    writer.WriteLine("log. ADD for Frames is not implemented");
                    return;
                }

                string regExpression = getRegExpr(ce);
                if (regExpression == null) { return; }
                string actualString = getActualString(sourceFile, regExpression);
                if (actualString == null) { return; }

                Window textEditor = Manager.getInstance().getEnvironment().ApplicationObject.OpenFile(Constants.vsViewKindCode, sourceFile);
                TextSelection textSelection = (TextSelection)textEditor.Document.Selection;
                textSelection.StartOfDocument(false);
                textSelection.EndOfDocument(false);

                // TODO: textSelection.findPattern doesn't work for some reason
                if (textSelection.FindText(actualString, (int)vsFindOptions.vsFindOptionsFromStart))
                {
                    textSelection.Cut();
                }
                else
                {
                    writer.WriteLine("log. actualString is not found in text file, actualString=" + actualString);
                }

                // TODO: save of the document
            }
            catch (Exception e0)
            {
                MessageBox.Show(e0.Message);
            }
        }
         */
        
        private static string getActualString(string sourceFile, string regExpression) {
            FileStream file = new FileStream(sourceFile, FileMode.Open);
            StreamReader reader = new StreamReader(file);
            string str = reader.ReadToEnd();

            MatchCollection Matches = Regex.Matches(str, regExpression, RegexOptions.None);
            string matchText = null;
            foreach (Match match in Matches)
            {
                matchText = match.Value;
            }

            reader.Close();
            file.Close();

            if (matchText == null){
                log.Debug("log. actual text is not found, regExpr="+regExpression);
            }
            return matchText;
        }

        /*
        private static string getRegExpr(Object tag)
        {
            KnowledgeNode source = ne.getSource();
            int id = source.getID();
            string name = source.getName();

            string pattern;
            string spaces = "\\s+";

            if (id == KnowledgeNode.EMPTY)
            {
                writer.WriteLine("log. empty knowledge -> event skipped");
                return null;
            }
            else if (id == KnowledgeNode.FRAME_CLASS)
            {
                pattern = "frame" + spaces + "class" + spaces + name;
            }
            else if (id == KnowledgeNode.FRAME_INSTANCE)
            {
                pattern = "frame" + spaces + "instance" + spaces + name;
            }
            else if (id == KnowledgeNode.FRAME_RULESET)
            {
                pattern = "frame" + spaces + "ruleset" + spaces + name;
            }
            else if (id == KnowledgeNode.SLOT_OWN)
            {
                // TODO: not implemented yet 
                // since class hierarchy is not implemented yet
                return null;
            }
            else if (id == KnowledgeNode.SLOT_INSTANCE)
            {
                // TODO: not implemented yet 
                // since class hierarchy is not implemented yet
                return null;
            }
            else
            {
                writer.WriteLine("log. knowledge is not found, id=" + id);
                return null;
            }
            return pattern;
        }
        
        // TODO: text "{}" (for REMOVE) should be removed also !!!
        // NOTE: ADD for Frames won't be implemented

        /*
        private string getRegExpr(ChangeEvent ce) {
            KnowledgeNode source = ce.getSource();
            int id = source.getID();
            string name = source.getName();

            string pattern;
            string spaces = "\\s+";

            if (id == KnowledgeNode.EMPTY)
            {
                writer.WriteLine("log. empty knowledge -> event skipped");
                writer.WriteLine("log. name = "+name);
                return null;
            }
            else if (id == KnowledgeNode.FRAME_CLASS)
            {
                pattern = "frame" + spaces + "class" + spaces + name;
            }
            else if (id == KnowledgeNode.FRAME_INSTANCE)
            {
                pattern = "frame" + spaces + "instance" + spaces + name;
            }
            else if (id == KnowledgeNode.FRAME_RULESET)
            {
                pattern = "frame" + spaces + "ruleset" + spaces + name;
            }
            else if (id == KnowledgeNode.SLOT_OWN)
            {
                // TODO: not implemented yet 
                // since class hierarchy is not implemented yet
                return null;
            }
            else if (id == KnowledgeNode.SLOT_INSTANCE)
            {
                // TODO: not implemented yet 
                // since class hierarchy is not implemented yet
                return null;
            }
            else
            {
                writer.WriteLine("log. knowledge is not found, id=" + id);
                return null;
            }

            // TODO: implement the same thing about other types
            if (id == KnowledgeNode.FRAME_CLASS ||
                id == KnowledgeNode.FRAME_INSTANCE ||
                id == KnowledgeNode.FRAME_RULESET)
            {
                pattern = pattern + spaces + "{" + "[^}]+" + "}";
            }

            return pattern;
        }
         * */

    }
}
