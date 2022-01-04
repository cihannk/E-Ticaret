using System.ComponentModel.DataAnnotations.Schema;

namespace ETicaretWebApi.Entitites
{
    public class Product : IEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public int ColorId { get; set; }
        public string ImageUrl { get; set; }
        public DateTime ListingDate { get; set; }
    }
}
