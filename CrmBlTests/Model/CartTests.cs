using Microsoft.VisualStudio.TestTools.UnitTesting;
using CrmBl.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrmBl.Model.Tests
{
    [TestClass()]
    public class CartTests
    {
        [TestMethod()]
        public void CartTest()
        {
            //arrenge
            var customer = new Customer()
            {
                CustomerId = 1,
                Name = "testUser"
            };
            var product1 = new Product()
            {
                ProductId = 1,
                Name = "product1",
                Price = 100,
                Count = 10
            };
            var product2 = new Product()
            {
                ProductId = 2,
                Name = "product2",
                Price = 200,
                Count = 20
            };
            var cart = new Cart(customer);

            var expectedResult = new List<Product>();
            expectedResult.Add(product1);
            expectedResult.Add(product1);
            expectedResult.Add(product2);
         
            //act

            cart.Add(product1);
            cart.Add(product1);
            cart.Add(product2);

            //assert

            var cartResult=cart.GetAll();
            Assert.AreEqual(expectedResult.Count,cartResult.Count);
            for(int i = 0; i < expectedResult.Count; i++)
            {
                Assert.AreEqual(expectedResult[i], cartResult[i]);
            }
        }
    }
}