namespace CoffeeManagement.Models
{
    public class Employee
    {
        public string MaNV { get; set; }
        public string TenNV { get; set; }
        public string SDTNV { get; set; }
        public string DiaChi { get; set; }
        public string ChucVu { get; set; }
        public decimal Luong { get; set; }

        public Employee() { }

        public Employee(string maNV, string tenNV, string sdtNV, string diaChi = "", string chucVu = "", decimal luong = 0)
        {
            MaNV = maNV;
            TenNV = tenNV;
            SDTNV = sdtNV;
            DiaChi = diaChi;
            ChucVu = chucVu;
            Luong = luong;
        }
    }
}
