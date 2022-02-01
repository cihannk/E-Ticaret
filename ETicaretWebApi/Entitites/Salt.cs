namespace ETicaretWebApi.Entitites
{
    public class Salt : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string SaltString { get; set; }
    }
}
