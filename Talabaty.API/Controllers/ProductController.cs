using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabaty.API.DTOS;
using Talabaty.BLL.Interfaces;
using Talabaty.BLL.Specifications;
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
        private readonly IMapper _mapper;

        public ProductController(IProductRepository productRepository, IGenericRepository<Product> productGenericRepo,
            IGenericRepository<ProductBrand> productBrandGenericRepo, IGenericRepository<ProductType> productTypeGenericRepo, IMapper mapper)
        {
            _productRepository = productRepository;
            _productGenericRepo = productGenericRepo;
            _productBrandGenericRepo = productBrandGenericRepo;
            _productTypeGenericRepo = productTypeGenericRepo;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(int id)
        {
            var spec = new ProductWithTypesAndBrandsSpecificataion(id);

            var product = await _productGenericRepo.GetEntityWithSpec(spec);

            return _mapper.Map<Product, ProductDTO>(product);
        }

        [HttpGet("products")]
        public async Task<ActionResult<List<ProductDTO>>> GetProducts()
        {
            var spec = new ProductWithTypesAndBrandsSpecificataion();

            var products = await _productGenericRepo.ListAsync(spec);

            return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDTO>>(products));
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
