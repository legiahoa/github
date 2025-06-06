namespace CoffeeManagement.Models
{
    public class Customer
    {
        public string MaKH { get; set; }
        public string TenKH { get; set; }
        public string SDTKH { get; set; }
        public string DiaChi { get; set; }
        public string Email { get; set; }

        public Customer() { }

        public Customer(string maKH, string tenKH, string sdtKH, string diaChi = "", string email = "")
        {
            MaKH = maKH;
            TenKH = tenKH;
            SDTKH = sdtKH;
            DiaChi = diaChi;
            Email = email;
        }
    }
}
