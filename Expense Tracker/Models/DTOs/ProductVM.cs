using System.ComponentModel.DataAnnotations;

namespace Expense_Tracker.Models.DTOs
{
    public class ProductVM
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public int Price { get; set; }
        [Required]
        public int Quantity { get; set; }
        public IFormFile Photo { get; set; } = null!;
    }
}
