using System;
using System.Reflection;
using Knowledge.Library;
using System.Collections;
using System.IO;
using Knowledge;
using System.Collections.Generic;

namespace Knowledge
{

	// CSharp Expert types
	enum FrameType {dataFrame, rulesetFrame}
	public class VarTable 
	{
		private IDictionary itemsByIden;  // key is iden
		private IDictionary itemsByScope; // key is scope
		private IDictionary itemsByPos; // key is a position in the buffer
			public VarTable()
		{
			itemsByIden = new SortedList();
			itemsByScope = new SortedList();
			itemsByPos = new SortedList();
		}

		public void add(TblItem itm) 
		{
			if (!itemsByIden.Contains(itm.iden))
			{
				itemsByIden.Add (itm.iden, new ArrayList());
			} 
			if (!itemsByScope.Contains(itm.scope))
			{
				itemsByScope.Add (itm.scope, new ArrayList());
			}

			((ArrayList)itemsByIden[itm.iden]).Add(itm);
			((ArrayList)itemsByScope[itm.scope]).Add(itm);
			
			itemsByPos.Add(itm.pos, itm);

		}

		/// <summary>
		/// Gets identificators which belong to C# Code <code>csCode</code>.
		/// </summary>
		/// <param name="csCode"></param>
		/// <returns>sorted list of identificators which belongs to C# Code <code>csCode</code></returns>
		public IList getIdens(CScode csCode) 
		{
			// algorithm should be optimized to logN complexity
			IList idens = new ArrayList();
			ArrayList positions = new ArrayList(itemsByPos.Keys);
			positions.Sort();
			foreach (int pos in positions) 
				if (pos >= csCode.begPos && pos < csCode.endPos) 
					idens.Add (itemsByPos[pos]);

			return idens;
		}

		public override string ToString()
		{
			string result="Table of Variables:\n";
			foreach (object obj in itemsByIden.Values)
			{
				foreach (object obj2 in (ArrayList)obj)
				{
					result+=(obj2+"\n");
				}
			}
			return result;
		}

	}

	public class TblItem 
	{
		public string iden;
		public string scope;
		public int pos;
		public int pos2;
		public bool isLeftPartExp;

		private void makeQualident()
		{
			// removes extra spaces
			if (iden=="")
				return;

			string[] parts = iden.Split(new Char[] {'.'});
			iden="";
			for (int i=0; i < parts.Length-1; i++)
			{
				iden+=(parts[i].Trim()+".");
			}
			iden+=parts[parts.Length-1].Trim();

			/*if (scope!="")
			{
				iden=scope+"."+iden;
			}*/
		}

		public TblItem(string iden, string scope, int pos, int pos2, bool isLeftPartExp)
		{
			this.iden = iden;
			this.scope = (scope == null)? "" : scope;
			makeQualident();
			this.pos = pos;
			this.pos2 = pos2;
			this.isLeftPartExp = isLeftPartExp;
		}
		public override string ToString()
		{
			string result="Iden: "+iden+"\n";
			result+="scope: "+scope+"\n";
			result+="Pos: "+pos+" Pos2: "+pos2+"\n";
			result+=("IsLeftPartExp: "+isLeftPartExp);
			return result;
		}

	}

	public class CScode 
	{
		private int innerBegPos;
		private int innerEndPos;

		public int begPos 
		{
			get 
			{
				return innerBegPos;
			}
		}

		public int endPos
		{
			get
			{
				return innerEndPos;
			}
		}

		public string csCode
		{
			get 
			{
				return ExpComp.parser.scanner.buffer.GetString(begPos, endPos);
			}
		}

		public CScode(int begPos, int endPos) 
		{
			this.innerBegPos = begPos;
			this.innerEndPos = endPos;
		}


		public override string ToString()
		{
			return "("+begPos+", "+endPos+"): "+csCode;
		}

	}

	
	public class DataFrame : ISemanticsChecker
	{
		public enum FrameTypes {instanceFrame, classFrame};
		public string iden;
		
		public CScode csCode = new CScode (0, 0);
		public FrameTypes frameType;
		public IList isA;
		public IDictionary ownSlots;
		public IDictionary instanceSlots;

		public void addOwnSlot (Slot slot) 
		{
			ownSlots.Add(slot.iden, slot);
		}

		public void addInstanceSlot (Slot slot)
		{
			instanceSlots.Add(slot.iden, slot);
		}

		public void addIsA (string iden)
		{
			isA.Add(iden);
		}


		public DataFrame(string iden, FrameTypes frameType)
		{
			this.iden = iden;
			this.frameType = frameType;
			instanceSlots = new SortedList();
			ownSlots = new SortedList();
			isA = new ArrayList();
		}

		/// <summary>
		/// Gets slot by its identificator
		/// </summary>
		/// <param name="iden">short name of own slot</param>
		/// <returns>instance of Class Slot, in case if slot is not found, returns null</returns>
		public virtual Slot getSlot (string iden)
		{
			if (iden.Split('.').Length != 1)
				return null;

			if (frameType.Equals(FrameTypes.instanceFrame)) 
			{
				if (this.ownSlots[iden] != null) 
					return (Slot)this.ownSlots[iden];

				if (isA.Count == 1) 
					return ExpComp.getDataFrame((string)isA[0]).getInstanceOrOwnSlot(iden);
			}
			else if (frameType.Equals(FrameTypes.classFrame))
			{
				if (this.ownSlots[iden] != null) 
					return (Slot)this.ownSlots[iden];

				foreach (string frame in isA) 
				{
					if (ExpComp.getDataFrame(frame).getSlot(iden) != null)
						return ExpComp.getDataFrame(frame).getSlot(iden);
				}
			}

			return null;
		}


