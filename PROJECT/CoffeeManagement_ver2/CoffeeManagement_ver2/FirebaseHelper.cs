using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using Firebase.Database.Streaming;

namespace CoffeeManagement_ver2
{
    public class FirebaseHelper
    {
        private static readonly FirebaseClient firebase = new FirebaseClient(
            "https://quanlycafe-4a7fa-default-rtdb.asia-southeast1.firebasedatabase.app/"
        );

        public async Task<TaiKhoanModel> DangNhapAsync(string username, string password)
        {
            var accounts = await firebase
                .Child("TaiKhoan")
                .OnceAsync<TaiKhoanModel>();

            var matchedUser = accounts.FirstOrDefault(acc =>
                acc.Object.Username == username &&
                acc.Object.Password == password);

            if (matchedUser != null)
                return matchedUser.Object; // Trả về toàn bộ thông tin user

            return null; // Sai tài khoản
        }
        // Lấy tất cả bàn, gán Id là key Firebase

        public async Task<Dictionary<string, BanModel>> LayTatCaBanAsync()
        {
            var banList = await firebase
                .Child("Ban")
                .OnceAsync<BanModel>();

            var dict = new Dictionary<string, BanModel>();
            foreach (var item in banList)
            {
                if (item.Object != null)
                {
                    item.Object.Id = item.Key; // Gán key Firebase vào thuộc tính Id
                    dict[item.Key] = item.Object;
                }
            }
            return dict;
        }

        // Thêm bàn mới
        public async Task ThemBanAsync(BanModel ban)
        {
            // Sinh mã theo quy tắc: Ban + số thứ tự lấy từ tên bàn
            string maBan = TaoMaBanTuTen(ban.TenBan);
            await firebase
                .Child("Ban")
                .Child(maBan)
                .PutAsync(new BanModel { TenBan = ban.TenBan, KhuVuc = ban.KhuVuc });
        }

        // Hàm sinh mã bàn từ tên bàn
        private string TaoMaBanTuTen(string tenBan)
        {
            // Tìm số trong tên bàn
            var so = new string(tenBan.Where(char.IsDigit).ToArray());
            if (string.IsNullOrEmpty(so)) so = "00";
            if (so.Length == 1) so = "0" + so;
            return $"Ban{so}";
        }

        // Sửa thông tin bàn (theo Id)
        public async Task<bool> CapNhatBanAsync(BanModel ban)
        {
            // Sử dụng lại hàm sinh mã từ tên bàn
            string maBan = TaoMaBanTuTen(ban.TenBan);
            try
            {
                await firebase
                    .Child("Ban")
                    .Child(maBan)
                    .PutAsync(new BanModel { TenBan = ban.TenBan, KhuVuc = ban.KhuVuc });
                return true;
            }
            catch { return false; }
        }

        // Xoá bàn theo Id
        public async Task<bool> XoaBanAsync(string tenBan)
        {
            string maBan = TaoMaBanTuTen(tenBan);
            try
            {
                await firebase
                    .Child("Ban")
                    .Child(maBan)
                    .DeleteAsync();
                return true;
            }
            catch { return false; }
        }
        public async Task<List<MonAnModel>> LayTatCaMonAsync()
        {
            var monList = await firebase
                .Child("MonAn")
                .OnceAsync<MonAnModel>();

            return monList.Select(m => m.Object).ToList();
        }
        public async Task ThemDonHangVaoFirebase(DonHangModel donHang)
        {
            await firebase
                .Child("DonHang")
                .PostAsync(donHang);
        }
        public IObservable<FirebaseEvent<DonHangModel>> LangNgheDonHangMoi()
        {
            return firebase
                .Child("DonHang")
                .AsObservable<DonHangModel>();
        }
        public async Task CapNhatTrangThaiDonHang(string maDon, string trangThaiMoi)
        {
            try
            {
                // Debug: In ra mã đơn để kiểm tra
                Console.WriteLine($"Đang tìm đơn hàng với MaDon: {maDon}");

                // Tìm đơn hàng theo MaDon trong các record
                var allRecords = await firebase
                    .Child("DonHang")
                    .OnceAsync<DonHangModel>();

                var targetRecord = allRecords.FirstOrDefault(r => r.Object.MaDon == maDon);

                if (targetRecord == null)
                {
                    throw new Exception($"Không tìm thấy đơn hàng với mã: {maDon}");
                }

                string firebaseKey = targetRecord.Key;
                Console.WriteLine($"Tìm thấy đơn hàng với Firebase key: {firebaseKey}");

                // Cập nhật trạng thái sử dụng Firebase key
                await firebase
                    .Child("DonHang")
                    .Child(firebaseKey)
                    .Child("TrangThai")
                    .PutAsync($"\"{trangThaiMoi}\"");

                Console.WriteLine($"Đã cập nhật thành công trạng thái: {trangThaiMoi}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi cập nhật trạng thái đơn hàng: {ex.Message}");
            }
        }

