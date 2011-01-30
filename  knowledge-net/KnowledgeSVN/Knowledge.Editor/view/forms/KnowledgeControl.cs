using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using EnvDTE;
using EnvDTE80;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;
using System.Reflection;

using log4net;

using Knowledge.Editor.AddIn;
using Knowledge.Editor.Controller;
using Knowledge.Editor.Model;


using Knowledge.KIF.Converter.gui;

namespace Knowledge.Editor.View
{
	public partial class KnowledgeControl : UserControl
	{
        private static readonly ILog log = LogManager.GetLogger(typeof(KnowledgeTreeView));

        private Knowledge.Editor.AddIn.Environment environment;
		private CurrentProject currentProject;
		private bool InProgress = false;

		public KnowledgeControl()
		{
			InitializeComponent();

			toolStripButton_RemoveAspectAssembly.Enabled = false;
			button_FindJoinpoints.Enabled = false;
			button_WeaveAspects.Enabled = false;
			InProgress = false;

			toolStripButton_MoveAssemblyDown.Enabled = false;
			toolStripButton_MoveAssemblyUp.Enabled = false;
            toolStripButton_Options.Enabled = false;

            tabPage_Visualization.Hide();
            toolStripButton_Visualise.Enabled = false;
            toolStripButton_VisOptions.Enabled = false;

            tabPage_Concepts.Hide();
            tabPage_Properties.Hide();
            tabPage_Individuals.Hide();

        }

        public Knowledge.Editor.AddIn.Environment Environment
		{
			get
			{
				return environment;
			}
			set
			{
				environment = value;
			}
		}

		public CurrentProject CurrentProject
		{
			get
			{
				return currentProject;
			}
			set
			{
				currentProject = value;
			}
		}

		/// <summary>
		/// Gets the path of the assembly that owns the given node
		/// </summary>

        // TODO: move this (and similar) code somewhere else !!!
        public void rebindFrames(TreeNode root){
            this.treeView4.Nodes.Clear();
            this.treeView4.Nodes.Add(root);
        }

        public void setConceptTreeView(TreeNode root) {
            this.treeView1.Nodes.Clear();
            this.treeView1.Nodes.Add(root);

            // concept tabs
            this.textBox1.Text = "Vehicle";
            this.listBox2.Items.Add("hasColor");
            this.listBox2.Items.Add("hasName");
        }

        public void setPropertyTreeView(TreeNode peer)
        {
            this.treeView2.Nodes.Clear();
            this.treeView2.Nodes.Add(peer);

            // properties tab
            this.textBox2.Text = "hasColor";
            this.comboBox1.Items.Add("Instance");
            this.listBox3.Items.Add("Vehicle");
            this.listBox4.Items.Add("Color");
        }

        public void setIndividual(TreeNode root) {
            this.treeView3.Nodes.Clear();
            this.treeView3.Nodes.Add(root);

            // individuals tab
            this.listBox5.Items.Add("Lada");
        }

        private void toolStripButton17_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Will be implemented ASAP");
            // TODO: uncomment this as soon as Anton implements flush
            // Manager.getInstance().processEvent(new ActionEvent(ActionEvent.RUN_REFRESH));
        }


        private void convertButton_Click(object sender, EventArgs e) {
            ConvForm convForm = new ConvForm(KnowledgeAdapter.getCurrentFilepath(), KnowledgeController.instance().environment.ApplicationObject);//TODO:
            convForm.ShowDialog();
        }

        private void toolStripButton18_Click(object sender, EventArgs e)
        {
            log.Debug(" click on button to convert");

            KnowledgeController instance = KnowledgeController.instance();

            if (instance == null) {
                log.Debug(" controller is null ");
            }

            instance.convert();
        }

        private void toolStripButton_Frames_Insert_Click(object sender, EventArgs e)
        {
            // TODO: implement
            MessageBox.Show("Frames adding isn't implemented");
        }

        private void toolStripButton_Frames_Delete_Click(object sender, EventArgs e)
        {
            if (treeView4.SelectedNode == null)
            {
                MessageBox.Show("Please, select the item in the tree view");
                return;
            }

            TreeNode selectedNode = treeView4.SelectedNode;
            if (DialogResult.Yes == MessageBox.Show("Are you sure that you want to delete the selected item?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question)){
                // text view
//                Manager.getInstance().processEvent(new ChangeEvent((KnowledgeNode)treeView4.SelectedNode.Tag, ChangeEvent.REMOVE));
                // graphic view
                selectedNode.Remove();
            }
         }

        private void toolStripButton_Frames_Commit_Click(object sender, EventArgs e)
        {
            // TODO: implement
            MessageBox.Show("Not implemented");
        }

        private void toolStripButton15_Click(object sender, EventArgs e)
        {
            if (treeView4.SelectedNode == null)
            {
                MessageBox.Show("Please, select the item in the tree view");
                return;
            }

            log.Debug(" click on button to navigate");

            KnowledgeController instance = KnowledgeController.instance();

            if (instance == null)
            {
                log.Debug(" controller is null ");
            }

            string filepath = KnowledgeAdapter.getCurrentFilepath();
            log.Debug(" filepath = " + filepath);
            TextEditorHelper.navigate(instance, treeView4.SelectedNode.Tag, filepath);
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not implemented");
            /*
            AddConceptForm form = new AddConceptForm();
	        form.setConceptForm("Plane", NodeBuilder.buildConceptTreeView());
            form.Show();
             */
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not implemented");
            /*
            ComboBox comboBox = new ComboBox();
            comboBox.Items.Add("Instance");
            ListBox listBox1 = new ListBox();
            listBox1.Items.Add("Vehicle");
            ListBox listBox2 = new ListBox();
            listBox2.Items.Add("Color");
            AddPropertyForm form = new AddPropertyForm();
	        form.setPropertyForm("hasColor", comboBox, listBox1, listBox2);
            form.Show();
             */
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not implemented");
            /*
            AddIndividualForm form = new AddIndividualForm();
	        form.setIndividualForm("Lada", NodeBuilder.buildConceptTreeView());
            form.Show();
             */
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not implemented");
            /*
            if (DialogResult.Yes == MessageBox.Show("Are you sure you want to delete 'Color'?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
            }
             */
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not implemented");
            /*
            if (DialogResult.Yes == MessageBox.Show("Are you sure you want to delete 'hasColor'?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
            }
             */
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not implemented");
            /*
            if (DialogResult.Yes == MessageBox.Show("Are you sure you want to delete 'Lada'?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
            }
             */
        }

        private void toolStripButton19_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Navigate to concept");
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Navigate to property");
        }

        private void toolStripButton13_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Navigate to individual");
        }

        private void treeView4_DoubleClick(object sender, EventArgs e)
        {
            if (treeView4.SelectedNode == null)
            {
                MessageBox.Show("Please, select an item of the tree view");
                return;
            }

            log.Debug(" double click on treeview to navigate");

            KnowledgeController instance = KnowledgeController.instance();

            if (instance == null)
            {
                log.Debug(" controller is null ");
		return;
            }

            if (treeView4.SelectedNode.Tag == null)
            {
                log.Debug(" tag is null ");
		return;
            }

            string filepath = KnowledgeAdapter.getCurrentFilepath();
            log.Debug(" filepath = "+filepath);
            TextEditorHelper.navigate(instance, treeView4.SelectedNode.Tag, filepath);
        }

	    // navigate to frame / slot
        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Navigate to frame / slot");
        }
    }
}
