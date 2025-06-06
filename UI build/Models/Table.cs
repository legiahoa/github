namespace CoffeeManagement.Models
{
    public class Table
    {
        public string MaBan { get; set; }
        public string TenBan { get; set; }
        public string MaKV { get; set; }
        public int SoGhe { get; set; }
        public string TrangThai { get; set; }

        public Table() { }

        public Table(string maBan, string tenBan, string maKV, int soGhe = 4, string trangThai = "Trống")
        {
            MaBan = maBan;
            TenBan = tenBan;
            MaKV = maKV;
            SoGhe = soGhe;
            TrangThai = trangThai;
        }
    }
}
