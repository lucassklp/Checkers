using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.RMI
{
    public class RemoteHandle
    {
        private int thisPort;

        private string connectingIP;

        private int connectingPort;

        public RemoteHandle(string connectingIP, int connectingPort, int thisPort)
        {
            this.connectingPort = connectingPort;
            this.connectingIP = connectingIP;
            this.thisPort = thisPort;

            TcpChannel tcpChannel = new TcpChannel(this.thisPort);
            ChannelServices.RegisterChannel(tcpChannel, true);
        }


        public void Register<T>(string uri) where T: class
        {
            Type type = typeof(T);
            RemotingConfiguration.RegisterWellKnownServiceType(type, uri, WellKnownObjectMode.SingleCall);
        }


        public T GetRemoteObject<T>(string uri) 
        {
            return (T)Activator.GetObject(typeof(T), $"tcp://{this.connectingIP}:{this.connectingPort}/{uri}");
        }
    }
}
