﻿using Catalog.API.Products.GetProducts;

namespace Catalog.API.Products.GetProductById
{

    public record GetProductByIdResponse(Product Product);
    internal class GetProductByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new GetProductByIdQuery(id));
                var response = result.Adapt<GetProductByIdResponse>();
                return Results.Ok(response);
            }).WithName("GetProductByIdEndpoint")
                .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("GetProductByIdEndpoint")
                .WithDescription("GetProductByIdEndpoint");
        }
    }
}
