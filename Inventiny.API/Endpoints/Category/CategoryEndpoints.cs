using Inventiny.DataBase.Models;
using Inventiny.Domain.Features.Category;
using Inventiny.Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace Inventiny.API.Endpoints.Category
{
    public static class CategoryEndpoints
    {
        public static void CategoryEndpoint(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/categories");

            group.MapGet("/", async ([FromServices] ICategoryService categoryService) =>
            {
                var result = await categoryService.GetAllCategories();
                return MapResult(result);
            });

            group.MapGet("/{id:int}", async (int id, [FromServices] ICategoryService categoryService) =>
            {
                var result = await categoryService.GetCategoryById(id);
                return MapResult(result);
            });

            group.MapPost("/", async ([FromBody] TblCategory category, [FromServices] ICategoryService categoryService) =>
            {
                var result = await categoryService.CreateCategory(category);
                return MapResult(result, includeMessage: true);
            });

            group.MapPut("/{id:int}", async (int id, [FromBody] TblCategory category, [FromServices] ICategoryService categoryService) =>
            {
                var result = await categoryService.UpdateCategory(id, category);
                return MapResult(result, includeMessage: true);
            });

            group.MapDelete("/{id:int}", async (int id, [FromServices] ICategoryService categoryService) =>
            {
                var result = await categoryService.DeleteCategory(id);
                return MapResult(result, includeMessage: true, messageOnly: true);
            });
        }

        private static IResult MapResult<T>(Result<T> result, bool includeMessage = false, bool messageOnly = false)
        {
            if (result.IsValidationError)
                return Results.BadRequest(new { error = result.Message });

            if (result.IsSystemError)
                return Results.BadRequest( new { error = result.Message });

            if (result.IsSuccess)
            {
                if (messageOnly)
                    return Results.Ok(new { message = result.Message });

                if (includeMessage)
                    return Results.Ok(new { message = result.Message, data = result.Data });

                return Results.Ok(result.Data);
            }

            return Results.BadRequest("Unknown error occurred.");
        }
    }
}

