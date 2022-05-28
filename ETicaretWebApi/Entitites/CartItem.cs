using System.Text.Json.Serialization;

namespace ETicaretWebApi.Entitites
{
    public class CartItem : IEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Amount { get; set; }
        public double UnitPrice { get;set; }
        [JsonIgnore]
        public IList<Order> Orders { get; set; }
    }
}
