using MarketManagement.Domain.Entities;
using MarketManagement.Domain.Exceptions;
using MarketManagement.Domain.Repositories;
using MarketManagement.Domain.Services.Interfaces;
using MediatR;

namespace MarketManagement.Application.Commands.Product.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductValidateService _productValidate;

        public CreateProductCommandHandler(IProductRepository productRepository, IProductValidateService productValidate)
        {
            _productRepository = productRepository;
            _productValidate = productValidate;
        }

        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _productValidate.ValidateCreateProductAsync(request.Name);

            if (!validationResult.IsValid)
            {
                throw new ValidationException($"{string.Join("; ", validationResult.Errors)}");
            }

            var product = new ProductEntity(request.Name, request.CurrentPrice, request.LastMonthPrice, request.CategoryEnum);

            await _productRepository.CreateAsync(product);

            return product.Id;
        }
    }
}
