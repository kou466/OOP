namespace WinformMenu
{
    partial class Form6
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
            this.btn_main = new System.Windows.Forms.Button();
            this.btn_day_sales = new System.Windows.Forms.Button();
            this.btn_month_sales = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_main
            // 
            this.btn_main.Location = new System.Drawing.Point(725, 410);
            this.btn_main.Name = "btn_main";
            this.btn_main.Size = new System.Drawing.Size(63, 28);
            this.btn_main.TabIndex = 0;
            this.btn_main.Text = "메인";
            this.btn_main.UseVisualStyleBackColor = true;
            this.btn_main.Click += new System.EventHandler(this.btn_main_Click);
            // 
            // btn_day_sales
            // 
            this.btn_day_sales.Location = new System.Drawing.Point(668, 12);
            this.btn_day_sales.Name = "btn_day_sales";
            this.btn_day_sales.Size = new System.Drawing.Size(120, 44);
            this.btn_day_sales.TabIndex = 2;
            this.btn_day_sales.Text = "일 매출";
            this.btn_day_sales.UseVisualStyleBackColor = true;
            this.btn_day_sales.Click += new System.EventHandler(this.btn_day_sales_Click);
            // 
            // btn_month_sales
            // 
            this.btn_month_sales.Location = new System.Drawing.Point(668, 240);
            this.btn_month_sales.Name = "btn_month_sales";
            this.btn_month_sales.Size = new System.Drawing.Size(120, 44);
            this.btn_month_sales.TabIndex = 2;
            this.btn_month_sales.Text = "월 매출";
            this.btn_month_sales.UseVisualStyleBackColor = true;
            this.btn_month_sales.Click += new System.EventHandler(this.btn_month_sales_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(26, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(582, 198);
            this.dataGridView1.TabIndex = 4;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.AllowUserToResizeColumns = false;
            this.dataGridView2.AllowUserToResizeRows = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(26, 240);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.RowTemplate.Height = 23;
            this.dataGridView2.Size = new System.Drawing.Size(582, 198);
            this.dataGridView2.TabIndex = 5;
            // 
            // Form6
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btn_month_sales);
            this.Controls.Add(this.btn_day_sales);
            this.Controls.Add(this.btn_main);
            this.Name = "Form6";
            this.Text = "Form6";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_main;
        private System.Windows.Forms.Button btn_day_sales;
        private System.Windows.Forms.Button btn_month_sales;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}