using Interview.Web.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using Sparcpoint.Inventory.Models;
using Sparcpoint.Inventory.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Interview.Web.Tests
{
    public class ProductControllerTests
    {

        [Fact]
        public async Task AddProduct_ValidProduct_Returns201()
        {
            // Arrange
            var mockProductService = new Mock<IProductService>();
            var mockLogger = new Mock<ILogger<ProductController>>();
            var controller = new ProductController(mockProductService.Object, mockLogger.Object);
            var product = new Product { Name = "Test Product" };
            // Act
            var response = await controller.AddProduct(product);
            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }
        [Fact]
        public async Task AddProduct_InvalidProduct_Returns404()
        {
            // Arrange
            var mockProductService = new Mock<IProductService>();
            mockProductService.Setup(x => x.AddProduct(It.IsAny<Product>())).Throws(new Exception());
            var mockLogger = new Mock<ILogger<ProductController>>();
            var controller = new ProductController(mockProductService.Object, mockLogger.Object);
            var product = new Product { Name = "" };
            // Act
            var response = await controller.AddProduct(product);
            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task AddProduct_ExceptionThrown_LogsErrorMessage()
        {
            // Arrange
            var mockProductService = new Mock<IProductService>();
            mockProductService.Setup(x => x.AddProduct(It.IsAny<Product>())).Throws(new Exception("Test Exception"));
            var mockLogger = new Mock<ILogger<ProductController>>();
            var controller = new ProductController(mockProductService.Object, mockLogger.Object);
            var product = new Product { Name = "Test Product" };
            // Act
            var response = await controller.AddProduct(product);
            // Assert
            mockLogger.Verify(x => x.Log(LogLevel.Error, "Test Exception"), Times.Once);
        }


    }
}
