using EShop.Domain.Models;
using EShop.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace EShop.Domain.Seeders;


public class EShopSeeder : IEShopSeeder
{
    private readonly DataContext _context;

    public EShopSeeder(DataContext context)
    {
        _context = context;
    }
    public async Task Seed()
    {
        // Sprawdzenie czy tabela jest pusta
        if (!_context.Products.Any())
        {
            var products = new List<Product>
            {
                new Product {Id = 1, Name = "Dan", Ean = "123", Price = 10, Stock = 2, Sku = "1" },
            };

            _context.Products.AddRange(products);
            _context.SaveChanges();
        }
    }
}