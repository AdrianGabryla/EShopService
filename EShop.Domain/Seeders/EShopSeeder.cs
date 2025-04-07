using EShop.Domain.Models;
using EShop.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace EShop.Domain.Seeders;


public class EShopSeeder(DataContext context) : IEShopSeeder
{
    public async Task Seed()
    {
        // Sprawdzenie czy tabela jest pusta
        if (!context.Products.Any())
        {
            var products = new List<Product>
            {
                new Product { Name = "Cobi", Ean = "1234" },
                    new Product { Name = "Duplo", Ean = "431" },
                    new Product { Name = "Lego", Ean = "12212" }
            };

            context.Products.AddRange(products);
            context.SaveChanges();
        }
    }
}