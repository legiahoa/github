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
    public partial class Quanlykhachhang: Form
    {
        public Quanlykhachhang()
        {
            InitializeComponent();
            ShowCustomer();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
        }
        void ShowCustomer()
        {
            string query = "SELECT * FROM KHACHHANG";
            DataProvider provider = new DataProvider();
            dataGridView1.DataSource = provider.ExcuteQuery(query);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
