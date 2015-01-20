// TODO: didn't like the ReverseBytesIfLittleEndian() idea, think again
using System;
using System.IO;

namespace geegpslistener
{
	partial class MainClass
	{
		static void Process007Data(byte[] data)
		{
			// Parsing data
			// TODO: verify endianness
			ushort dataHeader = BitConverter.ToUInt16 (data, 0).ReverseBytesIfLittleEndian (); 
			ushort dataLength = BitConverter.ToUInt16 (data, 2).ReverseBytesIfLittleEndian (); 
			byte[] dataID = data.SubArray (4, 7);
			ushort dataCommand = BitConverter.ToUInt16 (data, 11).ReverseBytesIfLittleEndian ();
			byte[] dataData = data.SubArray (13, data.Length - 13 - 4);											// TODO: verify if it will return correct results :: Seems to work
			ushort dataCRC = BitConverter.ToUInt16 (data, data.Length - 4).ReverseBytesIfLittleEndian ();		// TODO: may raise an error :: Seems to work
			ushort dataFooter = BitConverter.ToUInt16 (data, data.Length - 2).ReverseBytesIfLittleEndian ();	// TODO: may raise an error :: Seems to work
			// 00 01 02 03 04 05 06 07 08 09 10 11 12 13 14 15 16 17 18 19   
			// 01 02 03 04 05 06 07 08 09 10 11 12 13 14 15 16 17 18 19 20
			// Head- Len-- ID------------------ COMM- Data---- CRC-- FOT--



			// TODO: For testing purpose only
			log.Debug ("HEADER : " + dataHeader.ToString ("X4"));
			log.Debug ("LENGTH : " + "From Packet: " + dataLength.ToString ("X4") + " :: Calculated: " + data.Length.ToString ("X4"));
			log.Debug ("ID     : " + BitConverter.ToString (dataID));
			log.Debug ("COMMAND: " + dataCommand.ToString ("X4"));
			log.Debug ("DATA   : " + BitConverter.ToString (dataData));
			log.Debug ("CRC    : " + dataCRC.ToString ("X4"));
			log.Debug ("FOOTER : " + dataFooter.ToString ("X4"));
			log.Debug ("");


		}

		static byte[] Build007Data(byte[] ID, ushort command, byte[] data)
		{
			// TODO: Verify
			ushort length = Convert.ToUInt16 (data.Length) + (ushort)17; // TODO: 17 is for 007
			MemoryStream ms = new MemoryStream ();
			ms.Append (BitConverter.GetBytes (HEADER_SERVER_TO_DEVICE));
			ms.Append (BitConverter.GetBytes (length));
			ms.Append (ID); // TODO: Fill empty bytes of ID here or in the calling procedure
			ms.Append (BitConverter.GetBytes (command));
			ms.Append (data);
			ms.Append (CalculateCRC (ms.ToArray ()));
			ms.Append (FOOTER);
			return ms.ToArray ();
		}
	}
}
