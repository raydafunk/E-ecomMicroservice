using CommonOperations.Execeptions;

namespace Catalog.API.Excceptions
{
    public class ProductNotFoundExpection : NotFoundExecption
    {
        public ProductNotFoundExpection(Guid Id) : base("Product", Id) 
        {
                
        }
    }
}