		public virtual Slot getInstanceSlot (string iden) 
		{
			if (iden.Split('.').Length != 1)
				return null;

			if (this.instanceSlots[iden] != null)
				return (Slot)this.instanceSlots[iden];

			foreach (string frame in isA) 
			{
				if (ExpComp.getDataFrame(frame).getInstanceSlot(iden) != null) 
					return (Slot)ExpComp.getDataFrame(frame).getInstanceSlot(iden);
			}

			return null;
		}

		
		public virtual Slot getInstanceOrOwnSlot (string iden) 
		{
			if (iden.Split('.').Length != 1)
				return null;

			if (this.ownSlots[iden] != null)
				return (Slot)this.ownSlots[iden];

			if (this.instanceSlots[iden] != null)
				return (Slot)this.instanceSlots[iden];

			foreach (string frame in isA) 
			{
				if (ExpComp.getDataFrame(frame).getInstanceOrOwnSlot(iden) != null) 
					return (Slot)ExpComp.getDataFrame(frame).getInstanceOrOwnSlot(iden);
			}

			return null;
		}


		public override string ToString()
		{
			string result=("~~~ Frame: "+iden+"~~~\n");
			result+="C# code: "+csCode+"\n";
			result+=("FrameType: " + ((frameType == FrameTypes.classFrame)? "class" : "instance") + "\n");
			result+="Is a: ";
			foreach (object obj in isA) 
				result+=(obj+";");
			result+=("\n~~~Own Slots: \n");
			foreach (object obj in ownSlots.Values) 
				result+=(obj+"\n");
			result+=("\n~~~");

			result+=("Instance Slots: \n");
			foreach (object obj in instanceSlots.Values) 
				result+=(obj+";\n");
			result+=("~~~\n");
			return result;
		}
		/// <summary>
		/// Search through the frame <code>frameName</code> and its generalizated frames, frames 
		/// that are inherited from the frame <code>root</code>
		/// </summary>
		/// <param name="root"></param>
		/// <param name="frameName"></param>
		/// <returns>name of frame which has inherited from root, if no frames are found, it returns ""</returns>
		private static string getInheritedFrame(string root, string frameName)
		{
			DataFrame frame = ExpComp.getDataFrame(frameName);

			if (frame.isA.Contains (root)) 
				return frameName;

			foreach (string frm in frame.isA) 
			{
				string result = getInheritedFrame(root, frm);
				if (result != "")
					return frameName;
			}

			return "";
		}

		#region ISemanticsChecker Members
	    public bool checkSemantics()
		{
			// TODO it is needed to add checking of name-unique checking
			bool result=true;

			foreach (string frame in this.isA)
			{
				if (ExpComp.getDataFrame(frame)==null) 
				{
					MsgBundle.addError ("Frame \"{0}\" doesn\'t exist, so the frame \"{1}\" can\'t be inherited from this frame.");
				}
				if (ExpComp.getDataFrame(frame).frameType.Equals(FrameTypes.instanceFrame)) 
				{
					MsgBundle.addError ("Frame \"{0}\" can\'t be inherited from Instance Frame \"{1}\"", this.iden, frame);
					result=false;
				}
			}

			if (this.frameType.Equals(FrameTypes.instanceFrame)) 
			{
				if (isA.Count > 1)
				{
					MsgBundle.addError ("Instance Frame \"{0}\" can be inherited only from the one Class Frame", this.iden);
					result=false;
				}
				else if (isA.Count == 0)
				{
					MsgBundle.addError ("Instance Frame \"{0}\" must be inherited from some Class Frame", this.iden);
					result=false;
				}
			}
			string loopFrame = getInheritedFrame(this.iden, this.iden);
			if (loopFrame != "")
			{
				MsgBundle.addError ("Loop inheritance is not allowed: the frame \"{0}\" is inherited from \"{1}\" and vice vesa", this.iden, loopFrame);
				result=false;
			}
			return result;
		}

		#endregion
	}

	public class RuleFrame : ISemanticsChecker 
	{
		public string iden;
		public CScode csCode = new CScode (0,0);
		public string goal;
		private IDictionary context;
		private IDictionary parameters;
		private IDictionary ruleSlots;

		public void addContextItem(Parameter param) 
		{
			context.Add (param.parameterValue, param);
		}

		public IDictionary getContext() 
		{
			return context;
		}
		public void addParameter(Parameter param) 
		{
			parameters.Add (param.parameterValue, param);
		}

		public IDictionary getParameters()
		{
			return parameters;
		}


		public void addRule(Rule rule) 
		{
			ruleSlots.Add (rule.iden, rule);
		}

		public IDictionary getRules()
		{
			return ruleSlots;
		}


		public RuleFrame(string iden)
		{
			this.iden = iden;
			context = new SortedList();
			parameters = new SortedList();
			ruleSlots = new SortedList();
		}


