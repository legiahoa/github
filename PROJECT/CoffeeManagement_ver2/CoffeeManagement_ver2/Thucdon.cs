using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoffeeManagement_ver2
{
    public partial class Thucdon : Form
    {
        private FirebaseHelper firebaseHelper = new FirebaseHelper();
        private List<MonAnModel> danhSachMon = new List<MonAnModel>();

        public Thucdon()
        {
            InitializeComponent();
            Load += Thucdon_Load;
            btnThemMon.Click += btnThemMon_Click;
            btnSuaMon.Click += btnSuaMon_Click;
            btnSearch.Click += btnSearch_Click;
            dgvBan.CellClick += dgvBan_CellClick;
        }

        private async void Thucdon_Load(object sender, EventArgs e)
        {
            await HienThiDanhSachMon();
            // Gán danh mục mẫu nếu cần
            if (cboDanhmuc.Items.Count == 0)
            {
                cboDanhmuc.Items.AddRange(new string[] { "Đồ uống", "Đồ ăn", "Khác" });
            }
        }

        private async Task HienThiDanhSachMon(List<MonAnModel> list = null)
        {
            danhSachMon = list ?? await firebaseHelper.LayTatCaMonAsync();
            // Đã được sắp xếp theo danh mục ở FirebaseHelper
            dgvBan.DataSource = danhSachMon.Select(m => new { m.TenMon, DanhMuc = m.LoaiMon, Gia = m.Gia }).ToList();
        }

        private async void btnThemMon_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenMon.Text) || string.IsNullOrWhiteSpace(txtGia.Text) || cboDanhmuc.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin món ăn.");
                return;
            }
            if (!int.TryParse(txtGia.Text, out int gia))
            {
                MessageBox.Show("Giá phải là số.");
                return;
            }
            var mon = new MonAnModel
            {
                TenMon = txtTenMon.Text.Trim(),
                LoaiMon = cboDanhmuc.SelectedItem.ToString(),
                Gia = gia
            };
            await firebaseHelper.ThemMonAnAsync(mon);
            await HienThiDanhSachMon();
            MessageBox.Show("Đã thêm món ăn thành công.");
        }

        private async void btnSuaMon_Click(object sender, EventArgs e)
        {
            if (dgvBan.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn món cần sửa.");
                return;
            }
            string tenMonCu = dgvBan.CurrentRow.Cells["TenMon"].Value.ToString();
            if (!int.TryParse(txtGia.Text, out int gia))
            {
                MessageBox.Show("Giá phải là số.");
                return;
            }
            var monMoi = new MonAnModel
            {
                TenMon = txtTenMon.Text.Trim(),
                LoaiMon = cboDanhmuc.SelectedItem?.ToString() ?? "",
                Gia = gia
            };
            bool kq = await firebaseHelper.CapNhatMonAnAsync(tenMonCu, monMoi);
            if (kq)
            {
                await HienThiDanhSachMon();
                MessageBox.Show("Đã cập nhật món ăn thành công.");
            }
            else
            {
                MessageBox.Show("Không tìm thấy món ăn để cập nhật.");
            }
        }

        private async void btnXoaMon_Click(object sender, EventArgs e)
        {
            if (dgvBan.CurrentRow != null)
            {
                string tenMon = dgvBan.CurrentRow.Cells["TenMon"].Value.ToString();
                var confirm = MessageBox.Show($"Bạn có chắc muốn xoá món '{tenMon}'?", "Xác nhận", MessageBoxButtons.YesNo);
                if (confirm == DialogResult.Yes)
                {
                    bool kq = await firebaseHelper.XoaMonAnAsync(tenMon);
                    if (kq)
                    {
                        await HienThiDanhSachMon();
                        MessageBox.Show("Đã xoá món ăn thành công.");
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy món ăn để xoá.");
                    }
                }
            }
            txtTenMon.Clear();
            txtGia.Clear();
            cboDanhmuc.SelectedIndex = -1;
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            string tuKhoa = txtSoBan.Text.Trim();
            if (string.IsNullOrEmpty(tuKhoa))
            {
                await HienThiDanhSachMon();
                return;
            }
            var ketQua = await firebaseHelper.TimKiemMonAnTheoTenAsync(tuKhoa);
            await HienThiDanhSachMon(ketQua);
        }

        private void dgvBan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvBan.Rows.Count)
            {
                var row = dgvBan.Rows[e.RowIndex];
                txtTenMon.Text = row.Cells["TenMon"].Value?.ToString();
                cboDanhmuc.SelectedItem = row.Cells["DanhMuc"].Value?.ToString();
                txtGia.Text = row.Cells["Gia"].Value?.ToString();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
