using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace xml2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeDataGridView();
        }

        private void InitializeDataGridView()
        {
            dataGridView1.Columns.Add("Name", "Name");
            dataGridView1.Columns.Add("Age", "Age");
            dataGridView1.Columns.Add("Programmer", "Programmer");
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Заполните все поля.", "Ошибка.");
            }
            else
            {
                dataGridView1.Rows.Add(textBox1.Text, numericUpDown1.Value, comboBox1.Text);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedIndex = dataGridView1.SelectedRows[0].Index;
                dataGridView1.Rows[selectedIndex].SetValues(textBox1.Text, numericUpDown1.Value, comboBox1.Text);
            }
            else
            {
                MessageBox.Show("Выберите строку для редактирования.", "Ошибка.");
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
            }
            else
            {
                MessageBox.Show("Выберите строку для удаления.", "Ошибка.");
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value?.ToString();
                int n = 0;
                if (dataGridView1.SelectedRows[0].Cells[1].Value != null)
                {
                    int.TryParse(dataGridView1.SelectedRows[0].Cells[1].Value.ToString(), out n);
                }
                numericUpDown1.Value = n;
                comboBox1.Text = dataGridView1.SelectedRows[0].Cells[2].Value?.ToString();

               
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet dataSet = new DataSet();
                DataTable dataTable = new DataTable("Employee");
                dataTable.Columns.Add("Name");
                dataTable.Columns.Add("Age", typeof(int));
                dataTable.Columns.Add("Programmer");
                dataSet.Tables.Add(dataTable);

                foreach (DataGridViewRow dataGridViewRow in dataGridView1.Rows)
                {
                    DataRow row = dataTable.NewRow();
                    row["Name"] = dataGridViewRow.Cells[0].Value;
                    row["Age"] = dataGridViewRow.Cells[1].Value;
                    row["Programmer"] = dataGridViewRow.Cells[2].Value;
                    dataTable.Rows.Add(row);
                }

                dataSet.WriteXml("Data.xml");
                MessageBox.Show("XML файл успешно сохранен.", "Ошибка.");
            }
            catch
            {
                MessageBox.Show("Невозможно сохранить XML файл.", "Ошибка.");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                MessageBox.Show("Очистите поле перед загрузкой нового файла.", "Ошибка.");
                return;
            }

            if (File.Exists("Data.xml"))
            {
                try
                {
                    DataSet dataSet = new DataSet();
                    dataSet.ReadXml("Data.xml");

                    foreach (DataRow row in dataSet.Tables["Employee"].Rows)
                    {
                        dataGridView1.Rows.Add(row["Name"], row["Age"], row["Programmer"]);
                    }

                    MessageBox.Show("Данные успешно загружены из XML файла.", "Ошибка.");
                }
                catch
                {
                    MessageBox.Show("Не удалось загрузить данные из XML файла.", "Ошибка.");
                }
            }
            else
            {
                MessageBox.Show("XML файл не найден.", "Ошибка.");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Rows.Clear();
            }
            else
            {
                MessageBox.Show("Таблица пуста.", "Ошибка.");
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
