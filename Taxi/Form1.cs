using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft;
using DocumentFormat.OpenXml.Drawing;

namespace Taxi
{
    public partial class Form1 : Form
    {
        NpgsqlConnection connection = new NpgsqlConnection("Host=localhost;Port=5432;Username=postgres;Password=1111;Database=Taxi;");
        public Form1()
        {
            InitializeComponent();

            

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            
            this.Enabled = false;
            
            Login login = new Login(this);
            login.ShowDialog();
            if (login.comboBox1.Text == "Пользователь")
            {
                groupBox1.BackColor = Color.Green;
            }
            else
            {
                groupBox1.BackColor = Color.Red;
            }







        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            connection.Open();
            dataGridView1.Columns.Clear();
            string sql = "SELECT * FROM client";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, connection);
            dataGridView1.Columns.Add("S1", "Имя");
            dataGridView1.Columns.Add("S2", "Фамилия");
            dataGridView1.Columns.Add("S3", "Отчество");
            dataGridView1.Columns.Add("S4", "Номер телефона");
            dataGridView1.Columns.Add("S5", "Город");
            dataGridView1.Columns.Add("S6", "Улица");
            dataGridView1.Columns.Add("S7", "Дом");
            using (NpgsqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    dataGridView1.Rows.Add(reader["first_name"], reader["last_name"], reader["potranymic"], reader["phone_number"], reader["city"], reader["street"], reader["house"]);
                }
            }
            connection.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            connection.Open();
            dataGridView1.Columns.Clear();
            string sql = @"select wage.salary, cars.number_cars, driver.first_name, driver.last_name, driver.potranymic, driver.phone_number, driver.driver_license_number, driver.date_of_issue_of_the_driver_license from driver, wage, cars where driver.id_wage = wage.id and driver.id_cars = cars.id";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, connection);
            dataGridView1.Columns.Add("S1", "Зарплата");
            dataGridView1.Columns.Add("S2", "Номер машины");
            dataGridView1.Columns.Add("S3", "Имя");
            dataGridView1.Columns.Add("S4", "Фамилия");
            dataGridView1.Columns.Add("S5", "Отчество");
            dataGridView1.Columns.Add("S6", "Номер телефона");
            dataGridView1.Columns.Add("S7", "Номер водительское удостоверения");
            dataGridView1.Columns.Add("S7", "Дата получения водительского удостоверения");
            using (NpgsqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    dataGridView1.Rows.Add(reader["salary"], reader["number_cars"], reader["first_name"], reader["last_name"], reader["potranymic"], reader["phone_number"], reader["driver_license_number"], reader["date_of_issue_of_the_driver_license"]);
                }
            }
            connection.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            connection.Open();
            dataGridView1.Columns.Clear();
            string sql = @"select stamp.naming, colour.naming as class, clas.naming as class1, cars.number_cars, cars.certificate_of_registration, cars.year_of_release, cars.free_cars from cars, stamp, colour, clas where cars.id_stamp = stamp.id and cars.id_colour = colour.id and cars.id_clas = clas.id";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, connection);
            dataGridView1.Columns.Add("S1", "Марка");
            dataGridView1.Columns.Add("S2", "Цвет");
            dataGridView1.Columns.Add("S3", "Класс");
            dataGridView1.Columns.Add("S4", "Номер машины");
            dataGridView1.Columns.Add("S5", "Сертефикат регистрации");
            dataGridView1.Columns.Add("S6", "Год выпуска");
            dataGridView1.Columns.Add("S7", "Свободна ли машина");
            using (NpgsqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    dataGridView1.Rows.Add(reader["naming"], reader["class"], reader["class1"], reader["number_cars"], reader["certificate_of_registration"], reader["year_of_release"], reader["free_cars"]);
                }
            }
            connection.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            NewDriver insert = new NewDriver();
            insert.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Точно удалить элемент?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    connection.Open();
                    string name = dataGridView1.CurrentCell.Value.ToString();
                    NpgsqlCommand command = new NpgsqlCommand($"DELETE FROM client WHERE first_name='{name}'", connection);
                    command.ExecuteNonQuery();      
                    connection.Close();
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка, выбран неправильный элемент!");
                if(connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

           

            try
            {
                connection.Open();
                string name = dataGridView1.CurrentCell.Value.ToString();
                NpgsqlCommand command = new NpgsqlCommand($"UPDATE client WHERE first_name='{name}'", connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка, выбран неправильный элемент!");
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        public void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            MailForm mail= new MailForm();
            mail.Show();

        }

        private void button8_Click(object sender, EventArgs e)
        {
            string str = "";

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (dataGridView1.Rows[i].Cells[j].Value != null)
                    {
                        str += dataGridView1.Rows[i].Cells[j].Value.ToString() + " ";
                    }
                }

                str += Environment.NewLine;
            }

            using (SaveFileDialog fileDialog = new SaveFileDialog())
            {
                fileDialog.FileName = " ";
                fileDialog.Filter = "Текстовый документ (*.txt)|*.txt|Файл формата csv (*.csv)|*.csv";
                fileDialog.ShowDialog();
                File.WriteAllText(fileDialog.FileName, str);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            connection.Open();
            dataGridView1.Columns.Clear();
            string sql = "SELECT * FROM tovari";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, connection);
            dataGridView1.Columns.Add("S1", "Изображение");
            dataGridView1.Columns.Add("S2", "Количество");
            dataGridView1.Columns.Add("S3", "Цена");
           
            using (NpgsqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    dataGridView1.Rows.Add(reader["image"], reader["countt"], reader["costt"]);
                   // pictureBox1(reader["image"]);
                    //pictureBox1.Image;

                }
            }
            connection.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
    
