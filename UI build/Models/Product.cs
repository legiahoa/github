namespace CoffeeManagement.Models
{
    public class Product
    {
        public string MaSP { get; set; }
        public string TenSP { get; set; }
        public decimal GiaSP { get; set; }
        public string MaDM { get; set; }
        public string MoTa { get; set; }
        public string HinhAnh { get; set; }

        public Product() { }

        public Product(string maSP, string tenSP, decimal giaSP, string maDM, string moTa = "", string hinhAnh = "")
        {
            MaSP = maSP;
            TenSP = tenSP;
            GiaSP = giaSP;
            MaDM = maDM;
            MoTa = moTa;
            HinhAnh = hinhAnh;
        }
    }
}
