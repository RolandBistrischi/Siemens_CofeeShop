using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SieMarket.Classes
{
    internal class Customer
    {
        public List<Order> Orders { get; set; }
        public string Name { get; set; }
        public int CustomerID { get; set; }

        public Customer( List<Order> orders, string name, int id )
        {
            this.Orders = orders;
            this.Name = name;
            this.CustomerID = id;
        }

    }
}
