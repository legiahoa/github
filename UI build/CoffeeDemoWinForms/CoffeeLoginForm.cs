using System;
using System.Drawing;
using System.Windows.Forms;

namespace CoffeeDemoWinForms
{
    public partial class CoffeeLoginForm : Form
    {
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnLogin;
        private Button btnRegisterCustomer;
        private Button btnRegisterEmployee;
        private Label lblTitle;
        private Label lblUsername;
        private Label lblPassword;
        private Panel pnlMain;

        public CoffeeLoginForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.pnlMain = new Panel();
            this.txtUsername = new TextBox();
            this.txtPassword = new TextBox();
            this.btnLogin = new Button();
            this.btnRegisterCustomer = new Button();
            this.btnRegisterEmployee = new Button();
            this.lblTitle = new Label();
            this.lblUsername = new Label();
            this.lblPassword = new Label();
            
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();

            // pnlMain
            this.pnlMain.BackColor = Color.FromArgb(54, 85, 149);
            this.pnlMain.Dock = DockStyle.Fill;
            this.pnlMain.Controls.Add(this.lblTitle);
            this.pnlMain.Controls.Add(this.lblUsername);
            this.pnlMain.Controls.Add(this.txtUsername);
            this.pnlMain.Controls.Add(this.lblPassword);
            this.pnlMain.Controls.Add(this.txtPassword);
            this.pnlMain.Controls.Add(this.btnLogin);
            this.pnlMain.Controls.Add(this.btnRegisterCustomer);
            this.pnlMain.Controls.Add(this.btnRegisterEmployee);

            // lblTitle
            this.lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.White;
            this.lblTitle.Location = new Point(50, 30);
            this.lblTitle.Size = new Size(400, 50);
            this.lblTitle.Text = "☕ COFFEE MANAGEMENT SYSTEM";
            this.lblTitle.TextAlign = ContentAlignment.MiddleCenter;

            // lblUsername
            this.lblUsername.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblUsername.ForeColor = Color.White;
            this.lblUsername.Location = new Point(100, 110);
            this.lblUsername.Size = new Size(100, 25);
            this.lblUsername.Text = "Tên đăng nhập:";

            // txtUsername
            this.txtUsername.Font = new Font("Segoe UI", 11F);
            this.txtUsername.Location = new Point(100, 135);
            this.txtUsername.Size = new Size(300, 30);
            this.txtUsername.Text = "admin"; // Demo default

            // lblPassword
            this.lblPassword.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblPassword.ForeColor = Color.White;
            this.lblPassword.Location = new Point(100, 175);
            this.lblPassword.Size = new Size(100, 25);
            this.lblPassword.Text = "Mật khẩu:";

            // txtPassword
            this.txtPassword.Font = new Font("Segoe UI", 11F);
            this.txtPassword.Location = new Point(100, 200);
            this.txtPassword.Size = new Size(300, 30);
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Text = "123456"; // Demo default

            // btnLogin
            this.btnLogin.BackColor = Color.FromArgb(46, 204, 113);
            this.btnLogin.FlatStyle = FlatStyle.Flat;
            this.btnLogin.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.btnLogin.ForeColor = Color.White;
            this.btnLogin.Location = new Point(100, 250);
            this.btnLogin.Size = new Size(300, 45);
            this.btnLogin.Text = "🔑 ĐĂNG NHẬP";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new EventHandler(this.btnLogin_Click);

            // btnRegisterCustomer
            this.btnRegisterCustomer.BackColor = Color.FromArgb(230, 126, 34);
            this.btnRegisterCustomer.FlatStyle = FlatStyle.Flat;
            this.btnRegisterCustomer.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnRegisterCustomer.ForeColor = Color.White;
            this.btnRegisterCustomer.Location = new Point(100, 310);
            this.btnRegisterCustomer.Size = new Size(145, 40);
            this.btnRegisterCustomer.Text = "👤 Đăng ký KH";
            this.btnRegisterCustomer.UseVisualStyleBackColor = false;
            this.btnRegisterCustomer.Click += new EventHandler(this.btnRegisterCustomer_Click);

