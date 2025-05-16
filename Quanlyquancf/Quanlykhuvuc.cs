using Quanlyquancf.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Quanlyquancf
{
    public partial class Quanlykhuvuc: Form
    {
        public Quanlykhuvuc()
        {
            InitializeComponent();
            ShowArea();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
          
        }
        void ShowArea()
        {
            string query = "SELECT * FROM KHUVUC";
            DataProvider provider = new DataProvider();
            dataGridView1.DataSource = provider.ExcuteQuery(query);
        }
    }
}
