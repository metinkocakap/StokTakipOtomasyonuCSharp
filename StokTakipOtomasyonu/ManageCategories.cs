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
namespace StokTakipOtomasyonu
{
    public partial class ManageCategories : Form
    {
        public ManageCategories()
        {
            InitializeComponent();
        }
       
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\kocak\Documents\Inventorydb.mdf;Integrated Security=True;Connect Timeout=30");
        void populate()
        {
            try
            {
                Con.Open();
                string Myquery = "select*from CategoryTbl";
                SqlDataAdapter da = new SqlDataAdapter(Myquery, Con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                ManageCategoriesGV.DataSource = ds.Tables[0];
                Con.Close();
            }
            catch
            {


            }
        }
            private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("insert into CategoryTbl values('" + stokid.Text + "','" + stokad.Text + "')", Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Ürün başarıyla eklendi");
                Con.Close();
                populate();

            }
            catch
            {
                MessageBox.Show("Eklenemedi!");

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (stokid.Text == "")
            {
                MessageBox.Show("Lütfen Stok ID Giriniz..");
            }
            else
            {
                Con.Open();
                string myquery = "delete from CategoryTbl where stokid='" + stokid.Text + "';";
                SqlCommand cmd = new SqlCommand(myquery, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Ürün Silindi!");
                Con.Close();
                populate();

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("update CategoryTbl set stokid='" + stokid.Text + "',stokad='" + stokad.Text +"'", Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Ürün başarıyla düzenlendi");
                Con.Close();
                populate();

            }
            catch
            {
                MessageBox.Show("Düzenleme Basarısız!");

            }
        }

        private void ManageCategories_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ManageCategoriesGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            stokid.Text = ManageCategoriesGV.SelectedRows[0].Cells[0].Value.ToString();
            stokad.Text = ManageCategoriesGV.SelectedRows[0].Cells[1].Value.ToString();
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select count(*) from OrderTbl where CustId=" + stokid.Text + "", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            OrderLabel.Text = dt.Rows[0][0].ToString();
            SqlDataAdapter sda1 = new SqlDataAdapter("select (TotalAmt) from OrderTbl where CustId=" + stokid.Text + "", Con);
            DataTable dt1= new DataTable();
            sda1.Fill(dt1);
            AmountLabel.Text = dt.Rows[0][0].ToString();
            Con.Close();
            SqlDataAdapter sda2 = new SqlDataAdapter("select Max(OrderDate) from OrderTbl where CustId=" + stokid.Text + "", Con);
            DataTable dt2 = new DataTable();
            sda2.Fill(dt2);
            DateLabel.Text = dt2.Rows[0][0].ToString();
            Con.Close();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            HomeForm home = new HomeForm();
            home.Show();
            this.Hide();
        }
    }
}
