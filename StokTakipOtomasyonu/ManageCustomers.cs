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
    public partial class ManageCustomers : Form
    {
        public ManageCustomers()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\kocak\Documents\Inventorydb.mdf;Integrated Security=True;Connect Timeout=30");
        void populate()
        {
            try
            {
                Con.Open();
                string Myquery = "select*from CustomerTbl";
                SqlDataAdapter da = new SqlDataAdapter(Myquery, Con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                CustomerGV.DataSource = ds.Tables[0];
                Con.Close();
            }
            catch
            {


            }
        }
        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("insert into CustomerTbl values('" + stokTb.Text + "','" + stokadTb.Text + "','" + tedarikTb.Text + "','" + BirimTb.Text + "','" + GirisStokTb.Text + "')", Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Stok Eklendi..");
                Con.Close();
                populate();

            }
            catch
            {
                MessageBox.Show("Eklenemedi!");

            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ManageCustomers_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (stokTb.Text == "")
            {
                MessageBox.Show("Lütfen Stok ID Giriniz..");
            }
            else
            {
                Con.Open();
                string myquery = "delete from CustomerTbl where stokTb='" + stokTb.Text + "';";
                SqlCommand cmd = new SqlCommand(myquery, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Stok Silindi!");
                Con.Close();
                populate();

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("update CustomerTbl set stokTb='" + stokTb.Text + "',stokadTb='" + stokadTb.Text + "',tedarikTb='" + tedarikTb.Text + "',BirimTb='" + BirimTb.Text + "'where GirisStokTb='" + GirisStokTb.Text + "'", Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Stok başarıyla düzenlendi");
                Con.Close();
                populate();

            }
            catch
            {
                MessageBox.Show("Düzenleme Basarısız!");

            }
        }

        private void CustomerGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            stokTb.Text = CustomerGV.SelectedRows[0].Cells[0].Value.ToString();
            stokadTb.Text = CustomerGV.SelectedRows[0].Cells[1].Value.ToString();
            tedarikTb.Text = CustomerGV.SelectedRows[0].Cells[2].Value.ToString();
            BirimTb.Text = CustomerGV.SelectedRows[0].Cells[3].Value.ToString();
            GirisStokTb.Text = CustomerGV.SelectedRows[0].Cells[4].Value.ToString();
       
        }

        private void button4_Click(object sender, EventArgs e)
        {
            HomeForm home = new HomeForm();
            home.Show();
            this.Hide();
        }
    }
}
