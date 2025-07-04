using Inventiny.DataBase.Models;
using Inventiny.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventiny.Domain.Features.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _db;

        public CategoryService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Result<TblCategory>> CreateCategory(TblCategory category)
        {
            _db.TblCategories.Add(category);
            _db.SaveChanges();
            return Result<TblCategory>.Success(category, "Category created successfully.");
        }

        public async Task<Result<TblCategory>> UpdateCategory(int categoryId, TblCategory category)
        {
            var existingCategory = _db.TblCategories.Find(categoryId);
            if (existingCategory == null)
            {
                return Result<TblCategory>.ValidationError("Category not found.");
            }

            existingCategory.CategoryName = category.CategoryName;
            existingCategory.CategoryCode = category.CategoryCode;

            _db.SaveChanges();
            return Result<TblCategory>.Success(existingCategory, "Category updated successfully.");
        }

        
        public async Task<Result<bool?>> DeleteCategory(int categoryId)
        {
            var existingCategory = await _db.TblCategories.FindAsync(categoryId);
            if (existingCategory == null)
            {
                return Result<bool?>.ValidationError("Category not found.");
            }

            existingCategory.DeleteFlag = true;

            await _db.SaveChangesAsync();

            return Result<bool?>.Success(true, "Category deleted successfully.");
        }

            
        public async Task<Result<TblCategory>> GetCategoryById(int categoryId)
        {
            var category = _db.TblCategories.Find(categoryId);
            if (category == null)
            {
                return Result<TblCategory>.ValidationError("Category not found.");
            }

            return Result<TblCategory>.Success(category, "Category detail.");
        }

        public async Task<Result<List<TblCategory>>> GetAllCategories()
        {
            var categories = _db.TblCategories.ToList();
            if (categories == null || !categories.Any())
            {
                return Result<List<TblCategory>>.ValidationError("No categories found.");
            }


            return Result<List<TblCategory>>.Success(categories, "Categories list.");
        }

                                                                  

    }
}
