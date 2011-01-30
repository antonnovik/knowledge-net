using System;

namespace Knowledge.Prospector.Data.Entities
{
	/// <summary>
	/// Entity what represent Integer number.
	/// </summary>
	[Serializable]
	public class IntegerEntity : TrueEntity, IDatatypeEntity
	{
		private int _int;

		public IntegerEntity(int value)
			: base(value.ToString())
		{
			_int = value;
		}

		public new int Value
		{
			get
			{
				return _int;
			}
			private set
			{
				_int = value;
			}
		}

		public override string ToString()
		{
			return string.Format("<Integer>{0}</Integer>", Value);
		}

		public override string Name
		{
			get { return "Integer"; }
		}
	}
}
