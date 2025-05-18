using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Quanlyquancf.DAO;

namespace Quanlyquancf
{

    public partial class Khachhang : Form
    {

        public class MonAn
        {
            public string MaSP { get; set; }
            public string TenMon { get; set; }
            public string MaDM { get; set; }
            public decimal DonGia { get; set; }

            public MonAn() { }

            public MonAn(string maSP, string tenMon, string maDM, decimal donGia)
            {
                MaSP = maSP;
                TenMon = tenMon;
                MaDM = maDM;
                DonGia = donGia;
            }

            public override string ToString()
            {
                return TenMon;
            }
        }

        public class ChiTietDonHangItem
        {
            public MonAn Mon { get; set; }
            public int SoLuong { get; set; }
            public decimal ThanhTien => Mon.DonGia * SoLuong;

            public ChiTietDonHangItem(MonAn mon, int soLuong)
            {
                Mon = mon;
                SoLuong = soLuong;
            }
        }

        private List<MonAn> danhSachTatCaMonAn = new List<MonAn>();
        private Dictionary<string, string> danhSachDanhMuc = new Dictionary<string, string>();
        private DataTable danhSachBan;
        private Dictionary<string, string> danhSachKhuVuc = new Dictionary<string, string>();
        private Dictionary<string, string> banVaKhuVuc = new Dictionary<string, string>();

        private Button banDaChonHienTai = null;
        private List<ChiTietDonHangItem> danhSachMonTrongDonHangHienTai = new List<ChiTietDonHangItem>();

        // TODO: Cần có cách để lấy MAKH (Mã khách hàng) hiện tại, giả sử có một biến lưu trữ
        private string maKhachHangHienTai; 

        public Khachhang(string maKH)
        {
            InitializeComponent();
            maKhachHangHienTai = maKH;
        }

        private void Khachhang_Load(object sender, EventArgs e)
        {
            LoadDataFromDatabase();
            NapLoaiMonVaoComboBox();
            SetupListView();
            GanSuKienChoControls();
            numericUpDown1.Value = 1;
            numericUpDown1.Minimum = 1;
        }

        private void LoadDataFromDatabase()
        {
            // Sử dụng lớp DataProvider được định nghĩa tạm thời ở trên
            DataProvider dataProvider = new DataProvider();

            string queryDanhMuc = "SELECT MADM, TENDM FROM DANHMUCSP";
            DataTable dtDanhMuc = dataProvider.ExcuteQuery(queryDanhMuc);
            danhSachDanhMuc.Clear();
            foreach (DataRow row in dtDanhMuc.Rows)
            {
                danhSachDanhMuc.Add(row["MADM"].ToString(), row["TENDM"].ToString());
            }

            string querySanPham = "SELECT MASP, TENSP, GIASP, MADM FROM SANPHAM";
            DataTable dtSanPham = dataProvider.ExcuteQuery(querySanPham);
            danhSachTatCaMonAn.Clear();
            foreach (DataRow row in dtSanPham.Rows)
            {
                danhSachTatCaMonAn.Add(new MonAn
                {
                    MaSP = row["MASP"].ToString(),
                    TenMon = row["TENSP"].ToString(),
                    MaDM = row["MADM"].ToString(),
                    DonGia = (decimal)row["GIASP"]
                });
            }

            string queryKhuVuc = "SELECT MAKV, TENKV FROM KHUVUC";
            DataTable dtKhuVuc = dataProvider.ExcuteQuery(queryKhuVuc);
            danhSachKhuVuc.Clear();
            danhSachKhuVuc.Add("TatCa", "Tất cả");
            foreach (DataRow row in dtKhuVuc.Rows)
            {
                string maKV = row["MAKV"].ToString();
                string tenKV = row["TENKV"].ToString();
                danhSachKhuVuc.Add(maKV, tenKV);
            }


            string queryBan = "SELECT MABAN, SOBAN FROM BAN";
            danhSachBan = dataProvider.ExcuteQuery(queryBan);

            PopulateTableButtons();
            SetAreaFilterButtonTags();
        }

