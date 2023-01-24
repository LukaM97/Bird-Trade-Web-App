using System.ComponentModel.DataAnnotations;

namespace NewBirdApp.Models.DTO
{
    public class OrderNameFilterDTO
    {
        [Required]
        public string NameOfCustomer { get; set; }
    }
}
