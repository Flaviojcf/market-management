using MarketManagement.Domain.Repositories;
using MediatR;

namespace MarketManagement.Application.Commands.Product.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Unit>
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id);

            product.Update(request.Name, request.CurrentPrice, request.LastMonthPrice, request.CategoryEnum);

            await _productRepository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
