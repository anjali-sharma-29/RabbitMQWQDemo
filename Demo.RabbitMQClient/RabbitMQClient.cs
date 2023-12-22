using Demo.RabbitMQClient.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Demo.RabbitMQClient
{
    public interface IRabbitMQClient {
        string DeclareQueue();
        void DeclareQueue(RQueue q);
        void DeclareExchange(RExchange ex);
        void BindQueueToRoutingKeys(string qName, string exchange, List<string> rKeys);
        void BindQueueToRoutingKey(string qName, string exchange, string key);
        void DeclareConsumer(EventingBasicConsumer basicConsumer,
            string qName,
            bool autoAck,
            EventHandler<BasicDeliverEventArgs> handler);
        void PublishMessage(RMessage rMessage);        

    }
    public class RabbitMQClient: IRabbitMQClient
    {
        private static  ConnectionFactory _factory;
        private static  IModel _channel;
         
        public static IModel GetChannel()
        {            
            _factory = new ConnectionFactory { HostName = "localhost" };
            if (_channel == null)
            {
                var connection = _factory.CreateConnection();                
                // return channel
                _channel = connection.CreateModel();
                return _channel;                
            }
            return _channel;
        }

        public string DeclareQueue()
        {
            GetChannel();
            return _channel.QueueDeclare().QueueName;
        }
        public void DeclareQueue(RQueue q) {
            GetChannel();
            _channel.QueueDeclare(queue: q.Name,
                         durable: q.IsDurable,
                         exclusive: q.IsExclusive,
                         autoDelete: q.AutoDelete,
                         arguments: q.Arguments);
        }

        public void DeclareExchange(RExchange ex)
        {
            GetChannel();
            _channel.ExchangeDeclare(exchange: ex.Name,
                    type: ex.Type);            
        }

        public void BindQueueToRoutingKeys(string qName,string exchange,List<string> rKeys)
        {            
                foreach(var key in rKeys) {
                    BindQueueToRoutingKey(qName,exchange,key);
                }                         
        }

        public void BindQueueToRoutingKey(string qName, string exchange, string key)
        {
            GetChannel();
            _channel.QueueBind(queue: qName,
                      exchange: exchange,
                      routingKey: key);
        }

        public void DeclareConsumer(EventingBasicConsumer basicConsumer, 
            string qName,
            bool autoAck,
            EventHandler<BasicDeliverEventArgs> handler) {
            GetChannel();
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received +=handler;
            _channel.BasicConsume(queue: qName,
                                 autoAck: autoAck,
                                 consumer: consumer);
        }

        public void PublishMessage(RMessage rMessage)
        {
            GetChannel();
            _channel.BasicPublish(exchange: rMessage.Exchange,
                             routingKey: rMessage.RoutingKey,
                             basicProperties: null,
                             body: Encoding.UTF8.GetBytes(rMessage.Message));
        }
    }
}