namespace ETicaretWebApi.Entitites
{
    public class Order: IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public double Total { get; set; }
        public DateTime Date { get; set; }
        public IList<CartItem> CartItems { get; set; }
    }
}
