using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SieMarket.Classes
{
    internal class Customer( List<Order> orders, string name, int CustomerID )
    {
        public List<Order> Orders { get; set; } = orders;
        public string Name { get; set; } = name;
        public int CustomerID { get; set; } = CustomerID;

    }
}
