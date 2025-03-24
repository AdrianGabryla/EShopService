using EShop.Domain.Models;
using EShop.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace EShop.Domain.Seeders;


public class ProductsSeeder(DataContext context) : IEShopSeeder
{
    public async Task Seed()
    {
        // Sprawdzenie czy tabela jest pusta
        if (!context.Products.Any())
        {
            var products = new List<Product>
            {
                new Product {Id = 1, Name = "Dan", Ean = "123", Price = 10, Stock = 2, Sku = "1" },
            };

            context.Products.AddRange(products);
            context.SaveChanges();
        }
    }
}