		/// <summary>
		/// Collects all slots that could be used in the ruleset 
		/// </summary>
		/// <returns>instance of <code>IDictionary</code> with Slots, where key=identifier and value= instance of the class <code>Slot</code></returns>
		public IDictionary getSlots() 
		{
			IDictionary dict = new SortedList();

			foreach (DictionaryEntry entry in context) 
			{
				string paramName = (string)entry.Key;
				Parameter param = (Parameter)entry.Value;
				if (param.isInstance)
				{
					IList list2 = ExpComp.getNamesOfOwnAndInstanceSlots(param.parameterValue);
					foreach (string slotName in list2)
						dict.Add(slotName, ExpComp.getDataFrame(param.parameterValue).getInstanceOrOwnSlot(slotName));
				} 
				else 
				{
					IList list2 = ExpComp.getNamesOfOwnSlots(param.parameterValue);
					foreach (string slotName in list2)
						dict.Add(slotName, ExpComp.getDataFrame(param.parameterValue).getSlot(slotName));
				}
			}

			return dict;
		}


		/// <summary>
        /// Collects all parameters of ruleset, that have been passed to the ruleset by reference
        /// </summary>
        /// <returns>instance of <code>IDictionary</code> with <code>Parameter</code>, that equals to names of parameters</returns>
		public IDictionary getRefParams()
		{
			IDictionary dict = new SortedList();
			foreach (DictionaryEntry entry in parameters)
			{
				if (((Parameter)entry.Value).isRef) 
					dict.Add(entry.Key, entry.Value);
			}
			return dict;
		}


		public override string ToString()
		{
			string result;
			result=("*** Rule frame: "+iden+"\n");
			result+=("CS code: "+csCode+"\n");
			result+=("Goal: "+goal+"\n");
			
			result+="Context:\n";
			foreach (object obj in context.Values)
			{
				result+=obj;
			}

			result+="Parameters:\n";
			foreach (object obj in parameters.Values)
			{
				result+=obj;
			}

			result+="Rule slots:\n";
			foreach (object obj in ruleSlots.Values)
			{
				result+=obj;
			}

			return result;
		}

		#region ISemanticsChecker Members

		public bool checkSemantics()
		{
			// TODO:  Add RuleFrame.checkSemantics implementation
			bool result = true;

			// checks that instance frames in the context are exist and instance items are class frame;
			foreach (Parameter param in getContext().Values)
			{
				DataFrame frame = ExpComp.getDataFrame(param.parameterValue);
				if (frame == null)
				{
					MsgBundle.addError ("Incorrect reference to the frame in the context of the ruleset \"{0}\". The frame \"{1}\" doesn't exist", this.iden, param.parameterValue);
					result = false;
				}
				else
				{
					if (frame.frameType.Equals(DataFrame.FrameTypes.instanceFrame) && param.isInstance)
					{
						MsgBundle.addError ("Incorrect instance parameter in the context: \"instance {0}\". With keyword \"instance\" goes class frames only.", param.parameterValue);
						result = false;
					}
				}
			}
			return result;
		}

		#endregion

	}

	public class Slot 
	{
		public enum SlotCategory {OwnSlot, InstanceSlot}
		public string iden="";
		public string restriction="";
		public string slotType=null;
		public string slotValue="";
		public SlotCategory   category=SlotCategory.OwnSlot;

		public override string ToString()
		{
			string result;
			result=("\t*** Slot: "+iden+";\n");
			result+=("\tRestriction: "+restriction+"\n");
			result+=("\tCategory: "+category+"\n");
			result+=("\tType: "+slotType+"\n");
			result+=("\tValue: "+slotValue+"\n");
			result+=("\t***\n");
			return result;
		}

		public Slot() {}
		public Slot(SlotCategory category) 
		{
			this.category = category;
		}
	}

	public class Parameter 
	{
		public bool isRef;
		public bool isInstance;
		public string parameterType;
		public string parameterValue;

		public override string ToString()
		{
			string result;
			result=("\t\t*** Parameter\n");
			result+=("\t\tReference flag: " + isRef + "\n");
			result+=("\t\tInstance flag: " + isInstance + "\n");
			result+=("\t\tValue: " + parameterType + "\n");
			result+=("\t\tType: " + parameterValue + "\n");
			result+="\t\t***\n";

			return result;
		}

	}

	public class Rule 
	{
		public string iden;
		public CScode condition;
		public CScode trueBody = new CScode (0, 0);
		public CScode falseBody = new CScode (0,0);
		public string comment = "";

		public override string ToString()
		{
			string result;
			result="\t*** Rule: " + iden + "\n";
			result+=("\tCondition: " + condition + "\n");
			result+=("\tTrue body: " + trueBody + "\n");
			result+=("\tFalse body: " + falseBody + "\n");
			result+=("\tComment: " + comment + "\n");
			return result;
		}

	}

    // Knowledge.NET types
    public class Concept : ISemanticsChecker
    {
        public string iden = string.Empty;
        public readonly IList<string> isSubconceptOf = new List<string>();
        public readonly IList<string> disjointWith = new List<string>();
        public readonly IList<string> isIntersectionOf = new List<string>();
        public readonly IList<string> isUnionOf = new List<string>();
        public readonly IList<string> isComplementOf = new List<string>();

        #region ISemanticsChecker Members

        public bool checkSemantics()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }

    public class Property : ISemanticsChecker
    {
        public enum PropertyType {DataType, Object};

        public string iden = String.Empty;
        public PropertyType type;

        public bool isFunctional = false;
        public bool isTransitive = false;
        public bool isSymmetric = false;

        private IList<string> _domain = new List<string>();
        public IList<string> domain
        {
            get
            {
                return _domain;
            }
        }
        
