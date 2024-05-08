

namespace Basket.API.Basket.GetBasket;

public record GetBasketQuery(string UserName): IQuery<GetBasketResult>;
public record GetBasketResult(ShoppingCart Cart);

public class GetBasketQueryHandler(IBasketRepository _repository) 
    : IQueryHandler<GetBasketQuery, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
    {
        
        var basket =await _repository.GetBasket(query.UserName);

        return new GetBasketResult(basket);
    }
}
