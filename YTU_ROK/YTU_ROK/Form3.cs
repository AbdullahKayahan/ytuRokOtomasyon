using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
using System.Data.OleDb;
using System.IO;
using System.Xml;

namespace YTU_ROK
{
    public partial class E_mail : Form
    {
        public E_mail()
        {
            InitializeComponent();
        }
        string DosyaYolu;
        string DosyaAdi;
        bool hata_kont=false;
        private void btnSend_Click(object sender, EventArgs e)
        {
            string deger;
            string[] parcalar;

            parcalar = txtSender.Text.Split('@');//Belli Bir Karakterden Sonrasını Alma
            deger = parcalar[1];//Karakterden Sonraki Bölüm
           //MessageBox.Show(deger);
            kontrol();
            if (hata_kont == false)
            {
                if (deger == "hotmail.com")
                {
                   
                    SmtpClient sc = new SmtpClient();

                    sc.Port = 587;

                    sc.Host = "smtp.live.com";

                    sc.EnableSsl = true;


                    sc.Credentials = new NetworkCredential(txtSender.Text, txtPassword.Text);
                    MailMessage mail = new MailMessage();

                    mail.From = new MailAddress(txtSender.Text, "YTÜROK");

                    mail.To.Add(txtTo.Text);
                    mail.Subject = txtSubject.Text; 
                    mail.IsBodyHtml = true; 
                    mail.Body = txtBody.Text;

                   if( textBox3.Text!="")
                   {
                    mail.Attachments.Add(new Attachment(@textBox3.Text));
                   }
                   else { }

                   try
                   {
                       MessageBox.Show("Mesajınız Gönderiliyor");
                       sc.Send(mail);
                       MessageBox.Show("E_Mail Başarı İle Gönderilmiştir");
                   }
                   catch (Exception ep)
                   {
                       MessageBox.Show("Bir Hata Oluştu: " + ep.Message.ToString());
                   }

                }
                else if (deger == "gmail.com")
                {

                     SmtpClient sc = new SmtpClient();

                       sc.Port = 587;

                        sc.Host = "smtp.gmail.com";

                        sc.EnableSsl = true;

                   
                        sc.Credentials = new NetworkCredential(txtSender.Text, txtPassword.Text);
                        MailMessage mail = new MailMessage();

                        mail.From = new MailAddress(txtSender.Text, "YTÜROK");

                         mail.To.Add(txtTo.Text);
                         mail.Subject = txtSubject.Text; mail.IsBodyHtml = true; mail.Body = txtBody.Text;
                         if (textBox3.Text != "")
                         {
                             mail.Attachments.Add(new Attachment(@textBox3.Text));
                         }
                         else { }
                    try
                    {
                        MessageBox.Show("Mesajınız Gönderiliyor");
                        sc.Send(mail);
                        MessageBox.Show("E_Mail Başarı İle Gönderilmiştir");
                    }
                    catch (Exception ep)
                    {
                        MessageBox.Show("Bir Hata Oluştu: " + ep.Message.ToString());
                       // MessageBox.Show();
                    }
                }
                else
                {
                    MessageBox.Show("E_Mail Adresinizi Kontrol Ediniz");
                }

            }
            else 
            { 
                MessageBox.Show("Lütfen Bilgilerinizi Kontrol Edip Tekrar Deneyiniz");
                hata_kont = false;
            }
           

         }
        public void kontrol()
        {
            if (txtTo.Text == "" || txtBody.Text == "")
            {
                if(txtTo.Text=="")
                {
                 MessageBox.Show("Lütfen Göndereceğiniz Mail Adresinizi Yazınız");
                }
                else if (txtBody.Text == "")
                {
                  MessageBox.Show("Lütfen Göndereceğiniz Mailinizi Yazınız");
                }
                hata_kont = true;
            }
               
        }
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            //file.Filter = "Excel Dosyası |*.xlsx| Excel Dosyası|*.xls";  
            file.FilterIndex = 2;
            file.RestoreDirectory = true;
            file.CheckFileExists = false;
            //file.Title = "Excel Dosyası Seçiniz..";
            file.Multiselect = true;

