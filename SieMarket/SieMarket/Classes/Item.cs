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
        public int quantity_neded { get; set; }
        public decimal unit_price { get; set; }

        public Item( Product product, int quantity_neded, decimal unit_price )
        {
            this.product = product;
            this.quantity_neded = quantity_neded;
            this.unit_price = unit_price;
        }
    }
}
