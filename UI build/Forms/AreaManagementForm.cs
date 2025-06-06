using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CoffeeManagement.DAO;

namespace CoffeeManagement.Forms
{
    public partial class AreaManagementForm : Form
    {
        private bool isEdit = false;
        private string currentAreaId = "";

        public AreaManagementForm()
        {
            InitializeComponent();
        }

        private void AreaManagementForm_Load(object sender, EventArgs e)
        {
            LoadAreas();
            ClearInputs();
        }

        private void LoadAreas()
        {
            try
            {
                string query = @"SELECT kv.MAKV, kv.TENKV, kv.MOTA, kv.TRANGTHAI,
                               COUNT(b.MAB) as SoLuongBan
                               FROM KHUVUC kv
                               LEFT JOIN BAN b ON kv.MAKV = b.MAKV
                               GROUP BY kv.MAKV, kv.TENKV, kv.MOTA, kv.TRANGTHAI
                               ORDER BY kv.MAKV";

                DataTable areas = DataProvider.ExecuteQuery(query);
                dgvAreas.DataSource = areas;

                // Configure DataGridView
                dgvAreas.Columns["MAKV"].HeaderText = "Mã khu vực";
                dgvAreas.Columns["TENKV"].HeaderText = "Tên khu vực";
                dgvAreas.Columns["MOTA"].HeaderText = "Mô tả";
                dgvAreas.Columns["TRANGTHAI"].HeaderText = "Trạng thái";
                dgvAreas.Columns["SoLuongBan"].HeaderText = "Số lượng bàn";

                dgvAreas.Columns["MAKV"].Width = 120;
                dgvAreas.Columns["TENKV"].Width = 150;
                dgvAreas.Columns["MOTA"].Width = 200;
                dgvAreas.Columns["TRANGTHAI"].Width = 100;
                dgvAreas.Columns["SoLuongBan"].Width = 120;

                lblTotalAreas.Text = $"Tổng số khu vực: {areas.Rows.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách khu vực: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearInputs()
        {
            txtAreaId.Clear();
            txtAreaName.Clear();
            txtDescription.Clear();
            cboStatus.SelectedIndex = 0;
            isEdit = false;
            currentAreaId = "";
            
            btnSave.Text = "Thêm";
            btnCancel.Text = "Làm mới";
        }

        private string GenerateAreaId()
        {
            try
            {
                string query = "SELECT TOP 1 MAKV FROM KHUVUC ORDER BY MAKV DESC";
                DataTable result = DataProvider.ExecuteQuery(query);

                if (result.Rows.Count > 0)
                {
                    string lastId = result.Rows[0]["MAKV"].ToString();
                    int number = int.Parse(lastId.Substring(2)) + 1;
                    return "KV" + number.ToString("D3");
                }
                else
                {
                    return "KV001";
                }
            }
            catch
            {
                return "KV001";
            }
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtAreaName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên khu vực!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAreaName.Focus();
                return false;
            }

            if (txtAreaName.Text.Length > 100)
            {
                MessageBox.Show("Tên khu vực không được vượt quá 100 ký tự!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAreaName.Focus();
                return false;
            }

            if (txtDescription.Text.Length > 500)
            {
                MessageBox.Show("Mô tả không được vượt quá 500 ký tự!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDescription.Focus();
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
                    // Update area
                    string updateQuery = @"UPDATE KHUVUC SET 
                                         TENKV = @areaName, 
                                         MOTA = @description, 
                                         TRANGTHAI = @status 
                                         WHERE MAKV = @areaId";

                    int result = DataProvider.ExecuteNonQuery(updateQuery, new object[] {
                        txtAreaName.Text.Trim(),
                        txtDescription.Text.Trim(),
                        cboStatus.Text,
                        currentAreaId
                    });

                    if (result > 0)
                    {
                        MessageBox.Show("Cập nhật khu vực thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadAreas();
                        ClearInputs();
                    }
                }
                else
                {
                    // Check if area name already exists
                    string checkQuery = "SELECT COUNT(*) FROM KHUVUC WHERE TENKV = @areaName";
                    DataTable checkResult = DataProvider.ExecuteQuery(checkQuery, new object[] { txtAreaName.Text.Trim() });

                    if (Convert.ToInt32(checkResult.Rows[0][0]) > 0)
                    {
                        MessageBox.Show("Tên khu vực đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtAreaName.Focus();
                        return;
                    }

                    // Add new area
                    string areaId = GenerateAreaId();
                    string insertQuery = @"INSERT INTO KHUVUC (MAKV, TENKV, MOTA, TRANGTHAI) 
                                         VALUES (@areaId, @areaName, @description, @status)";

                    int result = DataProvider.ExecuteNonQuery(insertQuery, new object[] {
                        areaId,
                        txtAreaName.Text.Trim(),
                        txtDescription.Text.Trim(),
                        cboStatus.Text
                    });

                    if (result > 0)
                    {
                        MessageBox.Show("Thêm khu vực thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadAreas();
                        ClearInputs();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu khu vực: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvAreas.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn khu vực cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow selectedRow = dgvAreas.SelectedRows[0];
            currentAreaId = selectedRow.Cells["MAKV"].Value.ToString();
            
            txtAreaId.Text = currentAreaId;
            txtAreaName.Text = selectedRow.Cells["TENKV"].Value.ToString();
            txtDescription.Text = selectedRow.Cells["MOTA"].Value?.ToString() ?? "";
            cboStatus.Text = selectedRow.Cells["TRANGTHAI"].Value.ToString();

            isEdit = true;
            btnSave.Text = "Cập nhật";
            btnCancel.Text = "Hủy";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvAreas.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn khu vực cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string areaId = dgvAreas.SelectedRows[0].Cells["MAKV"].Value.ToString();
            string areaName = dgvAreas.SelectedRows[0].Cells["TENKV"].Value.ToString();

            if (MessageBox.Show($"Bạn có chắc chắn muốn xóa khu vực '{areaName}'?", "Xác nhận", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    // Check if area has tables
                    string checkQuery = "SELECT COUNT(*) FROM BAN WHERE MAKV = @areaId";
                    DataTable checkResult = DataProvider.ExecuteQuery(checkQuery, new object[] { areaId });

                    if (Convert.ToInt32(checkResult.Rows[0][0]) > 0)
                    {
                        MessageBox.Show("Không thể xóa khu vực này vì còn có bàn trong khu vực!", 
                            "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string deleteQuery = "DELETE FROM KHUVUC WHERE MAKV = @areaId";
                    int result = DataProvider.ExecuteNonQuery(deleteQuery, new object[] { areaId });

                    if (result > 0)
                    {
                        MessageBox.Show("Xóa khu vực thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadAreas();
                        ClearInputs();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xóa khu vực: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                LoadAreas();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string searchText = txtSearch.Text.Trim();
                string query;

                if (string.IsNullOrWhiteSpace(searchText))
                {
                    LoadAreas();
                    return;
                }

                query = @"SELECT kv.MAKV, kv.TENKV, kv.MOTA, kv.TRANGTHAI,
                         COUNT(b.MAB) as SoLuongBan
                         FROM KHUVUC kv
                         LEFT JOIN BAN b ON kv.MAKV = b.MAKV
                         WHERE kv.MAKV LIKE @search OR kv.TENKV LIKE @search OR kv.MOTA LIKE @search
                         GROUP BY kv.MAKV, kv.TENKV, kv.MOTA, kv.TRANGTHAI
                         ORDER BY kv.MAKV";

                DataTable result = DataProvider.ExecuteQuery(query, new object[] { $"%{searchText}%" });
                dgvAreas.DataSource = result;

                lblTotalAreas.Text = $"Tìm thấy: {result.Rows.Count} khu vực";
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

        private void dgvAreas_SelectionChanged(object sender, EventArgs e)
        {
            btnEdit.Enabled = dgvAreas.SelectedRows.Count > 0;
            btnDelete.Enabled = dgvAreas.SelectedRows.Count > 0;
        }
    }
}
