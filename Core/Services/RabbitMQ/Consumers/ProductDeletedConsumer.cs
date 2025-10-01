using MediatR;
using Shared.RabbitMQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.RabbitMQ.Consumers
{
    public class ProductDeletedConsumer(IMediator mediator)
    {

        public void Consume(BasicMessage basicMessage)
        {
            var message = basicMessage as ProductDeletedMessage;
            Console.WriteLine($"""
                the deleted product was Received seccessfully and thats its data :
                 Id : {message.Id},
                 Type : {message.Type},
                 Date : {message.Date}
                """);
        }
    }
}
