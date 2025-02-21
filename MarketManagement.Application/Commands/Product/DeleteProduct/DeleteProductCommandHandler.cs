using MarketManagement.Domain.Exceptions;
using MarketManagement.Domain.Repositories;
using MarketManagement.Domain.Services.Interfaces;
using MediatR;

namespace MarketManagement.Application.Commands.Product.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Unit>
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductValidateService _productValidateService;

        public DeleteProductCommandHandler(IProductRepository productRepository, IProductValidateService productValidateService)
        {
            _productRepository = productRepository;
            _productValidateService = productValidateService;
        }

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _productValidateService.ValidateUpdateProductAsync(request.Id);

            if (!validationResult.IsValid)
            {
                throw new ValidationException($"{string.Join("; ", validationResult.Errors)}");
            }

            var product = await _productRepository.GetByIdAsync(request.Id);

            product.Deactivate();

            await _productRepository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
