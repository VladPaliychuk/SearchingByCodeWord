using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SBCW.API.Controllers;
using SBCW.BLL.DTOs.Requests;
using SBCW.BLL.DTOs.Responses;
using SBCW.BLL.Services.Interfaces;

namespace TestProject1.API.Tests;

public class ProductControllerTests
{
    private readonly Mock<IProductService> _mockProductService;
    private readonly Mock<ILogger<ProductController>> _mockLogger;
    private readonly ProductController _productsController;

    public ProductControllerTests()
    {
        _mockProductService = new Mock<IProductService>();
        _mockLogger = new Mock<ILogger<ProductController>>();
        _productsController = new ProductController(_mockProductService.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task GetAllProductsAsync_ShouldReturnAllProducts()
    {
        // Arrange
        var productResponses = new List<ProductResponse> { new ProductResponse(), new ProductResponse() };
        _mockProductService.Setup(s => s.GetAllProductsAsync()).ReturnsAsync(productResponses);

        // Act
        var result = await _productsController.GetAllProductsAsync();

        // Assert
        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        var model = okResult.Value.Should().BeAssignableTo<IEnumerable<ProductResponse>>().Subject;

        model.Should().BeEquivalentTo(productResponses);
    }

    [Fact]
    public async Task GetProductByIdAsync_ShouldReturnProduct()
    {
        // Arrange
        var productResponse = new ProductResponse();
        _mockProductService.Setup(s => s.GetProductByIdAsync(It.IsAny<Guid>())).ReturnsAsync(productResponse);

        // Act
        var result = await _productsController.GetProductByIdAsync(Guid.NewGuid());

        // Assert
        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        var model = okResult.Value.Should().BeAssignableTo<ProductResponse>().Subject;

        model.Should().BeEquivalentTo(productResponse);
    }
    
    [Fact]
    public async Task GetAllProductsAsync_ShouldLogInformation()
    {
        // Arrange
        var productResponses = new List<ProductResponse> { new ProductResponse(), new ProductResponse() };
        _mockProductService.Setup(s => s.GetAllProductsAsync()).ReturnsAsync(productResponses);

        // Act
        var result = await _productsController.GetAllProductsAsync();

        // Assert
        _mockLogger.Verify(
            x => x.Log(
                It.Is<LogLevel>(l => l == LogLevel.Information),
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Retrieved")),
                It.IsAny<Exception>(),
                It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)),
            Times.Once);
    }
    
    [Fact]
    public async Task DeleteProductAsync_ShouldReturnNoContentResponse()
    {
        // Arrange
        var productId = Guid.NewGuid();

        // Act
        var result = await _productsController.DeleteProductByIdAsync(productId);

        // Assert
        result.Should().BeOfType<NoContentResult>();
    }

}