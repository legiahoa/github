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
                // Truy vấn đã được điều chỉnh để phù hợp với các bảng mới
                string query = "SELECT COUNT(*), TK.TENDANGNHAP, TK.MATKHAU, NV.MANV, KH.MAKH " +
                               "FROM TAIKHOAN TK " +
                               "LEFT JOIN NHANVIEN NV ON TK.MATK = NV.MANV " + // Liên kết với bảng NHANVIEN qua MATK = MANV
                               "LEFT JOIN KHACHHANG KH ON TK.MATK = KH.MAKH " + // Liên kết với bảng KHACHHANG qua MATK = MAKH
                               "WHERE TK.TENDANGNHAP = @user AND TK.MATKHAU = @pass AND TK.VAITRO = @role " +
                               "GROUP BY TK.TENDANGNHAP, TK.MATKHAU, NV.MANV, KH.MAKH";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@user", username);
                cmd.Parameters.AddWithValue("@pass", password);
                cmd.Parameters.AddWithValue("@role", vaiTro);



                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    int count = reader.GetInt32(0);
                    if (count > 0)
                    {
                        MessageBox.Show($"Đăng nhập {vaiTro} thành công!");

                        // Lấy Mã Nhân Viên hoặc Mã Khách Hàng
                        string maNV = reader["MANV"] != DBNull.Value ? reader.GetString(reader.GetOrdinal("MANV")) : null;
                        string maKH = reader["MAKH"] != DBNull.Value ? reader.GetString(reader.GetOrdinal("MAKH")) : null;
                        // TODO: Mở form chính theo vai trò
                        if (vaiTro == "NhanVien")
                        {
                            Hethongquanly formNV = new Hethongquanly(); // Truyền mã nhân viên
                            this.Hide();
                            formNV.ShowDialog();
                            this.Show();
                        }
                        else if (vaiTro == "KhachHang")
                        {
                            Khachhang formKH = new Khachhang(maKH); // Truyền mã khách hàng
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
                else
                {
                    MessageBox.Show("Sai tên đăng nhập, mật khẩu hoặc vai trò!");
                }
                reader.Close();
            }
        }
    }
}
