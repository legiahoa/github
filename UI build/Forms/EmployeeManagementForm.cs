using System;
using System.Data;
using System.Windows.Forms;
using CoffeeManagement.DAO;

namespace CoffeeManagement.Forms
{
    public partial class EmployeeManagementForm : Form
    {
        private enum FormMode
        {
            View,
            Add,
            Edit
        }

        private FormMode currentMode;
        private string currentEmployeeID;

        public EmployeeManagementForm()
        {
            InitializeComponent();
            currentMode = FormMode.View;
        }

        private void EmployeeManagementForm_Load(object sender, EventArgs e)
        {
            LoadEmployees();
            SetFormMode(FormMode.View);
        }

        private void LoadEmployees()
        {
            try
            {
                string query = @"
                    SELECT 
                        nv.MaNV AS 'Mã nhân viên',
                        nv.TenNV AS 'Họ và tên',
                        nv.SDT AS 'Số điện thoại',
                        nv.ChucVu AS 'Chức vụ',
                        tk.TenTaiKhoan AS 'Tài khoản'                    FROM NHANVIEN nv
                    LEFT JOIN TAIKHOAN tk ON nv.MaNV = tk.MaNV
                    ORDER BY nv.MaNV";

                DataTable dt = DataProvider.ExecuteQuery(query);
                dgvEmployees.DataSource = dt;

                // Configure columns
                if (dgvEmployees.Columns.Count > 0)
                {
                    dgvEmployees.Columns[0].Width = 120;
                    dgvEmployees.Columns[1].Width = 200;
                    dgvEmployees.Columns[2].Width = 150;
                    dgvEmployees.Columns[3].Width = 120;
                    dgvEmployees.Columns[4].Width = 150;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách nhân viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetFormMode(FormMode mode)
        {
            currentMode = mode;
            
            switch (mode)
            {
                case FormMode.View:
                    pnlEmployeeInfo.Visible = false;
                    btnAdd.Enabled = true;
                    btnEdit.Enabled = dgvEmployees.SelectedRows.Count > 0;
                    btnDelete.Enabled = dgvEmployees.SelectedRows.Count > 0;
                    break;
                    
                case FormMode.Add:
                    pnlEmployeeInfo.Visible = true;
                    ClearFields();
                    txtEmployeeID.Text = GenerateEmployeeID();
                    btnAdd.Enabled = false;
                    btnEdit.Enabled = false;
                    btnDelete.Enabled = false;
                    txtFullName.Focus();
                    break;
                    
                case FormMode.Edit:
                    pnlEmployeeInfo.Visible = true;
                    LoadSelectedEmployee();
                    btnAdd.Enabled = false;
                    btnEdit.Enabled = false;
                    btnDelete.Enabled = false;
                    txtFullName.Focus();
                    break;
            }
        }

        private string GenerateEmployeeID()
        {            try
            {
                string query = "SELECT TOP 1 MaNV FROM NHANVIEN ORDER BY MaNV DESC";
                DataTable dt = DataProvider.ExecuteQuery(query);
                
                if (dt.Rows.Count == 0)
                {
                    return "NV001";
                }
                
                string lastID = dt.Rows[0]["MaNV"].ToString();
                if (lastID.StartsWith("NV") && lastID.Length == 5)
                {
                    int number = int.Parse(lastID.Substring(2));
                    return $"NV{(number + 1):D3}";
                }
                
                return "NV001";
            }
            catch
            {
                return "NV001";
            }
        }

        private void ClearFields()
        {
            txtEmployeeID.Clear();
            txtFullName.Clear();
            txtPhone.Clear();
            cmbPosition.SelectedIndex = -1;
        }

        private void LoadSelectedEmployee()
        {
            if (dgvEmployees.SelectedRows.Count == 0) return;
            
            DataGridViewRow row = dgvEmployees.SelectedRows[0];
            currentEmployeeID = row.Cells["Mã nhân viên"].Value.ToString();
            
            txtEmployeeID.Text = currentEmployeeID;
            txtFullName.Text = row.Cells["Họ và tên"].Value.ToString();
            txtPhone.Text = row.Cells["Số điện thoại"].Value.ToString();
            cmbPosition.Text = row.Cells["Chức vụ"].Value.ToString();
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Vui lòng nhập họ và tên nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFullName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhone.Focus();
                return false;
            }

            if (cmbPosition.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn chức vụ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbPosition.Focus();
                return false;
            }

            // Validate phone number format
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtPhone.Text, @"^[0-9]{10,11}$"))
            {
                MessageBox.Show("Số điện thoại không hợp lệ! Vui lòng nhập 10-11 chữ số.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhone.Focus();
                return false;
            }

            return true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            SetFormMode(FormMode.Add);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvEmployees.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            SetFormMode(FormMode.Edit);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvEmployees.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                string employeeID = dgvEmployees.SelectedRows[0].Cells["Mã nhân viên"].Value.ToString();                // First, delete from TAIKHOAN if exists
                string deleteAccountQuery = "DELETE FROM TAIKHOAN WHERE MaNV = @MaNV";
                DataProvider.ExecuteNonQuery(deleteAccountQuery, new object[] { employeeID });

                // Then delete from NHANVIEN
                string deleteEmployeeQuery = "DELETE FROM NHANVIEN WHERE MaNV = @MaNV";
                int result = DataProvider.ExecuteNonQuery(deleteEmployeeQuery, new object[] { employeeID });

                if (result > 0)
                {
                    MessageBox.Show("Xóa nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadEmployees();
                    SetFormMode(FormMode.View);
                }
                else
                {
                    MessageBox.Show("Không thể xóa nhân viên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa nhân viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            try
            {
                if (currentMode == FormMode.Add)
                {                    // Check if employee ID already exists
                    string checkQuery = "SELECT COUNT(*) FROM NHANVIEN WHERE MaNV = @MaNV";
                    int count = (int)DataProvider.ExecuteScalar(checkQuery, new object[] { txtEmployeeID.Text });
                    
                    if (count > 0)
                    {
                        MessageBox.Show("Mã nhân viên đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Insert new employee
                    string insertQuery = @"
                        INSERT INTO NHANVIEN (MaNV, TenNV, SDT, ChucVu) 
                        VALUES (@MaNV, @TenNV, @SDT, @ChucVu)";
                      int result = DataProvider.ExecuteNonQuery(insertQuery, new object[] 
                    {
                        txtEmployeeID.Text,
                        txtFullName.Text,
                        txtPhone.Text,
                        cmbPosition.Text
                    });

                    if (result > 0)
                    {
                        MessageBox.Show("Thêm nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadEmployees();
                        SetFormMode(FormMode.View);
                    }
                    else
                    {
                        MessageBox.Show("Không thể thêm nhân viên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (currentMode == FormMode.Edit)
                {
                    // Update employee
                    string updateQuery = @"
                        UPDATE NHANVIEN 
                        SET TenNV = @TenNV, SDT = @SDT, ChucVu = @ChucVu
                        WHERE MaNV = @MaNV";
                      int result = DataProvider.ExecuteNonQuery(updateQuery, new object[] 
                    {
                        txtFullName.Text,
                        txtPhone.Text,
                        cmbPosition.Text,
                        currentEmployeeID
                    });

                    if (result > 0)
                    {
                        MessageBox.Show("Cập nhật thông tin nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadEmployees();
                        SetFormMode(FormMode.View);
                    }
                    else
                    {
                        MessageBox.Show("Không thể cập nhật thông tin nhân viên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu thông tin nhân viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            SetFormMode(FormMode.View);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvEmployees_SelectionChanged(object sender, EventArgs e)
        {
            btnEdit.Enabled = dgvEmployees.SelectedRows.Count > 0;
            btnDelete.Enabled = dgvEmployees.SelectedRows.Count > 0;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchEmployees();
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                SearchEmployees();
            }
        }

        private void SearchEmployees()
        {
            try
            {
                string searchText = txtSearch.Text.Trim();
                
                if (string.IsNullOrEmpty(searchText))
                {
                    LoadEmployees();
                    return;
                }

                string query = @"
                    SELECT 
                        nv.MaNV AS 'Mã nhân viên',
                        nv.TenNV AS 'Họ và tên',
                        nv.SDT AS 'Số điện thoại',
                        nv.ChucVu AS 'Chức vụ',
                        tk.TenTaiKhoan AS 'Tài khoản'
                    FROM NHANVIEN nv
                    LEFT JOIN TAIKHOAN tk ON nv.MaNV = tk.MaNV
                    WHERE nv.MaNV LIKE N'%' + @SearchText + '%'
                        OR nv.TenNV LIKE N'%' + @SearchText + '%'
                        OR nv.SDT LIKE N'%' + @SearchText + '%'                        OR nv.ChucVu LIKE N'%' + @SearchText + '%'
                    ORDER BY nv.MaNV";

                DataTable dt = DataProvider.ExecuteQuery(query, new object[] { searchText });
                dgvEmployees.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClearSearch_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            LoadEmployees();
        }
    }
}
