using System.ComponentModel.DataAnnotations;


namespace EShop.Domain.common
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public bool IsDelete { get; set; } = false;

    }
}
