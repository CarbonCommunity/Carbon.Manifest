using System;

internal class AllocationException : Exception
{
	public AllocationException (string message)
		: base (message)
	{
	}
}
