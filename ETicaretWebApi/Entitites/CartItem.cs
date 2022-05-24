namespace ETicaretWebApi.Entitites
{
    public class CartItem : IEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public double UnitPrice { get;set; }
    }
}
