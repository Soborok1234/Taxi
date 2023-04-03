using DocumentFormat.OpenXml.Drawing.Diagrams;
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
    public partial class NewDriver : Form
    {
        NpgsqlConnection connection = new NpgsqlConnection("Host=localhost;Port=5432;Username=postgres;Password=1111;Database=Taxi;");
        public NewDriver()
        {
            InitializeComponent();
        }

        private void NewDriver_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM client", connection);
                int max_id = 0;

                using (NpgsqlDataReader rd = command.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        max_id = Convert.ToInt32(rd["id"]);
                    }
                }
                max_id += 1;

                NpgsqlCommand command_2 = new NpgsqlCommand(string.Format(@"INSERT INTO client VALUES('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}')", max_id, textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text, textBox7.Text), connection);
                command_2.ExecuteNonQuery();
                connection.Close();
            }
            catch
            {
                MessageBox.Show("Запись не была добавлена", "Ошибка");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
