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
    public partial class Transfer : Form
    {
        private String acctNo;
        SqlConnection myconn;

        public Transfer(string acctNo)
        {
            InitializeComponent();
            this.acctNo = acctNo;
            textBox1.Text = acctNo;
            textBox1.ReadOnly = true;
            myconn = new SqlConnection();
            myconn.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=UserDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            myconn.Open();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataTable mydt1 = Select(acctNo);
            DataTable mydt2 = Select(textBox2.Text);

            int balance1 = int.Parse(mydt1.Rows[0]["Balance"].ToString()) - int.Parse(textBox3.Text);
            int balance2 = int.Parse(mydt2.Rows[0]["Balance"].ToString()) + int.Parse(textBox3.Text);
            int id1 = int.Parse(mydt1.Rows[0]["Id"].ToString());
            int id2 = int.Parse(mydt2.Rows[0]["Id"].ToString());

            Update(balance1, id1);
            Update(balance2, id2);

            label6.Visible = true;
            button3.Visible = true;
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

        private void button3_Click(object sender, EventArgs e)
        {
            DataTable mydt1 = Select(acctNo);
            String routNo = mydt1.Rows[0]["RoutingNumber"].ToString();
            int balance = int.Parse(mydt1.Rows[0]["Balance"].ToString());

            this.Hide();
            Main main = new Main(acctNo, routNo, balance);
            main.ShowDialog();
            this.Close();
        }
    }
}