        // Method để kiểm tra đơn hàng có tồn tại không
        public async Task<bool> KiemTraDonHangTonTai(string maDon)
        {
            try
            {
                // Tìm đơn hàng theo MaDon trong các record
                var allRecords = await firebase
                    .Child("DonHang")
                    .OnceAsync<DonHangModel>();

                var targetRecord = allRecords.FirstOrDefault(r => r.Object?.MaDon == maDon);

                return targetRecord != null;
            }
            catch
            {
                return false;
            }
        }

        // Method để lấy tất cả keys đơn hàng (debug)
        public async Task<List<string>> LayTatCaMaDonHang()
        {
            try
            {
                var donList = await firebase
                    .Child("DonHang")
                    .OnceAsync<DonHangModel>();

                Console.WriteLine("=== DEBUG: Tất cả đơn hàng trong Firebase ===");
                foreach (var item in donList)
                {
                    Console.WriteLine($"Firebase Key: {item.Key} -> MaDon: {item.Object?.MaDon}");
                }
                Console.WriteLine("==============================================");

                return donList.Select(d => d.Object?.MaDon ?? d.Key).ToList();
            }
            catch
            {
                return new List<string>();
            }
        }

        public async Task<List<DonHangModel>> LayTatCaDonHangAsync()
        {
            var donList = await firebase
                .Child("DonHang")
                .OnceAsync<DonHangModel>();

            // Đảm bảo mỗi đơn hàng có MaDon đúng (nếu null thì dùng Firebase key)
            var result = new List<DonHangModel>();
            foreach (var item in donList)
            {
                var donHang = item.Object;
                if (donHang != null)
                {
                    // Nếu MaDon bị null hoặc rỗng, dùng Firebase key
                    if (string.IsNullOrEmpty(donHang.MaDon))
                    {
                        donHang.MaDon = item.Key;
                    }
                    result.Add(donHang);
                }
            }

            return result;
        }

        // Method để xóa đơn hàng theo mã đơn
        public async Task<bool> XoaDonHang(string maDon)
        {
            try
            {
                // Tìm đơn hàng theo MaDon trong các record
                var allRecords = await firebase
                    .Child("DonHang")
                    .OnceAsync<DonHangModel>();

                var targetRecord = allRecords.FirstOrDefault(r => r.Object?.MaDon == maDon);

                if (targetRecord == null)
                {
                    return false; // Không tìm thấy đơn hàng
                }

                string firebaseKey = targetRecord.Key;

                // Xóa đơn hàng sử dụng Firebase key
                await firebase
                    .Child("DonHang")
                    .Child(firebaseKey)
                    .DeleteAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        // Method để xóa tất cả đơn hàng
        public async Task<bool> XoaTatCaDonHang()
        {
            try
            {
                await firebase
                    .Child("DonHang")
                    .DeleteAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
