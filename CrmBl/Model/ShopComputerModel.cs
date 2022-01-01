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
        public List<CashDesk> CashDesks { get; set; } = new List<CashDesk>();
        public List<Cart> Carts { get; set; } = new List<Cart>();
        public List<Check> Checks { get; set; } = new List<Check>();
        public List<Sell> Sells { get; set; } = new List<Sell>();
        public Queue<Seller> Sellers { get; set; } = new Queue<Seller>();
       
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        CancellationToken token;
        public int CustomerSpeed { get; set; } = 100;
        public int CashDeskSpeed { get; set; } = 100;
        public ShopComputerModel()
        {
            var sellers = generator.GetNewSellers(20);
            generator.GetNewProducts(1000);
            generator.GetNewCustomers(100);
            cancellationTokenSource=new CancellationTokenSource();  
            token=cancellationTokenSource.Token;

            foreach (var seller in sellers)
            {
                Sellers.Enqueue(seller);
            }
            for (int i = 0; i < 3; ++i)
            {
                CashDesks.Add(new CashDesk(CashDesks.Count, Sellers.Dequeue(),null));
            }
        }

        public void Start()
        {
            // IsWorckinig = true;
            //List<Task> tasks = new List<Task>(); 
            //tasks.Add(Task.Run(() => CreateCarts(10, token)));
            //tasks.AddRange(CashDesks.Select(t => new Task(() => CashDeskWork(t,token))));

            //foreach (var task in tasks)
            //{
            //   task.Start();
            //}
            Task.Run(() => CreateCarts(10, token));
            var cashDesckTasks = CashDesks.Select(t => new Task(() => CashDeskWork(t, token)));

            foreach (var task in cashDesckTasks)
            {
                task.Start();
            }
        }

        public void Stop()
        {
           // IsWorckinig = false;
            cancellationTokenSource.Cancel();
            Thread.Sleep(1000);
        }
        private void CashDeskWork(CashDesk desk,CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                if (desk.Count > 0)
                {
                    desk.Dequeue();
                    Thread.Sleep(CashDeskSpeed);
                }
               
            }
        }

        private void CreateCarts(int customerCounts,CancellationToken token)
        {
            while (!token.IsCancellationRequested)
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
                Thread.Sleep(CustomerSpeed);
            }
        }
    }
}
