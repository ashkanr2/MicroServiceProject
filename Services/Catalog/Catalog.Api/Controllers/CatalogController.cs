using Catalog.Api.Entities;
using Catalog.Api.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace Catalog.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        #region Constructor

        private readonly IProductRepository _productRepository;
        private readonly ILogger<CatalogController> _logger;

        public CatalogController(IProductRepository productRepository, ILogger<CatalogController> logger)
        {
            _logger = logger;
            _productRepository = productRepository;
        }
        #endregion

        #region Get product
        [HttpGet("id:Length(24)}", Name = "GetProduct")]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct(string id)
        {
            try
            {
                var products = await _productRepository.GetProduct(id);
                if (products==null)
                {
                    _logger.LogError($"peoduct with id :{id} is not found");
                    return NotFound();

                }
                return Ok(products);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Get All products
        [HttpGet()]
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

        #region Get Products By Category Name

        [HttpGet("[action}/{category}")]
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
        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> CrreateProduct([FromBody] Product product)
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
        [HttpPut]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> UpdateProduct([FromBody] Product product)
        {
            try
            {
                
                return Ok(_productRepository.UpdateProduct(product));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Delete Product
        [HttpDelete("id:Length(24)}", Name = "DeleteProduct")]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> DeleteProduct(string id)
        {
            try
            {

                return Ok(_productRepository.DeleteProduct(id));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}

