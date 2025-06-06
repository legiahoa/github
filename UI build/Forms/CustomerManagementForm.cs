using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CoffeeManagement.DAO;

namespace CoffeeManagement.Forms
{
    public partial class CustomerManagementForm : Form
    {
        private DataTable customerTable;
        private bool isEditing = false;
        private string currentCustomerId = "";

        public CustomerManagementForm()
        {
            InitializeComponent();
        }

        private void CustomerManagementForm_Load(object sender, EventArgs e)
        {
            LoadCustomerData();
        }

        private void LoadCustomerData()
        {
            try
            {
                string query = @"SELECT kh.MAKH, kh.HOTEN, kh.SODT, kh.DIACHI, kh.EMAIL, 
                                        kh.GIOITINH, kh.NGAYSINH, tk.TENTK, tk.TRANGTHAI
                                FROM KHACHHANG kh 
                                INNER JOIN TAIKHOAN tk ON kh.MATK = tk.MATK
                                ORDER BY kh.MAKH";
                
                customerTable = DataProvider.ExecuteQuery(query);
                dgvCustomers.DataSource = customerTable;
                
                // Format DataGridView
                FormatDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu khách hàng: " + ex.Message, "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatDataGridView()
        {
            dgvCustomers.Columns["MAKH"].HeaderText = "Mã KH";
            dgvCustomers.Columns["HOTEN"].HeaderText = "Họ tên";
            dgvCustomers.Columns["SODT"].HeaderText = "Số điện thoại";
            dgvCustomers.Columns["DIACHI"].HeaderText = "Địa chỉ";
            dgvCustomers.Columns["EMAIL"].HeaderText = "Email";
            dgvCustomers.Columns["GIOITINH"].HeaderText = "Giới tính";
            dgvCustomers.Columns["NGAYSINH"].HeaderText = "Ngày sinh";
            dgvCustomers.Columns["TENTK"].HeaderText = "Tên tài khoản";
            dgvCustomers.Columns["TRANGTHAI"].HeaderText = "Trạng thái";

            dgvCustomers.Columns["MAKH"].Width = 80;
            dgvCustomers.Columns["HOTEN"].Width = 150;
            dgvCustomers.Columns["SODT"].Width = 120;
            dgvCustomers.Columns["DIACHI"].Width = 200;
            dgvCustomers.Columns["EMAIL"].Width = 150;
            dgvCustomers.Columns["GIOITINH"].Width = 80;
            dgvCustomers.Columns["NGAYSINH"].Width = 100;
            dgvCustomers.Columns["TENTK"].Width = 120;
            dgvCustomers.Columns["TRANGTHAI"].Width = 80;
        }        private void dgvCustomers_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCustomers.CurrentRow != null)
            {
                DataGridViewRow row = dgvCustomers.CurrentRow;
                
                txtCustomerID.Text = row.Cells["MAKH"].Value?.ToString() ?? "";
                txtFullName.Text = row.Cells["HOTEN"].Value?.ToString() ?? "";
                txtPhone.Text = row.Cells["SODT"].Value?.ToString() ?? "";
                // Only use controls that exist in the Designer
                // txtAddress.Text = row.Cells["DIACHI"].Value?.ToString() ?? "";
                // txtEmail.Text = row.Cells["EMAIL"].Value?.ToString() ?? "";
                // txtUsername.Text = row.Cells["TENTK"].Value?.ToString() ?? "";
                
                // string gender = row.Cells["GIOITINH"].Value?.ToString() ?? "";
                // rbMale.Checked = gender == "Nam";
                // rbFemale.Checked = gender == "Nữ";
                
                // if (row.Cells["NGAYSINH"].Value != DBNull.Value)
                // {
                //     dtpBirthDate.Value = Convert.ToDateTime(row.Cells["NGAYSINH"].Value);
                // }
                
                // bool isActive = row.Cells["TRANGTHAI"].Value?.ToString() == "1";
                // chkActive.Checked = isActive;
                
                currentCustomerId = txtCustomerID.Text;
                isEditing = true;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ClearForm();
            isEditing = false;
            currentCustomerId = "";
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvCustomers.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần sửa!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            isEditing = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvCustomers.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần xóa!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa khách hàng này?", "Xác nhận", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    string customerId = dgvCustomers.CurrentRow.Cells["MAKH"].Value.ToString();
                    
                    // Get account ID first
                    string getAccountQuery = "SELECT MATK FROM KHACHHANG WHERE MAKH = @customerId";
                    DataTable accountResult = DataProvider.ExecuteQuery(getAccountQuery, new object[] { customerId });
                    
                    if (accountResult.Rows.Count > 0)
                    {
                        string accountId = accountResult.Rows[0]["MATK"].ToString();
                        
                        // Delete customer record
                        string deleteCustomerQuery = "DELETE FROM KHACHHANG WHERE MAKH = @customerId";
                        DataProvider.ExecuteNonQuery(deleteCustomerQuery, new object[] { customerId });
                        
                        // Delete account record
                        string deleteAccountQuery = "DELETE FROM TAIKHOAN WHERE MATK = @accountId";
                        DataProvider.ExecuteNonQuery(deleteAccountQuery, new object[] { accountId });
                        
                        MessageBox.Show("Xóa khách hàng thành công!", "Thông báo", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        LoadCustomerData();
                        ClearForm();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa khách hàng: " + ex.Message, "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            try
            {
                if (isEditing)
                {
                    UpdateCustomer();
                }
                else
                {
                    AddNewCustomer();
                }
                
                LoadCustomerData();
                ClearForm();
                MessageBox.Show("Lưu thông tin khách hàng thành công!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu thông tin khách hàng: " + ex.Message, "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFullName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhone.Focus();
                return false;
            }            return true;
        }

        private void AddNewCustomer()
        {
            // Generate new customer ID
            string newCustomerId = GenerateNewCustomerId();
            string newAccountId = GenerateNewAccountId();
            
            // Insert account first
            string insertAccountQuery = @"INSERT INTO TAIKHOAN (MATK, TENTK, MATKHAU, LOAITK, TRANGTHAI) 
                                         VALUES (@accountId, @username, @password, @accountType, @status)";
            
            DataProvider.ExecuteNonQuery(insertAccountQuery, new object[] 
            {
                newAccountId,
                "user_" + newCustomerId, // Generate username
                "123456", // Default password
                "KHACH",
                1 // Active by default
            });            
            // Insert customer
            string insertCustomerQuery = @"INSERT INTO KHACHHANG (MAKH, HOTEN, SODT, DIACHI, EMAIL, GIOITINH, NGAYSINH, MATK) 
                                          VALUES (@customerId, @fullName, @phone, @address, @email, @gender, @birthDate, @accountId)";
            
            DataProvider.ExecuteNonQuery(insertCustomerQuery, new object[] 
            {
                newCustomerId,
                txtFullName.Text.Trim(),
                txtPhone.Text.Trim(),
                "", // Default empty address
                "", // Default empty email
                "Nam", // Default gender
                DateTime.Now, // Default birth date
                newAccountId
            });
        }        private void UpdateCustomer()
        {
            // Get account ID
            string getAccountQuery = "SELECT MATK FROM KHACHHANG WHERE MAKH = @customerId";
            DataTable accountResult = DataProvider.ExecuteQuery(getAccountQuery, new object[] { currentCustomerId });
            
            if (accountResult.Rows.Count > 0)
            {
                string accountId = accountResult.Rows[0]["MATK"].ToString();
                
                // Update account
                string updateAccountQuery = @"UPDATE TAIKHOAN SET TENTK = @username, TRANGTHAI = @status 
                                             WHERE MATK = @accountId";
                
                DataProvider.ExecuteNonQuery(updateAccountQuery, new object[] 
                {
                    "user_" + currentCustomerId, // Generate username
                    1, // Active by default
                    accountId
                });
                
                // Update customer
                string updateCustomerQuery = @"UPDATE KHACHHANG SET HOTEN = @fullName, SODT = @phone, 
                                              DIACHI = @address, EMAIL = @email, GIOITINH = @gender, 
                                              NGAYSINH = @birthDate WHERE MAKH = @customerId";
                
                DataProvider.ExecuteNonQuery(updateCustomerQuery, new object[] 
                {
                    txtFullName.Text.Trim(),
                    txtPhone.Text.Trim(),
                    "", // Default empty address
                    "", // Default empty email
                    "Nam", // Default gender
                    DateTime.Now, // Default birth date
                    currentCustomerId
                });
            }
        }

        private string GenerateNewCustomerId()
        {
            string query = "SELECT MAX(CAST(SUBSTRING(MAKH, 3, LEN(MAKH) - 2) AS INT)) FROM KHACHHANG WHERE MAKH LIKE 'KH%'";
            DataTable result = DataProvider.ExecuteQuery(query);
            
            int maxId = 0;
            if (result.Rows.Count > 0 && result.Rows[0][0] != DBNull.Value)
            {
                maxId = Convert.ToInt32(result.Rows[0][0]);
            }
            
            return $"KH{(maxId + 1):D3}";
        }

        private string GenerateNewAccountId()
        {
            string query = "SELECT MAX(CAST(SUBSTRING(MATK, 3, LEN(MATK) - 2) AS INT)) FROM TAIKHOAN WHERE MATK LIKE 'TK%'";
            DataTable result = DataProvider.ExecuteQuery(query);
            
            int maxId = 0;
            if (result.Rows.Count > 0 && result.Rows[0][0] != DBNull.Value)
            {
                maxId = Convert.ToInt32(result.Rows[0][0]);
            }
            
            return $"TK{(maxId + 1):D3}";
        }        private void ClearForm()
        {
            txtCustomerID.Clear();
            txtFullName.Clear();
            txtPhone.Clear();
            
            isEditing = false;
            currentCustomerId = "";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (customerTable != null)
            {
                string searchText = txtSearch.Text.Trim();
                if (string.IsNullOrEmpty(searchText))
                {
                    dgvCustomers.DataSource = customerTable;
                }
                else
                {
                    DataView dv = customerTable.DefaultView;
                    dv.RowFilter = $"MAKH LIKE '%{searchText}%' OR HOTEN LIKE '%{searchText}%' OR SODT LIKE '%{searchText}%' OR TENTK LIKE '%{searchText}%'";
                    dgvCustomers.DataSource = dv;
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            // Trigger search functionality - same as txtSearch_TextChanged
            txtSearch_TextChanged(sender, e);
        }

        private void btnClearSearch_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            dgvCustomers.DataSource = customerTable;
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow search on Enter key press
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnSearch_Click(sender, e);
                e.Handled = true;
            }
        }
    }
}
