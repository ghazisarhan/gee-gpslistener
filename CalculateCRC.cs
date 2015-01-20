using System;

namespace geegpslistener
{
	partial class MainClass // Calculate CRC
	{

		static ushort CalculateCRC( params byte[] bytes ) 
		{
			ushort[] table = new ushort[256];

			// Building Table
			ushort polynomial = 0x8408;
			ushort value;
			ushort temp;

			for(ushort i = 0; i < table.Length; ++i) 
			{
				value = 0;
				temp = i;
				for(byte j = 0; j < 8; ++j) 
				{
					if(((value ^ temp) & 0x0001) != 0) 
					{
						value = (ushort)((value >> 1) ^ polynomial);
					}
					else
					{
						value >>= 1;
					}
					temp >>= 1;
				}
				table[i] = value;
			}


			// Calculating CRC
			ushort crc = 0;
			for(int i = 0; i < bytes.Length; ++i) 
			{
				byte index = (byte)(crc ^ bytes[i]);
				crc = (ushort)((crc >> 8) ^ table[index]);
			}

			// TODO: Working, but needs review, made it so quick
			// To solve little endian thing
			// Again, review, made it really quick
			// Point: is byte reversal related to endianess?
			if (!BitConverter.IsLittleEndian) {
				byte[] me = BitConverter.GetBytes (crc);
				Array.Reverse (me);
				return BitConverter.ToUInt16 (me, 0);

			} else
				return crc;
			
		}			
	}
}

