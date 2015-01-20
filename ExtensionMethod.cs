// TODO: didn't like the ReverseBytesIfLittleEndian() idea, think again
using System;
using System.IO;

namespace geegpslistener
{
	static class ExtensionMethods
	{
		// TODO: Removed public didn't work, added internal and it worked, review
		internal static T[] SubArray<T>(this T[] data, int index, int length)
		{
			T[] result = new T[length];
			Array.Copy(data, index, result, 0, length);
			return result;
		}

		// reverse byte order (16-bit)
		internal static UInt16 ReverseBytes(this UInt16 value)
		{
			return (UInt16)((value & 0xFFU) << 8 | (value & 0xFF00U) >> 8);
		}

		// reverse byte order if little endian(16-bit)
		internal static UInt16 ReverseBytesIfLittleEndian(this UInt16 value)
		{
			if (BitConverter.IsLittleEndian)
			{
				return (UInt16)((value & 0xFFU) << 8 | (value & 0xFF00U) >> 8);
			}
			else
			{
				return value;
			}
		}

		internal static void Append(this MemoryStream stream, byte value)
		{
			stream.Append(new[] { value });
		}

		internal static void Append(this MemoryStream stream, byte[] values)
		{
			stream.Write(values, 0, values.Length);
		}
	}
}

