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
    public partial class EmailForm : Form
    {
        public string EmailNguoiNhan { get; private set; }
        public string EmailNguoiGui { get; private set; }
        public string MatKhauEmail { get; private set; }

        public EmailForm()
        {
            InitializeComponent();
        }

        private void btnGui_Click(object sender, EventArgs e)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(txtEmailNguoiNhan.Text))
            {
                MessageBox.Show("Vui lòng nhập email người nhận!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmailNguoiNhan.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtEmailNguoiGui.Text))
            {
                MessageBox.Show("Vui lòng nhập email người gửi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmailNguoiGui.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtMatKhau.Text))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu ứng dụng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhau.Focus();
                return;
            }

            // Validate email format
            if (!IsValidEmail(txtEmailNguoiNhan.Text))
            {
                MessageBox.Show("Email người nhận không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmailNguoiNhan.Focus();
                return;
            }

            if (!IsValidEmail(txtEmailNguoiGui.Text))
            {
                MessageBox.Show("Email người gửi không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmailNguoiGui.Focus();
                return;
            }

            // Set properties
            EmailNguoiNhan = txtEmailNguoiNhan.Text.Trim();
            EmailNguoiGui = txtEmailNguoiGui.Text.Trim();
            MatKhauEmail = txtMatKhau.Text.Trim();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
