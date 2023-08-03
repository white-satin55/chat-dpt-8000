using System.Text;
using System.Net.Sockets;

namespace ChatServer
{
    public class ClientThread
    {
        protected ILogger logger;
        protected StreamReader reader;
        protected Server server;
        public string Name { get; private set; }

        public ClientThread(NetworkStream networkStream, Server server, ILogger logger)
        {
            reader = new StreamReader(networkStream, Encoding.Unicode);
            Name = reader.ReadLine();
            this.server = server;            
            this.logger = logger;
        }

        public void Run()
        {            
            try
            {
                server.BroadcastMessage($"[{DateTime.Now.ToString("hh:mm:ss")}] : {Name} присоединился в чат.");
                while (true)
                {
                    string message = reader.ReadLine();

                    if (string.IsNullOrEmpty(message))
                        continue;

                    server.BroadcastMessage($"[{DateTime.Now.ToString("hh:mm:ss")}] {Name} : {message}");
                }
            }
            catch (IOException)
            {
                server.Disconnect(reader.BaseStream);                               
                server.BroadcastMessage($"[{DateTime.Now.ToString("hh:mm:ss")}] {Name} покинул чат");
            }
        }
    }
}
