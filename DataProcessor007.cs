// TODO: didn't like the ReverseBytesIfLittleEndian() idea, think again
using System;
using System.IO;
using NetSockets;
using System.Text;

namespace geegpslistener
{
	partial class MainClass
	{
		static void Process007Data(byte[] data, NetClientReceivedEventArgs<byte[]> e)
		{
			// Parsing data
			// TODO: verify endianness
			ushort dataHeader = BitConverter.ToUInt16 (data, 0).ReverseBytesIfLittleEndian (); 
			ushort dataLength = BitConverter.ToUInt16 (data, 2).ReverseBytesIfLittleEndian (); 
			byte[] dataID = data.SubArray (4, 7);
			ushort dataCommand = BitConverter.ToUInt16 (data, 11).ReverseBytesIfLittleEndian ();
			byte[] dataData = data.SubArray (13, data.Length - 13 - 4);
			ushort dataCRC = BitConverter.ToUInt16 (data, data.Length - 4).ReverseBytesIfLittleEndian ();
			ushort dataFooter = BitConverter.ToUInt16 (data, data.Length - 2).ReverseBytesIfLittleEndian ();
			// 00 01 02 03 04 05 06 07 08 09 10 11 12 13 14 15 16 17 18 19   
			// 01 02 03 04 05 06 07 08 09 10 11 12 13 14 15 16 17 18 19 20
			// Head- Len-- ID------------------ COMM- Data---- CRC-- FOT--


			switch (dataCommand)
			{
				// Undocumented command!
				case OTHER_COMMAND_GET_IP_AND_PORT:
					// TODO: for testing only
					Build007Data (dataID, OTHER_REPLY_GET_IP_AND_PORT, Encoding.ASCII.GetBytes ("178.62.117.203:11000"));
					// TODO: Pass client stream instance to reply to client.

					break;
				
				// Never recieved it!
				case COMMAND_TRACKER_LOGIN:

					break;
				
				case COMMAND_SINGLE_LOCATION_REPORT:

					break;

				case COMMAND_ALARM:

					break;

				// Reply to commands
				// TODO: make it statueful?
				case REPLY_SET_TIME_INTERVAL_FOR_CONTINOUS_TRACKING:
					break;

				case REPLY_SET_AUTHORIZED_PHONE_NUMBER:
					break;

				case REPLY_SET_SPEED_LIMIT_FOR_OVER_SPEED_ALARM:
					break;

				case REPLY_SET_MOVEMENT_ALERT:
					break;

				case REPLY_SET_EXTENDED_FUNCTIONS:
					break;

				case REPLY_INITIALIZE_ALL_PARAMETERS:
					break;

				case REPLY_SET_GPRS_ALERT_FOR_BUTTON_OR_INPUT:
					break;

				case REPLY_SET_TELEPHONE_NUMBER_FOR_WIRETAPPING:
					break;

				case REPLY_SET_TIMEZONE:
					break;

				case REPLY_READ_TIME_INTERVAL_FOR_CONTINUOUS_TRACKING:
					break;

				case REPLY_READ_AUTHORIZED_PHONE_NUMBER:
					break;

				case REPLY_ALARM:
					break;

				case REPLY_BLACK_BOX_REPORT:
					break;

				case REPLY_SEND_SMS:
					break;

				case REPLY_GET_PLAIN_ADDRESSS:
					break;

				case REPLY_GET_GPRMC_FROM_GSM_CELL:
					break;

				default:
					log.Error ("Unknown Command recieved.");
					// TODO: ignore command!
					break;
			}


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
			// TODO: Verify endianness
			ushort length = Convert.ToUInt16 (data.Length + 17); // TODO: 17 is for 007
			MemoryStream ms = new MemoryStream ();
			ms.Append (BitConverter.GetBytes (HEADER_SERVER_TO_DEVICE));
			ms.Append (BitConverter.GetBytes (length));
			ms.Append (ID); // TODO: Fill empty bytes of ID here or in the calling procedure
			ms.Append (BitConverter.GetBytes (command));
			ms.Append (data);
			ms.Append (BitConverter.GetBytes (CalculateCRC (ms.ToArray ())));
			ms.Append (BitConverter.GetBytes (FOOTER));
			return ms.ToArray ();
		}
	}
}
