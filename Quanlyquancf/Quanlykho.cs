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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Quanlyquancf
{
    public partial class Quanlykho: Form
    {
        public Quanlykho()
        {
            InitializeComponent();
            ShowMaterial();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        void ShowMaterial()
        {
            string query = "select* from NGUYENLIEU";
            DataProvider provider = new DataProvider();
            dataGridView1.DataSource = provider.ExcuteQuery(query);
        }
    }
}
