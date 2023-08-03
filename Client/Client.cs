using System.Text;
using System.Net.Sockets;

namespace ChatClient
{
    public class Client
    {                
        protected string name;

        public Client(string name)
        {
            this.name = name;
        }

        public void Connect(string IP)
        {
            using var client = new TcpClient();
            client.Connect(IP, 8888);

            using var writer = new StreamWriter(client.GetStream(), Encoding.Unicode) { AutoFlush = true };

            writer.WriteLine(name);

            Console.WriteLine("Вы подключились к серверу");

            var receiveThread = new ReceiveThread(client.GetStream());
            var thread = new Thread(new ThreadStart(receiveThread.Run));
            thread.Start();

            while (true)
            {
                string message = Console.ReadLine();
                writer.WriteLine(message);
            }
        }
    }
}
