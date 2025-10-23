using Microsoft.EntityFrameworkCore;
using MediatR;
using AutoMapper;
//using Sales.Infrastructure.Persistence;
//using Sales.Application.Interfaces;
//using Sales.Infrastructure.Repositories;
//using Sales.Infrastructure.Messaging;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Configurar EF Core com PostgreSQL
//builder.Services.AddDbContext<SalesDbContext>(options =>
  //  options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Adicionar MediatR (Handlers da aplicação)
//builder.Services.AddMediatR(cfg =>
 //   cfg.RegisterServicesFromAssembly(Assembly.Load("Sales.Application")));

// AutoMapper
builder.Services.AddAutoMapper(Assembly.Load("Sales.Application"));

// Injeção de dependências
//builder.Services.AddScoped<ISaleRepository, SaleRepository>();
//builder.Services.AddScoped<IEventPublisher, FakeRebusPublisher>();

// Adicionar controllers
builder.Services.AddControllers();

// Configurar Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
