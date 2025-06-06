using System;
using System.Windows.Forms;

namespace CoffeeManagement
{
    public partial class SimpleLoginForm : Form
    {
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnLogin;
        private Label lblTitle;

        public SimpleLoginForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.txtUsername = new TextBox();
            this.txtPassword = new TextBox();
            this.btnLogin = new Button();
            this.lblTitle = new Label();
            this.SuspendLayout();

            // lblTitle
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(50, 30);
            this.lblTitle.Size = new System.Drawing.Size(300, 40);
            this.lblTitle.Text = "COFFEE MANAGEMENT SYSTEM";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // txtUsername
            this.txtUsername.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtUsername.Location = new System.Drawing.Point(50, 100);
            this.txtUsername.Size = new System.Drawing.Size(300, 30);
            this.txtUsername.Text = "admin"; // Default for demo

            // txtPassword
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtPassword.Location = new System.Drawing.Point(50, 150);
            this.txtPassword.Size = new System.Drawing.Size(300, 30);
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Text = "123456"; // Default for demo

            // btnLogin
            this.btnLogin.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnLogin.Location = new System.Drawing.Point(50, 200);
            this.btnLogin.Size = new System.Drawing.Size(300, 40);
            this.btnLogin.Text = "ĐĂNG NHẬP";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new EventHandler(this.btnLogin_Click);

            // SimpleLoginForm
            this.ClientSize = new System.Drawing.Size(400, 300);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Coffee Management - Login";
            this.ResumeLayout(false);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"Đăng nhập thành công!\nUsername: {txtUsername.Text}\n\nDemo Coffee Management System hoạt động bình thường.\n\nĐể chạy đầy đủ, hãy cài đặt GunaUI2 package trong Visual Studio.", 
                "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    // Simple Program class for demo
    internal static class SimpleDemoProgram
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SimpleLoginForm());
        }
    }
}
