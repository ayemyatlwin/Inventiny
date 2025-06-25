using Inventiny.DataBase.Models;
using Inventiny.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventiny.Domain.Features.Products
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _db;

        public ProductService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Result<TblProduct>> CreateProduct(TblProduct product)
        {
            _db.TblProducts.Add(product);
            await _db.SaveChangesAsync();
            return Result<TblProduct>.Success(product, "Product created successfully.");
        }

        public async Task<Result<TblProduct>> UpdateProduct(int productId, TblProduct product)
        {
            var existingProduct = await _db.TblProducts.FindAsync(productId);
            if (existingProduct == null)
            {
                return Result<TblProduct>.ValidationError("Product not found.");
            }

            existingProduct.ProductName = product.ProductName;
            existingProduct.CategoryCode = product.CategoryCode;
            existingProduct.ProductPrice = product.ProductPrice;
            existingProduct.ProductQuantity = product.ProductQuantity;

            await _db.SaveChangesAsync();
            return Result<TblProduct>.Success(existingProduct, "Product updated successfully.");
        }

        public async Task<Result<TblProduct>> GetProductById(int productId)
        {
            var product = await _db.TblProducts.FindAsync(productId);
            if (product == null)
            {
                return Result<TblProduct>.ValidationError("Product not found.");
            }
            return Result<TblProduct>.Success(product, "Product detail.");
        }

        public async Task<Result<PagedResult<TblProduct>>> GetAllProducts(int pageNumber, int pageSize)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;

            var totalCount = await _db.TblProducts.CountAsync();
            var products = await _db.TblProducts
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var model = new PagedResult<TblProduct>
            {
                Data = products,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return Result<PagedResult<TblProduct>>.Success(model, "Product List.");
        }


        public async Task<Result<List<TblProduct>>> GetProductsByCategoryCode(string categoryCode)
        {
            var products = await _db.TblProducts
                .Where(p => p.CategoryCode == categoryCode)
                .ToListAsync();
            return Result<List<TblProduct>>.Success(products, "Products by Category Code.");
        }

        public async Task<Result<List<TblProduct>>> GetProductsByName(string productName)
        {
            var products = await _db.TblProducts
                .Where(p => p.ProductName.Contains(productName, StringComparison.OrdinalIgnoreCase))
                .ToListAsync();
            return Result<List<TblProduct>>.Success(products, "Products by Name.");
        }

        public async Task<Result<List<TblProduct>>> GetProductsByPriceRange(decimal minPrice, decimal maxPrice)
        {
            var products = await _db.TblProducts
                .Where(p => p.ProductPrice >= minPrice && p.ProductPrice <= maxPrice)
                .ToListAsync();
            return Result<List<TblProduct>>.Success(products, "Products by Price Range.");
        }

    }
}
