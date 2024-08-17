using Catalog.Api.Data;
using Catalog.Api.Entities;
using Catalog.Api.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Net;

namespace Catalog.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        #region Constructor

        private readonly IProductRepository _productRepository;
        private readonly ILogger<CatalogController> _logger;
        private readonly ICatalogContect _catalogContext;
        public CatalogController(IProductRepository productRepository, ILogger<CatalogController> logger,ICatalogContect catalogContect)
        {
            _logger = logger;
            _productRepository = productRepository;
            _catalogContext = catalogContect;
        }
        #endregion

        #region Get All Products
        [HttpGet("GetAllProducts")]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            try
            {
                var products = await _productRepository.GetAllProductsAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Get Product by Id
        [HttpGet("{id:Length(24)}", Name = "GetProduct")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> GetProduct(string id)
        {
            try
            {
                var product = await _productRepository.GetProduct(id);
                if (product == null)
                {
                    _logger.LogError($"Product with id: {id} is not found");
                    return NotFound();
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Get Products By Category
        [HttpGet("GetProductByCategory")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByCategory(string category)
        {
            try
            {
                var products = await _productRepository.GetProductByCategory(category);
                return Ok(products);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Create Product
        [HttpPost("CreateProduct")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            try
            {
                await _productRepository.createProductAsync(product);
                return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Update Product
        [HttpPut("UpdateProduct")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> UpdateProduct([FromBody] Product product)
        {
            try
            {
                return Ok(await _productRepository.UpdateProduct(product));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Delete Product
        [HttpDelete("{id:Length(24)}", Name = "DeleteProduct")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> DeleteProduct(string id)
        {
            try
            {
                var result = await _productRepository.DeleteProduct(id);
                if (!result)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Seed Data
        
        [HttpPost("SeedData")]
        public async Task<ActionResult> SeedDatabase()
        {
            try
            {
                // Ensure seeding is only done when necessary
                var productsExist = await _catalogContext.products.Find(p => true).AnyAsync();

                if (!productsExist)
                {
                    CatalogContextSeed.SeedData(_catalogContext.products);
                    return Ok("Database has been seeded successfully.");
                }
                else
                {
                    return Ok("Database already contains data. Seeding was skipped.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database.");
                return StatusCode(500, "Internal server error");
            }
        }
        #endregion



    }
}
