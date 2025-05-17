using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Collections.Specialized.BitVector32;

namespace Quanlyquancf
{
    public partial class Dangnhap: Form
    {
        public Dangnhap()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            DangNhap("NhanVien");
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Dangnhap_Load(object sender, EventArgs e)
        {

        }

        private void Dangnhap_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có thật sự muốn thoát?", "Xác nhận", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                e.Cancel = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DangNhap("KhachHang");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dangkikhachhang f = new Dangkikhachhang();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Dangkinhanvien f = new Dangkinhanvien();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }
        // chức năng đăng nhập
        private void DangNhap(string vaiTro)
        {
            string username = textBox1.Text.Trim();
            string password = textBox2.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin đăng nhập!");
                return;
            }

            string connectSTR = "Data Source=localhost;Initial Catalog=COFFEE_MANAGEMENT;Integrated Security=True;Encrypt=False";
            using (SqlConnection conn = new SqlConnection(connectSTR))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM TAIKHOAN WHERE TENDANGNHAP = @user AND MATKHAU = @pass AND VAITRO = @role";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@user", username);
                cmd.Parameters.AddWithValue("@pass", password);
                cmd.Parameters.AddWithValue("@role", vaiTro);

                int count = (int)cmd.ExecuteScalar();
                if (count > 0)
                {
                    MessageBox.Show($"Đăng nhập {vaiTro} thành công!");

                    // TODO: Mở form chính theo vai trò
                    if (vaiTro == "NhanVien")
                    {
                        Hethongquanly formNV = new Hethongquanly();
                        this.Hide();
                        formNV.ShowDialog();
                        this.Show();
                    }
                    else if (vaiTro == "KhachHang")
                    {
                        
                        Khachhang formKH = new Khachhang();
                        this.Hide();
                        formKH.ShowDialog();
                        this.Show();
                    }

                    //this.Hide();
                }
                else
                {
                    MessageBox.Show("Sai tên đăng nhập, mật khẩu hoặc vai trò!");
                }
            }
        }
    }
}
