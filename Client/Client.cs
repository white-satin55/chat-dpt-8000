using System.Text;
using System.Net.Sockets;

namespace Client
{
    public class Client
    {

        protected TcpClient client;
        protected StreamWriter writer;

        public void Connect(string IP)
        {
            client = new TcpClient();
            client.Connect(IP, 8888);

            writer = new StreamWriter(client.GetStream(), Encoding.Unicode) { AutoFlush = true };

            Console.WriteLine("Введите имя:");
            string name = Console.ReadLine();

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
