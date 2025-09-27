using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.RabbitMQ
{
    public class BasicMessage
    {
        public DateTime Date { get; set; }
        public string Type { get; set; }
    }
}
