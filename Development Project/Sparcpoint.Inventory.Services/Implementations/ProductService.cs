using Sparcpoint.Inventory.Models;
using Sparcpoint.Inventory.Repositories.Contracts;
using Sparcpoint.Inventory.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sparcpoint.Inventory.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new Exception("Product Repository Object Null");
        }

        public async Task<int> AddProduct(Product product)
        {

            return await _productRepository.AddProduct(product);
        }
        public async Task<IEnumerable<Product>> SearchProducts(string name)
        {
            return await _productRepository.SearchProducts(name);
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _productRepository.GetAllProducts();
        }

    }
}
