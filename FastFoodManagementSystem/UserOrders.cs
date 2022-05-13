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
    public partial class UserOrders : Form
    {
        public UserOrders()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-M74EK9T\SQLEXPRESS;Initial Catalog=FastFoodDB;Integrated Security=True");
        void populate()
        {
            Con.Open();
            string query = "select * from ItemTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var sd = new DataSet();
            sda.Fill(sd);
            ItemsGV.DataSource = sd.Tables[0];
            Con.Close();
        }
        void filterbycategory()
        {
            Con.Open();
            string query = "select * from ItemTbl where Itemcat = '"+ ordercb.SelectedItem.ToString() + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var sd = new DataSet();
            sda.Fill(sd);
            ItemsGV.DataSource = sd.Tables[0];
            Con.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void label4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 login = new Form1();
            login.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            ItemsForm Item = new ItemsForm();
            Item.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            UsersForm user = new UsersForm();
            user.Show();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        int num = 0;
        int price, total;
        string item,cat;

        DataTable table = new DataTable();
        int flag = 0;
        int sum = 0;
        private void ItemsGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
                item = ItemsGV.SelectedRows[0].Cells[1].Value.ToString();
                cat = ItemsGV.SelectedRows[0].Cells[2].Value.ToString();
                price = Convert.ToInt32(ItemsGV.SelectedRows[0].Cells[3].Value.ToString());
                flag = 1;
            
        }


        private void ordercb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            filterbycategory();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            populate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Con.Open();
            string query = "insert into OrdersTbl values(" + ordernumtb.Text + ",'" + data.Text + "','" + sellernametb.Text + "'," + labelAmt.Text + ")";
            SqlCommand cmd = new SqlCommand(query, Con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Order Successfully Created");
            Con.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ViewOrders view = new ViewOrders();
            view.Show();
        }

        private void UserOrders_Load(object sender, EventArgs e)
        {
            populate();
            table.Columns.Add("Num", typeof(int));
            table.Columns.Add("Item", typeof(string));
            table.Columns.Add("Category", typeof(string));
            table.Columns.Add("UnitPrice", typeof(int));
            table.Columns.Add("Total", typeof(int));
            ordergv.DataSource = table;
            data.Text = DateTime.Today.Day.ToString() + "/" + DateTime.Today.Month.ToString() + "/" + DateTime.Today.Year.ToString();
            sellernametb.Text = Form1.user;
            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if(qtytxt.Text == "")
            {
                MessageBox.Show("What is The Quantity of Item?");
            }
            else if(flag == 0)
            {
                MessageBox.Show("Select The Product To be Ordered");
            }
            else
            {
                num = num + 1;
                total = price * Convert.ToInt32(qtytxt.Text);
                table.Rows.Add(num,item,cat,price,total);
                ordergv.DataSource = table;
                flag =0;
               
                
            }
            sum = sum + total;
            labelAmt.Text = "" + sum;
        }

    }
}
