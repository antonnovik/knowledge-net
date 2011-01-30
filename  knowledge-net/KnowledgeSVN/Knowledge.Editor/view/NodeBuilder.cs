using System;
using System.Collections.Generic;
using System.Text;
using Knowledge;
using System.Windows.Forms;

using log4net;

using Knowledge.Editor.Model;
using Knowledge.Library;

namespace Knowledge.Editor.View
{
 

    class NodeBuilder
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(NodeBuilder));


        public static TreeNode buildConceptTreeView() {

            TreeNode node1 = new TreeNode("Thing");
            node1.SelectedImageIndex = node1.ImageIndex = (int)4;

            TreeNode node11 = new TreeNode("Vehicle");
            TreeNode node12 = new TreeNode("Color");
            node11.SelectedImageIndex = node11.ImageIndex = (int)4;
            node12.SelectedImageIndex = node12.ImageIndex = (int)4;
            node1.Nodes.Add(node11);
            node1.Nodes.Add(node12);

            TreeNode node111 = new TreeNode("Plane");
            TreeNode node112 = new TreeNode("Submarine");
            node111.SelectedImageIndex = node111.ImageIndex = (int)4;
            node112.SelectedImageIndex = node112.ImageIndex = (int)4;
            node11.Nodes.Add(node111);
            node11.Nodes.Add(node112);
            return node1;
        }

        public static TreeNode buildPropertyTreeView()
        {
            TreeNode peer = new TreeNode("Properties");
            TreeNode node1 = new TreeNode("hasColor");
            TreeNode node2 = new TreeNode("hasName");
            peer.SelectedImageIndex = peer.ImageIndex = (int)5;
            node1.SelectedImageIndex = node1.ImageIndex = (int)5;
            node2.SelectedImageIndex = node2.ImageIndex = (int)5;
            peer.Nodes.Add(node1);
            peer.Nodes.Add(node2);
            return peer;
        }

    }
}
