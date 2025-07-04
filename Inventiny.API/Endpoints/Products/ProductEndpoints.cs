using Inventiny.DataBase.Models;
using Inventiny.Domain.Features.Products;
using Inventiny.Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace Inventiny.API.Endpoints.Products
{
    public static class ProductEndpoints
    {
        public static void ProductEndpoint(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/products");

            group.MapPost("/list", async (
             [FromBody] PaginationRequest pagination,
             [FromServices] IProductService productService) =>
            {
                var result = await productService.GetAllProducts(pagination.PageNumber, pagination.PageSize);
                return MapResult(result);
            });

            group.MapGet("/{id:int}", async (int id, [FromServices] IProductService productService) =>
            {
                var result = await productService.GetProductById(id);
                return MapResult(result);
            });

            group.MapPost("/", async ([FromBody] TblProduct product, [FromServices] IProductService productService) =>
            {
                var result = await productService.CreateProduct(product);
                return MapResult(result, includeMessage: true);
            });

            group.MapPut("/{id:int}", async (int id, [FromBody] TblProduct product, [FromServices] IProductService productService) =>
            {
                var result = await productService.UpdateProduct(id, product);
                return MapResult(result, includeMessage: true);
            });

            group.MapGet("/by-category/{categoryCode}", async (string categoryCode, [FromServices] IProductService productService) =>
            {
                var result = await productService.GetProductsByCategoryCode(categoryCode);
                return MapResult(result);
            });

            group.MapGet("/search", async ([FromQuery] string name, [FromServices] IProductService productService) =>
            {
                var result = await productService.GetProductsByName(name);
                return MapResult(result);
            });
        }


        public class PaginationRequest
        {
            public int PageNumber { get; set; } = 1;
            public int PageSize { get; set; } = 10;
        }
        private static IResult MapResult<T>(Result<T> result, bool includeMessage = false)
        {
            if (result.IsValidationError)
                return Results.BadRequest(new { error = result.Message });

            if (result.IsSystemError)
                return Results.BadRequest( new { error = result.Message });

            if (result.IsSuccess)
            {
                if (includeMessage)
                    return Results.Ok(new { message = result.Message, data = result.Data });
                else
                    return Results.Ok(result.Data);
            }

            return Results.BadRequest("Unknown error occurred.");
        }
    }
}
