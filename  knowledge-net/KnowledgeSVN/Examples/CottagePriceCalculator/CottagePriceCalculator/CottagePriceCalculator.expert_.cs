//This file was generated by Knowledge.NET Cross-Compiler ver. 1.0.2462.6772
using System;
using Knowledge.Library;
using System;
using System.Collections;

namespace CottagePriceCalculator
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	delegate uint CalcCottagePriceFunc (uint space, uint distanceFromSPb, uint numberOfBedrooms, uint numberOfFloors);
      
	class MainClass
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		public static uint priceCalcOfHouses (uint space, uint distanceFromSPb, uint numberOfBedrooms, uint numberOfFloors)
		{
			uint priceOfSqrMetre=12;
			uint result=0;
			if (distanceFromSPb < 1000)
			{
				priceOfSqrMetre=938*(1000-distanceFromSPb)/1000+12; 
			}
			result+=priceOfSqrMetre*space;

			result+=numberOfFloors*3000;
			result+=numberOfBedrooms*300;

			return result;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="comment">Takes a value from the console</param>
		/// <param name="maxValue">sets maximum allowed value</param>
		/// <returns>entered value</returns>
		public static uint getUint (string text, int maxValue)
		{
			Console.Write (text);
			string result=Console.ReadLine();
			try
			{
				uint intResult = Convert.ToUInt32(result);
				if (intResult <= 0 || intResult > maxValue)
				{
					Console.WriteLine ("The value must be in the range from 0 to {0}, try again...", maxValue);
					return getUint (text, maxValue);
				}
				return intResult;
			}
			catch
			{
				Console.WriteLine ("Wrong value, entered value must be integer, try again...");
				return getUint(text, maxValue);
			}
		}

		[STAThread]
		static void Main(string[] args)
		{
			//
			// TODO: Add code to start application here
			//
			bool firstStart=false;
			bool askData=true;
			try 
			{
				CSharpExpert.loadKnoweldgeBase ();
			} 
			catch 
			{
				CSharpExpert.firstStart();
				(CSharpExpert.getDataFrame("MyHouse").getSlot("priceCalculator")).slotValue = new CalcCottagePriceFunc(MainClass.priceCalcOfHouses);
				firstStart = true;
			}
			if (!firstStart)
			{
				Console.WriteLine ("Do you want re-enter all data?");
				Console.WriteLine ("1 - yes");
				Console.WriteLine ("2 - no");
				askData = (getUint("Enter the value from 1 to 2: ", 2) == 1)? true : false;
			}
			bool repeat=true;
			while (repeat)
			{
				if (askData)
				{
					(CSharpExpert.getDataFrame("Buyer").getSlot("moneyAmount")).slotValue = getUint("Enter money amount (1 -- 1 000 000): ", 1000000);
					(CSharpExpert.getDataFrame("MyHouse").getSlot("space")).slotValue = getUint ("Enter house's living space (1 -- 5000): ", 5000);
					(CSharpExpert.getDataFrame("MyHouse").getSlot("distanceFromSPb")).slotValue = getUint ("How far is this house located from SPb?(1 -- 1000): ", 1000);
					(CSharpExpert.getDataFrame("MyHouse").getSlot("numberOfBedrooms")).slotValue = getUint ("How many bedroms in the house?(1 -- 200): ", 200);
					(CSharpExpert.getDataFrame("MyHouse").getSlot("numberOfFloors")).slotValue = getUint ("How many floors in the house? (1 -- 10): ", 10);
				}
				else
					askData = true;

				((MoneyEnough)CSharpExpert.getRulesetFrame("MoneyEnough")).initContext("MyHouse");
				(CSharpExpert.getDataFrame("Buyer").getSlot("isMoneyEnough")).slotValue = null;
				ProductionSystem.consult ("MoneyEnough");
				Console.WriteLine ("Money amount: {0}", (uint)(CSharpExpert.getDataFrame("Buyer").getSlot("moneyAmount")).slotValue);
				Console.WriteLine ("Price of the house: {0}", (uint)(CSharpExpert.getDataFrame("MyHouse").getSlot("priceOfHouse")).slotValue);
				Console.WriteLine ("Is money enough: {0}", (bool)(CSharpExpert.getDataFrame("Buyer").getSlot("isMoneyEnough")).slotValue);

				Console.WriteLine ("One more time?");
				Console.WriteLine ("1 - yes");
				Console.WriteLine ("2 - exit");
				repeat = (getUint("Enter the value from 1 to 2: ", 2) == 1)? true : false;

			}
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
		dataFrames.Add ("Buyer", new Buyer());
		dataFrames.Add ("House", new House());
		dataFrames.Add ("Human", new Human());
		dataFrames.Add ("MyHouse", new MyHouse());
// *** End of data frames

// *** Rulesets
		rulesetFrames.Add ("MoneyEnough", new MoneyEnough());
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
public class Buyer : InstanceFrame
{
	//Attributes
	
	public Buyer()
	{
		//Initialization isA
		isA.Add("Human");
	}
	public override void resetSlots()
	{
		// Initialization of slots
		CSharpExpert.addSlot("Buyer.isMoneyEnough", new Slot(null));
		CSharpExpert.addSlot("Buyer.moneyAmount", new Slot(0));
		// Default initialization of instance slots
	}
	public override string getName()
	{
		return "Buyer";
	}
}
public class House : ClassFrame
{
	//Attributes
	
	public House()
	{
		//Initialization isA
	}
	public override void resetSlots()
	{
		// Initialization of default values for instance slots
	}
	public override string getName()
	{
		return "House";
	}
}
public class Human : ClassFrame
{
	//Attributes
	
	public Human()
	{
		//Initialization isA
	}
	public override void resetSlots()
	{
		// Initialization of default values for instance slots
	}
	public override string getName()
	{
		return "Human";
	}
}
public class MyHouse : InstanceFrame
{
	//Attributes
	
	public MyHouse()
	{
		//Initialization isA
		isA.Add("House");
	}
	public override void resetSlots()
	{
		// Initialization of slots
		CSharpExpert.addSlot("MyHouse.distanceFromSPb", new Slot(null));
		CSharpExpert.addSlot("MyHouse.numberOfBedrooms", new Slot(null));
		CSharpExpert.addSlot("MyHouse.numberOfFloors", new Slot(null));
		CSharpExpert.addSlot("MyHouse.priceCalculator", new Slot(null));
		CSharpExpert.addSlot("MyHouse.priceOfHouse", new Slot(null));
		CSharpExpert.addSlot("MyHouse.space", new Slot(null));
		// Default initialization of instance slots
	}
	public override string getName()
	{
		return "MyHouse";
	}
}
public class MoneyEnough : RulesetFrame
{
	
	
	// Parameters of the ruleset

	private class R01 : Knowledge.Library.Rule
	{
		public override bool condition()
		{
			return true;
		}
		public override void if_statement()
		{
			CottagePriceCalculator.CalcCottagePriceFunc localFunc = (CottagePriceCalculator.CalcCottagePriceFunc)(CSharpExpert.getRulesetFrame("MoneyEnough").getSlot("priceCalculator")).slotValue;
			(CSharpExpert.getRulesetFrame("MoneyEnough").getSlot("priceOfHouse")).slotValue=localFunc((uint)(CSharpExpert.getRulesetFrame("MoneyEnough").getSlot("space")).slotValue, (uint)(CSharpExpert.getRulesetFrame("MoneyEnough").getSlot("distanceFromSPb")).slotValue, (uint)(CSharpExpert.getRulesetFrame("MoneyEnough").getSlot("numberOfBedrooms")).slotValue, (uint)(CSharpExpert.getRulesetFrame("MoneyEnough").getSlot("numberOfFloors")).slotValue);
			if ((uint)(CSharpExpert.getRulesetFrame("MoneyEnough").getSlot("priceOfHouse")).slotValue <= (uint)(CSharpExpert.getRulesetFrame("MoneyEnough").getSlot("moneyAmount")).slotValue)
				(CSharpExpert.getRulesetFrame("MoneyEnough").getSlot("isMoneyEnough")).slotValue = true;
			else
				(CSharpExpert.getRulesetFrame("MoneyEnough").getSlot("isMoneyEnough")).slotValue = false;
		}
	
		public override string getName()
		{
			return "R01";
		}
	}

	public MoneyEnough() : base()
	{
		goal = "isMoneyEnough";
		
		context.Add("Buyer", new ContextParam("Buyer", false));
		context.Add("House", new ContextParam("House", true));
		rules.Add("R01", new R01());
	}

	public void initContext(string House)
	{
		isParamsInitialized = true;
		base._initContext();
		if (!CSharpExpertAbstract.getDataFrame(House).isInherited("House"))
			throw new KnowledgeException("Instance frame \""+House+"\"is not inherited from Class Frame\"House\"");
		IDictionary slots = CSharpExpertAbstract.getDataFrame(House).getSlots();
		foreach (DictionaryEntry entry in slots)
		{
			if (this.slots.Contains(entry.Key))
				throw new KnowledgeException("Not unique names of slot in the context of the frame \""+getName()+"\"");
			this.slots.Add(entry.Key, entry.Value);
		}
	}

	public override string getName()
	{
		return "MoneyEnough";
	}
}
#endregion
