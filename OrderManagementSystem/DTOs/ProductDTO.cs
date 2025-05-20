using System.ComponentModel.DataAnnotations;

namespace OrderManagementSystem.DTOs
{
    public class ProductDTO
    {

        [Required]
        [StringLength(100)]
        public string ProductName { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public int StockQuantity { get; set; }

        [StringLength(50)]
        public string Category { get; set; }
    }
}
