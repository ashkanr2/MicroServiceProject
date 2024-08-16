using Catalog.Api.Entities;
using MongoDB.Driver;

namespace Catalog.Api.Data
{
    public interface ICatalogContect
    {
         
        IMongoCollection<Product> products { get; }
    }
}
