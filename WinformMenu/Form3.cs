using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace WinformMenu
{
    public partial class Form3 : Form
    {
        MySqlConnection connection;

        private DataTable productsTable; // 상품 목록을 저장할 DataTable

        private string username { get; set; }
        public Form3(string username)
        {
            InitializeComponent();
            this.username = username;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            LoadProducts();
            dataGridView1.DataSource = productsTable;
        }

        private void btn_main_Click(object sender, EventArgs e)
        {
            Form4 form4;
            form4 = new Form4(username);
            form4.Show();
            this.Close();
        }

        public void searchProduct(string valueToSearch)
        {
            string connectionString ="DB";
            string query = "SELECT i_sn AS '상품번호', i_name AS '상품명', i_price AS '가격', i_count AS '현재 수량' FROM item_table WHERE CONCAT(`i_name`, `i_price`, `i_count`, `i_sn`) LIKE '%" + valueToSearch + "%'";
            using (connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    // 데이터베이스 연결 및 명령 실행
                    connection.Open();
                    MySqlDataReader reader = command.ExecuteReader();

                    // DataTable 생성 및 데이터 채우기
                    productsTable = new DataTable();
                    productsTable.Load(reader);

                    // 데이터 그리드 뷰에 바인딩
                    dataGridView1.DataSource = productsTable;
                }
            }
            if (productsTable.Rows.Count == 0)
            {
                DialogResult result = MessageBox.Show("해당 상품이 없습니다.\n추가하시겠습니까?", "확인", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                { // 추가 작업
                    LoadProducts();
                    dataGridView1.DataSource = productsTable;
                    tb_SN.Focus();
                }
                else
                { // 취소 작업
                    LoadProducts();
                    dataGridView1.DataSource = productsTable;
                    tb_search.Focus();
                }
            }
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(tb_search.Text))
            {
                MessageBox.Show("검색 정보를 입력해주세요.");
            }
            else
            {
                string valueToSearch = tb_search.Text;
                searchProduct(valueToSearch);
            }
        }

        private void Insert_Click(object sender, EventArgs e)
        {
            string sn = tb_SN.Text;
            string name = tb_name.Text;
            string price = tb_price.Text;
            string count = tb_count.Text;

            connection = new MySqlConnection("DB");
            string query = "INSERT INTO item_table (i_sn, i_name, i_price, i_count) VALUES (@sn, @name, @price, @count)";
            MySqlCommand command = new MySqlCommand(query, connection);

            command.Parameters.AddWithValue("@sn", tb_SN.Text);
            command.Parameters.AddWithValue("@name", tb_name.Text);
            command.Parameters.AddWithValue("@price", tb_price.Text);
            command.Parameters.AddWithValue("@count", tb_count.Text);

            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("추가 완료");

                    LoadProducts();
                    dataGridView1.DataSource = productsTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("모든 항목을 입력해주세요.", ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void Update_Click(object sender, EventArgs e)
        {
            string connectionString = ("DB");
            if (string.IsNullOrEmpty(tb_SN.Text) || string.IsNullOrEmpty(tb_name.Text) || string.IsNullOrEmpty(tb_price.Text) || string.IsNullOrEmpty(tb_count.Text))
            {
                MessageBox.Show("항목을 정확히 입력해주세요.");
                tb_SN.Clear();
                tb_name.Clear();
                tb_price.Clear();
                tb_count.Clear();
            }
            else
            {
                int price = int.Parse(tb_price.Text);
                int count = int.Parse(tb_count.Text);

                string query = "UPDATE item_table SET i_sn = @sn, i_name = @name, i_price = @price, i_count = @count WHERE i_sn = @sn";
                using (connection = new MySqlConnection(connectionString))
                {
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@sn", this.tb_SN.Text);
                        command.Parameters.AddWithValue("@name", this.tb_name.Text);
                        command.Parameters.AddWithValue("@price", this.tb_price.Text);
                        command.Parameters.AddWithValue("@count", this.tb_count.Text);

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("수정 완료되었습니다.");
                            LoadProducts();
                            dataGridView1.DataSource = productsTable;
                        }
                        else
                        {
                            MessageBox.Show("해당 항목을 찾을 수 없습니다.");
                        }
                    }
                }
            }
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            string connectionString = ("DB");
            if (string.IsNullOrEmpty(tb_SN.Text) || string.IsNullOrEmpty(tb_name.Text) || string.IsNullOrEmpty(tb_price.Text) || string.IsNullOrEmpty(tb_count.Text))
            {
                MessageBox.Show("해당 항목을 찾을 수 없습니다.");
            }
            else
            {
                DialogResult result = MessageBox.Show("정말로 항목을 삭제하시겠습니까?", "삭제 확인", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    string query = "DELETE FROM item_table WHERE i_sn = @sn AND i_name = @name AND i_price = @price AND i_count = @count";
                    using (connection = new MySqlConnection(connectionString))
                    {
                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@sn", this.tb_SN.Text);
                            command.Parameters.AddWithValue("@name", this.tb_name.Text);
                            command.Parameters.AddWithValue("@price", this.tb_price.Text);
                            command.Parameters.AddWithValue("@count", this.tb_count.Text);

                            connection.Open();
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("삭제 완료되었습니다.");

                                tb_SN.Clear();
                                tb_name.Clear();
                                tb_price.Clear();
                                tb_count.Clear();

                                LoadProducts();
                                dataGridView1.DataSource = productsTable;
                            }
                            else
                            {
                                MessageBox.Show("해당 항목을 찾을 수 없습니다.");
                            }
                        }
                    }
                }
            }
        }

        private void LoadProducts()
        {
            string connectionString = ("DB");
            string query = "SELECT i_sn AS '상품번호', i_name AS '상품명', i_price AS '가격', i_count AS '현재 수량' FROM item_table";

            using (connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    // 데이터베이스 연결 및 명령 실행
                    connection.Open();
                    MySqlDataReader reader = command.ExecuteReader();

                    // DataTable 생성 및 데이터 채우기
                    productsTable = new DataTable();
                    productsTable.Load(reader);
                }
            }
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            LoadProducts();
            dataGridView1.DataSource = productsTable;

            tb_SN.Clear();
            tb_name.Clear();
            tb_price.Clear();
            tb_count.Clear();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                tb_SN.Text = row.Cells[0].Value.ToString();
                tb_name.Text = row.Cells[1].Value.ToString();
                tb_price.Text = row.Cells[2].Value.ToString();
                tb_count.Text = row.Cells[3].Value.ToString();
            }
        }
    }
}
