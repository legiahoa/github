namespace Quanlyquancf
{
    partial class Quanlykho
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
        /// </summary>         private void InitializeComponent()
        {
            this.panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.cboDanhMuc = new Guna.UI2.WinForms.Guna2ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSearch = new Guna.UI2.WinForms.Guna2Button();
            this.textBoxSearch = new Guna.UI2.WinForms.Guna2TextBox();
            this.dgvMon = new Guna.UI2.WinForms.Guna2DataGridView();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelChiTiet = new Guna.UI2.WinForms.Guna2Panel();
            this.lblChiTietTitle = new System.Windows.Forms.Label();
            this.txtTenMon = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblTenMon = new System.Windows.Forms.Label();
            this.txtGia = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblGia = new System.Windows.Forms.Label();
            this.cboDanhMucChiTiet = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblDanhMuc = new System.Windows.Forms.Label();
            this.btnThemMon = new Guna.UI2.WinForms.Guna2Button();
            this.btnSuaMon = new Guna.UI2.WinForms.Guna2Button();
            this.btnXoaMon = new Guna.UI2.WinForms.Guna2Button();
            this.panel1.SuspendLayout();
            this.panelChiTiet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMon)).BeginInit();
            this.SuspendLayout();// 
            // panel1 - Panel tìm kiếm và lọc
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(218)))), ((int)(((byte)(223)))));
            this.panel1.BorderRadius = 15;
            this.panel1.BorderThickness = 1;
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.textBoxSearch);
            this.panel1.Controls.Add(this.comboBoxTrangThai);
            this.panel1.Controls.Add(this.label3);
            this.panel1.FillColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(35, 100);
            this.panel1.Name = "panel1";
            this.panel1.ShadowDecoration.BorderRadius = 15;
            this.panel1.ShadowDecoration.Depth = 10;
            this.panel1.ShadowDecoration.Enabled = true;
            this.panel1.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(0, 0, 5, 5);
            this.panel1.Size = new System.Drawing.Size(1320, 80);
            this.panel1.TabIndex = 26;            // 
            // comboBoxTrangThai
            // 
            this.cboDanhMuc.BackColor = System.Drawing.Color.Transparent;
            this.cboDanhMuc.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(218)))), ((int)(((byte)(223)))));
            this.cboDanhMuc.BorderRadius = 8;
            this.cboDanhMuc.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboDanhMuc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDanhMuc.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.cboDanhMuc.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.cboDanhMuc.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboDanhMuc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboDanhMuc.ItemHeight = 30;
            this.cboDanhMuc.Items.AddRange(new object[] {
            "Tất cả",
            "Thức uống",
            "Món chính",
            "Món phụ",
            "Tráng miệng"});
            this.cboDanhMuc.Location = new System.Drawing.Point(150, 22);
            this.cboDanhMuc.Name = "cboDanhMuc";
            this.cboDanhMuc.Size = new System.Drawing.Size(200, 36);
            this.cboDanhMuc.TabIndex = 23;
            this.cboDanhMuc.SelectedIndex = 0;
            this.cboDanhMuc.SelectedIndexChanged += new System.EventHandler(this.cboDanhMuc_SelectedIndexChanged);
            // 
            // label3
            //            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.label3.Location = new System.Drawing.Point(30, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 23);
            this.label3.TabIndex = 22;
            this.label3.Text = "Danh mục:";
            // 
            // btnSearch
            // 
            this.btnSearch.Animated = true;
            this.btnSearch.BorderRadius = 10;
            this.btnSearch.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSearch.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSearch.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSearch.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnSearch.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));            this.btnSearch.Location = new System.Drawing.Point(1180, 22);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(121, 36);
            this.btnSearch.TabIndex = 33;
            this.btnSearch.Text = "Tìm kiếm";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(218)))), ((int)(((byte)(223)))));
            this.textBoxSearch.BorderRadius = 8;
            this.textBoxSearch.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxSearch.DefaultText = "";
            this.textBoxSearch.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.textBoxSearch.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.textBoxSearch.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.textBoxSearch.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.textBoxSearch.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.textBoxSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.textBoxSearch.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.textBoxSearch.Location = new System.Drawing.Point(650, 22);
            this.textBoxSearch.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.PasswordChar = '\0';
            this.textBoxSearch.PlaceholderText = "Tìm kiếm theo tên món ăn...";            this.textBoxSearch.SelectedText = "";
            this.textBoxSearch.Size = new System.Drawing.Size(495, 36);
            this.textBoxSearch.TabIndex = 32;
            this.textBoxSearch.TextChanged += new System.EventHandler(this.textBoxSearch_TextChanged);            // 
            // dgvMon - Danh sách món ăn
            // 
            this.dgvMon.AllowUserToAddRows = false;
            this.dgvMon.AllowUserToDeleteRows = false;
            this.dgvMon.AllowUserToResizeRows = false;
            this.dgvMon.BackgroundColor = System.Drawing.Color.White;
            this.dgvMon.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvMon.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvMon.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.dgvMon.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.dgvMon.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvMon.ColumnHeadersDefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.dgvMon.ColumnHeadersDefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
            this.dgvMon.ColumnHeadersHeight = 40;
            this.dgvMon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvMon.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.dgvMon.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dgvMon.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvMon.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(241)))), ((int)(((byte)(243)))));
            this.dgvMon.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvMon.EnableHeadersVisualStyles = false;
            this.dgvMon.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvMon.Location = new System.Drawing.Point(35, 200);
            this.dgvMon.Name = "dgvMon";
            this.dgvMon.ReadOnly = true;
            this.dgvMon.RowHeadersVisible = false;
            this.dgvMon.RowHeadersWidth = 62;
            this.dgvMon.RowTemplate.Height = 35;
            this.dgvMon.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMon.Size = new System.Drawing.Size(850, 580);
            this.dgvMon.TabIndex = 31;
            this.dgvMon.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvMon.ThemeStyle.AlternatingRowsStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dgvMon.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvMon.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgvMon.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgvMon.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgvMon.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvMon.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.dgvMon.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvMon.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.dgvMon.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dgvMon.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvMon.ThemeStyle.HeaderStyle.Height = 40;
            this.dgvMon.ThemeStyle.ReadOnly = true;
            this.dgvMon.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvMon.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvMon.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dgvMon.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvMon.ThemeStyle.RowsStyle.Height = 35;
            this.dgvMon.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(241)))), ((int)(((byte)(243)))));
            this.dgvMon.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvMon.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMon_CellClick);// 
            // lblTitle
            //            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.lblTitle.Location = new System.Drawing.Point(35, 35);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(223, 48);
            this.lblTitle.TabIndex = 34;
            this.lblTitle.Text = "Quản Lý Menu";            // 
            // panelChiTiet - Panel thông tin món
            // 
            this.panelChiTiet.BackColor = System.Drawing.Color.Transparent;
            this.panelChiTiet.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(218)))), ((int)(((byte)(223)))));
            this.panelChiTiet.BorderRadius = 15;
            this.panelChiTiet.BorderThickness = 1;
            this.panelChiTiet.Controls.Add(this.btnXoaMon);
            this.panelChiTiet.Controls.Add(this.btnSuaMon);
            this.panelChiTiet.Controls.Add(this.btnThemMon);
            this.panelChiTiet.Controls.Add(this.cboDanhMucChiTiet);
            this.panelChiTiet.Controls.Add(this.lblDanhMuc);
            this.panelChiTiet.Controls.Add(this.txtGia);
            this.panelChiTiet.Controls.Add(this.lblGia);
            this.panelChiTiet.Controls.Add(this.txtTenMon);
            this.panelChiTiet.Controls.Add(this.lblTenMon);
            this.panelChiTiet.Controls.Add(this.lblChiTietTitle);
            this.panelChiTiet.FillColor = System.Drawing.Color.White;
            this.panelChiTiet.Location = new System.Drawing.Point(905, 200);
            this.panelChiTiet.Name = "panelChiTiet";
            this.panelChiTiet.ShadowDecoration.BorderRadius = 15;
            this.panelChiTiet.ShadowDecoration.Depth = 10;
            this.panelChiTiet.ShadowDecoration.Enabled = true;
            this.panelChiTiet.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(0, 0, 5, 5);
            this.panelChiTiet.Size = new System.Drawing.Size(450, 580);
            this.panelChiTiet.TabIndex = 35;
            // 
            // lblChiTietTitle
            // 
            this.lblChiTietTitle.AutoSize = true;
            this.lblChiTietTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChiTietTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.lblChiTietTitle.Location = new System.Drawing.Point(25, 25);
            this.lblChiTietTitle.Name = "lblChiTietTitle";
            this.lblChiTietTitle.Size = new System.Drawing.Size(157, 32);
            this.lblChiTietTitle.TabIndex = 0;
            this.lblChiTietTitle.Text = "Thông Tin Món";
            // 
            // txtTenMon
            // 
            this.txtTenMon.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(218)))), ((int)(((byte)(223)))));
            this.txtTenMon.BorderRadius = 8;
            this.txtTenMon.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTenMon.DefaultText = "";
            this.txtTenMon.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtTenMon.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtTenMon.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTenMon.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTenMon.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.txtTenMon.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTenMon.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.txtTenMon.Location = new System.Drawing.Point(25, 120);
            this.txtTenMon.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtTenMon.Name = "txtTenMon";
            this.txtTenMon.PasswordChar = '\0';
            this.txtTenMon.PlaceholderText = "Nhập tên món ăn...";
            this.txtTenMon.SelectedText = "";
            this.txtTenMon.Size = new System.Drawing.Size(400, 45);
            this.txtTenMon.TabIndex = 1;
            // 
            // lblTenMon
            // 
            this.lblTenMon.AutoSize = true;
            this.lblTenMon.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTenMon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.lblTenMon.Location = new System.Drawing.Point(25, 90);
            this.lblTenMon.Name = "lblTenMon";
            this.lblTenMon.Size = new System.Drawing.Size(86, 23);
            this.lblTenMon.TabIndex = 2;
            this.lblTenMon.Text = "Tên món:";
            // 
            // txtGia
            // 
            this.txtGia.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(218)))), ((int)(((byte)(223)))));
            this.txtGia.BorderRadius = 8;
            this.txtGia.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtGia.DefaultText = "";
            this.txtGia.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtGia.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtGia.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtGia.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtGia.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.txtGia.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtGia.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.txtGia.Location = new System.Drawing.Point(25, 210);
            this.txtGia.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtGia.Name = "txtGia";
            this.txtGia.PasswordChar = '\0';
            this.txtGia.PlaceholderText = "Nhập giá món...";
            this.txtGia.SelectedText = "";
            this.txtGia.Size = new System.Drawing.Size(400, 45);
            this.txtGia.TabIndex = 3;
            this.txtGia.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtGia_KeyPress);
            // 
            // lblGia
            // 
            this.lblGia.AutoSize = true;
            this.lblGia.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGia.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.lblGia.Location = new System.Drawing.Point(25, 180);
            this.lblGia.Name = "lblGia";
            this.lblGia.Size = new System.Drawing.Size(88, 23);
            this.lblGia.TabIndex = 4;
            this.lblGia.Text = "Giá (VNĐ):";
            // 
            // cboDanhMucChiTiet
            // 
            this.cboDanhMucChiTiet.BackColor = System.Drawing.Color.Transparent;
            this.cboDanhMucChiTiet.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(218)))), ((int)(((byte)(223)))));
            this.cboDanhMucChiTiet.BorderRadius = 8;
            this.cboDanhMucChiTiet.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboDanhMucChiTiet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDanhMucChiTiet.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.cboDanhMucChiTiet.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.cboDanhMucChiTiet.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboDanhMucChiTiet.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboDanhMucChiTiet.ItemHeight = 30;
            this.cboDanhMucChiTiet.Items.AddRange(new object[] {
            "Thức uống",
            "Món chính",
            "Món phụ",
            "Tráng miệng"});
            this.cboDanhMucChiTiet.Location = new System.Drawing.Point(25, 300);
            this.cboDanhMucChiTiet.Name = "cboDanhMucChiTiet";
            this.cboDanhMucChiTiet.Size = new System.Drawing.Size(400, 36);
            this.cboDanhMucChiTiet.TabIndex = 5;
            // 
            // lblDanhMuc
            // 
            this.lblDanhMuc.AutoSize = true;
            this.lblDanhMuc.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDanhMuc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.lblDanhMuc.Location = new System.Drawing.Point(25, 270);
            this.lblDanhMuc.Name = "lblDanhMuc";
            this.lblDanhMuc.Size = new System.Drawing.Size(93, 23);
            this.lblDanhMuc.TabIndex = 6;
            this.lblDanhMuc.Text = "Danh mục:";
            // 
            // btnThemMon
            // 
            this.btnThemMon.Animated = true;
            this.btnThemMon.BorderRadius = 10;
            this.btnThemMon.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnThemMon.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnThemMon.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnThemMon.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnThemMon.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.btnThemMon.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnThemMon.ForeColor = System.Drawing.Color.White;
            this.btnThemMon.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(139)))), ((int)(((byte)(77)))));
            this.btnThemMon.Location = new System.Drawing.Point(25, 370);
            this.btnThemMon.Name = "btnThemMon";
            this.btnThemMon.Size = new System.Drawing.Size(120, 45);
            this.btnThemMon.TabIndex = 7;
            this.btnThemMon.Text = "Thêm";
            this.btnThemMon.Click += new System.EventHandler(this.btnThemMon_Click);
            // 
            // btnSuaMon
            // 
            this.btnSuaMon.Animated = true;
            this.btnSuaMon.BorderRadius = 10;
            this.btnSuaMon.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSuaMon.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSuaMon.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSuaMon.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnSuaMon.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnSuaMon.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSuaMon.ForeColor = System.Drawing.Color.White;
            this.btnSuaMon.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnSuaMon.Location = new System.Drawing.Point(165, 370);
            this.btnSuaMon.Name = "btnSuaMon";
            this.btnSuaMon.Size = new System.Drawing.Size(120, 45);
            this.btnSuaMon.TabIndex = 8;
            this.btnSuaMon.Text = "Sửa";
            this.btnSuaMon.Click += new System.EventHandler(this.btnSuaMon_Click);
            // 
            // btnXoaMon
            // 
            this.btnXoaMon.Animated = true;
            this.btnXoaMon.BorderRadius = 10;
            this.btnXoaMon.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnXoaMon.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnXoaMon.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnXoaMon.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnXoaMon.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnXoaMon.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnXoaMon.ForeColor = System.Drawing.Color.White;
            this.btnXoaMon.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(57)))), ((int)(((byte)(43)))));
            this.btnXoaMon.Location = new System.Drawing.Point(305, 370);
            this.btnXoaMon.Name = "btnXoaMon";
            this.btnXoaMon.Size = new System.Drawing.Size(120, 45);
            this.btnXoaMon.TabIndex = 9;
            this.btnXoaMon.Text = "Xóa";
            this.btnXoaMon.Click += new System.EventHandler(this.btnXoaMon_Click);
            this.btnXacNhan.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnXacNhan.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnXacNhan.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnXacNhan.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnXacNhan.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnXacNhan.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnXacNhan.ForeColor = System.Drawing.Color.White;
            this.btnXacNhan.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.btnXacNhan.Location = new System.Drawing.Point(25, 535);
            this.btnXacNhan.Name = "btnXacNhan";            this.btnXacNhan.Size = new System.Drawing.Size(120, 35);
            this.btnXacNhan.TabIndex = 5;
            this.btnXacNhan.Text = "Xác nhận đơn";
            this.btnXacNhan.Click += new System.EventHandler(this.btnXacNhan_Click);
            // 
            // btnHuyDon
            // 
            this.btnHuyDon.Animated = true;
            this.btnHuyDon.BorderRadius = 10;
            this.btnHuyDon.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnHuyDon.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnHuyDon.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnHuyDon.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnHuyDon.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnHuyDon.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnHuyDon.ForeColor = System.Drawing.Color.White;
            this.btnHuyDon.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(57)))), ((int)(((byte)(43)))));
            this.btnHuyDon.Location = new System.Drawing.Point(165, 535);
            this.btnHuyDon.Name = "btnHuyDon";            this.btnHuyDon.Size = new System.Drawing.Size(120, 35);
            this.btnHuyDon.TabIndex = 6;
            this.btnHuyDon.Text = "Hủy đơn";
            this.btnHuyDon.Click += new System.EventHandler(this.btnHuyDon_Click);
            // 
            // btnInHoaDon
            // 
            this.btnInHoaDon.Animated = true;
            this.btnInHoaDon.BorderRadius = 10;
            this.btnInHoaDon.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnInHoaDon.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnInHoaDon.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnInHoaDon.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnInHoaDon.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnInHoaDon.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnInHoaDon.ForeColor = System.Drawing.Color.White;
            this.btnInHoaDon.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnInHoaDon.Location = new System.Drawing.Point(305, 535);
            this.btnInHoaDon.Name = "btnInHoaDon";            this.btnInHoaDon.Size = new System.Drawing.Size(120, 35);
            this.btnInHoaDon.TabIndex = 7;
            this.btnInHoaDon.Text = "In hóa đơn";
            this.btnInHoaDon.Click += new System.EventHandler(this.btnInHoaDon_Click);// 
            // Quanlykho
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(242)))), ((int)(((byte)(245)))));
            this.ClientSize = new System.Drawing.Size(1380, 820);
            this.Controls.Add(this.panelChiTiet);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Quanlykho";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản Lý Đơn Hàng - Quản Lý Quán Cà Phê";
            this.Load += new System.EventHandler(this.Quanlykho_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelChiTiet.ResumeLayout(false);
            this.panelChiTiet.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();            // 
            // Quanlykho
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(242)))), ((int)(((byte)(245)))));
            this.ClientSize = new System.Drawing.Size(1400, 820);
            this.Controls.Add(this.panelChiTiet);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.dgvMon);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Quanlykho";
            this.Text = "Quản Lý Menu";
            this.Load += new System.EventHandler(this.Quanlykho_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelChiTiet.ResumeLayout(false);
            this.panelChiTiet.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel panel1;
        private Guna.UI2.WinForms.Guna2ComboBox cboDanhMuc;
        private System.Windows.Forms.Label label3;
        private Guna.UI2.WinForms.Guna2Button btnSearch;
        private Guna.UI2.WinForms.Guna2TextBox textBoxSearch;
        private Guna.UI2.WinForms.Guna2DataGridView dgvMon;
        private System.Windows.Forms.Label lblTitle;
        private Guna.UI2.WinForms.Guna2Panel panelChiTiet;
        private System.Windows.Forms.Label lblChiTietTitle;
        private Guna.UI2.WinForms.Guna2TextBox txtTenMon;
        private System.Windows.Forms.Label lblTenMon;
        private Guna.UI2.WinForms.Guna2TextBox txtGia;
        private System.Windows.Forms.Label lblGia;
        private Guna.UI2.WinForms.Guna2ComboBox cboDanhMucChiTiet;
        private System.Windows.Forms.Label lblDanhMuc;
        private Guna.UI2.WinForms.Guna2Button btnThemMon;
        private Guna.UI2.WinForms.Guna2Button btnSuaMon;
        private Guna.UI2.WinForms.Guna2Button btnXoaMon;
    }
}