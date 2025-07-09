using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net.Mail;
using System.Net;

namespace CoffeeManagement_ver2
{
    public partial class BaoCaoDoanhThu : Form
    {
        private FirebaseHelper firebaseHelper = new FirebaseHelper();
        private BaoCaoDoanhThuModel baoCaoHienTai = null;
        private string loaiBaoCao = "";

        public BaoCaoDoanhThu()
        {
            InitializeComponent();
        }

        private void BaoCaoDoanhThu_Load(object sender, EventArgs e)
        {
            // Thiết lập DataGridView
            dataGridViewTopMon.Columns.Clear();
            dataGridViewTopMon.Columns.Add("TenMon", "Tên món");
            dataGridViewTopMon.Columns.Add("SoLuongBan", "Số lượng bán");
            dataGridViewTopMon.Columns.Add("DoanhThu", "Doanh thu");
            dataGridViewTopMon.Columns.Add("PhanTramDoanhThu", "% Tổng doanh thu");

            // Thiết lập định dạng cột
            dataGridViewTopMon.Columns["DoanhThu"].DefaultCellStyle.Format = "N0";
            dataGridViewTopMon.Columns["DoanhThu"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewTopMon.Columns["PhanTramDoanhThu"].DefaultCellStyle.Format = "N1";
            dataGridViewTopMon.Columns["PhanTramDoanhThu"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            // Thiết lập giá trị mặc định
            dateTimePicker1.Value = DateTime.Now;
            numThang.Value = DateTime.Now.Month;
            numNam1.Value = DateTime.Now.Year;
            numNam2.Value = DateTime.Now.Year;
        }

        private async void btnXemBaoCaoNgay_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime ngayChon = dateTimePicker1.Value;
                baoCaoHienTai = await firebaseHelper.LayBaoCaoDoanhThuTheoNgay(ngayChon);
                loaiBaoCao = $"ngày {ngayChon:dd/MM/yyyy}";
                
                lblTitle.Text = $"Báo cáo doanh thu ngày {ngayChon:dd/MM/yyyy}";
                HienThiBaoCao();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tạo báo cáo: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnXemBaoCaoThang_Click(object sender, EventArgs e)
        {
            try
            {
                int thang = (int)numThang.Value;
                int nam = (int)numNam1.Value;
                baoCaoHienTai = await firebaseHelper.LayBaoCaoDoanhThuTheoThang(thang, nam);
                loaiBaoCao = $"tháng {thang}/{nam}";
                
                lblTitle.Text = $"Báo cáo doanh thu tháng {thang}/{nam}";
                HienThiBaoCao();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tạo báo cáo: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnXemBaoCaoNam_Click(object sender, EventArgs e)
        {
            try
            {
                int nam = (int)numNam2.Value;
                baoCaoHienTai = await firebaseHelper.LayBaoCaoDoanhThuTheoNam(nam);
                loaiBaoCao = $"năm {nam}";
                
                lblTitle.Text = $"Báo cáo doanh thu năm {nam}";
                HienThiBaoCao();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tạo báo cáo: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HienThiBaoCao()
        {
            if (baoCaoHienTai == null) return;

            // Hiển thị thông tin tổng quan
            lblTongDoanhThu.Text = $"Tổng doanh thu: {baoCaoHienTai.TongDoanhThu:N0} VNĐ";
            lblSoDonHang.Text = $"Số đơn hàng: {baoCaoHienTai.SoDonHang}";
            lblDoanhThuTrungBinh.Text = $"Doanh thu trung bình: {baoCaoHienTai.DoanhThuTrungBinh:N0} VNĐ";

            // Hiển thị top món ăn
            dataGridViewTopMon.Rows.Clear();
            
            if (baoCaoHienTai.TopMonAn.Count == 0) return;
            
            foreach (var mon in baoCaoHienTai.TopMonAn)
            {
                dataGridViewTopMon.Rows.Add(
                    mon.TenMon,
                    mon.SoLuongBan,
                    $"{mon.DoanhThu:N0}",
                    $"{mon.PhanTramDoanhThu:F1}%"
                );
            }
        }

        private void btnXuatFile_Click(object sender, EventArgs e)
        {
            if (baoCaoHienTai == null)
            {
                MessageBox.Show("Vui lòng tạo báo cáo trước khi xuất file!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "Text files (*.txt)|*.txt|CSV files (*.csv)|*.csv";
                saveDialog.FileName = $"BaoCaoDoanhThu_{loaiBaoCao.Replace("/", "-").Replace(" ", "_")}.txt";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    string noiDung = TaoNoiDungBaoCao();
                    File.WriteAllText(saveDialog.FileName, noiDung, Encoding.UTF8);
                    MessageBox.Show($"Đã xuất báo cáo thành công!\nFile: {saveDialog.FileName}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuiEmail_Click(object sender, EventArgs e)
        {
            if (baoCaoHienTai == null)
            {
                MessageBox.Show("Vui lòng tạo báo cáo trước khi gửi email!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tạo form nhập email
            EmailForm emailForm = new EmailForm();
            if (emailForm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    GuiEmailBaoCao(emailForm.EmailNguoiNhan, emailForm.EmailNguoiGui, emailForm.MatKhauEmail);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi gửi email: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void GuiEmailBaoCao(string emailNhan, string emailGui, string matKhau)
        {
            try
            {
                // Tạo file CSV tạm thời
                string csvFilePath = TaoFileCSV();
                
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(emailGui);
                mail.To.Add(emailNhan);
                mail.Subject = $"Báo cáo doanh thu {loaiBaoCao} - Coffee Management System";
                mail.Body = TaoNoiDungEmailHTML();
                mail.IsBodyHtml = true;

                // Đính kèm file CSV
                if (File.Exists(csvFilePath))
                {
                    Attachment attachment = new Attachment(csvFilePath);
                    attachment.Name = $"BaoCaoDoanhThu_{loaiBaoCao}_{DateTime.Now:yyyyMMdd_HHmmss}.csv";
                    mail.Attachments.Add(attachment);
                }

                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential(emailGui, matKhau);
                smtp.EnableSsl = true;

                smtp.Send(mail);
                
                // Xóa file CSV tạm thời
                if (File.Exists(csvFilePath))
                {
                    File.Delete(csvFilePath);
                }
                
                MessageBox.Show($"Đã gửi báo cáo thành công đến {emailNhan}!\nFile báo cáo CSV đã được đính kèm.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi gửi email: " + ex.Message + "\n\nLưu ý: Hãy đảm bảo email và mật khẩu ứng dụng đúng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string TaoFileCSV()
        {
            try
            {
                string fileName = $"BaoCaoDoanhThu_{loaiBaoCao}_{DateTime.Now:yyyyMMdd_HHmmss}.csv";
                string filePath = Path.Combine(Path.GetTempPath(), fileName);

                using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
                {
                    // Header thông tin báo cáo
                    writer.WriteLine($"Báo cáo doanh thu {loaiBaoCao}");
                    writer.WriteLine($"Ngày tạo báo cáo,{DateTime.Now:dd/MM/yyyy HH:mm}");
                    writer.WriteLine($"Hệ thống,Coffee Management System");
                    writer.WriteLine();

                    // Tổng quan
                    writer.WriteLine("TỔNG QUAN");
                    writer.WriteLine($"Tổng doanh thu,{baoCaoHienTai.TongDoanhThu:N0} VNĐ");
                    writer.WriteLine($"Số đơn hàng,{baoCaoHienTai.SoDonHang}");
                    writer.WriteLine($"Doanh thu trung bình,{baoCaoHienTai.DoanhThuTrungBinh:N0} VNĐ");
                    writer.WriteLine();

                    // Top món ăn
                    writer.WriteLine("TOP MÓN ĂN BÁN CHẠY");
                    writer.WriteLine("STT,Tên món,Số lượng bán,Doanh thu (VNĐ),% Tổng doanh thu");

                    for (int i = 0; i < baoCaoHienTai.TopMonAn.Count; i++)
                    {
                        var mon = baoCaoHienTai.TopMonAn[i];
                        writer.WriteLine($"{i + 1},{mon.TenMon},{mon.SoLuongBan},{mon.DoanhThu:N0},{mon.PhanTramDoanhThu:F1}%");
                    }
                }

                return filePath;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tạo file CSV: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
        }

        private string TaoNoiDungEmailHTML()
        {
            StringBuilder html = new StringBuilder();
            
            html.AppendLine("<!DOCTYPE html>");
            html.AppendLine("<html>");
            html.AppendLine("<head>");
            html.AppendLine("<meta charset='UTF-8'>");
            html.AppendLine("<title>Báo cáo doanh thu</title>");
            html.AppendLine("<style>");
            html.AppendLine("body { font-family: Arial, sans-serif; margin: 20px; background-color: #f5f5f5; }");
            html.AppendLine(".container { max-width: 800px; margin: 0 auto; background-color: white; padding: 30px; border-radius: 10px; box-shadow: 0 4px 6px rgba(0,0,0,0.1); }");
            html.AppendLine(".header { text-align: center; background: linear-gradient(135deg, #667eea 0%, #764ba2 100%); color: white; padding: 25px; border-radius: 8px; margin-bottom: 30px; }");
            html.AppendLine(".header h1 { margin: 0; font-size: 28px; font-weight: bold; }");
            html.AppendLine(".header p { margin: 10px 0 0 0; font-size: 16px; opacity: 0.9; }");
            html.AppendLine(".summary-box { background: linear-gradient(135deg, #f093fb 0%, #f5576c 100%); color: white; padding: 20px; border-radius: 8px; margin-bottom: 25px; }");
            html.AppendLine(".summary-row { display: flex; justify-content: space-between; margin-bottom: 10px; }");
            html.AppendLine(".summary-label { font-weight: bold; }");
            html.AppendLine(".summary-value { font-size: 18px; font-weight: bold; }");
            html.AppendLine(".table-container { margin-top: 25px; }");
            html.AppendLine(".table-title { font-size: 20px; font-weight: bold; color: #333; margin-bottom: 15px; padding-bottom: 10px; border-bottom: 3px solid #667eea; }");
            html.AppendLine("table { width: 100%; border-collapse: collapse; border-radius: 8px; overflow: hidden; box-shadow: 0 2px 8px rgba(0,0,0,0.1); }");
            html.AppendLine("th { background: linear-gradient(135deg, #667eea 0%, #764ba2 100%); color: white; padding: 15px; text-align: left; font-weight: bold; }");
            html.AppendLine("td { padding: 12px 15px; border-bottom: 1px solid #eee; }");
            html.AppendLine("tr:nth-child(even) { background-color: #f8f9ff; }");
            html.AppendLine("tr:hover { background-color: #e8f0fe; }");
            html.AppendLine(".rank-1 { background-color: #fff3cd !important; border-left: 4px solid #ffc107; }");
            html.AppendLine(".rank-2 { background-color: #e2e3e5 !important; border-left: 4px solid #6c757d; }");
            html.AppendLine(".rank-3 { background-color: #f8d7da !important; border-left: 4px solid #dc3545; }");
            html.AppendLine(".footer { text-align: center; margin-top: 30px; padding: 20px; background-color: #f8f9fa; border-radius: 8px; color: #666; }");
            html.AppendLine(".attachment-note { background-color: #d1ecf1; border: 1px solid #bee5eb; color: #0c5460; padding: 15px; border-radius: 8px; margin-top: 20px; }");
            html.AppendLine("</style>");
            html.AppendLine("</head>");
            html.AppendLine("<body>");
            
            html.AppendLine("<div class='container'>");
            
            // Header
            html.AppendLine("<div class='header'>");
            html.AppendLine($"<h1>📊 BÁO CÁO DOANH THU {loaiBaoCao.ToUpper()}</h1>");
            html.AppendLine("<p>Coffee Management System</p>");
            html.AppendLine($"<p>📅 Ngày tạo: {DateTime.Now:dd/MM/yyyy HH:mm}</p>");
            html.AppendLine("</div>");
            
            // Tổng quan
            html.AppendLine("<div class='summary-box'>");
            html.AppendLine("<div class='summary-row'>");
            html.AppendLine("<span class='summary-label'>💰 Tổng doanh thu:</span>");
            html.AppendLine($"<span class='summary-value'>{baoCaoHienTai.TongDoanhThu:N0} VNĐ</span>");
            html.AppendLine("</div>");
            html.AppendLine("<div class='summary-row'>");
            html.AppendLine("<span class='summary-label'>📝 Số đơn hàng:</span>");
            html.AppendLine($"<span class='summary-value'>{baoCaoHienTai.SoDonHang}</span>");
            html.AppendLine("</div>");
            html.AppendLine("<div class='summary-row'>");
            html.AppendLine("<span class='summary-label'>📈 Doanh thu trung bình:</span>");
            html.AppendLine($"<span class='summary-value'>{baoCaoHienTai.DoanhThuTrungBinh:N0} VNĐ</span>");
            html.AppendLine("</div>");
            html.AppendLine("</div>");
            
            // Top món ăn
            html.AppendLine("<div class='table-container'>");
            html.AppendLine("<div class='table-title'>🏆 TOP MÓN ĂN BÁN CHẠY</div>");
            html.AppendLine("<table>");
            html.AppendLine("<thead>");
            html.AppendLine("<tr>");
            html.AppendLine("<th style='width: 60px;'>Hạng</th>");
            html.AppendLine("<th>Tên món</th>");
            html.AppendLine("<th style='width: 100px;'>SL bán</th>");
            html.AppendLine("<th style='width: 150px;'>Doanh thu</th>");
            html.AppendLine("<th style='width: 100px;'>% Tổng DT</th>");
            html.AppendLine("</tr>");
            html.AppendLine("</thead>");
            html.AppendLine("<tbody>");
            
            for (int i = 0; i < baoCaoHienTai.TopMonAn.Count; i++)
            {
                var mon = baoCaoHienTai.TopMonAn[i];
                string rankClass = "";
                string rankIcon = "";
                
                if (i == 0) { rankClass = "rank-1"; rankIcon = "🥇"; }
                else if (i == 1) { rankClass = "rank-2"; rankIcon = "🥈"; }
                else if (i == 2) { rankClass = "rank-3"; rankIcon = "🥉"; }
                else { rankIcon = $"{i + 1}"; }
                
                html.AppendLine($"<tr class='{rankClass}'>");
                html.AppendLine($"<td style='text-align: center; font-weight: bold;'>{rankIcon}</td>");
                html.AppendLine($"<td style='font-weight: bold;'>{mon.TenMon}</td>");
                html.AppendLine($"<td style='text-align: center;'>{mon.SoLuongBan}</td>");
                html.AppendLine($"<td style='text-align: right; font-weight: bold;'>{mon.DoanhThu:N0} VNĐ</td>");
                html.AppendLine($"<td style='text-align: center;'>{mon.PhanTramDoanhThu:F1}%</td>");
                html.AppendLine("</tr>");
            }
            
            html.AppendLine("</tbody>");
            html.AppendLine("</table>");
            html.AppendLine("</div>");
            
            // Ghi chú file đính kèm
            html.AppendLine("<div class='attachment-note'>");
            html.AppendLine("<strong>📎 Lưu ý:</strong> Báo cáo chi tiết được đính kèm dưới dạng file CSV để bạn có thể xử lý và phân tích thêm.");
            html.AppendLine("</div>");
            
            // Footer
            html.AppendLine("<div class='footer'>");
            html.AppendLine("<p><strong>Coffee Management System</strong></p>");
            html.AppendLine("<p>Báo cáo được tạo tự động</p>");
            html.AppendLine("<p>© 2025 Coffee Management. All rights reserved.</p>");
            html.AppendLine("</div>");
            
            html.AppendLine("</div>");
            html.AppendLine("</body>");
            html.AppendLine("</html>");
            
            return html.ToString();
        }

        private string TaoNoiDungBaoCao()
        {
            StringBuilder noiDung = new StringBuilder();
            
            // Header
            noiDung.AppendLine("=====================================");
            noiDung.AppendLine($"       BÁO CÁO DOANH THU {loaiBaoCao.ToUpper()}");
            noiDung.AppendLine("       Coffee Management System");
            noiDung.AppendLine("=====================================");
            noiDung.AppendLine($"Ngày tạo: {DateTime.Now:dd/MM/yyyy HH:mm}");
            noiDung.AppendLine();
            
            // Tổng quan
            noiDung.AppendLine("TỔNG QUAN:");
            noiDung.AppendLine($"- Tổng doanh thu: {baoCaoHienTai.TongDoanhThu:N0} VNĐ");
            noiDung.AppendLine($"- Số đơn hàng: {baoCaoHienTai.SoDonHang}");
            noiDung.AppendLine($"- Doanh thu trung bình: {baoCaoHienTai.DoanhThuTrungBinh:N0} VNĐ");
            noiDung.AppendLine();
            
            // Top món ăn
            noiDung.AppendLine("TOP MÓN ĂN BÁN CHẠY:");
            noiDung.AppendLine("=====================================");
            noiDung.AppendLine($"{"Hạng",-5} {"Tên món",-25} {"SL bán",-10} {"Doanh thu",-15} {"% Tổng DT",-10}");
            noiDung.AppendLine("-------------------------------------");
            
            for (int i = 0; i < baoCaoHienTai.TopMonAn.Count; i++)
            {
                var mon = baoCaoHienTai.TopMonAn[i];
                string rank = (i + 1).ToString();
                
                noiDung.AppendLine($"{rank,-5} {mon.TenMon,-25} {mon.SoLuongBan,-10} {mon.DoanhThu + " VNĐ",-15} {mon.PhanTramDoanhThu:F1}%");
            }
            
            noiDung.AppendLine("=====================================");
            noiDung.AppendLine();
            noiDung.AppendLine("© 2025 Coffee Management System");
            
            return noiDung.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            AdminDashboard adminDashboard = new AdminDashboard();
            adminDashboard.Show();
        }
    }
}
