using System.ComponentModel.DataAnnotations;

namespace NewsPortal.API.Entites
{
    public class EntityBase
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
