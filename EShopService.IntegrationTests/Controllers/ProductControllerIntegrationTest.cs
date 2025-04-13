using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using EShop.Domain.Repositories;
using EShop.Domain.Models;
using System.Net.Http.Json;

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
                        .SingleOrDefault(service => service.ServiceType == typeof(DbContextOptions<DbContext>));

                    // usunięcie dotychczasowej konfiguracji bazy danych
                    services.Remove(dbContextOptions);

                    // Stworzenie nowej bazy danych
                    services.AddDbContext<DbContext>(options => options.UseInMemoryDatabase("MyDBForTest"));

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
            var dbContext = scope.ServiceProvider.GetRequiredService<DbContext>();
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
}
