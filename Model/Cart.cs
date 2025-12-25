using Mazada.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mazada.Model
{
    [Table("cart")]
    class Cart
    {
        [Column("cart_id", AutoIncrement = true, IsPrimaryKey = true)]
        public int? CartId { get; private set; }
        [Column("user_id")]
        public int UserId { get; set; }
        [Column("product_id")]
        public int ProductId { get; set; }
        [Column("quantity")]
        public int Quantity { get; set; } = 1;
        [Column("created_at", AutoIncrement = true)]
        public DateTime CreatedAt { get; private set; } = DateTime.Now;
    }
}
