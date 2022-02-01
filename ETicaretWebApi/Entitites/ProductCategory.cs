using System.ComponentModel.DataAnnotations.Schema;

namespace ETicaretWebApi.Entitites
{
    public class ProductCategory : IEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
    }
}
