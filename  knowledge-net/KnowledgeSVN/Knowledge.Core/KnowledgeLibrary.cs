using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


namespace Knowledge.Library
{
	public enum ChainingMethod {forward, backward};

	[Serializable()]
	public abstract class CSharpExpertAbstract
	{
		protected IDictionary slots; // key is full-qualified name of a slot (e.g. Frame1.slot1), value is instance of the class Slot 
		
		[NonSerialized]
		protected IDictionary dataFrames; // key is a name of the frame, value is instance of the class DataFrame
		[NonSerialized]
		protected IDictionary rulesetFrames; // key is a name of ruleset frame, value is instance of the class RuleFrame

		[NonSerialized]
		protected TextWriter outputStream;

		protected static CSharpExpertAbstract instance = null;
		protected static CSharpExpertAbstract getInstance() 
		{
			if (instance == null) 
			{
				throw new NullReferenceException("CSharpExpertAbstract::instance member should be intialized by means of CreateInstance() method.");
			}

			return instance;
		}

		public static void setOutputStream(Stream s)
		{
			getInstance().outputStream = new StreamWriter(s);
		}

		public static void setOutputStream(TextWriter s)
		{
			getInstance().outputStream = s;
		}

		
		public static CSharpExpertAbstract loadKnoweldgeBase(string fname) 
		{
			Stream stream = File.Open (fname, FileMode.Open);
			BinaryFormatter formatter = new BinaryFormatter();
			instance = (CSharpExpertAbstract)formatter.Deserialize (stream);
			stream.Close();

			//
			setOutputStream(Console.Out);
			getInstance().createFrames();
			return instance;
		}
		public static CSharpExpertAbstract loadKnoweldgeBase()
		{
			return loadKnoweldgeBase("CSharpExpertKnowledgeStorage.bin");
		}

		public static void saveKnoweldgeBase(string fname) 
		{
			Stream stream = File.Open (fname, FileMode.Create);
			BinaryFormatter formatter = new BinaryFormatter();
			formatter.Serialize (stream, getInstance());
			stream.Close();
		}

		public static void saveKnoweldgeBase()
		{
			saveKnoweldgeBase("CSharpExpertKnowledgeStorage.bin");
		}

		
		protected CSharpExpertAbstract()
		{
			outputStream = Console.Out;
		}

		protected static void firstStartAbstract() 
		{
			getInstance().slots = new SortedList();
			
			getInstance().createFrames();

			foreach (DataFrame frame in getInstance().dataFrames.Values)
				frame.resetSlots();

		}


		public static RulesetFrame getRulesetFrame(string key)
		{
			RulesetFrame frame = (RulesetFrame)getInstance().rulesetFrames[key];
			if (frame == null)
				throw new FrameIsNotFound("The ruleset "+key+" is not found");
			return (RulesetFrame)getInstance().rulesetFrames[key];
		}

		public static DataFrame getDataFrame(string key)
		{
			DataFrame frame = (DataFrame)getInstance().dataFrames[key];
			if (frame == null)
				throw new FrameIsNotFound("The frame "+key+" is not found");
			return frame;
		}

		
		public static void addSlot(string key, Slot slot) 
		{
			getInstance().slots.Add (key, slot);
		}
		public static Slot getSlot(string key)
		{
			Slot slot = (Slot)getInstance().slots[key];
			if (slot == null)
				throw new SlotIsNotFound("The slot \""+key+"\" is not found");
			return slot;
		}

		public static bool isSlot(string key) 
		{
			return getInstance().slots.Contains(key);
		}
		
		/// <summary>
		/// Returns slots with defined prefix (e.g if prefix = "A" then the slots, that have a key in the dictionary <code>slots</code>, 
		/// which equals "A.x", "A.y" will be returned)
		/// </summary>
		public static IDictionary getSlots (string prefix) 
		{
			IDictionary dict = new SortedList();
			foreach (DictionaryEntry entry in getInstance().slots) 
				if ((((string)entry.Key).Split(new char[]{'.'}))[0].Equals(prefix))
					dict.Add (entry.Key, entry.Value);
			return dict;
		}

		
		protected virtual void createFrames()
		{
			rulesetFrames = new SortedList();
			dataFrames = new SortedList();
		}

