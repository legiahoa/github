using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeManagement_ver2
{
    public class FirebaseHelper
    {
        private static readonly FirebaseClient firebase = new FirebaseClient(
            "https://quanlycafe-4a7fa-default-rtdb.asia-southeast1.firebasedatabase.app/"
        );

        public async Task<string> DangNhapAsync(string username, string password)
        {
            var accounts = await firebase
                .Child("TaiKhoan")
                .OnceAsync<dynamic>();

            var matchedUser = accounts.FirstOrDefault(acc =>
                acc.Object.Username == username &&
                acc.Object.Password == password);

            if (matchedUser != null)
                return matchedUser.Object.Role; // Trả về "NhanVien" hoặc "KhachHang"

            return null; // Sai tài khoản
        }
        public async Task<Dictionary<string, BanModel>> LayTatCaBanAsync()
        {
            var banList = await firebase
            .Child("Ban")
            .OnceAsync<BanModel>();

            foreach (var item in banList)
            {
                string tenBan = item.Object.TenBan;
                string khuVuc = item.Object.KhuVuc;

                Console.WriteLine($"Tên bàn: {tenBan} - Khu vực: {khuVuc}");
            }


            return banList.ToDictionary(b => b.Key, b => b.Object);
        }
        public async Task<List<MonAnModel>> LayTatCaMonAsync()
        {
            var monList = await firebase
                .Child("MonAn")
                .OnceAsync<MonAnModel>();

            return monList.Select(m => m.Object).ToList();
        }
    }
}
