using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClothesWebUI.Models
{
    public class Products
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        public string? Description { get; set; }

        [MaxLength(100)]
        public string? Brand { get; set; }

        [MaxLength(100)]
        public string? Category { get; set; }

        [MaxLength(10)]
        public string? Gender { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public int? Discount { get; set; } = 0;

        public string? Sizes { get; set; }

        public string? Colors { get; set; }

        [MaxLength(255)]
        public string? Material { get; set; }

        [Column("stock_quantity")]
        public int? StockQuantity { get; set; } = 0;

        public string? Images { get; set; } 

        [Column("is_new")]
        public bool? IsNew { get; set; } = false;

        [Column("is_featured")]
        public bool? IsFeatured { get; set; } = false;

        public double? Rating { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
