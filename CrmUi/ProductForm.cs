using CrmBl.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrmUi
{
    public partial class ProductForm : Form
    {
        public Product product;
        public ProductForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            product = new Product()
            {
                Name = textBox1.Text,
                Count=Convert.ToInt32(numericUpDown1.Value),
                Price=Convert.ToDecimal(numericUpDown2.Value)
            };
        }
    }
}