        private void PopulateTableButtons()
        {
            flowLayoutPanel1.Controls.Clear();
            banVaKhuVuc.Clear();

            if (danhSachBan != null)
            {
                int i = 0;
                foreach (DataRow row in danhSachBan.Rows)
                {
                    Button btnBan = new Button();
                    btnBan.Width = 100;
                    btnBan.Height = 80;
                    btnBan.Margin = new Padding(5);

                    string maBan = row["MABAN"].ToString();
                    string soBan = row["SOBAN"].ToString();

                    btnBan.Name = maBan;
                    btnBan.Text = soBan;
                    btnBan.BackColor = SystemColors.Control;


                    string khuVucAssigned = "Unknown";
                    if (i < 5)
                    {
                        khuVucAssigned = "NgoaiTroi";
                    }
                    else if (i < 9)
                    {
                        khuVucAssigned = "PhongLanh";
                    }
                    else if (i < 12)
                    {
                        khuVucAssigned = "Rooftop";
                    }

                    btnBan.Tag = maBan; // Lưu MABAN vào Tag của nút bàn
                    banVaKhuVuc[btnBan.Name] = khuVucAssigned;


                    btnBan.Click += BtnBan_Click;
                    flowLayoutPanel1.Controls.Add(btnBan);
                    i++;
                }
            }
        }

