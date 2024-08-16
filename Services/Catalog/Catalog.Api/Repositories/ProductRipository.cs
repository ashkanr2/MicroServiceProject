using Catalog.Api.Data;
using Catalog.Api.Entities;
using MongoDB.Driver;
using System.Xml.Linq;

namespace Catalog.Api.Repositories
{
    public class ProductRipository : IProductRepository
    {
        #region constructor
        private readonly ICatalogContect _context;
        public ProductRipository(ICatalogContect catalogContect)
        {
            _context = catalogContect;
        }
        
        #endregion

        #region RroductRepo
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            try
            {
                return await _context.products.Find(p => true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<Product> GetProduct(string productId)
        {
            try
            {
                return await _context.products.Find(p => p.Id==productId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(string category)
        {
            try
            {
                FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Category,category);
                return await _context.products.Find(filter).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            try
            {
                FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Name, name);
                return await _context.products.Find(filter).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<bool> createProductAsync(Product product)
        {
            try
            {
                await _context.products.InsertOneAsync(product);
                return true;
            }
            catch (Exception ex)
            {
 
                throw ex;
            }
        }
        public async Task<bool> UpdateProduct(Product product)
        {
            try
            {
                var updateResult = await _context.products.ReplaceOneAsync(
                    filter: p => p.Id == product.Id,
                    replacement: product
                );

            
                return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> DeleteProduct(string Id)
        {
            try
            {
                var deleteResult = await _context.products.DeleteOneAsync(p => p.Id == Id);
                return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
