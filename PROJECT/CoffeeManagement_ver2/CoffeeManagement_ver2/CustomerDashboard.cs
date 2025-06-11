using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoffeeManagement_ver2
{
    public partial class CustomerDashboard : Form
    {
        public CustomerDashboard()
        {
            InitializeComponent();           
        }
        private async Task HienThiBanTheoKhuVuc(string khuVuc)
        {
            FirebaseHelper firebaseService = new FirebaseHelper();
            var danhSachBan = await firebaseService.LayTatCaBanAsync();

            flowLayoutPanel1.Controls.Clear(); // Xóa bàn cũ

            foreach (var item in danhSachBan)
            {
                var tenBan = item.Value.TenBan;
                var khuVucBan = item.Value.KhuVuc;

                if (khuVuc == "Tất cả" || khuVucBan == khuVuc)
                {
                    Button btn = new Button();
                    btn.Text = tenBan;
                    btn.Width = 100;
                    btn.Height = 60;
                    btn.Margin = new Padding(10);
                    btn.BackColor = Color.LightGreen;

                    btn.Click += (s, e) =>
                    {
                        MessageBox.Show($"Bạn đã chọn {tenBan}");
                        // Lưu biến chọn bàn ở đây nếu cần
                    };

                    flowLayoutPanel1.Controls.Add(btn);
                }
            }
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
                LoginForm login = new LoginForm();
                login.Show();
                this.Close();
            }
        }

        private void guna2ShadowPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnBan1_Click(object sender, EventArgs e)
        {

        }
    }
}
