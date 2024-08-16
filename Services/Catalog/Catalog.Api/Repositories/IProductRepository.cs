using Catalog.Api.Entities;

namespace Catalog.Api.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product>GetProduct(string productId);
        Task<IEnumerable<Product>> GetProductByName(string name);
        Task<IEnumerable<Product>> GetProductByCategory(string category);
        Task <bool>UpdateProduct(Product product);
        Task<bool> DeleteProduct(string Id);
        Task <bool>createProductAsync(Product product); 
    }
}
