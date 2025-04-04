﻿using EShop.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly DataContext _context;

    public ProductRepository(DataContext context)
    {
        _context = context;
    }
    public IEnumerable<Product> GetAllProducts()
    {
        return _context.Products.ToList();
    }
    public Product GetProductById(int id)
    {
        return _context.Products.Single(x => x.Id == id);
    }
    public void AddProduct(Product product)
    {
        _context.Products.Add(product);
        _context.SaveChanges();
    }
    public void UpdateProduct(Product product)
    {
        _context.Products.Update(product);
        _context.SaveChanges();
    }
    public void DeleteProduct(int id)
    {
        var product = _context.Products.Single(x => x.Id == id);
        _context.Products.Remove(product);
        _context.SaveChanges();
    }
}
