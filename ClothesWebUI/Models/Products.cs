using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClothesWebUI.Models
{
    public class Products
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductID { get; set; }

        [Required]
        [Column("ProductName")]
        public string ProductName { get; set; } = string.Empty;

        [Required]
        [Column("Description")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Column("Price")]
        public decimal Price { get; set; }

        [Required]
        [Column("Quantity")]
        public int Quantity { get; set; }
    }
}