            // btnRegisterEmployee
            this.btnRegisterEmployee.BackColor = Color.FromArgb(142, 68, 173);
            this.btnRegisterEmployee.FlatStyle = FlatStyle.Flat;
            this.btnRegisterEmployee.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnRegisterEmployee.ForeColor = Color.White;
            this.btnRegisterEmployee.Location = new Point(255, 310);
            this.btnRegisterEmployee.Size = new Size(145, 40);
            this.btnRegisterEmployee.Text = "👔 Đăng ký NV";
            this.btnRegisterEmployee.UseVisualStyleBackColor = false;
            this.btnRegisterEmployee.Click += new EventHandler(this.btnRegisterEmployee_Click);

            // CoffeeLoginForm
            this.ClientSize = new Size(500, 400);
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Coffee Management System - Demo";
            this.BackColor = Color.White;
            
            this.pnlMain.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Demo logic - simulate different user types
            if (username.ToLower() == "admin" && password == "123456")
            {
                MessageBox.Show("🎉 Đăng nhập thành công với quyền ADMIN!\n\n" +
                    "Demo Coffee Management System:\n" +
                    "✅ Xác thực người dùng\n" +
                    "✅ Phân quyền theo vai trò\n" +
                    "✅ Giao diện hiện đại\n\n" +
                    "Để xem đầy đủ chức năng, hãy cài đặt GunaUI2 và chạy project chính.",
                    "Đăng nhập thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                // Open simple admin dashboard
                OpenAdminDashboard();
            }
            else if (username.StartsWith("KH") && password == "123456")
            {
                MessageBox.Show("🎉 Đăng nhập thành công với quyền KHÁCH HÀNG!\n\n" +
                    "Demo hệ thống đặt hàng của khách hàng sẽ được mở.",
                    "Đăng nhập thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                OpenCustomerDashboard();
            }
            else
            {
                MessageBox.Show("❌ Sai tên đăng nhập hoặc mật khẩu!\n\n" +
                    "Demo accounts:\n" +
                    "• Admin: admin / 123456\n" +
                    "• Customer: KH001 / 123456",
                    "Đăng nhập thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRegisterCustomer_Click(object sender, EventArgs e)
        {
            MessageBox.Show("🆕 Chức năng đăng ký khách hàng\n\n" +
                "Trong hệ thống đầy đủ:\n" +
                "✅ Tự động tạo mã KH (KH001, KH002...)\n" +
                "✅ Validation dữ liệu đầu vào\n" +
                "✅ Lưu vào database\n" +
                "✅ Giao diện đẹp với GunaUI2",
                "Demo - Đăng ký khách hàng", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnRegisterEmployee_Click(object sender, EventArgs e)
        {
            MessageBox.Show("🆕 Chức năng đăng ký nhân viên\n\n" +
                "Trong hệ thống đầy đủ:\n" +
                "✅ Tự động tạo mã NV (NV001, NV002...)\n" +
                "✅ Chọn chức vụ và phòng ban\n" +
                "✅ Quản lý thông tin cá nhân\n" +
                "✅ Phân quyền truy cập hệ thống",
                "Demo - Đăng ký nhân viên", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void OpenAdminDashboard()
        {
            MessageBox.Show("🏢 ADMIN DASHBOARD\n\n" +
                "Chức năng quản lý:\n" +
                "👥 Quản lý khách hàng\n" +
                "👔 Quản lý nhân viên\n" +
                "☕ Quản lý sản phẩm\n" +
                "🛒 Quản lý đơn hàng\n" +
                "🪑 Quản lý bàn và khu vực\n" +
                "📦 Quản lý kho\n" +
                "📊 Báo cáo và thống kê\n\n" +
                "Tất cả đã được implement với giao diện đẹp!",
                "Admin Dashboard", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void OpenCustomerDashboard()
        {
            MessageBox.Show("👤 CUSTOMER DASHBOARD\n\n" +
                "Chức năng cho khách hàng:\n" +
                "📋 Xem menu sản phẩm\n" +
                "🛒 Thêm vào giỏ hàng\n" +
                "💳 Đặt hàng và thanh toán\n" +
                "📜 Xem lịch sử đơn hàng\n" +
                "🪑 Chọn bàn ngồi\n\n" +
                "Giao diện thân thiện và dễ sử dụng!",
                "Customer Dashboard", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
