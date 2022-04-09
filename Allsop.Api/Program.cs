using System.Reflection;
using Allsop.Api;
using Allsop.DataAccess;
using Allsop.Domain.Handlers;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCommonServices();
builder.Services.AddDbContext<ProductCatalogDbContext>(opt=>opt.UseInMemoryDatabase(nameof(ProductCatalogDbContext)));
builder.Services.AddDbContext<PriceCalculationDbContext>(opt=>opt.UseInMemoryDatabase(nameof(PriceCalculationDbContext)));
builder.Services.AddDbContext<ShoppingCartDbContext>(opt=>opt.UseInMemoryDatabase(nameof(ShoppingCartDbContext)));
builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddMappers();
builder.Services.AddMediatR(typeof(GetShoppingCartByUserIdHandler).GetTypeInfo().Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
