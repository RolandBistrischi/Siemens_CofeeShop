using SieMarket.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SieMarket.Tests
{
    public class Test_Order
    {
        [Fact]
        public void Calculate_Total_Cost_NoDiscount_WhenUnder500()
        {
            var items = new List<Item>
            {
                new Item(new Product("Mouse",30, 20), 1),
                new Item(new Product("Keyboard",40, 50), 1)
            };
            var order = new Order(1, items, DateTime.Now);

            // Act: Calculate price
            decimal total = order.Calculate_Total_Cost();

            // Assert: Verify the price is 70$
            Assert.Equal(70, total);
        }

        [Fact]
        public void Calculate_Total_Cost_AppliesDiscount_WhenOver500()
        {
            var items = new List<Item>
            {
                new Item(new Product("Laptop",20, 1000), 1)
            };
            var order = new Order(1, items, DateTime.Now);

            // Act
            decimal total = order.Calculate_Total_Cost();

            // Assert: 1000$ - 10% *1000$ = 900$
            Assert.Equal(900, total);
        }

        [Theory]
        [InlineData(499, 499)]
        [InlineData(500, 450)]
        [InlineData(1000, 900)]
        public void Calculate_Total_Cost_BoundaryTests( decimal rawPrice, decimal expectedPrice )
        {
            // Arrange
            var items = new List<Item>
            {
                new Item(new Product("TestItem",20, rawPrice), 1)
            };
            var order = new Order(1, items, DateTime.Now);

            // Act
            var actualPrice = order.Calculate_Total_Cost();

            // Assert
            Assert.Equal(expectedPrice, actualPrice);
        }

        [Fact]
        public void Calculate_Total_Cost_MixedItems_WithDiscount()
        {
            // Arrange: 
            var p1 = new Product("Smartphone", 10, 300);
            var p2 = new Product("Monitor", 5, 150);
            var p3 = new Product("Cabluri", 50, 10);
            var p4 = new Product("Tastatura", 20, 80);
            var p5 = new Product("Mouse", 30, 40);

            var items = new List<Item>
            {
                new Item(p1, 1), // 1 x 300 = 300
                new Item(p3, 2), // 2 x 10 = 20
                new Item(p5, 5)   // 5 x 40 = 200
            };

            // Sum: 300 + 20 + 200 = 520
            //  10% discount: 650 * 0.9 = 468

            var order = new Order(1, items, DateTime.Now);

            // Act
            decimal actualPrice = order.Calculate_Total_Cost();

            // Assert
            Assert.Equal(468, actualPrice);
        }

        [Fact]
        public void Calculate_Total_Cost_MixedItems_NODiscount()
        {
            // Arrange: 
            var p1 = new Product("Smartphone", 10, 300);
            var p2 = new Product("Monitor", 5, 150);
            var p3 = new Product("Cabluri", 50, 10);
            var p4 = new Product("Tastatura", 20, 80);
            var p5 = new Product("Mouse", 30, 40);

            var items = new List<Item>
            {
                new Item(p1, 1), // 1 x 300 = 300
                new Item(p3, 2), // 2 x 10 = 20
                new Item(p4, 2)   // 2 x 80 = 160
            };

            // Sum: 300 + 20 + 160 = 480

            var order = new Order(1, items, DateTime.Now);

            // Act
            decimal actualPrice = order.Calculate_Total_Cost();

            // Assert
            Assert.Equal(480, actualPrice);
        }
    }
}
