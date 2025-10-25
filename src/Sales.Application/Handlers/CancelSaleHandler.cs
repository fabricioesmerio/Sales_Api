

using MediatR;
using Sales.Application.Commands;
using Sales.Application.Interfaces;

namespace Sales.Application.Handlers
{

    public class CancelSaleHandler : IRequestHandler<CancelSaleCommand,Guid>
    {
        private readonly ISaleRepository _repo;
        private readonly IEventPublisher _publisher;

        public CancelSaleHandler(
            ISaleRepository repo,
            IEventPublisher publisher
            )
        {
            _repo = repo;
            _publisher = publisher;
        }

            public async Task<Guid> Handle(CancelSaleCommand command, CancellationToken cancellationToken)
            {
                var sale = await _repo.GetByIdAsync(command.Id)
                    ?? throw new KeyNotFoundException("Sale not found.");

                sale.Cancel();

                await _repo.UpdateAsync(sale);
                await _repo.SaveChangesAsync();
                await _publisher.PublishAsync("saleCancelled", sale.SaleNumber);
                await _publisher.PublishAsync("itemCancelled", sale.SaleNumber);

            return command.Id;
            }
    }
}
