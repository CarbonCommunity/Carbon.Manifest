using System;

[AttributeUsage (AttributeTargets.Field, AllowMultiple = false)]
public sealed class MaxAttribute : Attribute
{
	public readonly float max;

	public MaxAttribute (float max)
	{
		this.max = max;
	}
}
