using MarketManagement.Domain.Exceptions;
using MarketManagement.Domain.Repositories;
using MarketManagement.Domain.Services.Interfaces;
using MediatR;

namespace MarketManagement.Application.Commands.Product.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Unit>
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductValidateService _productValidate;

        public UpdateProductCommandHandler(IProductRepository productRepository, IProductValidateService productValidate)
        {
            _productRepository = productRepository;
            _productValidate = productValidate;
        }

        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _productValidate.ValidateUpdateProductAsync(request.Id);

            if (!validationResult.IsValid)
            {
                throw new ValidationException($"{string.Join("; ", validationResult.Errors)}");
            }

            var product = await _productRepository.GetByIdAsync(request.Id);

            product.Update(request.Name, request.CurrentPrice, request.LastMonthPrice, request.CategoryEnum);

            await _productRepository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
