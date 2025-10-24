

using MediatR;
using Sales.Domain.Entities;

namespace Sales.Application.Commands
{
    
    public record UpdateSaleCommand(Sale Sale) : IRequest<Sale>;
    
}
