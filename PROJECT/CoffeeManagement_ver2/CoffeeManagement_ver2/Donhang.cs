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

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
