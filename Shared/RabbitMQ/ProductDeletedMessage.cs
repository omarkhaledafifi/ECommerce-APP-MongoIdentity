using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.RabbitMQ
{
    public class ProductDeletedMessage : BasicMessage
    {
        public int Id { get; set; }
    }
}
