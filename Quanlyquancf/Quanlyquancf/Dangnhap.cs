using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quanlyquancf
{
    public partial class Dangnhap: Form
    {
        public Dangnhap()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Hethongquanly manager = new Hethongquanly();
            this.Hide();
            manager.ShowDialog();
            this.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Dangnhap_Load(object sender, EventArgs e)
        {

        }

        private void Dangnhap_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có thật sự muốn thoát?", "Xác nhận", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                e.Cancel = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Khachhang f = new Khachhang();
            this.Hide();
            f.ShowDialog();
            this.Show();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dangki f = new Dangki();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }
    }
}
