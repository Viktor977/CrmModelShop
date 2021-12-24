using CrmBl.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrmUi
{
    public partial class Catalog <T>: Form 
        where T: class 
    {
       // DbContext db;
       CrmContext db;
        DbSet<T> set;
        public Catalog(DbSet<T>set,CrmContext db)
        {
            InitializeComponent();
            this.db = db;
            this.set = set;
            set.Load();
            dataGridView.DataSource = set.Local.ToBindingList();
        }
       

        private void Catalog_Load(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (typeof(T) == typeof(Product))
            {
 
            }
            else if (typeof(T) == typeof(Customer))
            {
                var customer = new CustomerForm();
                if (customer.ShowDialog() == DialogResult.OK)
                {
                    db.Customers.Add(customer.Customer);
                    db.SaveChanges();
                }
            }
            else if (typeof(T) == typeof(Seller))
            {
                 
            }
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            var id = dataGridView.SelectedRows[0].Cells[0].Value;
            if (typeof(T) == typeof(Product))
            {
                var product = set.Find(id) as Product;
                if (product != null)
                {
                    var form = new ProductForm(product);
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        product = form.Product;

                        dataGridView.Update();
                        db.SaveChanges();
                    }
                }


            }
        }

       
    }
}
