using System;

[AttributeUsage (AttributeTargets.Field, AllowMultiple = false)]
public sealed class MinAttribute : Attribute
{
	public readonly float min;

	public MinAttribute (float min)
	{
		this.min = min;
	}
}
