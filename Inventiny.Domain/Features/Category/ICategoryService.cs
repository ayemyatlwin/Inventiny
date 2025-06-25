using Inventiny.DataBase.Models;
using Inventiny.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventiny.Domain.Features.Category
{
    public  interface  ICategoryService
    {
        Task<Result<TblCategory>> CreateCategory(TblCategory category);
        Task<Result<TblCategory>> UpdateCategory(int categoryId, TblCategory category);
        Task<Result<bool?>> DeleteCategory(int categoryId);
        Task<Result<TblCategory>> GetCategoryById(int categoryId);
        Task<Result<List<TblCategory>>> GetAllCategories();
    }
}
