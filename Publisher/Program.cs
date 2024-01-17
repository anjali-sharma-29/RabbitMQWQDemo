using EasyNetQ;
using RabbitMQMessages;

public class Program
{
    public static void Main(string[] args)
    {
        using (var bus = RabbitHutch.CreateBus("host=localhost"))
        {
            for (int i = 0; i < 10; i++)
            {
                var input = new User
                {
                    Name = "Anjali Sharma",
                    Age = 25,
                    Gender = 'F',
                    Occupation = "Software Developer"
                };

                bus.PubSub.Publish(input);
                Console.WriteLine("Message published!");
            }
        }
        Console.WriteLine("Press any key to exit");
        Console.ReadLine();
    }
}