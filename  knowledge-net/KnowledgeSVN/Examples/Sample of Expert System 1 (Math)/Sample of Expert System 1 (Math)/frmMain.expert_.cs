//This file was generated by Knowledge.NET Cross-Compiler ver. 1.0.2462.8423
using System;
using Knowledge.Library;
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace Sample_of_Expert_System_1__Math_
{
	public enum Answers {Yes, No};

	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private string[] Answers1 = {"Yes", "No"};
		private Sample_of_Expert_System_1__Math_.Question question1;
		private Sample_of_Expert_System_1__Math_.Question question2;
		private Sample_of_Expert_System_1__Math_.Question question3;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox txtDecision;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.question1 = new Sample_of_Expert_System_1__Math_.Question();
			this.question2 = new Sample_of_Expert_System_1__Math_.Question();
			this.question3 = new Sample_of_Expert_System_1__Math_.Question();
			this.txtDecision = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// question1
			// 
			this.question1.AnswerIndex = -1;
			this.question1.Items = new string[] {
													"Yes",
													"No"};
			this.question1.Location = new System.Drawing.Point(8, 16);
			this.question1.Name = "question1";
			this.question1.QuestionValue = "Did student score less than 50%?";
			this.question1.Size = new System.Drawing.Size(456, 24);
			this.question1.TabIndex = 0;
			// 
			// question2
			// 
			this.question2.AnswerIndex = -1;
			this.question2.Items = new string[] {
													"Yes",
													"No"};
			this.question2.Location = new System.Drawing.Point(8, 40);
			this.question2.Name = "question2";
			this.question2.QuestionValue = "Did student score more than 50% and less than 80%?";
			this.question2.Size = new System.Drawing.Size(456, 24);
			this.question2.TabIndex = 1;
			// 
			// question3
			// 
			this.question3.AnswerIndex = -1;
			this.question3.Items = new string[] {
													"Yes",
													"No"};
			this.question3.Location = new System.Drawing.Point(8, 64);
			this.question3.Name = "question3";
			this.question3.QuestionValue = "Did student score above 80%?";
			this.question3.Size = new System.Drawing.Size(456, 24);
			this.question3.TabIndex = 2;
			// 
			// txtDecision
			// 
			this.txtDecision.BackColor = System.Drawing.Color.Khaki;
			this.txtDecision.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtDecision.Location = new System.Drawing.Point(8, 96);
			this.txtDecision.Name = "txtDecision";
			this.txtDecision.Size = new System.Drawing.Size(360, 20);
			this.txtDecision.TabIndex = 3;
			this.txtDecision.Text = "";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(376, 96);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(88, 23);
			this.button1.TabIndex = 4;
			this.button1.Text = "Get decision";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(472, 122);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.txtDecision);
			this.Controls.Add(this.question3);
			this.Controls.Add(this.question2);
			this.Controls.Add(this.question1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "Form1";
			this.Text = "Questionnaire ";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.Deactivate += new System.EventHandler(this.Form1_Deactivate);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
			try 
			{
			  	CSharpExpert.loadKnoweldgeBase();
			} catch (System.IO.FileNotFoundException exc)
			{
				CSharpExpert.firstStart();
			}
							
			if ((bool)(CSharpExpert.getDataFrame("Ivanov").getSlot("scoreLess50")).slotValue) 
				question1.AnswerIndex = 0;
			else
				question1.AnswerIndex = 1;
			
			if ((bool)(CSharpExpert.getDataFrame("Ivanov").getSlot("scoreMore50AndLess80")).slotValue)
				question2.AnswerIndex = 0;
			else
				question2.AnswerIndex = 1;
			
			if ((bool)(CSharpExpert.getDataFrame("Ivanov").getSlot("scoreMore80")).slotValue)
				question3.AnswerIndex = 0;
			else
				question3.AnswerIndex = 1;

			txtDecision.Text = (string)(CSharpExpert.getDataFrame("Ivanov").getSlot("decision")).slotValue;
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			if (question1.AnswerIndex == 0)
				(CSharpExpert.getDataFrame("Ivanov").getSlot("scoreLess50")).slotValue = true;
			else
				(CSharpExpert.getDataFrame("Ivanov").getSlot("scoreLess50")).slotValue = false;

			if (question2.AnswerIndex == 0)
				(CSharpExpert.getDataFrame("Ivanov").getSlot("scoreMore50AndLess80")).slotValue = true;
			else
				(CSharpExpert.getDataFrame("Ivanov").getSlot("scoreMore50AndLess80")).slotValue = false;

			if (question3.AnswerIndex == 0)
				(CSharpExpert.getDataFrame("Ivanov").getSlot("scoreMore80")).slotValue = true;
			else
				(CSharpExpert.getDataFrame("Ivanov").getSlot("scoreMore80")).slotValue = false;

            (CSharpExpert.getDataFrame("Ivanov").getSlot("decision")).slotValue = "";
            ((ScoreAdviser)CSharpExpert.getRulesetFrame("ScoreAdviser")).initContext("Ivanov");
			ProductionSystem.consult ("ScoreAdviser", "decision");

			txtDecision.Text = (string)(CSharpExpert.getDataFrame("Ivanov").getSlot("decision")).slotValue;
		}

		private void Form1_Deactivate(object sender, System.EventArgs e)
		{
			CSharpExpert.saveKnoweldgeBase();
		}

	}
}


