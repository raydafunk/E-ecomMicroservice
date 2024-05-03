namespace Basket.API.Models
{
    public class ShoppingCart
    {
        public string Username { get; set; } = default!;

        public List<ShoppingCartItem> items { get; set; } = new();

        public decimal TotalPrice => items.Sum(x=> x.Price * x.Quantity);

        public ShoppingCart(string userName)
        {
                Username = userName;
        }

        //Requried the mapping
        public ShoppingCart()
        {
                
        }
    }
}
