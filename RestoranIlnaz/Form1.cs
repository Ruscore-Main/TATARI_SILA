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
 textBox1 - логин
textBox2 - пароль
button1 - ввойти
linkLabel1 - назад
 */
namespace RestoranIlnaz
{
    public partial class Form1 : Form
    {
        Model1Container _db = new Model1Container();    
        public Form1()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form2 form2 = new Form2(_db, this);
            this.Hide();
            form2.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Employee employee = _db.EmployeeSet.FirstOrDefault(el => el.Login == textBox1.Text && el.Password == textBox2.Text);

            if (employee != null)
            {
                Form3 form3 = new Form3(_db, this, employee);
                this.Hide();
                form3.Show();
            }
            else
            {
                MessageBox.Show("Сотрудник не найден!");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (_db.StorageSet.Count() == 0)
            {
                Storage storage1 = new Storage()
                {
                    Address = "г. Казань, ул. Николая Ершова, д. 20",
                    Name = "Склад Казани"
                };

                Storage storage2 = new Storage()
                {
                    Address = "г. Дебессы, ул. Советская, д. 15",
                    Name = "Склад Удмуртии"
                };

                _db.StorageSet.Add(storage1);
                _db.StorageSet.Add(storage2);

                _db.SaveChanges();
            }
        }
    }
}
