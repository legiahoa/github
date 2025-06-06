using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CoffeeManagement.DAO;

namespace CoffeeManagement.Forms
{
    public partial class AdminDashboard : Form
    {
        private string accountId;
        private string employeeName;

        public AdminDashboard(string accountId)
        {
            InitializeComponent();
            this.accountId = accountId;
            LoadEmployeeInfo();
        }

        private void LoadEmployeeInfo()
        {
            try
            {
                string query = "SELECT nv.HOTEN FROM NHANVIEN nv INNER JOIN TAIKHOAN tk ON nv.MATK = tk.MATK WHERE tk.MATK = @accountId";
                DataTable result = DataProvider.ExecuteQuery(query, new object[] { accountId });

                if (result.Rows.Count > 0)
                {
                    employeeName = result.Rows[0]["HOTEN"].ToString();
                    lblWelcome.Text = $"Xin chào, {employeeName}!";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải thông tin nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AdminDashboard_Load(object sender, EventArgs e)        {
            LoadStatistics();
        }

        private void LoadStatistics()
        {
            try
            {
                // Load statistics
                string customerCountQuery = "SELECT COUNT(*) FROM KHACHHANG";
                string employeeCountQuery = "SELECT COUNT(*) FROM NHANVIEN";
                string productCountQuery = "SELECT COUNT(*) FROM SANPHAM";
                string orderCountQuery = "SELECT COUNT(*) FROM DONDATHANG";

                DataTable customerResult = DataProvider.ExecuteQuery(customerCountQuery);
                DataTable employeeResult = DataProvider.ExecuteQuery(employeeCountQuery);
                DataTable productResult = DataProvider.ExecuteQuery(productCountQuery);
                DataTable orderResult = DataProvider.ExecuteQuery(orderCountQuery);

                lblCustomerCount.Text = customerResult.Rows[0][0].ToString();
                lblEmployeeCount.Text = employeeResult.Rows[0][0].ToString();
                lblProductCount.Text = productResult.Rows[0][0].ToString();
                lblOrderCount.Text = orderResult.Rows[0][0].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải thống kê: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }        private void btnCustomerManagement_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("Customer Management - Đang phát triển", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // CustomerManagementForm customerForm = new CustomerManagementForm();
                // customerForm.ShowDialog();
                LoadStatistics(); // Refresh statistics after form closes
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở form quản lý khách hàng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }        private void btnEmployeeManagement_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("Employee Management - Đang phát triển", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // EmployeeManagementForm employeeForm = new EmployeeManagementForm();
                // employeeForm.ShowDialog();
                LoadStatistics(); // Refresh statistics after form closes
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở form quản lý nhân viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }private void btnProductManagement_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("Product Management - Đang phát triển", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // ProductManagementForm productForm = new ProductManagementForm();
                // productForm.ShowDialog();
                LoadStatistics(); // Refresh statistics after form closes
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở form quản lý sản phẩm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }        private void btnOrderManagement_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("Order Management - Đang phát triển", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);                // OrderManagementForm orderForm = new OrderManagementForm();
                // orderForm.ShowDialog();
                LoadStatistics(); // Refresh statistics after form closes
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở form quản lý đơn hàng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }        private void btnTableManagement_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("Table Management - Đang phát triển", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);                // TableManagementForm tableForm = new TableManagementForm();
                // tableForm.ShowDialog();
                LoadStatistics(); // Refresh statistics after form closes
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở form quản lý bàn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }        private void btnAreaManagement_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("Area Management - Đang phát triển", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);                // AreaManagementForm areaForm = new AreaManagementForm();
                // areaForm.ShowDialog();
                LoadStatistics(); // Refresh statistics after form closes
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở form quản lý khu vực: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }        private void btnInventoryManagement_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("Inventory Management - Đang phát triển", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);                // InventoryManagementForm inventoryForm = new InventoryManagementForm();
                // inventoryForm.ShowDialog();
                LoadStatistics(); // Refresh statistics after form closes
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở form quản lý kho: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }        private void btnReports_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("Reports - Đang phát triển", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // ReportsForm reportsForm = new ReportsForm();
                // reportsForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở form báo cáo: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn đăng xuất?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }        private void AdminDashboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Form will close and return to login
        }
    }
}
