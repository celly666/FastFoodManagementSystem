using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Fast_Food_Management_System
{
    
    public partial class UsersForm : Form
    {
        public UsersForm()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-M74EK9T\SQLEXPRESS;Initial Catalog=FastFoodDB;Integrated Security=True");
        void populate()
        {
            Con.Open();
            string query = "select * from UsersTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var sd = new DataSet();
            sda.Fill(sd);
            UsersGV.DataSource = sd.Tables[0];

            Con.Close();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            UserOrders uorder = new UserOrders();
            uorder.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ItemsForm item = new ItemsForm();
            item.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 login = new Form1();  
            login.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Con.Open();
            string query = "insert into UsersTbl values('"+Unametb.Text+ "','" + uphonetb.Text + "','" + upasswordtb.Text + "')";
            SqlCommand cmd = new SqlCommand(query,Con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("User Successfully Created");
            Con.Close();
            populate();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void UsersForm_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void UsersGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Unametb.Text = UsersGV.SelectedRows[0].Cells[0].Value.ToString();
            uphonetb.Text = UsersGV.SelectedRows[0].Cells[1].Value.ToString();
            upasswordtb.Text = UsersGV.SelectedRows[0].Cells[2].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(uphonetb.Text == "")
            {
                MessageBox.Show("Select The User to be Deleted");

            }
            else
            {
                Con.Open();
                string query = "delete from UsersTbl where Uphone = '" + uphonetb.Text + "'";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("User Successfully Deleted");
                Con.Close();
                populate();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
            if (uphonetb.Text == "" || upasswordtb.Text == "" || Unametb.Text == "")
            {
                MessageBox.Show("Fill All The fields");
            }
            else
            {
                Con.Open();
                string query = " update UsersTbl set Uname = '" + Unametb.Text + "', Upassword = '" + upasswordtb.Text + "' where Uphone = '" + uphonetb.Text + "'";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("User Successfully Updated");
                Con.Close();
                populate();
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
