using FribergRentals.Data.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace FribergRentals.Data.Models
{
    public class Order
    {
        public int Id { get; set; }

        public Car Car { get; set; }

        public User User { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Pick-Up Date")]
        public DateTime PickUpDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayName("Return Date")]
        public DateTime ReturnDate { get; set; }
        public int NumberOfDays { get { return (int)(ReturnDate - PickUpDate).TotalDays; } }
        [Column(TypeName = "decimal(18,2)")]
        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
        public decimal TotalPrice { get; set; } = 0;
        [DataType(DataType.DateTime)]
        public DateTime TimeOfOrder { get; set; }
    }
}
