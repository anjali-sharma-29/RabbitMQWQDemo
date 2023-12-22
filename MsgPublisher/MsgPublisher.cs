// See https://aka.ms/new-console-template for more information

using Demo.RabbitMQClient;
using RabbitMQ.Client;
using System.Text;
using Demo.RabbitMQClient.Models;

public class MsgPublisher {
    private static IRabbitMQClient _mQClient;
   
    public static void Main(string[] args) {        
        _mQClient = new RabbitMQClient();

        Console.WriteLine("Starting Message Publisher");

        //Declare Exchange
        _mQClient.DeclareExchange(new RExchange()
        {
            Name = "DemoPubSub",
            Type = ExchangeType.Direct
        });

        //Declare Queue Sub_A
        _mQClient.DeclareQueue(new RQueue()
        {
            Name = "Sub_A",
            IsDurable = false,
            IsExclusive=false,
            AutoDelete=false,
        });

        //Declare Queue Sub_B
        _mQClient.DeclareQueue(new RQueue()
        {
            Name = "Sub_B",
            IsDurable = false,
            IsExclusive = false,
            AutoDelete = false,
        });

        //Both Queue having same binding
        //_mQClient.BindQueueToRoutingKey("Sub_A", "DemoPubSub","Sub");
        //_mQClient.BindQueueToRoutingKey("Sub_B", "DemoPubSub", "Sub");

        _mQClient.BindQueueToRoutingKey("Sub_A", "DemoPubSub", "Sub_A");
        _mQClient.BindQueueToRoutingKey("Sub_B", "DemoPubSub", "Sub_B");

        for (int i=0;i<10;i++)
        {
             Thread.Sleep(i * 1000);
            _mQClient.PublishMessage(new RMessage()
            {
                Exchange = "DemoPubSub",
                Message = "CheckingDmeo PubSub RabbitMQ Working  "+i,
                RoutingKey =i%2==0 ? "Sub_A": "Sub_B"
            });
        }        

        Console.WriteLine("Press any key to exit");
        Console.ReadLine();
    }
}
