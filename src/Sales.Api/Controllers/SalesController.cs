using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sales.Application.Commands;
using Sales.Application.Dto;

namespace Sales.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SalesController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaleDto dto)
        => Ok(await _mediator.Send(new CreateSaleCommand(dto)));

        
    }
}
