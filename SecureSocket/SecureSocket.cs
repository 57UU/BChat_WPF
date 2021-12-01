using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;
using SecureSocket;

namespace SecureSocket
{
   
    public class SecureSocket
    {
        private Socket socket;
        public SecureSocket(string IP,int port) 
        {
            //使用指定的地址簇协议、套接字类型和通信协议
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(new IPEndPoint(IPAddress.Parse(IP), port));
            init();
        }
/*        public SecureSocket()
        {
            init();
        }*/
        public string read()
        {
            return Utilities.AESDecrypt(readDirect(), AESKey);
        }
        public void send(string msg)
        {
            string s = Utilities.AESEncrypt(msg, AESKey);
            //Console.WriteLine(s);
            sendDirect(s);
        }




        private void init()
        {
            var stream = new NetworkStream(socket);
            reader = new StreamReader(stream);
            writer = new StreamWriter(stream);
            string RSAPublicKey = readDirect();
            Console.WriteLine(RSAPublicKey);
            AESKey = Utilities.getAESKey(16);
            Console.WriteLine("AES Key: "+AESKey);
            sendDirect(Utilities.RSAEncrypt(RSAPublicKey, AESKey));
            readDirect();
        }
        private string AESKey;
        private StreamReader reader;
        private  StreamWriter writer;
        private static char EOF = (char)4;
        private void sendDirect(string msg)
        {
            writer.Write(msg);
            writer.Write(EOF);
            writer.Flush();
        }
        private StringBuilder sb = new StringBuilder();
        private string readDirect()
        {
            sb.Clear();
            char[] recv = new char[1];
            int bytes;
            while(true)
            {
                _= reader.ReadBlock(recv, 0, 1);
                char charRead = recv[0];
                if (charRead==(EOF))
                {
                    break;
                }
                sb.Append(charRead);
            } 
            return sb.ToString();
        }
        //------------------------test-----------------------------
        public static void Main()
        {
            SecureSocket ss = new SecureSocket("127.0.0.1", 3333);
            ss.send("Hi");
            Console.WriteLine(ss.read());
        }

    }
}
