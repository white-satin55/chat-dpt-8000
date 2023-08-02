namespace Server;

class Program
{
    public static void Main(string[] args)
    {
        new Server(new ConsoleLogger()).Start();
    }
}