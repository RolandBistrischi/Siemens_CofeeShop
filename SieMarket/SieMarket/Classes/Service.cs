using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SieMarket.Classes
{

    internal class Service
    {
        public required List<Customer> Customers_list { get; set; }
        public List<Order> All_Orders { get; set; }

        public Service( List<Customer> customers, List<Order> all_orders )
        {
            this.Customers_list = customers;
            this.All_Orders = all_orders;
        }

        public string Customer_who_has_spent_the_most_money_on_all_their_orders()
        {
            string name_of_customer = "";
            decimal max_spending = -1;
            foreach ( Customer customer in Customers_list )
            {
                decimal amount = 0;
                foreach ( Order order in customer.Orders )
                {
                    amount = amount + order.Calculate_Total_Cost();
                }
                if ( amount > max_spending )
                {
                    name_of_customer = customer.Name;
                    max_spending = amount;
                }
            }

            return name_of_customer;
        }

        public Dictionary<string, int> Top_most_selling_Products( int amount )
        {

            Dictionary<string, int> top_products = new Dictionary<string, int>();


            foreach ( Order order in All_Orders )
            {
                foreach ( Item item in order.item_list )
                {
                    if ( top_products.ContainsKey(item.product.name) )
                    {
                        top_products[item.product.name] += item.quantity_needed;
                    }
                    else
                    {
                        top_products.Add(item.product.name, item.quantity_needed);
                    }
                }
            }
            // Source - https://stackoverflow.com/a/298
            // Posted by Leon Bambrick, modified by community. See post 'Timeline' for change history
            // Retrieved 2026-02-28, License - CC BY-SA 3.0

            var myList = top_products.ToList();

            myList.Sort(( pair1, pair2 ) => pair2.Value.CompareTo(pair1.Value));

            return myList.Take(amount).ToDictionary(key => key.Key, value => value.Value);

        }


    }
}
