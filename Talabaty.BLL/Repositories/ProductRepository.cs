﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabaty.BLL.Interfaces;
using Talabaty.DAL.Data;
using Talabaty.DAL.Entities;

namespace Talabaty.BLL.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _context;
        public ProductRepository(StoreContext context)
        {
            _context = context;
        }
        public async Task<IReadOnlyList<Product>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<IReadOnlyList<ProductBrand>> GetAllProductBrandsAsync()
        {
            return await _context.ProductBrands.ToListAsync();
        }


        public async Task<IReadOnlyList<ProductType>> GetAllProductTypesAsync()
        {
            return await _context.ProductTypes.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products
                .Include(p => p.ProductBrand)
                .Include(p => p.ProductType)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        
        public async Task<IReadOnlyList<Product>> GetProductsAsync(int id)
        {
            return await _context.Products
                .Include(p => p.ProductBrand)
                .Include(p => p.ProductType)
                .ToListAsync();
        }
    }
}
