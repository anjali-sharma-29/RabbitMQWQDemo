﻿using Demo.RabbitMQClient;
using RabbitMQ.Client;
using System.Text;
using Demo.RabbitMQClient.Models;
using RabbitMQ.Client.Events;
public class Subcriber_A
{
    private static IRabbitMQClient _mQClient;

    public static void Main(string[] args)
    {
        _mQClient = new RabbitMQClient();

        Console.WriteLine("Starting Message Subscriber A");                

        var channel = RabbitMQClient.GetChannel();
        var consumer = new EventingBasicConsumer(RabbitMQClient.GetChannel());       

        _mQClient.DeclareConsumer(consumer,"Sub_A",false, (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine($" [x] Received {message}");

            // Send acknowledgement when task is done
            channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
        });                

        Console.WriteLine("Press any key to exit");
        Console.ReadLine();
    }
}
