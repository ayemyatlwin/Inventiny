using Inventiny.DataBase.Models;
using Inventiny.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventiny.Domain.Features.Products
{
    public interface IProductService

    {
       Task<Result<TblProduct>> CreateProduct(TblProduct product);
        Task<Result<TblProduct>> UpdateProduct(int productId, TblProduct product);
        Task<Result<TblProduct>> GetProductById(int productId);
        Task<Result<PagedResult<TblProduct>>> GetAllProducts(int pageNumber, int pageSize);
        Task<Result<List<TblProduct>>> GetProductsByCategoryCode(string categoryCode);
        Task<Result<List<TblProduct>>> GetProductsByName(string productName);
        Task<Result<List<TblProduct>>> GetProductsByPriceRange(decimal minPrice, decimal maxPrice);

    }
}
