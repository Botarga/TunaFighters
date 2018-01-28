using System;
using System.Net;
using System.Net.Sockets;

namespace TunaFightersServer
{
    public class Program
    {
        private const string version = "1.0.0";
        private const int port = 27015;
        private static IPAddress serverIp;

        private static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        public static void Main(string[] args)
        {
            Console.WriteLine("Tuna Fighters server version {0} started at {1}", version, DateTime.Now);
            Console.WriteLine("Resolving ip...");
            using (var client = new WebClient())
            {
                string ipRaw = client.DownloadString("http://checkip.dyndns.org");
                int beginIndex = ipRaw.IndexOf(":") + 2;
                int endIndex = ipRaw.IndexOf("<", beginIndex);
                ipRaw = ipRaw.Substring(beginIndex, endIndex - beginIndex);
                serverIp = IPAddress.Parse(ipRaw);
            }

            Console.WriteLine("Listening by {0}:{1} Local IP: {2}", serverIp.ToString(), port, GetLocalIPAddress());
            TcpListener server = null;
            byte[] bufferReceived = new byte[256];
            string dataReceived = null;

            server = new TcpListener(port);
            server.Start();
            while (true)
            {
                Console.WriteLine("Listening...");
                TcpClient client = server.AcceptTcpClient();
                NetworkStream stream = client.GetStream();
                byte[] data = new byte[256];

                // String to store the response ASCII representation.
                String responseData = String.Empty;

                // Read the first batch of the TcpServer response bytes.
                Int32 bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                Console.WriteLine("Received:" + responseData);

                data = System.Text.Encoding.ASCII.GetBytes("success");
                stream.Write(data, 0, data.Length);
            }
        }
    }       
}
