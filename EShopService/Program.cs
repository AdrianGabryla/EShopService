using EShop.Application.Services;
using EShop.Application;
using EShop.Domain.Repositories;
using EShop.Domain.Seeders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options =>
    options.UseInMemoryDatabase("TestDatabase"));
builder.Services.AddScoped<ICreditCardService, CreditCardService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IEShopSeeder, EShopSeeder>();

builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


var app = builder.Build();


var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<IEShopSeeder>();
await seeder.Seed();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Add services to the container


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
