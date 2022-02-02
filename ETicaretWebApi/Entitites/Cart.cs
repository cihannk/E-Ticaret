namespace ETicaretWebApi.Entitites
{
    public class Cart : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        //public List<CartItem> CartItems { get; set; }
        public double CartTotal { get; set; }
    }
}
