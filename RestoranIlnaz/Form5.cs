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
textBox3 - количество
pictureBox1 - изображение
button1 - обзор
button2 - Добавить
linkLbalel1 - назад
 */
namespace RestoranIlnaz
{
    public partial class Form5 : Form
    {
        Form4 form4;
        Model1Container _db;
        Storage currentStorage;
        byte[] imageBytes;

        public Form5(Model1Container db, Form4 backForm, Storage storage)
        {
            InitializeComponent();
            this._db = db;
            this.form4 = backForm;
            this.currentStorage = storage;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Product newProduct = new Product()
                {
                    Name = textBox1.Text,
                    Price = Convert.ToInt32(textBox2.Text),
                    Amount = Convert.ToInt32(textBox3.Text),
                    Image = this.imageBytes,
                    Storage = currentStorage
                };
                _db.ProductSet.Add(newProduct);
                _db.SaveChanges();
                form4.UpdateList();
                MessageBox.Show("Успешно добавлено!");
                this.Close();
            }
            catch
            {
                MessageBox.Show("Неправильный формат данных!");
            }
        }

        private void Form5_FormClosing(object sender, FormClosingEventArgs e)
        {
            form4.Show();
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
                }
                catch
                {
                    MessageBox.Show("Ошибка");
                }
            }
        }
    }
}
