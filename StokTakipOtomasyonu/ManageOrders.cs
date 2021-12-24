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
    public partial class ManageOrders : Form
    {
        public ManageOrders()
        {
            InitializeComponent();
        }
        DataTable table = new DataTable();

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
                CustomersGV.DataSource = ds.Tables[0];
                Con.Close();
            }
            catch
            {


            }
        }
        void populateProducts()
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
                SearchCombo.ValueMember = "stokad";
               // SearchCombo.DataSource = dt;
                //SearchCombo.ValueMember = "stokad";
                SearchCombo.DataSource = dt;
                Con.Close();
            }
            catch
            {


            }

        }
        void updateproduct()
        {
           
            int id=Convert.ToInt32( ProductsGV.SelectedRows[0].Cells[0].Value.ToString());
            int newQty = stock - Convert.ToInt32(QtyTb.Text);
            if (newQty < 0)
                MessageBox.Show("Başarısız ekleme");
            else
            {

                Con.Open();
                string query = "update ProductsTbl set urunMK=" + newQty + "where urunID=" + id + ";";
            SqlCommand cmd = new SqlCommand(query, Con);
            cmd.ExecuteNonQuery();
            Con.Close();
            populateProducts();
            }
        }
        int sayi = 0;
        int miktar, yüksekfiyat, toplam;
        string ürün;
        private void ManageOrders_Load(object sender, EventArgs e)
        {
            populate();
            populateProducts();
            fillcategory();
            table.Columns.Add("Sayi", typeof(int));
            table.Columns.Add("Ürün", typeof(string));
            table.Columns.Add("Miktar", typeof(int));
            table.Columns.Add("Fiyat", typeof(int));
            table.Columns.Add("Toplam", typeof(int));
            OrderGv.DataSource = table;
        }
        
        int flag = 0;
        int stock = 0;
       
        private void ProductsGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ürün = ProductsGV.SelectedRows[0].Cells[1].Value.ToString();
            // miktar = Convert.ToInt32(QtyTb.Text);
            stock = Convert.ToInt32(ProductsGV.SelectedRows[0].Cells[2].Value.ToString());
            yüksekfiyat = Convert.ToInt32(ProductsGV.SelectedRows[0].Cells[3].Value.ToString());
            //toplam = miktar * yüksekfiyat;
            flag = 1;
            

        }
        int sum = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            
            if (QtyTb.Text == "")
                MessageBox.Show("Ürün Miktarını Giriniz..");
            else if (flag == 0)
            {
                MessageBox.Show("Ürün Seçiniz..");
            }
            else if (Convert.ToInt32(QtyTb.Text) > stock)
                MessageBox.Show("Ürün Tükendi..");
            else
            {
                sayi = sayi + 1;
                miktar = Convert.ToInt32(QtyTb.Text);
                toplam = miktar * yüksekfiyat;
                table.Rows.Add(sayi, ürün, miktar, yüksekfiyat, toplam);
                OrderGv.DataSource = table;
                flag = 0;
            }
            sum = sum + toplam;
            TotAmount.Text = "RS" + sum.ToString();
            updateproduct();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (OrderId.Text == "" || CustId.Text == "" || CustName.Text == "" || TotAmount.Text == "")
            {
                MessageBox.Show("Lütfen Boş Alanları Doldurun!");
            }
            else
            {
                
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into OrderTbl values('"  + OrderId.Text+ "','" + CustId.Text + "','" + CustName.Text + "','" + orderdate.Text + "','" + TotAmount.Text +  "')", Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Ürün başarıyla eklendi");
                    Con.Close();
                    //populate();
try
                {
                }
                catch
                {
                    MessageBox.Show("Eklenemedi!");

                }

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ViewOrders view = new ViewOrders();
            view.Show();

        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            HomeForm home = new HomeForm();
            home.Show();
            this.Hide();
        }

        private void CustomersGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CustId.Text = CustomersGV.SelectedRows[0].Cells[0].Value.ToString();
            CustName.Text= CustomersGV.SelectedRows[0].Cells[1].Value.ToString();
        }

        private void SearchCombo_SelectionChangeCommitted(object sender, EventArgs e)
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
        }
    }

