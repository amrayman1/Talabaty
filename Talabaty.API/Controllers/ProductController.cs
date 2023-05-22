using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabaty.BLL.Interfaces;
using Talabaty.DAL.Entities;

namespace Talabaty.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IGenericRepository<Product> _productGenericRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandGenericRepo;
        private readonly IGenericRepository<ProductType> _productTypeGenericRepo;

        public ProductController(IProductRepository productRepository, IGenericRepository<Product> productGenericRepo,
            IGenericRepository<ProductBrand> productBrandGenericRepo, IGenericRepository<ProductType> productTypeGenericRepo)
        {
            _productRepository = productRepository;
            _productGenericRepo = productGenericRepo;
            _productBrandGenericRepo = productBrandGenericRepo;
            _productTypeGenericRepo = productTypeGenericRepo;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _productGenericRepo.GetByIdAsync(id);
            return product;
        }

        [HttpGet("products")]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var products = await _productGenericRepo.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            return Ok(await _productTypeGenericRepo.GetAllAsync());
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            return Ok(await _productBrandGenericRepo.GetAllAsync());
        }

    }
}
