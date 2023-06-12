using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Menu;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace WinformMenu
{
    public partial class Form4 : Form
    {
        public string username { get; set; }

        public Form4(string username)
        {
            InitializeComponent();
            this.username = username;
    }
        private void Form4_Load(object sender, EventArgs e)
        {
            if (username == "admin")
            {
                SetLabel();
                // 관리자인 경우 모든 버튼을 활성화합니다.
                btn_sales.Enabled = true;
                btn_item.Enabled = true;
            }
            else
            {
                // 관리자가 아닌 경우 Sales 버튼과 Item 버튼을 비활성화합니다.
                btn_sales.Enabled = false;
                btn_item.Enabled = false;
            }
        }

        public void SetLabel()
        {
            label1.Text = "관리자 모드";
        }

        private void btn_manage_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5(username);
            form5.Show();
            this.Close();
        }

        private void btn_sales_Click(object sender, EventArgs e)
        {
            Form6 form6 = new Form6(username);
            form6.Show();
            this.Close();
        }

        private void btn_item_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3(username);
            form3.Show();
            this.Hide();
        }

        private void btn_logout_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Close();
        }
    }
}
