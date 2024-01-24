using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FribergRentals.Data.Models
{
    public class Car
    {
        public int Id { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public string Description { get; set; }
        [Display(Name = "Availability")]
        public bool IsAvailable { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public List<string> ImageUrls { get; set; }
    }
}
