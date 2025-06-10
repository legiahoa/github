using Quanlyquancf.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quanlyquancf
{
    public partial class Quanlyban : Form
    {
        private DataProvider provider;

        public Quanlyban()
        {
            InitializeComponent();
            provider = new DataProvider();
            LoadTable();
            SetupDataGridView();
        }

        private void Quanlyban_Load(object sender, EventArgs e)
        {
            LoadTable();
        }

        private void SetupDataGridView()
        {
            // Thiết lập các cột cho DataGridView
            dgvBan.AutoGenerateColumns = false;
            dgvBan.Columns.Clear();

            dgvBan.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MABAN",
                HeaderText = "Mã Bàn",
                DataPropertyName = "MABAN",
                Width = 100
            });

            dgvBan.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TENBAN",
                HeaderText = "Tên Bàn",
                DataPropertyName = "TENBAN",
                Width = 150
            });

            dgvBan.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MAKHUVUC",
                HeaderText = "Mã Khu Vực",
                DataPropertyName = "MAKHUVUC",
                Width = 120
            });

            dgvBan.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TRANGTHAI",
                HeaderText = "Trạng Thái",
                DataPropertyName = "TRANGTHAI",
                Width = 150
            });
        }

        private void LoadTable()
        {
            try
            {
                string query = "SELECT MABAN, TENBAN, MAKHUVUC, TRANGTHAI FROM BAN";
                dgvBan.DataSource = provider.ExcuteQuery(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearInputs()
        {
            txtMaBan.Clear();
            txtTenBan.Clear();
            txtMaKhuVuc.Clear();
            cboTrangThai.SelectedIndex = 0;
        }

        private void btnThemBan_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMaBan.Text) || 
                    string.IsNullOrWhiteSpace(txtTenBan.Text) || 
                    string.IsNullOrWhiteSpace(txtMaKhuVuc.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string query = "INSERT INTO BAN (MABAN, TENBAN, MAKHUVUC, TRANGTHAI) VALUES (@maban, @tenban, @makhuvuc, @trangthai)";
                SqlParameter[] parameters = {
                    new SqlParameter("@maban", txtMaBan.Text.Trim()),
                    new SqlParameter("@tenban", txtTenBan.Text.Trim()),
                    new SqlParameter("@makhuvuc", txtMaKhuVuc.Text.Trim()),
                    new SqlParameter("@trangthai", cboTrangThai.SelectedItem.ToString())
                };

                int result = provider.ExcuteNonQuery(query, parameters);
                if (result > 0)
                {
                    MessageBox.Show("Thêm bàn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadTable();
                    ClearInputs();
                }
                else
                {
                    MessageBox.Show("Thêm bàn thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm bàn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSuaBan_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvBan.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn bàn cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtMaBan.Text) || 
                    string.IsNullOrWhiteSpace(txtTenBan.Text) || 
                    string.IsNullOrWhiteSpace(txtMaKhuVuc.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string originalMaBan = dgvBan.SelectedRows[0].Cells["MABAN"].Value.ToString();
                string query = "UPDATE BAN SET TENBAN = @tenban, MAKHUVUC = @makhuvuc, TRANGTHAI = @trangthai WHERE MABAN = @maban";
                SqlParameter[] parameters = {
                    new SqlParameter("@tenban", txtTenBan.Text.Trim()),
                    new SqlParameter("@makhuvuc", txtMaKhuVuc.Text.Trim()),
                    new SqlParameter("@trangthai", cboTrangThai.SelectedItem.ToString()),
                    new SqlParameter("@maban", originalMaBan)
                };

                int result = provider.ExcuteNonQuery(query, parameters);
                if (result > 0)
                {
                    MessageBox.Show("Cập nhật bàn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadTable();
                    ClearInputs();
                }
                else
                {
                    MessageBox.Show("Cập nhật bàn thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật bàn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoaBan_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvBan.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn bàn cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa bàn này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    string maBan = dgvBan.SelectedRows[0].Cells["MABAN"].Value.ToString();
                    string query = "DELETE FROM BAN WHERE MABAN = @maban";
                    SqlParameter[] parameters = {
                        new SqlParameter("@maban", maBan)
                    };

                    int deleteResult = provider.ExcuteNonQuery(query, parameters);
                    if (deleteResult > 0)
                    {
                        MessageBox.Show("Xóa bàn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadTable();
                        ClearInputs();
                    }
                    else
                    {
                        MessageBox.Show("Xóa bàn thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa bàn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string searchText = txtSoBan.Text.Trim();
                if (string.IsNullOrEmpty(searchText))
                {
                    LoadTable();
                    return;
                }

                string query = "SELECT MABAN, TENBAN, MAKHUVUC, TRANGTHAI FROM BAN WHERE MABAN LIKE @search OR TENBAN LIKE @search OR MAKHUVUC LIKE @search";
                SqlParameter[] parameters = {
                    new SqlParameter("@search", "%" + searchText + "%")
                };

                dgvBan.DataSource = provider.ExcuteQuery(query, parameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvBan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dgvBan.Rows[e.RowIndex];
                    txtMaBan.Text = row.Cells["MABAN"].Value?.ToString() ?? "";
                    txtTenBan.Text = row.Cells["TENBAN"].Value?.ToString() ?? "";
                    txtMaKhuVuc.Text = row.Cells["MAKHUVUC"].Value?.ToString() ?? "";
                    
                    string trangThai = row.Cells["TRANGTHAI"].Value?.ToString() ?? "";
                    if (cboTrangThai.Items.Contains(trangThai))
                    {
                        cboTrangThai.SelectedItem = trangThai;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi chọn dòng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
