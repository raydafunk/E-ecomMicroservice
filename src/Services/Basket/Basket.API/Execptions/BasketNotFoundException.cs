
namespace Basket.API.Execptions
{
    public class BasketNotFoundException : NotFoundExecption
    {
        public BasketNotFoundException(string userName): base("Basket",userName) 
        {
            
        }
    }
}
