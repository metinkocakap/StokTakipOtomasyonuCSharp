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
    public partial class ManageUsers : Form
    {
        public ManageUsers()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\kocak\Documents\Inventorydb.mdf;Integrated Security=True;Connect Timeout=30");
        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        void populate()
        {
            try
            {
                Con.Open();
                string Myquery = "select*from UserTbl";
                SqlDataAdapter da = new SqlDataAdapter(Myquery, Con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                UsersGV.DataSource = ds.Tables[0];
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
                SqlCommand cmd = new SqlCommand("insert into UserTbl values('" + unameTb.Text + "','" + FnameTb.Text + "','"+ Uadress.Text +"','" + PasswordTb.Text + "','" + PhoneTb.Text + "')", Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Kullanıcı başarıyla eklendi");
                Con.Close();
                populate();

            }
            catch 
            {
                MessageBox.Show("Eklenemedi!");
                
            }
        }

        private void ManageUsers_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (unameTb.Text == "")
            {
                MessageBox.Show("Lütfen Firma ID Giriniz..");
            }
            else
            {
                Con.Open();
                string myquery = "delete from UserTbl where Uname='" + unameTb.Text + "';";
                SqlCommand cmd = new SqlCommand(myquery, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Kullanıcı Silindi!");
                Con.Close();
                populate();
                     
            }
        }

       

        private void button2_Click(object sender, EventArgs e)
        {

            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("update UserTbl set Uname='" + unameTb.Text + "',Ufullname='"+FnameTb.Text + "',Uadress='"+Uadress.Text+ "',Upassword='" + PasswordTb.Text + "'where Uphone=" + PhoneTb.Text + "'", Con);
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

        private void usersGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
            unameTb.Text = UsersGV.SelectedRows[0].Cells[0].Value.ToString();
            FnameTb.Text = UsersGV.SelectedRows[0].Cells[1].Value.ToString();
            Uadress.Text = UsersGV.SelectedRows[0].Cells[2].Value.ToString();
            PasswordTb.Text = UsersGV.SelectedRows[0].Cells[3].Value.ToString();
            PhoneTb.Text = UsersGV.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            HomeForm home = new HomeForm();
            home.Show();
            this.Hide();
        }
    }
}
