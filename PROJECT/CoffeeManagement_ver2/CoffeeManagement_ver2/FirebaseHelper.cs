﻿using System;
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

    //Quản lý bàn

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

    //Quản lý thực đơn

        public async Task<List<MonAnModel>> LayTatCaMonAsync()
        {
            var monList = await firebase
                .Child("MonAn")
                .OnceAsync<MonAnModel>();

            // Sắp xếp theo danh mục (LoaiMon), sau đó theo tên món
            return monList
                .Select(m => m.Object)
                .Where(m => m != null)
                .OrderBy(m => m.LoaiMon)
                .ThenBy(m => m.TenMon)
                .ToList();
        }

        // Thêm món ăn mới vào thực đơn
        public async Task ThemMonAnAsync(MonAnModel monAn)
        {
            // Lấy tất cả các key hiện tại
            var allMon = await firebase
                .Child("MonAn")
                .OnceAsync<MonAnModel>();

            // Tìm số lớn nhất trong các key dạng "Mon<number>"
            int maxNum = 0;
            foreach (var item in allMon)
            {
                if (item.Key.StartsWith("Mon"))
                {
                    var numStr = item.Key.Substring(3);
                    if (int.TryParse(numStr, out int num))
                    {
                        if (num > maxNum) maxNum = num;
                    }
                }
            }
            string newKey = $"Mon{maxNum + 1}";
            await firebase
                .Child("MonAn")
                .Child(newKey)
                .PutAsync(monAn);
        }

        // Cập nhật thông tin món ăn dựa trên tên món (hoặc key)
        public async Task<bool> CapNhatMonAnAsync(string tenMon, MonAnModel monAnMoi)
        {
            // Tìm món ăn theo tên
            var allMon = await firebase
                .Child("MonAn")
                .OnceAsync<MonAnModel>();

            var target = allMon.FirstOrDefault(m => m.Object.TenMon == tenMon);
            if (target == null)
                return false;

            await firebase
                .Child("MonAn")
                .Child(target.Key)
                .PutAsync(monAnMoi);
            return true;
        }

        // Xoá món ăn dựa trên tên món (hoặc key)
        public async Task<bool> XoaMonAnAsync(string tenMon)
        {
            var allMon = await firebase
                .Child("MonAn")
                .OnceAsync<MonAnModel>();

            var target = allMon.FirstOrDefault(m => m.Object.TenMon == tenMon);
            if (target == null)
                return false;

            await firebase
                .Child("MonAn")
                .Child(target.Key)
                .DeleteAsync();
            return true;
        }

        // Tìm kiếm món ăn theo tên (có thể trả về nhiều kết quả gần đúng)
        public async Task<List<MonAnModel>> TimKiemMonAnTheoTenAsync(string tuKhoa)
        {
            var allMon = await firebase
                .Child("MonAn")
                .OnceAsync<MonAnModel>();

            return allMon
                .Where(m => m.Object.TenMon != null && m.Object.TenMon.ToLower().Contains(tuKhoa.ToLower()))
                .Select(m => m.Object)
                .ToList();
        }

    //Quản lý đơn hàng

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

        // ===== METHODS CHO BÁO CÁO DOANH THU =====
        
        // Lấy báo cáo doanh thu theo ngày
        public async Task<BaoCaoDoanhThuModel> LayBaoCaoDoanhThuTheoNgay(DateTime ngay)
        {
            var donList = await LayTatCaDonHangAsync();
            
            // Debug: In ra tất cả đơn hàng
            System.Diagnostics.Debug.WriteLine($"=== DEBUG: Tổng số đơn hàng: {donList.Count} ===");
            foreach (var don in donList)
            {
                System.Diagnostics.Debug.WriteLine($"Đơn {don.MaDon}: Thời gian = {don.ThoiGian}, Trạng thái = {don.TrangThai}");
            }
            
            var donHangNgay = donList.Where(d => 
            {
                // Sử dụng TryParseExact để parse chính xác định dạng "dd/MM/yyyy HH:mm"
                bool parseSuccess = DateTime.TryParseExact(d.ThoiGian, "dd/MM/yyyy HH:mm", 
                    System.Globalization.CultureInfo.InvariantCulture, 
                    System.Globalization.DateTimeStyles.None, 
                    out DateTime thoiGian);
                
                bool sameDayCheck = parseSuccess && thoiGian.Date == ngay.Date;
                // Thay vì chỉ lọc "Hoàn thành", chúng ta lấy tất cả trạng thái trừ "Hủy"
                bool validStatus = !string.IsNullOrEmpty(d.TrangThai) && d.TrangThai != "Hủy";
                
                System.Diagnostics.Debug.WriteLine($"Đơn {d.MaDon}: Parse = {parseSuccess}, SameDay = {sameDayCheck}, ValidStatus = {validStatus}");
                
                return sameDayCheck && validStatus;
            }).ToList();

            System.Diagnostics.Debug.WriteLine($"=== Số đơn hàng phù hợp: {donHangNgay.Count} ===");
            
            return TinhToanBaoCao(donHangNgay, ngay);
        }

        // Lấy báo cáo doanh thu theo tháng
        public async Task<BaoCaoDoanhThuModel> LayBaoCaoDoanhThuTheoThang(int thang, int nam)
        {
            var donList = await LayTatCaDonHangAsync();
            var donHangThang = donList.Where(d => 
            {
                // Sử dụng TryParseExact để parse chính xác định dạng "dd/MM/yyyy HH:mm"
                bool parseSuccess = DateTime.TryParseExact(d.ThoiGian, "dd/MM/yyyy HH:mm", 
                    System.Globalization.CultureInfo.InvariantCulture, 
                    System.Globalization.DateTimeStyles.None, 
                    out DateTime thoiGian);
                
                bool matchMonth = parseSuccess && thoiGian.Month == thang && thoiGian.Year == nam;
                bool validStatus = !string.IsNullOrEmpty(d.TrangThai) && d.TrangThai != "Hủy";
                
                return matchMonth && validStatus;
            }).ToList();

            var ngayDauTien = new DateTime(nam, thang, 1);
            var baoCao = TinhToanBaoCao(donHangThang, ngayDauTien);
            baoCao.ThangBaoCao = thang;
            baoCao.NamBaoCao = nam;
            return baoCao;
        }

        // Lấy báo cáo doanh thu theo năm
        public async Task<BaoCaoDoanhThuModel> LayBaoCaoDoanhThuTheoNam(int nam)
        {
            var donList = await LayTatCaDonHangAsync();
            var donHangNam = donList.Where(d => 
            {
                // Sử dụng TryParseExact để parse chính xác định dạng "dd/MM/yyyy HH:mm"
                bool parseSuccess = DateTime.TryParseExact(d.ThoiGian, "dd/MM/yyyy HH:mm", 
                    System.Globalization.CultureInfo.InvariantCulture, 
                    System.Globalization.DateTimeStyles.None, 
                    out DateTime thoiGian);
                
                bool matchYear = parseSuccess && thoiGian.Year == nam;
                bool validStatus = !string.IsNullOrEmpty(d.TrangThai) && d.TrangThai != "Hủy";
                
                return matchYear && validStatus;
            }).ToList();

            var ngayDauTien = new DateTime(nam, 1, 1);
            var baoCao = TinhToanBaoCao(donHangNam, ngayDauTien);
            baoCao.NamBaoCao = nam;
            return baoCao;
        }

        // Tính toán báo cáo từ danh sách đơn hàng
        private BaoCaoDoanhThuModel TinhToanBaoCao(List<DonHangModel> danhSachDonHang, DateTime ngayBaoCao)
        {
            var tongDoanhThu = danhSachDonHang.Sum(d => d.TongTien);
            var soDonHang = danhSachDonHang.Count;
            var doanhThuTrungBinh = soDonHang > 0 ? tongDoanhThu / soDonHang : 0;

            // Tính toán top món ăn
            var tatCaMonAn = new Dictionary<string, TopMonAnModel>();
            foreach (var donHang in danhSachDonHang)
            {
                if (donHang.DanhSachMon != null && donHang.DanhSachMon.Count > 0)
                {
                    foreach (var mon in donHang.DanhSachMon)
                    {
                        if (!string.IsNullOrEmpty(mon.TenMon))
                        {
                            if (tatCaMonAn.ContainsKey(mon.TenMon))
                            {
                                tatCaMonAn[mon.TenMon].SoLuongBan += mon.SoLuong;
                                tatCaMonAn[mon.TenMon].DoanhThu += mon.ThanhTien;
                            }
                            else
                            {
                                tatCaMonAn[mon.TenMon] = new TopMonAnModel
                                {
                                    TenMon = mon.TenMon,
                                    SoLuongBan = mon.SoLuong,
                                    DoanhThu = mon.ThanhTien
                                };
                            }
                        }
                    }
                }
            }

            // Tính phần trăm và sắp xếp
            var topMonAn = tatCaMonAn.Values.OrderByDescending(m => m.DoanhThu).ToList();
            foreach (var mon in topMonAn)
            {
                mon.PhanTramDoanhThu = tongDoanhThu > 0 ? (double)mon.DoanhThu / tongDoanhThu * 100 : 0;
            }

            // Debug logging
            System.Diagnostics.Debug.WriteLine($"=== TÍNH TOÁN BÁO CÁO ===");
            System.Diagnostics.Debug.WriteLine($"Tổng doanh thu: {tongDoanhThu:N0} VNĐ");
            System.Diagnostics.Debug.WriteLine($"Số đơn hàng: {soDonHang}");
            System.Diagnostics.Debug.WriteLine($"Số loại món: {topMonAn.Count}");

            return new BaoCaoDoanhThuModel
            {
                NgayBaoCao = ngayBaoCao,
                TongDoanhThu = tongDoanhThu,
                SoDonHang = soDonHang,
                DoanhThuTrungBinh = doanhThuTrungBinh,
                TopMonAn = topMonAn ?? new List<TopMonAnModel>(),
                ChiTietDonHang = danhSachDonHang // Thêm chi tiết đơn hàng vào báo cáo
            };
        }
    }
}
