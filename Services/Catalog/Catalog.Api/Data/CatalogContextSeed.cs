using Catalog.Api.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Catalog.Api.Data
{
    public class CatalogContextSeed
    {
        public static void SeedData(IMongoCollection<Product> productCollection)
        {

            if (productCollection == null)
            {
                productCollection.InsertManyAsync(GetSeedData());
            }
        }

        private static IEnumerable<Product> GetSeedData()
        {
            return new List<Product>
            {
                new Product
                {
                    Id = ObjectId.GenerateNewId().ToString(), // یا از یک شناسه خاص استفاده کنید
                    Name = "Asus Laptop",
                    Category = "Laptop",
                    Price = 200.00m,
                    Summary = "High-performance laptop for professionals.",
                    ImageFile = "asus-laptop.jpg"
                },
                new Product
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    Name = "Apple iPhone 13",
                    Category = "Smartphone",
                    Price = 999.99m,
                    Summary = "Latest iPhone model with advanced features.",
                    ImageFile = "iphone-13.jpg"
                },
                new Product
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    Name = "Samsung Galaxy S21",
                    Category = "Smartphone",
                    Price = 799.99m,
                    Summary = "Flagship smartphone with a stunning display.",
                    ImageFile = "galaxy-s21.jpg"
                },
                new Product
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    Name = "Sony WH-1000XM4",
                    Category = "Headphones",
                    Price = 349.99m,
                    Summary = "Noise-cancelling headphones with superior sound quality.",
                    ImageFile = "sony-wh-1000xm4.jpg"
                },
                new Product
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    Name = "Dell XPS 13",
                    Category = "Laptop",
                    Price = 1200.00m,
                    Summary = "Compact and powerful laptop for on-the-go professionals.",
                    ImageFile = "dell-xps-13.jpg"
                }
            };
        }
    }

}