		public static TextWriter getOutputStream() 
		{
			return getInstance().outputStream;
		}
	}

	
	// Data Frames and Slots
	public abstract class InstanceFrame : DataFrame
	{
	}

	public abstract class ClassFrame : DataFrame
	{
	}

	public abstract class DataFrame : INamedClass	 
	{
		protected IList isA;

		public DataFrame()
		{
			isA = new ArrayList();
		}

		/// <summary>
		/// Returns own slot of frame by the name of own slot
		/// </summary>
		/// <param name="iden">short or full-qualified name of own slot</param>
		/// <exception cref="SlotIsNotFound">throw this exception in case of the slot is not found</exception>
		/// <returns>instance of the class Slot</returns>
		public virtual Slot getSlot (string iden)
		{
			string[] idens = iden.Split('.');
			Slot result = null;

			if (idens.Length>2 || idens.Length==0)
				throw new SlotIsNotFound();

			if (idens.Length == 1)
			{
				if (!CSharpExpertAbstract.isSlot(getName()+"."+iden)) 
				{
					foreach (string frame in isA) 
					{
						try 
						{
							result=CSharpExpertAbstract.getDataFrame(frame).getSlot(iden);
						}
						catch (SlotIsNotFound) {}

						if (result != null) 
						{
							break;
						}
					}

					if (result == null) 
					{
						throw new SlotIsNotFound();
					}
				} 
				else 
					result =  CSharpExpertAbstract.getSlot(getName()+"."+iden);
			} 
			else if (idens.Length == 2) 
				result = CSharpExpertAbstract.getDataFrame(idens[0]).getSlot(idens[1]);

			return result;
		}


		public virtual IDictionary getSlots() 
		{
			IDictionary dict = new SortedList();
			
			foreach (DictionaryEntry entry in CSharpExpertAbstract.getSlots(this.getName()))
				dict.Add(((String)entry.Key).Split(new char[]{'.'})[1], entry.Value);

			foreach (string frameName in this.isA) 
			{
				IDictionary slots = CSharpExpertAbstract.getDataFrame(frameName).getSlots();
				foreach (DictionaryEntry entry in slots)
				{
					if (!dict.Contains(entry.Key)) // in case of instance slot, the defined value must be used
						dict.Add ((String)entry.Key, entry.Value);
				}
			}

			return dict;
		}


		public virtual bool isInherited(string origin)
		{
			foreach (string frameName in isA)
			{
				if (frameName == origin)
					return true;
				else if (CSharpExpertAbstract.getDataFrame(frameName).isInherited(origin))
					return true;
			}

			return false;
		}
		/// <summary>
		///  recreate slots, as it was defined in source file
		/// </summary>
		public abstract void resetSlots();

		public abstract string getName();
	}


	[Serializable]
	public class Slot 
	{
		private object current_value;
		public object slotValue 
		{
			set 
			{
				if (this.current_value != value)
				{
				
					object oldValue = null;
					if (this.slotValue is ICloneable)
						oldValue = ((ICloneable)this.slotValue).Clone();
					else
						oldValue = this.slotValue;

					setValue(value); //todo: add type checking
				
					if (oldValue == null) 
						ifAdded();
					else if (slotValue == null) 
						ifRemoved();
					else
						ifModified();
				}
			}
			get 
			{
				ifNeeded();
				return current_value;
			}
		}

		public Slot(object slotValue) 
		{
			setValue(slotValue);
		}

		public Slot() {}

		protected virtual void setValue(object new_value)
		{
			this.current_value=new_value;
		}

		public virtual void ifAdded() {}
		public virtual void ifRemoved() {}
		public virtual void ifNeeded() {}
		public virtual void ifModified() {}

	}


    // Rulesets and Rules
public abstract class RulesetFrame : INamedClass
	{
		public class ContextParam
		{
			public string name;
			public bool isInstance;
			public ContextParam (string name, bool isInstance)
			{
				this.name = name;
				this.isInstance = isInstance;
			}
		}

