using SieMarket.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SieMarket.Tests
{
    public class Test_Service
    {

        [Fact]
        public void GetTopSpender_ShouldReturnCorrectCustomer_WhenMultipleCustomersExist()
        {
            // Arrange
            var product = new Product("Laptop", 10, 1000);

            // Customer 1
            var order1 = new Order(1, new List<Item> { new Item(product, 1) }, DateTime.Now);
            var customer1 = new Customer([order1], "Andrei", 1);

            // Customer 2
            var cheapProduct = new Product("Mouse", 20, 200);
            var order2 = new Order(2, new List<Item> { new Item(cheapProduct, 1) }, DateTime.Now);
            var order3 = new Order(2, new List<Item> { new Item(cheapProduct, 1) }, DateTime.Now);
            var customer2 = new Customer(new List<Order> { order2, order3 }, "Maria", 2);



            var customers = new List<Customer> { customer1, customer2 };
            var service = new Service(customers, new List<Order>());

            // Act
            string topSpender = service.Customer_who_has_spent_the_most_money_on_all_their_orders();

            // Assert
            Assert.Equal("Andrei", topSpender); // 900 > 400
        }


        [Fact]
        public void Customer_WhoSpentMost_ShouldCalculateComplexOrdersCorrectly()
        {
            // --- ARRANGE ---
            var laptop = new Product("Laptop", 10, 1000);
            var mouse = new Product("Mouse", 50, 20);
            var cablu = new Product("Cablu", 100, 5);

            // Customer 1: Andrei 
            // 1 x 1000€ = 1000€ -> Discount 10% -> Total: 900€
            var itemsAndrei = new List<Item> { new Item(laptop, 1) };
            var orderAndrei = new Order(1, itemsAndrei, DateTime.Now);
            var customer1 = new Customer(new List<Order> { orderAndrei }, "Andrei", 1);

            // Customer 2: Maria 
            // Order A: 20 x 20€ (Mouse) = 400€ (NO discount)
            // Order B: 20 x 5€ (Cablu) = 100€ (NO discount)
            // Total Maria: 400 + 100 = 500€
            var itemsMariaA = new List<Item> { new Item(mouse, 20) };
            var itemsMariaB = new List<Item> { new Item(cablu, 20) };

            var orderMariaA = new Order(2, itemsMariaA, DateTime.Now);
            var orderMariaB = new Order(2, itemsMariaB, DateTime.Now);

            var customer2 = new Customer(new List<Order> { orderMariaA, orderMariaB }, "Maria", 2);

            var customers = new List<Customer> { customer1, customer2 };
            var service = new Service(customers, new List<Order>());

            // --- ACT ---
            string winner = service.Customer_who_has_spent_the_most_money_on_all_their_orders();

            // --- ASSERT ---
            // Andrei (900€) vs Maria (500€)
            Assert.Equal("Andrei", winner);
        }


        [Fact]
        public void Customer_WhoSpentMost_MariaCloseSecondScenario()
        {
            // --- ARRANGE ---
            var laptop = new Product("Laptop", 10, 1000);
            var mouse = new Product("Mouse", 50, 20);
            var cablu = new Product("Cablu", 100, 5);

            // Customer 1
            // 1 x 1000€ = 1000€ -> Discount 10% -> Total: 900€
            var itemsAndrei = new List<Item> { new Item(laptop, 1) };
            var orderAndrei = new Order(1, itemsAndrei, DateTime.Now);
            var customer1 = new Customer(new List<Order> { orderAndrei }, "Andrei", 1);

            // Customer 2: Maria 
            // Order A: 20 x 20€ (Mouse) = 400€ (NO discount)
            // Order B: 100 x 5€ (Cablu) = 500€ (WITH discount) -> 500 * 0.9 = 450€
            // Total Maria: 400 + 450 = 850€
            var itemsMariaA = new List<Item> { new Item(mouse, 20) };
            var itemsMariaB = new List<Item> { new Item(cablu, 100) };

            var orderMariaA = new Order(2, itemsMariaA, DateTime.Now);
            var orderMariaB = new Order(2, itemsMariaB, DateTime.Now);

            var customer2 = new Customer(new List<Order> { orderMariaA, orderMariaB }, "Maria", 2);

            var customers = new List<Customer> { customer1, customer2 };
            var service = new Service(customers, new List<Order>());

            // --- ACT ---
            string winner = service.Customer_who_has_spent_the_most_money_on_all_their_orders();

            // --- ASSERT ---
            // Andrei (900€) vs Maria (850€)
            Assert.Equal("Andrei", winner);
        }

        [Fact]
        public void Customer_WhoSpentMost_ShouldReturnEmpty_WhenDataIsEmpty()
        {
            // --- Scenario 1: Empty List
            var emptyCustomers = new List<Customer>();
            var serviceEmpty = new Service(emptyCustomers, new List<Order>());

            // Act
            string result1 = serviceEmpty.Customer_who_has_spent_the_most_money_on_all_their_orders();

            // Assert
            Assert.Equal("", result1);

            // --- Scenario 2: Order List Empty
            var customerNoOrders = new Customer(new List<Order>(), "Gigel", 3);
            var serviceNoOrders = new Service(new List<Customer> { customerNoOrders }, new List<Order>());

            // Act
            string result2 = serviceNoOrders.Customer_who_has_spent_the_most_money_on_all_their_orders();

            // Assert
            Assert.Equal("Gigel", result2);
        }

        // test for top selling products

        [Fact]
        public void TopSelling_ShouldReturnEmpty_WhenAllOrdersIsEmpty()
        {
            // Arrange
            var service = new Service(new List<Customer>(), new List<Order>());

            // Act
            var result = service.Top_most_selling_Products(5);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void TopSelling_ShouldReturnEmpty_WhenItemsInOrderAreEmpty()
        {
            // Arrange
            var order = new Order(1, new List<Item>(), DateTime.Now);
            var service = new Service(new List<Customer>(), new List<Order> { order });

            // Act
            var result = service.Top_most_selling_Products(5);

            // Assert
            Assert.Empty(result);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void TopSelling_ShouldReturnEmpty_WhenAmountIsZeroOrNegative( int amount )
        {
            // Arrange
            var p1 = new Product("Laptop", 10, 1000);
            var items = new List<Item> { new Item(p1, 5) };
            var order = new Order(1, items, DateTime.Now);
            var service = new Service(new List<Customer>(), new List<Order> { order });

            // Act
            var result = service.Top_most_selling_Products(amount);

            // Assert
            Assert.Empty(result);
        }

        // --- TESTE NORMALE ---

        [Fact]
        public void TopSelling_ShouldReturnCorrectOrder_WhenDataIsNormal()
        {
            // Arrange
            var p1 = new Product("Mouse", 100, 20);
            var p2 = new Product("Laptop", 10, 1000);
            var p3 = new Product("Tastatura", 50, 50);

            // Laptop : 2 buc
            // Mouse : 10 buc(most wanted)
            // Tastatura : 5 buc
            var items = new List<Item>
            {
                new Item(p1, 10),
                new Item(p2, 2),
                new Item(p3, 5)
            };
            var order = new Order(1, items, DateTime.Now);
            var service = new Service(new List<Customer>(), new List<Order> { order });

            // Act: Top 2
            var result = service.Top_most_selling_Products(2);

            // Assert
            Assert.Equal(2, result.Count);

            var expectedKeys = new List<string> { "Mouse", "Tastatura" };
            var actualKeys = result.Keys.ToList();

            Assert.Equal(expectedKeys, actualKeys);


            var resultList = result.ToList();

            Assert.Equal("Mouse", resultList[0].Key);
            Assert.Equal(10, resultList[0].Value);

            Assert.Equal("Tastatura", resultList[1].Key);
            Assert.Equal(5, resultList[1].Value);
        }

        [Fact]
        public void TopSelling_ShouldAggregateQuantities_AcrossMultipleOrders()
        {
            // Arrange
            var p1 = new Product("Laptop", 10, 1000);

            var order1 = new Order(1, new List<Item> { new Item(p1, 3) }, DateTime.Now);
            var order2 = new Order(2, new List<Item> { new Item(p1, 4) }, DateTime.Now);

            var service = new Service(new List<Customer>(), new List<Order> { order1, order2 });

            // Act
            var result = service.Top_most_selling_Products(1);

            // Assert
            Assert.Equal(7, result["Laptop"]); // 3 + 4 = 7
        }

        [Fact]
        public void TopSelling_ShouldNotCrash_WhenAmountIsLargerThanProductCount()
        {
            // --- ARRANGE ---
            var p1 = new Product("Laptop", 10, 1000);
            var p2 = new Product("Mouse", 50, 20);
            var p3 = new Product("Tastatura", 30, 50);

            var allOrders = new List<Order>();
            for ( int i = 0; i < 3; i++ )
            {
                var items = new List<Item>
                    {
                        new Item(p1, 1),
                        new Item(p2, 1),
                        new Item(p3, 1)
                    };
                allOrders.Add(new Order(i, items, DateTime.Now));
            }

            var service = new Service(new List<Customer>(), allOrders);

            // --- ACT ---
            int excessiveAmount = int.MaxValue; //big number
            var result = service.Top_most_selling_Products(excessiveAmount);

            // --- ASSERT ---
            // 1. No Error?
            // 2. Exist 3 products?
            Assert.Equal(3, result.Count);

            // 3. Correct products?
            Assert.Contains("Laptop", result.Keys);
            Assert.Contains("Mouse", result.Keys);
            Assert.Contains("Tastatura", result.Keys);
        }


    }
}
