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
    public partial class ViewOrders : Form
    {
        public ViewOrders()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-M74EK9T\SQLEXPRESS;Initial Catalog=FastFoodDB;Integrated Security=True");
        void populate()
        {
            Con.Open();
            string query = "select * from OrdersTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var sd = new DataSet();
            sda.Fill(sd);
            OrdersGV.DataSource = sd.Tables[0];
            Con.Close();
        }

            private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void ViewOrders_Load(object sender, EventArgs e)
        {
            populate();
        }
        
 

        private void printDocument_Load(object sender, EventArgs e)
        {

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            
            e.Graphics.DrawString("=====Order Summary=====", new Font("Century", 20, FontStyle.Bold), Brushes.Red, new Point(208,70));
            e.Graphics.DrawString("Number : "+OrdersGV.SelectedRows[0].Cells[0].Value.ToString(), new Font("Century", 15, FontStyle.Regular), Brushes.Black, new Point(120, 210));
            e.Graphics.DrawString("Date : " + OrdersGV.SelectedRows[0].Cells[1].Value.ToString(), new Font("Century", 15, FontStyle.Regular), Brushes.Black, new Point(120, 240));
            e.Graphics.DrawString("Seller: " + OrdersGV.SelectedRows[0].Cells[2].Value.ToString(), new Font("Century", 15, FontStyle.Regular), Brushes.Black, new Point(120, 270));
            e.Graphics.DrawString("Amount : " + OrdersGV.SelectedRows[0].Cells[3].Value.ToString(), new Font("Century", 15, FontStyle.Regular), Brushes.Black, new Point(120, 300));
            e.Graphics.DrawString("=====Le Domingo=====", new Font("Century", 20, FontStyle.Bold), Brushes.Red, new Point(208, 400));


        }


        private void OrdersGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           if(printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }
    }
}
