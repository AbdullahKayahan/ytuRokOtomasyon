using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;
using System.Xml;
namespace YTU_ROK
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
        OleDbConnection conn;//bağlatı için değişken tanımlandı
        OleDbCommand com;// sorgu cümleleri için " 
        private void Form4_Load(object sender, EventArgs e)
        {
            conn = new OleDbConnection("Provider=Microsoft.jet.oledb.4.0;data source=c:\\rok.mdb");

        }
        private void button1_Click(object sender, EventArgs e)
        {
            conn.Open();
            DataSet dtst1 = new DataSet();
            OleDbDataAdapter adtr1 = new OleDbDataAdapter("select * From sifre where k_adi='" + textBox1.Text + "' ", conn);
            adtr1.Fill(dtst1, "sifre");

            textBox3.Text = ""; ;//temizleme yapılıyor
            textBox3.DataBindings.Add("text", dtst1, "sifre.k_adi");//tablonun özet alanı text1 e aktarılıyor
            textBox3.DataBindings.Clear();

            textBox4.Text = ""; ;//temizleme yapılıyor
            textBox4.DataBindings.Add("text", dtst1, "sifre.sifre");//tablonun özet alanı text1 e aktarılıyor
            textBox4.DataBindings.Clear();

            textBox5.Text = ""; ;//temizleme yapılıyor
            textBox5.DataBindings.Add("text", dtst1, "sifre.rütbe");//tablonun özet alanı text1 e aktarılıyor
            textBox5.DataBindings.Clear();
            adtr1.Dispose();
            conn.Close();


            if (textBox1.Text == textBox3.Text & textBox2.Text == textBox4.Text & textBox5.Text == "admin")
            {
                MessageBox.Show("Başarılı Giriş Yapıldı", "Hoşgeldiniz", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Form ac = new ANA();
                ac.Show();
                this.Hide();
            }
            else if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Lütfen Gerekli Alanları Doldurun", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else if (textBox3.Text == "")
            {
                MessageBox.Show("Kullanıcı Adınız Hatalı", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else if (textBox2.Text != textBox4.Text)
            {
                MessageBox.Show("Şifreniz Hatalı", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else if (textBox5.Text != "admin")
            {
                MessageBox.Show("Giriş İçin Uygun Kullanıcı Değilsiniz", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
          
        }
    }
}
