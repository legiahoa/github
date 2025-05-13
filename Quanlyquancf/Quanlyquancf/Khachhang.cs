using Quanlyquancf.DAO;
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
    public partial class Khachhang: Form
    {
        public Khachhang()
        {
            InitializeComponent();
            ShowFoodType();
            ShowFood();
        }

        private void Khachhang_Load(object sender, EventArgs e)
        {

        }

        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AccountProfile f = new AccountProfile();
            f.ShowDialog();
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        void ShowFoodType()
        {
            string query = "SELECT TENDM FROM DANHMUCSP";
            DataProvider provider = new DataProvider();
            DataTable data = provider.ExcuteQuery(query);

            comboBox1.DataSource = data;
            comboBox1.DisplayMember = "TENDM"; // ← dòng này là quan trọng
        }
        void ShowFood()
        {
            string query = "SELECT TENSP FROM SANPHAM";
            DataProvider provider = new DataProvider();
            DataTable data = provider.ExcuteQuery(query);

            comboBox2.DataSource = data;
            comboBox2.DisplayMember = "TENSP"; // ← dòng này là quan trọng
        }
    }
}
