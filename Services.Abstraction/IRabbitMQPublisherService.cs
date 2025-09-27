using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction
{
    public interface IRabbitMQPublisherService
    {
        Task PublishMessage(string exchangeName, string routingKey, string message);
    }
}
