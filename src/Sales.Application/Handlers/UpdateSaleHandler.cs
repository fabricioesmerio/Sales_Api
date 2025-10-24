

using AutoMapper;
using MediatR;
using Sales.Application.Commands;
using Sales.Application.Dto;
using Sales.Application.Interfaces;
using Sales.Domain.Entities;

namespace Sales.Application.Handlers
{
    public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, Sale>
    {
        private readonly ISaleRepository _repo;
        private readonly IMapper _mapper;
        private readonly IEventPublisher _publisher;

        public UpdateSaleHandler(
            ISaleRepository repo, 
            IMapper mapper, 
            IEventPublisher publisher
            )
        { 
            _repo = repo;
            _mapper = mapper;
            _publisher = publisher;
        }

        public async Task<Sale> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
        {

            var sale = await _repo.GetByIdAsync(request.Sale.Id)
                ?? throw new KeyNotFoundException();

            if (sale.IsCancelled)
                throw new InvalidOperationException("Cannot update a cancelled sale.");

            sale.UpdateDetails(request.Sale.SaleNumber, request.Sale.Customer, 
                request.Sale.Branch, request.Sale.Date);

            sale.UpdateItems(request.Sale.Items);

            await _repo.UpdateAsync(sale);
            await _repo.SaveChangesAsync();
            await _publisher.PublishAsync("saleModified", sale.SaleNumber);
            return sale;
        }
    }
}
