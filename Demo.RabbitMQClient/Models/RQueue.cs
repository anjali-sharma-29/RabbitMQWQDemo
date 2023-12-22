using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.RabbitMQClient.Models
{
    public class RQueue
    {
        public string Name { get; set; }
        //Queue persist on broker restart
        public bool IsDurable { get; set; }
        //Queue limited to current connection
        public bool IsExclusive { get; set; }
        //Delete queue if all consumers unsubscribe
        public bool AutoDelete { get; set; }
        public IDictionary<string,object>? Arguments { get; set; }
    }


}
