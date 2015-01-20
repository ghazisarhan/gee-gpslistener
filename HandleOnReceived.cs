// TODO: Implement a "tag" in the NetSockets to add a flag to a client
// TODO: didn't like the ReverseBytesIfLittleEndian() idea, think again
using System;
using NetSockets;

namespace geegpslistener
{
	partial class MainClass
	{
		static void HandleOnReceived (object sender, NetClientReceivedEventArgs<byte[]> e)
		{
			// verify data length
			if (e.Data.Length < 17) // TODO: 17 according to 007, needs rewritten for other models
			{
				log.Error ("Packet recieved with data length less than 17 bytes.");
				return;
			}

			// Parsing data
			//----------------
			byte[] data = new byte[e.Data.Length];
			Buffer.BlockCopy (e.Data, 0, data, 0, e.Data.Length);
			// TODO: clean up code
			//----------------
			ushort dataHeader = BitConverter.ToUInt16 (data, 0).ReverseBytesIfLittleEndian ();
			ushort dataCRC = BitConverter.ToUInt16 (data, data.Length - 4).ReverseBytesIfLittleEndian ();		// TODO: may raise an error
			ushort dataFooter = BitConverter.ToUInt16 (data, data.Length - 2).ReverseBytesIfLittleEndian (); 	// TODO: may raise an error
							
			if ((dataHeader == HEADER_DEVICE_TO_SERVER) && (dataFooter == FOOTER) && (dataCRC == CalculateCRC (e.Data.SubArray (0, e.Data.Length - 4))))
			{
				// Device 007 and data are valid
				log.Debug ("Valid 007 packet recieved."); // TODO: add more info for debug purpose
				Process007Data (data, e);
			}
			else
			{
				log.Error ("Invalid Packet: " + BitConverter.ToString (e.Data));
			}


		}
	}
}

