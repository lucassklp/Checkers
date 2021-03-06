﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Socket.TCP
{
    class SocketDataReceiver
    {
        private TcpListener server;
        public event OnReceiveSocket Changed;

        private static int BUFFER_SIZE = 1024 * 60;

        public SocketDataReceiver(TcpListener server, OnReceiveSocket Changed)
        {
            this.server = server;
            this.Changed = Changed;
        }

        public void Start()
        {
            this.server.Start();
            Console.WriteLine("Waiting for a connection... ");
            TcpClient client = server.AcceptTcpClient();
            Console.WriteLine("Connected!");
            Byte[] bytes = new Byte[BUFFER_SIZE];
            NetworkStream stream = client.GetStream();

            try
            {
                while (client.Connected)
                {
                    Console.WriteLine("Esperando receber dados!");
                    stream.Read(bytes, 0, bytes.Length);
                    this.Changed(bytes);
                    Console.WriteLine("Dados recebidos!");
                }
                stream.Close();
                client.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro");
                stream.Close();
                client.Close();
            }
        }
    }
}
