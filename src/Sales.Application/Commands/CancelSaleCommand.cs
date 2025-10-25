
using MediatR;

namespace Sales.Application.Commands
{
     public record CancelSaleCommand(Guid Id) : IRequest<Guid>;
}
