using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Torrent
{
    public class TCPSocket
    {
        //private TcpListener tttServer;
        private TcpClient tttSocket;
        private NetworkStream tttStream;
        private bool tttClosed = true;


        //public TcpListener ThatServer { get => tttServer; set => tttServer = value; }
        public TcpClient ThatSocket { get => tttSocket; set => tttSocket = value; }
        public NetworkStream ThatStream { get => tttStream; set => tttStream = value; }
        public bool ThatClosed { get => tttClosed; set => tttClosed = value; }


        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public TCPSocket(string ip_, string port_)
        {
            //ThatServer = new TcpListener(new IPEndPoint(IPAddress.Parse(ip_), Int32.Parse(port_)));
            ThatSocket = new TcpClient(ip_, Int32.Parse(port_));
            ThatStream = ThatSocket.GetStream();
            ThatClosed = false;
        }
        public TCPSocket(string ip_, int port_)
        {
            try
            {
                ThatSocket = new TcpClient(ip_, port_);
                ThatStream = ThatSocket.GetStream();
                ThatClosed = false;
            }
            catch (Exception e)
            {
                MessageBox.Show( e.Message);
            }
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public void CloseConnection()
        {
            ThatSocket.Close();
            ThatStream.Close();
            ThatClosed = true;
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public void Send(string mess_)
        {
            ThatStream.Write(System.Text.Encoding.ASCII.GetBytes(mess_), 0, mess_.Length);
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public string Receive()
        {
            Byte[] buf = new Byte[16];
            string result = string.Empty;
            Int32 bytes = ThatStream.Read(buf, 0, buf.Length);
            result = System.Text.Encoding.ASCII.GetString(buf, 0, bytes);
            return result;
        }
    }
}
