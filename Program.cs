// TODO: Improve signal handler, look for weak points and error, specially with cross thread call, this is the first I use it
// TODO: check why error was raised inside a try block!!! in NetBaseServer.cs Line: 353
//		Cancel blocking accepttcpclient to avoid error

// TODO: Cancel blocking accepttcpclient before stopping server

using System;
using log4net;
using NetSockets;
using System.Net;
using System.Threading;
using Mono.Unix;
using Mono.Unix.Native;
using System.Net.Sockets;

namespace geegpslistener
{
	partial class MainClass
	{
		private static readonly ILog log = LogManager.GetLogger(typeof(MainClass));
		private static NetServer tcpserver = new NetServer ();

		public static void Main (string[] args)
		{
			StartSignalHandler();
			Logger.Setup ();

			log.Info ("Server Started.");
			log.Info ("Starting TCP Service.");


			tcpserver.OnClientConnected += HandleOnClientConnected;
			tcpserver.OnClientDisconnected += HandleOnClientDisconnected;
			tcpserver.OnError += HandleOnError;
			tcpserver.OnReceived += HandleOnReceived;
			tcpserver.OnClientAccepted += HandleOnClientAccepted;
			tcpserver.Start (IPAddress.Parse ("0.0.0.0"), 11000);	// TODO: make it configurable

			log.Info ("TCP Service started succefully.");

			Console.WriteLine ("Press Ctrl+C to exit.");

			while (true)
				Thread.Sleep (1000);
		}

		static void shutDown()
		{
			log.Info ("Stopping TCP Service.");
			//-------------------------------------------------------
			// TODO: Problem Solved
			// while ((DateTime.Now - startTime).Seconds < 10)		// work around to overcome an error when the service
			//	Thread.Sleep (1000);								// shuts down immediately after start
			//-------------------------------------------------------
			tcpserver.Stop ();
			log.Info ("Server is now shutdown, bye.");
			Environment.Exit (0);
		}

		static void HandleOnClientAccepted (object sender, NetClientAcceptedEventArgs e)
		{
			log.Info ("Client ID: " + e.Guid.ToString () + " -- Client Connection Accepted.");
		}

		static void HandleOnError (object sender, NetExceptionEventArgs e)
		{
			if (e.Exception.GetType ().IsAssignableFrom (typeof(SocketException))
			    && (((SocketException)e.Exception).ErrorCode == 10004)) 
			{
				// do nothing, normal error because the TcpListener.AcceptTcpClient blocking call was interrupted
				//TODO: refine the if statement to avoid this empty block
			}
			else
			{
				log.Error ("Socker error occured, error message: " + e.Exception.Message);
			}
				
		}

		static void HandleOnClientDisconnected (object sender, NetClientDisconnectedEventArgs e)
		{
			log.Info ("Client ID: " + e.Guid.ToString () + " -- Client Disconnected.");
		}

		static void HandleOnClientConnected (object sender, NetClientConnectedEventArgs e)
		{
			log.Info ("Client ID: " + e.Guid.ToString () + " -- New client attempting to connect.");
			e.Reject = false;
		}

		static void StartSignalHandler()
		{
			new Thread(TerminateHandler).Start();
		}

		static void TerminateHandler()
		{
			UnixSignal[] signals = new UnixSignal [] {
				new UnixSignal (Signum.SIGINT),
				new UnixSignal (Signum.SIGTERM),
				//new UnixSignal (Signum.SIGQUIT),		// TODO: don't know this signal!
				//new UnixSignal (Signum.SIGHUP),		// TODO: don't know this signal!
			};

			while(true)
			{
				int index = UnixSignal.WaitAny (signals, -1);
				Mono.Unix.Native.Signum signal = signals [index].Signum;
				log.Info ("Recieved Signal " + signal.ToString () + ", shutting down.");
				shutDown ();
			}
		}
	}
}
