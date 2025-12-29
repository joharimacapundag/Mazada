using Mazada.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mazada.Model
{
    [Table("orders")]
    class Order
    {
        [Column("order_id", IsPrimaryKey = true, AutoIncrement = true)]
        public int? OrderId { get; private set; }
        [Column("user_id")]
        public int UserId { get; set; }
        [Column("total_amount")]
        public decimal TotalAmount { get; set; }
        [Column("status")]
        public OrderStatus Status { get; set; } = OrderStatus.PENDING;
        [Column("created_at", AutoIncrement = true)]
        public DateTime CreatedAt { get; private set; } = DateTime.Now;

    }

    public enum OrderStatus
    {
        PENDING,
        PAID,
        SHIPPED,
        CANCELLED
    }
}
