using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bank_System
{
    public partial class Deposit : Form
    {

        private String acctNo;
        SqlConnection myconn;

        public Deposit(string acctNo)
        {
            InitializeComponent();

            this.acctNo = acctNo;
            textBox2.Text = acctNo;
            textBox2.ReadOnly = true;

            myconn = new SqlConnection();
            myconn.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=UserDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            myconn.Open();
        }

        private void button4_Click(object sender, EventArgs e)
        {

            int amount = int.Parse(textBox1.Text);

            if (amount > 15000)
            {
                MessageBox.Show("Amount too large to process!!!");
            }
            else { 

                DataTable data = Select(acctNo);
                int balance = int.Parse(data.Rows[0]["Balance"].ToString()) + int.Parse(textBox1.Text);
                int id = int.Parse(data.Rows[0]["Id"].ToString());

                Update(balance, id);

                MessageBox.Show("Deposit Successful");

                DataTable mydt1 = Select(acctNo);
                String routNo = mydt1.Rows[0]["RoutingNumber"].ToString();
                int bal = int.Parse(mydt1.Rows[0]["Balance"].ToString());

                this.Hide();
                Main main = new Main(acctNo, routNo, bal);
                main.ShowDialog();
                this.Close();
            }
        }

        private DataTable Select(String acct)
        {
            SqlCommand mycommand = new SqlCommand();
            mycommand.CommandText = "Select * from Account where AccountNumber=@acct";
            mycommand.Parameters.Add("@acct", SqlDbType.VarChar, 50);
            mycommand.Parameters["@acct"].Value = acct;
            mycommand.Connection = myconn;
            SqlDataAdapter myadapter = new SqlDataAdapter(mycommand);
            DataTable mydt = new DataTable();
            myadapter.Fill(mydt);
            return mydt;
        }

        private void Update(int bal, int id)
        {
            SqlCommand updcommand = new SqlCommand();
            updcommand.Connection = myconn;
            updcommand.CommandText = "Update Account set Balance = @bal where ID = @ID";
            updcommand.Parameters.Add("@bal", SqlDbType.Int, 50);
            updcommand.Parameters["@bal"].Value = bal;
            updcommand.Parameters.Add("@ID", SqlDbType.BigInt, 50);
            updcommand.Parameters["@ID"].Value = id;
            updcommand.ExecuteNonQuery();
        }
    }
}
