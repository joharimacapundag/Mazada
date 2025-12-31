using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mazada.Model
{
    class CartNavArgs
    {
        public int UserId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
