using AutoMapper;
using Moq;
using SBCW.BLL.DTOs.Requests;
using SBCW.BLL.DTOs.Responses;
using SBCW.BLL.Services.Interfaces;
using SBCW.DAL.Models;
using SBCW.DAL.Repositories.Interfaces;

namespace TestProject1.BLL.Tests;

public class ProductServiceTests
{
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly Mock<IMapper> _mockMapper;
    private readonly ProductService _productService;

    public ProductServiceTests()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _mockMapper = new Mock<IMapper>();
        _productService = new ProductService(_mockUnitOfWork.Object, _mockMapper.Object);
    }
    
    [Fact]
    public async Task GetAllProductsAsync_ShouldReturnAllProducts()
    {
        // Arrange
        var products = new List<Product> { new Product(), new Product() };
        _mockUnitOfWork.Setup(u => u._productRepository.GetAllAsync()).ReturnsAsync(products);

        var productResponses = new List<ProductResponse> { new ProductResponse(), new ProductResponse() };
        _mockMapper.Setup(m => m.Map<IEnumerable<ProductResponse>>(products)).Returns(productResponses);

        // Act
        var result = await _productService.GetAllProductsAsync();

        // Assert
        Assert.Equal(productResponses, result);
    }

    [Fact]
    public async Task GetProductByIdAsync_ShouldReturnProduct()
    {
        // Arrange
        var product = new Product();
        _mockUnitOfWork.Setup(u => u._productRepository.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(product);

        var productResponse = new ProductResponse();
        _mockMapper.Setup(m => m.Map<ProductResponse>(product)).Returns(productResponse);

        // Act
        var result = await _productService.GetProductByIdAsync(Guid.NewGuid());

        // Assert
        Assert.Equal(productResponse, result);
    }
    
    [Fact]
    public async Task AddProductAsync_ShouldAddProduct()
    {
        // Arrange
        var productRequest = new ProductRequest { 
            Name = "Test2",
            Description = "Test2",
            Type = "Film" };
        var product = new Product { 
            Id = Guid.NewGuid(),
            Name = "Test2",
            Description = "Test2",
            Type = "Film" };

        _mockMapper.Setup(m => m.Map<Product>(productRequest)).Returns(product);
        _mockUnitOfWork.Setup(u => u._productRepository.AddAsync(product)).Returns(Task.CompletedTask);
        _mockUnitOfWork.Setup(u => u.SaveChangesAsync()).Returns(Task.CompletedTask);

        // Act
        await _productService.CreateProductAsync(productRequest);

        // Assert
        _mockUnitOfWork.Verify(u => u._productRepository.AddAsync(product), Times.Once);
        _mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task DeleteProductByIdAsync_ShouldDeleteProduct()
    {
        // Arrange
        var productId = Guid.NewGuid();
        _mockUnitOfWork.Setup(u => u._productRepository.DeleteByIdAsync(productId)).Returns(Task.CompletedTask);
        _mockUnitOfWork.Setup(u => u.SaveChangesAsync()).Returns(Task.CompletedTask);

        // Act
        await _productService.DeleteProductByIdAsync(productId);

        // Assert
        _mockUnitOfWork.Verify(u => u._productRepository.DeleteByIdAsync(productId), Times.Once);
        _mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
    }
}