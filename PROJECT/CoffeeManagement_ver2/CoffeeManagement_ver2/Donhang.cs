using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoffeeManagement_ver2
{
    public partial class Donhang : Form
    {
        private TcpListener listener;
        private DateTime thoiGianMoForm;

        private List<DonHangModel> danhSachTatCaDonHang = new List<DonHangModel>();
        private FirebaseHelper firebaseHelper = new FirebaseHelper();

        public Donhang()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private HashSet<string> maDonDaXuLy = new HashSet<string>();
        private void Donhang_Load(object sender, EventArgs e)
        {
            thoiGianMoForm = DateTime.Now;

            FirebaseHelper firebaseHelper = new FirebaseHelper();

            KhoiTaoListViewDonHang();
            TaiTatCaDonHangTuFirebase();

            firebaseHelper.LangNgheDonHangMoi()
                .Subscribe(don =>
                {
                    var donHang = don.Object;
                    if (donHang == null || string.IsNullOrEmpty(donHang.MaDon))
                        return;

                    if (maDonDaXuLy.Contains(donHang.MaDon))
                        return;

                    if (donHang.TrangThai != "Chờ xử lý")
                        return;

                    // Kiểm tra thời gian đơn hàng
                    if (!DateTime.TryParseExact(donHang.ThoiGian, "dd/MM/yyyy HH:mm", null, System.Globalization.DateTimeStyles.None, out DateTime thoiGianDonHang))
                        return;

                    if (thoiGianDonHang < thoiGianMoForm)
                        return; // bỏ qua đơn hàng cũ được tạo trước khi form mở

                    maDonDaXuLy.Add(donHang.MaDon);

                    if (this.IsHandleCreated)
                    {
                        this.Invoke(new Action(() =>
                        {
                            MessageBox.Show($"Đơn hàng mới từ {donHang.Ban} - {donHang.TongTien:N0} VNĐ");
                            // TODO: thêm vào danh sách, bảng...
                        }));
                    }

                    _ = firebaseHelper.CapNhatTrangThaiDonHang(donHang.MaDon, "Đã nhận");
                });
        }
        private void KhoiTaoListViewDonHang()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();

            dataGridView1.Columns.Add("MaDon", "Mã đơn");
            dataGridView1.Columns.Add("Ban", "Bàn");
            dataGridView1.Columns.Add("TrangThai", "Trạng thái");
            dataGridView1.Columns.Add("TongTien", "Tổng tiền");
            dataGridView1.Columns.Add("ThoiGian", "Thời gian");
        }
        private async void TaiTatCaDonHangTuFirebase()
        {
            danhSachTatCaDonHang = await firebaseHelper.LayTatCaDonHangAsync(); // bạn cần có hàm này trong FirebaseHelper

            dataGridView1.Rows.Clear();
            foreach (var don in danhSachTatCaDonHang)
            {
                dataGridView1.Rows.Add(don.MaDon, don.Ban, don.TrangThai, $"{don.TongTien:N0}", don.ThoiGian);
            }
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = textBoxSearch.Text.ToLower();
            var ketQua = danhSachTatCaDonHang
                .Where(d => d.MaDon.ToLower().Contains(keyword) ||
                            d.Ban.ToLower().Contains(keyword) ||
                            d.TrangThai.ToLower().Contains(keyword) ||
                            d.ThoiGian.ToLower().Contains(keyword))
                .ToList();

            dataGridView1.Rows.Clear();
            foreach (var don in ketQua)
            {
                dataGridView1.Rows.Add(don.MaDon, don.Ban, don.TrangThai, $"{don.TongTien:N0}", don.ThoiGian);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count)
            {
                string maDon = dataGridView1.Rows[e.RowIndex].Cells["MaDon"].Value.ToString();
                var donHang = danhSachTatCaDonHang.FirstOrDefault(d => d.MaDon == maDon);

                if (donHang != null)
                    HienThiChiTietDonHang(donHang);
            }
        }
        private void HienThiChiTietDonHang(DonHangModel don)
        {
            panelChiTiet.Controls.Clear();

            Label lblMaDon = new Label() { Text = $"Mã đơn: {don.MaDon}", AutoSize = true };
            Label lblBan = new Label() { Text = $"Bàn: {don.Ban}", AutoSize = true };
            Label lblTrangThai = new Label() { Text = $"Trạng thái: {don.TrangThai}", AutoSize = true };
            Label lblThoiGian = new Label() { Text = $"Thời gian: {don.ThoiGian}", AutoSize = true };
            Label lblTongTien = new Label() { Text = $"Tổng tiền: {don.TongTien:N0} VNĐ", AutoSize = true };

            panelChiTiet.Controls.Add(lblMaDon);
            panelChiTiet.Controls.Add(lblBan);
            panelChiTiet.Controls.Add(lblTrangThai);
            panelChiTiet.Controls.Add(lblThoiGian);
            panelChiTiet.Controls.Add(lblTongTien);

            int y = 100;
            foreach (var item in don.DanhSachMon)
            {
                Label lblMon = new Label()
                {
                    Text = $"{item.TenMon} - SL: {item.SoLuong} - Đơn giá: {item.DonGia:N0} - Thành tiền: {item.ThanhTien:N0}",
                    Location = new Point(10, y),
                    AutoSize = true
                };
                panelChiTiet.Controls.Add(lblMon);
                y += 25;
            }
        }
    }
}