        private IList<string> _range = new List<string>();
        public IList<string> range
        {
            get
            {
                if (isSymmetric)
                    return domain;
                else
                    return _range;
            }
        }

        private string _inversePropertyName = string.Empty;
        public string inversePropertyName
        {
            set
            {
                if (isSymmetric)
                    throw new KnowledgeException("Symmetric propery is inverse itself. So you can't set the name for inverse property");
                else if (type == PropertyType.DataType)
                    throw new KnowledgeException("Datatype property doesn't have inverse property");
                else
                    _inversePropertyName = value;
            }
            get
            {
                if (type == PropertyType.DataType)
                    throw new KnowledgeException("Datatype property doesn't have inverse property");
                else if (_inversePropertyName == string.Empty)
                    return "inverse_of_" + iden + iden.GetHashCode();
                else if (isSymmetric)
                    return iden;
                else
                    return _inversePropertyName;
            }
        }

        #region ISemanticsChecker Members

        public bool checkSemantics()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }

    public class Individual : ISemanticsChecker
    {
        public string iden = string.Empty;

        public readonly IList<string> aliases = new List<string> ();

        #region ISemanticsChecker Members

        public bool  checkSemantics()
        {
 	        throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }


	/// <summary>
	/// Generates inner representation from C# Expert code
	/// </summary>
public class ExpComp
	{
        public static Parser parser = null; // initialized in entry point (e.g. func main()) 
        public static string curFr = null; // current frame iden

        public static CScode precedingCScode = new CScode(0,0);

        public static VarTable varTbl = new VarTable();
		public static IDictionary dataFrames = new SortedList();
		public static IDictionary ruleFrames = new SortedList();

		public static void addDataFrame(DataFrame frame)
		{
			dataFrames.Add (frame.iden, frame);
		}

		public static void addRuleFrame(RuleFrame frame)
		{
			ruleFrames.Add (frame.iden, frame);
		}


		public static DataFrame getDataFrame(string iden)
		{
			return (DataFrame)dataFrames[iden];
		}

		public static RuleFrame getRuleFrame(string iden)
		{
			return (RuleFrame)ruleFrames[iden];
		}

		public static IList getNamesOfOwnSlots (string frame) 
		{
			IList list = new ArrayList();
			foreach (string slotName in getDataFrame(frame).ownSlots.Keys) 
				list.Add(slotName);
			foreach (string frameName in getDataFrame(frame).isA)
			{
				IList list2 = getNamesOfOwnSlots(frameName); // takes slots of inherited frames
				foreach (string slotName in list2)
					list.Add(slotName);
			}
			return list;
		}

		public static IList getNamesOfOwnAndInstanceSlots (string frame)
		{
			IList list = new ArrayList();
			foreach (string slotName in getDataFrame(frame).ownSlots.Keys) 
				list.Add(slotName);
			foreach (string slotName in getDataFrame(frame).instanceSlots.Keys)
				list.Add(slotName);
			foreach (string frameName in getDataFrame(frame).isA)
			{
				IList list2 = getNamesOfOwnAndInstanceSlots (frameName); // takes slots of inherited frames
				foreach (string slotName in list2)
					list.Add(slotName);
			}
			return list;
		}

        /// <summary>
        /// Removes all data related with last parsing
        /// </summary>
        public static void Clear()
        {
            parser = null;
            curFr = null;

            precedingCScode = new CScode(0, 0);
            
            varTbl = new VarTable();
		    dataFrames = new SortedList();
		    ruleFrames = new SortedList();

        }

		
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] arg) 
		{
			Console.WriteLine ("Knowledge.NET version {0}, Anton Novikov, 2004-2006", Assembly.GetExecutingAssembly().GetName().Version);
			Console.WriteLine();
			if (arg.Length == 0) 
			{
				Console.WriteLine("-- file name expected");
				// System.Environment.Exit(1);
                return;
			}

            Clear();
            Scanner scanner = new Scanner(arg[0]);
            parser = new Parser(scanner);
            parser.Parse();

			#region debugInfo
			
			/*foreach (object fr in dataFrames.Values) 
				Console.WriteLine(fr);
			foreach (object fr in ruleFrames.Values)
				Console.WriteLine(fr);
			
			Console.WriteLine(varTbl);

			Console.WriteLine("CS code: " + precedingCScode);
			if (parser.errors.count>0)
			{
				Console.WriteLine ("Total Errors: {0}", parser.errors.count);
				Console.WriteLine ("Failed!");
				System.Environment.Exit(0);
			}*/
			#endregion

			OutputTextGenerator generator = new OutputTextGenerator();
			StreamWriter s=null;
			try 
			{
				s = new StreamWriter(arg[0]+"_.cs", false, System.Text.Encoding.Default);
				generator.generateOutputProgram(s);
			} 
			catch (IOException) 
			{
				Console.WriteLine("--- Cannot open file {0}", arg[0]+"_.cs");
				// System.Environment.Exit(0);
                return;
			}
			finally
			{
				if (s!=null)
                    s.Close();

			}


		}



