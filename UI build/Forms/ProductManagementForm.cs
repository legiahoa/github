using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CoffeeManagement.DAO;

namespace CoffeeManagement.Forms
{
    public partial class ProductManagementForm : Form
    {
        public ProductManagementForm()
        {
            InitializeComponent();
        }

        private void ProductManagementForm_Load(object sender, EventArgs e)
        {
            LoadProducts();
            LoadCategories();
        }

        private void LoadProducts()
        {
            try
            {
                string query = @"SELECT sp.MASP, sp.TENSP, sp.GIASP, sp.MOTA, dm.TENDM, 
                                CASE WHEN sp.TRANGTHAI = 1 THEN N'Còn bán' ELSE N'Ngừng bán' END as TRANGTHAI
                                FROM SANPHAM sp 
                                INNER JOIN DANHMUC dm ON sp.MADM = dm.MADM 
                                ORDER BY dm.TENDM, sp.TENSP";

                DataTable data = DataProvider.ExecuteQuery(query);
                dgvProducts.DataSource = data;

                // Customize columns
                dgvProducts.Columns["MASP"].HeaderText = "Mã SP";
                dgvProducts.Columns["TENSP"].HeaderText = "Tên sản phẩm";
                dgvProducts.Columns["GIASP"].HeaderText = "Giá";
                dgvProducts.Columns["MOTA"].HeaderText = "Mô tả";
                dgvProducts.Columns["TENDM"].HeaderText = "Danh mục";
                dgvProducts.Columns["TRANGTHAI"].HeaderText = "Trạng thái";

                dgvProducts.Columns["GIASP"].DefaultCellStyle.Format = "C0";
                dgvProducts.Columns["GIASP"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách sản phẩm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCategories()
        {
            try
            {
                string query = "SELECT MADM, TENDM FROM DANHMUC ORDER BY TENDM";
                DataTable data = DataProvider.ExecuteQuery(query);
                
                cmbCategory.DataSource = data;
                cmbCategory.DisplayMember = "TENDM";
                cmbCategory.ValueMember = "MADM";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh mục: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvProducts_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvProducts.SelectedRows[0];
                txtProductId.Text = row.Cells["MASP"].Value.ToString();
                txtProductName.Text = row.Cells["TENSP"].Value.ToString();
                nudPrice.Value = Convert.ToDecimal(row.Cells["GIASP"].Value);
                txtDescription.Text = row.Cells["MOTA"].Value.ToString();
                
                // Set category
                string categoryName = row.Cells["TENDM"].Value.ToString();
                for (int i = 0; i < cmbCategory.Items.Count; i++)
                {
                    DataRowView item = (DataRowView)cmbCategory.Items[i];
                    if (item["TENDM"].ToString() == categoryName)
                    {
                        cmbCategory.SelectedIndex = i;
                        break;
                    }
                }

                chkActive.Checked = row.Cells["TRANGTHAI"].Value.ToString() == "Còn bán";
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ClearInputs();
            GenerateNewProductId();
        }

        private void GenerateNewProductId()
        {
            try
            {
                string query = "SELECT COUNT(*) FROM SANPHAM";
                DataTable result = DataProvider.ExecuteQuery(query);
                int count = Convert.ToInt32(result.Rows[0][0]) + 1;
                txtProductId.Text = "SP" + count.ToString("D3");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tạo mã sản phẩm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearInputs()
        {
            txtProductId.Clear();
            txtProductName.Clear();
            nudPrice.Value = 0;
            txtDescription.Clear();
            cmbCategory.SelectedIndex = -1;
            chkActive.Checked = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateInputs())
            {
                try
                {
                    // Check if product exists
                    string checkQuery = "SELECT COUNT(*) FROM SANPHAM WHERE MASP = @productId";
                    DataTable checkResult = DataProvider.ExecuteQuery(checkQuery, new object[] { txtProductId.Text });
                    bool exists = Convert.ToInt32(checkResult.Rows[0][0]) > 0;

                    string query;
                    object[] parameters;

                    if (exists)
                    {
                        // Update
                        query = @"UPDATE SANPHAM 
                                 SET TENSP = @name, GIASP = @price, MOTA = @description, 
                                     MADM = @categoryId, TRANGTHAI = @status 
                                 WHERE MASP = @productId";
                        parameters = new object[] 
                        { 
                            txtProductName.Text.Trim(),
                            nudPrice.Value,
                            txtDescription.Text.Trim(),
                            cmbCategory.SelectedValue,
                            chkActive.Checked ? 1 : 0,
                            txtProductId.Text
                        };
                    }
                    else
                    {
                        // Insert
                        query = @"INSERT INTO SANPHAM (MASP, TENSP, GIASP, MOTA, MADM, TRANGTHAI) 
                                 VALUES (@productId, @name, @price, @description, @categoryId, @status)";
                        parameters = new object[] 
                        { 
                            txtProductId.Text,
                            txtProductName.Text.Trim(),
                            nudPrice.Value,
                            txtDescription.Text.Trim(),
                            cmbCategory.SelectedValue,
                            chkActive.Checked ? 1 : 0
                        };
                    }

                    int result = DataProvider.ExecuteNonQuery(query, parameters);
                    if (result > 0)
                    {
                        MessageBox.Show("Lưu sản phẩm thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadProducts();
                        ClearInputs();
                    }
                    else
                    {
                        MessageBox.Show("Có lỗi xảy ra khi lưu sản phẩm!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtProductId.Text))
            {
                MessageBox.Show("Vui lòng nhập mã sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtProductId.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtProductName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtProductName.Focus();
                return false;
            }

            if (nudPrice.Value <= 0)
            {
                MessageBox.Show("Vui lòng nhập giá hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nudPrice.Focus();
                return false;
            }

            if (cmbCategory.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn danh mục!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCategory.Focus();
                return false;
            }

            return true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa sản phẩm này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    string productId = dgvProducts.SelectedRows[0].Cells["MASP"].Value.ToString();
                    string query = "DELETE FROM SANPHAM WHERE MASP = @productId";
                    
                    int result = DataProvider.ExecuteNonQuery(query, new object[] { productId });
                    if (result > 0)
                    {
                        MessageBox.Show("Xóa sản phẩm thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadProducts();
                        ClearInputs();
                    }
                    else
                    {
                        MessageBox.Show("Có lỗi xảy ra khi xóa sản phẩm!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message + "\nCó thể sản phẩm đang được sử dụng trong đơn hàng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
