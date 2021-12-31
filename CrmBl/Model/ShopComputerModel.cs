using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CrmBl.Model
{
    public class ShopComputerModel
    {
        private Generator generator = new Generator();
        private Random rnd = new Random();
        private bool IsWorckinig = false;
        public List<CashDesk> CashDesks { get; set; } = new List<CashDesk>();
        public List<Cart> Carts { get; set; } = new List<Cart>();
        public List<Check> Checks { get; set; } = new List<Check>();
        public List<Sell> Sells { get; set; } = new List<Sell>();
        public Queue<Seller> Sellers { get; set; } = new Queue<Seller>();
        public ShopComputerModel()
        {
            var sellers = generator.GetNewSellers(20);
            generator.GetNewProducts(1000);
            generator.GetNewCustomers(100);
            foreach (var seller in sellers)
            {
                Sellers.Enqueue(seller);
            }
            for (int i = 0; i < 3; ++i)
            {
                CashDesks.Add(new CashDesk(CashDesks.Count, Sellers.Dequeue()));
            }
        }

        public void Start()
        {
            IsWorckinig = true;
            Task.Run(() => CreateCarts(10, 1000));
            var cashDesckTasks = CashDesks.Select(t => new Task(() => CashDeskWork(t, 1000)));

            foreach (var task in cashDesckTasks)
            {
                task.Start();
            }

        }

        public void Stop()
        {
            IsWorckinig = false;
        }
        private void CashDeskWork(CashDesk desk, int sleep)
        {
            while (IsWorckinig)
            {
                if (desk.Count > 0)
                {
                    desk.Dequeue();
                }
                Thread.Sleep(sleep);
            }
        }

        private void CreateCarts(int customerCounts, int sleep)
        {
            while (IsWorckinig)
            {
                var customers = generator.GetNewCustomers(customerCounts);
               
                foreach (var customer in customers)
                {
                    var cart = new Cart(customer);
                    foreach (var product in generator.GetRandomProduct(10,30))
                    {
                        cart.Add(product);
                    }
                    var cash = CashDesks[rnd.Next(CashDesks.Count)];
                    cash.Enqueue(cart);
                }
                Thread.Sleep(sleep);
            }
        }
    }
}
