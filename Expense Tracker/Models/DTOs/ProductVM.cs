using System.ComponentModel.DataAnnotations;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Expense_Tracker.Models.DTOs
{
    public class ProductVM
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public int Price { get; set; }
        public int quantity { get; set; }
        [Required]

        public IFormFile Photo { get; set; } = null!;
    }
}


public abstract class global
{
    public static string name = "Danish";
}