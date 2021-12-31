using CrmBl.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrmUi
{
    internal class CashBoxView
    {
        CashDesk cashDask;
        public Label CahdDeskname { get; set; }
        public Label LeaveCustomerCount { get; set; }
        public NumericUpDown Price { get; set; }  
       public ProgressBar QueueLenght { get; set; }
        public CashBoxView(CashDesk cashDesk,int number,int x,int y)
        {
            cashDask = cashDesk;
            CahdDeskname= new Label();
            Price = new NumericUpDown();

            CahdDeskname.AutoSize=true;
            CahdDeskname.Location=new System.Drawing.Point(x, y);
            CahdDeskname.Name = "label" + number;
            CahdDeskname.Size=new System.Drawing.Size(35, 15);
            CahdDeskname.TabIndex = number;
            cashDask.Number = number;
            CahdDeskname.Text=cashDask.ToString();

            //numeric UpDown

            Price.Location=new System.Drawing.Point(x + 60,y);
            Price.Name = "numericUpDown"+ number;
            Price.Size = new System.Drawing.Size(120,20);
            Price.TabIndex = number;       
            Price.Maximum = 10000000000;

            //ProgressBar
            QueueLenght = new ProgressBar();
            QueueLenght.Location = new System.Drawing.Point(x+300, y);
            QueueLenght.Maximum = cashDask.MaxQueueLangth;
            QueueLenght.Name = "progressbar"+number;
            QueueLenght.Size = new System.Drawing.Size(100, 20);
            QueueLenght.TabIndex = number;
            QueueLenght.Value = 1;

            //LeaveCustomerCount
            LeaveCustomerCount = new Label();
            LeaveCustomerCount.AutoSize = true;
            LeaveCustomerCount.Location = new System.Drawing.Point(x+400,y);
            LeaveCustomerCount.Name = "label2"+number;
            LeaveCustomerCount.Size = new System.Drawing.Size(35, 15);
            LeaveCustomerCount.TabIndex = number;
            LeaveCustomerCount.Text = "";
           
             cashDask.CheckClosed += CashDask_CheckClosed;
        }

        private void CashDask_CheckClosed(object sender, Check e)
        {
            Price.Invoke((Action)delegate 
            { 
                Price.Value += e.Price;
                QueueLenght.Value =cashDask.Number;
                LeaveCustomerCount.Text=cashDask.ExitCustomer.ToString();
            });
        }
    }
}
