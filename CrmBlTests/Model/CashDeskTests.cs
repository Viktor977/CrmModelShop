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
    public class CashDeskTests
    {
        [TestMethod()]
        public void CashDeskTest()
        {
            //arrenge
            var customer1 = new Customer()
            {
                Name = "testCustomer1",
                CustomerId = 1,
            };
            var customer2 = new Customer()
            {
                Name = "testCustomer2",
                CustomerId = 2,
            };
            var seller = new Seller()
            {
                Name = "testSeller",
                SellerId = 1,
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
            var cart1 = new Cart(customer1);
            
            cart1.Add(product1);
            cart1.Add(product1);
            cart1.Add(product2);

            var cart2 = new Cart(customer2);
            cart2.Add(product1);
            cart2.Add(product2);
            cart2.Add(product2);
              

            var cashdask = new CashDesk(1, seller,null);
            cashdask.MaxQueueLangth = 10;
            cashdask.Enqueue(cart1);
            cashdask.Enqueue(cart2);

            var cart1ExpectedResult = 400;
            var cart2ExpectedResult = 500;

            //act

            var cart1Actualresult = cashdask.Dequeue();
            var cart2Actualresult = cashdask.Dequeue();
            //assert
            Assert.AreEqual(cart1ExpectedResult,cart1Actualresult);
            Assert.AreEqual(cart2ExpectedResult,cart2Actualresult);
            Assert.AreEqual(7, product1.Count);
            Assert.AreEqual(17, product2.Count);
        }

    }
}