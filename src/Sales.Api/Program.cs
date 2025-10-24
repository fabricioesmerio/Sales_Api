using MediatR;
using Microsoft.EntityFrameworkCore;
using Sales.Application.Commands;
using Sales.Application.Dto;
using Sales.Application.Interfaces;
using Sales.Application.Messaging;
using Sales.Infrastructure.Persistence;
using Sales.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
  options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ISaleRepository, SaleRepository>();
builder.Services.AddScoped<IEventPublisher, FakeRebusPublisher>();
builder.Services.AddMediatR(typeof(CreateSaleCommand).Assembly);
builder.Services.AddAutoMapper(typeof(SaleDto).Assembly);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
