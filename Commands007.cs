﻿// TODO: Choose better const names in next release
// TODO: Device commands and replies are missed up! review all
using System;
namespace geegpslistener
{
	partial class MainClass // Const
	{
		const ushort HEADER_SERVER_TO_DEVICE = 0x4040;
		const ushort HEADER_DEVICE_TO_SERVER = 0x2424;
		const ushort FOOTER = 0x0D0A;
		const ushort COMMAND_SERVER_CONFIRMS_TRACKER_LOGIN = 0x4000;
		const ushort COMMAND_TRACKER_LOGIN = 0x5000;
		const ushort COMMAND_REQUEST_ONE_SINGLE_LOCATION = 0x4101;
		const ushort COMMAND_SET_TIME_INTERVAL_FOR_CONTINUOUS_TRACKING = 0x4102;
		const ushort COMMAND_SET_AUTHORIZED_PHONE_NUMBER = 0x4103;
		const ushort COMMAND_SET_SPEED_LIMIT_FOR_OVER_SPEED_ALARM = 0x4105;
		const ushort COMMAND_SET_MOVEMENT_ALERT = 0x4106;
		const ushort COMMAND_SET_EXTENDED_FUNCTIONS = 0x4108;
		const ushort COMMAND_INITIALIZE_ALL_PARAMETERS = 0x4110; 		// Except Password
		const ushort COMMAND_SET_GPRS_ALERT_FOR_BUTTON_OR_INPUT = 0x4116;
		const ushort COMMAND_SET_TELEPHONE_NUMBER_FOR_WIRETAPPING = 0x4130;
		const ushort COMMAND_SET_TIMEZONE = 0x4132;
		const ushort COMMAND_READ_TIME_INTERVAL_FOR_CONTINUOUS_TRACKING = 0x9002;
		const ushort COMMAND_READ_AUTHORIZED_PHONE_NUMBER = 0x9003;
		const ushort COMMAND_SINGLE_LOCATION_REPORT = 0x9955;
		const ushort COMMAND_BLACK_BOX_REPORT = 0x9956;
		const ushort COMMAND_SEND_SMS = 0x9957;
		const ushort COMMAND_GET_PLAIN_ADDRESSS = 0x9958;
		const ushort COMMAND_GET_GPRMC_FROM_GSM_CELL = 0x9959;
		const ushort COMMAND_ALARM = 0x9999;

		const ushort OTHER_COMMAND_HEART_BEAT = 0x0001;
		const ushort OTHER_COMMAND_GET_IP_AND_PORT = 0x0002;

		const ushort REPLY_SET_TIME_INTERVAL_FOR_CONTINOUS_TRACKING = 0x5100; // TODO: The only reply that's different from the command number
		const ushort REPLY_SET_AUTHORIZED_PHONE_NUMBER = 0x4103;
		const ushort REPLY_SET_SPEED_LIMIT_FOR_OVER_SPEED_ALARM = 0X4105;
		const ushort REPLY_SET_MOVEMENT_ALERT = 0x4106;
		const ushort REPLY_SET_EXTENDED_FUNCTIONS = 0x4108;
		const ushort REPLY_INITIALIZE_ALL_PARAMETERS = 0x4110;
		const ushort REPLY_SET_GPRS_ALERT_FOR_BUTTON_OR_INPUT = 0x4116;
		const ushort REPLY_SET_TELEPHONE_NUMBER_FOR_WIRETAPPING = 0x4130;
		const ushort REPLY_SET_TIMEZONE = 0x4132;
		const ushort REPLY_READ_TIME_INTERVAL_FOR_CONTINUOUS_TRACKING = 0x9002;
		const ushort REPLY_READ_AUTHORIZED_PHONE_NUMBER = 0x9003;
		const ushort REPLY_ALARM = 0x9999;
		const ushort REPLY_BLACK_BOX_REPORT = 0x9956;
		const ushort REPLY_SEND_SMS = 0x9957;
		const ushort REPLY_GET_PLAIN_ADDRESSS = 0x9958; // TODO: Missing from document!
		const ushort REPLY_GET_GPRMC_FROM_GSM_CELL = 0x9959;

		const ushort OTHER_REPLY_HEART_BEAT = 0xaaaa; // Document says 0xaaaa!
		const ushort OTHER_REPLY_GET_IP_AND_PORT = 0x0002;

	}
}

