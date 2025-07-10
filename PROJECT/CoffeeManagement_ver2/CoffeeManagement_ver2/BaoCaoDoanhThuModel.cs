using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeManagement_ver2
{
    public class BaoCaoDoanhThuModel
    {
        public DateTime NgayBaoCao { get; set; }
        public int ThangBaoCao { get; set; }
        public int NamBaoCao { get; set; }
        public int TongDoanhThu { get; set; }
        public int SoDonHang { get; set; }
        public int DoanhThuTrungBinh { get; set; }
        public List<TopMonAnModel> TopMonAn { get; set; }
        public List<DonHangModel> ChiTietDonHang { get; set; } // Thêm danh sách chi tiết đơn hàng
    }

    public class TopMonAnModel
    {
        public string TenMon { get; set; }
        public int SoLuongBan { get; set; }
        public int DoanhThu { get; set; }
        public double PhanTramDoanhThu { get; set; }
    }
}