		protected IDictionary		rules;
		protected IDictionary		slots;
		protected IDictionary		context;
		public  bool  				isParamsInitialized;
		public  bool				isContextInitialized;
		protected string			goal;
		public    string			Goal
		{
			get 
			{
				return goal;
			}
		}
		public RulesetFrame()
		{
			isParamsInitialized = false;
			isContextInitialized = false;
			rules = new SortedList();
			context = new SortedList();
		}


		public IDictionary getRules()
		{
			return rules;
		}

		public Slot getSlot (string iden)
		{
			Slot slot = (Slot)slots[iden];
			if (slot == null)
				throw new SlotIsNotFound ("Slot \""+iden+"\" is not found in the frame \""+getName()+"\"");
			return slot;
		}


		public virtual ComparableDict getParams ()
		{
			ComparableDict dict = new ComparableDict();
			foreach (DictionaryEntry entry in slots) 
			{
				object slotValue = ((Slot)entry.Value).slotValue;
				if (slotValue is ICloneable)
					slotValue = ((ICloneable)slotValue).Clone();
				dict.Add (entry.Key, slotValue);
			}
			return dict;
		}


		protected void _initContext () 
		{
			this.slots = new SortedList();
			isContextInitialized = true;
			foreach (ContextParam param in context.Values)
			{
				if (!param.isInstance)
				{
					IDictionary slots = CSharpExpertAbstract.getDataFrame(param.name).getSlots();
					foreach (DictionaryEntry entry in slots) 
					{
						if (this.slots.Contains(entry.Key))
							throw new KnowledgeException("Not unique names of slot in the context of the frame \""+getName()+"\"");
						this.slots.Add(entry.Key, entry.Value);
					}
				}
			}
		}


		public abstract string getName();
	}


	public abstract class Rule : INamedClass 
	{
		public virtual bool condition() {return true;}
		public virtual void if_statement() {}
		public virtual void else_statement() {}
		public virtual string getComment() {return "";}
		public virtual int getPriority() {return 1;}

		public abstract string getName();

	}


	// Chaininig System
	public class ProductionSystem 
	{
		// defines, whether comments are shown during consultation or not
		public static bool showComments=false;

		private static ComparableDict statesByRule; // key -- name of rule, value -- IDictionary of states of parameters
		private static IList firedRules; // this list contains rules, that have been fired in this session, they have less priority than rules that have no fired yet

		private ProductionSystem() {}

		public static void consult (string ruleset)
		{
			RulesetFrame frame = CSharpExpertAbstract.getRulesetFrame(ruleset);
			consult (ruleset, frame.Goal);
		}
		public static void consult (string ruleset, string goal) 
		{
			consult(ruleset, goal, ChainingMethod.forward);
		}

		public static bool consult (string ruleset, string goal, ChainingMethod method)
		{
			statesByRule = new ComparableDict();
			firedRules = new ArrayList();
			foreach (string ruleName in CSharpExpertAbstract.getRulesetFrame(ruleset).getRules().Keys)
				statesByRule.Add (ruleName, new ArrayList());
			
			if (method.Equals(ChainingMethod.backward))
				return backwardChaining(ruleset, goal);
			else if (method.Equals(ChainingMethod.forward)) 
				return forwardChaining(ruleset, goal);

			return false;
		}

		public static bool backwardChaining (string ruleset, string goal)
		{
			throw new KnowledgeException ("Backward chaining mehtod isn't supported by the current version");
			return false;
		}

