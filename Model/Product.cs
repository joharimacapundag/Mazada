using Mazada.Services;
using System;

namespace Mazada.Model
{
    [Table("products")]
    class Product
    {
        [Column("product_id", AutoIncrement = true, IsPrimaryKey = true)]
        public int? ProductId { get; private set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("description")]
        public string Description { get; set; }
        [Column("price")]
        public decimal Price { get; set; }
        [Column("stock")]
        public int Stock { get; set; } = 0;
        [Column("created_at", AutoIncrement = true)]
        public DateTime CreatedAt { get; private set; } = DateTime.Now;
    }
}
