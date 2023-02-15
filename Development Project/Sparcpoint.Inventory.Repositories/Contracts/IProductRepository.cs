using Sparcpoint.Inventory.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sparcpoint.Inventory.Repositories.Contracts
{
    public interface IProductRepository
    {
        Task<int> AddProduct(Product product);
        Task<IEnumerable<Product>> SearchProducts(string name);

        Task<IEnumerable<Product>> GetAllProducts();
    }
}
