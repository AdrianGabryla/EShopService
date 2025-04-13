﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EShop.Application.Services;
using EShopService.Controllers;
using EShop.Domain.Models;
using Moq;

namespace EShopService.Tests.Controllers;

public class ProductControllerTests
{
    private readonly Mock<IProductService> _mockService;
    private readonly ProductController _controller;

    public ProductControllerTests()
    {
        _mockService = new Mock<IProductService>();
        _controller = new ProductController(_mockService.Object);
    }
    [Fact]
    public async Task Get_ShouldReturnAllProducts_ReturnTrue()
    {
        // Arrange
        var products = new List<Product> { new Product(), new Product() };
        _mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(products);

        // Act
        var result = await _controller.Get();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(products, okResult.Value);
    }
    [Fact]
    public async Task Get_ReturnProductbyId_ReturnTrue()
    {
        //Arrange
        var product = new Product { Id = 1};
        _mockService.Setup(s => s.GetAsync(1)).ReturnsAsync(product);
        //Act
        var result = await _controller.Get(1);
        //Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(product, okResult.Value);
    }
    [Fact]
    public async Task Get_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        _mockService.Setup(s => s.GetAsync(It.IsAny<int>())).ReturnsAsync((Product)null);

        // Act
        var result = await _controller.Get(999);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
    [Fact]
    public async Task Post_AddProduct_ReturnNewProduct()
    {
        //Arrange
        var product = new Product();
        _mockService.Setup(s => s.Add(It.IsAny<Product>())).ReturnsAsync(product);
        //Act
        var result = await _controller.Post(product);
        //Assert
        _mockService.Verify(s => s.Add(product), Times.Once);
        Assert.IsType<OkObjectResult>(result);
    }
    [Fact]
    public async Task Put_UpdateProductById_ReturnTrue()
    {
        var product = new Product { Id = 1 };
        _mockService.Setup(s => s.Update(product)).ReturnsAsync(product);
        var result = await _controller.Put(1, product);
        _mockService.Verify(s => s.Update(product), Times.Once);
        Assert.IsType<OkObjectResult>(result);
    }
    [Fact]
    public async Task Delete_ValidId_DeletesAndUpdates()
    {
        var product = new Product { Id = 1, Deleted = false };
        _mockService.Setup(s => s.GetAsync(1)).ReturnsAsync(product);
        _mockService.Setup(s => s.Update(It.IsAny<Product>())).ReturnsAsync(product);
        var result = await _controller.Delete(1);
        _mockService.Verify(s => s.GetAsync(1), Times.Once);
        _mockService.Verify(s => s.Update(It.Is<Product>(p => p.Deleted)), Times.Once);
        Assert.IsType<OkObjectResult>(result);
    }

}
