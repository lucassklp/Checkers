using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Checkers.Socket.TCP
{
    public delegate void OnReceiveSocket(object sender, byte[] data);

    public class TcpSocket
    {
        private TcpClient client;
        private TcpListener server;

        private string ip;
        private int port;

        public bool IsConnected
        {
            get
            {
                return this.client != null;
            }
        }

        public event OnReceiveSocket OnReceiveSocket;

        public TcpSocket(string host, int listeningPort)
        {
            this.ip = host;
            this.port = listeningPort;
            IPAddress ip = IPAddress.Parse(host);
            this.server = new TcpListener(ip, listeningPort);
        }

        public void StartServer()
        {
            Console.WriteLine("Server iniciado. Host: {0}, Port: {1}", this.ip, this.port);
            SocketDataReceiver dataReceiver = new SocketDataReceiver(this.server, this.OnReceiveSocket);
            Thread thread = new Thread(dataReceiver.Start);
            thread.Start();
        }

        public void Connect(string ip, int port)
        {
            this.client = new TcpClient();
            this.client.Connect(ip, port);
        }

        public void Send(byte[] data)
        {
            if (this.client.Connected)
            {
                NetworkStream stream = client.GetStream();

                // Send the message to the connected TcpServer. 
                stream.Write(data, 0, data.Length);
            }
            else
            {
                client.Close();
            }
        }
    }
}
