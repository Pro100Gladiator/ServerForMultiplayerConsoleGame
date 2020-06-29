using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Server
{
    class Program
    {
        static Socket socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
        static void Main(string[] args)
        {
            Console.Title = "Server";

            socket.Bind(new IPEndPoint(IPAddress.Any, 2048));
            socket.Listen(0);

            socket.BeginAccept(AcceptCallBack, null);

            Console.ReadLine();
        }
        static void AcceptCallBack(IAsyncResult ar)
        {
            Socket client = socket.EndAccept(ar);
            Thread thread = new Thread(HandleClient);
            thread.Start(client);
        }

        static void HandleClient(object o)
        {
            var client = (Socket)o;
            MemoryStream ms = new MemoryStream(new byte[256], 0, 256, true, true);

            while (true)
            {
                client.Receive(ms.GetBuffer());
            }
        }
    }
}
