﻿using EShop.Domain.Models;
using EShop.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.Services;

public class ProductService : IProductService
{
    private IRepository _repository;
    public ProductService(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Product>> GetAllAsync()
    {
        var result = await _repository.GetAllProductAsync();

        return result;
    }

    public async Task<Product> GetAsync(int id)
    {
        var result = await _repository.GetProductAsync(id);

        return result;
    }

    public async Task<Product> Update(Product product)
    {
        var result = await _repository.UpdateProductAsync(product);

        return result;
    }

    public async Task<Product> Add(Product product)
    {
        var result = await _repository.AddProductAsync(product);

        return result;
    }
}
