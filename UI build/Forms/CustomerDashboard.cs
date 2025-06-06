using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CoffeeManagement.DAO;

namespace CoffeeManagement.Forms
{
    public partial class CustomerDashboard : Form
    {
        private string accountId;
        private string customerName;
        private string customerId;

        public CustomerDashboard(string accountId)
        {
            InitializeComponent();
            this.accountId = accountId;
            LoadCustomerInfo();
        }

        private void LoadCustomerInfo()
        {
            try
            {
                string query = "SELECT kh.MAKH, kh.HOTEN FROM KHACHHANG kh INNER JOIN TAIKHOAN tk ON kh.MATK = tk.MATK WHERE tk.MATK = @accountId";
                DataTable result = DataProvider.ExecuteQuery(query, new object[] { accountId });

                if (result.Rows.Count > 0)
                {
                    customerId = result.Rows[0]["MAKH"].ToString();
                    customerName = result.Rows[0]["HOTEN"].ToString();
                    lblWelcome.Text = $"Xin chào, {customerName}!";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải thông tin khách hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CustomerDashboard_Load(object sender, EventArgs e)
        {
            LoadMenu();
            LoadTables();
        }

        private void LoadMenu()
        {
            try
            {
                string query = @"SELECT sp.MASP, sp.TENSP, sp.GIASP, sp.MOTA, dm.TENDM
                                FROM SANPHAM sp 
                                INNER JOIN DANHMUC dm ON sp.MADM = dm.MADM 
                                WHERE sp.TRANGTHAI = 1
                                ORDER BY dm.TENDM, sp.TENSP";
                
                DataTable menuData = DataProvider.ExecuteQuery(query);
                dgvMenu.DataSource = menuData;

                // Customize columns
                dgvMenu.Columns["MASP"].HeaderText = "Mã SP";
                dgvMenu.Columns["TENSP"].HeaderText = "Tên sản phẩm";
                dgvMenu.Columns["GIASP"].HeaderText = "Giá";
                dgvMenu.Columns["MOTA"].HeaderText = "Mô tả";
                dgvMenu.Columns["TENDM"].HeaderText = "Danh mục";

                dgvMenu.Columns["GIASP"].DefaultCellStyle.Format = "C0";
                dgvMenu.Columns["GIASP"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải menu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadTables()
        {
            try
            {
                string query = @"SELECT b.MAB, b.TENB, kv.TENKV, 
                                CASE WHEN b.TRANGTHAI = 1 THEN N'Trống' ELSE N'Có khách' END as TRANGTHAI
                                FROM BAN b 
                                INNER JOIN KHUVUC kv ON b.MAKV = kv.MAKV 
                                ORDER BY kv.TENKV, b.TENB";
                
                DataTable tableData = DataProvider.ExecuteQuery(query);
                cmbTable.DataSource = tableData;
                cmbTable.DisplayMember = "TENB";
                cmbTable.ValueMember = "MAB";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách bàn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            if (dgvMenu.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (nudQuantity.Value <= 0)
            {
                MessageBox.Show("Vui lòng nhập số lượng hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow selectedRow = dgvMenu.SelectedRows[0];
            string productId = selectedRow.Cells["MASP"].Value.ToString();
            string productName = selectedRow.Cells["TENSP"].Value.ToString();
            decimal price = Convert.ToDecimal(selectedRow.Cells["GIASP"].Value);
            int quantity = (int)nudQuantity.Value;

            // Add to cart (DataGridView)
            bool found = false;
            foreach (DataGridViewRow row in dgvCart.Rows)
            {
                if (row.Cells["ProductId"].Value?.ToString() == productId)
                {
                    int existingQty = Convert.ToInt32(row.Cells["Quantity"].Value);
                    int newQty = existingQty + quantity;
                    row.Cells["Quantity"].Value = newQty;
                    row.Cells["Total"].Value = newQty * price;
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                dgvCart.Rows.Add(productId, productName, price, quantity, price * quantity);
            }

            UpdateTotalAmount();
            nudQuantity.Value = 1;
        }

        private void UpdateTotalAmount()
        {
            decimal total = 0;
            foreach (DataGridViewRow row in dgvCart.Rows)
            {
                if (row.Cells["Total"].Value != null)
                {
                    total += Convert.ToDecimal(row.Cells["Total"].Value);
                }
            }
            lblTotalAmount.Text = total.ToString("C0");
        }

        private void btnRemoveFromCart_Click(object sender, EventArgs e)
        {
            if (dgvCart.SelectedRows.Count > 0)
            {
                dgvCart.Rows.Remove(dgvCart.SelectedRows[0]);
                UpdateTotalAmount();
            }
        }

        private void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            if (dgvCart.Rows.Count == 0)
            {
                MessageBox.Show("Giỏ hàng trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbTable.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn bàn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Generate order ID
                string orderQuery = "SELECT COUNT(*) FROM DONDATHANG";
                DataTable orderResult = DataProvider.ExecuteQuery(orderQuery);
                int orderCount = Convert.ToInt32(orderResult.Rows[0][0]) + 1;
                string orderId = "DH" + orderCount.ToString("D3");

                // Create order
                string insertOrderQuery = @"INSERT INTO DONDATHANG (MADH, MAKH, MAB, NGAYDAT, TONGTIEN, TRANGTHAI) 
                                           VALUES (@orderId, @customerId, @tableId, @orderDate, @totalAmount, @status)";
                
                decimal totalAmount = 0;
                foreach (DataGridViewRow row in dgvCart.Rows)
                {
                    if (row.Cells["Total"].Value != null)
                    {
                        totalAmount += Convert.ToDecimal(row.Cells["Total"].Value);
                    }
                }

                int result = DataProvider.ExecuteNonQuery(insertOrderQuery, new object[] 
                { 
                    orderId, customerId, cmbTable.SelectedValue, DateTime.Now, totalAmount, "Đang xử lý" 
                });

                if (result > 0)
                {
                    // Add order details
                    foreach (DataGridViewRow row in dgvCart.Rows)
                    {
                        if (row.Cells["ProductId"].Value != null)
                        {
                            string insertDetailQuery = @"INSERT INTO CHITIETDATHANG (MADH, MASP, SOLUONG, DONGIA) 
                                                        VALUES (@orderId, @productId, @quantity, @price)";
                            
                            DataProvider.ExecuteNonQuery(insertDetailQuery, new object[]
                            {
                                orderId,
                                row.Cells["ProductId"].Value,
                                row.Cells["Quantity"].Value,
                                row.Cells["Price"].Value
                            });
                        }
                    }

                    MessageBox.Show($"Đặt hàng thành công! Mã đơn hàng: {orderId}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    // Clear cart
                    dgvCart.Rows.Clear();
                    UpdateTotalAmount();
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra khi đặt hàng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnViewOrders_Click(object sender, EventArgs e)
        {
            try
            {
                string query = @"SELECT dh.MADH, dh.NGAYDAT, dh.TONGTIEN, dh.TRANGTHAI, b.TENB
                                FROM DONDATHANG dh
                                INNER JOIN BAN b ON dh.MAB = b.MAB
                                WHERE dh.MAKH = @customerId
                                ORDER BY dh.NGAYDAT DESC";
                
                DataTable orderData = DataProvider.ExecuteQuery(query, new object[] { customerId });
                dgvOrders.DataSource = orderData;

                // Customize columns
                dgvOrders.Columns["MADH"].HeaderText = "Mã đơn hàng";
                dgvOrders.Columns["NGAYDAT"].HeaderText = "Ngày đặt";
                dgvOrders.Columns["TONGTIEN"].HeaderText = "Tổng tiền";
                dgvOrders.Columns["TRANGTHAI"].HeaderText = "Trạng thái";
                dgvOrders.Columns["TENB"].HeaderText = "Bàn";

                dgvOrders.Columns["TONGTIEN"].DefaultCellStyle.Format = "C0";
                dgvOrders.Columns["TONGTIEN"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải đơn hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn đăng xuất?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void CustomerDashboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Form will close and return to login
        }
    }
}
