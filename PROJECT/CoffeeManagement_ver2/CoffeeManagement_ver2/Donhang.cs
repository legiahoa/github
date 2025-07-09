using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoffeeManagement_ver2
{
    public partial class Donhang : Form
    {
        private DateTime thoiGianMoForm;

        private List<DonHangModel> danhSachTatCaDonHang = new List<DonHangModel>();
        private FirebaseHelper firebaseHelper = new FirebaseHelper();
        private System.Windows.Forms.Timer searchTimer;
        private DonHangModel donHangDangChon = null;

        public Donhang()
        {
            InitializeComponent();
            
            // Khởi tạo timer cho search debounce
            searchTimer = new System.Windows.Forms.Timer();
            searchTimer.Interval = 300; // 300ms delay
            searchTimer.Tick += SearchTimer_Tick;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            // Kích hoạt tìm kiếm bằng cách gọi sự kiện TextChanged
            textBoxSearch_TextChanged(textBoxSearch, EventArgs.Empty);
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            AdminDashboard adminDashboard = new AdminDashboard();   
            adminDashboard.Show();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            // Cleanup resources
            searchTimer?.Stop();
            searchTimer?.Dispose();
            
            base.OnFormClosed(e);
        }

        private HashSet<string> maDonDaXuLy = new HashSet<string>();
        private void Donhang_Load(object sender, EventArgs e)
        {
            thoiGianMoForm = DateTime.Now;

            FirebaseHelper firebaseHelper = new FirebaseHelper();

            KhoiTaoListViewDonHang();
            _ = TaiTatCaDonHangTuFirebase(); // Fire and forget for async call

            firebaseHelper.LangNgheDonHangMoi()
                .Subscribe(don =>
                {
                    try
                    {
                        var donHang = don.Object;
                        if (donHang == null || string.IsNullOrEmpty(donHang.MaDon))
                            return;

                        if (maDonDaXuLy.Contains(donHang.MaDon))
                            return;

                        if (donHang.TrangThai != "Chờ xử lý")
                            return;

                        // Kiểm tra thời gian đơn hàng
                        if (!DateTime.TryParseExact(donHang.ThoiGian, "dd/MM/yyyy HH:mm", null, System.Globalization.DateTimeStyles.None, out DateTime thoiGianDonHang))
                            return;

                        if (thoiGianDonHang < thoiGianMoForm)
                            return; // bỏ qua đơn hàng cũ được tạo trước khi form mở

                        maDonDaXuLy.Add(donHang.MaDon);

                        if (this.IsHandleCreated)
                        {
                            this.Invoke(new Action(() =>
                            {
                                string tenKhach = donHang.TenKhachHang ?? "Khách vãng lai";
                                MessageBox.Show($"Đơn hàng mới từ {tenKhach} (Bàn {donHang.Ban})\nTổng tiền: {donHang.TongTien:N0} VNĐ", 
                                               "Đơn hàng mới", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                
                                // Thêm đơn hàng mới vào danh sách
                                danhSachTatCaDonHang.Add(donHang);
                                
                                // Cập nhật DataGridView nếu không có filter
                                if (string.IsNullOrEmpty(textBoxSearch.Text.Trim()))
                                {
                                    dataGridView1.Rows.Add(donHang.MaDon, donHang.Ban, 
                                                          donHang.TenKhachHang ?? "N/A", 
                                                          donHang.SoDienThoai ?? "N/A", 
                                                          donHang.TrangThai, 
                                                          $"{donHang.TongTien:N0}", 
                                                          donHang.ThoiGian);
                                }
                                else
                                {
                                    // Nếu có filter, thực hiện search lại
                                    PerformSearch();
                                }
                            }));
                        }

                        // Cập nhật trạng thái tự động
                        _ = Task.Run(async () =>
                        {
                            try
                            {
                                await firebaseHelper.CapNhatTrangThaiDonHang(donHang.MaDon, "Đã nhận");
                                
                                // Cập nhật trạng thái trong danh sách local và UI
                                if (this.IsHandleCreated)
                                {
                                    this.Invoke(new Action(() =>
                                    {
                                        // Cập nhật trong danh sách local
                                        donHang.TrangThai = "Đã nhận";
                                        
                                        // Cập nhật DataGridView nếu đơn hàng đang hiển thị
                                        foreach (DataGridViewRow row in dataGridView1.Rows)
                                        {
                                            if (row.Cells["MaDon"].Value?.ToString() == donHang.MaDon)
                                            {
                                                row.Cells["TrangThai"].Value = "Đã nhận";
                                                break;
                                            }
                                        }
                                        
                                        // Nếu đơn hàng này đang được xem chi tiết, cập nhật luôn
                                        if (donHangDangChon?.MaDon == donHang.MaDon)
                                        {
                                            HienThiChiTietDonHang(donHang);
                                        }
                                    }));
                                }
                            }
                            catch (Exception ex)
                            {
                                // Log error but don't show to user
                                Console.WriteLine($"Lỗi cập nhật trạng thái đơn hàng {donHang.MaDon}: {ex.Message}");
                            }
                        });
                    }
                    catch (Exception ex)
                    {
                        // Handle error in real-time processing
                        if (this.IsHandleCreated)
                        {
                            this.Invoke(new Action(() =>
                            {
                                MessageBox.Show($"Lỗi xử lý đơn hàng real-time: {ex.Message}", "Lỗi", 
                                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }));
                        }
                    }
                });
        }
        private void KhoiTaoListViewDonHang()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();

            dataGridView1.Columns.Add("MaDon", "Mã đơn");
            dataGridView1.Columns.Add("Ban", "Bàn");
            dataGridView1.Columns.Add("TenKhachHang", "Khách hàng");
            dataGridView1.Columns.Add("SoDienThoai", "SĐT");
            dataGridView1.Columns.Add("TrangThai", "Trạng thái");
            dataGridView1.Columns.Add("TongTien", "Tổng tiền");
            dataGridView1.Columns.Add("ThoiGian", "Thời gian");
            
            // Thiết lập độ rộng cột
            dataGridView1.Columns["MaDon"].Width = 120;
            dataGridView1.Columns["Ban"].Width = 80;
            dataGridView1.Columns["TenKhachHang"].Width = 150;
            dataGridView1.Columns["SoDienThoai"].Width = 120;
            dataGridView1.Columns["TrangThai"].Width = 120;
            dataGridView1.Columns["TongTien"].Width = 100;
            dataGridView1.Columns["ThoiGian"].Width = 150;
        }
        private async Task TaiTatCaDonHangTuFirebase()
        {
            try
            {
                danhSachTatCaDonHang = await firebaseHelper.LayTatCaDonHangAsync();

                dataGridView1.Rows.Clear();
                foreach (var don in danhSachTatCaDonHang)
                {
                    dataGridView1.Rows.Add(don.MaDon, don.Ban, 
                                          don.TenKhachHang ?? "N/A", 
                                          don.SoDienThoai ?? "N/A", 
                                          don.TrangThai, 
                                          $"{don.TongTien:N0}", 
                                          don.ThoiGian);
                }
                
                // Cập nhật tiêu đề với số lượng đơn hàng
                CapNhatTieuDe();
                
                // Nếu có đơn hàng đang được chọn, cập nhật lại thông tin chi tiết
                if (donHangDangChon != null)
                {
                    var donHangCapNhat = danhSachTatCaDonHang.FirstOrDefault(d => d.MaDon == donHangDangChon.MaDon);
                    if (donHangCapNhat != null)
                    {
                        donHangDangChon = donHangCapNhat;
                        HienThiChiTietDonHang(donHangDangChon);
                    }
                }
                
                // Kiểm tra và đồng bộ trạng thái UI
                KiemTraVaDongBoTrangThai();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu đơn hàng: {ex.Message}", "Lỗi", 
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            // Stop previous timer và start lại để debounce
            searchTimer.Stop();
            searchTimer.Start();
        }

        private void SearchTimer_Tick(object sender, EventArgs e)
        {
            searchTimer.Stop();
            PerformSearch();
        }

        private void PerformSearch()
        {
            string keyword = textBoxSearch.Text.ToLower().Trim();
            
            List<DonHangModel> ketQua;
            
            if (string.IsNullOrEmpty(keyword))
            {
                // Nếu không có từ khóa, hiển thị tất cả
                ketQua = danhSachTatCaDonHang;
            }
            else
            {
                // Tìm kiếm theo từ khóa (bao gồm thông tin khách hàng)
                ketQua = danhSachTatCaDonHang
                    .Where(d => d.MaDon.ToLower().Contains(keyword) ||
                                d.Ban.ToLower().Contains(keyword) ||
                                d.TrangThai.ToLower().Contains(keyword) ||
                                d.ThoiGian.ToLower().Contains(keyword) ||
                                (d.TenKhachHang != null && d.TenKhachHang.ToLower().Contains(keyword)) ||
                                (d.SoDienThoai != null && d.SoDienThoai.Contains(keyword)) ||
                                (d.GhiChu != null && d.GhiChu.ToLower().Contains(keyword)))
                    .ToList();
            }

            // Cập nhật DataGridView
            dataGridView1.Rows.Clear();
            foreach (var don in ketQua)
            {
                dataGridView1.Rows.Add(don.MaDon, don.Ban, 
                                      don.TenKhachHang ?? "N/A", 
                                      don.SoDienThoai ?? "N/A", 
                                      don.TrangThai, 
                                      $"{don.TongTien:N0}", 
                                      don.ThoiGian);
            }
            
            // Nếu đơn hàng đang được chọn vẫn trong kết quả tìm kiếm, giữ nguyên selection
            if (donHangDangChon != null && ketQua.Any(d => d.MaDon == donHangDangChon.MaDon))
            {
                // Tìm và chọn lại row tương ứng
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells["MaDon"].Value?.ToString() == donHangDangChon.MaDon)
                    {
                        row.Selected = true;
                        break;
                    }
                }
                
                // Cập nhật lại thông tin chi tiết với dữ liệu mới nhất
                var donHangCapNhat = ketQua.FirstOrDefault(d => d.MaDon == donHangDangChon.MaDon);
                if (donHangCapNhat != null)
                {
                    donHangDangChon = donHangCapNhat;
                    HienThiChiTietDonHang(donHangDangChon);
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count)
            {
                string maDon = dataGridView1.Rows[e.RowIndex].Cells["MaDon"].Value.ToString();
                var donHang = danhSachTatCaDonHang.FirstOrDefault(d => d.MaDon == maDon);

                // Debug thông tin
                Console.WriteLine($"User chọn đơn hàng với mã: {maDon}");
                Console.WriteLine($"Tìm thấy đơn hàng: {donHang != null}");

                if (donHang != null)
                {
                    donHangDangChon = donHang; // Lưu đơn hàng đang chọn
                    Console.WriteLine($"Đã chọn đơn hàng: {donHang.MaDon} - {donHang.Ban} - {donHang.TrangThai}");
                    HienThiChiTietDonHang(donHang);
                    
                    // Cập nhật trạng thái UI
                    KiemTraVaDongBoTrangThai();
                }
            }
        }
        private void HienThiChiTietDonHang(DonHangModel don)
        {
            listViewChiTiet.Items.Clear();
            
            // Cấu hình ListView
            listViewChiTiet.View = View.Details;
            listViewChiTiet.FullRowSelect = true;
            listViewChiTiet.GridLines = true;
            listViewChiTiet.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            
            // Thiết lập columns nếu chưa có
            if (listViewChiTiet.Columns.Count == 0)
            {
                listViewChiTiet.Columns.Add("Thông tin", 150);
                listViewChiTiet.Columns.Add("Giá trị", 250);
            }

            // Thêm thông tin đơn hàng với style khác nhau
            var itemMaDon = new ListViewItem(new[] { "Mã đơn", don.MaDon });
            itemMaDon.Font = new Font("Arial", 9, FontStyle.Bold);
            listViewChiTiet.Items.Add(itemMaDon);
            
            listViewChiTiet.Items.Add(new ListViewItem(new[] { "Bàn", don.Ban }));
            
            // Thông tin khách hàng
            var itemKhachHang = new ListViewItem(new[] { "Khách hàng", don.TenKhachHang ?? "Khách vãng lai" });
            itemKhachHang.Font = new Font("Arial", 9, FontStyle.Bold);
            itemKhachHang.ForeColor = Color.Blue;
            listViewChiTiet.Items.Add(itemKhachHang);
            
            if (!string.IsNullOrEmpty(don.SoDienThoai))
            {
                listViewChiTiet.Items.Add(new ListViewItem(new[] { "Số điện thoại", don.SoDienThoai }));
            }
            
            var itemTrangThai = new ListViewItem(new[] { "Trạng thái", don.TrangThai });
            // Màu theo trạng thái
            switch (don.TrangThai)
            {
                case "Chờ xử lý":
                    itemTrangThai.BackColor = Color.LightBlue;
                    break;
                case "Đã nhận":
                    itemTrangThai.BackColor = Color.LightYellow;
                    break;
                case "Đã hoàn thành":
                    itemTrangThai.BackColor = Color.LightGreen;
                    break;
                case "Đã hủy":
                    itemTrangThai.BackColor = Color.LightCoral;
                    break;
            }
            listViewChiTiet.Items.Add(itemTrangThai);
            
            listViewChiTiet.Items.Add(new ListViewItem(new[] { "Thời gian", don.ThoiGian }));
            
            var itemTongTien = new ListViewItem(new[] { "Tổng tiền", $"{don.TongTien:N0} VNĐ" });
            itemTongTien.Font = new Font("Arial", 9, FontStyle.Bold);
            itemTongTien.ForeColor = Color.Red;
            listViewChiTiet.Items.Add(itemTongTien);
            
            // Ghi chú nếu có
            if (!string.IsNullOrEmpty(don.GhiChu))
            {
                var itemGhiChu = new ListViewItem(new[] { "Ghi chú", don.GhiChu });
                itemGhiChu.ForeColor = Color.DarkGreen;
                itemGhiChu.Font = new Font("Arial", 8, FontStyle.Italic);
                listViewChiTiet.Items.Add(itemGhiChu);
            }
            
            // Thêm separator
            var separator = new ListViewItem(new[] { "", "" });
            separator.BackColor = Color.LightGray;
            listViewChiTiet.Items.Add(separator);
            
            var headerMon = new ListViewItem(new[] { "── CHI TIẾT MÓN ĂN ──", "" });
            headerMon.Font = new Font("Arial", 9, FontStyle.Bold);
            headerMon.BackColor = Color.LightGray;
            listViewChiTiet.Items.Add(headerMon);

            // Hiển thị từng món
            if (don.DanhSachMon != null && don.DanhSachMon.Count > 0)
            {
                foreach (var item in don.DanhSachMon)
                {
                    string monInfo = $"• {item.TenMon} (SL: {item.SoLuong})";
                    string giaInfo = $"{item.DonGia:N0} VNĐ × {item.SoLuong} = {item.ThanhTien:N0} VNĐ";
                    
                    var monItem = new ListViewItem(new[] { monInfo, giaInfo });
                    monItem.Font = new Font("Arial", 8);
                    listViewChiTiet.Items.Add(monItem);
                }
            }
            else
            {
                var emptyItem = new ListViewItem(new[] { "Không có món nào", "" });
                emptyItem.ForeColor = Color.Gray;
                emptyItem.Font = new Font("Arial", 8, FontStyle.Italic);
                listViewChiTiet.Items.Add(emptyItem);
            }
            
            // Auto resize columns để fit nội dung
            listViewChiTiet.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            
            // Hiển thị ghi chú trong textBoxGhiChu
            textBoxGhiChu.Text = don.GhiChu ?? "";
        }

        // Event handler cho button Xác nhận đơn hàng
        private async void btnXacNhan_Click(object sender, EventArgs e)
        {
            if (donHangDangChon == null)
            {
                MessageBox.Show("Vui lòng chọn đơn hàng cần xác nhận!", "Thông báo", 
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (donHangDangChon.TrangThai == "Đã hoàn thành")
            {
                MessageBox.Show("Đơn hàng này đã được hoàn thành!", "Thông báo", 
                               MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult result = MessageBox.Show($"Bạn có chắc muốn xác nhận hoàn thành đơn hàng {donHangDangChon.Ban}?", 
                                                 "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (result == DialogResult.Yes)
            {
                try
                {
                    // Hiển thị loading
                    this.Cursor = Cursors.WaitCursor;
                    
                    // Kiểm tra đơn hàng có tồn tại không
                    bool tonTai = await firebaseHelper.KiemTraDonHangTonTai(donHangDangChon.MaDon);
                    if (!tonTai)
                    {
                        MessageBox.Show($"Không tìm thấy đơn hàng {donHangDangChon.MaDon} trên Firebase!\nVui lòng làm mới danh sách.", 
                                       "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    
                    // Cập nhật trạng thái lên Firebase
                    await firebaseHelper.CapNhatTrangThaiDonHang(donHangDangChon.MaDon, "Đã hoàn thành");
                    
                    // Cập nhật trong danh sách local
                    donHangDangChon.TrangThai = "Đã hoàn thành";
                    
                    // Refresh DataGridView
                    PerformSearch();
                    
                    // Refresh chi tiết
                    HienThiChiTietDonHang(donHangDangChon);
                    
                    MessageBox.Show("Đã xác nhận hoàn thành đơn hàng!", "Thành công", 
                                   MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xác nhận đơn hàng:\n{ex.Message}", "Lỗi", 
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
        }

        // Event handler cho button Hủy đơn hàng  
        private async void btnHuyDon_Click(object sender, EventArgs e)
        {
            if (donHangDangChon == null)
            {
                MessageBox.Show("Vui lòng chọn đơn hàng cần hủy!", "Thông báo", 
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (donHangDangChon.TrangThai == "Đã hủy")
            {
                MessageBox.Show("Đơn hàng này đã được hủy!", "Thông báo", 
                               MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (donHangDangChon.TrangThai == "Đã hoàn thành")
            {
                MessageBox.Show("Không thể hủy đơn hàng đã hoàn thành!", "Thông báo", 
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show($"Bạn có chắc muốn hủy đơn hàng {donHangDangChon.Ban}?\nHành động này không thể hoàn tác!", 
                                                 "Xác nhận hủy", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            
            if (result == DialogResult.Yes)
            {
                try
                {
                    // Hiển thị loading
                    this.Cursor = Cursors.WaitCursor;
                    
                    // Kiểm tra đơn hàng có tồn tại không
                    bool tonTai = await firebaseHelper.KiemTraDonHangTonTai(donHangDangChon.MaDon);
                    if (!tonTai)
                    {
                        MessageBox.Show($"Không tìm thấy đơn hàng {donHangDangChon.MaDon} trên Firebase!\nVui lòng làm mới danh sách.", 
                                       "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    
                    // Cập nhật trạng thái lên Firebase
                    await firebaseHelper.CapNhatTrangThaiDonHang(donHangDangChon.MaDon, "Đã hủy");
                    
                    // Cập nhật trong danh sách local
                    donHangDangChon.TrangThai = "Đã hủy";
                    
                    // Refresh DataGridView
                    PerformSearch();
                    
                    // Refresh chi tiết
                    HienThiChiTietDonHang(donHangDangChon);
                    
                    MessageBox.Show("Đã hủy đơn hàng!", "Thành công", 
                                   MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi hủy đơn hàng:\n{ex.Message}", "Lỗi", 
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
        }

        // Event handler cho button In hóa đơn
        private void btnInHoaDon_Click(object sender, EventArgs e)
        {
            if (donHangDangChon == null)
            {
                MessageBox.Show("Vui lòng chọn đơn hàng cần in!", "Thông báo", 
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Tạo nội dung hóa đơn
                string hoaDon = TaoNoiDungHoaDon(donHangDangChon);
                
                // Hiển thị preview hóa đơn
                Form previewForm = new Form();
                previewForm.Text = "Preview Hóa Đơn";
                previewForm.Size = new Size(500, 600);
                previewForm.StartPosition = FormStartPosition.CenterParent;
                
                TextBox txtHoaDon = new TextBox();
                txtHoaDon.Multiline = true;
                txtHoaDon.ScrollBars = ScrollBars.Vertical;
                txtHoaDon.ReadOnly = true;
                txtHoaDon.Font = new Font("Courier New", 10);
                txtHoaDon.Text = hoaDon;
                txtHoaDon.Dock = DockStyle.Fill;
                
                Button btnPrint = new Button();
                btnPrint.Text = "In Hóa Đơn";
                btnPrint.Size = new Size(100, 30);
                btnPrint.Dock = DockStyle.Bottom;
                btnPrint.Click += (s, ev) => {
                    // Thực hiện in (có thể mở Notepad hoặc sử dụng PrintDocument)
                    System.IO.File.WriteAllText($"HoaDon_{donHangDangChon.MaDon}.txt", hoaDon);
                    System.Diagnostics.Process.Start("notepad.exe", $"HoaDon_{donHangDangChon.MaDon}.txt");
                    previewForm.Close();
                };
                
                previewForm.Controls.Add(txtHoaDon);
                previewForm.Controls.Add(btnPrint);
                previewForm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tạo hóa đơn: {ex.Message}", "Lỗi", 
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Hàm tạo nội dung hóa đơn
        private string TaoNoiDungHoaDon(DonHangModel donHang)
        {
            StringBuilder sb = new StringBuilder();
            
            sb.AppendLine("═══════════════════════════════════");
            sb.AppendLine("          QUÁN VINCOFFEE");
            sb.AppendLine("    Địa chỉ: 123 Đường ABC");
            sb.AppendLine("      Tel: 0999-999-999");
            sb.AppendLine("═══════════════════════════════════");
            sb.AppendLine();
            sb.AppendLine("            HÓA ĐƠN");
            sb.AppendLine();
            sb.AppendLine($"Mã đơn: {donHang.MaDon}");
            sb.AppendLine($"Bàn: {donHang.Ban}");
            sb.AppendLine($"Khách hàng: {donHang.TenKhachHang ?? "Khách vãng lai"}");
            if (!string.IsNullOrEmpty(donHang.SoDienThoai))
            {
                sb.AppendLine($"SĐT: {donHang.SoDienThoai}");
            }
            sb.AppendLine($"Thời gian: {donHang.ThoiGian}");
            sb.AppendLine($"Trạng thái: {donHang.TrangThai}");
            if (!string.IsNullOrEmpty(donHang.GhiChu))
            {
                sb.AppendLine($"Ghi chú: {donHang.GhiChu}");
            }
            sb.AppendLine();
            sb.AppendLine("───────────────────────────────────");
            sb.AppendLine("STT | Tên món           | SL | Giá");
            sb.AppendLine("───────────────────────────────────");
            
            int stt = 1;
            int tongTien = 0;
            
            if (donHang.DanhSachMon != null)
            {
                foreach (var item in donHang.DanhSachMon)
                {
                    string tenMon = item.TenMon.Length > 15 ? item.TenMon.Substring(0, 15) : item.TenMon;
                    sb.AppendLine($"{stt,2}  | {tenMon,-15} | {item.SoLuong,2} | {item.ThanhTien:N0}");
                    tongTien += item.ThanhTien;
                    stt++;
                }
            }
            
            sb.AppendLine("───────────────────────────────────");
            sb.AppendLine($"TỔNG TIỀN:               {tongTien:N0} VNĐ");
            sb.AppendLine("═══════════════════════════════════");
            sb.AppendLine();
            sb.AppendLine("       Cảm ơn quý khách!");
            sb.AppendLine("     Hẹn gặp lại lần sau!");
            sb.AppendLine();
            sb.AppendLine($"In lúc: {DateTime.Now:dd/MM/yyyy HH:mm}");
            
            return sb.ToString();
        }

        // Event handler cho button Làm mới danh sách
        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                // Hiển thị loading
                this.Cursor = Cursors.WaitCursor;
                
                // Disable button nếu tồn tại
                if (sender is Button btn)
                {
                    btn.Enabled = false;
                }
                
                // Debug: Lấy tất cả mã đơn hàng để kiểm tra
                var tatCaMaDon = await firebaseHelper.LayTatCaMaDonHang();
                Console.WriteLine($"Tất cả mã đơn hàng trong Firebase: {string.Join(", ", tatCaMaDon)}");
                
                // Tải lại dữ liệu từ Firebase
                await TaiTatCaDonHangTuFirebase();
                
                // Clear selection
                donHangDangChon = null;
                listViewChiTiet.Items.Clear();
                textBoxGhiChu.Text = "";
                
                // Reset search box nếu cần
                if (!string.IsNullOrEmpty(textBoxSearch.Text))
                {
                    PerformSearch(); // Áp dụng lại search filter
                }
                
                MessageBox.Show($"Đã làm mới danh sách đơn hàng!\nTìm thấy {danhSachTatCaDonHang.Count} đơn hàng.", "Thông báo", 
                               MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi làm mới: {ex.Message}", "Lỗi", 
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                
                // Enable lại button nếu tồn tại
                if (sender is Button btn)
                {
                    btn.Enabled = true;
                }
            }
        }

        // Event handler cho ComboBox lọc theo trạng thái
        private void comboBoxTrangThai_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender is ComboBox combo && combo.SelectedItem != null)
            {
                string trangThaiLoc = combo.SelectedItem.ToString();
                LocTheoTrangThai(trangThaiLoc);
            }
        }

        private void LocTheoTrangThai(string trangThai)
        {
            List<DonHangModel> ketQua;
            
            if (trangThai == "Tất cả")
            {
                ketQua = danhSachTatCaDonHang;
            }
            else
            {
                ketQua = danhSachTatCaDonHang
                    .Where(d => d.TrangThai == trangThai)
                    .ToList();
            }

            // Cập nhật DataGridView
            dataGridView1.Rows.Clear();
            foreach (var don in ketQua)
            {
                dataGridView1.Rows.Add(don.MaDon, don.Ban, 
                                      don.TenKhachHang ?? "N/A", 
                                      don.SoDienThoai ?? "N/A", 
                                      don.TrangThai, 
                                      $"{don.TongTien:N0}", 
                                      don.ThoiGian);
            }
        }

        // Phiên bản alternative cho Panel (nếu muốn dùng Panel thay vì ListView)
        private void HienThiChiTietDonHangTrenPanel(DonHangModel don, Panel panel)
        {
            panel.Controls.Clear();
            panel.AutoScroll = true;

            // Tạo các label với layout có kiểm soát
            Label lblMaDon = new Label() 
            { 
                Text = $"Mã đơn: {don.MaDon}", 
                AutoSize = true, 
                Location = new Point(10, 10),
                Font = new Font("Arial", 9, FontStyle.Bold),
                MaximumSize = new Size(panel.Width - 20, 0)
            };
            
            Label lblBan = new Label() 
            { 
                Text = $"Bàn: {don.Ban}", 
                AutoSize = true, 
                Location = new Point(10, 35),
                Font = new Font("Arial", 9)
            };
            
            Label lblTrangThai = new Label() 
            { 
                Text = $"Trạng thái: {don.TrangThai}", 
                AutoSize = true, 
                Location = new Point(10, 60),
                Font = new Font("Arial", 9)
            };
            
            Label lblThoiGian = new Label() 
            { 
                Text = $"Thời gian: {don.ThoiGian}", 
                AutoSize = true, 
                Location = new Point(10, 85),
                Font = new Font("Arial", 9)
            };
            
            Label lblTongTien = new Label() 
            { 
                Text = $"Tổng tiền: {don.TongTien:N0} VNĐ", 
                AutoSize = true, 
                Location = new Point(10, 110),
                Font = new Font("Arial", 9, FontStyle.Bold),
                ForeColor = Color.Red
            };

            // Thêm header cho danh sách món
            Label lblHeaderMon = new Label()
            {
                Text = "Chi tiết món ăn:",
                AutoSize = true,
                Location = new Point(10, 140),
                Font = new Font("Arial", 9, FontStyle.Bold)
            };

            panel.Controls.Add(lblMaDon);
            panel.Controls.Add(lblBan);
            panel.Controls.Add(lblTrangThai);
            panel.Controls.Add(lblThoiGian);
            panel.Controls.Add(lblTongTien);
            panel.Controls.Add(lblHeaderMon);

            // Hiển thị từng món
            int y = 165;
            if (don.DanhSachMon != null)
            {
                foreach (var item in don.DanhSachMon)
                {
                    Label lblMon = new Label()
                    {
                        Text = $"• {item.TenMon} - SL: {item.SoLuong} - Giá: {item.DonGia:N0} VNĐ - TT: {item.ThanhTien:N0} VNĐ",
                        Location = new Point(20, y),
                        AutoSize = true,
                        Font = new Font("Arial", 8),
                        MaximumSize = new Size(panel.Width - 40, 0)
                    };
                    panel.Controls.Add(lblMon);
                    y += 25;
                }
            }
        }

        // Event handler cho ListView chi tiết (optional - để copy thông tin)
        private void listViewChiTiet_DoubleClick(object sender, EventArgs e)
        {
            if (listViewChiTiet.SelectedItems.Count > 0)
            {
                var selectedItem = listViewChiTiet.SelectedItems[0];
                string textToCopy = $"{selectedItem.Text}: {selectedItem.SubItems[1].Text}";
                
                try
                {
                    Clipboard.SetText(textToCopy);
                    MessageBox.Show($"Đã copy: {textToCopy}", "Thông báo", 
                                   MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi copy: {ex.Message}", "Lỗi", 
                                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        // Event handler cho ListView khi selection thay đổi
        private void listViewChiTiet_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Có thể thêm logic xử lý khi user chọn item khác trong ListView
            // Hiện tại để trống hoặc có thể hiển thị thông tin bổ sung
        }

        private void lblChiTietTitle_Click(object sender, EventArgs e)
        {

        }

        // Event handler cho button Xóa danh sách (guna2Button1)
        private async void guna2Button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("⚠️ CẢNH BÁO NGHIÊM TRỌNG ⚠️\n\nBạn có chắc muốn xóa toàn bộ danh sách đơn hàng?\n\nHành động này sẽ:\n• Xóa TẤT CẢ đơn hàng khỏi Firebase\n• Không thể hoàn tác\n• Mất hết dữ liệu lịch sử\n\nChỉ thực hiện khi thực sự cần thiết!", 
                                                 "⚠️ XÓA TẤT CẢ DỮ LIỆU", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            
            if (result == DialogResult.Yes)
            {
                // Xác nhận lần 2 để đảm bảo
                DialogResult confirmAgain = MessageBox.Show("Bạn có THỰC SỰ chắc chắn?\nSau khi xóa sẽ KHÔNG THỂ khôi phục!", 
                                                           "Xác nhận lần cuối", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
                
                if (confirmAgain == DialogResult.Yes)
                {
                    try
                    {
                        // Hiển thị loading
                        this.Cursor = Cursors.WaitCursor;
                        guna2Button1.Enabled = false;
                        
                        // Backup count trước khi xóa
                        int soLuongTruocKhiXoa = danhSachTatCaDonHang.Count;
                        
                        // Sử dụng method XoaTatCaDonHang mới
                        bool thanhCong = await firebaseHelper.XoaTatCaDonHang();
                        
                        if (thanhCong)
                        {
                            // Clear danh sách local
                            danhSachTatCaDonHang.Clear();
                            danhSachTatCaDonHang.Clear();
                            maDonDaXuLy.Clear();
                            
                            // Clear UI
                            dataGridView1.DataSource = null;
                            dataGridView1.Rows.Clear();
                            listViewChiTiet.Items.Clear();
                            donHangDangChon = null;
                            textBoxGhiChu.Text = "";
                            
                            // Reset search và filter
                            textBoxSearch.Text = "";
                            if (comboBoxTrangThai.Items.Count > 0)
                            {
                                comboBoxTrangThai.SelectedIndex = 0;
                            }
                            
                            // Cập nhật title
                            CapNhatTieuDe();
                            
                            MessageBox.Show($"✅ Đã xóa toàn bộ danh sách!\n\nSố đơn hàng đã xóa: {soLuongTruocKhiXoa}\nDanh sách hiện tại: Trống", 
                                           "Hoàn thành", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("❌ Không thể xóa tất cả đơn hàng. Vui lòng thử lại sau!", "Lỗi", 
                                           MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"❌ Lỗi khi xóa danh sách đơn hàng:\n{ex.Message}", "Lỗi", 
                                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        this.Cursor = Cursors.Default;
                        guna2Button1.Enabled = true;
                    }
                }
            }
        }

        // Event handler cho button Xóa đơn (guna2Button2)
        private async void guna2Button2_Click(object sender, EventArgs e)
        {
            if (donHangDangChon == null)
            {
                MessageBox.Show("⚠️ Vui lòng chọn đơn hàng cần xóa!\n\nHướng dẫn: Click vào một dòng trong danh sách đơn hàng", "Chưa chọn đơn hàng", 
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Hiển thị thông tin chi tiết về đơn hàng sẽ xóa
            string thongTinDonHang = $"📋 Thông tin đơn hàng sẽ xóa:\n\n" +
                                   $"• Mã đơn: {donHangDangChon.MaDon}\n" +
                                   $"• Bàn: {donHangDangChon.Ban}\n" +
                                   $"• Khách hàng: {donHangDangChon.TenKhachHang ?? "Khách vãng lai"}\n" +
                                   $"• Tổng tiền: {donHangDangChon.TongTien:N0} VNĐ\n" +
                                   $"• Trạng thái: {donHangDangChon.TrangThai}\n" +
                                   $"• Thời gian: {donHangDangChon.ThoiGian}\n\n" +
                                   $"⚠️ Hành động này không thể hoàn tác!";

            DialogResult result = MessageBox.Show(thongTinDonHang, 
                                                 "❌ Xác nhận xóa đơn hàng", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (result == DialogResult.Yes)
            {
                try
                {
                    // Hiển thị loading
                    this.Cursor = Cursors.WaitCursor;
                    guna2Button2.Enabled = false;
                    
                    // Backup thông tin đơn hàng
                    string maDonBackup = donHangDangChon.MaDon;
                    string banBackup = donHangDangChon.Ban;
                    
                    // Kiểm tra đơn hàng có tồn tại không
                    bool tonTai = await firebaseHelper.KiemTraDonHangTonTai(donHangDangChon.MaDon);
                    if (!tonTai)
                    {
                        MessageBox.Show($"⚠️ Không tìm thấy đơn hàng {donHangDangChon.MaDon} trên Firebase!\n\nCó thể đơn hàng đã được xóa bởi người khác.\nVui lòng làm mới danh sách.", 
                                       "Đơn hàng không tồn tại", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        
                        // Làm mới danh sách
                        await TaiTatCaDonHangTuFirebase();
                        return;
                    }
                    
                    // Xóa đơn hàng từ Firebase
                    await firebaseHelper.XoaDonHang(donHangDangChon.MaDon);
                    
                    // Xóa khỏi danh sách local
                    danhSachTatCaDonHang.RemoveAll(d => d.MaDon == donHangDangChon.MaDon);
                    maDonDaXuLy.Remove(donHangDangChon.MaDon);
                    
                    // Refresh DataGridView
                    PerformSearch();
                    
                    // Clear chi tiết
                    listViewChiTiet.Items.Clear();
                    textBoxGhiChu.Text = "";
                    donHangDangChon = null;
                    
                    MessageBox.Show($"✅ Đã xóa đơn hàng thành công!\n\n📋 Thông tin đơn hàng đã xóa:\n• Mã đơn: {maDonBackup}\n• Bàn: {banBackup}", 
                                   "Xóa thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"❌ Lỗi khi xóa đơn hàng:\n\n{ex.Message}\n\nVui lòng thử lại sau hoặc kiểm tra kết nối mạng.", "Lỗi xóa đơn hàng", 
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                    guna2Button2.Enabled = true;
                }
            }
        }

        // Helper method để kiểm tra và đồng bộ trạng thái UI
        private void KiemTraVaDongBoTrangThai()
        {
            // Kiểm tra nếu không có đơn hàng nào được chọn
            if (donHangDangChon == null)
            {
                // Disable các button cần chọn đơn hàng
                if (guna2Button2 != null) guna2Button2.Enabled = false;
                return;
            }
            
            // Enable button xóa đơn nếu có đơn hàng được chọn
            if (guna2Button2 != null) guna2Button2.Enabled = true;
            
            // Cập nhật màu trạng thái trong DataGridView
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["MaDon"].Value?.ToString() == donHangDangChon.MaDon)
                {
                    string trangThai = row.Cells["TrangThai"].Value?.ToString();
                    switch (trangThai)
                    {
                        case "Chờ xử lý":
                            row.DefaultCellStyle.BackColor = Color.LightBlue;
                            break;
                        case "Đã nhận":
                            row.DefaultCellStyle.BackColor = Color.LightYellow;
                            break;
                        case "Đã hoàn thành":
                            row.DefaultCellStyle.BackColor = Color.LightGreen;
                            break;
                        case "Đã hủy":
                            row.DefaultCellStyle.BackColor = Color.LightCoral;
                            break;
                        default:
                            row.DefaultCellStyle.BackColor = Color.White;
                            break;
                    }
                    row.Selected = true;
                    break;
                }
            }
        }

        // Method cập nhật số lượng đơn hàng trên title
        private void CapNhatTieuDe()
        {
            int tongDonHang = danhSachTatCaDonHang.Count;
            int donChoXuLy = danhSachTatCaDonHang.Count(d => d.TrangThai == "Chờ xử lý");
            int donDaNhan = danhSachTatCaDonHang.Count(d => d.TrangThai == "Đã nhận");
            int donHoanThanh = danhSachTatCaDonHang.Count(d => d.TrangThai == "Đã hoàn thành");
            int donDaHuy = danhSachTatCaDonHang.Count(d => d.TrangThai == "Đã hủy");
            
            this.Text = $"Quản Lý Đơn Hàng - Tổng: {tongDonHang} | Chờ: {donChoXuLy} | Nhận: {donDaNhan} | Hoàn thành: {donHoanThanh} | Hủy: {donDaHuy}";
        }
    }
}
