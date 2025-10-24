using AutoMapper;
using MediatR;
using Sales.Application.Dto;
using Sales.Application.Interfaces;
using Sales.Domain.Entities;

namespace Sales.Application.Commands
{
    public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, SaleDto>
    {
        private readonly ISaleRepository _repo;
        private readonly IMapper _mapper;
        private readonly IEventPublisher _publisher;

        public CreateSaleHandler(ISaleRepository repo, IMapper mapper, IEventPublisher publisher)
        {
            _repo = repo;
            _mapper = mapper;
            _publisher = publisher;
        }

        public async Task<SaleDto> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            var saleDto = request.Sale;
            var sale = _mapper.Map<Sale>(saleDto);

            sale.Id = Guid.NewGuid();
            sale.Items.Clear();

            saleDto.Items.ForEach(itemDto =>
            {
                var item = _mapper.Map<SaleItem>(itemDto);
                sale.AddItem(item);
            });
            
            await _repo.AddAsync(sale);
            await _repo.SaveChangesAsync();
            await _publisher.PublishAsync("saleCreated", sale.SaleNumber);
            return _mapper.Map<SaleDto>(sale);
        }
    }
}
