namespace ShopTP22.Models.ShoppingCarts
{
    public class Cart
    {
        public Guid Id { get; set; }

        public string OwnerSessionId { get; set; }
        public ICollection<CartItem> Items { get; set; }
    }
}