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
    public partial class Ban : Form
    {
        public Ban()
        {
            InitializeComponent();
        }

        private void cboTrangThai_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cboTrangThai_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnXoaBan_Click(object sender, EventArgs e)
        {
            txtTenBan.Clear();
            txtMaKhuVuc.Clear();
            cboTrangThai.SelectedIndex = -1;
        }
    }
}
