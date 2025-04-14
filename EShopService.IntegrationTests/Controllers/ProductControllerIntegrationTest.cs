using EShop.Domain.Models;
using EShop.Application.Services;
using EShopService.Controllers;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net.Http.Json;
using EShop.Domain.Repositories;

namespace EShopService.IntegrationTests.Controllers;

public class ProductControllerIntegrationTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private WebApplicationFactory<Program> _factory;
    public ProductControllerIntegrationTest(WebApplicationFactory<Program> factory)
    {
        _factory = factory
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    // pobranie dotychczasowej konfiguracji bazy danych
                    var dbContextOptions = services
                        .SingleOrDefault(service => service.ServiceType == typeof(DbContextOptions<DataContext>));

                    // usunięcie dotychczasowej konfiguracji bazy danych
                    services.Remove(dbContextOptions);

                    // Stworzenie nowej bazy danych
                    services.AddDbContext<DataContext>(options => options.UseInMemoryDatabase("MyDBForTest"));

                });
            });

        _client = _factory.CreateClient();
    }
    [Fact]
    public async Task Get_ShouldReturnAllProducts()
    {
        //Arrange
        using (var scope = _factory.Services.CreateScope())
        {
            // Pobranie kontekstu bazy danych
            var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
            dbContext.Products.RemoveRange(dbContext.Products);
            // Stworzenie obiektu
            dbContext.Products.AddRange(new Product
            {
                Id = 1,
                Name = "Bombardillo Crocodillo"
            });
            // Zapisanie obiektu
            await dbContext.SaveChangesAsync();
        }
        //Act
        var response = await _client.GetAsync("/api/products");
        response.EnsureSuccessStatusCode();


        //Assert
        var products = await response.Content.ReadFromJsonAsync<List<Product>>();
        Assert.Equal(1, products?.Count);
    }
    [Fact]
    public async Task Post_AddThousandsProductsAsync_ExceptedThousandsProducts()
    {
        using (var scope = _factory.Services.CreateScope())
        {
            var start = DateTime.Now;
            var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
            dbContext.Products.RemoveRange(dbContext.Products);
            await dbContext.SaveChangesAsync();
            for (int i = 0; i < 10000; i++)
            {
                dbContext.Products.AddRange(new Product { Id = i });
                await dbContext.SaveChangesAsync();
            }
            var response = await _client.GetAsync("/api/products");
            response.EnsureSuccessStatusCode();


            //Assert
            var products = await response.Content.ReadFromJsonAsync<List<Product>>();
            Assert.Equal(10000, products?.Count);
            Console.WriteLine(DateTime.Now - start);
        }
    }
    [Fact]
    public async Task Post_AddThousandsProducts_ExceptedThousandsProducts()
    {
        using (var scope = _factory.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
            dbContext.Products.RemoveRange(dbContext.Products);
            await dbContext.SaveChangesAsync();
            for (int i = 0; i < 10000; i++)
            {
                dbContext.Products.AddRange(new Product { Id = i });
                dbContext.SaveChanges();
            }
            var response = await _client.GetAsync("/api/products");
            response.EnsureSuccessStatusCode();


            //Assert
            var products = await response.Content.ReadFromJsonAsync<List<Product>>();
            Assert.Equal(10000, products?.Count);
        }
    }
}
