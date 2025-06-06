namespace CoffeeManagement.Forms
{
    partial class AdminDashboard
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
            this.pnlSidebar = new Guna.UI2.WinForms.Guna2Panel();
            this.btnLogout = new Guna.UI2.WinForms.Guna2Button();
            this.btnReports = new Guna.UI2.WinForms.Guna2Button();
            this.btnInventoryManagement = new Guna.UI2.WinForms.Guna2Button();
            this.btnAreaManagement = new Guna.UI2.WinForms.Guna2Button();
            this.btnTableManagement = new Guna.UI2.WinForms.Guna2Button();
            this.btnOrderManagement = new Guna.UI2.WinForms.Guna2Button();
            this.btnProductManagement = new Guna.UI2.WinForms.Guna2Button();
            this.btnEmployeeManagement = new Guna.UI2.WinForms.Guna2Button();
            this.btnCustomerManagement = new Guna.UI2.WinForms.Guna2Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlMain = new Guna.UI2.WinForms.Guna2Panel();
            this.pnlStats = new Guna.UI2.WinForms.Guna2Panel();
            this.pnlOrderStats = new Guna.UI2.WinForms.Guna2Panel();
            this.lblOrderCount = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.pnlProductStats = new Guna.UI2.WinForms.Guna2Panel();
            this.lblProductCount = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.pnlEmployeeStats = new Guna.UI2.WinForms.Guna2Panel();
            this.lblEmployeeCount = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pnlCustomerStats = new Guna.UI2.WinForms.Guna2Panel();
            this.lblCustomerCount = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.pnlSidebar.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnlStats.SuspendLayout();
            this.pnlOrderStats.SuspendLayout();
            this.pnlProductStats.SuspendLayout();
            this.pnlEmployeeStats.SuspendLayout();
            this.pnlCustomerStats.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSidebar
            // 
            this.pnlSidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(85)))), ((int)(((byte)(149)))));
            this.pnlSidebar.Controls.Add(this.btnLogout);
            this.pnlSidebar.Controls.Add(this.btnReports);
            this.pnlSidebar.Controls.Add(this.btnInventoryManagement);
            this.pnlSidebar.Controls.Add(this.btnAreaManagement);
            this.pnlSidebar.Controls.Add(this.btnTableManagement);
            this.pnlSidebar.Controls.Add(this.btnOrderManagement);
            this.pnlSidebar.Controls.Add(this.btnProductManagement);
            this.pnlSidebar.Controls.Add(this.btnEmployeeManagement);
            this.pnlSidebar.Controls.Add(this.btnCustomerManagement);
            this.pnlSidebar.Controls.Add(this.lblTitle);
            this.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSidebar.Location = new System.Drawing.Point(0, 0);
            this.pnlSidebar.Name = "pnlSidebar";
            this.pnlSidebar.Size = new System.Drawing.Size(280, 700);
            this.pnlSidebar.TabIndex = 0;
            // 
            // btnLogout
            // 
            this.btnLogout.BorderRadius = 10;
            this.btnLogout.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnLogout.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnLogout.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnLogout.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnLogout.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLogout.ForeColor = System.Drawing.Color.White;
            this.btnLogout.Location = new System.Drawing.Point(20, 620);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(240, 45);
            this.btnLogout.TabIndex = 9;
            this.btnLogout.Text = "Đăng xuất";
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnReports
            // 
            this.btnReports.BorderRadius = 10;
            this.btnReports.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnReports.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnReports.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnReports.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnReports.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(105)))), ((int)(((byte)(169)))));
            this.btnReports.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnReports.ForeColor = System.Drawing.Color.White;
            this.btnReports.Location = new System.Drawing.Point(20, 550);
            this.btnReports.Name = "btnReports";
            this.btnReports.Size = new System.Drawing.Size(240, 45);
            this.btnReports.TabIndex = 8;
            this.btnReports.Text = "Báo cáo";
            this.btnReports.Click += new System.EventHandler(this.btnReports_Click);
            // 
            // btnInventoryManagement
            // 
            this.btnInventoryManagement.BorderRadius = 10;
            this.btnInventoryManagement.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnInventoryManagement.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnInventoryManagement.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnInventoryManagement.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnInventoryManagement.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(105)))), ((int)(((byte)(169)))));
            this.btnInventoryManagement.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnInventoryManagement.ForeColor = System.Drawing.Color.White;
            this.btnInventoryManagement.Location = new System.Drawing.Point(20, 490);
            this.btnInventoryManagement.Name = "btnInventoryManagement";
            this.btnInventoryManagement.Size = new System.Drawing.Size(240, 45);
            this.btnInventoryManagement.TabIndex = 7;
            this.btnInventoryManagement.Text = "Quản lý kho";
            this.btnInventoryManagement.Click += new System.EventHandler(this.btnInventoryManagement_Click);
            // 
            // btnAreaManagement
            // 
            this.btnAreaManagement.BorderRadius = 10;
            this.btnAreaManagement.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnAreaManagement.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnAreaManagement.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnAreaManagement.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnAreaManagement.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(105)))), ((int)(((byte)(169)))));
            this.btnAreaManagement.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnAreaManagement.ForeColor = System.Drawing.Color.White;
            this.btnAreaManagement.Location = new System.Drawing.Point(20, 430);
            this.btnAreaManagement.Name = "btnAreaManagement";
            this.btnAreaManagement.Size = new System.Drawing.Size(240, 45);
            this.btnAreaManagement.TabIndex = 6;
            this.btnAreaManagement.Text = "Quản lý khu vực";
            this.btnAreaManagement.Click += new System.EventHandler(this.btnAreaManagement_Click);
            // 
            // btnTableManagement
            // 
            this.btnTableManagement.BorderRadius = 10;
            this.btnTableManagement.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnTableManagement.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnTableManagement.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnTableManagement.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnTableManagement.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(105)))), ((int)(((byte)(169)))));
            this.btnTableManagement.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnTableManagement.ForeColor = System.Drawing.Color.White;
            this.btnTableManagement.Location = new System.Drawing.Point(20, 370);
            this.btnTableManagement.Name = "btnTableManagement";
            this.btnTableManagement.Size = new System.Drawing.Size(240, 45);
            this.btnTableManagement.TabIndex = 5;
            this.btnTableManagement.Text = "Quản lý bàn";
            this.btnTableManagement.Click += new System.EventHandler(this.btnTableManagement_Click);
            // 
            // btnOrderManagement
            // 
            this.btnOrderManagement.BorderRadius = 10;
            this.btnOrderManagement.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnOrderManagement.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnOrderManagement.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnOrderManagement.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnOrderManagement.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(105)))), ((int)(((byte)(169)))));
            this.btnOrderManagement.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnOrderManagement.ForeColor = System.Drawing.Color.White;
            this.btnOrderManagement.Location = new System.Drawing.Point(20, 310);
            this.btnOrderManagement.Name = "btnOrderManagement";
            this.btnOrderManagement.Size = new System.Drawing.Size(240, 45);
            this.btnOrderManagement.TabIndex = 4;
            this.btnOrderManagement.Text = "Quản lý đơn hàng";
            this.btnOrderManagement.Click += new System.EventHandler(this.btnOrderManagement_Click);
            // 
            // btnProductManagement
            // 
            this.btnProductManagement.BorderRadius = 10;
            this.btnProductManagement.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnProductManagement.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnProductManagement.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnProductManagement.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnProductManagement.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(105)))), ((int)(((byte)(169)))));
            this.btnProductManagement.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnProductManagement.ForeColor = System.Drawing.Color.White;
            this.btnProductManagement.Location = new System.Drawing.Point(20, 250);
            this.btnProductManagement.Name = "btnProductManagement";
            this.btnProductManagement.Size = new System.Drawing.Size(240, 45);
            this.btnProductManagement.TabIndex = 3;
            this.btnProductManagement.Text = "Quản lý sản phẩm";
            this.btnProductManagement.Click += new System.EventHandler(this.btnProductManagement_Click);
            // 
            // btnEmployeeManagement
            // 
            this.btnEmployeeManagement.BorderRadius = 10;
            this.btnEmployeeManagement.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnEmployeeManagement.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnEmployeeManagement.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnEmployeeManagement.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnEmployeeManagement.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(105)))), ((int)(((byte)(169)))));
            this.btnEmployeeManagement.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnEmployeeManagement.ForeColor = System.Drawing.Color.White;
            this.btnEmployeeManagement.Location = new System.Drawing.Point(20, 190);
            this.btnEmployeeManagement.Name = "btnEmployeeManagement";
            this.btnEmployeeManagement.Size = new System.Drawing.Size(240, 45);
            this.btnEmployeeManagement.TabIndex = 2;
            this.btnEmployeeManagement.Text = "Quản lý nhân viên";
            this.btnEmployeeManagement.Click += new System.EventHandler(this.btnEmployeeManagement_Click);
            // 
            // btnCustomerManagement
            // 
            this.btnCustomerManagement.BorderRadius = 10;
            this.btnCustomerManagement.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnCustomerManagement.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnCustomerManagement.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnCustomerManagement.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnCustomerManagement.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(105)))), ((int)(((byte)(169)))));
            this.btnCustomerManagement.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCustomerManagement.ForeColor = System.Drawing.Color.White;
            this.btnCustomerManagement.Location = new System.Drawing.Point(20, 130);
            this.btnCustomerManagement.Name = "btnCustomerManagement";
            this.btnCustomerManagement.Size = new System.Drawing.Size(240, 45);
            this.btnCustomerManagement.TabIndex = 1;
            this.btnCustomerManagement.Text = "Quản lý khách hàng";
            this.btnCustomerManagement.Click += new System.EventHandler(this.btnCustomerManagement_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 30);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(240, 60);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "QUẢN LÝ QUÁN CAFÉ";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.White;
            this.pnlMain.Controls.Add(this.pnlStats);
            this.pnlMain.Controls.Add(this.lblWelcome);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(280, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(30);
            this.pnlMain.Size = new System.Drawing.Size(920, 700);
            this.pnlMain.TabIndex = 1;
            // 
            // pnlStats
            // 
            this.pnlStats.Controls.Add(this.pnlOrderStats);
            this.pnlStats.Controls.Add(this.pnlProductStats);
            this.pnlStats.Controls.Add(this.pnlEmployeeStats);
            this.pnlStats.Controls.Add(this.pnlCustomerStats);
            this.pnlStats.Location = new System.Drawing.Point(30, 120);
            this.pnlStats.Name = "pnlStats";
            this.pnlStats.Size = new System.Drawing.Size(860, 550);
            this.pnlStats.TabIndex = 1;
            // 
            // pnlOrderStats
            // 
            this.pnlOrderStats.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(196)))), ((int)(((byte)(15)))));
            this.pnlOrderStats.BorderRadius = 15;
            this.pnlOrderStats.Controls.Add(this.lblOrderCount);
            this.pnlOrderStats.Controls.Add(this.label8);
            this.pnlOrderStats.Location = new System.Drawing.Point(440, 280);
            this.pnlOrderStats.Name = "pnlOrderStats";
            this.pnlOrderStats.Size = new System.Drawing.Size(400, 150);
            this.pnlOrderStats.TabIndex = 3;
            // 
            // lblOrderCount
            // 
            this.lblOrderCount.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblOrderCount.ForeColor = System.Drawing.Color.White;
            this.lblOrderCount.Location = new System.Drawing.Point(20, 70);
            this.lblOrderCount.Name = "lblOrderCount";
            this.lblOrderCount.Size = new System.Drawing.Size(360, 50);
            this.lblOrderCount.TabIndex = 1;
            this.lblOrderCount.Text = "0";
            this.lblOrderCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(20, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(360, 30);
            this.label8.TabIndex = 0;
            this.label8.Text = "TỔNG ĐƠN HÀNG";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlProductStats
            // 
            this.pnlProductStats.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(68)))), ((int)(((byte)(173)))));
            this.pnlProductStats.BorderRadius = 15;
            this.pnlProductStats.Controls.Add(this.lblProductCount);
            this.pnlProductStats.Controls.Add(this.label6);
            this.pnlProductStats.Location = new System.Drawing.Point(20, 280);
            this.pnlProductStats.Name = "pnlProductStats";
            this.pnlProductStats.Size = new System.Drawing.Size(400, 150);
            this.pnlProductStats.TabIndex = 2;
            // 
            // lblProductCount
            // 
            this.lblProductCount.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblProductCount.ForeColor = System.Drawing.Color.White;
            this.lblProductCount.Location = new System.Drawing.Point(20, 70);
            this.lblProductCount.Name = "lblProductCount";
            this.lblProductCount.Size = new System.Drawing.Size(360, 50);
            this.lblProductCount.TabIndex = 1;
            this.lblProductCount.Text = "0";
            this.lblProductCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(20, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(360, 30);
            this.label6.TabIndex = 0;
            this.label6.Text = "TỔNG SẢN PHẨM";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlEmployeeStats
            // 
            this.pnlEmployeeStats.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(126)))), ((int)(((byte)(34)))));
            this.pnlEmployeeStats.BorderRadius = 15;
            this.pnlEmployeeStats.Controls.Add(this.lblEmployeeCount);
            this.pnlEmployeeStats.Controls.Add(this.label4);
            this.pnlEmployeeStats.Location = new System.Drawing.Point(440, 20);
            this.pnlEmployeeStats.Name = "pnlEmployeeStats";
            this.pnlEmployeeStats.Size = new System.Drawing.Size(400, 150);
            this.pnlEmployeeStats.TabIndex = 1;
            // 
            // lblEmployeeCount
            // 
            this.lblEmployeeCount.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblEmployeeCount.ForeColor = System.Drawing.Color.White;
            this.lblEmployeeCount.Location = new System.Drawing.Point(20, 70);
            this.lblEmployeeCount.Name = "lblEmployeeCount";
            this.lblEmployeeCount.Size = new System.Drawing.Size(360, 50);
            this.lblEmployeeCount.TabIndex = 1;
            this.lblEmployeeCount.Text = "0";
            this.lblEmployeeCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(20, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(360, 30);
            this.label4.TabIndex = 0;
            this.label4.Text = "TỔNG NHÂN VIÊN";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlCustomerStats
            // 
            this.pnlCustomerStats.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.pnlCustomerStats.BorderRadius = 15;
            this.pnlCustomerStats.Controls.Add(this.lblCustomerCount);
            this.pnlCustomerStats.Controls.Add(this.label1);
            this.pnlCustomerStats.Location = new System.Drawing.Point(20, 20);
            this.pnlCustomerStats.Name = "pnlCustomerStats";
            this.pnlCustomerStats.Size = new System.Drawing.Size(400, 150);
            this.pnlCustomerStats.TabIndex = 0;
            // 
            // lblCustomerCount
            // 
            this.lblCustomerCount.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblCustomerCount.ForeColor = System.Drawing.Color.White;
            this.lblCustomerCount.Location = new System.Drawing.Point(20, 70);
            this.lblCustomerCount.Name = "lblCustomerCount";
            this.lblCustomerCount.Size = new System.Drawing.Size(360, 50);
            this.lblCustomerCount.TabIndex = 1;
            this.lblCustomerCount.Text = "0";
            this.lblCustomerCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(20, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(360, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "TỔNG KHÁCH HÀNG";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblWelcome
            // 
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblWelcome.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(85)))), ((int)(((byte)(149)))));
            this.lblWelcome.Location = new System.Drawing.Point(30, 30);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(860, 60);
            this.lblWelcome.TabIndex = 0;
            this.lblWelcome.Text = "Xin chào, Admin!";
            this.lblWelcome.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // AdminDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlSidebar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "AdminDashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý Quán Café - Dashboard";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AdminDashboard_FormClosing);
            this.Load += new System.EventHandler(this.AdminDashboard_Load);
            this.pnlSidebar.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            this.pnlStats.ResumeLayout(false);
            this.pnlOrderStats.ResumeLayout(false);
            this.pnlProductStats.ResumeLayout(false);
            this.pnlEmployeeStats.ResumeLayout(false);
            this.pnlCustomerStats.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnlSidebar;
        private Guna.UI2.WinForms.Guna2Button btnLogout;
        private Guna.UI2.WinForms.Guna2Button btnReports;
        private Guna.UI2.WinForms.Guna2Button btnInventoryManagement;
        private Guna.UI2.WinForms.Guna2Button btnAreaManagement;
        private Guna.UI2.WinForms.Guna2Button btnTableManagement;
        private Guna.UI2.WinForms.Guna2Button btnOrderManagement;
        private Guna.UI2.WinForms.Guna2Button btnProductManagement;
        private Guna.UI2.WinForms.Guna2Button btnEmployeeManagement;
        private Guna.UI2.WinForms.Guna2Button btnCustomerManagement;
        private System.Windows.Forms.Label lblTitle;
        private Guna.UI2.WinForms.Guna2Panel pnlMain;
        private Guna.UI2.WinForms.Guna2Panel pnlStats;
        private Guna.UI2.WinForms.Guna2Panel pnlOrderStats;
        private System.Windows.Forms.Label lblOrderCount;
        private System.Windows.Forms.Label label8;
        private Guna.UI2.WinForms.Guna2Panel pnlProductStats;
        private System.Windows.Forms.Label lblProductCount;
        private System.Windows.Forms.Label label6;
        private Guna.UI2.WinForms.Guna2Panel pnlEmployeeStats;
        private System.Windows.Forms.Label lblEmployeeCount;
        private System.Windows.Forms.Label label4;
        private Guna.UI2.WinForms.Guna2Panel pnlCustomerStats;
        private System.Windows.Forms.Label lblCustomerCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblWelcome;
    }
}
