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
    public partial class LogIn : Form
    {
        public LogIn()
        {
            InitializeComponent();
            textBox2.PasswordChar = '*';
        }

        DataTable mydt;
        SqlDataAdapter myadapter;
        SqlCommand mycommand;
        SqlConnection myconn;

        private void button1_Click(object sender, EventArgs e)
        {

            myconn = new SqlConnection();
            myconn.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=User;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            myconn.Open();

            mycommand = new SqlCommand();
            mycommand.CommandText = "Select * from Account where Username=@user and Password=@pass";
            mycommand.Parameters.Add("@user", SqlDbType.VarChar, 50);
            mycommand.Parameters.Add("@pass", SqlDbType.VarChar, 50);
            mycommand.Parameters["@user"].Value = textBox1.Text;
            mycommand.Parameters["@pass"].Value = textBox2.Text;
            mycommand.Connection = myconn;

            myadapter = new SqlDataAdapter(mycommand);
            mydt = new DataTable();
            myadapter.Fill(mydt);

            if (mydt.Rows.Count > 0)
            {
                String acctNo = mydt.Rows[0]["AccountNumber"].ToString();
                String routNo = mydt.Rows[0]["RoutingNumber"].ToString();
                int balance = int.Parse(mydt.Rows[0]["Balance"].ToString());



                this.Hide();
                Main main = new Main(acctNo, routNo, balance);
                main.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Inavlid Username or Password");
            }
            
        }
    }
}
