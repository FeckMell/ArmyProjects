using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Torrent
{
    public class UDPSocket
    {
        private UdpClient tttSocket;
        private IPEndPoint tttRemoteEP;
        private IPEndPoint tttLocalEP;

        public UdpClient ThatSocket { get => tttSocket; set => tttSocket = value; }
        public IPEndPoint ThatRemoteEP { get => tttRemoteEP; set => tttRemoteEP = value; }
        public IPEndPoint ThatLocalEP { get => tttLocalEP; set => tttLocalEP = value; }

        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public UDPSocket(string server_ip_, string server_port_)
        {
            ThatLocalEP = new IPEndPoint(IPAddress.Parse(server_ip_), Int32.Parse(server_port_));
            ThatSocket = new UdpClient(ThatLocalEP);
        }
        public UDPSocket(string server_ip_, int server_port_)
        {
            ThatLocalEP = new IPEndPoint(IPAddress.Parse(server_ip_), server_port_);
            ThatSocket = new UdpClient(ThatLocalEP);
        }
        public UDPSocket(string server_ip_, string server_port_, string remote_ip_, string remote_port_)
        {
            ThatRemoteEP = new IPEndPoint(IPAddress.Parse(remote_ip_), Int32.Parse(remote_port_));
            ThatLocalEP = new IPEndPoint(IPAddress.Parse(server_ip_), Int32.Parse(server_port_));
            ThatSocket = new UdpClient(ThatLocalEP);
        }
        public UDPSocket(string server_ip_, int server_port_, string remote_ip_, int remote_port_)
        {
            ThatRemoteEP = new IPEndPoint(IPAddress.Parse(remote_ip_), remote_port_);
            ThatLocalEP = new IPEndPoint(IPAddress.Parse(server_ip_), server_port_);
            ThatSocket = new UdpClient(ThatLocalEP);
        }
        ~UDPSocket()
        {
            ThatSocket.Close();
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public void SetRemoteEndPoint(string remote_ip_, string remote_port_)
        {
            ThatRemoteEP = new IPEndPoint(IPAddress.Parse(remote_ip_), Int32.Parse(remote_port_));
        }
        public void SetRemoteEndPoint(string remote_ip_, int remote_port_)
        {
            ThatRemoteEP = new IPEndPoint(IPAddress.Parse(remote_ip_), remote_port_);
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public void SendTo(string mess_)
        {
            Byte[] bytes = Encoding.ASCII.GetBytes(mess_);
            ThatSocket.Connect(ThatRemoteEP);
            ThatSocket.Send(bytes, bytes.Length);
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public string ReceiveFrom()
        {
            IPEndPoint copyEP = new IPEndPoint(ThatRemoteEP.Address, ThatRemoteEP.Port);
            Byte[] receiveBytes = ThatSocket.Receive(ref copyEP);
            string result = Encoding.ASCII.GetString(receiveBytes);

            return result;
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public void AsyncSentTo(string mess_)
        {

        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public string AsyncReceiveFrom()
        {
            return "";
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public string AsyncReceiveAny()
        {
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
            return "";
        }
    }
}
