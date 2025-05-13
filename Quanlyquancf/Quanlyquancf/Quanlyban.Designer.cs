namespace Quanlyquancf
{
    partial class Quanlyban
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnChange = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox3);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Blue;
            this.groupBox1.Location = new System.Drawing.Point(16, 125);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(791, 470);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Bàn";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(207, 271);
            this.textBox3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(509, 44);
            this.textBox3.TabIndex = 23;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(8, 271);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(175, 32);
            this.label3.TabIndex = 22;
            this.label3.Text = "Mã khu vực:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(207, 188);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(509, 44);
            this.textBox1.TabIndex = 21;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(8, 188);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 32);
            this.label2.TabIndex = 20;
            this.label2.Text = "Tên bàn:";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(207, 94);
            this.textBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(509, 44);
            this.textBox2.TabIndex = 19;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(8, 94);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 32);
            this.label1.TabIndex = 18;
            this.label1.Text = "Mã bàn:";
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.Green;
            this.btnAdd.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnAdd.Location = new System.Drawing.Point(16, 636);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(161, 48);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "Thêm";
            this.btnAdd.UseVisualStyleBackColor = false;
            // 
            // btnChange
            // 
            this.btnChange.BackColor = System.Drawing.Color.Blue;
            this.btnChange.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnChange.Location = new System.Drawing.Point(221, 636);
            this.btnChange.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnChange.Name = "btnChange";
            this.btnChange.Size = new System.Drawing.Size(161, 48);
            this.btnChange.TabIndex = 2;
            this.btnChange.Text = "Cập nhật";
            this.btnChange.UseVisualStyleBackColor = false;
            // 
            // btnDel
            // 
            this.btnDel.BackColor = System.Drawing.Color.Orange;
            this.btnDel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnDel.Location = new System.Drawing.Point(431, 636);
            this.btnDel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(161, 48);
            this.btnDel.TabIndex = 3;
            this.btnDel.Text = "Xóa";
            this.btnDel.UseVisualStyleBackColor = false;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Red;
            this.btnExit.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnExit.Location = new System.Drawing.Point(645, 636);
            this.btnExit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(161, 48);
            this.btnExit.TabIndex = 4;
            this.btnExit.Text = "Thoát";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(852, 144);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.Size = new System.Drawing.Size(917, 846);
            this.dataGridView1.TabIndex = 5;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(852, 31);
            this.textBox4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(659, 31);
            this.textBox4.TabIndex = 24;
            this.textBox4.TextChanged += new System.EventHandler(this.textBox4_TextChanged);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.Brown;
            this.btnSearch.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnSearch.Location = new System.Drawing.Point(1567, 24);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(161, 48);
            this.btnSearch.TabIndex = 25;
            this.btnSearch.Text = "Tìm kiếm";
            this.btnSearch.UseVisualStyleBackColor = false;
            // 
            // Quanlyban
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1796, 1134);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.btnChange);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Quanlyban";
            this.Text = "Quanlyban";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnChange;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Button btnSearch;
    }
}