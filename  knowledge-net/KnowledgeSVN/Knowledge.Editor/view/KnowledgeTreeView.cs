using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using log4net;

using EnvDTE;
using EnvDTE80;

using Knowledge.Editor.Model;
using Knowledge.Editor.Controller;

namespace Knowledge.Editor.View
{
    public class KnowledgeTreeView : IObserver
    {

        private static readonly ILog log = LogManager.GetLogger(typeof(KnowledgeTreeView));

        KnowledgeController controller;
        KnowledgeModel model;
        KnowledgeControl control;

        public KnowledgeTreeView(KnowledgeController controller,
                                 KnowledgeModel model,
                                 KnowledgeControl control)
        {

            log.Debug(" tree view ctor ... ");

            this.controller = controller;
            this.model = model;
            this.control = control;

            // TODO: disable all tabs initially
            this.control.Enabled = false;

        }

        public void update(ISubject subject)
        {

            log.Debug(" tree view updating... ");

            KnowledgeTreeView.rebindFrames(model.data, control);

            // enable tabs if any
            log.Debug(" data is empty? " + model.data.isEmpty);
            this.control.Enabled = !model.data.isEmpty;

            // TODO: Anton? references from IR to input files
            // it's required at navigation impl (fileShownAsTree)

            // TODO: add normalize and other Anton's logic

            log.Debug(" tree view updated .");
        }

        // TODO: Ontology stuff
        public static void rebindFrames(KnowledgeAdapter data,
                                        KnowledgeControl control)
        {
            // LEVEL #I - FILE LEVEL
            TreeNode root = KnowledgeTreeView.makeNode("Frame", 0, null, null);

            // LEVEL #II - CATEFORY OF FRAME LEVEL
            TreeNode classes = KnowledgeTreeView.makeNode("Class frames", 1, null, root);
            TreeNode instances = KnowledgeTreeView.makeNode("Instance frames", 1, null, root);
            TreeNode rules = KnowledgeTreeView.makeNode("Ruleset frames", 1, null, root);

            // LEVEL #III - CONCRETE FRAME LEVEL
            TreeNode node;
            log.Debug(" data.getFrames.Count="+data.getFrames().Values.Count);
            foreach (object obj in data.getFrames().Values) {

                string name = KnowledgeAdapter.getFrameName(obj);
                log.Debug(" frame - "+name);

                if (KnowledgeAdapter.isClassFrame(obj)){

                    node = KnowledgeTreeView.makeNode(name, 2, obj, classes);
                    
                } else if (KnowledgeAdapter.isInstanceFrame(obj)){

                    node = KnowledgeTreeView.makeNode(name, 2, obj, instances);
                    
                } else {
                    log.Debug(" warning - the frame is nobody's");
                    continue;
                }

                KnowledgeTreeView.rebindSlots(obj, node);
            }

            // TODO: include this cycle into previous somehow
            log.Debug(" data.getRules.Count=" + data.getRules().Values.Count);
            foreach (object obj in data.getRules().Values)
            {
                string name = KnowledgeAdapter.getFrameName(obj);
                log.Debug(" frame - " + name);

                if (KnowledgeAdapter.isRuleFrame(obj)) {
                    node = KnowledgeTreeView.makeNode(name, 2, obj, rules);
                } else {
                    log.Debug(" warning - the frame is nobody's");
                }

                // TODO: need to implement
                // buildSlotsForFrame(frame, curNode);
            }

            // ooh!
            control.rebindFrames(root);
        }

        public static void rebindSlots(object tag, TreeNode parent) {

            // LEVEL #IIII - CATEGORY OF SLOT LEVEL
            TreeNode owns = KnowledgeTreeView.makeNode("Own slots", 1, tag, parent);
            TreeNode instances = KnowledgeTreeView.makeNode("Instance slots", 1, tag, parent);

            // LEVEL #IIIII - CONCRETE SLOT LEVEL
            TreeNode node;
            foreach (Object obj in KnowledgeAdapter.getOwnSlots(tag).Values)
            {
                string name = KnowledgeAdapter.getSlotName(obj);
                log.Debug(" slot - "+name);
                node = KnowledgeTreeView.makeNode(name, 3, obj, owns);
            }

            foreach (Object obj in KnowledgeAdapter.getInstanceSlots(tag).Values)
            {
                string name = KnowledgeAdapter.getSlotName(obj);
                log.Debug(" slot - " + name);
                node = KnowledgeTreeView.makeNode(name, 3, obj, instances);
            }
        }

        public static TreeNode makeNode(String name, int index, Object tag, TreeNode parent) {

            TreeNode node = new TreeNode(name);
            node.SelectedImageIndex = node.ImageIndex = index;
            node.Tag = tag;

            if (parent != null) {
                parent.Nodes.Add(node);
            }
            
            return node;
        }

    }
}
