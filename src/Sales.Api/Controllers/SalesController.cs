using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sales.Application.Commands;
using Sales.Application.Dto;
using Sales.Application.Interfaces;

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

    }
}
