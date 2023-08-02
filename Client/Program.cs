namespace Client;

class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Введите IP сервера:");
        string ip = Console.ReadLine();
        try
        {
            new Client().Connect(ip);
        }        
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        Console.ReadLine();
    }
}