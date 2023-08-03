using System.Net.Sockets;
using System.Text;

namespace ChatServer
{
    public class Client
    {
        public StreamWriter StreamWriter { get; private set; }

        public Client(NetworkStream stream)
        {
            StreamWriter = new StreamWriter(stream, Encoding.UTF8);
        }
    }
}