#region Knowledge.NET generated code
[Serializable]
public class CSharpExpert : CSharpExpertAbstract
{
	protected override void createFrames()
	{
		base.createFrames();
// *** Data frames
		dataFrames.Add ("Ivanov", new Ivanov());
		dataFrames.Add ("Student", new Student());
// *** End of data frames

// *** Rulesets
		rulesetFrames.Add ("ScoreAdviser", new ScoreAdviser());
// *** End of rulesets

	}
	private static CSharpExpertAbstract createInstance()
	{
		instance = new CSharpExpert();
		return instance;
	}
	public static void firstStart()
	{
		createInstance();
		firstStartAbstract();
	}
}
public class Ivanov : InstanceFrame
{
	//Attributes
	
	public Ivanov()
	{
		//Initialization isA
		isA.Add("Student");
	}
	public override void resetSlots()
	{
		// Default initialization of instance slots
		CSharpExpert.addSlot("Ivanov.decision", new Slot(""));
		CSharpExpert.addSlot("Ivanov.scoreLess50", new Slot(false));
		CSharpExpert.addSlot("Ivanov.scoreMore50AndLess80", new Slot(false));
		CSharpExpert.addSlot("Ivanov.scoreMore80", new Slot(false));
	}
	public override string getName()
	{
		return "Ivanov";
	}
}
public class Student : ClassFrame
{
	//Attributes
	
	public Student()
	{
		//Initialization isA
	}
	public override void resetSlots()
	{
		// Initialization of default values for instance slots
		CSharpExpert.addSlot("Student.decision", new Slot(""));
		CSharpExpert.addSlot("Student.scoreLess50", new Slot(false));
		CSharpExpert.addSlot("Student.scoreMore50AndLess80", new Slot(false));
		CSharpExpert.addSlot("Student.scoreMore80", new Slot(false));
	}
	public override string getName()
	{
		return "Student";
	}
}
public class ScoreAdviser : RulesetFrame
{
	
	
	// Parameters of the ruleset

	private class R1 : Knowledge.Library.Rule
	{
		public override bool condition()
		{
			return (bool)(CSharpExpert.getRulesetFrame("ScoreAdviser").getSlot("scoreLess50")).slotValue==true;
		}
		public override void if_statement()
		{
				(CSharpExpert.getRulesetFrame("ScoreAdviser").getSlot("decision")).slotValue = "Reteach number concepts";
			}
		
		public override string getName()
		{
			return "R1";
		}
	}

	private class R2 : Knowledge.Library.Rule
	{
		public override bool condition()
		{
			return (bool)(CSharpExpert.getRulesetFrame("ScoreAdviser").getSlot("scoreMore50AndLess80")).slotValue==true && (bool)(CSharpExpert.getRulesetFrame("ScoreAdviser").getSlot("scoreLess50")).slotValue==false;
		}
		public override void if_statement()
		{
				(CSharpExpert.getRulesetFrame("ScoreAdviser").getSlot("decision")).slotValue = "Additional practice";
			}
		
		public override string getName()
		{
			return "R2";
		}
	}

	private class R3 : Knowledge.Library.Rule
	{
		public override bool condition()
		{
			return (bool)(CSharpExpert.getRulesetFrame("ScoreAdviser").getSlot("scoreMore80")).slotValue==true && (bool)(CSharpExpert.getRulesetFrame("ScoreAdviser").getSlot("scoreMore50AndLess80")).slotValue==false && (bool)(CSharpExpert.getRulesetFrame("ScoreAdviser").getSlot("scoreLess50")).slotValue==false;
		}
		public override void if_statement()
		{
				(CSharpExpert.getRulesetFrame("ScoreAdviser").getSlot("decision")).slotValue = "Begin double digit math.";
			}
		
		public override string getName()
		{
			return "R3";
		}
	}

	public ScoreAdviser() : base()
	{
		goal = "decision";
		
		context.Add("Student", new ContextParam("Student", true));
		rules.Add("R1", new R1());
		rules.Add("R2", new R2());
		rules.Add("R3", new R3());
	}

	public void initContext(string Student)
	{
		isParamsInitialized = true;
		base._initContext();
		if (!CSharpExpertAbstract.getDataFrame(Student).isInherited("Student"))
			throw new KnowledgeException("Instance frame \""+Student+"\"is not inherited from Class Frame\"Student\"");
		IDictionary slots = CSharpExpertAbstract.getDataFrame(Student).getSlots();
		foreach (DictionaryEntry entry in slots)
		{
			if (this.slots.Contains(entry.Key))
				throw new KnowledgeException("Not unique names of slot in the context of the frame \""+getName()+"\"");
			this.slots.Add(entry.Key, entry.Value);
		}
	}

	public override string getName()
	{
		return "ScoreAdviser";
	}
}
#endregion
