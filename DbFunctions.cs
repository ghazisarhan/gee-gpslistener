// TODO: Impelent interface for different types of databases
using System;
using MySql.Data.MySqlClient;


namespace geegpslistener
{
	partial class MainClass
	{

		const string connectionString = "Server=127.0.0.1;Database=gpslistener;Uid=gpslistener;Pwd=FdzQXZQv9p3bShwc;"; // TODO: make it configurable

		static void test()
		{
			// update_status($device_id, $traffic_from = 0, $traffic_to = 0 )
			// get_S103device_from_db($imei)
			// put_S103position_in_db($data)
			// put_received_data($data)


		}

	}
}

