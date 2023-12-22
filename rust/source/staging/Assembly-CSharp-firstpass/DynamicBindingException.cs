using System;

internal class DynamicBindingException : Exception
{
	public DynamicBindingException (string bindingName)
		: base ($"Failed to hook dynamic binding for '{bindingName}'")
	{
	}
}
