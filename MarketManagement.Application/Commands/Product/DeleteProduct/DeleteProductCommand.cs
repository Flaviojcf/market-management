using MediatR;

namespace MarketManagement.Application.Commands.Product.DeleteProduct
{
    public class DeleteProductCommand : IRequest<Unit>
    {
        public DeleteProductCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
