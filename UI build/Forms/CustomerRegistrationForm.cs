using System;
using System.Data;
using System.Windows.Forms;
using CoffeeManagement.DAO;

namespace CoffeeManagement.Forms
{
    public partial class CustomerRegistrationForm : Form
    {
        public CustomerRegistrationForm()
        {
            InitializeComponent();
        }

        private void CustomerRegistrationForm_Load(object sender, EventArgs e)
        {
            // Clear all fields on load
            ClearFields();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            try
            {
                string customerId = GenerateCustomerId();
                string username = txtUsername.Text.Trim();
                string password = txtPassword.Text.Trim();
                string fullName = txtFullName.Text.Trim();
                string phone = txtPhone.Text.Trim();

                // Check if username already exists
                string checkQuery = "SELECT COUNT(*) FROM TAIKHOAN WHERE TENDANGNHAP = @username";
                int count = (int)DataProvider.ExecuteScalar(checkQuery, new object[] { username });

                if (count > 0)
                {
                    MessageBox.Show("Tên đăng nhập đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Insert into TAIKHOAN table
                string insertAccountQuery = "INSERT INTO TAIKHOAN (MATK, TENDANGNHAP, MATKHAU, VAITRO) VALUES (@accountId, @username, @password, @role)";
                DataProvider.ExecuteNonQuery(insertAccountQuery, new object[] { customerId, username, password, "KhachHang" });

                // Insert into KHACHHANG table
                string insertCustomerQuery = "INSERT INTO KHACHHANG (MAKH, TENKH, SDTKH) VALUES (@customerId, @fullName, @phone)";
                DataProvider.ExecuteNonQuery(insertCustomerQuery, new object[] { customerId, fullName, phone });

                MessageBox.Show("Đăng ký khách hàng thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrEmpty(txtUsername.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtPassword.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return false;
            }

            if (txtPassword.Text.Trim() != txtConfirmPassword.Text.Trim())
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirmPassword.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtFullName.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập họ tên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFullName.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtPhone.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhone.Focus();
                return false;
            }

            return true;
        }

        private string GenerateCustomerId()
        {
            try
            {
                string query = "SELECT TOP 1 MAKH FROM KHACHHANG ORDER BY MAKH DESC";
                DataTable result = DataProvider.ExecuteQuery(query);

                if (result.Rows.Count == 0)
                {
                    return "KH001";
                }

                string lastId = result.Rows[0]["MAKH"].ToString();
                int number = int.Parse(lastId.Substring(2)) + 1;
                return "KH" + number.ToString("D3");
            }
            catch
            {
                return "KH001";
            }
        }

        private void ClearFields()
        {
            txtUsername.Clear();
            txtPassword.Clear();
            txtConfirmPassword.Clear();
            txtFullName.Clear();
            txtPhone.Clear();
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = chkShowPassword.Checked ? '\0' : '*';
            txtConfirmPassword.PasswordChar = chkShowPassword.Checked ? '\0' : '*';
        }
    }
}
