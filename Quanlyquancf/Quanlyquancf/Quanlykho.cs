using Quanlyquancf.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quanlyquancf
{
    public partial class Quanlykho : Form
    {
        private DataTable menuData;
        private int selectedMenuId = -1;

        public Quanlykho()
        {
            InitializeComponent();
            InitializeMenu();
        }

        private void Quanlykho_Load(object sender, EventArgs e)
        {
            LoadMenuData();
            SetupDataGridViewColumns();
        }

        private void InitializeMenu()
        {
            // Initialize sample menu data
            menuData = new DataTable();
            menuData.Columns.Add("ID", typeof(int));
            menuData.Columns.Add("Tên món", typeof(string));
            menuData.Columns.Add("Giá", typeof(decimal));
            menuData.Columns.Add("Danh mục", typeof(string));

            // Add sample data
            AddSampleMenuItems();
        }

        private void AddSampleMenuItems()
        {
            menuData.Rows.Add(1, "Cà phê đen", 25000, "Thức uống");
            menuData.Rows.Add(2, "Cà phê sữa", 30000, "Thức uống");
            menuData.Rows.Add(3, "Bánh mì thịt nướng", 35000, "Món chính");
            menuData.Rows.Add(4, "Phở bò", 55000, "Món chính");
            menuData.Rows.Add(5, "Chả cá", 45000, "Món phụ");
            menuData.Rows.Add(6, "Chè đậu xanh", 20000, "Tráng miệng");
        }

        private void LoadMenuData()
        {
            dgvMon.DataSource = menuData;
        }

        private void SetupDataGridViewColumns()
        {
            if (dgvMon.Columns.Count > 0)
            {
                dgvMon.Columns["ID"].Width = 80;
                dgvMon.Columns["Tên món"].Width = 250;
                dgvMon.Columns["Giá"].Width = 150;
                dgvMon.Columns["Danh mục"].Width = 150;
                
                // Format currency column
                dgvMon.Columns["Giá"].DefaultCellStyle.Format = "N0";
                dgvMon.Columns["Giá"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                string maDonHang = row.Cells["Mã đơn hàng"].Value?.ToString();
                
                if (!string.IsNullOrEmpty(maDonHang))
                {
                    LoadOrderDetails(maDonHang);
                }
            }
        }

        private void LoadOrderDetails(string maDonHang)
        {
            // Clear existing items
            listViewChiTiet.Items.Clear();
            
            // Sample order details - replace with actual database query
            var orderDetails = GetOrderDetailsSample(maDonHang);
            
            decimal tongTien = 0;
            foreach (var detail in orderDetails)
            {
                var item = new ListViewItem(detail.TenMon);
                item.SubItems.Add(detail.SoLuong.ToString());
                item.SubItems.Add(detail.Gia.ToString("N0"));
                item.SubItems.Add(detail.ThanhTien.ToString("N0"));
                
                listViewChiTiet.Items.Add(item);
                tongTien += detail.ThanhTien;
            }
            
            lblTongTienChiTiet.Text = $"Tổng tiền: {tongTien:N0} VNĐ";
            
            // Load order notes
            textBoxGhiChu.Text = GetOrderNotes(maDonHang);
        }

        private List<OrderDetailSample> GetOrderDetailsSample(string maDonHang)
        {
            // Sample data - replace with actual database query
            var details = new List<OrderDetailSample>();
            
            switch (maDonHang)
            {
                case "DH001":
                    details.Add(new OrderDetailSample { TenMon = "Cà phê đen", SoLuong = 2, Gia = 25000, ThanhTien = 50000 });
                    details.Add(new OrderDetailSample { TenMon = "Bánh tiramisu", SoLuong = 1, Gia = 45000, ThanhTien = 45000 });
                    details.Add(new OrderDetailSample { TenMon = "Trà sữa", SoLuong = 1, Gia = 35000, ThanhTien = 35000 });
                    break;
                case "DH002":
                    details.Add(new OrderDetailSample { TenMon = "Cappuccino", SoLuong = 1, Gia = 35000, ThanhTien = 35000 });
                    details.Add(new OrderDetailSample { TenMon = "Sandwich", SoLuong = 2, Gia = 45000, ThanhTien = 90000 });
                    details.Add(new OrderDetailSample { TenMon = "Nước cam", SoLuong = 1, Gia = 25000, ThanhTien = 25000 });
                    break;
                case "DH003":
                    details.Add(new OrderDetailSample { TenMon = "Espresso", SoLuong = 1, Gia = 30000, ThanhTien = 30000 });
                    details.Add(new OrderDetailSample { TenMon = "Croissant", SoLuong = 2, Gia = 25000, ThanhTien = 50000 });
                    break;
            }
            
            return details;
        }

        private string GetOrderNotes(string maDonHang)
        {
            // Sample notes - replace with actual database query
            switch (maDonHang)
            {
                case "DH001": return "Khách yêu cầu ít đường";
                case "DH002": return "Giao hàng tận nơi";
                case "DH003": return "Khách VIP, ưu tiên phục vụ";
                default: return "";
            }
        }

        void ShowOrders()
        {
            // Sample data table for orders
            DataTable ordersTable = new DataTable();
            ordersTable.Columns.Add("Mã đơn hàng", typeof(string));
            ordersTable.Columns.Add("Bàn", typeof(string));
            ordersTable.Columns.Add("Tên khách hàng", typeof(string));
            ordersTable.Columns.Add("Trạng thái", typeof(string));
            ordersTable.Columns.Add("Thời gian", typeof(DateTime));
            ordersTable.Columns.Add("Tổng tiền", typeof(decimal));
            
            // Sample data
            ordersTable.Rows.Add("DH001", "Bàn 01", "Nguyễn Văn A", "Hoàn thành", DateTime.Now.AddHours(-2), 130000);
            ordersTable.Rows.Add("DH002", "Bàn 03", "Trần Thị B", "Đang chuẩn bị", DateTime.Now.AddMinutes(-30), 150000);
            ordersTable.Rows.Add("DH003", "Bàn 05", "Lê Văn C", "Chờ xử lý", DateTime.Now.AddMinutes(-10), 80000);
            ordersTable.Rows.Add("DH004", "Bàn 02", "Phạm Thị D", "Chờ xử lý", DateTime.Now.AddMinutes(-5), 95000);
            ordersTable.Rows.Add("DH005", "Bàn 07", "Hoàng Văn E", "Đang chuẩn bị", DateTime.Now.AddMinutes(-45), 220000);
            
            dataGridView1.DataSource = ordersTable;
            
            // Uncomment the line below and comment the above code when you have the actual database table
            // string query = "SELECT MaDonHang, Ban, TenKhachHang, TrangThai, ThoiGian, TongTien FROM DONHANG ORDER BY ThoiGian DESC";
            // DataProvider provider = new DataProvider();
            // dataGridView1.DataSource = provider.ExcuteQuery(query);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchOrders();
        }

        private void SearchOrders()
        {
            string searchText = textBoxSearch.Text.Trim().ToLower();
            string selectedStatus = comboBoxTrangThai.SelectedItem?.ToString();
            
            if (string.IsNullOrEmpty(searchText) && (selectedStatus == "Tất cả" || string.IsNullOrEmpty(selectedStatus)))
            {
                ShowOrders();
                return;
            }
            
            // Filter logic here - for now showing sample filtered data
            DataTable filteredTable = new DataTable();
            filteredTable.Columns.Add("Mã đơn hàng", typeof(string));
            filteredTable.Columns.Add("Bàn", typeof(string));
            filteredTable.Columns.Add("Tên khách hàng", typeof(string));
            filteredTable.Columns.Add("Trạng thái", typeof(string));
            filteredTable.Columns.Add("Thời gian", typeof(DateTime));
            filteredTable.Columns.Add("Tổng tiền", typeof(decimal));
            
            // Sample filtered data based on search
            if (!string.IsNullOrEmpty(searchText) && searchText.Contains("a"))
            {
                filteredTable.Rows.Add("DH001", "Bàn 01", "Nguyễn Văn A", "Hoàn thành", DateTime.Now.AddHours(-2), 130000);
            }
            
            dataGridView1.DataSource = filteredTable;
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string maDonHang = dataGridView1.SelectedRows[0].Cells["Mã đơn hàng"].Value?.ToString();
                if (!string.IsNullOrEmpty(maDonHang))
                {
                    // Update order status to confirmed
                    MessageBox.Show($"Đã xác nhận đơn hàng {maDonHang}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ShowOrders(); // Refresh the list
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn đơn hàng cần xác nhận!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnHuyDon_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string maDonHang = dataGridView1.SelectedRows[0].Cells["Mã đơn hàng"].Value?.ToString();
                if (!string.IsNullOrEmpty(maDonHang))
                {
                    DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn hủy đơn hàng {maDonHang}?", 
                        "Xác nhận hủy đơn", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    
                    if (result == DialogResult.Yes)
                    {
                        // Update order status to cancelled
                        MessageBox.Show($"Đã hủy đơn hàng {maDonHang}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ShowOrders(); // Refresh the list
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn đơn hàng cần hủy!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnInHoaDon_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string maDonHang = dataGridView1.SelectedRows[0].Cells["Mã đơn hàng"].Value?.ToString();
                if (!string.IsNullOrEmpty(maDonHang))
                {
                    // Print invoice logic here
                    MessageBox.Show($"Đang in hóa đơn cho đơn hàng {maDonHang}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn đơn hàng cần in hóa đơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void comboBoxTrangThai_SelectedIndexChanged(object sender, EventArgs e)
        {
            SearchOrders();
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            // Optional: Auto-search as user types (with delay)
        }
    }

    // Helper class for order details
    public class OrderDetailSample
    {
        public string TenMon { get; set; }
        public int SoLuong { get; set; }
        public decimal Gia { get; set; }
        public decimal ThanhTien { get; set; }
    }
}
