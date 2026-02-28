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

        private decimal total_cost;

        public Order( int customerID, List<Item> item_, DateTime order_date )
        {
            this.customerID = customerID;
            this.item_list = item_;
            this.order_date = order_date;
            this.total_cost = Calculate_Total_Cost();
        }

        public decimal Calculate_Total_Cost()
        {
            decimal sum = 0;
            foreach ( var item in item_list )
            {
                sum = sum + item.unit_price * item.quantity_needed;
            }

            if ( sum >= 500 )
                sum = sum * ( decimal )0.9;

            return sum;
        }



    }
}
