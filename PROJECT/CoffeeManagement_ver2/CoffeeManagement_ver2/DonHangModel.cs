using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeManagement_ver2
{
    public class DonHangModel
    {
        public string MaDon { get; set; }
        public string Ban { get; set; }
        public string TrangThai { get; set; } = "Chờ xử lý";
        public string ThoiGian { get; set; }
        public int TongTien { get; set; }
        public List<DonHangItemModel> DanhSachMon { get; set; }
    }
}
