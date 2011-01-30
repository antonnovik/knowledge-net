using System;

namespace Knowledge.Library
{
	public abstract class FuzzyType
	{
		protected object innerValue;
		protected int innerCf=100;
		public int Cf 
		{
			set
			{
				if (value>=0 && value <=100)
					innerCf=value;
				else
					throw new Exception ("Cf value must be in the range from 0 to 100");
			}
			get
			{
				return innerCf;
			}
		}


		public override bool Equals (object o)
		{
			if (o is FuzzyType)
			{
				FuzzyType obj = (FuzzyType)o;
				return (innerValue == obj.innerValue) && (Cf == obj.Cf);
			}
			else
				return base.Equals(o);
		}


		//overloaded operators
		public static FuzzyBool operator ==(FuzzyType x, FuzzyType y)
		{
			return new FuzzyBool (x.innerValue == y.innerValue, Math.Min(x.Cf, y.Cf));
		}

		public static FuzzyBool operator !=(FuzzyType x, FuzzyType y)
		{
			return new FuzzyBool (x.innerValue != y.innerValue, Math.Min(x.Cf, y.Cf));
		}

		public override string ToString()
		{
			return innerValue.ToString ();
		}

	}

	public class FuzzyInt : FuzzyType
	{
		public int Value
		{
			set
			{
				innerValue = value;
			}
			get
			{
				return (int)innerValue;
			}
		}
		
		public FuzzyInt (int Value, int Cf)
		{
			this.Value = Value;
			this.Cf = Cf;
		}

		public FuzzyInt (int Value)
		{
			this.Value = Value;
		}


		public static implicit operator FuzzyInt (int x)
		{
			return new FuzzyInt(x);
		}

		public static implicit operator int(FuzzyInt x)
		{
			return x.Value;
		}

		public static implicit operator FuzzyDouble(FuzzyInt x)
		{
			return new FuzzyDouble (x.Value, x.Cf);
		}

		public static implicit operator FuzzyString(FuzzyInt x)
		{
			return new FuzzyString (x.Value.ToString(), x.Cf);
		}


		public static FuzzyInt operator &(FuzzyInt x, FuzzyInt y)
		{
			return new FuzzyInt(x.Value & y.Value, Math.Min (x.Cf, y.Cf));
		}

		public static FuzzyInt operator |(FuzzyInt x, FuzzyInt y)
		{
			return new FuzzyInt(x.Value | y.Value, Math.Max (x.Cf, y.Cf));
		}

		public static FuzzyInt operator ^(FuzzyInt x, FuzzyInt y)
		{
			return new FuzzyInt(x.Value^y.Value,
				Math.Min(Math.Max(x.Cf, y.Cf), Math.Max(100-x.Cf, 100-y.Cf)));
		}


		public static FuzzyInt operator +(FuzzyInt x, FuzzyInt y)
		{
			return new FuzzyInt(x.Value+y.Value, Math.Min(x.Cf, y.Cf));
		}

		public static FuzzyInt operator -(FuzzyInt x, FuzzyInt y)
		{
			return new FuzzyInt(x.Value-y.Value, Math.Min(x.Cf, y.Cf));
		}

		public static FuzzyInt operator *(FuzzyInt x, FuzzyInt y)
		{
			return new FuzzyInt(x.Value*y.Value, Math.Min(x.Cf, y.Cf));
		}

		public static FuzzyInt operator /(FuzzyInt x, FuzzyInt y)
		{
			return new FuzzyInt(x.Value/y.Value, Math.Min(x.Cf, y.Cf));
		}

		public static FuzzyInt operator %(FuzzyInt x, FuzzyInt y)
		{
			return new FuzzyInt(x.Value%y.Value, Math.Min(x.Cf, y.Cf));
		}


		public static FuzzyBool operator <(FuzzyInt x, FuzzyInt y)
		{
			return new FuzzyBool(x.Value<y.Value, Math.Min(x.Cf, y.Cf));
		}
		public static FuzzyBool operator >(FuzzyInt x, FuzzyInt y)
		{
			return new FuzzyBool(x.Value<y.Value, Math.Min(x.Cf, y.Cf));
		}
		public static FuzzyBool operator <=(FuzzyInt x, FuzzyInt y)
		{
			return new FuzzyBool(x.Value<=y.Value, Math.Min(x.Cf, y.Cf));
		}
		public static FuzzyBool operator >=(FuzzyInt x, FuzzyInt y)
		{
			return new FuzzyBool(x.Value>=y.Value, Math.Min(x.Cf, y.Cf));
		}
	}
	public class FuzzyDouble : FuzzyType
	{
		public double Value
		{
			set
			{
				innerValue=value;
			}
			get
			{
				return (double)innerValue;
			}
		}

