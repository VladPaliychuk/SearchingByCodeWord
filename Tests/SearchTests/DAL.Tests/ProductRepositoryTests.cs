using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using SBCW.DAL.Data;
using SBCW.DAL.Models;
using SBCW.DAL.Repositories.Interfaces;

namespace TestProject1.DAL.Tests;

public sealed class ProductRepositoryTests
{

    DbContextOptions<SearchContext> options = new DbContextOptionsBuilder<SearchContext>()
        .UseInMemoryDatabase(databaseName: "Test")
        .Options;

    public ProductRepositoryTests()
    {
        using (var context = new SearchContext(options))
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
    }

    [Fact]
    public async Task AddProduct_And_RetrieveById()
    {
        // Arrange
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = "Test",
            Description = "Test",
            Type = "Film"
        };

        // Arrange
        var mockProductRepository = new Mock<IProductRepository>();
        mockProductRepository.Setup(m => m.GetByIdAsync(product.Id)).ReturnsAsync(product);

        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.Setup(m => m._productRepository).Returns(mockProductRepository.Object);

        mockProductRepository.Setup(m => m.AddAsync(product)).Returns(Task.CompletedTask);
        mockUnitOfWork.Setup(m => m.SaveChangesAsync()).Returns(Task.CompletedTask);
        mockProductRepository.Setup(m => m.GetByIdAsync(product.Id)).ReturnsAsync(product);

        // Act
        await mockProductRepository.Object.AddAsync(product);
        await mockUnitOfWork.Object.SaveChangesAsync();

        // Assert
        var result = await mockUnitOfWork.Object._productRepository.GetByIdAsync(product.Id);

        result.Should().NotBeNull();
        result.Name.Should().Be(product.Name);
    }

    
    [Fact]
    public async Task DeleteProduct_Then_VerifyDeletion()
    {
        // Arrange
        var mockProductRepository = new Mock<IProductRepository>();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = "Test",
            Description = "Test",
            Type = "Film"
        };

        mockProductRepository.Setup(m => m.AddAsync(product)).Returns(Task.CompletedTask);
        mockUnitOfWork.Setup(m => m.SaveChangesAsync()).Returns(Task.CompletedTask);
        mockProductRepository.Setup(m => m.DeleteByIdAsync(product.Id)).Returns(Task.CompletedTask);

        // Act
        await mockProductRepository.Object.AddAsync(product);
        await mockUnitOfWork.Object.SaveChangesAsync();
        await mockProductRepository.Object.DeleteByIdAsync(product.Id);
        await mockUnitOfWork.Object.SaveChangesAsync();

        // Assert
        var result = await mockProductRepository.Object.GetByIdAsync(product.Id);
        
        result.Should().BeNull();
        
        mockProductRepository.Verify(m => m.AddAsync(product), Times.Once());
        mockProductRepository.Verify(m => m.DeleteByIdAsync(product.Id), Times.Once());
        mockProductRepository.Verify(m => m.GetByIdAsync(product.Id), Times.Once());
        mockUnitOfWork.Verify(m => m.SaveChangesAsync(), Times.Exactly(2));
    }
    
    [Fact]
    public async Task GetAllProducts_ReturnsAllProducts()
    {
        var mockProductRepository = new Mock<IProductRepository>();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        // Arrange
        var product1 = new Product
        {
            Id = Guid.NewGuid(),
            Name = "Test",
            Description = "Test",
            Type = "Film"
        };
        var product2 = new Product
        {
            Id = Guid.NewGuid(),
            Name = "Test2",
            Description = "Test2",
            Type = "Film"
        };
        var products = new List<Product> { product1, product2 }.AsQueryable();
        
        mockProductRepository.Setup(m => m.AddAsync(product1)).Returns(Task.CompletedTask);
        mockProductRepository.Setup(m => m.AddAsync(product2)).Returns(Task.CompletedTask);
        
        mockProductRepository.Setup(m => m.GetAllAsync()).ReturnsAsync(products);
        mockUnitOfWork.Setup(m => m._productRepository).Returns(mockProductRepository.Object);
        
        // Act
        await mockProductRepository.Object.AddAsync(product1);
        await mockProductRepository.Object.AddAsync(product2);
        await mockUnitOfWork.Object.SaveChangesAsync();
        
        // Assert
        var result = await mockUnitOfWork.Object._productRepository.GetAllAsync();
        result.Should().NotBeNull();
        result.Should().HaveCount(2);
        
        mockProductRepository.Verify(m => m.AddAsync(product1), Times.Once());
        mockProductRepository.Verify(m => m.AddAsync(product2), Times.Once());
        mockUnitOfWork.Verify(m => m.SaveChangesAsync(), Times.Once());
    }
    
    /// <summary>
    /// Simulation of failure scenario
    /// </summary>
    [Fact]
    public async Task SaveChangesAsync_Fails()
    {
        var mockProductRepository = new Mock<IProductRepository>();
        var mockUnitOfWork = new Mock<IUnitOfWork>();

        // Arrange
        mockUnitOfWork.Setup(m => m.SaveChangesAsync()).ThrowsAsync(new Exception("Failed to save changes"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(async () => await mockUnitOfWork.Object.SaveChangesAsync());
    }
    
    /// <summary>
    /// The scenario where there are no products in the repository.
    /// </summary>
    [Fact]
    public async Task GetAllProducts_NoProductsInRepository()
    {
        var mockProductRepository = new Mock<IProductRepository>();
        var mockUnitOfWork = new Mock<IUnitOfWork>();

        // Arrange
        var emptyProductsList = new List<Product>().AsQueryable();
        mockProductRepository.Setup(m => m.GetAllAsync()).ReturnsAsync(emptyProductsList);
        mockUnitOfWork.Setup(m => m._productRepository).Returns(mockProductRepository.Object);

        // Act
        var result = await mockUnitOfWork.Object._productRepository.GetAllAsync();

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(0);
    }
}