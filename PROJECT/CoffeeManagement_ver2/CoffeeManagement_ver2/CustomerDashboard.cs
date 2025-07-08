using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoffeeManagement_ver2
{
    public partial class CustomerDashboard : Form
    {
        private List<DonHangItemModel> donHangTam = new List<DonHangItemModel>();
        private Dictionary<string, Button> danhSachButtonBan = new Dictionary<string, Button>();
        private string banDangChon = "";
        private List<MonAnModel> danhSachMonAn = new List<MonAnModel>();
        private int tongTien = 0;
        
        // Thông tin user hiện tại
        private TaiKhoanModel currentUser;
        
        public CustomerDashboard(TaiKhoanModel user = null)
        {
            InitializeComponent();
            currentUser = user;
            LoadDuLieuMonAn();
            
            // Hiển thị thông tin khách hàng đăng nhập
            if (currentUser != null)
            {
                this.Text = $"Coffee Management - Xin chào {currentUser.HoTen}";
            }
            else
            {
                this.Text = "Coffee Management - Khách vãng lai";
            }
        }
        
        // Method để logout
        private void Logout()
        {
            currentUser = null;
            this.Close();
        }

        private async Task HienThiBanTheoKhuVuc(string khuVuc)
        {
            FirebaseHelper firebaseService = new FirebaseHelper();
            var danhSachBan = await firebaseService.LayTatCaBanAsync();

            foreach (var btn in danhSachButtonBan.Values)
            {
                var tenBan = btn.Text;
                var ban = danhSachBan.Values.FirstOrDefault(b => b.TenBan == tenBan);

                if (ban != null)
                {
                    bool hienThi = (khuVuc == "Tất cả" || ban.KhuVuc == khuVuc);
                    btn.Visible = hienThi;
                }
            }
        }
        private async Task KhoiTaoTatCaButtonBan()
        {
            FirebaseHelper firebaseService = new FirebaseHelper();
            var danhSachBan = await firebaseService.LayTatCaBanAsync();
            flowLayoutPanel1.Controls.Clear();

            foreach (var ban in danhSachBan.Values)
            {
                Button btn = new Button();
                btn.Text = ban.TenBan;
                btn.Width = 100;
                btn.Height = 60;
                btn.Margin = new Padding(10);
                btn.BackColor = Color.LightGreen;

                btn.Click += (s, e) =>
                {
                    banDangChon = btn.Text;
                    lblBanDangChonDisplay.Text = $"Đang chọn: {banDangChon}";
                    CapNhatMauButtonDaChon();
                };

                danhSachButtonBan[ban.TenBan] = btn;
                flowLayoutPanel1.Controls.Add(btn);
            }
        }
        private void CapNhatMauButtonDaChon()
        {
            foreach (var btn in danhSachButtonBan.Values)
            {
                if (btn.Text == banDangChon)
                {
                    btn.BackColor = Color.Red;
                    btn.ForeColor = Color.White;
                    btn.Font = new Font(btn.Font, FontStyle.Bold);
                }
                else
                {
                    btn.BackColor = Color.LightGreen;
                    btn.ForeColor = Color.Black;
                    btn.Font = new Font(btn.Font, FontStyle.Regular);
                }
            }
        }
        private async void LoadDuLieuMonAn()
        {
            FirebaseHelper firebaseHelper = new FirebaseHelper();
            danhSachMonAn = await firebaseHelper.LayTatCaMonAsync();

            // Lấy danh sách loại món duy nhất
            var loaiMon = danhSachMonAn.Select(m => m.LoaiMon).Distinct().ToList();
            comboBox1.DataSource = loaiMon;
        }


        private async void btnTatCa_Click_1(object sender, EventArgs e)
        {
            await HienThiBanTheoKhuVuc("Tất cả");
        }

        private async void btnNgoaiTroi_Click_1(object sender, EventArgs e)
        {
            await HienThiBanTheoKhuVuc("Ngoài trời");
        }

        private async void btnPhongLanh_Click_1(object sender, EventArgs e)
        {
            await HienThiBanTheoKhuVuc("Phòng lạnh");
        }

        private async void btnRooftop_Click_1(object sender, EventArgs e)
        {
            await HienThiBanTheoKhuVuc("Rooftop");
        }
        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn thật sự muốn đăng xuất?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Hide();
                /*LoginForm login = new LoginForm();
                login.Show();*/
                this.Close();
            }
        }

        private void guna2ShadowPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnBan1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string loaiDuocChon = comboBox1.SelectedItem.ToString();
            var tenMon = danhSachMonAn
                .Where(m => m.LoaiMon == loaiDuocChon)
                .Select(m => m.TenMon)
                .ToList();

            comboBox2.DataSource = tenMon;
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(banDangChon))
            {
                MessageBox.Show("Vui lòng chọn bàn trước!");
                return;
            }

            if (comboBox2.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn món!");
                return;
            }

            string tenMonDaChon = comboBox2.SelectedItem.ToString();
            var selectedMon = danhSachMonAn.FirstOrDefault(m => m.TenMon == tenMonDaChon);

            if (selectedMon != null)
            {
                int soLuong = (int)numericUpDown1.Value;
                int donGia = selectedMon.Gia;
                int thanhTien = donGia * soLuong;

                // Hiển thị lên ListView
                ListViewItem item = new ListViewItem(selectedMon.TenMon);
                item.SubItems.Add(soLuong.ToString());
                item.SubItems.Add($"{donGia:N0}");
                item.SubItems.Add($"{thanhTien:N0}");
                listView1.Items.Add(item);

                // Cập nhật tổng tiền
                tongTien += thanhTien;
                lblTongtien.Text = $"Tổng tiền: {tongTien:N0} VNĐ";

                // Thêm vào donHangTam (dùng để gửi đi)
                donHangTam.Add(new DonHangItemModel
                {
                    TenMon = selectedMon.TenMon,
                    DonGia = donGia,
                    SoLuong = soLuong,
                    ThanhTien = thanhTien
                });
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var item = listView1.SelectedItems[0];
                int thanhTien = int.Parse(item.SubItems[3].Text.Replace(",", "").Replace(" VNĐ", ""));

                tongTien -= thanhTien;
                lblTongtien.Text = $"Tổng tiền: {tongTien:N0} VNĐ";

                listView1.Items.Remove(item);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn món cần xóa!");
            }
        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private async void CustomerDashboard_Load(object sender, EventArgs e)
        {
            listView1.View = View.Details;
            listView1.Columns.Clear();
            listView1.Columns.Add("Tên món", 150);
            listView1.Columns.Add("Số lượng", 80);
            listView1.Columns.Add("Đơn giá", 100);
            listView1.Columns.Add("Thành tiền", 120);

            await KhoiTaoTatCaButtonBan(); // tạo tất cả button 1 lần duy nhất
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (donHangTam.Count == 0)
            {
                MessageBox.Show("Vui lòng thêm món trước khi đặt!", "Thông báo");
                return;
            }

            if (string.IsNullOrEmpty(banDangChon))
            {
                MessageBox.Show("Vui lòng chọn bàn trước khi đặt!", "Thông báo");
                return;
            }

            var donHang = new DonHangModel
            {
                MaDon = Guid.NewGuid().ToString(),
                Ban = banDangChon,
                ThoiGian = DateTime.Now.ToString("dd/MM/yyyy HH:mm"),
                TongTien = tongTien,
                DanhSachMon = new List<DonHangItemModel>(donHangTam),
                TrangThai = "Chờ xử lý", // Thiết lập trạng thái ban đầu rõ ràng
                // Thêm thông tin khách hàng từ user hiện tại
                TenKhachHang = currentUser?.HoTen ?? "Khách vãng lai",
                SoDienThoai = currentUser?.SoDienThoai ?? "",
                GhiChu = "" // Có thể thêm textbox ghi chú nếu cần
            };

            try
            {
                FirebaseHelper firebase = new FirebaseHelper();
                await firebase.ThemDonHangVaoFirebase(donHang);

                MessageBox.Show($"Đặt món thành công!\nKhách hàng: {donHang.TenKhachHang}\nBàn: {donHang.Ban}\nTổng tiền: {donHang.TongTien:N0} VNĐ", "Thông báo");

                // Reset UI
                listView1.Items.Clear();
                lblTongtien.Text = "Tổng tiền: 0 VNĐ";
                tongTien = 0;
                donHangTam.Clear();
                banDangChon = "";
                lblBanDangChonDisplay.Text = "Chưa chọn bàn";
                CapNhatMauButtonDaChon();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi gửi đơn hàng: " + ex.Message);
            }
        }
    }
}