		public static bool forwardChaining (string rulesetName, string goal)
		{
			IList candidates1 = new ArrayList(); // candidates that have high proirity (first lane)
			IList candidates2 = new ArrayList(); // candidates that have low priority (second lane)
			RulesetFrame ruleset = CSharpExpertAbstract.getRulesetFrame(rulesetName);
			if (!ruleset.isContextInitialized)
				throw new KnowledgeException ("Context of ruleset \""+rulesetName+"\" must be initialized before consulting");
			if (!ruleset.isParamsInitialized)
				throw new KnowledgeException ("Parameters of ruleset \""+rulesetName+"\" must be set before consulting");

			foreach (Rule rule in ruleset.getRules().Values)
			{
				if (rule.condition() == true && 
					!((IList)statesByRule[rule.getName()]).Contains(ruleset.getParams())) 
				{
					if (firedRules.Contains(rule))
						candidates2.Add(rule);
					else
						candidates1.Add(rule);
				}
			}

			if (candidates1.Count == 0 && candidates2.Count == 0)
			{
				ruleset.isContextInitialized = false;
				ruleset.isParamsInitialized = false;
				CSharpExpertAbstract.getOutputStream().WriteLine ("Goal = \"{0}\" can't be calculated by using the set of rules \"{1}\"", goal, ruleset.getName());
				return false;
			}

			// method of conflict-resolution is very simple, just call first rule
			ComparableDict parameters = ruleset.getParams();
			Rule choosenRule = null;
			if (candidates1.Count > 0)
				choosenRule = ((Rule)candidates1[0]);
			else if (candidates2.Count > 0)
				choosenRule = ((Rule)candidates2[0]);
 
			((IList)statesByRule[choosenRule.getName()]).Add(parameters);
			object old_value_of_goal = parameters[goal];

			choosenRule.if_statement();  // "if" block of chosen rule is fired
			firedRules.Add(choosenRule);
			if (showComments)
				CSharpExpertAbstract.getOutputStream().WriteLine("Rule \"{0}\" has been fired:\n {1}", choosenRule.getName(), choosenRule.getComment());

			foreach (Rule rule in ruleset.getRules().Values) // "else" blocks of all other rules are fired
				if (rule != choosenRule)
					rule.else_statement();

			if (!ruleset.getParams()[goal].Equals(old_value_of_goal))
			{
				ruleset.isContextInitialized = false;
				ruleset.isParamsInitialized = false;
				CSharpExpertAbstract.getOutputStream().WriteLine ("Goal is calculated: \"{0}\"={1}", goal, ruleset.getParams()[goal]);
				return true;
			}

            return forwardChaining(rulesetName, goal);
			}


	}



	public interface INamedClass 
	{
		string getName();
	}


	// This class allows to create comparable ditionaries (maps) of elements
	public class ComparableDict : SortedList
	{
		public ComparableDict() : base()
		{}

		/// <summary>
		/// Comparable dictionaries should have equal collections of key.
		/// </summary>
		/// <param name="dict"></param>
		/// <returns>collection of keys, where valuesin this dictionary and the dictionary <code>dict</code> are different.</returns>
		public IList getDifferentValues(IDictionary dict) 
		{
			IList list = new ArrayList();
			if (this.Count != dict.Count)
				throw new Exception("Comparable dictionaries should have equal collections of key: number of elements are different in the collections.");
			foreach (string key in dict.Keys) 
			{
				if (!this.Contains(key))
					throw new Exception ("Comparable dictionaries should have equal collections of key: one collection doesn't contatin the key \""+key+"\" which is contained by other collection");
					
				if (!this[key].Equals(dict[key]))
					list.Add(key);
			}

			return list;
		}

		public override bool Equals(object obj)
		{
			if (obj is IDictionary) 
			{
				if (((IDictionary)obj).Count != this.Count) 
					return false;

				foreach (DictionaryEntry entry in (IDictionary)obj)
				{
					if (this.Contains(entry.Key)) 
					{
						if (!this[entry.Key].Equals(entry.Value)) 
							return false;
					} 
					else 
						return false;
				}
				return true;
			} 
			else 
				return base.Equals (obj);
		}

	}


	// Exceptions
	public class KnowledgeException : Exception
	{
		public KnowledgeException() : base ()
		{}

		public KnowledgeException(string msg) : base (msg)
		{}
	}
	
	public class SlotIsNotFound : KnowledgeException 
	{
		public SlotIsNotFound() :  base()
		{}

		public SlotIsNotFound(string msg) : base (msg)
		{}
	}

	public class FrameIsNotFound : KnowledgeException
	{
		public FrameIsNotFound() : base()
		{}

		public FrameIsNotFound(string msg) : base(msg)
		{}
	}

}
