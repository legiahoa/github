using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CoffeeManagement.DAO;

namespace CoffeeManagement.Forms
{
    public partial class ReportsForm : Form
    {
        public ReportsForm()
        {
            InitializeComponent();
        }

        private void ReportsForm_Load(object sender, EventArgs e)
        {
            dtpFromDate.Value = DateTime.Now.AddDays(-30);
            dtpToDate.Value = DateTime.Now;
            cboReportType.SelectedIndex = 0;
            LoadSalesReport();
        }

        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            try
            {
                string reportType = cboReportType.Text;
                DateTime fromDate = dtpFromDate.Value.Date;
                DateTime toDate = dtpToDate.Value.Date.AddDays(1).AddSeconds(-1);

                switch (reportType)
                {
                    case "Báo cáo doanh thu":
                        LoadSalesReport();
                        break;
                    case "Báo cáo sản phẩm bán chạy":
                        LoadProductReport();
                        break;
                    case "Báo cáo khách hàng":
                        LoadCustomerReport();
                        break;
                    case "Báo cáo nhân viên":
                        LoadEmployeeReport();
                        break;
                    case "Báo cáo tồn kho":
                        LoadInventoryReport();
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tạo báo cáo: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadSalesReport()
        {
            try
            {
                DateTime fromDate = dtpFromDate.Value.Date;
                DateTime toDate = dtpToDate.Value.Date.AddDays(1).AddSeconds(-1);

                string query = @"SELECT 
                               CONVERT(date, dh.NGAYDAT) as NgayBan,
                               COUNT(dh.MADH) as SoLuongDon,
                               SUM(dh.TONGTIEN) as TongTien,
                               AVG(dh.TONGTIEN) as TrungBinhDon
                               FROM DONHANG dh
                               WHERE dh.NGAYDAT BETWEEN @fromDate AND @toDate
                               AND dh.TRANGTHAI != N'Đã hủy'
                               GROUP BY CONVERT(date, dh.NGAYDAT)
                               ORDER BY NgayBan DESC";

                DataTable result = DataProvider.ExecuteQuery(query, new object[] { fromDate, toDate });
                dgvReport.DataSource = result;

                // Configure columns
                if (dgvReport.Columns.Count > 0)
                {
                    dgvReport.Columns["NgayBan"].HeaderText = "Ngày bán";
                    dgvReport.Columns["SoLuongDon"].HeaderText = "Số lượng đơn";
                    dgvReport.Columns["TongTien"].HeaderText = "Tổng tiền";
                    dgvReport.Columns["TrungBinhDon"].HeaderText = "Trung bình/đơn";

                    dgvReport.Columns["NgayBan"].Width = 120;
                    dgvReport.Columns["SoLuongDon"].Width = 120;
                    dgvReport.Columns["TongTien"].Width = 150;
                    dgvReport.Columns["TrungBinhDon"].Width = 150;

                    // Format currency columns
                    dgvReport.Columns["TongTien"].DefaultCellStyle.Format = "N0";
                    dgvReport.Columns["TrungBinhDon"].DefaultCellStyle.Format = "N0";
                }

                LoadSalesSummary(fromDate, toDate);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải báo cáo doanh thu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadSalesSummary(DateTime fromDate, DateTime toDate)
        {
            try
            {
                // Total revenue
                string revenueQuery = @"SELECT ISNULL(SUM(TONGTIEN), 0) FROM DONHANG 
                                      WHERE NGAYDAT BETWEEN @fromDate AND @toDate 
                                      AND TRANGTHAI != N'Đã hủy'";
                DataTable revenueResult = DataProvider.ExecuteQuery(revenueQuery, new object[] { fromDate, toDate });
                decimal totalRevenue = Convert.ToDecimal(revenueResult.Rows[0][0]);

                // Total orders
                string ordersQuery = @"SELECT COUNT(*) FROM DONHANG 
                                     WHERE NGAYDAT BETWEEN @fromDate AND @toDate 
                                     AND TRANGTHAI != N'Đã hủy'";
                DataTable ordersResult = DataProvider.ExecuteQuery(ordersQuery, new object[] { fromDate, toDate });
                int totalOrders = Convert.ToInt32(ordersResult.Rows[0][0]);

                // Average order value
                decimal avgOrderValue = totalOrders > 0 ? totalRevenue / totalOrders : 0;

                lblTotalRevenue.Text = $"Tổng doanh thu: {totalRevenue:N0} VNĐ";
                lblTotalOrders.Text = $"Tổng số đơn: {totalOrders}";
                lblAvgOrderValue.Text = $"Giá trị TB/đơn: {avgOrderValue:N0} VNĐ";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải tổng quan: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadProductReport()
        {
            try
            {
                DateTime fromDate = dtpFromDate.Value.Date;
                DateTime toDate = dtpToDate.Value.Date.AddDays(1).AddSeconds(-1);

                string query = @"SELECT sp.TENSP, 
                               SUM(ct.SOLUONG) as SoLuongBan,
                               SUM(ct.SOLUONG * ct.GIA) as DoanhThu,
                               AVG(ct.GIA) as GiaTrungBinh
                               FROM CHITIETDONHANG ct
                               INNER JOIN SANPHAM sp ON ct.MASP = sp.MASP
                               INNER JOIN DONHANG dh ON ct.MADH = dh.MADH
                               WHERE dh.NGAYDAT BETWEEN @fromDate AND @toDate
                               AND dh.TRANGTHAI != N'Đã hủy'
                               GROUP BY sp.TENSP, sp.MASP
                               ORDER BY SoLuongBan DESC";

                DataTable result = DataProvider.ExecuteQuery(query, new object[] { fromDate, toDate });
                dgvReport.DataSource = result;

                if (dgvReport.Columns.Count > 0)
                {
                    dgvReport.Columns["TENSP"].HeaderText = "Tên sản phẩm";
                    dgvReport.Columns["SoLuongBan"].HeaderText = "Số lượng bán";
                    dgvReport.Columns["DoanhThu"].HeaderText = "Doanh thu";
                    dgvReport.Columns["GiaTrungBinh"].HeaderText = "Giá trung bình";

                    dgvReport.Columns["TENSP"].Width = 200;
                    dgvReport.Columns["SoLuongBan"].Width = 120;
                    dgvReport.Columns["DoanhThu"].Width = 150;
                    dgvReport.Columns["GiaTrungBinh"].Width = 150;

                    dgvReport.Columns["DoanhThu"].DefaultCellStyle.Format = "N0";
                    dgvReport.Columns["GiaTrungBinh"].DefaultCellStyle.Format = "N0";
                }

                ClearSummary();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải báo cáo sản phẩm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCustomerReport()
        {
            try
            {
                DateTime fromDate = dtpFromDate.Value.Date;
                DateTime toDate = dtpToDate.Value.Date.AddDays(1).AddSeconds(-1);

                string query = @"SELECT kh.HOTEN, kh.SDT,
                               COUNT(dh.MADH) as SoLuongDon,
                               SUM(dh.TONGTIEN) as TongTien,
                               MAX(dh.NGAYDAT) as LanMuaCuoi
                               FROM KHACHHANG kh
                               LEFT JOIN DONHANG dh ON kh.MAKH = dh.MAKH
                               WHERE dh.NGAYDAT BETWEEN @fromDate AND @toDate
                               AND dh.TRANGTHAI != N'Đã hủy'
                               GROUP BY kh.MAKH, kh.HOTEN, kh.SDT
                               ORDER BY TongTien DESC";

                DataTable result = DataProvider.ExecuteQuery(query, new object[] { fromDate, toDate });
                dgvReport.DataSource = result;

                if (dgvReport.Columns.Count > 0)
                {
                    dgvReport.Columns["HOTEN"].HeaderText = "Tên khách hàng";
                    dgvReport.Columns["SDT"].HeaderText = "Số điện thoại";
                    dgvReport.Columns["SoLuongDon"].HeaderText = "Số lượng đơn";
                    dgvReport.Columns["TongTien"].HeaderText = "Tổng tiền";
                    dgvReport.Columns["LanMuaCuoi"].HeaderText = "Lần mua cuối";

                    dgvReport.Columns["HOTEN"].Width = 180;
                    dgvReport.Columns["SDT"].Width = 120;
                    dgvReport.Columns["SoLuongDon"].Width = 120;
                    dgvReport.Columns["TongTien"].Width = 150;
                    dgvReport.Columns["LanMuaCuoi"].Width = 150;

                    dgvReport.Columns["TongTien"].DefaultCellStyle.Format = "N0";
                }

                ClearSummary();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải báo cáo khách hàng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadEmployeeReport()
        {
            try
            {
                DateTime fromDate = dtpFromDate.Value.Date;
                DateTime toDate = dtpToDate.Value.Date.AddDays(1).AddSeconds(-1);

                string query = @"SELECT nv.HOTEN, nv.CHUCVU,
                               COUNT(dh.MADH) as SoLuongDon,
                               SUM(dh.TONGTIEN) as TongTien
                               FROM NHANVIEN nv
                               LEFT JOIN DONHANG dh ON nv.MANV = dh.MANV
                               WHERE dh.NGAYDAT BETWEEN @fromDate AND @toDate
                               AND dh.TRANGTHAI != N'Đã hủy'
                               GROUP BY nv.MANV, nv.HOTEN, nv.CHUCVU
                               ORDER BY TongTien DESC";

                DataTable result = DataProvider.ExecuteQuery(query, new object[] { fromDate, toDate });
                dgvReport.DataSource = result;

                if (dgvReport.Columns.Count > 0)
                {
                    dgvReport.Columns["HOTEN"].HeaderText = "Tên nhân viên";
                    dgvReport.Columns["CHUCVU"].HeaderText = "Chức vụ";
                    dgvReport.Columns["SoLuongDon"].HeaderText = "Số lượng đơn";
                    dgvReport.Columns["TongTien"].HeaderText = "Tổng tiền";

                    dgvReport.Columns["HOTEN"].Width = 180;
                    dgvReport.Columns["CHUCVU"].Width = 120;
                    dgvReport.Columns["SoLuongDon"].Width = 120;
                    dgvReport.Columns["TongTien"].Width = 150;

                    dgvReport.Columns["TongTien"].DefaultCellStyle.Format = "N0";
                }

                ClearSummary();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải báo cáo nhân viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadInventoryReport()
        {
            try
            {
                string query = @"SELECT sp.TENSP, kh.SOLUONG, kh.SOLUONGTOITHIEU,
                               kh.NGAYNHAP, kh.HANSUDUNG,
                               CASE 
                                   WHEN kh.SOLUONG <= kh.SOLUONGTOITHIEU THEN N'Thiếu hàng'
                                   WHEN kh.HANSUDUNG < GETDATE() THEN N'Hết hạn'
                                   ELSE N'Bình thường'
                               END as TinhTrang
                               FROM KHOHANGHOA kh
                               INNER JOIN SANPHAM sp ON kh.MASP = sp.MASP
                               ORDER BY 
                               CASE 
                                   WHEN kh.SOLUONG <= kh.SOLUONGTOITHIEU THEN 1
                                   WHEN kh.HANSUDUNG < GETDATE() THEN 2
                                   ELSE 3
                               END, sp.TENSP";

                DataTable result = DataProvider.ExecuteQuery(query);
                dgvReport.DataSource = result;

                if (dgvReport.Columns.Count > 0)
                {
                    dgvReport.Columns["TENSP"].HeaderText = "Tên sản phẩm";
                    dgvReport.Columns["SOLUONG"].HeaderText = "Tồn kho";
                    dgvReport.Columns["SOLUONGTOITHIEU"].HeaderText = "Tối thiểu";
                    dgvReport.Columns["NGAYNHAP"].HeaderText = "Ngày nhập";
                    dgvReport.Columns["HANSUDUNG"].HeaderText = "Hạn sử dụng";
                    dgvReport.Columns["TinhTrang"].HeaderText = "Tình trạng";

                    dgvReport.Columns["TENSP"].Width = 200;
                    dgvReport.Columns["SOLUONG"].Width = 100;
                    dgvReport.Columns["SOLUONGTOITHIEU"].Width = 100;
                    dgvReport.Columns["NGAYNHAP"].Width = 120;
                    dgvReport.Columns["HANSUDUNG"].Width = 120;
                    dgvReport.Columns["TinhTrang"].Width = 120;

                    // Color coding
                    foreach (DataGridViewRow row in dgvReport.Rows)
                    {
                        string status = row.Cells["TinhTrang"].Value?.ToString();
                        if (status == "Thiếu hàng")
                            row.DefaultCellStyle.BackColor = Color.FromArgb(255, 235, 235);
                        else if (status == "Hết hạn")
                            row.DefaultCellStyle.BackColor = Color.FromArgb(255, 220, 220);
                    }
                }

                ClearSummary();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải báo cáo tồn kho: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearSummary()
        {
            lblTotalRevenue.Text = "Tổng doanh thu: --";
            lblTotalOrders.Text = "Tổng số đơn: --";
            lblAvgOrderValue.Text = "Giá trị TB/đơn: --";
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "Excel Files|*.xlsx";
                saveDialog.Title = "Lưu báo cáo";
                saveDialog.FileName = $"BaoCao_{cboReportType.Text}_{DateTime.Now:yyyyMMdd}";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    // Here you would implement Excel export functionality
                    // For now, we'll show a placeholder message
                    MessageBox.Show("Chức năng xuất Excel đang được phát triển!", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xuất báo cáo: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cboReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Update date controls visibility based on report type
            bool showDateRange = cboReportType.Text != "Báo cáo tồn kho";
            dtpFromDate.Visible = showDateRange;
            dtpToDate.Visible = showDateRange;
            lblFromDate.Visible = showDateRange;
            lblToDate.Visible = showDateRange;
        }
    }
}
