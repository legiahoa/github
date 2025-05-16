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
    public partial class Hethongquanly: Form
    {
        public Hethongquanly()
        {
            InitializeComponent();
            ShowFoodType();
            ShowFood();
        }

        private void button1_Click(object sender, EventArgs e)
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

        private void quảnLýKháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Quanlykhachhang f = new Quanlykhachhang();
            f.ShowDialog();
        }

        private void quảnLýNhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Quanlynhanvien f = new Quanlynhanvien();
            f.ShowDialog();
        }

        private void quảnLýBànToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Quanlyban f = new Quanlyban();
            f.ShowDialog();
        }

        private void quảnLýKhuVựcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Quanlykhuvuc f = new Quanlykhuvuc();
            f.ShowDialog();
        }

        private void quảnLýKhoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Quanlykho f = new Quanlykho();
            f.ShowDialog();
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

        private void Hethongquanly_Load(object sender, EventArgs e)
        {

        }
    }
}
