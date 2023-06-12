using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;

namespace WinformMenu
{
    public partial class Form2 : Form
    {
        MySqlConnection connection;
        public Form2()
        {
            InitializeComponent();
        }

        private bool UserIsPresent(string id)
        {
            string query = "SELECT COUNT(*) FROM account_info WHERE userID = @id";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);


            int count = Convert.ToInt32(command.ExecuteScalar());
            return count > 0;
        }

        private bool IsPasswordValid(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("비밀번호를 입력해주세요.");
                return false;
            }
            return true;
        }


        private void signup_Click(object sender, EventArgs e)
        {
            try
            {
                connection = new MySqlConnection("DB");
                connection.Open();

                string userID = ID.Text.Trim();
                string userPW = PW.Text;

                if (UserIsPresent(userID))
                {
                    MessageBox.Show("이미 존재하는 아이디입니다.");
                    ID.Text = "";
                    PW.Text = "";
                    ID.Focus();
                    return;
                }
                else if (!IsPasswordValid(userPW))
                {
                    PW.Text = "";
                    PW.Focus();
                    return;
                }
                else
                { // 비밀번호 암호화
                    byte[] passwordBytes = Encoding.UTF8.GetBytes(userPW);
                    byte[] hashedBytes;

                    using (SHA512 sha512 = SHA512.Create())
                    {
                        hashedBytes = sha512.ComputeHash(passwordBytes);
                    }

                    string hashedPassword = Convert.ToBase64String(hashedBytes);

                    string insertQuery = "INSERT INTO account_info (userID, userPW) VALUES (@userID, @userPW);";

                    MySqlCommand command = new MySqlCommand(insertQuery, connection);
                    command.Parameters.AddWithValue("@userID", userID);
                    command.Parameters.AddWithValue("@userPW", hashedPassword);

                    if (command.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("회원가입이 완료되었습니다.");
                        connection.Close();

                        Form1 form = new Form1();
                        form.Show();
                        form.SetIDFocus();

                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("회원가입 중 오류가 발생했습니다.");
                        ID.Text = "";
                        PW.Text = "";
                        ID.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Close();
        }
    }
}
