using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Newtonsoft.Json;
using System.Threading;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace WinformMenu
{
    public partial class Form5 : Form
    {
        MySqlConnection connection;

        private DataTable productsTable; // 상품 목록을 저장할 DataTable
        private decimal totalAmount; // 총 금액을 저장할 변수

        private string username { get; set; }

        public Form5(string username)
        {
            InitializeComponent();
            progressBar1.Visible = false;
            this.username = username;
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            LoadProducts(); // 상품 목록을 불러옴
            UpdateTotalAmount(); // 초기 총 금액을 업데이트

            // DataGridView에 상품 목록 바인딩
            dataGridView1.DataSource = productsTable;

            listView1.View = View.Details;
            listView1.Columns.Add("상품명", 100);
            listView1.Columns.Add("가격", 70);
            listView1.Columns.Add("수량", 50);
            listView1.Columns.Add("합계", 70);

        }

        private void LoadProducts()
        {
            string connectionString = ("DB");
            string query = "SELECT i_name AS '상품명', i_price AS '가격', i_count AS '현재 수량' FROM item_table";

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

        private void UpdateTotalAmount()
        {
            totalAmount = 0;

            foreach (ListViewItem item in listView1.Items)
            {
                int price = int.Parse(item.SubItems[1].Text);
                int quantity = int.Parse(item.SubItems[2].Text);
                int subtotal = price * quantity;

                totalAmount += subtotal;

                item.SubItems[3].Text = subtotal.ToString();  // 합계 열 값 업데이트
            }

            labelTotalAmount.Text = totalAmount.ToString("C");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string productName = dataGridView1.Rows[e.RowIndex].Cells["상품명"].Value.ToString();
                int productPrice = int.Parse(dataGridView1.Rows[e.RowIndex].Cells["가격"].Value.ToString());
                int productQuantity = int.Parse(dataGridView1.Rows[e.RowIndex].Cells["현재 수량"].Value.ToString());

                // 상품 수량이 0보다 큰 경우에만 동작하도록 조건 추가
                if (productQuantity > 0)
                {
                    ListViewItem existingItem = listView1.Items.Cast<ListViewItem>().FirstOrDefault(item => item.SubItems[0].Text == productName);

                    if (existingItem != null)
                    {
                        int quantity = int.Parse(existingItem.SubItems[2].Text);
                        quantity++;
                        existingItem.SubItems[2].Text = quantity.ToString();
                    }
                    else
                    {
                        ListViewItem newItem = new ListViewItem(new[] { productName, productPrice.ToString(), "1", "" });
                        listView1.Items.Add(newItem);
                    }

                    // 상품 수량 감소
                    dataGridView1.Rows[e.RowIndex].Cells["현재 수량"].Value = (productQuantity - 1).ToString();

                    UpdateTotalAmount();
                }
                else
                {
                    MessageBox.Show("해당 상품은 현재 품절입니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btn_main_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4(username);
            form4.Show();
            this.Close();
        }
        private void btn_pay_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count == 0)
            {
                MessageBox.Show("장바구니가 비어 있습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                // 버튼 비활성화
                btn_pay.Enabled = false;
                btn_main.Enabled = false;

                // 프로그레스바 활성화
                progressBar1.Visible = true;
                progressBar1.Maximum = 100;
                progressBar1.Value = 0;

                int progressBarDuration = 2000; // 2초
                int progressBarInterval = 100; // 0.1초

                for (int i = 0; i <= progressBarDuration / progressBarInterval; i++)
                {
                    progressBar1.Value = (i * progressBarInterval) * 100 / progressBarDuration;
                    Application.DoEvents();
                    System.Threading.Thread.Sleep(progressBarInterval);
                }

                // 프로그레스바 비활성화
                progressBar1.Visible = false;

                // 버튼 활성화
                btn_pay.Enabled = true;
                btn_main.Enabled = true;

                foreach (ListViewItem item in listView1.Items)
                {
                    string productName = item.SubItems[0].Text;
                    int quantity = int.Parse(item.SubItems[2].Text);

                    // 상품 수량을 업데이트하고 DB에 반영합니다.
                    UpdateProductQuantityInDB(productName, quantity);

                    // 상품 정보를 sales_table에 저장합니다.
                    InsertProductQuantityInDB(productName, quantity);
                }

                listView1.Items.Clear();

                totalAmount = 0;
                labelTotalAmount.Text = totalAmount.ToString("C");

                MessageBox.Show("결제가 완료되었습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"결제 중 오류가 발생했습니다: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 재고 관리와 연결
        private void UpdateProductQuantityInDB(string productName, int quantity)
        {
            string connectionString = "DB";
            string query = "UPDATE item_table SET i_count = i_count - @quantity WHERE i_name = @productName";

            using (connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@quantity", quantity);
                    command.Parameters.AddWithValue("@productName", productName);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        // 판매 현황과 연결
        private void InsertProductQuantityInDB(string productName, int quantity)
        {
            string connectionString = "DB";
            string selectQuery = "SELECT i_sn, i_price FROM item_table WHERE i_name = @productName";
            string insertQuery = "INSERT INTO sales_table(s_name, s_price, s_count, s_total, s_sn, s_date) VALUES (@productName, @price, @quantity, @total, @s_sn, @date)";

            using (connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // 상품 정보 가져오기
                string s_sn;
                int price;
                using (MySqlCommand selectCommand = new MySqlCommand(selectQuery, connection))
                {
                    selectCommand.Parameters.AddWithValue("@productName", productName);
                    using (MySqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            s_sn = reader.GetString("i_sn");
                            price = reader.GetInt32("i_price");
                        }
                        else
                        {
                            throw new Exception("상품 정보를 찾을 수 없습니다.");
                        }
                    }
                }

                // 상품 정보 저장
                using (MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection))
                {
                    insertCommand.Parameters.AddWithValue("@productName", productName);
                    insertCommand.Parameters.AddWithValue("@price", price);
                    insertCommand.Parameters.AddWithValue("@quantity", quantity);
                    insertCommand.Parameters.AddWithValue("@total", price * quantity);
                    insertCommand.Parameters.AddWithValue("@s_sn", s_sn);
                    insertCommand.Parameters.AddWithValue("@date", DateTime.Now);

                    insertCommand.ExecuteNonQuery();
                }
            }
        }
    }
}

        /*
        private async Task<string> GetPaymentUrl(string itemName, decimal totalAmount)
        {
            try
            {
                string apiUrl = "https://kapi.kakao.com/v1/payment/ready";
                string apiKey = "---";  // 발급받은 KakaoPay API 키를 입력해야 합니다.

                // 결제 API 요청 데이터 구성
                var requestBody = new
                {
                    cid = "TC0ONETIME",
                    partner_order_id = "partner_order_id",
                    partner_user_id = "partner_user_id",
                    item_name = itemName,
                    quantity = 1,
                    total_amount = (int)totalAmount,
                    tax_free_amount = 0,
                    approval_url = "https://your-approval-url.com",
                    cancel_url = "https://your-cancel-url.com",
                    fail_url = "https://your-fail-url.com"
                };

                var json = JsonConvert.SerializeObject(requestBody);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // 결제 API 호출
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"KakaoAK {apiKey}");

                    var response = await client.PostAsync(apiUrl, content);
                    var responseContent = await response.Content.ReadAsStringAsync();

                    // 결제 API 응답 처리
                    if (response.IsSuccessStatusCode)
                    {
                        dynamic responseData = JsonConvert.DeserializeObject(responseContent);
                        string paymentUrl = responseData.next_redirect_pc_url;
                        return paymentUrl;
                    }
                    else
                    {
                        throw new Exception("결제 요청에 실패했습니다.");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"결제 중 오류가 발생했습니다: {ex.Message}");
            }
        }

        private void btn_pay_Click(object sender, EventArgs e)
        {
            // 장바구니에 상품이 있는지 확인합니다.
            if (listView1.Items.Count == 0)
            {
                MessageBox.Show("장바구니가 비어 있습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 결제 처리를 수행합니다.
            try
            {
                // 결제를 위한 데이터 준비
                string itemName = "상품명";  // 상품명을 설정해야 합니다.
                decimal totalAmount = CalculateTotalAmount();  // 총 결제 금액을 계산해야 합니다.

                // 결제 페이지 URL을 가져옵니다.
                Task<string> getPaymentUrlTask = GetPaymentUrl(itemName, totalAmount);
                string paymentUrl = getPaymentUrlTask.GetAwaiter().GetResult();

                // 결제 페이지를 웹 브라우저로 엽니다.
                System.Diagnostics.Process.Start(paymentUrl);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"결제 중 오류가 발생했습니다: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private decimal CalculateTotalAmount()
        {
            decimal totalAmount = 0;

            foreach (ListViewItem item in listView1.Items)
            {
                int price = int.Parse(item.SubItems[1].Text);
                int quantity = int.Parse(item.SubItems[2].Text);

                totalAmount += price * quantity;
            }

            return totalAmount;
        }
    }

}*/