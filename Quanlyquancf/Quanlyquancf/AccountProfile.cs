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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Quanlyquancf
{
    public partial class AccountProfile: Form
    {
        private bool isInfoLoaded = false;
        public AccountProfile()
        {
            InitializeComponent();
            
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        //nút cập nhật
        private void button1_Click(object sender, EventArgs e)
        {
            if (!isInfoLoaded)
            {
                MessageBox.Show("Vui lòng nhấn nút 'Load thông tin' trước khi cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string username = textBox1.Text.Trim();
            string newPassword = textBox4.Text.Trim();
            string rePassword = textBox5.Text.Trim();
            string fullName = textBox2.Text.Trim();

            if (newPassword != rePassword)
            {
                MessageBox.Show("Mật khẩu nhập lại không khớp!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox5.Clear();
                textBox5.Focus();
                return;
            }

            string connectSTR = "Data Source=localhost;Initial Catalog=COFFEE_MANAGEMENT;Integrated Security=True;Encrypt=False";
            using (SqlConnection conn = new SqlConnection(connectSTR))
            {
                conn.Open();

                // Cập nhật mật khẩu
                string updatePassword = "UPDATE TAIKHOAN SET MATKHAU = @pass WHERE TENDANGNHAP = @user";
                SqlCommand cmdPass = new SqlCommand(updatePassword, conn);
                cmdPass.Parameters.AddWithValue("@pass", newPassword);
                cmdPass.Parameters.AddWithValue("@user", username);
                cmdPass.ExecuteNonQuery();

                // Cập nhật họ tên KH hoặc NV
                
                    string updateName = @" IF EXISTS (SELECT 1 FROM KHACHHANG WHERE MAKH = (SELECT MATK FROM TAIKHOAN WHERE TENDANGNHAP = @user))
                UPDATE KHACHHANG SET TENKH = @hoten WHERE MAKH = (SELECT MATK FROM TAIKHOAN WHERE TENDANGNHAP = @user)
                ELSE
                UPDATE NHANVIEN SET TENNV = @hoten WHERE MANV = (SELECT MATK FROM TAIKHOAN WHERE TENDANGNHAP = @user)";
                    SqlCommand cmdName = new SqlCommand(updateName, conn);
                    cmdName.Parameters.AddWithValue("@hoten", fullName);
                    cmdName.Parameters.AddWithValue("@user", username);
                    cmdName.ExecuteNonQuery();


                MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        //nút tải thông tin
        private void button3_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text.Trim();
            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Vui lòng điền tên đăng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string connectSTR = "Data Source=localhost;Initial Catalog=COFFEE_MANAGEMENT;Integrated Security=True;Encrypt=False";
            using (SqlConnection conn = new SqlConnection(connectSTR))
            {
                conn.Open();
                string query = @"
            SELECT T.MATKHAU, K.TENKH 
            FROM TAIKHOAN T 
            LEFT JOIN KHACHHANG K ON T.MATK = K.MAKH 
            WHERE T.TENDANGNHAP = @user
        ";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@user", username);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        textBox3.Text = reader["MATKHAU"].ToString();
                        textBox2.Text = reader["TENKH"].ToString(); // Hoặc TENNV nếu là nhân viên
                        isInfoLoaded = true;
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy tài khoản!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
