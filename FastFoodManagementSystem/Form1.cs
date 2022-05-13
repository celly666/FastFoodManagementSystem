using System.Data.SqlClient;
using System.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Fast_Food_Management_System
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-M74EK9T\SQLEXPRESS;Initial Catalog=FastFoodDB;Integrated Security=True");
        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            this.Hide();
            GuestOrders guest = new GuestOrders();
            guest.Show();
        }
        public static string user;

        private void button1_Click(object sender, EventArgs e)
        {
            user = username.Text;
            if (username.Text == "" || userpass.Text == "")
            {
                MessageBox.Show("Enter A UserName Or Password");
            }
            else
            {
                Con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("select count(*) from UsersTbl where Uname = '" + username.Text + "'and Upassword ='" + userpass.Text + "'", Con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows[0][0].ToString() == "1")
                {

                    UserOrders uorder = new UserOrders();
                    uorder.Show();
                    this.Hide();
                    
                }
                else
                {
                    MessageBox.Show("Wrong Username Or Password");
                }
                Con.Close();

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}