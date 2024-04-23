namespace Catalog.API.Products.CreateProduct;
public record CreateProductRequest(string Name, List<string> Category, string Description, string ImageFile, decimal Price);
public record CreateProductResponse(Guid Id);
public class CreateProductEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/products",
            async (CreateProductRequest request, ISender sender) =>
            {
                var commandObject = request.Adapt<CreateProductCommand>();

                var result = await sender.Send(commandObject);

                var response = result.Adapt<CreateProductResponse>();

                return Results.Created($"/product{response.Id}", response);
            })
             .WithName("CreateProduct")
             .Produces<CreateProductResponse>(StatusCodes.Status201Created)
             .ProducesProblem(StatusCodes.Status400BadRequest)
             .WithSummary("Create Product")
             .WithDescription(" create Product");
        
    }
}
