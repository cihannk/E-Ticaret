namespace ETicaretWebApi.Entitites
{
    public class MainPageCategory : IEntity
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string DisplayName { get; set; }
        public string PathName { get; set; }
        public string ImageUrl { get; set; }
    }
}
