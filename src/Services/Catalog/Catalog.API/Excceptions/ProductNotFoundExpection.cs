namespace Catalog.API.Excceptions
{
    public class ProductNotFoundExpection : Exception
    {
        public ProductNotFoundExpection() : base("Product not Found!") 
        {
                
        }
    }
}
