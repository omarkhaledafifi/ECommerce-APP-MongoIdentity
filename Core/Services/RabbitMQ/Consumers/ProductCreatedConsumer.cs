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

        }
    }
}