		public FuzzyDouble (double Value, int Cf)
		{
			this.Value = Value;
			this.Cf = Cf;
		}

		public FuzzyDouble (double Value)
		{
			this.Value = Value;
		}


		public static implicit operator FuzzyDouble (double x)
		{
			return new FuzzyDouble(x);
		}

		public static implicit operator double(FuzzyDouble x)
		{
			return x.Value;
		}

		public static implicit operator string(FuzzyDouble x)
		{
			return new FuzzyString (x.Value.ToString(), x.Cf);
		}


		public static FuzzyDouble operator +(FuzzyDouble x, FuzzyDouble y)
		{
			return new FuzzyDouble(x.Value+y.Value, Math.Min(x.Cf, y.Cf));
		}

		public static FuzzyDouble operator -(FuzzyDouble x, FuzzyDouble y)
		{
			return new FuzzyDouble(x.Value-y.Value, Math.Min(x.Cf, y.Cf));
		}

		public static FuzzyDouble operator *(FuzzyDouble x, FuzzyDouble y)
		{
			return new FuzzyDouble(x.Value*y.Value, Math.Min(x.Cf, y.Cf));
		}

		public static FuzzyDouble operator /(FuzzyDouble x, FuzzyDouble y)
		{
			return new FuzzyDouble(x.Value/y.Value, Math.Min(x.Cf, y.Cf));
		}

		public static FuzzyDouble operator %(FuzzyDouble x, FuzzyDouble y)
		{
			return new FuzzyDouble(x.Value%y.Value, Math.Min(x.Cf, y.Cf));
		}

		public static FuzzyBool operator <(FuzzyDouble x, FuzzyDouble y)
		{
			return new FuzzyBool(x.Value<y.Value, Math.Min(x.Cf, y.Cf));
		}
		public static FuzzyBool operator >(FuzzyDouble x, FuzzyDouble y)
		{
			return new FuzzyBool(x.Value<y.Value, Math.Min(x.Cf, y.Cf));
		}
		public static FuzzyBool operator <=(FuzzyDouble x, FuzzyDouble y)
		{
			return new FuzzyBool(x.Value<=y.Value, Math.Min(x.Cf, y.Cf));
		}
		public static FuzzyBool operator >=(FuzzyDouble x, FuzzyDouble y)
		{
			return new FuzzyBool(x.Value>=y.Value, Math.Min(x.Cf, y.Cf));
		}
	}

	public class FuzzyBool : FuzzyType
	{
		public static FuzzyBool FuzzyTrue = new FuzzyBool(true);
		public static FuzzyBool FuzzyFalse = new FuzzyBool(false);

		public bool Value
		{
			set 
			{
				innerValue=value;
			}
			get
			{
				return (bool)innerValue;
			}
		}

		public FuzzyBool (bool x)
		{
			Value=x;
		}

		public FuzzyBool (bool x, int cf)
		{
			Value=x;
			Cf=cf;
		}


		// overloaded operators
		public static implicit operator FuzzyBool(bool x)
		{
			return (x)? FuzzyTrue : FuzzyFalse;
		}

		public static implicit operator bool(FuzzyBool x)
		{
			return (x.Value)? true : false;
		}


		public static FuzzyBool operator &(FuzzyBool x, FuzzyBool y)
		{
			return new FuzzyBool(x.Value & y.Value, Math.Min (x.Cf, y.Cf));
		}

		public static FuzzyBool operator |(FuzzyBool x, FuzzyBool y)
		{
			return new FuzzyBool(x.Value | y.Value, Math.Max (x.Cf, y.Cf));
		}

		public static FuzzyBool operator ^(FuzzyBool x, FuzzyBool y)
		{
			return new FuzzyBool(x.Value ^ y.Value, 
				Math.Min(Math.Max(x.Cf, y.Cf), Math.Max(100-x.Cf, 100-y.Cf)));
		}



		public static FuzzyBool operator !(FuzzyBool x)
		{
			return new FuzzyBool(!x.Value, 100-x.Cf);
		}

		public static bool operator true(FuzzyBool x)
		{
			return x.Value == true;
		}

		public static bool operator false(FuzzyBool x)
		{
			return x.Value == false;
		}


		public override string ToString()
		{
			return Value.ToString();
		}

	}

	public class FuzzyString : FuzzyType
	{
		public string Value
		{
			set
			{
				innerValue = value;
			}
			get
			{
				return (string)innerValue;
			}
		}

		public FuzzyString (string Value)
		{
			this.Value = Value;
		}

		public FuzzyString (string Value, int Cf)
		{
			this.Value = Value;
			this.Cf = Cf;
		}

		// overloaded operators
		public static implicit operator FuzzyString(string x)
		{
			return new FuzzyString (x);
		}

		public static implicit operator string(FuzzyString x)
		{
			return x.Value;
		}

	}

}
