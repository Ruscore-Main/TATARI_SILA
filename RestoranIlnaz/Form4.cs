using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
/*
 textBox1 - название
textBox2 - цена
textbox3 - количество
pictureBox1 - изображение
button1 - Обзор
button2 - удалить
button3 - изменить
button4 - добавить
linkLabel1 - назад
listBox1 - склад
 */
namespace RestoranIlnaz
{
    public partial class Form4 : Form
    {
        Employee currentEmployee;
        Model1Container _db;
        Form3 form1;
        Product currentProduct;
        byte[] imageBytes;

        public Form4(Model1Container db, Form3 backForm, Employee employee)
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

        private void button4_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5(_db, this, currentEmployee.Storage);
            this.Hide();
            form5.Show();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            UpdateList();
        }

        public void UpdateList()
        {
            listBox1.Items.Clear();
            foreach (Product product in currentEmployee.Storage.Product)
            {
                listBox1.Items.Add(product);
            }
        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            form1.Show();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                currentProduct = listBox1.SelectedItem as Product;
                if (currentProduct != null)
                {
                    textBox1.Text = currentProduct.Name;
                    textBox2.Text = Convert.ToString(currentProduct.Price);
                    textBox3.Text = Convert.ToString(currentProduct.Amount);

                    pictureBox1.Image = (Image)((new ImageConverter()).ConvertFrom(currentProduct.Image));
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    button1.Enabled = true;
                    button2.Enabled = true;
                    button3.Enabled = true;
                }

            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open_dialog = new OpenFileDialog();

            open_dialog.Filter = "Image Files(*.BMP; *.JPG; *.GIF; *.PNG)| *.BMP; *.JPG; *.GIF; *.PNG | All files(*.*) | *.* ";
            if (open_dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    this.imageBytes = File.ReadAllBytes(open_dialog.FileName);
                    pictureBox1.Image = (Image)((new ImageConverter()).ConvertFrom(imageBytes));
                    currentProduct.Image = imageBytes;
                }
                catch
                {
                    MessageBox.Show("Ошибка");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            currentProduct.Name = textBox1.Text;
            currentProduct.Price = Convert.ToInt32(textBox2.Text);
            currentProduct.Amount = Convert.ToInt32(textBox3.Text);

            _db.SaveChanges();
            UpdateList();
            MessageBox.Show("Успешно изменено!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _db.ProductSet.Remove(currentProduct);
            _db.SaveChanges();
            UpdateList();
            MessageBox.Show("Успешно удалено!");
        }
    }
}
