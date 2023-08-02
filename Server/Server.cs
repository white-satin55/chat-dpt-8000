using System.Net;
using System.Net.Sockets;
using System.Text;


namespace Server
{
    public class Server
    {
        protected readonly ILogger logger;
        protected TcpListener tcpListener;
        protected List<StreamWriter> clientStreams;

        public Server(ILogger logger)
        {
            this.logger = logger;
            clientStreams = new List<StreamWriter>();
        }

        public void Start()
        {
            var ip = IPAddress.Parse("127.0.0.1");
            tcpListener = new TcpListener(ip, 8888);
            tcpListener.Start();

            logger.Log("Сервер запущен, ожидание подключений...");
            logger.Log($"IP адресс сервера: {ip}");

            while (true)
            {                
                TcpClient tcpClient = tcpListener.AcceptTcpClient();                                

                clientStreams.Add(new StreamWriter(tcpClient.GetStream(), Encoding.Unicode) { AutoFlush = true });

                var clientThread = new ClientThread(tcpClient.GetStream(), this, this.logger);                
                var thread = new Thread(new ThreadStart(clientThread.Run));
                thread.Start();                
            }
        }
        
        public void Disconnect(Stream stream)
        {            
            clientStreams.Remove(clientStreams.Find(s => s.BaseStream.Equals(stream)));
        }

        public void BroadcastMessage(string message)
        {            
            foreach (var stream in clientStreams)
            {
                stream.WriteLine(message);                
            }
            logger.Log(message);
        }
    }
}
