using System;
using System.Data;
using System.Windows.Forms;
using CoffeeManagement.DAO;

namespace CoffeeManagement.Forms
{
    public partial class TableManagementForm : Form
    {
        public TableManagementForm()
        {
            InitializeComponent();
        }

        private void TableManagementForm_Load(object sender, EventArgs e)
        {
            LoadTables();
            LoadAreas();
            ClearInputs();
        }

        private void LoadTables()
        {
            try
            {
                string query = @"SELECT b.MAB, b.TENB, b.SUCCHUA, b.TRANGTHAI, kv.TENKV
                                FROM BAN b
                                INNER JOIN KHUVUC kv ON b.MAKV = kv.MAKV
                                ORDER BY kv.TENKV, b.TENB";
                
                DataTable tableData = DataProvider.ExecuteQuery(query);
                dgvTables.DataSource = tableData;

                // Customize columns
                dgvTables.Columns["MAB"].HeaderText = "Mã bàn";
                dgvTables.Columns["TENB"].HeaderText = "Tên bàn";
                dgvTables.Columns["SUCCHUA"].HeaderText = "Sức chứa";
                dgvTables.Columns["TRANGTHAI"].HeaderText = "Trạng thái";
                dgvTables.Columns["TENKV"].HeaderText = "Khu vực";

                // Auto resize columns
                dgvTables.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách bàn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadAreas()
        {
            try
            {
                string query = "SELECT MAKV, TENKV FROM KHUVUC ORDER BY TENKV";
                DataTable areaData = DataProvider.ExecuteQuery(query);
                
                cmbArea.DataSource = areaData;
                cmbArea.DisplayMember = "TENKV";
                cmbArea.ValueMember = "MAKV";
                cmbArea.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách khu vực: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvTables_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvTables.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvTables.SelectedRows[0];
                txtTableId.Text = row.Cells["MAB"].Value.ToString();
                txtTableName.Text = row.Cells["TENB"].Value.ToString();
                nudCapacity.Value = Convert.ToDecimal(row.Cells["SUCCHUA"].Value);
                chkAvailable.Checked = row.Cells["TRANGTHAI"].Value.ToString() == "Trống";
                
                // Set area
                string areaName = row.Cells["TENKV"].Value.ToString();
                for (int i = 0; i < cmbArea.Items.Count; i++)
                {
                    DataRowView item = (DataRowView)cmbArea.Items[i];
                    if (item["TENKV"].ToString() == areaName)
                    {
                        cmbArea.SelectedIndex = i;
                        break;
                    }
                }

                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
            }
            else
            {
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ClearInputs();
            GenerateNewTableId();
            txtTableName.Focus();
        }

        private void GenerateNewTableId()
        {
            try
            {
                string query = "SELECT COUNT(*) FROM BAN";
                DataTable result = DataProvider.ExecuteQuery(query);
                int count = Convert.ToInt32(result.Rows[0][0]) + 1;
                txtTableId.Text = "B" + count.ToString("D3");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tạo mã bàn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearInputs()
        {
            txtTableId.Clear();
            txtTableName.Clear();
            nudCapacity.Value = 1;
            chkAvailable.Checked = true;
            cmbArea.SelectedIndex = -1;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateInputs())
            {
                try
                {
                    // Check if table exists
                    string checkQuery = "SELECT COUNT(*) FROM BAN WHERE MAB = @tableId";
                    DataTable checkResult = DataProvider.ExecuteQuery(checkQuery, new object[] { txtTableId.Text });
                    bool exists = Convert.ToInt32(checkResult.Rows[0][0]) > 0;

                    string query;
                    object[] parameters;

                    if (exists)
                    {
                        // Update
                        query = @"UPDATE BAN 
                                 SET TENB = @name, SUCCHUA = @capacity, TRANGTHAI = @status, MAKV = @areaId 
                                 WHERE MAB = @tableId";
                        parameters = new object[] 
                        { 
                            txtTableName.Text.Trim(),
                            nudCapacity.Value,
                            chkAvailable.Checked ? "Trống" : "Có khách",
                            cmbArea.SelectedValue,
                            txtTableId.Text
                        };
                    }
                    else
                    {
                        // Insert
                        query = @"INSERT INTO BAN (MAB, TENB, SUCCHUA, TRANGTHAI, MAKV) 
                                 VALUES (@tableId, @name, @capacity, @status, @areaId)";
                        parameters = new object[] 
                        { 
                            txtTableId.Text,
                            txtTableName.Text.Trim(),
                            nudCapacity.Value,
                            chkAvailable.Checked ? "Trống" : "Có khách",
                            cmbArea.SelectedValue
                        };
                    }

                    int result = DataProvider.ExecuteNonQuery(query, parameters);
                    if (result > 0)
                    {
                        MessageBox.Show(exists ? "Cập nhật bàn thành công!" : "Thêm bàn thành công!", 
                                      "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadTables();
                        ClearInputs();
                    }
                    else
                    {
                        MessageBox.Show("Có lỗi xảy ra khi lưu bàn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (string.IsNullOrEmpty(txtTableId.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập mã bàn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTableId.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtTableName.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập tên bàn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTableName.Focus();
                return false;
            }

            if (nudCapacity.Value <= 0)
            {
                MessageBox.Show("Sức chứa phải lớn hơn 0!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nudCapacity.Focus();
                return false;
            }

            if (cmbArea.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn khu vực!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbArea.Focus();
                return false;
            }

            return true;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            btnSave_Click(sender, e);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvTables.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn bàn cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string tableId = dgvTables.SelectedRows[0].Cells["MAB"].Value.ToString();
            string tableName = dgvTables.SelectedRows[0].Cells["TENB"].Value.ToString();

            DialogResult result = MessageBox.Show($"Bạn có chắc muốn xóa bàn {tableName}?", 
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    // Check if table is being used in orders
                    string checkOrderQuery = "SELECT COUNT(*) FROM DONDATHANG WHERE MAB = @tableId";
                    DataTable checkResult = DataProvider.ExecuteQuery(checkOrderQuery, new object[] { tableId });
                    int orderCount = Convert.ToInt32(checkResult.Rows[0][0]);

                    if (orderCount > 0)
                    {
                        MessageBox.Show("Không thể xóa bàn này vì đã có đơn hàng sử dụng!", 
                            "Không thể xóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string deleteQuery = "DELETE FROM BAN WHERE MAB = @tableId";
                    int deleteResult = DataProvider.ExecuteNonQuery(deleteQuery, new object[] { tableId });

                    if (deleteResult > 0)
                    {
                        MessageBox.Show("Xóa bàn thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadTables();
                        ClearInputs();
                    }
                    else
                    {
                        MessageBox.Show("Có lỗi xảy ra khi xóa bàn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtSearchTable_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSearchTable.Text.Trim()))
                {
                    LoadTables();
                    return;
                }

                string query = @"SELECT b.MAB, b.TENB, b.SUCCHUA, b.TRANGTHAI, kv.TENKV
                                FROM BAN b
                                INNER JOIN KHUVUC kv ON b.MAKV = kv.MAKV
                                WHERE b.TENB LIKE @search OR b.MAB LIKE @search
                                ORDER BY kv.TENKV, b.TENB";
                
                string searchTerm = "%" + txtSearchTable.Text.Trim() + "%";
                DataTable searchResult = DataProvider.ExecuteQuery(query, new object[] { searchTerm });
                dgvTables.DataSource = searchResult;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
