using ChatServer;
using Infrastructure.Logging;
using ChatClient;

public class Program
{    
    public static void Main(string[] args)
    {
        new Server(new ConsoleLogger()).Start();

        while (true)
        {
            Console.WriteLine("Что делать?");
            Console.WriteLine("1. Добавить клиента");
            Console.WriteLine("2. Переключиться на существующего");
        }
    }
    
    static ChatClient.Client CreateClient(string name, string ip)
    {
        var client = new ChatClient.Client(name);
        client.Connect(ip);
        return client;
    }

}