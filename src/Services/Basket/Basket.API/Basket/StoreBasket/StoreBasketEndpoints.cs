
namespace Basket.API.Basket.StoreBasket
{

    public record StoreBasketCommand(ShoppingCart Cart);

    public record StoreBasketResult(string UserName);

    public class StoreBasketEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/basket", async (StoreBasketCommand request, ISender sender) =>
            {
                var command = request.Adapt<StoreBasketCommand>();
                var result = await sender.Send(command);
                var response = result.Adapt<StoreBasketResult>();
                return Results.Created($"/basket/{response.UserName}", response);

            }).WithName("StoreBasket")
                .Produces<StoreBasketResult>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Store all basket Items")
                .WithDescription("Store all basket Items");
        }
    }
}
