using System;
using System.Diagnostics;
using System.Text;
using Epic.OnlineServices;

[DebuggerDisplay ("{ToString()}")]
public sealed class Utf8String
{
	public static Utf8String EmptyString = new Utf8String ();

	public int Length { get; private set; }

	public byte[] Bytes { get; private set; }

	private string Utf16 {
		get {
			if (Length > 0) {
				return Encoding.UTF8.GetString (Bytes, 0, Length);
			}
			if (Bytes == null) {
				throw new Exception ("Bytes array is null.");
			}
			if (Bytes.Length == 0 || Bytes [Bytes.Length - 1] != 0) {
				throw new Exception ("Bytes array is not null terminated.");
			}
			return "";
		}
		set {
			if (value != null) {
				Bytes = new byte[Encoding.UTF8.GetMaxByteCount (value.Length) + 1];
				Length = Encoding.UTF8.GetBytes (value, 0, value.Length, Bytes, 0);
			} else {
				Length = 0;
			}
		}
	}

	public byte this [int index] {
		get {
			return Bytes [index];
		}
		set {
			Bytes [index] = value;
		}
	}

	public Utf8String ()
	{
		Length = 0;
	}

	public Utf8String (byte[] bytes)
	{
		if (bytes == null) {
			throw new ArgumentNullException ("bytes");
		}
		if (bytes.Length == 0 || bytes [^1] != 0) {
			throw new ArgumentException ("Argument is not null terminated.", "bytes");
		}
		Bytes = bytes;
		Length = Bytes.Length - 1;
	}

	public Utf8String (string value)
	{
		Utf16 = value;
	}

	public static explicit operator Utf8String (byte[] bytes)
	{
		return new Utf8String (bytes);
	}

	public static explicit operator byte[] (Utf8String u8str)
	{
		return u8str.Bytes;
	}

	public static implicit operator Utf8String (string str)
	{
		return new Utf8String (str);
	}

	public static implicit operator string (Utf8String u8str)
	{
		if (u8str != null) {
			return u8str.ToString ();
		}
		return null;
	}

	public static Utf8String operator + (Utf8String left, Utf8String right)
	{
		byte[] array = new byte[left.Length + right.Length + 1];
		Buffer.BlockCopy (left.Bytes, 0, array, 0, left.Length);
		Buffer.BlockCopy (right.Bytes, 0, array, left.Length, right.Length + 1);
		return new Utf8String (array);
	}

	public static bool operator == (Utf8String left, Utf8String right)
	{
		if ((object)left == null) {
			if ((object)right == null) {
				return true;
			}
			return false;
		}
		return left.Equals (right);
	}

	public static bool operator != (Utf8String left, Utf8String right)
	{
		return !(left == right);
	}

	public override bool Equals (object obj)
	{
		if (!(obj is Utf8String utf8String)) {
			return false;
		}
		if ((object)this == utf8String) {
			return true;
		}
		if (Length != utf8String.Length) {
			return false;
		}
		for (int i = 0; i < Length; i++) {
			if (this [i] != utf8String [i]) {
				return false;
			}
		}
		return true;
	}

	public override string ToString ()
	{
		return Utf16;
	}

	public override int GetHashCode ()
	{
		return ToString ().GetHashCode ();
	}
}
