using MarketManagement.Domain.Entities;
using MarketManagement.Domain.Repositories;
using MediatR;

namespace MarketManagement.Application.Commands.Product.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
    {
        private readonly IProductRepository _productRepository;

        public CreateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new ProductEntity(request.Name, request.CurrentPrice, request.LastMonthPrice, request.CategoryEnum);

            await _productRepository.CreateAsync(product);

            return product.Id;
        }
    }
}
