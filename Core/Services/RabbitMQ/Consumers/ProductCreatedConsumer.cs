using MediatR;
using Shared.RabbitMQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.RabbitMQ.Consumers
{
    public class ProductCreatedConsumer
    {
        IMediator _mediator;
        public ProductCreatedConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }

        public void Consume(BasicMessage basicMessage)
        {
            var message = basicMessage as ProductCreatedMessage;
            Console.WriteLine($"""
                the product with the next data was Created.
                Product :
                Date : {message.Date},
                Type : {message.Type},
                Id : {message.Id},
                Name : {message.Name},
                Description : {message.Description},
                PictureUrl : {message.PictureUrl},
                Price : {message.Price},
                BrandId : {message.BrandId},
                BrandName : {message.BrandName},
                TypeId : {message.TypeId},
                TypeName : {message.TypeName}
                """);

        }
    }
}
