using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bank_System
{
    public partial class Main : Form
    {
        private String acctNo;
        private String routNo;
        private int balance;

        public Main(String acct, String rout, int bal)
        {
            acctNo = acct;
            routNo = rout;
            balance = bal;
            InitializeComponent();
            button5.Text = "$" + balance;
            button14.Text = routNo;
            button13.Text = acctNo;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Transfer t = new Transfer(acctNo);
            t.ShowDialog();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Deposit d = new Deposit(acctNo);
            d.ShowDialog();
            this.Close();
        }
    }
}
