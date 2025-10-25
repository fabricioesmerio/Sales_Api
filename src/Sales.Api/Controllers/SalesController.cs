using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sales.Application.Commands;
using Sales.Application.Dto;
using Sales.Application.Interfaces;
using Sales.Domain.Entities;

namespace Sales.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ISaleRepository _repo;
        private readonly IMapper _mapper;

        public SalesController(
            IMediator mediator,
            ISaleRepository repo,
            IMapper mapper)
        {
            _mediator = mediator;
            _repo = repo;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaleDto dto)
        => Ok(await _mediator.Send(new CreateSaleCommand(dto)));

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok((await _repo.GetAllAsync()).Select(_mapper.Map<SaleDto>));

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var sale = await _repo.GetByIdAsync(id);
            if (sale == null) return NotFound();
            return Ok(_mapper.Map<SaleDto>(sale));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _repo.DeleteAsync(id);
            await _repo.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSale(Guid id, [FromBody] SaleUpdateDto saleDto)
        {
            var sale = _mapper.Map<Sale>(saleDto);
            sale.Id = id;
            await _mediator.Send(new UpdateSaleCommand(sale));
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> CancelSale(Guid id)
        => Ok(await _mediator.Send(new CancelSaleCommand(id)));

    }
}
