using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace Sample_of_Expert_System_1__Math_
{

	/// <summary>
	/// Summary description for Question.
	/// </summary>
	public class Question : System.Windows.Forms.UserControl
	{
		public string QuestionValue
		{
			set 
			{
				lblQuestion.Text=value;
			}
			get
			{
				return lblQuestion.Text;
			}
		}
		
		public int AnswerIndex 
		{
			set 
			{
				cbxAnswer.SelectedIndex=value;
			}
			get
			{
				return cbxAnswer.SelectedIndex;
			}
		}

		public string[] Items
		{
			set
			{
				cbxAnswer.Items.Clear();
				foreach (object item in value)
                    cbxAnswer.Items.Add(item);
			}
			get
			{
				string [] coll = new string[cbxAnswer.Items.Count];
				int i=0;
				foreach (object item in cbxAnswer.Items)
					coll[i]=cbxAnswer.Items[i++].ToString();
				return coll;
			}
		}
		private System.Windows.Forms.Label lblQuestion;
		private System.Windows.Forms.ComboBox cbxAnswer;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Question()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call

		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.lblQuestion = new System.Windows.Forms.Label();
			this.cbxAnswer = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// lblQuestion
			// 
			this.lblQuestion.Dock = System.Windows.Forms.DockStyle.Left;
			this.lblQuestion.Location = new System.Drawing.Point(0, 0);
			this.lblQuestion.Name = "lblQuestion";
			this.lblQuestion.Size = new System.Drawing.Size(424, 24);
			this.lblQuestion.TabIndex = 0;
			this.lblQuestion.Text = "label1";
			this.lblQuestion.Click += new System.EventHandler(this.lblQuestion_Click);
			// 
			// cbxAnswer
			// 
			this.cbxAnswer.Dock = System.Windows.Forms.DockStyle.Right;
			this.cbxAnswer.Location = new System.Drawing.Point(431, 0);
			this.cbxAnswer.Name = "cbxAnswer";
			this.cbxAnswer.Size = new System.Drawing.Size(121, 21);
			this.cbxAnswer.TabIndex = 1;
			this.cbxAnswer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbxAnswer_KeyDown);
			this.cbxAnswer.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cbxAnswer_KeyUp);
			this.cbxAnswer.TextChanged += new System.EventHandler(this.cbxAnswer_TextChanged);
			this.cbxAnswer.SelectedIndexChanged += new System.EventHandler(this.cbxAnswer_SelectedIndexChanged);
			// 
			// Question
			// 
			this.Controls.Add(this.cbxAnswer);
			this.Controls.Add(this.lblQuestion);
			this.Name = "Question";
			this.Size = new System.Drawing.Size(552, 24);
			this.ResumeLayout(false);

		}
		#endregion

		private void cbxAnswer_TextChanged(object sender, System.EventArgs e)
		{
			if (cbxAnswer.SelectedIndex != -1)
			{
				if (!cbxAnswer.Items.Contains(cbxAnswer.Text))
				{
					cbxAnswer.Text = cbxAnswer.Items[cbxAnswer.SelectedIndex].ToString();
				}
			}
			else
				cbxAnswer.Text="";
		}

		private void lblQuestion_Click(object sender, System.EventArgs e)
		{
		
		}

		private void cbxAnswer_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		}

		private void cbxAnswer_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
		}

		private void cbxAnswer_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
		}


	}
}
