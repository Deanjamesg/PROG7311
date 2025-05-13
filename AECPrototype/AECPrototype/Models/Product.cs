using System.ComponentModel.DataAnnotations;

namespace AECPrototype.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public DateOnly Date { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
