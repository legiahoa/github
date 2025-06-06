namespace CoffeeManagement.Models
{
    public class Category
    {
        public string MaDM { get; set; }
        public string TenDM { get; set; }
        public string MoTa { get; set; }

        public Category() { }

        public Category(string maDM, string tenDM, string moTa = "")
        {
            MaDM = maDM;
            TenDM = tenDM;
            MoTa = moTa;
        }
    }
}