        private void SetAreaFilterButtonTags()
        {
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is Button btnFilter)
                {
                    switch (btnFilter.Text)
                    {
                        case "Tất cả":
                            btnFilter.Tag = "TatCa";
                            break;
                        case "Ngoài trời":
                            btnFilter.Tag = "NgoaiTroi";
                            break;
                        case "Phòng lạnh":
                            btnFilter.Tag = "PhongLanh";
                            break;
                        case "Roof top":
                        case "Rooftop":
                            btnFilter.Tag = "Rooftop";
                            break;
                    }
                }
            }
        }


        private void NapLoaiMonVaoComboBox()
        {
            comboBox1.Items.Clear();
            var distinctCategories = danhSachTatCaMonAn.Select(m => m.MaDM).Distinct().ToList();

            foreach (string maDM in distinctCategories)
            {
                if (danhSachDanhMuc.TryGetValue(maDM, out string tenDM))
                {
                    comboBox1.Items.Add(tenDM);
                }
            }

            if (comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedIndex = 0;
            }
        }

        private void SetupListView()
        {
            listView1.View = View.Details;
            listView1.Columns.Clear();
            listView1.Columns.Add("Tên món", 250, HorizontalAlignment.Left);
            listView1.Columns.Add("SL", 60, HorizontalAlignment.Center);
            listView1.Columns.Add("Đơn giá", 100, HorizontalAlignment.Right);
            listView1.Columns.Add("Thành tiền", 120, HorizontalAlignment.Right);
            listView1.GridLines = true;
            listView1.FullRowSelect = true;
        }

        private void GanSuKienChoControls()
        {
            btnAdd.Click += btnAdd_Click;

            button1.Click += btnDatMon_Click;

            foreach (Control ctrl in flowLayoutPanel1.Controls)
            {
                if (ctrl is Button btnBan)
                {
                    btnBan.Click += BtnBan_Click;
                }
            }

            button16.Click += BtnLocKhuVuc_Click;
            button17.Click += BtnLocKhuVuc_Click;
            button18.Click += BtnLocKhuVuc_Click;
            button19.Click += BtnLocKhuVuc_Click;


            listView1.KeyDown += listView1_KeyDown;
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null) return;

            string loaiMonDaChonText = comboBox1.SelectedItem.ToString();

            string maDMDaChon = danhSachDanhMuc.FirstOrDefault(x => x.Value == loaiMonDaChonText).Key;


            comboBox2.Items.Clear();
            comboBox2.Text = "";

            var monTheoLoai = danhSachTatCaMonAn.Where(m => m.MaDM == maDMDaChon).ToList();

            foreach (MonAn mon in monTheoLoai)
            {
                comboBox2.Items.Add(mon);
            }

            if (comboBox2.Items.Count > 0)
            {
                comboBox2.SelectedIndex = 0;
            }
        }

        private void BtnBan_Click(object sender, EventArgs e)
        {
            Button btnClicked = sender as Button;
            if (btnClicked != null)
            {
                if (banDaChonHienTai != null)
                {
                    banDaChonHienTai.BackColor = SystemColors.Control;
                }

                btnClicked.BackColor = Color.LightSkyBlue;
                banDaChonHienTai = btnClicked;

                if (lblBanDangChonDisplay != null)
                    lblBanDangChonDisplay.Text = $"Đang chọn: {banDaChonHienTai.Text}";


                danhSachMonTrongDonHangHienTai.Clear();
                CapNhatListViewVaTongTien();
            }
        }

        private void BtnLocKhuVuc_Click(object sender, EventArgs e)
        {
            Button btnLocClicked = sender as Button;
            if (btnLocClicked == null || btnLocClicked.Tag == null) return;

            string khuVucCanLocTag = btnLocClicked.Tag.ToString();

            foreach (Control ctrlBan in flowLayoutPanel1.Controls)
            {
                if (ctrlBan is Button btnBan)
                {
                    // Lấy Tag của nút bàn, nơi lưu MABAN hoặc khu vực gán tạm
                    string banTag = btnBan.Tag?.ToString();

                    if (khuVucCanLocTag == "TatCa")
                    {
                        btnBan.Visible = true;
                    }
                    else
                    {
                        // Lấy khu vực gán tạm từ dictionary banVaKhuVuc dựa vào Name (MABAN) của nút bàn
                        if (banVaKhuVuc.TryGetValue(btnBan.Name, out string khuVucCuaBanAssigned))
                        {
                            btnBan.Visible = (khuVucCuaBanAssigned == khuVucCanLocTag);
                        }
                        else
                        {
                            // Nếu không tìm thấy trong dictionary, ẩn nút bàn (hoặc xử lý khác tùy ý)
                            btnBan.Visible = false;
                        }
                    }
                }
            }
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (banDaChonHienTai == null)
            {
                MessageBox.Show("Vui lòng chọn bàn trước!", "Chưa chọn bàn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (comboBox2.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn một món ăn!", "Chưa chọn món", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MonAn monDuocChon = comboBox2.SelectedItem as MonAn;
            int soLuong = (int)numericUpDown1.Value;

            if (monDuocChon != null && soLuong > 0)
            {
                ChiTietDonHangItem itemDaCo = danhSachMonTrongDonHangHienTai.FirstOrDefault(item => item.Mon.MaSP == monDuocChon.MaSP);

                if (itemDaCo != null)
                {
                    itemDaCo.SoLuong += soLuong;
                }
                else
                {
                    danhSachMonTrongDonHangHienTai.Add(new ChiTietDonHangItem(monDuocChon, soLuong));
                }
                CapNhatListViewVaTongTien();
                numericUpDown1.Value = 1;
            }
        }

        private void CapNhatListViewVaTongTien()
        {
            listView1.Items.Clear();
            decimal tongTienTamThoi = 0;

            foreach (var chiTiet in danhSachMonTrongDonHangHienTai)
            {
                ListViewItem item = new ListViewItem(chiTiet.Mon.TenMon);
                item.SubItems.Add(chiTiet.SoLuong.ToString());
                item.SubItems.Add(chiTiet.Mon.DonGia.ToString("N0"));
                item.SubItems.Add(chiTiet.ThanhTien.ToString("N0"));
                item.Tag = chiTiet;

                listView1.Items.Add(item);
                tongTienTamThoi += chiTiet.ThanhTien;
            }

            if (lblTongTienDisplay != null)
                lblTongTienDisplay.Text = $"Tổng tiền: {tongTienTamThoi:N0} VNĐ";
        }

        private void btnDatMon_Click(object sender, EventArgs e)
        {
            if (banDaChonHienTai == null)
            {
                MessageBox.Show("Vui lòng chọn bàn để đặt món!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (danhSachMonTrongDonHangHienTai.Count == 0)
            {
                MessageBox.Show("Chưa có món nào trong đơn hàng của bàn này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maBanHienTai = banDaChonHienTai.Tag.ToString(); 
            decimal tongTienHoaDon = danhSachMonTrongDonHangHienTai.Sum(ct => ct.ThanhTien);

            StringBuilder chiTietStr = new StringBuilder();
            foreach (var ct in danhSachMonTrongDonHangHienTai)
            {
                chiTietStr.AppendLine($"- {ct.Mon.TenMon} (x{ct.SoLuong}): {ct.ThanhTien:N0} VNĐ");
            }

            DialogResult result = MessageBox.Show($"Xác nhận đặt món cho {banDaChonHienTai.Text}:\n\n{chiTietStr.ToString()}\nTổng cộng: {tongTienHoaDon:N0} VNĐ",
                                                    "Xác nhận đơn hàng", MessageBoxButtons.YesNo, MessageBoxIcon.Question);


            if (result == DialogResult.Yes)
            {
                
                DataProvider dataProvider = new DataProvider();
                SqlTransaction transaction = null; 

                try
                {

                    string newMaDon = GenerateUniqueMaDon();

                    string insertDonHangQuery = $"INSERT INTO DONHANG (MADON, MAKH, TONGGIADON, TRANGTHAIDON) VALUES ('{newMaDon}', '{maKhachHangHienTai}', {tongTienHoaDon}, N'Pending')";
                    dataProvider.ExcuteNonQuery(insertDonHangQuery);

                    foreach (var chiTiet in danhSachMonTrongDonHangHienTai)
                    {
                        string insertChiTietDonHangQuery = $"INSERT INTO CHITIETDONHANG (MADON, MASP, SOLUONGSP, GIAMOISP, TONGGIASANPHAM) VALUES ('{newMaDon}', '{chiTiet.Mon.MaSP}', {chiTiet.SoLuong}, {chiTiet.Mon.DonGia}, {chiTiet.ThanhTien})";
                        dataProvider.ExcuteNonQuery(insertChiTietDonHangQuery);
                    }

                    string insertChiTietDonBanQuery = $"INSERT INTO CHITIETDONBAN (MADON, MABAN) VALUES ('{newMaDon}', '{maBanHienTai}')";
                    dataProvider.ExcuteNonQuery(insertChiTietDonBanQuery);

                    MessageBox.Show("Đặt món thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    danhSachMonTrongDonHangHienTai.Clear();
                    CapNhatListViewVaTongTien();

                    if (banDaChonHienTai != null)
                    {
                        banDaChonHienTai.BackColor = SystemColors.Control;
                    }
                    banDaChonHienTai = null;

                    if (lblBanDangChonDisplay != null) lblBanDangChonDisplay.Text = "Vui lòng chọn bàn";
                    numericUpDown1.Value = 1;
                    if (comboBox1.Items.Count > 0) comboBox1.SelectedIndex = 0;

                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Lỗi khi lưu đơn hàng vào cơ sở dữ liệu: {ex.Message}", "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private string GenerateUniqueMaDon()
        {
            DataProvider dataProvider = new DataProvider();
            string query = "SELECT TOP 1 MADON FROM DONHANG ORDER BY MADON DESC";
            DataTable result = dataProvider.ExcuteQuery(query);

            int nextNumber = 1; 

            if (result != null && result.Rows.Count > 0)
            {
                string lastMaDon = result.Rows[0]["MADON"].ToString();
               
                if (lastMaDon.StartsWith("HD") && lastMaDon.Length > 2)
                {
                    string numberPart = lastMaDon.Substring(2);
                    if (int.TryParse(numberPart, out int lastNumber))
                    {
                        nextNumber = lastNumber + 1;
                    }
                }
            }

            return $"HD{nextNumber:D3}"; 
        }


        private void listView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (listView1.SelectedItems.Count > 0)
                {
                    ListViewItem selectedLvItem = listView1.SelectedItems[0];
                    ChiTietDonHangItem chiTietCanXoa = selectedLvItem.Tag as ChiTietDonHangItem;

                    if (chiTietCanXoa != null)
                    {
                        DialogResult confirmDelete = MessageBox.Show($"Bạn có chắc muốn xóa món '{chiTietCanXoa.Mon.TenMon}' khỏi đơn hàng?",
                                                                     "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (confirmDelete == DialogResult.Yes)
                        {
                            danhSachMonTrongDonHangHienTai.Remove(chiTietCanXoa);
                            CapNhatListViewVaTongTien();
                        }
                    }
                }
                e.Handled = true;
            }
        }

        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng Thông tin cá nhân cần được hoàn thiện để hiển thị thông tin người dùng.");
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
