using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CashTest
{
    public partial class frmMain : Form
    {
        public static Int32 port_exchanger=3;           //serial port
        public static Int32 baud_exchanger=115200;      //buadrate
        public static Int32 exchanger_type=1000;        //거스름돈 타입.
        public static Boolean coin_500 = false;         //500동전 사용여부

        public frmMain()
        {
            InitializeComponent();
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            int price = 0;
            bool pay_result;
            try
            {
                port_exchanger = Convert.ToInt32(textBox2.Text);
                price = Convert.ToInt32(textBox1.Text);
            }
            catch
            {
                return;
            }
            
            frmPopCashExchanger cash = new frmPopCashExchanger(price);
            cash.ShowDialog();
        
            if (cash.ReturnValue == "cancel")
            {
                pay_result = false;
            }
            else
                pay_result = true;


            label3.Text = pay_result.ToString();
        }
    }
}
