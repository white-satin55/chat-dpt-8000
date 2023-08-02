using System.Net.Sockets;
using System.Text;

namespace Client
{
    public class ReceiveThread
    {
        protected StreamReader reader;

        public ReceiveThread(NetworkStream networkStream)        
        {
            reader = new StreamReader(networkStream, Encoding.Unicode);
        }

        public void Run()
        {
            try
            {
                while (true)
                {
                    string message = reader.ReadLine();
                    Console.WriteLine(message);
                }
            }
            catch (IOException)
            {
                Console.WriteLine("Соединение разорвано");
            }
        }
    }
}
