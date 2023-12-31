using JSON;

public class Value
{
	public ValueType Type { get; private set; }

	public string Str { get; set; }

	public double Number { get; set; }

	public Object Obj { get; set; }

	public Array Array { get; set; }

	public bool Boolean { get; set; }

	public Value Parent { get; set; }

	public Value (ValueType type)
	{
		Type = type;
	}

	public Value (string str)
	{
		Type = ValueType.String;
		Str = str;
	}

	public Value (double number)
	{
		Type = ValueType.Number;
		Number = number;
	}

	public Value (Object obj)
	{
		if (obj == null) {
			Type = ValueType.Null;
			return;
		}
		Type = ValueType.Object;
		Obj = obj;
	}

	public Value (Array array)
	{
		Type = ValueType.Array;
		Array = array;
	}

	public Value (bool boolean)
	{
		Type = ValueType.Boolean;
		Boolean = boolean;
	}

	public Value (Value value)
	{
		Type = value.Type;
		switch (Type) {
		case ValueType.String:
			Str = value.Str;
			break;
		case ValueType.Boolean:
			Boolean = value.Boolean;
			break;
		case ValueType.Number:
			Number = value.Number;
			break;
		case ValueType.Object:
			if (value.Obj != null) {
				Obj = new Object (value.Obj);
			}
			break;
		case ValueType.Array:
			Array = new Array (value.Array);
			break;
		}
	}

	public static implicit operator Value (string str)
	{
		return new Value (str);
	}

	public static implicit operator Value (double number)
	{
		return new Value (number);
	}

	public static implicit operator Value (Object obj)
	{
		return new Value (obj);
	}

	public static implicit operator Value (Array array)
	{
		return new Value (array);
	}

	public static implicit operator Value (bool boolean)
	{
		return new Value (boolean);
	}

	public override string ToString ()
	{
		return Type switch {
			ValueType.Object => Obj.ToString (), 
			ValueType.Array => Array.ToString (), 
			ValueType.Boolean => Boolean ? "true" : "false", 
			ValueType.Number => Number.ToString (), 
			ValueType.String => "\"" + Str + "\"", 
			ValueType.Null => "null", 
			_ => "null", 
		};
	}
}
