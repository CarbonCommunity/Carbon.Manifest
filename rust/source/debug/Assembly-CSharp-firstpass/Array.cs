using System.Collections;
using System.Collections.Generic;
using System.Text;
using JSON;

public class Array : IEnumerable<Value>, IEnumerable
{
	private readonly List<Value> values = new List<Value> ();

	public Value this [int index] {
		get {
			return values [index];
		}
		set {
			values [index] = value;
		}
	}

	public int Length => values.Count;

	public Array ()
	{
	}

	public Array (Array array)
	{
		values = new List<Value> ();
		foreach (Value value in array.values) {
			values.Add (new Value (value));
		}
	}

	public void Add (Value value)
	{
		values.Add (value);
	}

	public override string ToString ()
	{
		StringBuilder stringBuilder = new StringBuilder ();
		stringBuilder.Append ('[');
		foreach (Value value in values) {
			stringBuilder.Append (value.ToString ());
			stringBuilder.Append (',');
		}
		if (values.Count > 0) {
			stringBuilder.Remove (stringBuilder.Length - 1, 1);
		}
		stringBuilder.Append (']');
		return stringBuilder.ToString ();
	}

	public IEnumerator<Value> GetEnumerator ()
	{
		return values.GetEnumerator ();
	}

	IEnumerator IEnumerable.GetEnumerator ()
	{
		return values.GetEnumerator ();
	}

	public static Array Parse (string jsonString)
	{
		return Object.Parse ("{ \"array\" :" + jsonString + "}")?.GetValue ("array").Array;
	}

	public void Clear ()
	{
		values.Clear ();
	}

	public void Remove (int index)
	{
		if (index >= 0 && index < values.Count) {
			values.RemoveAt (index);
		}
	}

	public static Array operator + (Array lhs, Array rhs)
	{
		Array array = new Array (lhs);
		foreach (Value value in rhs.values) {
			array.Add (value);
		}
		return array;
	}
}
