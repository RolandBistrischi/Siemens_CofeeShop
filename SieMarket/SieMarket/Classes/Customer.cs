using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SieMarket.Classes
{
    internal class Customer( List<Order> orders )
    {
        public List<Order> Orders { get; set; } = orders;
    }
}
