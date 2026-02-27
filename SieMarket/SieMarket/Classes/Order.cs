using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SieMarket.Classes
{
    internal class Order
    {
        public int customerID { get; set; }
        public List<Item> item_list { get; set; }
        public DateTime order_date { get; set; }

        public Order( int customerID, List<Item> item_, DateTime order_date )
        {
            this.customerID = customerID;
            this.item_list = item_;
            this.order_date = order_date;
        }
    }
}
