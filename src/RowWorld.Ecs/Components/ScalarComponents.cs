namespace RowWorld.Ecs.Components
{
	public class DoubleComponent : ValueComponent<double>
	{
		public DoubleComponent(double value) : base(value)
		{
		}
	}

	public class FloatComponent : ValueComponent<float>
	{
		public FloatComponent(float value) : base(value)
		{
		}
	}

	public class DecimalComponent : ValueComponent<decimal>
	{
		public DecimalComponent(decimal value) : base(value)
		{
		}
	}

	public class LongComponent : ValueComponent<long>
	{
		public LongComponent(long value) : base(value)
		{
		}
	}

	public class StringComponent : ValueComponent<string>
	{
		public StringComponent(string value) : base(value)
		{
		}
	}
}
