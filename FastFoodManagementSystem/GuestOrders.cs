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
    public partial class GuestOrders : Form
    {
        public GuestOrders()
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
            string query = "select * from ItemTbl where Itemcat = '" + questcombo.SelectedItem.ToString() + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var sd = new DataSet();
            sda.Fill(sd);
            ItemsGV.DataSource = sd.Tables[0];
            Con.Close();
        }


        private void label5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 login = new Form1();
            login.Show();
        }

        private void GuestOrders_Load(object sender, EventArgs e)
        {
            populate();
            table.Columns.Add("Num", typeof(int));
            table.Columns.Add("Item", typeof(string));
            table.Columns.Add("Category", typeof(string));
            table.Columns.Add("UnitPrice", typeof(int));
            table.Columns.Add("Total", typeof(int));
            gestdv.DataSource = table;
            data.Text = DateTime.Today.Day.ToString() + "/" + DateTime.Today.Month.ToString() + "/" + DateTime.Today.Year.ToString(); ;
        }
        int num = 0;
        int price, total;
        string item, cat;
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

        private void button2_Click(object sender, EventArgs e)
        {
            Con.Open();
            string query = "insert into OrdersTbl values(" + ordernumtb.Text + ",'" + data.Text + "','" + sellernametb.Text + "'," + guestamnt.Text + ")";
            SqlCommand cmd = new SqlCommand(query, Con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Order Successfully Created");
            Con.Close();
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (qtytxt.Text == "")
            {
                MessageBox.Show("What is The Quantity of Item?");
            }
            else if (flag == 0)
            {
                MessageBox.Show("Select The Product To be Ordered");
            }
            else
            {
                num = num + 1;
                total = price * Convert.ToInt32(qtytxt.Text);
                table.Rows.Add(num, item, cat, price, total);
                gestdv.DataSource = table;
                flag = 0;


            }
            sum = sum + total;
            guestamnt.Text = "" + sum;

        }

        private void questcombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            filterbycategory();
        }
    }
}
