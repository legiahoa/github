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
    public partial class AdminDashboard : Form
    {
        public AdminDashboard()
        {
            InitializeComponent();
        }

        private void btnDonhang_Click(object sender, EventArgs e)
        {
            this.Hide();
            Donhang donhangForm = new Donhang();
            donhangForm.Show();
            this.Show();
        }

        private void btnThucdon_Click(object sender, EventArgs e)
        {
            this.Hide();
            Thucdon thucdonForm = new Thucdon();    
            thucdonForm.Show();   
            this.Show();
        }

        private void btnBan_Click(object sender, EventArgs e)
        {
            this.Hide();    
            Ban banForm = new Ban();
            banForm.Show();
            this.Show();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn thật sự muốn đăng xuất?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
