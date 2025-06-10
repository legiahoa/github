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
    public partial class Thucdon : Form
    {
        public Thucdon()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnXoaMon_Click(object sender, EventArgs e)
        {
            txtTenMon.Clear();
            txtGia.Clear();
            cboDanhmuc.SelectedIndex = -1;
        }
    }
}
