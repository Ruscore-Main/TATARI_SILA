using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
/*
 textBox1 - Логин
textBox2 - Фио
textBox3 - Пароль
comboBox1 - ресторан
comboBox2 - должность
button1 - зарегестрировать
 */
namespace RestoranIlnaz
{
    public partial class Form2 : Form
    {
        Form1 form1;
        Model1Container _db;
        public Form2(Model1Container db, Form1 backForm)
        {
            InitializeComponent();
            this.form1 = backForm;
            this._db = db;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string resValidation = ValidateTest.ValidateRegistration(textBox1.Text, textBox3.Text, textBox2.Text);
            if (resValidation == "Успешно")
            {
                Employee employee = new Employee()
                {
                    Login = textBox1.Text,
                    FIO = textBox2.Text,
                    Password = textBox3.Text,
                    Storage = comboBox1.SelectedItem as Storage,
                    Post = Convert.ToString(comboBox2.SelectedItem)
                };

                _db.EmployeeSet.Add(employee);
                _db.SaveChanges();
                MessageBox.Show("Успешная регистрация!");
                this.Close();
            }
            else
            {
                MessageBox.Show(resValidation);

            }

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            comboBox2.Items.Add("Директор");
            comboBox2.Items.Add("Заведующй складом");


            foreach (Storage storage in _db.StorageSet)
            {
                comboBox1.Items.Add(storage);
            }
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            form1.Show();
        }
    }
}
