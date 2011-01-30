using System;

namespace Knowledge.Prospector.Data.Entities
{

	/// <summary>
	/// Date-Time entity.
	/// </summary>
	[Serializable]
	public class DateTimeEntity : TrueEntity, IDatatypeEntity
	{
		//[Flags]
		//public enum PartsOfDay
		//{
		//    Morning = 1 << 0,
		//    Day = 1 << 1,
		//    Evening = 1 << 2,
		//    Night = 1 << 3,
		//}

		//[Flags]
		//public enum Seasons
		//{
		//    winter = 1 << 0,
		//    spring = 1 << 1,
		//    summer = 1 << 2,
		//    autumn = 1 << 3,
		//}

		//public PartsOfDay PartOfDay;
		//public Seasons Season;
		//public DayOfWeek DayOfWeekType;

		#region Properties

		private Nullable<int> _year;
		public Nullable<int> Year
		{
			get
			{
				return _year;
			}
			set
			{
				_year = value;
			}
		}

		private Nullable<int> _month;
		public Nullable<int> Month
		{
			get
			{
				return _month;
			}
			set
			{
				_month = value;
			}
		}

		private Nullable<int> _day;
		public Nullable<int> Day
		{
			get
			{
				return _day;
			}
			set
			{
				_day = value;
			}
		}

		private Nullable<int> _hour;
		public Nullable<int> Hour
		{
			get
			{
				return _hour;
			}
			set
			{
				_hour = value;
			}
		}

		private Nullable<int> _minute;
		public Nullable<int> Minute
		{
			get
			{
				return _minute;
			}
			set
			{
				_minute = value;
			}
		}

		private Nullable<int> _second;
		public Nullable<int> Second
		{
			get
			{
				return _second;
			}
			set
			{
				_second = value;
			}
		}

		#endregion

		#region Overrides

		public override string Name
		{
			get { return "DateTimeEntity"; }
		}

		public override string Value
		{
			get
			{
				string str = string.Empty;
				if (Year != null)
					str += (str != string.Empty ? ", " : string.Empty) + "Year: " + Year.ToString();
				if (Month != null)
					str += (str != string.Empty ? ", " : string.Empty) + "Month: " + Month.ToString();
				if (Day != null)
					str += (str != string.Empty ? ", " : string.Empty) + "Day: " + Day.ToString();
				if (Hour != null)
					str += (str != string.Empty ? ", " : string.Empty) + "Hour: " + Hour.ToString();
				if (Minute != null)
					str += (str != string.Empty ? ", " : string.Empty) + "Minute: " + Minute.ToString();
				if (Second != null)
					str += (str != string.Empty ? ", " : string.Empty) + "Second: " + Second.ToString();
				return str;
			}
		}

		public override void GenerateIdentity()
		{
			string str = string.Empty;
			if (Year != null)
				str += Year.ToString();
			str += ":";
			if (Month != null)
				str += Month.ToString();
			str += ":";
			if (Day != null)
				str += Day.ToString();
			str += ":";
			if (Hour != null)
				str += Hour.ToString();
			str += ":";
			if (Minute != null)
				str += Minute.ToString();
			str += ":";
			if (Second != null)
				str += Second.ToString();
			this.Identity = str;
		}

		public override string ToString()
		{
			return string.Format("<DateTime>{0}</DateTime>", this.Value);
		}

		#endregion

		private int GetInt(object o)
		{
			if(o is int)
				return (int)o;
			if(o is string)
				return int.Parse(o as string);
			return -1;
		}

		public void Set(string type, object value)
		{
			switch (type.Trim().ToUpper())
			{
				case "Y":
				case "YEAR":
					Year = GetInt(value);
					break;
				case "M":
				case "MONTH":
					Month = GetInt(value);
					break;
				case "D":
				case "DAY":
					Day = GetInt(value);
					break;
				case "HOUR":
				case "H":
					Hour = GetInt(value);
					break;
				case "MINUTE":
				case "MIN":
					Minute = GetInt(value);
					break;
				case "SECOND":
				case "S":
					Second = GetInt(value);
					break;
				//case "SEASON":
				//    Season = (Seasons)GetInt(value);
				//    break;
				//case "DOW":
				//    DayOfWeekType = (DayOfWeek)GetInt(value);
				//    break;
				//case "POD":
				//    PartOfDay = (PartsOfDay)GetInt(value);
				//    break;
				default:
					throw new ArgumentException("Unknown type: " + type, "type");
			}
			GenerateIdentity();
		}


		public DateTimeEntity()
			: base(string.Empty)
		{
		}
	}
}
