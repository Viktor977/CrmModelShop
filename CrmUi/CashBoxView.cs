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
        public Label label { get; set; }
        public NumericUpDown numericUpDown { get; set; }  
       public ProgressBar ProgressBar { get; set; }
        public CashBoxView(CashDesk cashDesk,int number,int x,int y)
        {
            cashDask = cashDesk;
            label= new Label();
            numericUpDown = new NumericUpDown();

            label.AutoSize=true;
            label.Location=new System.Drawing.Point(x, y);
            label.Name = "label" + number;
            label.Size=new System.Drawing.Size(35, 15);
            label.TabIndex = number;
            cashDask.Number = number;
            label.Text=cashDask.ToString();

            //numeric UpDown

            numericUpDown.Location=new System.Drawing.Point(x + 60,y);
            numericUpDown.Name = "numericUpDown"+ number;
            numericUpDown.Size = new System.Drawing.Size(120,20);
            numericUpDown.TabIndex = number;
            cashDask.CheckClosed += CashDask_CheckClosed;
            numericUpDown.Maximum = 1000000000000;

            //ProgressBar
            ProgressBar = new ProgressBar();
            ProgressBar.Location = new System.Drawing.Point(x+300, y);
            ProgressBar.Maximum = cashDask.MaxQueueLangth;
            ProgressBar.Name = "progressbar"+number;
            ProgressBar.Size = new System.Drawing.Size(200, 20);
            ProgressBar.TabIndex = number;
            ProgressBar.Value = 0;
        }

        private void CashDask_CheckClosed(object sender, Check e)
        {
            numericUpDown.Invoke((Action)delegate 
            { 
                numericUpDown.Value += e.Price;
                ProgressBar.Value =cashDask.Number;
            });
        }
    }
}
