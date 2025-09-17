using Domain.Contracts;
using MediatR;
using Shared.CQRS.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.CQRS.Product.Handlers
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Domain.Entities.Product() { Id = request.Id };
            _unitOfWork.GetRepository<Domain.Entities.Product, int>().DeleteAsync(product);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }
    }
}
