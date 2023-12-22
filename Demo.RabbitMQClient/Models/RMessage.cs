using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.RabbitMQClient.Models
{
    public class RMessage
    {
        public string Exchange { get; set; }
        public string RoutingKey { get; set; }
        public string Message { get; set; }
    }
}
