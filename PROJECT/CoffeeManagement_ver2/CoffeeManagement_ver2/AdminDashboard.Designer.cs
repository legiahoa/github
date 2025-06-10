namespace CoffeeManagement_ver2
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
            this.lblWelcome = new System.Windows.Forms.Label();
            this.btnLogout = new Guna.UI2.WinForms.Guna2Button();
            this.btnDonhang = new Guna.UI2.WinForms.Guna2Button();
            this.btnThucdon = new Guna.UI2.WinForms.Guna2Button();
            this.btnBan = new Guna.UI2.WinForms.Guna2Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblWelcome
            // 
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI", 32F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblWelcome.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblWelcome.Location = new System.Drawing.Point(301, 34);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(651, 151);
            this.lblWelcome.TabIndex = 1;
            this.lblWelcome.Text = "Xin chào, Admin!";
            this.lblWelcome.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnLogout
            // 
            this.btnLogout.BorderRadius = 20;
            this.btnLogout.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnLogout.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnLogout.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnLogout.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnLogout.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLogout.ForeColor = System.Drawing.Color.White;
            this.btnLogout.Location = new System.Drawing.Point(898, 12);
            this.btnLogout.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(236, 56);
            this.btnLogout.TabIndex = 10;
            this.btnLogout.Text = "Đăng xuất";
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnDonhang
            // 
            this.btnDonhang.BorderRadius = 35;
            this.btnDonhang.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnDonhang.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnDonhang.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnDonhang.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnDonhang.FillColor = System.Drawing.Color.Firebrick;
            this.btnDonhang.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnDonhang.ForeColor = System.Drawing.Color.White;
            this.btnDonhang.Location = new System.Drawing.Point(340, 252);
            this.btnDonhang.Name = "btnDonhang";
            this.btnDonhang.Size = new System.Drawing.Size(455, 152);
            this.btnDonhang.TabIndex = 11;
            this.btnDonhang.Text = "Đơn hàng";
            this.btnDonhang.Click += new System.EventHandler(this.btnDonhang_Click);
            // 
            // btnThucdon
            // 
            this.btnThucdon.BorderRadius = 35;
            this.btnThucdon.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnThucdon.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnThucdon.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnThucdon.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnThucdon.FillColor = System.Drawing.Color.DarkOrange;
            this.btnThucdon.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnThucdon.ForeColor = System.Drawing.Color.White;
            this.btnThucdon.Location = new System.Drawing.Point(340, 452);
            this.btnThucdon.Name = "btnThucdon";
            this.btnThucdon.Size = new System.Drawing.Size(455, 152);
            this.btnThucdon.TabIndex = 12;
            this.btnThucdon.Text = "Thực đơn";
            this.btnThucdon.Click += new System.EventHandler(this.btnThucdon_Click);
            // 
            // btnBan
            // 
            this.btnBan.BorderRadius = 35;
            this.btnBan.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnBan.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnBan.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnBan.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnBan.FillColor = System.Drawing.Color.LimeGreen;
            this.btnBan.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnBan.ForeColor = System.Drawing.Color.White;
            this.btnBan.Location = new System.Drawing.Point(340, 652);
            this.btnBan.Name = "btnBan";
            this.btnBan.Size = new System.Drawing.Size(455, 152);
            this.btnBan.TabIndex = 13;
            this.btnBan.Text = "Bàn";
            this.btnBan.Click += new System.EventHandler(this.btnBan_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Image = global::CoffeeManagement_ver2.Properties.Resources.vicf;
            this.pictureBox1.Location = new System.Drawing.Point(33, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(225, 223);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // AdminDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.RosyBrown;
            this.ClientSize = new System.Drawing.Size(1146, 872);
            this.Controls.Add(this.btnBan);
            this.Controls.Add(this.btnThucdon);
            this.Controls.Add(this.btnDonhang);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblWelcome);
            this.Name = "AdminDashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AdminDashboard";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblWelcome;
        private Guna.UI2.WinForms.Guna2Button btnLogout;
        private Guna.UI2.WinForms.Guna2Button btnDonhang;
        private Guna.UI2.WinForms.Guna2Button btnThucdon;
        private Guna.UI2.WinForms.Guna2Button btnBan;
    }
}