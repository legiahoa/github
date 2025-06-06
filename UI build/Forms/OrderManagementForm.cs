using System;
using System.Data;
using System.Windows.Forms;
using CoffeeManagement.DAO;

namespace CoffeeManagement.Forms
{
    public partial class OrderManagementForm : Form
    {
        public OrderManagementForm()
        {
            InitializeComponent();
        }

        private void OrderManagementForm_Load(object sender, EventArgs e)
        {
            LoadOrders();
            LoadTables();
            LoadOrderStatuses();
        }

        private void LoadOrders()
        {
            try
            {
                string query = @"SELECT dh.MADH, kh.HOTEN as TENKHACHHANG, b.TENB, dh.NGAYDAT, 
                                dh.TONGTIEN, dh.TRANGTHAI
                                FROM DONDATHANG dh
                                INNER JOIN KHACHHANG kh ON dh.MAKH = kh.MAKH
                                INNER JOIN BAN b ON dh.MAB = b.MAB
                                ORDER BY dh.NGAYDAT DESC";
                
                DataTable orderData = DataProvider.ExecuteQuery(query);
                dgvOrders.DataSource = orderData;

                // Customize columns
                dgvOrders.Columns["MADH"].HeaderText = "Mã đơn hàng";
                dgvOrders.Columns["TENKHACHHANG"].HeaderText = "Khách hàng";
                dgvOrders.Columns["TENB"].HeaderText = "Bàn";
                dgvOrders.Columns["NGAYDAT"].HeaderText = "Ngày đặt";
                dgvOrders.Columns["TONGTIEN"].HeaderText = "Tổng tiền";
                dgvOrders.Columns["TRANGTHAI"].HeaderText = "Trạng thái";

                // Format currency columns
                dgvOrders.Columns["TONGTIEN"].DefaultCellStyle.Format = "C0";
                dgvOrders.Columns["TONGTIEN"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                // Auto resize columns
                dgvOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải đơn hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadTables()
        {
            try
            {
                // Add "All Tables" option
                DataTable tableData = new DataTable();
                tableData.Columns.Add("MAB", typeof(string));
                tableData.Columns.Add("TENB", typeof(string));
                
                DataRow allRow = tableData.NewRow();
                allRow["MAB"] = "";
                allRow["TENB"] = "Tất cả";
                tableData.Rows.Add(allRow);

                // Load actual tables
                string query = "SELECT MAB, TENB FROM BAN ORDER BY TENB";
                DataTable tables = DataProvider.ExecuteQuery(query);
                
                foreach (DataRow row in tables.Rows)
                {
                    tableData.ImportRow(row);
                }

                cmbTableFilter.DataSource = tableData;
                cmbTableFilter.DisplayMember = "TENB";
                cmbTableFilter.ValueMember = "MAB";
                cmbTableFilter.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách bàn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadOrderStatuses()
        {
            try
            {
                // Status filter combo
                DataTable statusFilterData = new DataTable();
                statusFilterData.Columns.Add("Value", typeof(string));
                statusFilterData.Columns.Add("Text", typeof(string));
                
                statusFilterData.Rows.Add("", "Tất cả");
                statusFilterData.Rows.Add("Đang xử lý", "Đang xử lý");
                statusFilterData.Rows.Add("Đang chuẩn bị", "Đang chuẩn bị");
                statusFilterData.Rows.Add("Đã hoàn thành", "Đã hoàn thành");
                statusFilterData.Rows.Add("Đã hủy", "Đã hủy");

                cmbStatusFilter.DataSource = statusFilterData;
                cmbStatusFilter.DisplayMember = "Text";
                cmbStatusFilter.ValueMember = "Value";
                cmbStatusFilter.SelectedIndex = 0;

                // New status combo
                DataTable newStatusData = new DataTable();
                newStatusData.Columns.Add("Value", typeof(string));
                newStatusData.Columns.Add("Text", typeof(string));
                
                newStatusData.Rows.Add("Đang xử lý", "Đang xử lý");
                newStatusData.Rows.Add("Đang chuẩn bị", "Đang chuẩn bị");
                newStatusData.Rows.Add("Đã hoàn thành", "Đã hoàn thành");

                cmbNewStatus.DataSource = newStatusData;
                cmbNewStatus.DisplayMember = "Text";
                cmbNewStatus.ValueMember = "Value";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải trạng thái: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyFilters()
        {
            try
            {
                string baseQuery = @"SELECT dh.MADH, kh.HOTEN as TENKHACHHANG, b.TENB, dh.NGAYDAT, 
                                    dh.TONGTIEN, dh.TRANGTHAI
                                    FROM DONDATHANG dh
                                    INNER JOIN KHACHHANG kh ON dh.MAKH = kh.MAKH
                                    INNER JOIN BAN b ON dh.MAB = b.MAB WHERE 1=1";

                string whereClause = "";
                object[] parameters = new object[0];
                int paramIndex = 0;

                // Customer name filter
                if (!string.IsNullOrEmpty(txtSearchCustomer.Text.Trim()))
                {
                    whereClause += " AND kh.HOTEN LIKE @param" + paramIndex;
                    Array.Resize(ref parameters, parameters.Length + 1);
                    parameters[paramIndex] = "%" + txtSearchCustomer.Text.Trim() + "%";
                    paramIndex++;
                }

                // Table filter
                if (cmbTableFilter.SelectedValue != null && !string.IsNullOrEmpty(cmbTableFilter.SelectedValue.ToString()))
                {
                    whereClause += " AND dh.MAB = @param" + paramIndex;
                    Array.Resize(ref parameters, parameters.Length + 1);
                    parameters[paramIndex] = cmbTableFilter.SelectedValue;
                    paramIndex++;
                }

                // Status filter
                if (cmbStatusFilter.SelectedValue != null && !string.IsNullOrEmpty(cmbStatusFilter.SelectedValue.ToString()))
                {
                    whereClause += " AND dh.TRANGTHAI = @param" + paramIndex;
                    Array.Resize(ref parameters, parameters.Length + 1);
                    parameters[paramIndex] = cmbStatusFilter.SelectedValue;
                    paramIndex++;
                }

                string finalQuery = baseQuery + whereClause + " ORDER BY dh.NGAYDAT DESC";
                DataTable filteredData = DataProvider.ExecuteQuery(finalQuery, parameters);
                dgvOrders.DataSource = filteredData;

                // Re-apply column formatting
                if (dgvOrders.Columns.Contains("TONGTIEN"))
                {
                    dgvOrders.Columns["TONGTIEN"].DefaultCellStyle.Format = "C0";
                    dgvOrders.Columns["TONGTIEN"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lọc đơn hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvOrders_SelectionChanged(object sender, EventArgs e)
        {
            bool hasSelection = dgvOrders.SelectedRows.Count > 0;
            btnViewDetails.Enabled = hasSelection;
            btnUpdateStatus.Enabled = hasSelection;
            btnCancelOrder.Enabled = hasSelection;

            if (hasSelection)
            {
                string currentStatus = dgvOrders.SelectedRows[0].Cells["TRANGTHAI"].Value.ToString();
                btnCancelOrder.Enabled = currentStatus != "Đã hủy" && currentStatus != "Đã hoàn thành";
                btnUpdateStatus.Enabled = currentStatus != "Đã hủy" && currentStatus != "Đã hoàn thành";
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadOrders();
        }

        private void txtSearchCustomer_TextChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void cmbTableFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void cmbStatusFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void btnUpdateStatus_Click(object sender, EventArgs e)
        {
            if (dgvOrders.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn đơn hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbNewStatus.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn trạng thái mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string orderId = dgvOrders.SelectedRows[0].Cells["MADH"].Value.ToString();
                string newStatus = cmbNewStatus.SelectedValue.ToString();
                string currentStatus = dgvOrders.SelectedRows[0].Cells["TRANGTHAI"].Value.ToString();

                if (currentStatus == newStatus)
                {
                    MessageBox.Show("Trạng thái mới giống với trạng thái hiện tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string updateQuery = "UPDATE DONDATHANG SET TRANGTHAI = @newStatus WHERE MADH = @orderId";
                int result = DataProvider.ExecuteNonQuery(updateQuery, new object[] { newStatus, orderId });

                if (result > 0)
                {
                    MessageBox.Show($"Cập nhật trạng thái đơn hàng {orderId} thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ApplyFilters(); // Refresh data
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra khi cập nhật trạng thái!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelOrder_Click(object sender, EventArgs e)
        {
            if (dgvOrders.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn đơn hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string orderId = dgvOrders.SelectedRows[0].Cells["MADH"].Value.ToString();
            string currentStatus = dgvOrders.SelectedRows[0].Cells["TRANGTHAI"].Value.ToString();

            if (currentStatus == "Đã hủy")
            {
                MessageBox.Show("Đơn hàng đã được hủy trước đó!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (currentStatus == "Đã hoàn thành")
            {
                MessageBox.Show("Không thể hủy đơn hàng đã hoàn thành!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirmResult = MessageBox.Show($"Bạn có chắc muốn hủy đơn hàng {orderId}?", 
                "Xác nhận hủy đơn hàng", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    string updateQuery = "UPDATE DONDATHANG SET TRANGTHAI = 'Đã hủy' WHERE MADH = @orderId";
                    int result = DataProvider.ExecuteNonQuery(updateQuery, new object[] { orderId });

                    if (result > 0)
                    {
                        MessageBox.Show($"Hủy đơn hàng {orderId} thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ApplyFilters(); // Refresh data
                    }
                    else
                    {
                        MessageBox.Show("Có lỗi xảy ra khi hủy đơn hàng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnViewDetails_Click(object sender, EventArgs e)
        {
            if (dgvOrders.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn đơn hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string orderId = dgvOrders.SelectedRows[0].Cells["MADH"].Value.ToString();
                
                string query = @"SELECT sp.TENSP, ct.SOLUONG, ct.DONGIA, (ct.SOLUONG * ct.DONGIA) as THANHTIEN
                                FROM CHITIETDATHANG ct
                                INNER JOIN SANPHAM sp ON ct.MASP = sp.MASP
                                WHERE ct.MADH = @orderId";
                
                DataTable detailData = DataProvider.ExecuteQuery(query, new object[] { orderId });

                if (detailData.Rows.Count > 0)
                {
                    string details = $"Chi tiết đơn hàng: {orderId}\n\n";
                    decimal total = 0;

                    foreach (DataRow row in detailData.Rows)
                    {
                        string productName = row["TENSP"].ToString();
                        int quantity = Convert.ToInt32(row["SOLUONG"]);
                        decimal price = Convert.ToDecimal(row["DONGIA"]);
                        decimal subtotal = Convert.ToDecimal(row["THANHTIEN"]);

                        details += $"• {productName}\n";
                        details += $"  Số lượng: {quantity} x {price:C0} = {subtotal:C0}\n\n";
                        total += subtotal;
                    }

                    details += $"TỔNG CỘNG: {total:C0}";

                    MessageBox.Show(details, "Chi tiết đơn hàng", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Không tìm thấy chi tiết đơn hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xem chi tiết: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
