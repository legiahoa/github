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

namespace Quanlyquancf
{
    public partial class Dangkinhanvien : Form
    {
        public Dangkinhanvien()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void button1_Click(object sender, EventArgs e) //nút đăng ký
        {
            string username = textBox4.Text.Trim();
            string password = textBox3.Text.Trim();
            string confirmPassword = textBox5.Text.Trim();
            string hoTen = textBox1.Text.Trim();
            string soDienThoai = textBox2.Text.Trim();
            string vaiTro = "NhanVien";

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) ||
                string.IsNullOrEmpty(confirmPassword) || string.IsNullOrEmpty(hoTen) || string.IsNullOrEmpty(soDienThoai))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Mật khẩu nhập lại không khớp. Vui lòng nhập lại!");
                textBox5.Clear();
                textBox5.Focus();
                return;
            }

            string connectSTR = "Data Source=localhost;Initial Catalog=COFFEE_MANAGEMENT;Integrated Security=True;Encrypt=False";
            using (SqlConnection conn = new SqlConnection(connectSTR))
            {
                conn.Open();

                // Kiểm tra tên đăng nhập trùng
                string checkQuery = "SELECT COUNT(*) FROM TAIKHOAN WHERE TENDANGNHAP = @user";
                SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@user", username);
                int count = (int)checkCmd.ExecuteScalar();

                if (count > 0)
                {
                    MessageBox.Show("Tên đăng nhập đã tồn tại. Vui lòng chọn tên khác!");
                    return;
                }

                // Sinh mã tự động
                string prefix = "NV";
                string getIdQuery = "SELECT TOP 1 MATK FROM TAIKHOAN WHERE VAITRO = @vaitro AND MATK LIKE @prefix + '%' ORDER BY MATK DESC";
                SqlCommand getIdCmd = new SqlCommand(getIdQuery, conn);
                getIdCmd.Parameters.AddWithValue("@vaitro", vaiTro);
                getIdCmd.Parameters.AddWithValue("@prefix", prefix);
                object result = getIdCmd.ExecuteScalar();

                int nextNumber = 1;
                if (result != null)
                {
                    string lastId = result.ToString(); // VD: KH007
                    string numberPart = lastId.Substring(2); // "007"
                    if (int.TryParse(numberPart, out int num))
                        nextNumber = num + 1;
                }

                string maTK = prefix + nextNumber.ToString("D3"); // VD: KH008

                // BẮT BUỘC: Chèn vào TAIKHOAN trước
                string insertTK = "INSERT INTO TAIKHOAN (MATK, TENDANGNHAP, MATKHAU, VAITRO) VALUES (@matk, @user, @pass, @vaitro)";
                SqlCommand cmdTK = new SqlCommand(insertTK, conn);
                cmdTK.Parameters.AddWithValue("@matk", maTK);
                cmdTK.Parameters.AddWithValue("@user", username);
                cmdTK.Parameters.AddWithValue("@pass", password);
                cmdTK.Parameters.AddWithValue("@vaitro", vaiTro);
                cmdTK.ExecuteNonQuery();

                // Sau đó chèn vào NHANVIEN (dùng đúng MATK làm MANV)
                string insertKH = "INSERT INTO NHANVIEN (MANV, TENNV, SDTNV) VALUES (@manv, @hoten, @sdt)";
                SqlCommand cmdKH = new SqlCommand(insertKH, conn);
                cmdKH.Parameters.AddWithValue("@manv", maTK);
                cmdKH.Parameters.AddWithValue("@hoten", hoTen);
                cmdKH.Parameters.AddWithValue("@sdt", soDienThoai);
                cmdKH.ExecuteNonQuery();

                MessageBox.Show("Đăng ký nhân viên thành công!");
                this.Close();
            }
        }
    }
}
