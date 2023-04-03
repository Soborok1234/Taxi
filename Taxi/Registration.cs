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

namespace Taxi
{
    public partial class Registration : Form
    {
        NpgsqlConnection connection = new NpgsqlConnection("Host=localhost;Port=5432;Username=postgres;Password=1111;Database=Taxi;");
        public Registration()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM users", connection);
                int max_id = 0;
                using (NpgsqlDataReader rd = command.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        max_id = Convert.ToInt32(rd["id_user"]);
                    }
                }
                max_id += 1;
                NpgsqlCommand command_2 = new NpgsqlCommand(string.Format(@"INSERT INTO users VALUES({0}, '{1}', '{2}')", max_id, textBox1.Text, textBox2.Text), connection);
                command_2.ExecuteNonQuery();
                connection.Close();
                this.Close();
            }
            catch
            {
                MessageBox.Show("Пользователь не может быть добавлен", "Ошибка");
            }
        }

        private void Registration_Load(object sender, EventArgs e)
        {

        }
    }
}
