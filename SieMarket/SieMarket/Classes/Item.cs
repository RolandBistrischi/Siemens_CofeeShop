using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SieMarket.Classes
{
    internal class Item
    {
        public Product product { get; set; }
        public int quantity_needed { get; set; }
        public decimal unit_price { get; set; }

        public Item( Product product, int quantity_neded)
        {
            this.product = product;
            this.quantity_needed = quantity_neded;
            this.unit_price = product.price;
        }
    }
}
