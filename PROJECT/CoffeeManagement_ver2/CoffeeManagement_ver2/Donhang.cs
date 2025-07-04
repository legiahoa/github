using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoffeeManagement_ver2
{
    public partial class Donhang : Form
    {
        private TcpListener listener;
        public Donhang()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Donhang_Load(object sender, EventArgs e)
        {
            listener = new TcpListener(IPAddress.Any, 8080);
            listener.Start();

            Task.Run(() =>
            {
                while (true)
                {
                    TcpClient client = listener.AcceptTcpClient();
                    NetworkStream stream = client.GetStream();

                    byte[] buffer = new byte[4096];
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    string json = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                    DonHangModel donHang = JsonConvert.DeserializeObject<DonHangModel>(json);

                    // Hiển thị ra giao diện hoặc xử lý đơn
                    Invoke(new Action(() =>
                    {
                        MessageBox.Show($"Đơn hàng mới từ {donHang.Ban} - {donHang.TongTien:N0} VNĐ");
                        // TODO: thêm đơn hàng vào DataGridView hoặc Firebase tại đây
                    }));

                    stream.Close();
                    client.Close();
                }
            });
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
