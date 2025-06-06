namespace CoffeeManagement.Models
{
    public class Area
    {
        public string MaKV { get; set; }
        public string TenKV { get; set; }
        public string MoTa { get; set; }

        public Area() { }

        public Area(string maKV, string tenKV, string moTa = "")
        {
            MaKV = maKV;
            TenKV = tenKV;
            MoTa = moTa;
        }
    }
}
