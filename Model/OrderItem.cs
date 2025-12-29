using Mazada.Services;

namespace Mazada.Model
{
    [Table("orderitems")]
    class OrderItem
    {
        [Column("order_item_id", AutoIncrement = true, IsPrimaryKey = true)]
        public int? OrderItemId { get; private set; }
        [Column("order_id")]
        public int OrderId { get; set; }
        [Column("product_id")]
        public int ProductId { get; set; }
        [Column("quantity")]
        public int Quantity { get; set; }
        [Column("price", AutoIncrement = true)]
        public decimal Price { get; set; }
    }
}
