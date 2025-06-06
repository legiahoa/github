using System;
using System.Data;
using System.Windows.Forms;
using CoffeeManagement.DAO;

namespace CoffeeManagement.Forms
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            // Test database connection
            if (!DataProvider.TestConnection())
            {
                MessageBox.Show("Không thể kết nối đến cơ sở dữ liệu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string query = "SELECT * FROM TAIKHOAN WHERE TENDANGNHAP = @username AND MATKHAU = @password";
                DataTable result = DataProvider.ExecuteQuery(query, new object[] { username, password });

                if (result.Rows.Count > 0)
                {
                    string role = result.Rows[0]["VAITRO"].ToString();
                    string accountId = result.Rows[0]["MATK"].ToString();

                    this.Hide();

                    if (role == "NhanVien")
                    {
                        AdminDashboard adminForm = new AdminDashboard(accountId);
                        adminForm.ShowDialog();
                    }
                    else if (role == "KhachHang")
                    {
                        CustomerDashboard customerForm = new CustomerDashboard(accountId);
                        customerForm.ShowDialog();
                    }

                    this.Show();
                    txtUsername.Clear();
                    txtPassword.Clear();
                }
                else
                {
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRegisterCustomer_Click(object sender, EventArgs e)
        {
            CustomerRegistrationForm regForm = new CustomerRegistrationForm();
            regForm.ShowDialog();
        }

        private void btnRegisterEmployee_Click(object sender, EventArgs e)
        {
            EmployeeRegistrationForm regForm = new EmployeeRegistrationForm();
            regForm.ShowDialog();
        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát chương trình?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = chkShowPassword.Checked ? '\0' : '*';
        }
    }
}
