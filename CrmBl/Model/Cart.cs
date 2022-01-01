﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrmBl.Model
{
    public class Cart : IEnumerable
    {
        public Customer Customer { get; set; }
        public Dictionary<Product,int> Products { get; set; }
        public decimal Price =>GetAll().Sum(t=>t.Price);
        public Cart (Customer customer)
        {
            Customer= customer;
            Products= new Dictionary<Product,int>();    
        }
        public void Add(Product product)
        {
            if(Products.TryGetValue(product, out int value))
            {
                Products[product] = ++value;
            }
            else
            {
                Products.Add(product, 1);
            }
        }

        public IEnumerator GetEnumerator()
        {
            foreach (var product in Products.Keys)
            {
                for (int i = 0; i < Products[product]; i++)
                {
                    yield return product;
                }
            }
        }

        public List<Product> GetAll()
        {
            var result=new List<Product>();
            foreach (Product item in this)
            {
                result.Add(item);
            }

            return result;
        }
    }
}
