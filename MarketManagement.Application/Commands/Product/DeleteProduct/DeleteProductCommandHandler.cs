using MarketManagement.Domain.Repositories;
using MediatR;

namespace MarketManagement.Application.Commands.Product.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Unit>
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id);

            product.Deactivate();

            await _productRepository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
