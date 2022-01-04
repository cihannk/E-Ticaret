namespace ETicaretWebApi.Entitites
{
    public class Slider : IEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
    }
}
