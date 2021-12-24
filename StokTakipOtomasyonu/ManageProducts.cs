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
    public partial class ManageProducts : Form
    {
        public ManageProducts()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\kocak\Documents\Inventorydb.mdf;Integrated Security=True;Connect Timeout=30");

        void populate()
        {
            try
            {
                Con.Open();
                string Myquery = "select*from ProductsTbl";
                SqlDataAdapter da = new SqlDataAdapter(Myquery, Con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                ProductsGV.DataSource = ds.Tables[0];
                Con.Close();
            }
            catch
            {


            }
        }
        void filterbycategory()
        {
            try
            {
                Con.Open();
                string Myquery = "select*from ProductsTbl where CatCombo='" + SearchCombo.SelectedValue.ToString() + "'";
                SqlDataAdapter da = new SqlDataAdapter(Myquery, Con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                ProductsGV.DataSource = ds.Tables[0];
                Con.Close();
            }
            catch
            {


            }
        }
            void fillcategory()
        {
            string query = "select*from CategoryTbl";
            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataReader rdr;
            try
            {
                Con.Open();
                DataTable dt = new DataTable();
                dt.Columns.Add("stokad", typeof(string));
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                CatCombo.ValueMember = "stokad";
                CatCombo.DataSource = dt;
                SearchCombo.ValueMember = "stokad";
                SearchCombo.DataSource = dt;
                Con.Close();
            }
            catch 
            {

                
            }

        }
        private void ManageProducts_Load(object sender, EventArgs e)
        {
            fillcategory();
            populate();
        }
        private void ProductsGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            urunID.Text = ProductsGV.SelectedRows[0].Cells[0].Value.ToString();
            urunAD.Text = ProductsGV.SelectedRows[0].Cells[1].Value.ToString();
            urunMK.Text = ProductsGV.SelectedRows[0].Cells[2].Value.ToString();
            urunFYT.Text = ProductsGV.SelectedRows[0].Cells[3].Value.ToString();
            aciklama.Text = ProductsGV.SelectedRows[0].Cells[4].Value.ToString();
            CatCombo.SelectedValue = ProductsGV.SelectedRows[0].Cells[5].Value.ToString();
        }
        private void button1_Click(object sender, EventArgs e)
        {
              try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("insert into ProductsTbl values('" + urunID.Text + "','" + urunAD.Text +"','"+urunMK.Text +"','"+urunFYT.Text+"','"+aciklama.Text+ "','"+CatCombo.SelectedItem.ToString()+ "')", Con);
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
            if (urunID.Text == "")
            {
                MessageBox.Show("Lütfen Ürün ID Giriniz..");
            }
            else
            {
                Con.Open();
                string myquery = "delete from ProductsTbl where urunID='" + urunID.Text + "';";
                SqlCommand cmd = new SqlCommand(myquery, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Ürün Silindi!");
                Con.Close();
                populate();

            }
        }

    

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

    
        private void button2_Click(object sender, EventArgs e)
        {

            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("update ProductsTbl set urunAD='" + urunAD.Text + "',urunMK=" + urunMK.Text + ",urunFYT=" + urunFYT.Text + ",aciklama= '" + aciklama.Text + "',CatCombo='" + CatCombo.SelectedValue.ToString() + "' where urunID=" + urunID.Text + "", Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Kullanıcı başarıyla düzenlendi");
                Con.Close();
                populate();

            }
            catch
            {
                MessageBox.Show("Düzenleme Basarısız!");

            }
        }

        private void Search_Click(object sender, EventArgs e)
        {
            filterbycategory();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            populate();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            HomeForm home = new HomeForm();
            home.Show();
            this.Hide();
        }
    }
}
