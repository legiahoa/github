using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CoffeeManagement.DAO;

namespace CoffeeManagement.Forms
{
    public partial class InventoryManagementForm : Form
    {
        private bool isEdit = false;
        private string currentInventoryId = "";

        public InventoryManagementForm()
        {
            InitializeComponent();
        }

        private void InventoryManagementForm_Load(object sender, EventArgs e)
        {
            LoadInventory();
            LoadProducts();
            ClearInputs();
        }

        private void LoadInventory()
        {
            try
            {
                string query = @"SELECT k.MAKHO, sp.TENSP, k.SOLUONG, k.SOLUONGTOITHIEU, 
                               k.NGAYNHAP, k.HANSUDUNG, k.TRANGTHAI,
                               CASE 
                                   WHEN k.SOLUONG <= k.SOLUONGTOITHIEU THEN N'Thiếu hàng'
                                   WHEN k.HANSUDUNG < GETDATE() THEN N'Hết hạn'
                                   ELSE N'Bình thường'
                               END as TinhTrang
                               FROM KHOHANGHOA k
                               INNER JOIN SANPHAM sp ON k.MASP = sp.MASP
                               ORDER BY k.MAKHO";

                DataTable inventory = DataProvider.ExecuteQuery(query);
                dgvInventory.DataSource = inventory;

                // Configure DataGridView
                dgvInventory.Columns["MAKHO"].HeaderText = "Mã kho";
                dgvInventory.Columns["TENSP"].HeaderText = "Tên sản phẩm";
                dgvInventory.Columns["SOLUONG"].HeaderText = "Số lượng";
                dgvInventory.Columns["SOLUONGTOITHIEU"].HeaderText = "SL tối thiểu";
                dgvInventory.Columns["NGAYNHAP"].HeaderText = "Ngày nhập";
                dgvInventory.Columns["HANSUDUNG"].HeaderText = "Hạn sử dụng";
                dgvInventory.Columns["TRANGTHAI"].HeaderText = "Trạng thái";
                dgvInventory.Columns["TinhTrang"].HeaderText = "Tình trạng";

                dgvInventory.Columns["MAKHO"].Width = 100;
                dgvInventory.Columns["TENSP"].Width = 200;
                dgvInventory.Columns["SOLUONG"].Width = 100;
                dgvInventory.Columns["SOLUONGTOITHIEU"].Width = 120;
                dgvInventory.Columns["NGAYNHAP"].Width = 120;
                dgvInventory.Columns["HANSUDUNG"].Width = 120;
                dgvInventory.Columns["TRANGTHAI"].Width = 100;
                dgvInventory.Columns["TinhTrang"].Width = 120;

                // Color coding for status
                foreach (DataGridViewRow row in dgvInventory.Rows)
                {
                    string status = row.Cells["TinhTrang"].Value?.ToString();
                    if (status == "Thiếu hàng")
                        row.DefaultCellStyle.BackColor = Color.FromArgb(255, 235, 235);
                    else if (status == "Hết hạn")
                        row.DefaultCellStyle.BackColor = Color.FromArgb(255, 220, 220);
                }

                lblTotalItems.Text = $"Tổng số mặt hàng: {inventory.Rows.Count}";

                // Load statistics
                LoadInventoryStats();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải kho hàng hóa: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadInventoryStats()
        {
            try
            {
                // Count low stock items
                string lowStockQuery = "SELECT COUNT(*) FROM KHOHANGHOA WHERE SOLUONG <= SOLUONGTOITHIEU";
                DataTable lowStockResult = DataProvider.ExecuteQuery(lowStockQuery);
                int lowStockCount = Convert.ToInt32(lowStockResult.Rows[0][0]);

                // Count expired items
                string expiredQuery = "SELECT COUNT(*) FROM KHOHANGHOA WHERE HANSUDUNG < GETDATE()";
                DataTable expiredResult = DataProvider.ExecuteQuery(expiredQuery);
                int expiredCount = Convert.ToInt32(expiredResult.Rows[0][0]);

                lblLowStock.Text = $"Thiếu hàng: {lowStockCount}";
                lblExpired.Text = $"Hết hạn: {expiredCount}";

                // Set warning colors
                if (lowStockCount > 0)
                    lblLowStock.ForeColor = Color.Orange;
                if (expiredCount > 0)
                    lblExpired.ForeColor = Color.Red;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải thống kê: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadProducts()
        {
            try
            {
                string query = "SELECT MASP, TENSP FROM SANPHAM WHERE TRANGTHAI = N'Hoạt động' ORDER BY TENSP";
                DataTable products = DataProvider.ExecuteQuery(query);

                cboProduct.DataSource = products;
                cboProduct.DisplayMember = "TENSP";
                cboProduct.ValueMember = "MASP";
                cboProduct.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách sản phẩm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearInputs()
        {
            txtInventoryId.Clear();
            cboProduct.SelectedIndex = -1;
            nudQuantity.Value = 0;
            nudMinQuantity.Value = 1;
            dtpImportDate.Value = DateTime.Now;
            dtpExpiryDate.Value = DateTime.Now.AddMonths(6);
            cboStatus.SelectedIndex = 0;
            isEdit = false;
            currentInventoryId = "";
            
            btnSave.Text = "Thêm";
            btnCancel.Text = "Làm mới";
        }

        private string GenerateInventoryId()
        {
            try
            {
                string query = "SELECT TOP 1 MAKHO FROM KHOHANGHOA ORDER BY MAKHO DESC";
                DataTable result = DataProvider.ExecuteQuery(query);

                if (result.Rows.Count > 0)
                {
                    string lastId = result.Rows[0]["MAKHO"].ToString();
                    int number = int.Parse(lastId.Substring(2)) + 1;
                    return "KH" + number.ToString("D3");
                }
                else
                {
                    return "KH001";
                }
            }
            catch
            {
                return "KH001";
            }
        }

        private bool ValidateInputs()
        {
            if (cboProduct.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboProduct.Focus();
                return false;
            }

            if (nudQuantity.Value < 0)
            {
                MessageBox.Show("Số lượng phải lớn hơn hoặc bằng 0!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nudQuantity.Focus();
                return false;
            }

            if (nudMinQuantity.Value <= 0)
            {
                MessageBox.Show("Số lượng tối thiểu phải lớn hơn 0!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nudMinQuantity.Focus();
                return false;
            }

            if (dtpExpiryDate.Value <= dtpImportDate.Value)
            {
                MessageBox.Show("Hạn sử dụng phải sau ngày nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpExpiryDate.Focus();
                return false;
            }

            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            try
            {
                if (isEdit)
                {
                    // Update inventory
                    string updateQuery = @"UPDATE KHOHANGHOA SET 
                                         MASP = @productId, 
                                         SOLUONG = @quantity, 
                                         SOLUONGTOITHIEU = @minQuantity,
                                         NGAYNHAP = @importDate,
                                         HANSUDUNG = @expiryDate,
                                         TRANGTHAI = @status 
                                         WHERE MAKHO = @inventoryId";

                    int result = DataProvider.ExecuteNonQuery(updateQuery, new object[] {
                        cboProduct.SelectedValue,
                        nudQuantity.Value,
                        nudMinQuantity.Value,
                        dtpImportDate.Value,
                        dtpExpiryDate.Value,
                        cboStatus.Text,
                        currentInventoryId
                    });

                    if (result > 0)
                    {
                        MessageBox.Show("Cập nhật kho hàng hóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadInventory();
                        ClearInputs();
                    }
                }
                else
                {
                    // Check if product already exists in inventory
                    string checkQuery = "SELECT COUNT(*) FROM KHOHANGHOA WHERE MASP = @productId";
                    DataTable checkResult = DataProvider.ExecuteQuery(checkQuery, new object[] { cboProduct.SelectedValue });

                    if (Convert.ToInt32(checkResult.Rows[0][0]) > 0)
                    {
                        MessageBox.Show("Sản phẩm này đã có trong kho!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cboProduct.Focus();
                        return;
                    }

                    // Add new inventory item
                    string inventoryId = GenerateInventoryId();
                    string insertQuery = @"INSERT INTO KHOHANGHOA (MAKHO, MASP, SOLUONG, SOLUONGTOITHIEU, NGAYNHAP, HANSUDUNG, TRANGTHAI) 
                                         VALUES (@inventoryId, @productId, @quantity, @minQuantity, @importDate, @expiryDate, @status)";

                    int result = DataProvider.ExecuteNonQuery(insertQuery, new object[] {
                        inventoryId,
                        cboProduct.SelectedValue,
                        nudQuantity.Value,
                        nudMinQuantity.Value,
                        dtpImportDate.Value,
                        dtpExpiryDate.Value,
                        cboStatus.Text
                    });

                    if (result > 0)
                    {
                        MessageBox.Show("Thêm vào kho hàng hóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadInventory();
                        ClearInputs();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu kho hàng hóa: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvInventory.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn mặt hàng cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DataGridViewRow selectedRow = dgvInventory.SelectedRows[0];
                currentInventoryId = selectedRow.Cells["MAKHO"].Value.ToString();
                
                // Get full inventory data
                string query = "SELECT * FROM KHOHANGHOA WHERE MAKHO = @inventoryId";
                DataTable result = DataProvider.ExecuteQuery(query, new object[] { currentInventoryId });
                
                if (result.Rows.Count > 0)
                {
                    DataRow row = result.Rows[0];
                    
                    txtInventoryId.Text = currentInventoryId;
                    cboProduct.SelectedValue = row["MASP"];
                    nudQuantity.Value = Convert.ToDecimal(row["SOLUONG"]);
                    nudMinQuantity.Value = Convert.ToDecimal(row["SOLUONGTOITHIEU"]);
                    dtpImportDate.Value = Convert.ToDateTime(row["NGAYNHAP"]);
                    dtpExpiryDate.Value = Convert.ToDateTime(row["HANSUDUNG"]);
                    cboStatus.Text = row["TRANGTHAI"].ToString();

                    isEdit = true;
                    btnSave.Text = "Cập nhật";
                    btnCancel.Text = "Hủy";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải thông tin mặt hàng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvInventory.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn mặt hàng cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string inventoryId = dgvInventory.SelectedRows[0].Cells["MAKHO"].Value.ToString();
            string productName = dgvInventory.SelectedRows[0].Cells["TENSP"].Value.ToString();

            if (MessageBox.Show($"Bạn có chắc chắn muốn xóa mặt hàng '{productName}' khỏi kho?", "Xác nhận", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    string deleteQuery = "DELETE FROM KHOHANGHOA WHERE MAKHO = @inventoryId";
                    int result = DataProvider.ExecuteNonQuery(deleteQuery, new object[] { inventoryId });

                    if (result > 0)
                    {
                        MessageBox.Show("Xóa mặt hàng khỏi kho thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadInventory();
                        ClearInputs();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xóa mặt hàng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (isEdit)
            {
                ClearInputs();
            }
            else
            {
                ClearInputs();
                LoadInventory();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string searchText = txtSearch.Text.Trim();
                string filter = cboFilter.Text;
                string query;

                if (string.IsNullOrWhiteSpace(searchText) && filter == "Tất cả")
                {
                    LoadInventory();
                    return;
                }

                query = @"SELECT k.MAKHO, sp.TENSP, k.SOLUONG, k.SOLUONGTOITHIEU, 
                         k.NGAYNHAP, k.HANSUDUNG, k.TRANGTHAI,
                         CASE 
                             WHEN k.SOLUONG <= k.SOLUONGTOITHIEU THEN N'Thiếu hàng'
                             WHEN k.HANSUDUNG < GETDATE() THEN N'Hết hạn'
                             ELSE N'Bình thường'
                         END as TinhTrang
                         FROM KHOHANGHOA k
                         INNER JOIN SANPHAM sp ON k.MASP = sp.MASP
                         WHERE (k.MAKHO LIKE @search OR sp.TENSP LIKE @search)";

                // Add filter condition
                if (filter == "Thiếu hàng")
                    query += " AND k.SOLUONG <= k.SOLUONGTOITHIEU";
                else if (filter == "Hết hạn")
                    query += " AND k.HANSUDUNG < GETDATE()";
                else if (filter == "Bình thường")
                    query += " AND k.SOLUONG > k.SOLUONGTOITHIEU AND k.HANSUDUNG >= GETDATE()";

                query += " ORDER BY k.MAKHO";

                DataTable result = DataProvider.ExecuteQuery(query, new object[] { $"%{searchText}%" });
                dgvInventory.DataSource = result;

                lblTotalItems.Text = $"Tìm thấy: {result.Rows.Count} mặt hàng";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnSearch_Click(sender, e);
            }
        }

        private void dgvInventory_SelectionChanged(object sender, EventArgs e)
        {
            btnEdit.Enabled = dgvInventory.SelectedRows.Count > 0;
            btnDelete.Enabled = dgvInventory.SelectedRows.Count > 0;
        }

        private void btnUpdateStock_Click(object sender, EventArgs e)
        {
            if (dgvInventory.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn mặt hàng cần cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string inventoryId = dgvInventory.SelectedRows[0].Cells["MAKHO"].Value.ToString();
            string productName = dgvInventory.SelectedRows[0].Cells["TENSP"].Value.ToString();
            decimal currentStock = Convert.ToDecimal(dgvInventory.SelectedRows[0].Cells["SOLUONG"].Value);

            StockUpdateForm stockForm = new StockUpdateForm(inventoryId, productName, currentStock);
            if (stockForm.ShowDialog() == DialogResult.OK)
            {
                LoadInventory();
            }
        }
    }

    // Simple form for stock updates
    public partial class StockUpdateForm : Form
    {
        private string inventoryId;
        private NumericUpDown nudAdjustment;
        private ComboBox cboAdjustmentType;
        private Button btnOK, btnCancel;

        public StockUpdateForm(string inventoryId, string productName, decimal currentStock)
        {
            this.inventoryId = inventoryId;
            InitializeComponents(productName, currentStock);
        }

        private void InitializeComponents(string productName, decimal currentStock)
        {
            this.Text = "Cập nhật tồn kho";
            this.Size = new Size(400, 250);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            Label lblProduct = new Label { Text = $"Sản phẩm: {productName}", Location = new Point(20, 20), Size = new Size(350, 20) };
            Label lblCurrentStock = new Label { Text = $"Tồn kho hiện tại: {currentStock}", Location = new Point(20, 50), Size = new Size(200, 20) };
            Label lblAdjustmentType = new Label { Text = "Loại điều chỉnh:", Location = new Point(20, 80), Size = new Size(100, 20) };
            
            cboAdjustmentType = new ComboBox 
            { 
                Location = new Point(130, 78), 
                Size = new Size(150, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cboAdjustmentType.Items.AddRange(new object[] { "Nhập thêm", "Xuất hàng", "Điều chỉnh" });
            cboAdjustmentType.SelectedIndex = 0;

            Label lblQuantity = new Label { Text = "Số lượng:", Location = new Point(20, 120), Size = new Size(100, 20) };
            nudAdjustment = new NumericUpDown 
            { 
                Location = new Point(130, 118), 
                Size = new Size(150, 25),
                Minimum = 0,
                Maximum = 9999,
                DecimalPlaces = 0
            };

            btnOK = new Button { Text = "OK", Location = new Point(200, 170), Size = new Size(80, 30) };
            btnOK.Click += BtnOK_Click;

            btnCancel = new Button { Text = "Hủy", Location = new Point(290, 170), Size = new Size(80, 30) };
            btnCancel.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };

            this.Controls.AddRange(new Control[] { lblProduct, lblCurrentStock, lblAdjustmentType, cboAdjustmentType, lblQuantity, nudAdjustment, btnOK, btnCancel });
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            try
            {
                decimal adjustment = nudAdjustment.Value;
                string adjustmentType = cboAdjustmentType.Text;
                
                if (adjustment == 0)
                {
                    MessageBox.Show("Vui lòng nhập số lượng cần điều chỉnh!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string updateQuery = "";
                if (adjustmentType == "Nhập thêm")
                    updateQuery = "UPDATE KHOHANGHOA SET SOLUONG = SOLUONG + @adjustment WHERE MAKHO = @inventoryId";
                else if (adjustmentType == "Xuất hàng")
                    updateQuery = "UPDATE KHOHANGHOA SET SOLUONG = CASE WHEN SOLUONG - @adjustment >= 0 THEN SOLUONG - @adjustment ELSE 0 END WHERE MAKHO = @inventoryId";
                else // Điều chỉnh
                    updateQuery = "UPDATE KHOHANGHOA SET SOLUONG = @adjustment WHERE MAKHO = @inventoryId";

                int result = DataProvider.ExecuteNonQuery(updateQuery, new object[] { adjustment, inventoryId });

                if (result > 0)
                {
                    MessageBox.Show("Cập nhật tồn kho thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật tồn kho: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
