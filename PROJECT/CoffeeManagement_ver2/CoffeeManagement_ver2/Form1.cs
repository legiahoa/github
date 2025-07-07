using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoffeeManagement_ver2
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = chkShowPassword.Checked ? '\0' : '*';
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            FirebaseHelper firebaseService = new FirebaseHelper();
            TaiKhoanModel user = await firebaseService.DangNhapAsync(username, password);

            if (user != null)
            {
                if (user.Role == "NhanVien")
                {
                    AdminDashboard dashboard = new AdminDashboard();
                    dashboard.Show();
                    this.Hide();
                }
                else if (user.Role == "KhachHang")
                {
                    CustomerDashboard dashboard = new CustomerDashboard(user);
                    dashboard.Show();
                    this.Hide();
                }
            }
            else
            {
                MessageBox.Show("Tài khoản hoặc mật khẩu không đúng!", "Đăng nhập thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRegisterCustomer_Click(object sender, EventArgs e)
        {
            Dangkikhachhang regForm = new Dangkikhachhang("KhachHang");
            this.Hide(); 
            regForm.ShowDialog();
            this.Show(); 
        }

        private void btnRegisterEmployee_Click(object sender, EventArgs e)
        {
            Dangkinhanvien regForm = new Dangkinhanvien("NhanVien");
            this.Hide();
            regForm.ShowDialog();
            this.Show();
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát chương trình?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}
