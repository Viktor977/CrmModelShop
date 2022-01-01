using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrmBl.Model;
using CrmUi.Forms;

namespace CrmUi
{
    public partial class Main : Form
    {
        CrmContext db;
        Cart cart;
        Customer customer;
        CashDesk cashDesk;
        public Main()
        {
            InitializeComponent();
            db = new CrmContext();
            cart = new Cart(customer);
            cashDesk = new CashDesk(1, db.Sellers.FirstOrDefault(),db)
            {
                IsModel = false
            };
        }

        private void ProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var catalogProduct = new Catalog<Product>(db.Products,db);
            catalogProduct.Show();
        }

        private void SellerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var catalogSeller = new Catalog<Seller>(db.Sellers,db);
            catalogSeller.Show();

        }

        private void CustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var catalogCustomer = new Catalog<Customer>(db.Customers,db);
            catalogCustomer.Show();
        }

        private void ChekToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var catalogCheck = new Catalog<Check>(db.Checks,db);
            catalogCheck.Show();
        }

        private void СustomerAddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new CustomerForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                db.Customers.Add(form.Customer);
                db.SaveChanges();
            }
        }

        private void SellerAddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var seller = new SellerForm();
            if (seller.ShowDialog() == DialogResult.OK)
            {
                db.Sellers.Add(seller.Seller);
                db.SaveChanges();
            }
        }

        private void productAddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProductForm form = new ProductForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
              
                db.Products.Add(form.Product);
                db.SaveChanges();
            }
        }

        private void modelingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new ModelForm();
            form.Show();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                listBox1.Invoke((Action)delegate {
                    listBox1.Items.AddRange(db.Products.ToArray());
                    UpdateLists();
                });
            });
        }

        private void listBox2_DoubleClick(object sender, EventArgs e)
        {

        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if(listBox1.SelectedItem is Product product)
            {
                cart.Add(product);
                listBox2.Items.Add(product);
                UpdateLists();
            }
        }
        private void UpdateLists()
        {
            listBox2.Items.Clear();
            listBox2.Items.AddRange(cart.GetAll().ToArray());
            label4.Text=cart.Price.ToString();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var form = new Login();
            form.ShowDialog();
            if (DialogResult.OK == form.DialogResult)
            {
                var tempCustomer=db.Customers.FirstOrDefault(t=>t.Name.Equals(form.Name));
                if (tempCustomer != null)
                {
                    customer = tempCustomer;
                }
                else
                { 
                    customer = form.customer;
                    db.Customers.Add(customer);
                    db.SaveChanges();                  
                }
                cart.Customer= customer;
            }
            linkLabel1.Text = $"Hello {Name}";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (customer!= null)
            {
                cashDesk.Enqueue(cart);
               var price= cashDesk.Dequeue();
                listBox2.Items.Clear();
                cart = new Cart(customer);
                MessageBox.Show("You make purchase soccesfully" + price.ToString(),"",MessageBoxButtons.OK,MessageBoxIcon.Information) ;

            }
            else
            {
                MessageBox.Show("You need avtotization","",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }
    }
}
