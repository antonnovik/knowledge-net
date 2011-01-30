using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Knowledge.Editor.View
{
    public partial class AddConceptForm : Form
    {
        public AddConceptForm()
        {
            InitializeComponent();
        }

        public void setConceptForm(string name, TreeNode treeNode) {
            this.textBox1.Text = name;
            this.treeView1.Nodes.Clear();
	    this.treeView1.Nodes.Add(treeNode);
        }
    }
}