            if (file.ShowDialog() == DialogResult.OK)
            {
                DosyaYolu = file.FileName;
                MessageBox.Show(DosyaYolu);
               DosyaAdi = file.SafeFileName;
               textBox3.Text = DosyaYolu;
            }  
        }
        OleDbConnection conn;//bağlatı için değişken tanımlandı
        OleDbCommand com;// sorgu cümleleri için " 
        int son_deger, i;
       
   

        private void E_mail_Load(object sender, EventArgs e)
        {
            conn = new OleDbConnection("Provider=Microsoft.jet.oledb.4.0;data source=c:\\rok.mdb");
            //bağlantı cümlesi access için 
           //txtBody.LoadFile("rok.rtf");
        }
        private void son_getir()
        {
            conn.Open();
            DataSet dtst1 = new DataSet();
            OleDbDataAdapter adtr1 = new OleDbDataAdapter("select id From uye order by id desc ", conn);
            adtr1.Fill(dtst1, "uye");

            textBox1.Text = ""; ;//temizleme yapılıyor
            textBox1.DataBindings.Add("text", dtst1, "uye.id");//tablonun özet alanı text1 e aktarılıyor
            textBox1.DataBindings.Clear();
           
            adtr1.Dispose();
            conn.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
        }

        private void E_mail_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form ac = new ANA();
            this.Hide();
            ac.Show();
     
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                txtTo.Clear();
                son_getir();
                son_deger = Convert.ToInt32(textBox1.Text);
                for (i = 1; i <= son_deger; i++)
                {
                    

                    conn.Open();
                    DataSet dtst3 = new DataSet();
                    OleDbDataAdapter adtr3 = new OleDbDataAdapter("select id From uye  where id=" + i + "", conn);
                    adtr3.Fill(dtst3, "uye");


                    textBox3.DataBindings.Add("text", dtst3, "uye.id");//tablonun özet alanı text1 e aktarılıyor

                    if (textBox3.Text == "")
                    {
                    }
                    else
                    {
                        DataSet dtst2 = new DataSet();
                        OleDbDataAdapter adtr2 = new OleDbDataAdapter("select E_mail From uye  where id=" + i + "", conn);
                        adtr2.Fill(dtst2, "uye");


                        textBox2.DataBindings.Add("text", dtst2, "uye.E_mail");//tablonun özet alanı text1 e aktarılıyor

                        adtr2.Dispose();
                       
                        if (txtTo.Text == "")
                        {
                            txtTo.Text = textBox2.Text;
                        }
                        else
                        {
                            txtTo.Text = txtTo.Text + ";" + textBox2.Text;
                        }
                    }
                    adtr3.Dispose();
                    conn.Close();
                    textBox2.DataBindings.Clear();
                    textBox3.DataBindings.Clear();
                    textBox3.Clear();
                }

            }

            else if (comboBox1.SelectedIndex == 2)
            {
                string Mail_deger;
                string[] parcalarmail;

                txtTo.Clear();
                son_getir();
                son_deger = Convert.ToInt32(textBox1.Text);
                for (i = 1; i <= son_deger; i++)
                {

                    conn.Open();
                    DataSet dtst3 = new DataSet();
                    OleDbDataAdapter adtr3 = new OleDbDataAdapter("select id From uye  where id=" + i + " AND Mail_Grp='İdari Kurul'", conn);
                    adtr3.Fill(dtst3, "uye");


                    textBox3.DataBindings.Add("text", dtst3, "uye.id");//tablonun özet alanı text1 e aktarılıyor
                    if (textBox3.Text == "")
                    {
                    }
                    else
                    {
                        DataSet dtst2 = new DataSet();
                        OleDbDataAdapter adtr2 = new OleDbDataAdapter("select E_mail From uye  where id=" + i + " AND Mail_Grp='İdari Kurul'", conn);
                        adtr2.Fill(dtst2, "uye");


                        textBox2.DataBindings.Add("text", dtst2, "uye.E_mail");//tablonun özet alanı text1 e aktarılıyor
                        adtr2.Dispose();


                        if (txtTo.Text == "")
                        {
                            txtTo.Text = textBox2.Text;
                        }
                        else
                        {


                            parcalarmail = txtTo.Text.Split(';', ';');//Belli Bir Karakterden Sonrasını Alma
                            Mail_deger = parcalarmail[0];//Karakterden Sonraki Bölüm

                            if (Mail_deger == textBox2.Text)
                            {
                            }
                            else
                            {
                                txtTo.Text = txtTo.Text + ";" + textBox2.Text;
                            }

                        }
                    }
                    adtr3.Dispose();
                    conn.Close();
                    textBox2.DataBindings.Clear();
                    textBox3.DataBindings.Clear();
                    textBox3.Clear();
                }
            }
                else if(comboBox1.SelectedIndex==1)
                {
                 string Mail_deger;
            string[] parcalarmail;

            txtTo.Clear();
            son_getir();
            son_deger = Convert.ToInt32(textBox1.Text);
            for (i = 1; i <= son_deger; i++)
            {

                conn.Open();
                DataSet dtst3 = new DataSet();
                OleDbDataAdapter adtr3 = new OleDbDataAdapter("select id From uye  where id=" + i + " AND Mail_Grp='Yönetim Kurulu'", conn);
                adtr3.Fill(dtst3, "uye");


                textBox3.DataBindings.Add("text", dtst3, "uye.id");//tablonun özet alanı text1 e aktarılıyor
                if (textBox3.Text == "")
                {
                }
                else
                {
                    DataSet dtst2 = new DataSet();
                    OleDbDataAdapter adtr2 = new OleDbDataAdapter("select E_mail From uye  where id=" + i + " AND Mail_Grp='Yönetim Kurulu'", conn);
                    adtr2.Fill(dtst2, "uye");


                    textBox2.DataBindings.Add("text", dtst2, "uye.E_mail");//tablonun özet alanı text1 e aktarılıyor
                    adtr2.Dispose();
                
               
                if (txtTo.Text == "")
                {
                    txtTo.Text = textBox2.Text;
                }
                else
                {


                    parcalarmail = txtTo.Text.Split(';',';');//Belli Bir Karakterden Sonrasını Alma
                    Mail_deger = parcalarmail[0];//Karakterden Sonraki Bölüm

                    if (Mail_deger == textBox2.Text)
                    {
                    }
                    else
                    { 
                    txtTo.Text = txtTo.Text + ";" + textBox2.Text;
                    }
                    
                }
                }
                adtr3.Dispose();
                conn.Close();
                textBox2.DataBindings.Clear();
                textBox3.DataBindings.Clear();
                textBox3.Clear();
            }
                }
            else if (comboBox1.SelectedIndex == 3)
            {
                string Mail_deger;
                string[] parcalarmail;

                txtTo.Clear();
                son_getir();
                son_deger = Convert.ToInt32(textBox1.Text);
                for (i = 1; i <= son_deger; i++)
                {

                    conn.Open();
                    DataSet dtst3 = new DataSet();
                    OleDbDataAdapter adtr3 = new OleDbDataAdapter("select id From uye  where id=" + i + " AND Mail_Grp='Üye'", conn);
                    adtr3.Fill(dtst3, "uye");


                    textBox3.DataBindings.Add("text", dtst3, "uye.id");//tablonun özet alanı text1 e aktarılıyor
                    if (textBox3.Text == "")
                    {
                    }
                    else
                    {
                        DataSet dtst2 = new DataSet();
                        OleDbDataAdapter adtr2 = new OleDbDataAdapter("select E_mail From uye  where id=" + i + " AND Mail_Grp='Üye'", conn);
                        adtr2.Fill(dtst2, "uye");


                        textBox2.DataBindings.Add("text", dtst2, "uye.E_mail");//tablonun özet alanı text1 e aktarılıyor
                        adtr2.Dispose();


                        if (txtTo.Text == "")
                        {
                            txtTo.Text = textBox2.Text;
                        }
                        else
                        {


                            parcalarmail = txtTo.Text.Split(';', ';');//Belli Bir Karakterden Sonrasını Alma
                            Mail_deger = parcalarmail[0];//Karakterden Sonraki Bölüm

                            if (Mail_deger == textBox2.Text)
                            {
                            }
                            else
                            {
                                txtTo.Text = txtTo.Text + ";" + textBox2.Text;
                            }

                        }
                    }
                    adtr3.Dispose();
                    conn.Close();
                    textBox2.DataBindings.Clear();
                    textBox3.DataBindings.Clear();
                    textBox3.Clear();
                }
            }
            }
        }
        }
    

