using Dapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sparcpoint.Inventory.Models;
using Sparcpoint.Inventory.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Sparcpoint.Inventory.Repositories.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private const string procAddProduct = "dbo.usp_AddProduct";

       
        private const string procSearchProduct = "dbo.usp_SearchProduct";

        private const string procGetAllProducts = "dbo.usp_GetProducts";

        private readonly ILogger<ProductRepository> _logger;
        private readonly IOptions<ConnConfig> _connConfig;

        public ProductRepository(ILogger<ProductRepository> logger, IOptions<ConnConfig> connConfig)
        {
            _logger = logger;
            _connConfig = connConfig;
        }

        public async Task<int> AddProduct(Product product)
        {
            try
            {
                string connStr = _connConfig.Value.DataConnection;
                using (IDbConnection db = new SqlConnection(connStr))
                {
                    var queryParams = new DynamicParameters();
                    queryParams.Add("@AddedProductId", DbType.Int32, direction: ParameterDirection.Output);
                    queryParams.Add("@Name", product.Name);
                    queryParams.Add("@Description", product.Description);
                    queryParams.Add("@ProductImageUris", product.ProductImageUris);
                    queryParams.Add("@ValidSkus", product.ValidSkus);
                    queryParams.Add("@CreatedTimestamp", DateTime.Now);

                    var result = await db.ExecuteScalarAsync<int>(procAddProduct, queryParams, commandType: CommandType.StoredProcedure);

                    return queryParams.Get<int>("WaterLogID");
                }
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<Product>> SearchProducts(string name)
        {
            try
            {
                string connStr = _connConfig.Value.DataConnection;
                using (IDbConnection db = new SqlConnection(connStr))
                {
                    var queryParams = new DynamicParameters();
                    queryParams.Add("@name", name);

                    return await db.QueryAsync<Product>(procSearchProduct, queryParams, commandType: CommandType.StoredProcedure);


                }
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            try
            {
                string connStr = _connConfig.Value.DataConnection;
                using (IDbConnection db = new SqlConnection(connStr))
                {
                    
                    return await db.QueryAsync<Product>(procGetAllProducts, null, commandType: CommandType.StoredProcedure);


                }
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                throw;
            }
        }

    }
}
