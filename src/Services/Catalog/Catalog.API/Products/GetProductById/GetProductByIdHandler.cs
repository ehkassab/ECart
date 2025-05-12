using Catalog.API.Products.GetProducts;

namespace Catalog.API.Products.GetProductById
{

    public record GetProductByIdQuery(Guid Id) : IQuery<GetProductsByIdResult>;

    public record GetProductsByIdResult(Product Product);

    public class GetProductByIdQueryHandler (IDocumentSession session, ILogger<GetProductByIdQueryHandler> logger)
        : IQueryHandler<GetProductByIdQuery, GetProductsByIdResult>
    {
        public async Task<GetProductsByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductByIdQuery handler called with {@query}", query);


            var product = await session.LoadAsync<Product>(query.Id, cancellationToken);

            if(product is null)
            {
                throw new ProductNotFoundException();
            }

            return new GetProductsByIdResult(product);
        }
    }
}
