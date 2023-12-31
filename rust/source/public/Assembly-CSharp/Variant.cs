using System;
using System.Globalization;
using TinyJSON;

public abstract class Variant : IConvertible
{
	protected static readonly IFormatProvider FormatProvider = new NumberFormatInfo ();

	public virtual Variant this [string key] {
		get {
			throw new NotSupportedException ();
		}
		set {
			throw new NotSupportedException ();
		}
	}

	public virtual Variant this [int index] {
		get {
			throw new NotSupportedException ();
		}
		set {
			throw new NotSupportedException ();
		}
	}

	public void Make<T> (out T item)
	{
		TinyJSON.JSON.MakeInto<T> (this, out item);
	}

	public T Make<T> ()
	{
		TinyJSON.JSON.MakeInto<T> (this, out var item);
		return item;
	}

	public string ToJSON ()
	{
		return TinyJSON.JSON.Dump (this);
	}

	public virtual TypeCode GetTypeCode ()
	{
		return TypeCode.Object;
	}

	public virtual object ToType (Type conversionType, IFormatProvider provider)
	{
		throw new InvalidCastException ("Cannot convert " + GetType ()?.ToString () + " to " + conversionType.Name);
	}

	public virtual DateTime ToDateTime (IFormatProvider provider)
	{
		throw new InvalidCastException ("Cannot convert " + GetType ()?.ToString () + " to DateTime");
	}

	public virtual bool ToBoolean (IFormatProvider provider)
	{
		throw new InvalidCastException ("Cannot convert " + GetType ()?.ToString () + " to Boolean");
	}

	public virtual byte ToByte (IFormatProvider provider)
	{
		throw new InvalidCastException ("Cannot convert " + GetType ()?.ToString () + " to Byte");
	}

	public virtual char ToChar (IFormatProvider provider)
	{
		throw new InvalidCastException ("Cannot convert " + GetType ()?.ToString () + " to Char");
	}

	public virtual decimal ToDecimal (IFormatProvider provider)
	{
		throw new InvalidCastException ("Cannot convert " + GetType ()?.ToString () + " to Decimal");
	}

	public virtual double ToDouble (IFormatProvider provider)
	{
		throw new InvalidCastException ("Cannot convert " + GetType ()?.ToString () + " to Double");
	}

	public virtual short ToInt16 (IFormatProvider provider)
	{
		throw new InvalidCastException ("Cannot convert " + GetType ()?.ToString () + " to Int16");
	}

	public virtual int ToInt32 (IFormatProvider provider)
	{
		throw new InvalidCastException ("Cannot convert " + GetType ()?.ToString () + " to Int32");
	}

	public virtual long ToInt64 (IFormatProvider provider)
	{
		throw new InvalidCastException ("Cannot convert " + GetType ()?.ToString () + " to Int64");
	}

	public virtual sbyte ToSByte (IFormatProvider provider)
	{
		throw new InvalidCastException ("Cannot convert " + GetType ()?.ToString () + " to SByte");
	}

	public virtual float ToSingle (IFormatProvider provider)
	{
		throw new InvalidCastException ("Cannot convert " + GetType ()?.ToString () + " to Single");
	}

	public virtual string ToString (IFormatProvider provider)
	{
		throw new InvalidCastException ("Cannot convert " + GetType ()?.ToString () + " to String");
	}

	public virtual ushort ToUInt16 (IFormatProvider provider)
	{
		throw new InvalidCastException ("Cannot convert " + GetType ()?.ToString () + " to UInt16");
	}

	public virtual uint ToUInt32 (IFormatProvider provider)
	{
		throw new InvalidCastException ("Cannot convert " + GetType ()?.ToString () + " to UInt32");
	}

	public virtual ulong ToUInt64 (IFormatProvider provider)
	{
		throw new InvalidCastException ("Cannot convert " + GetType ()?.ToString () + " to UInt64");
	}

	public override string ToString ()
	{
		return ToString (FormatProvider);
	}

	public static implicit operator bool (Variant variant)
	{
		return variant.ToBoolean (FormatProvider);
	}

	public static implicit operator float (Variant variant)
	{
		return variant.ToSingle (FormatProvider);
	}

	public static implicit operator double (Variant variant)
	{
		return variant.ToDouble (FormatProvider);
	}

	public static implicit operator ushort (Variant variant)
	{
		return variant.ToUInt16 (FormatProvider);
	}

	public static implicit operator short (Variant variant)
	{
		return variant.ToInt16 (FormatProvider);
	}

	public static implicit operator uint (Variant variant)
	{
		return variant.ToUInt32 (FormatProvider);
	}

	public static implicit operator int (Variant variant)
	{
		return variant.ToInt32 (FormatProvider);
	}

	public static implicit operator ulong (Variant variant)
	{
		return variant.ToUInt64 (FormatProvider);
	}

	public static implicit operator long (Variant variant)
	{
		return variant.ToInt64 (FormatProvider);
	}

	public static implicit operator decimal (Variant variant)
	{
		return variant.ToDecimal (FormatProvider);
	}

	public static implicit operator string (Variant variant)
	{
		return variant.ToString (FormatProvider);
	}

	public static implicit operator Guid (Variant variant)
	{
		return new Guid (variant.ToString (FormatProvider));
	}
}
