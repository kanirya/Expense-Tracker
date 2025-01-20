using System.ComponentModel.DataAnnotations;

namespace Expense_Tracker.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(50)]
        public string InternalReference { get; set; } // SKU

        public string Barcode { get; set; }

        [Required]
        public string ProductType { get; set; } // Stockable, Consumable, Service

        public string Category { get; set; } // Optional, can relate to another table

        [Required]
        public decimal SalesPrice { get; set; }

        public decimal CostPrice { get; set; }

        public string Taxes { get; set; } // JSON or a separate table for tax details

        public int QuantityOnHand { get; set; }

        public int ForecastedQuantity { get; set; }

        public bool TrackBySerialNumber { get; set; } // Lot/Serial Number tracking

        public decimal Weight { get; set; }

        public decimal Volume { get; set; }

        public string Vendor { get; set; } // Optional, can relate to a Vendor table

        public DateTime? WarrantyEndDate { get; set; } // Optional
    }
}

