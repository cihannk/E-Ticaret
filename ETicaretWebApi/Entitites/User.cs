namespace ETicaretWebApi.Entitites
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int AuthRoleId { get; set; }
        public DateTime SignedUpDate { get; set; }
    }
}
