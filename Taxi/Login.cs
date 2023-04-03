using Microsoft.VisualBasic.Logging;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Taxi
{
    public partial class Login : Form
    {
        NpgsqlConnection connection = new NpgsqlConnection("Host=localhost;Port=5432;Username=postgres;Password=1111;Database=Taxi;");
        
        Form1 form;
        public Login(Form1 form1)
        {
            form = form1;
            InitializeComponent();

        }
        int counter = 3;
        private void button1_Click(object sender, EventArgs e)
        {
            
            counter--;
            label5.Text = "Осталось попыток: " + counter.ToString();
            if (label3.Text==textBox3.Text)
            {
                
           


            try
            {
                string login = "qqqqqq#%@@##@&@&&@&@qqqqqqqqq";
                string password = "@#^@#*^@U^(@(^*@#^*&@#^";
                connection.Open();
                NpgsqlCommand command1 = new($"SELECT login FROM users WHERE login = '{textBox1.Text}'", connection);
                NpgsqlCommand command2 = new($"SELECT password FROM users WHERE password = '{textBox2.Text}'", connection);
                    NpgsqlCommand command3 = new($"SELECT login FROM admin WHERE login = '{textBox1.Text}'", connection);
                    NpgsqlCommand command4 = new($"SELECT password FROM admin WHERE password = '{textBox2.Text}'", connection);

                    if (comboBox1.SelectedItem == "Пользователь")
                    {



                        using (var reader = command1.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                login = reader["login"].ToString();
                            }
                        }
                        using (var reader = command2.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                password = reader["password"].ToString();
                            }
                        }

                        if (login == textBox1.Text)
                        {
                            if (password == textBox2.Text)
                            {
                                form.Enabled = true;
                                this.Close();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Пользователя не существует", "Ошибка");
                        }
                        connection.Close();
                    }
                    else if (comboBox1.SelectedItem == "Админ")
                    {
                        using (var reader = command3.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                login = reader["login"].ToString();
                            }
                        }
                        using (var reader = command4.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                password = reader["password"].ToString();
                            }
                        }

                        if (login == textBox1.Text)
                        {
                            if (password == textBox2.Text)
                            {
                                form.Enabled = true;
                                this.Close();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Пользователя не существует", "Ошибка");
                        }
                        connection.Close();
                    }

                    
            }
            catch
            {
                MessageBox.Show("Не удалось пройти авторизацию", "Ошибка");
            }
            }
            
            else if (counter == 2)
            {

                
                MessageBox.Show("Капча не верная");
                this.OnLoad(e);
                
            }

            else if (counter == 1)
            {

               
                MessageBox.Show("Капча не верная");
                this.OnLoad(e);

            }


            else if (counter == 0)
            {
                this.Enabled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (label3.Text == textBox3.Text)
            {
                Registration registration = new Registration();
                registration.Show();
            }
            else
            {
                MessageBox.Show("Капча не верная");
                this.OnLoad(e);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        public void Login_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("Пользователь");
            comboBox1.Items.Add("Админ");

            



           Random rand = new Random();
            int num = rand.Next(6, 8);
            string captcha = "";
            int totl = 0;
            do
            {
                int chr = rand.Next(48, 123);
                if ((chr >= 48 && chr <= 57) || (chr >= 65 && chr <= 90) || (chr >= 97 && chr <= 122))
                {
                    captcha = captcha + (char)chr;
                    totl++;
                    if (totl == num)
                        break;
                    {

                    }
                }
            } while(true);
           label3.Text= captcha;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        public void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
           
            
        }
    }
}
