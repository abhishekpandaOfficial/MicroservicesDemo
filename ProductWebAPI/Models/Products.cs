using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductWebAPI.Models
{
    [Table("product")]
    public class Products
    {
        [Key]
        [Column("product_id")]
        public int ProductId { get; set; }
        [Column("product_name")]
        public string? ProductName { get; set; }
        [Column("product_code")]
        public string? ProductCode { get; set; }
        [Column("product_price")]
        public decimal ProductPrice { get; set; }

    }
}
