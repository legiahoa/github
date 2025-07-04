using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using System.Windows.Forms;

namespace CoffeeManagement_ver2
{
    public partial class Dangkinhanvien : Form
    {

        private string role;  // Vai trò: KhachHang hoặc NhanVien
        private FirebaseClient firebaseClient;
        public Dangkinhanvien(string vaiTro)
        {
            InitializeComponent();
            role = vaiTro;
            firebaseClient = new FirebaseClient("https://quanlycafe-4a7fa-default-rtdb.asia-southeast1.firebasedatabase.app/");
        }
 

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = chkShowPassword.Checked ? '\0' : '*';
        }

        private async void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string confirmPassword = txtConfirmPassword.Text.Trim();
            string hoTen = txtFullName.Text.Trim();
            string soDienThoai = txtPhone.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) ||
                string.IsNullOrEmpty(confirmPassword) || string.IsNullOrEmpty(hoTen) || string.IsNullOrEmpty(soDienThoai))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // ✅ Lấy danh sách tài khoản từ Firebase
                var taiKhoanList = await firebaseClient
                    .Child("TaiKhoan")
                    .OnceAsync<TaiKhoanModel>();

                // ✅ Kiểm tra username đã tồn tại chưa
                bool isDuplicate = taiKhoanList.Any(tk => tk.Object.Username.Equals(username, StringComparison.OrdinalIgnoreCase));

                if (isDuplicate)
                {
                    MessageBox.Show("Tên đăng nhập đã tồn tại. Vui lòng chọn tên khác.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // ✅ Nếu không trùng, đăng ký tài khoản
                var taiKhoan = new TaiKhoanModel
                {
                    Username = username,
                    Password = password,
                    Role = role,
                    HoTen = hoTen,
                    SoDienThoai = soDienThoai
                };

                await firebaseClient.Child("TaiKhoan").PostAsync(taiKhoan);
                MessageBox.Show("Đăng ký thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi đăng ký: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Dangkinhanvien_Load(object sender, EventArgs e)
        {

        }
    }
}
