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

namespace CrmUi.Forms
{
    public partial class Login : Form
    {
        public Customer customer;
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            customer = new Customer();
            customer.Name = textBox1.Text;
            DialogResult = DialogResult.OK;
        }
    }
}
