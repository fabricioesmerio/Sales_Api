

using MediatR;
using Sales.Application.Dto;

namespace Sales.Application.Commands
{    
    public record CreateSaleCommand(SaleDto Sale) : IRequest<SaleDto>;
}
