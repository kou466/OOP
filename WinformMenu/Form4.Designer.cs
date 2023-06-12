namespace WinformMenu
{
    partial class Form4
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
            this.btn_manage = new System.Windows.Forms.Button();
            this.btn_sales = new System.Windows.Forms.Button();
            this.btn_item = new System.Windows.Forms.Button();
            this.btn_logout = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_manage
            // 
            this.btn_manage.Location = new System.Drawing.Point(146, 154);
            this.btn_manage.Name = "btn_manage";
            this.btn_manage.Size = new System.Drawing.Size(125, 125);
            this.btn_manage.TabIndex = 0;
            this.btn_manage.Text = "매장관리";
            this.btn_manage.UseVisualStyleBackColor = true;
            this.btn_manage.Click += new System.EventHandler(this.btn_manage_Click);
            // 
            // btn_sales
            // 
            this.btn_sales.Location = new System.Drawing.Point(357, 154);
            this.btn_sales.Name = "btn_sales";
            this.btn_sales.Size = new System.Drawing.Size(125, 125);
            this.btn_sales.TabIndex = 1;
            this.btn_sales.Text = "매출현황";
            this.btn_sales.UseVisualStyleBackColor = true;
            this.btn_sales.Click += new System.EventHandler(this.btn_sales_Click);
            // 
            // btn_item
            // 
            this.btn_item.Location = new System.Drawing.Point(576, 154);
            this.btn_item.Name = "btn_item";
            this.btn_item.Size = new System.Drawing.Size(125, 125);
            this.btn_item.TabIndex = 2;
            this.btn_item.Text = "재고현황";
            this.btn_item.UseVisualStyleBackColor = true;
            this.btn_item.Click += new System.EventHandler(this.btn_item_Click);
            // 
            // btn_logout
            // 
            this.btn_logout.Location = new System.Drawing.Point(713, 415);
            this.btn_logout.Name = "btn_logout";
            this.btn_logout.Size = new System.Drawing.Size(75, 23);
            this.btn_logout.TabIndex = 3;
            this.btn_logout.Text = "로그아웃";
            this.btn_logout.UseVisualStyleBackColor = true;
            this.btn_logout.Click += new System.EventHandler(this.btn_logout_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("굴림", 12F);
            this.label1.Location = new System.Drawing.Point(12, 420);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 16);
            this.label1.TabIndex = 4;
            // 
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_logout);
            this.Controls.Add(this.btn_item);
            this.Controls.Add(this.btn_sales);
            this.Controls.Add(this.btn_manage);
            this.Name = "Form4";
            this.Text = "Form4";
            this.Load += new System.EventHandler(this.Form4_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_manage;
        private System.Windows.Forms.Button btn_sales;
        private System.Windows.Forms.Button btn_item;
        private System.Windows.Forms.Button btn_logout;
        private System.Windows.Forms.Label label1;
    }
}