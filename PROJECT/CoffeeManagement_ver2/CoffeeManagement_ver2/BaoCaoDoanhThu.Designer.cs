namespace CoffeeManagement_ver2
{
    partial class BaoCaoDoanhThu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.btnXemBaoCaoNgay = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.numThang = new System.Windows.Forms.NumericUpDown();
            this.numNam1 = new System.Windows.Forms.NumericUpDown();
            this.btnXemBaoCaoThang = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.numNam2 = new System.Windows.Forms.NumericUpDown();
            this.btnXemBaoCaoNam = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblDoanhThuTrungBinh = new System.Windows.Forms.Label();
            this.lblSoDonHang = new System.Windows.Forms.Label();
            this.lblTongDoanhThu = new System.Windows.Forms.Label();
            this.dataGridViewTopMon = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.btnGuiEmail = new System.Windows.Forms.Button();
            this.btnXuatFile = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numThang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numNam1)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numNam2)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTopMon)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.AutoSize = true;
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(69)))), ((int)(((byte)(19)))));
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(2471, 96);
            this.panel1.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(2377, 22);
            this.btnClose.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(69, 68);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "×";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(861, 26);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(683, 55);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Báo cáo doanh thu năm 2025";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dateTimePicker1);
            this.groupBox1.Controls.Add(this.btnXemBaoCaoNgay);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.groupBox1.Location = new System.Drawing.Point(40, 154);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.groupBox1.Size = new System.Drawing.Size(725, 192);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "📅 Báo cáo theo ngày";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.dateTimePicker1.Location = new System.Drawing.Point(120, 58);
            this.dateTimePicker1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(396, 38);
            this.dateTimePicker1.TabIndex = 2;
            // 
            // btnXemBaoCaoNgay
            // 
            this.btnXemBaoCaoNgay.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnXemBaoCaoNgay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXemBaoCaoNgay.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnXemBaoCaoNgay.ForeColor = System.Drawing.Color.White;
            this.btnXemBaoCaoNgay.Location = new System.Drawing.Point(540, 28);
            this.btnXemBaoCaoNgay.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnXemBaoCaoNgay.Name = "btnXemBaoCaoNgay";
            this.btnXemBaoCaoNgay.Size = new System.Drawing.Size(175, 104);
            this.btnXemBaoCaoNgay.TabIndex = 1;
            this.btnXemBaoCaoNgay.Text = "Xem báo cáo";
            this.btnXemBaoCaoNgay.UseVisualStyleBackColor = false;
            this.btnXemBaoCaoNgay.Click += new System.EventHandler(this.btnXemBaoCaoNgay_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 64);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ngày:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.numThang);
            this.groupBox2.Controls.Add(this.numNam1);
            this.groupBox2.Controls.Add(this.btnXemBaoCaoThang);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.groupBox2.Location = new System.Drawing.Point(848, 154);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.groupBox2.Size = new System.Drawing.Size(700, 192);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "📊 Báo cáo theo tháng";
            // 
            // numThang
            // 
            this.numThang.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.numThang.Location = new System.Drawing.Point(120, 58);
            this.numThang.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.numThang.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.numThang.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numThang.Name = "numThang";
            this.numThang.Size = new System.Drawing.Size(100, 38);
            this.numThang.TabIndex = 4;
            this.numThang.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numNam1
            // 
            this.numNam1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.numNam1.Location = new System.Drawing.Point(340, 58);
            this.numNam1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.numNam1.Maximum = new decimal(new int[] {
            2030,
            0,
            0,
            0});
            this.numNam1.Minimum = new decimal(new int[] {
            2020,
            0,
            0,
            0});
            this.numNam1.Name = "numNam1";
            this.numNam1.Size = new System.Drawing.Size(140, 38);
            this.numNam1.TabIndex = 3;
            this.numNam1.Value = new decimal(new int[] {
            2025,
            0,
            0,
            0});
            // 
            // btnXemBaoCaoThang
            // 
            this.btnXemBaoCaoThang.BackColor = System.Drawing.Color.LimeGreen;
            this.btnXemBaoCaoThang.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXemBaoCaoThang.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnXemBaoCaoThang.ForeColor = System.Drawing.Color.White;
            this.btnXemBaoCaoThang.Location = new System.Drawing.Point(525, 32);
            this.btnXemBaoCaoThang.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnXemBaoCaoThang.Name = "btnXemBaoCaoThang";
            this.btnXemBaoCaoThang.Size = new System.Drawing.Size(175, 99);
            this.btnXemBaoCaoThang.TabIndex = 2;
            this.btnXemBaoCaoThang.Text = "Xem báo cáo";
            this.btnXemBaoCaoThang.UseVisualStyleBackColor = false;
            this.btnXemBaoCaoThang.Click += new System.EventHandler(this.btnXemBaoCaoThang_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(251, 64);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 31);
            this.label3.TabIndex = 1;
            this.label3.Text = "Năm:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 64);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 31);
            this.label2.TabIndex = 0;
            this.label2.Text = "Tháng:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.numNam2);
            this.groupBox3.Controls.Add(this.btnXemBaoCaoNam);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.groupBox3.Location = new System.Drawing.Point(1640, 154);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.groupBox3.Size = new System.Drawing.Size(633, 192);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "📈 Báo cáo theo năm";
            // 
            // numNam2
            // 
            this.numNam2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.numNam2.Location = new System.Drawing.Point(120, 58);
            this.numNam2.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.numNam2.Maximum = new decimal(new int[] {
            2030,
            0,
            0,
            0});
            this.numNam2.Minimum = new decimal(new int[] {
            2020,
            0,
            0,
            0});
            this.numNam2.Name = "numNam2";
            this.numNam2.Size = new System.Drawing.Size(140, 38);
            this.numNam2.TabIndex = 2;
            this.numNam2.Value = new decimal(new int[] {
            2025,
            0,
            0,
            0});
            // 
            // btnXemBaoCaoNam
            // 
            this.btnXemBaoCaoNam.BackColor = System.Drawing.Color.BlueViolet;
            this.btnXemBaoCaoNam.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXemBaoCaoNam.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnXemBaoCaoNam.ForeColor = System.Drawing.Color.White;
            this.btnXemBaoCaoNam.Location = new System.Drawing.Point(435, 32);
            this.btnXemBaoCaoNam.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnXemBaoCaoNam.Name = "btnXemBaoCaoNam";
            this.btnXemBaoCaoNam.Size = new System.Drawing.Size(175, 99);
            this.btnXemBaoCaoNam.TabIndex = 1;
            this.btnXemBaoCaoNam.Text = "Xem báo cáo";
            this.btnXemBaoCaoNam.UseVisualStyleBackColor = false;
            this.btnXemBaoCaoNam.Click += new System.EventHandler(this.btnXemBaoCaoNam_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(29, 64);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 31);
            this.label4.TabIndex = 0;
            this.label4.Text = "Năm:";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.lblDoanhThuTrungBinh);
            this.panel2.Controls.Add(this.lblSoDonHang);
            this.panel2.Controls.Add(this.lblTongDoanhThu);
            this.panel2.Location = new System.Drawing.Point(40, 385);
            this.panel2.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(2233, 152);
            this.panel2.TabIndex = 4;
            // 
            // lblDoanhThuTrungBinh
            // 
            this.lblDoanhThuTrungBinh.AutoSize = true;
            this.lblDoanhThuTrungBinh.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblDoanhThuTrungBinh.ForeColor = System.Drawing.Color.Green;
            this.lblDoanhThuTrungBinh.Location = new System.Drawing.Point(1404, 42);
            this.lblDoanhThuTrungBinh.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblDoanhThuTrungBinh.Name = "lblDoanhThuTrungBinh";
            this.lblDoanhThuTrungBinh.Size = new System.Drawing.Size(460, 37);
            this.lblDoanhThuTrungBinh.TabIndex = 2;
            this.lblDoanhThuTrungBinh.Text = "Doanh thu trung bình: 0 VNĐ";
            // 
            // lblSoDonHang
            // 
            this.lblSoDonHang.AutoSize = true;
            this.lblSoDonHang.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblSoDonHang.ForeColor = System.Drawing.Color.Blue;
            this.lblSoDonHang.Location = new System.Drawing.Point(827, 42);
            this.lblSoDonHang.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblSoDonHang.Name = "lblSoDonHang";
            this.lblSoDonHang.Size = new System.Drawing.Size(251, 37);
            this.lblSoDonHang.TabIndex = 1;
            this.lblSoDonHang.Text = "Số đơn hàng: 0";
            // 
            // lblTongDoanhThu
            // 
            this.lblTongDoanhThu.AutoSize = true;
            this.lblTongDoanhThu.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.lblTongDoanhThu.ForeColor = System.Drawing.Color.Red;
            this.lblTongDoanhThu.Location = new System.Drawing.Point(40, 39);
            this.lblTongDoanhThu.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblTongDoanhThu.Name = "lblTongDoanhThu";
            this.lblTongDoanhThu.Size = new System.Drawing.Size(435, 44);
            this.lblTongDoanhThu.TabIndex = 0;
            this.lblTongDoanhThu.Text = "Tổng doanh thu: 0 VNĐ";
            // 
            // dataGridViewTopMon
            // 
            this.dataGridViewTopMon.AllowUserToAddRows = false;
            this.dataGridViewTopMon.AllowUserToDeleteRows = false;
            this.dataGridViewTopMon.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewTopMon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTopMon.Location = new System.Drawing.Point(40, 615);
            this.dataGridViewTopMon.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.dataGridViewTopMon.Name = "dataGridViewTopMon";
            this.dataGridViewTopMon.ReadOnly = true;
            this.dataGridViewTopMon.RowHeadersWidth = 62;
            this.dataGridViewTopMon.Size = new System.Drawing.Size(2233, 481);
            this.dataGridViewTopMon.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(40, 568);
            this.label5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(396, 37);
            this.label5.TabIndex = 6;
            this.label5.Text = "🍴 Top món ăn bán chạy";
            // 
            // btnGuiEmail
            // 
            this.btnGuiEmail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnGuiEmail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuiEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.btnGuiEmail.ForeColor = System.Drawing.Color.White;
            this.btnGuiEmail.Location = new System.Drawing.Point(1693, 1129);
            this.btnGuiEmail.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnGuiEmail.Name = "btnGuiEmail";
            this.btnGuiEmail.Size = new System.Drawing.Size(260, 78);
            this.btnGuiEmail.TabIndex = 7;
            this.btnGuiEmail.Text = "📧 Gửi Email";
            this.btnGuiEmail.UseVisualStyleBackColor = false;
            this.btnGuiEmail.Click += new System.EventHandler(this.btnGuiEmail_Click);
            // 
            // btnXuatFile
            // 
            this.btnXuatFile.BackColor = System.Drawing.Color.LimeGreen;
            this.btnXuatFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXuatFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.btnXuatFile.ForeColor = System.Drawing.Color.White;
            this.btnXuatFile.Location = new System.Drawing.Point(2013, 1129);
            this.btnXuatFile.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnXuatFile.Name = "btnXuatFile";
            this.btnXuatFile.Size = new System.Drawing.Size(260, 78);
            this.btnXuatFile.TabIndex = 8;
            this.btnXuatFile.Text = "📄 Xuất File";
            this.btnXuatFile.UseVisualStyleBackColor = false;
            this.btnXuatFile.Click += new System.EventHandler(this.btnXuatFile_Click);
            // 
            // BaoCaoDoanhThu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(2471, 1250);
            this.Controls.Add(this.btnXuatFile);
            this.Controls.Add(this.btnGuiEmail);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dataGridViewTopMon);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.Name = "BaoCaoDoanhThu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Báo cáo doanh thu";
            this.Load += new System.EventHandler(this.BaoCaoDoanhThu_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numThang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numNam1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numNam2)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTopMon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button btnXemBaoCaoNgay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown numThang;
        private System.Windows.Forms.NumericUpDown numNam1;
        private System.Windows.Forms.Button btnXemBaoCaoThang;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.NumericUpDown numNam2;
        private System.Windows.Forms.Button btnXemBaoCaoNam;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblDoanhThuTrungBinh;
        private System.Windows.Forms.Label lblSoDonHang;
        private System.Windows.Forms.Label lblTongDoanhThu;
        private System.Windows.Forms.DataGridView dataGridViewTopMon;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnGuiEmail;
        private System.Windows.Forms.Button btnXuatFile;
    }
}
