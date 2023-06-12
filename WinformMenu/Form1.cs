using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace WinformMenu
{
    public partial class Form1 : Form
    {
        MySqlConnection connection;

        private string username;

        public Form1()
        {
            InitializeComponent();
            SetIDFocus();
        }

        public void SetIDFocus()
        {
            ID.Focus();
        }
        private void signin_Click(object sender, EventArgs e)
        {
            try
            {
                connection = new MySqlConnection("DB");
                connection.Open();

                string loginid = ID.Text;
                string loginpwd = PW.Text;

                string selectQuery = "SELECT userPW, userID FROM account_info WHERE userID = @loginid";
                MySqlCommand selectCommand = new MySqlCommand(selectQuery, connection);
                selectCommand.Parameters.AddWithValue("@loginid", loginid);

                MySqlDataReader reader = selectCommand.ExecuteReader();

                if (reader.Read())
                {
                    string storedPassword = reader.GetString("userPW");
                    username = reader.GetString("userID");

                    if (VerifyPassword(loginpwd, storedPassword))
                    {
                        MessageBox.Show("로그인 완료");
                        ID.Text = "";
                        PW.Text = "";

                        Form4 form4 = new Form4(username);
                        form4.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("회원 정보를 확인해 주세요.");
                        PW.Text = "";
                        PW.Focus();
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("회원 정보를 확인해 주세요.");
                    PW.Text = "";
                    PW.Focus();
                    return;
                }

                reader.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // 비밀번호 검증 함수
        private bool VerifyPassword(string password, string hashedPassword)
        {
            byte[] hashedBytes = Convert.FromBase64String(hashedPassword);

            using (SHA512 sha512 = SHA512.Create())
            {
                byte[] computedHash = sha512.ComputeHash(Encoding.UTF8.GetBytes(password));

                return computedHash.SequenceEqual(hashedBytes);
            }
        }

        private void signup_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
            this.Hide();
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("종료하시겠습니까?", "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                // 프로그램 종료
                Application.Exit();
            }
            // 그대로 실행
        }
    }
}