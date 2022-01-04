namespace ETicaretWebApi.Entitites
{
    public class CartCartItem : IEntity
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public int CartItemId { get; set; }
    }
}
