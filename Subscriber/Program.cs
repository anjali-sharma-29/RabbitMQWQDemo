using EasyNetQ;
using RabbitMQMessages;

public class Program
{   
    public static void Main(string[] args)
    {
        using (var bus = RabbitHutch.CreateBus("host=localhost"))
        {
            bus.PubSub.Subscribe<User>("user", UserHandler);
            Console.WriteLine("Listening for messages. Hit <return> to quit.");
            Console.ReadLine();
        }
        Console.WriteLine("Press any key to exit");
        Console.ReadLine();
    }

    public static void UserHandler(User u)
    {
        Console.WriteLine("Added User:  "+ u.Name);
    }
}