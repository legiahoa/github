namespace CoffeeManagement_ver2
{
    partial class EmailForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtEmailNguoiNhan = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtEmailNguoiGui = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMatKhau = new System.Windows.Forms.TextBox();
            this.btnGui = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(69)))), ((int)(((byte)(19)))));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(450, 60);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(160, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "📧 Gửi Email";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label2.Location = new System.Drawing.Point(30, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Email người nhận:";
            // 
            // txtEmailNguoiNhan
            // 
            this.txtEmailNguoiNhan.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtEmailNguoiNhan.Location = new System.Drawing.Point(30, 100);
            this.txtEmailNguoiNhan.Name = "txtEmailNguoiNhan";
            this.txtEmailNguoiNhan.Size = new System.Drawing.Size(390, 23);
            this.txtEmailNguoiNhan.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label3.Location = new System.Drawing.Point(30, 140);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "Email người gửi:";
            // 
            // txtEmailNguoiGui
            // 
            this.txtEmailNguoiGui.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtEmailNguoiGui.Location = new System.Drawing.Point(30, 160);
            this.txtEmailNguoiGui.Name = "txtEmailNguoiGui";
            this.txtEmailNguoiGui.Size = new System.Drawing.Size(390, 23);
            this.txtEmailNguoiGui.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label4.Location = new System.Drawing.Point(30, 200);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(136, 17);
            this.label4.TabIndex = 5;
            this.label4.Text = "Mật khẩu ứng dụng:";
            // 
            // txtMatKhau
            // 
            this.txtMatKhau.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtMatKhau.Location = new System.Drawing.Point(30, 220);
            this.txtMatKhau.Name = "txtMatKhau";
            this.txtMatKhau.PasswordChar = '*';
            this.txtMatKhau.Size = new System.Drawing.Size(390, 23);
            this.txtMatKhau.TabIndex = 6;
            // 
            // btnGui
            // 
            this.btnGui.BackColor = System.Drawing.Color.LimeGreen;
            this.btnGui.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGui.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnGui.ForeColor = System.Drawing.Color.White;
            this.btnGui.Location = new System.Drawing.Point(250, 300);
            this.btnGui.Name = "btnGui";
            this.btnGui.Size = new System.Drawing.Size(80, 35);
            this.btnGui.TabIndex = 7;
            this.btnGui.Text = "Gửi";
            this.btnGui.UseVisualStyleBackColor = false;
            this.btnGui.Click += new System.EventHandler(this.btnGui_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.BackColor = System.Drawing.Color.Gray;
            this.btnHuy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHuy.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnHuy.ForeColor = System.Drawing.Color.White;
            this.btnHuy.Location = new System.Drawing.Point(340, 300);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(80, 35);
            this.btnHuy.TabIndex = 8;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.UseVisualStyleBackColor = false;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.label5.ForeColor = System.Drawing.Color.Gray;
            this.label5.Location = new System.Drawing.Point(30, 250);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(380, 39);
            this.label5.TabIndex = 9;
            this.label5.Text = "Lưu ý: Với Gmail, bạn cần tạo \"Mật khẩu ứng dụng\" thay vì mật khẩu thường.\r\nTruy" +
    " cập: Cài đặt Gmail → Bảo mật → Xác minh 2 bước → Mật khẩu ứng dụng\r\nHoặc sử d" +
    "ụng email khác hỗ trợ SMTP.";
            // 
            // EmailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(450, 350);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnGui);
            this.Controls.Add(this.txtMatKhau);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtEmailNguoiGui);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtEmailNguoiNhan);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EmailForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Gửi Email";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtEmailNguoiNhan;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtEmailNguoiGui;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtMatKhau;
        private System.Windows.Forms.Button btnGui;
        private System.Windows.Forms.Button btnHuy;
        private System.Windows.Forms.Label label5;
    }
}
