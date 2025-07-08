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
    public partial class Ban : Form
    {
        private FirebaseHelper firebaseHelper = new FirebaseHelper();
        private Dictionary<string, BanModel> danhSachBan = new Dictionary<string, BanModel>();
        private List<KeyValuePair<string, BanModel>> danhSachBanHienThi = new List<KeyValuePair<string, BanModel>>();
        private string selectedBanId = null;

        public Ban()
        {
            InitializeComponent();
            Load += Ban_Load;
            dgvBan.SelectionChanged += dgvBan_SelectionChanged;
            btnThemBan.Click += btnThemBan_Click;
            btnSuaBan.Click += btnSuaBan_Click;
        }

        private async void Ban_Load(object sender, EventArgs e)
        {
            await LoadBanToGrid();
        }

        private async Task LoadBanToGrid(string search = "")
        {
            danhSachBan = await firebaseHelper.LayTatCaBanAsync();
            if (string.IsNullOrWhiteSpace(search))
            {
                danhSachBanHienThi = danhSachBan.ToList();
            }
            else
            {
                danhSachBanHienThi = danhSachBan.Where(kv => kv.Value.TenBan.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            }
            dgvBan.DataSource = danhSachBanHienThi.Select(kv => new { kv.Value.TenBan, kv.Value.KhuVuc }).ToList();
            dgvBan.ClearSelection();
            selectedBanId = null;
            txtTenBan.Clear();
            txtMaKhuVuc.Clear();
        }

        private void dgvBan_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvBan.SelectedRows.Count > 0)
            {
                var row = dgvBan.SelectedRows[0];
                var tenBan = row.Cells["TenBan"].Value?.ToString();
                var khuVuc = row.Cells["KhuVuc"].Value?.ToString();
                txtTenBan.Text = tenBan;
                txtMaKhuVuc.Text = khuVuc;
                selectedBanId = tenBan; // Lưu lại tên bàn để sửa/xoá
            }
        }

        private async void btnThemBan_Click(object sender, EventArgs e)
        {
            var tenBan = txtTenBan.Text.Trim();
            var khuVuc = txtMaKhuVuc.Text.Trim();
            if (string.IsNullOrEmpty(tenBan) || string.IsNullOrEmpty(khuVuc))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                return;
            }
            await firebaseHelper.ThemBanAsync(new BanModel { TenBan = tenBan, KhuVuc = khuVuc });
            await LoadBanToGrid();
        }

        private async void btnSuaBan_Click(object sender, EventArgs e)
        {
            var tenBan = txtTenBan.Text.Trim();
            var khuVuc = txtMaKhuVuc.Text.Trim();
            if (string.IsNullOrEmpty(tenBan) || string.IsNullOrEmpty(khuVuc))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                return;
            }
            if (string.IsNullOrEmpty(selectedBanId))
            {
                MessageBox.Show("Vui lòng chọn bàn để sửa.");
                return;
            }
            var ban = new BanModel { TenBan = selectedBanId, KhuVuc = khuVuc };
            var result = await firebaseHelper.CapNhatBanAsync(ban);
            if (!result)
                MessageBox.Show("Cập nhật thất bại!");
            await LoadBanToGrid();
        }

        private async void btnXoaBan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedBanId))
            {
                MessageBox.Show("Vui lòng chọn bàn để xoá.");
                return;
            }
            var confirm = MessageBox.Show("Bạn có chắc muốn xoá bàn này?", "Xác nhận", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.Yes)
            {
                var result = await firebaseHelper.XoaBanAsync(selectedBanId);
                if (!result)
                    MessageBox.Show("Xoá thất bại!");
                await LoadBanToGrid();
            }
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            await LoadBanToGrid(txtSoBan.Text.Trim());
        }
    }
}
