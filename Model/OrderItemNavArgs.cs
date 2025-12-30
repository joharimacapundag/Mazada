namespace Mazada.Model
{
    class OrderItemNavArgs
    {
        public int UserId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public decimal TotalPrice
        {
            get => Quantity * Product.Price; 
            private set { }
        }

    }
}
