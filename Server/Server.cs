using System.Net;
using System.Net.Sockets;
using System.Text;


namespace ChatServer
{
    public class Server
    {
        protected IPAddress ipAddress = IPAddress.Loopback;
        protected int port = 8888;
        protected readonly ILogger logger;
        protected TcpListener tcpListener;
        //protected List<StreamWriter> clientStreams;
        protected ClientRepository repository = new ClientRepository();

        public Server(ILogger logger)
        {
            this.logger = logger;
            tcpListener = new TcpListener(ipAddress, port);                        
        }

        public void Start()
        {                        
            tcpListener.Start();

            logger.Log("Сервер запущен, ожидание подключений...");
            logger.Log($"IP адресс сервера: {ipAddress}");

            while (true)
            {                
                TcpClient tcpClient = tcpListener.AcceptTcpClient();                                
                Client client = new Client(tcpClient.GetStream());                
                repository.Add(client);

                var clientThread = new ClientThread(tcpClient.GetStream(), this, this.logger);                
                var thread = new Thread(new ThreadStart(clientThread.Run));
                thread.Start();                
            }
        }
            
        public void BroadcastMessage(string message)
        {            
            foreach (var client in repository)
            {
                client.StreamWriter.WriteLine(message);         
            }
            logger.Log(message);
        }
    }
}
