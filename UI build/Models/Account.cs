namespace CoffeeManagement.Models
{
    public class Account
    {
        public string MaTK { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public string VaiTro { get; set; }

        public Account() { }

        public Account(string maTK, string tenDangNhap, string matKhau, string vaiTro)
        {
            MaTK = maTK;
            TenDangNhap = tenDangNhap;
            MatKhau = matKhau;
            VaiTro = vaiTro;
        }
    }
}
