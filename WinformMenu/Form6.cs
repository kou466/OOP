using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinformMenu
{
    public partial class Form6 : Form
    {
        MySqlConnection connection;

        private string username { get; set; }

        public Form6(string username)
        {
            InitializeComponent();
            this.username = username;

            // MySQL 연결 설정
            string connectionString = ("DB");
            connection = new MySqlConnection(connectionString);
            connection.Open();
        }

        private void btn_main_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4(username);
            form4.Show();
            this.Close();
        }

        private void btn_day_sales_Click(object sender, EventArgs e)
        {
            // 일별 매출 조회
            string dailySalesQuery = "SELECT DATE(s_date) AS '날짜', SUM(s_total) AS '일매출' FROM sales_table GROUP BY DATE(s_date)";
            MySqlCommand dailySalesCommand = new MySqlCommand(dailySalesQuery, connection);
            MySqlDataAdapter dailySalesAdapter = new MySqlDataAdapter(dailySalesCommand);
            DataTable dailySalesTable = new DataTable();
            dailySalesAdapter.Fill(dailySalesTable);

            // DataGridView에 데이터 바인딩
            dataGridView1.DataSource = dailySalesTable;
        }

        private void btn_month_sales_Click(object sender, EventArgs e)
        {
            // 월별 매출 조회
            string monthlySalesQuery = "SELECT YEAR(s_date) AS '년도', MONTH(s_date) AS '월', SUM(s_total) AS '월 매출' FROM sales_table GROUP BY YEAR(s_date), MONTH(s_date)";
            MySqlCommand monthlySalesCommand = new MySqlCommand(monthlySalesQuery, connection);
            MySqlDataAdapter monthlySalesAdapter = new MySqlDataAdapter(monthlySalesCommand);
            DataTable monthlySalesTable = new DataTable();
            monthlySalesAdapter.Fill(monthlySalesTable);

            // DataGridView에 데이터 바인딩
            dataGridView2.DataSource = monthlySalesTable;
        }
    }
}