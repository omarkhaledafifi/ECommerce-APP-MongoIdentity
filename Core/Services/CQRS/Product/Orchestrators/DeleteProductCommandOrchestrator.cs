using MediatR;
using Services.Abstraction;
using Services.Abstraction.CQRS;
using Shared.CQRS.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.CQRS.Product.Orchestrators
{
    public class DeleteProductCommandOrchestrator(IMediator mediator, IImageHelper imageHelper) : IDeleteProductCommandOrchestrator
    {
        public async Task<bool> DeleteProductAsync(int id)
        {
            var Product = await mediator.Send(new GetProductByIdQuery(id));
            if (Product == null)
                return false;

            var isDeleted = await mediator.Send(new DeleteProductCommand(id));
            if (isDeleted)
                imageHelper.DeleteImage(Product.PictureUrl);

            return isDeleted;
        }
    }
}
