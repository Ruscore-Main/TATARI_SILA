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
 button1 - склад
linkLabel1 - назад
 */
namespace RestoranIlnaz
{
    public partial class Form3 : Form
    {
        Employee currentEmployee;
        Model1Container _db;
        Form1 form1;
        public Form3(Model1Container db, Form1 backForm, Employee employee)
        {
            InitializeComponent();
            this._db = db;
            this.form1 = backForm;
            this.currentEmployee = employee;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4(_db, this, currentEmployee);
            this.Hide();
            form4.Show();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            label3.Text = currentEmployee.FIO;
            label4.Text = currentEmployee.Login;
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            form1.Show();
        }
    }
}