        public static void convert(string filename)
        {
            string[] args = { filename };
            Main(args);
        }
    }


	/// <summary>
	/// Generates C# code from inner representation
	/// </summary>
	public class OutputTextGenerator 
	{
		private StreamWriter s; // output stream

		// key -- full-qualified slot identificator (e.g. Frame1.slot1), value -- instance of Slot
		private IDictionary tblSlots;

		public OutputTextGenerator()
		{
			tblSlots = new SortedList();
		}
		/// <summary>
		/// Initialize instance slots which are not initialized by default value
		/// </summary>
		/// <param name="root_iden">name of frame, whom slots are initialized</param>
		/// <param name="frame">class frame, root_iden instance frame is inherited from it </param>
		private void initializeInstanceSlotsByDefaultValue(StreamWriter s, string root_iden, DataFrame frame)
		{
			// checks that each instance slot of this class frame is initialized in inherited instance frame
			foreach (Slot slot in frame.instanceSlots.Values) 
			{
				if (!tblSlots.Contains(root_iden+"."+slot.iden)) 
				{
					if (!slot.slotValue.Trim().Equals(""))
					{
						s.WriteLine("\t\tCSharpExpert.addSlot(\"{0}\", new Slot({1}));", root_iden+"."+slot.iden, slot.slotValue);
						tblSlots.Add(root_iden+"."+slot.iden, slot);
					} 
					else 
					{
						MsgBundle.addError ("the value of the slot \""+slot.iden+"\" should be defined in the frame \""+frame.iden+"\"");
					}
				}
			}

			// recursive  
			foreach (string frameName in frame.isA) 
			{
				initializeInstanceSlotsByDefaultValue(s, root_iden, ExpComp.getDataFrame(frameName));
			}
		}

		private void generateCSharpExpertClass() 
		{
			s.WriteLine ("[Serializable]");
			s.WriteLine ("public class CSharpExpert : CSharpExpertAbstract");
			s.WriteLine ("{");
			s.WriteLine ("\tprotected override void createFrames()");
			s.WriteLine ("\t{");
			s.WriteLine ("\t\tbase.createFrames();");
			if (ExpComp.dataFrames.Count > 0) 
			{
				s.WriteLine ("// *** Data frames");
				foreach (object obj in ExpComp.dataFrames.Values) 
				{
					DataFrame frame = (DataFrame)obj;
					s.WriteLine("\t\tdataFrames.Add (\""+frame.iden+"\", new "+frame.iden+"());");
				}
				s.WriteLine ("// *** End of data frames");
				s.WriteLine ();
			}
			if (ExpComp.ruleFrames.Count > 0) 
			{
				s.WriteLine ("// *** Rulesets");
				foreach (object frame in ExpComp.ruleFrames.Values) 
				{
					s.WriteLine("\t\trulesetFrames.Add (\""+((RuleFrame)frame).iden+"\", new "+((RuleFrame)frame).iden+"());");
				}
				s.WriteLine ("// *** End of rulesets");
				s.WriteLine ("");
			}
			s.WriteLine ("\t}");
			s.WriteLine ("\tprivate static CSharpExpertAbstract createInstance()");
			s.WriteLine ("\t{");
			s.WriteLine ("\t\tinstance = new CSharpExpert();");
			s.WriteLine ("\t\treturn instance;");
			s.WriteLine ("\t}");
			s.WriteLine ("\tpublic static void firstStart()");
			s.WriteLine ("\t{");
			s.WriteLine ("\t\tcreateInstance();");
			s.WriteLine ("\t\tfirstStartAbstract();");
			s.WriteLine ("\t}");
			s.WriteLine ("}");
		}

		/// <summary>
		/// This method makes semantic analysis of slots and normalizes the structure of slots
		/// </summary>
		private void normalizeSlots() 
		{
			foreach (DataFrame frame in ExpComp.dataFrames.Values) 
			{
				if (frame.frameType.Equals(DataFrame.FrameTypes.instanceFrame)) 
				{
					foreach (Slot slot in frame.ownSlots.Values) 
					{
						if (slot.slotType == null) 
						{
							Slot instanceSlot = frame.getInstanceSlot(slot.iden);
							if (instanceSlot == null) 
								MsgBundle.addError ("Can't assign the value to the slot \""+slot.iden+"\" in the frame \""+frame.iden+"\", which has no defenition");
							else
								slot.slotType = instanceSlot.slotType;
						}
					}
				}
			}
		}


		/// <summary>
		/// Changes accessors to slots
		/// </summary>
		/// <param name="code">C# Code</param>
		/// <param name="root">name of current frame, in case of global scope root == ""</param>
		/// <returns>piece of code where accessors to the slots are changed</returns>
		private string changeSlotAccessors (CScode code, string root) 
		{
			IList idens = ExpComp.varTbl.getIdens(code);
			int offset = code.begPos;
			int csPos = 0;
			string result = "";
			foreach (TblItem qualident in idens) 
			{
				string[] parts = qualident.iden.Split('.');
				if (root != "" && parts.Length >= 1)
				{
					try 
					{
						DataFrame frame = ExpComp.getDataFrame(root);
						if (frame != null) 
						{
							Slot slot = frame.getSlot(parts[0]);
							int pos = qualident.pos-offset;
							string castParam = (!qualident.isLeftPartExp)? "("+slot.slotType+")" : "";
							result += (code.csCode.Substring(csPos, pos-csPos)+castParam+"(CSharpExpert.getDataFrame(\""+root+"\").getSlot(\""+parts[0]+"\")).slotValue");
							csPos = pos + parts[0].Length;
						}
					} 
					catch (FrameIsNotFound) {}
				}
					
				if (parts.Length >= 2)
				{
					try
					{
						DataFrame frame = ExpComp.getDataFrame(parts[0]);
						if (frame != null) 
						{
							Slot slot = frame.getSlot(parts[1]);
							int pos = qualident.pos-offset;
							if (slot != null)
							{
								string castParam = (!qualident.isLeftPartExp)? "("+slot.slotType+")" : "";
								result += (code.csCode.Substring(csPos, pos-csPos)+castParam+"(CSharpExpert.getDataFrame(\""+parts[0]+"\").getSlot(\""+parts[1]+"\")).slotValue");
								csPos = pos + ((string)(parts[0]+"."+parts[1])).Length;
							}
						}
					} 

					catch (FrameIsNotFound) {}
				}
			}

            result+=code.csCode.Substring(csPos, code.csCode.Length-csPos);
			return result;
		}


		private string changeSlotAccessors (CScode code, RuleFrame ruleset) 
		{
			IList idens = ExpComp.varTbl.getIdens(code);
			IDictionary slots = ruleset.getSlots();
			IList slotNames = new ArrayList(slots.Keys);
			int offset = code.begPos;
			int csPos = 0;
			string result = "";
			foreach (TblItem qualident in idens)
			{
				string[] parts=qualident.iden.Split(new char[]{'.'});
				if (slotNames.Contains(parts[0]))
				{
					Slot slot = (Slot)slots[parts[0]];
					int pos = qualident.pos-offset;
					string castParam = (!qualident.isLeftPartExp)? "("+slot.slotType+")" : "";
					result += (code.csCode.Substring(csPos, pos-csPos)+castParam+"(CSharpExpert.getRulesetFrame(\""+ruleset.iden+"\").getSlot(\""+parts[0]+"\")).slotValue");
					csPos = pos + parts[0].Length;
				}
			}

            result+=code.csCode.Substring(csPos, code.csCode.Length-csPos);
			return result;
		}

		/*private string changeAccessorsInRules (CScode code, string ruleset) 
		{
			IList idens = ExpComp.varTbl.getIdens(code);
			IList parameters = ExpComp.getRuleFrame(ruleset).getNamesOfSlots(); //todo: insert parameters too
			int offset = code.begPos;
			int csPos = 0;
			string result = "";

			foreach (TblItem item in idens) 
			{
				string[] parts = item.iden.Split(new char[]{'.'});
				if (parameters.Contains(parts[0])) 
				{
					int pos = code.begPos-offset;
					result = result.Substring (csPos, pos-csPos) + ruleset + "." + parts[0];
				}
			}

            result+=code.csCode.Substring(csPos, code.csCode.Length-csPos);
			return result;
		}*/


		private void generateDataFrame (DataFrame dataFrame) 
		{
			s.WriteLine ("public class {0} : {1}", dataFrame.iden, (dataFrame.frameType == DataFrame.FrameTypes.classFrame)? "ClassFrame" : "InstanceFrame");
			s.WriteLine ("{");
			s.WriteLine ("\t//Attributes");
			s.WriteLine ("\t{0}", changeSlotAccessors(dataFrame.csCode, dataFrame.iden));

			// constructor
			s.WriteLine ("\tpublic {0}()", dataFrame.iden);
			s.WriteLine ("\t{");
			s.WriteLine ("\t\t//Initialization isA");
			foreach (string frameName in dataFrame.isA)
				s.WriteLine ("\t\tisA.Add(\"{0}\");", frameName);
			s.WriteLine ("\t}");

			// resetSlots()
			s.WriteLine ("\tpublic override void resetSlots()");
			s.WriteLine ("\t{");
			if(dataFrame.ownSlots.Count > 0)
				s.WriteLine ("\t\t// Initialization of slots");
			foreach (Slot slot in dataFrame.ownSlots.Values) 
			{
				if (tblSlots.Contains(dataFrame.iden+"."+slot.iden)) 
				{
					MsgBundle.addWarning ( slot.iden + " is overriden in the frame "+dataFrame.iden);
				}
				else 
				{
					s.WriteLine("\t\tCSharpExpert.addSlot(\"{0}\", new Slot({1}));", dataFrame.iden+"."+slot.iden, slot.slotValue);
					tblSlots.Add(dataFrame.iden+"."+slot.iden, slot);
				}
			}

			if (dataFrame.frameType.Equals(DataFrame.FrameTypes.instanceFrame))
			{
				if (dataFrame.isA.Count == 1) 
				{
					s.WriteLine ("\t\t// Default initialization of instance slots");
					initializeInstanceSlotsByDefaultValue(s, dataFrame.iden, (DataFrame)ExpComp.dataFrames[(string)dataFrame.isA[0]]);
				} 
				else if (dataFrame.isA.Count != 0) 
				{
					MsgBundle.addError ("Instance frame can't be inherited more then from one class frame");
				}
			} 

			else if (dataFrame.frameType.Equals(DataFrame.FrameTypes.classFrame))
			{
				s.WriteLine ("\t\t// Initialization of default values for instance slots");
				foreach (Slot slot in dataFrame.instanceSlots.Values) 
				{
					if (slot.slotValue!="")
					{
						if (tblSlots.Contains(dataFrame.iden+"."+slot.iden)) 
						{
							MsgBundle.addWarning ( slot.iden + " is overriden in the frame "+dataFrame.iden);
						}
						else 
						{
							s.WriteLine("\t\tCSharpExpert.addSlot(\"{0}\", new Slot({1}));", dataFrame.iden+"."+slot.iden, slot.slotValue);
							tblSlots.Add(dataFrame.iden+"."+slot.iden, slot);
						}
					} 
					else if (slot.slotValue == "" && tblSlots.Contains(dataFrame.iden+"."+slot.iden)) 
					{
						MsgBundle.addWarning ( slot.iden + " is overriden in the frame "+dataFrame.iden);
					}
				}
			}
			s.WriteLine ("\t}");
			
			// getFrameName() method
			s.WriteLine ("\tpublic override string getName()");
			s.WriteLine ("\t{");
			s.WriteLine ("\t\treturn \"{0}\";", dataFrame.iden);
			s.WriteLine ("\t}");
			s.WriteLine ("}");
		}


		private void genereateRule (Rule rule, RuleFrame ruleset) 
		{
			s.WriteLine ("\tprivate class {0} : Knowledge.Library.Rule", rule.iden);
			s.WriteLine ("\t{");
	
			// *** condition()
			s.WriteLine ("\t\tpublic override bool condition()");
			s.WriteLine ("\t\t{");
			s.WriteLine ("\t\t\treturn {0};", changeSlotAccessors(rule.condition, ruleset));
			s.WriteLine ("\t\t}");
			// ***
			
			// *** if_statement()
			s.WriteLine ("\t\tpublic override void if_statement()");
			s.WriteLine ("\t\t{0}", changeSlotAccessors(rule.trueBody, ruleset));
			// ***
			
			// *** else_statement()
			if (!rule.falseBody.csCode.Equals(""))
			{
				s.WriteLine ("\t\tpublic override void else_statement()");
				s.WriteLine ("\t\t{0}", changeSlotAccessors(rule.falseBody, ruleset));
			}
			// ***

			// *** getComment()
			if (!rule.comment.Equals(""))
			{
				s.WriteLine ("\t\tpublic override string getComment()");
				s.WriteLine ("\t\t{");
				s.WriteLine ("\t\t\treturn {0} ;", rule.comment);
				s.WriteLine ("\t\t}");
			}
			// ***

            // *** getName()
			s.WriteLine ("\t\tpublic override string getName()");
			s.WriteLine ("\t\t{");
			s.WriteLine ("\t\t\treturn \"{0}\";", rule.iden);
			s.WriteLine ("\t\t}");
			// ***

			s.WriteLine ("\t}");
		}

		private void generateRuleFrame (RuleFrame ruleset)
		{
			s.WriteLine ("public class {0} : RulesetFrame", ruleset.iden);
			s.WriteLine ("{");
			s.WriteLine ("\t");
			s.WriteLine ("\t{0}", changeSlotAccessors(ruleset.csCode, ""));
			s.WriteLine ("\t// Parameters of the ruleset");
			foreach (Parameter param in ruleset.getParameters().Values)
				s.WriteLine ("\tprivate static {0} {1};", param.parameterType, param.parameterValue);

			foreach (Rule rule in ruleset.getRules().Values)
			{
				s.WriteLine();
				genereateRule(rule, ruleset);
			}
			s.WriteLine();

			// *** constructor
			s.WriteLine("\tpublic {0}() : base()", ruleset.iden);
			s.WriteLine("\t{");
			s.WriteLine("\t\tgoal = \"{0}\";", ruleset.goal);
			s.WriteLine("\t\t");
			foreach (Parameter param in ruleset.getContext().Values)
			{
				s.WriteLine("\t\tcontext.Add(\"{0}\", new ContextParam(\"{0}\", {1}));", param.parameterValue, (param.isInstance)? "true" : "false");
			}

			// add rules to the collection
			foreach (string rule in ruleset.getRules().Keys)
				s.WriteLine ("\t\trules.Add(\"{0}\", new {0}());", rule);
			s.WriteLine("\t}");
			s.WriteLine();

			
			// initParameters()
			if (ruleset.getParameters().Count > 0)
			{
				s.Write("\tpublic void initParameters (");
				int i = 1;
				int len = ruleset.getParameters().Count;
				foreach (Parameter param in ruleset.getParameters().Values)
				{
					s.Write ("{0} {1}", param.parameterType, param.parameterValue);
					if (i!=len) 
						s.Write(", ");
					i++;
				}
				s.WriteLine(")");
				s.WriteLine("\t{");
				s.WriteLine("\t\tisParamsInitialized = true;");
				foreach (string paramName in ruleset.getParameters().Keys)
				{
					s.WriteLine ("\t\t{0}.{1}={1};", ruleset.iden, paramName);
				}
				s.WriteLine("\t}");
			}
			// ***


			// *** getRefParams()
			if (ruleset.getRefParams().Count > 0)
			{
				s.Write("\tpublic void getRefValues(");
			
				int i=1;
				int len = ruleset.getRefParams().Count;

				foreach (Parameter param in ruleset.getRefParams().Values)
				{
					s.Write ("ref {0} {1}", param.parameterType, param.parameterValue);
					if (i!=len) 
						s.Write(", ");
					i++;
				}
				s.WriteLine(")");
				s.WriteLine("\t{");

				foreach (string paramName in ruleset.getRefParams().Keys)
				{
					s.WriteLine ("\t\t{0}={1}.{0};", paramName, ruleset.iden);
				}
				s.WriteLine("\t}");
			}
			
			// *** InitContext()
			s.Write("\tpublic void initContext(");
			bool firstElem=true;
			IList instanceFrames = new ArrayList();
			foreach (Parameter param in ruleset.getContext().Values)
			{
				if (param.isInstance) 
				{
					if (!firstElem) 
						s.Write(", ");
					firstElem=false;
					s.Write ("string {0}", param.parameterValue);
					instanceFrames.Add(param.parameterValue);
				}
			}
			s.WriteLine(")");
			s.WriteLine("\t{");
			if (ruleset.getParameters().Count == 0)
				s.WriteLine("\t\tisParamsInitialized = true;");
			s.WriteLine("\t\tbase._initContext();");
			foreach (string frame in instanceFrames)
			{
				s.WriteLine("\t\tif (!CSharpExpertAbstract.getDataFrame({0}).isInherited(\"{0}\"))", frame);
                s.WriteLine("\t\t\tthrow new KnowledgeException(\"Instance frame \\\"\"+{0}+\"\\\"is not inherited from Class Frame\\\"{0}\\\"\");", frame);
				s.WriteLine("\t\tIDictionary slots = CSharpExpertAbstract.getDataFrame({0}).getSlots();", frame);
				s.WriteLine("\t\tforeach (DictionaryEntry entry in slots)");
				s.WriteLine("\t\t{");
				s.WriteLine("\t\t\tif (this.slots.Contains(entry.Key))");
                s.WriteLine("\t\t\t\tthrow new KnowledgeException(\"Not unique names of slot in the context of the frame \\\"\"+getName()+\"\\\"\");");
				s.WriteLine("\t\t\tthis.slots.Add(entry.Key, entry.Value);");
				s.WriteLine("\t\t}");
			}
			s.WriteLine("\t}");
			// ***
			
			// *** getParams()
			if (ruleset.getParameters().Count > 0)
			{
				s.WriteLine ("\tpublic override ComparableDict getParams()");
				s.WriteLine ("\t{");
				s.WriteLine ("\t\tComparableDict dict = base.getParams();");
				s.WriteLine ("\t\tobject paramValue = null;");
				foreach (string param in ruleset.getParameters().Keys)
				{
					s.WriteLine("\t\tparamValue={0};", param);
					s.WriteLine ("\t\tif ({0} is ICloneable)", param);
					s.WriteLine ("\t\t\tparamValue=((ICloneable)(object){0}).Clone();", param);
					s.WriteLine ("\t\tdict.Add(\"{0}\", paramValue);", param);
					s.WriteLine ("\t\t");
				}
				s.WriteLine ("\t\treturn dict;");
				s.WriteLine ("\t}");
			}

			// ***

			// *** getName()
			s.WriteLine();
			s.WriteLine("\tpublic override string getName()");
			s.WriteLine("\t{");
			s.WriteLine("\t\treturn \"{0}\";", ruleset.iden);
			s.WriteLine("\t}");
			// ***
			s.WriteLine ("}");

		}

		public void generateOutputProgram(StreamWriter s)
		{
			if (s==null)
				return;

			this.s=s;

			// semantic anlyzes and normalization
			normalizeSlots();
			bool checkSemantics = true;
			foreach (DataFrame frame in ExpComp.dataFrames.Values) 
				checkSemantics = frame.checkSemantics();
			foreach (RuleFrame frame in ExpComp.ruleFrames.Values)
				checkSemantics = frame.checkSemantics();
			if (!checkSemantics)
			{
				Console.WriteLine ("Total errors: {0}", MsgBundle.NumberOfErrors + ExpComp.parser.errors.count);
				Console.WriteLine ("Failed!");
				return;
			}

			Assembly assembly = Assembly.GetExecutingAssembly();
			s.WriteLine ("//This file was generated by Knowledge.NET Cross-Compiler ver. "+assembly.GetName().Version);
			s.WriteLine ("using System;");
			s.WriteLine ("using Knowledge.Library;");
			s.WriteLine (changeSlotAccessors(ExpComp.precedingCScode, ""));
			

            s.WriteLine ("#region Knowledge.NET generated code");
			// output code generator
			generateCSharpExpertClass();
			
			foreach (DataFrame frame in ExpComp.dataFrames.Values) 
				generateDataFrame(frame);

			foreach (RuleFrame frame in ExpComp.ruleFrames.Values)
				generateRuleFrame(frame);
			s.WriteLine ("#endregion");


			//s.WriteLine("}");
			Console.WriteLine ("Total errors: {0}", MsgBundle.NumberOfErrors + ExpComp.parser.errors.count);
			Console.WriteLine ("Successful!");            	
		}
	}
	
	
	class MsgBundle 
	{
		private static int errCounter=0;
		public static int NumberOfErrors
		{
			get
			{
				return errCounter;
			}
		}
		public enum MessageType {warning, error};
		
		public static void addError(string str, params string[] strParams) 
		{
			addMessage(MessageType.error, str, strParams);
		}

		public static void addWarning(string str, params string[] strParams) 
		{
			addMessage(MessageType.warning, str, strParams);
		}

		public static void addMessage(MessageType type, string str, params string[] strParams)
		{
			switch (type) 
			{
				case MessageType.error:
					Console.Write ("Knowledge.NET Error: ");
					Console.WriteLine (str, strParams);
					errCounter++;
					break;
				case MessageType.warning:
					Console.Write ("Knowledge.NET Warning: ");
					Console.WriteLine (str, strParams);
					break;
				default:
					Console.Write ("Knowledge.NET: ");
					Console.WriteLine (str, strParams);
					break;
			}
		}
	}


	interface ISemanticsChecker 
	{
		bool checkSemantics();
	}
}
