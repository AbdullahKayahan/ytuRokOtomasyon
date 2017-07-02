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
    public partial class UYE : Form
    {
        public UYE()
        {
            InitializeComponent();

        }
        OleDbConnection conn;//bağlatı için değişken tanımlandı
        OleDbCommand com;// sorgu cümleleri için " 
        int son_id_degeri;
        bool DG1_kont = false;//datagridin hangi sayfada olduğunu kntrol eder
        bool mail_kont = false;//mail adresi kontolı
        bool genel_kont = false;//boş text kontolu

        private void UYE_Load(object sender, EventArgs e)
        {
            conn = new OleDbConnection("Provider=Microsoft.jet.oledb.4.0;data source=c:\\rok.mdb");
           // conn = new OleDbConnection(" Provider=MS Remote; Remote Server=http://www.kelimecalisma.com/; Remote Provider=Microsoft.Jet.OLEDB.4.0; DataSource=yturok/rok.mdb");
           // conn = new OleDbConnection(" Provider=MS Remote;Remote Provider=Microsoft.Jet.OLEDB.4.0;Remote Server=http://kelimecalisma.com;Data Source=httpdocs/yturok/rok.mdb");
          //  Provider=MS Remote;Remote Provider=Microsoft.Jet.OLEDB.4.0; Remote Server=http://server.adress.com;Data Source=d:\myPath\myDatabase.mdf;

            uye_getir();
            son_getir();
        }

        private void son_getir()//Veri tabanındaki son üyenin id sini getirir
        {
            conn.Open();
            DataSet dtst1 = new DataSet();
            OleDbDataAdapter adtr1 = new OleDbDataAdapter("select id From uye order by id desc ", conn);
            adtr1.Fill(dtst1, "uye");

            textBox1.Text = ""; ;//temizleme yapılıyor
            textBox1.DataBindings.Add("text", dtst1, "uye.id");//tablonun özet alanı text1 e aktarılıyor
            textBox1.DataBindings.Clear();
            son_id_degeri = Convert.ToInt32(textBox1.Text);
            son_id_degeri += 1;

            textBox1.Text = son_id_degeri.ToString();//Veri tabanındaki son üyenin id sinine 1 ekler
            adtr1.Dispose();
            conn.Close();
        }

        private void uye_getir()//Veri tabanındaki üyeleri datagride çeker
        {
            conn.Close();
            conn.Open();
            DataSet dtst1 = new DataSet();
            OleDbDataAdapter adtr1 = new OleDbDataAdapter("select * From uye ", conn);
            adtr1.Fill(dtst1, "uye");
            DG1.DataSource = dtst1.Tables["uye"];
            adtr1.Dispose();
            conn.Close();
        }

        private void temizle()//textBoxları temzilemek için kullanılır


        {
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            comboBox1.Text = "GÖREV";

        }
        private void temizle_sil_güncelle()//textBoxları temzilemek için kullanılır
        {
            textBox8.Clear();
            textBox9.Clear();
            textBox10.Clear();
            textBox11.Clear();
            textBox12.Clear();
            textBox13.Clear();
            textBox14.Clear();
            comboBox2.Text = "GÖREV";
            

        }

        private void mail_kontrol()//Mail adresinin sonu gmail veya hotmail mi ona bakar
        {
            string deger;
            string[] parcalar;

            parcalar = textBox7.Text.Split('@');//Belli Bir Karakterden Sonrasını Alma
            deger = parcalar[1];
            //MessageBox.Show(deger);
            if (deger == "hotmail.com" || deger == "gmail.com")
            {
                mail_kont = false;
               
            }
            else
            {
                MessageBox.Show("Mail Adresinizi Kontrol Ediniz.","E_Mail Uyarısı",MessageBoxButtons.OKCancel,MessageBoxIcon.Warning);
                mail_kont = true;
            }
        }

        private void genel_kontrol()//Hiç boş textBox Varmı Onu Kontrol eder
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "" || textBox7.Text == "" || comboBox1.Text == "GÖREV")
            {
               genel_kont = true;
                MessageBox.Show("Hiç Bir Alan Boş Geçilemez", "Alanlar Boş", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
            else { genel_kont = false; mail_kontrol(); }
        }

        private void button1_Click(object sender, EventArgs e) //Kayıt Ekleme
        {
           
            genel_kontrol();

            if (genel_kont == false)//önce boşluk kontrolü
            {
                if (mail_kont == false)//mail adresi kontrolü
                {
                    conn.Close();


                    OleDbCommand com = new OleDbCommand("insert into uye(Uye_no,Makbuz_no,Adi,Soyadi,Bolumu,Telefonu,E_mail,Mail_Grp) values ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + textBox6.Text + "','" + textBox7.Text + "','" +comboBox1.Text+  "')", conn);
                    MessageBox.Show("Kayıt Tamamlandı","Kayıt İşlemi",MessageBoxButtons.OK,MessageBoxIcon.Information);

                    com.Connection.Open();
                    com.ExecuteNonQuery();
                    conn.Close();
                    uye_getir();
                    son_getir();
                    temizle();
                }
                else
                {
                    //mail adresinin elsesi
                }
            }
            else
            { 
                //boş textin elsesi
            }
        }
      
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            label15.Text = "Adı Giriniz:";
           
            textBox15.Enabled = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            label15.Text = "Bölümü Giriniz:";
           
            textBox15.Enabled = true;
        }

        private void DG1_CellClick(object sender, DataGridViewCellEventArgs e)
        //data gride tıklayınca kayıtları texte aktarma
        {
            if (DG1_kont == true)
            {
                textBox8.Text = (DG1.Rows[e.RowIndex].Cells[1].Value.ToString());
                textBox9.Text = (DG1.Rows[e.RowIndex].Cells[2].Value.ToString());
                textBox10.Text = (DG1.Rows[e.RowIndex].Cells[3].Value.ToString());
                textBox11.Text = (DG1.Rows[e.RowIndex].Cells[4].Value.ToString());
                textBox12.Text = (DG1.Rows[e.RowIndex].Cells[5].Value.ToString());
                textBox13.Text = (DG1.Rows[e.RowIndex].Cells[6].Value.ToString());
                textBox14.Text = (DG1.Rows[e.RowIndex].Cells[7].Value.ToString());
                comboBox2.Text = (DG1.Rows[e.RowIndex].Cells[8].Value.ToString());
            }
            else
            {
                MessageBox.Show("Lütfen Bu İşlemi Arama Sekmesinde Yapınız");
            }
        }

        private void button2_Click(object sender, EventArgs e)//VeriTabanı Arama işlemleri
        {
            if (radioButton1.Checked == true)
            {
                conn.Open();
                DataSet dtst1 = new DataSet();
                OleDbDataAdapter adtr1 = new OleDbDataAdapter("select * From uye  where Adi='" + textBox15.Text + "'", conn);
                adtr1.Fill(dtst1, "uye");
                DG1.DataSource = dtst1.Tables["uye"];
                adtr1.Dispose();
                conn.Close();
            }
            else if (radioButton2.Checked == true)
            {
                conn.Open();
                DataSet dtst1 = new DataSet();
                OleDbDataAdapter adtr1 = new OleDbDataAdapter("select * From uye  where Bolumu='" + textBox15.Text + "'", conn);
                adtr1.Fill(dtst1, "uye");
                DG1.DataSource = dtst1.Tables["uye"];
                adtr1.Dispose();
                conn.Close();
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            uye_getir();
        }

        private void textBox15_TextChanged(object sender, EventArgs e)//Anlık arama
        {
            if (radioButton1.Checked == true)
            {
                conn.Open();
                DataSet dtst1 = new DataSet();
                OleDbDataAdapter adtr1 = new OleDbDataAdapter("select * From uye  where Adi like'" + textBox15.Text + "%'", conn);
                adtr1.Fill(dtst1, "uye");
                DG1.DataSource = dtst1.Tables["uye"];
                adtr1.Dispose();
                conn.Close();
            }
            else if (radioButton2.Checked == true)
            {
                conn.Open();
                DataSet dtst1 = new DataSet();
                OleDbDataAdapter adtr1 = new OleDbDataAdapter("select * From uye  where Bolumu like '" + textBox15.Text + "%'", conn);
                adtr1.Fill(dtst1, "uye");
                DG1.DataSource = dtst1.Tables["uye"];
                adtr1.Dispose();
                conn.Close();
            }
        }

        private void tabPage2_Enter(object sender, EventArgs e)//2. sekmeye geçildiğinde
        {
            DG1_kont = true;
        }

        private void tabPage1_Enter(object sender, EventArgs e)//1. sekmeye geçildiğinde
        {
            DG1_kont = false;
            temizle();
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            button3.Text = "SİL";
            textinaktif();
        }
        private void textaktif()
        {
            textBox9.Enabled = true;
            textBox10.Enabled = true;
            textBox11.Enabled = true;
            textBox12.Enabled = true;
            textBox13.Enabled = true;
            textBox14.Enabled = true;
            comboBox2.Enabled = true;

        }
        private void textinaktif()
        {
            textBox9.Enabled = false;
            textBox10.Enabled = false;
            textBox11.Enabled = false;
            textBox12.Enabled = false;
            textBox13.Enabled = false;
            textBox14.Enabled = false;
            comboBox2.Enabled = false;
           
        }
        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            button3.Text = "GÜNCELLE";
            textaktif();   
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (radioButton5.Checked == true)
            {
                try
                {
                    conn.Close();
                    // seçili olan kayıdı siler
                    OleDbCommand sil = new OleDbCommand("delete from uye where Uye_No= '" + textBox8.Text + "' ", conn);
                    sil.Connection.Open();
                    sil.ExecuteNonQuery();
                    conn.Close();                 
                   
                    MessageBox.Show("Silme İşlemi Tamamlandı");
                    uye_getir();
                    radioButton6.Checked = true;
                    temizle_sil_güncelle();
                   // temizle();//temizle alt progmını çalıştırır.

                }
                catch { }
            }
            else if (radioButton4.Checked == true)
            {
                try
                {
                    conn.Close();

                    OleDbCommand gncl = new OleDbCommand("update uye set Makbuz_no= '" + textBox9.Text + "', Adi= '" + textBox10.Text + "',Soyadi= '" + textBox11.Text + "',Bolumu= '" + textBox12.Text + "',Telefonu= '" + textBox13.Text + "',E_mail= '" + textBox14.Text + "',Mail_Grp= '" + comboBox2.Text + "' where Uye_No = '" + textBox8.Text + "'", conn);
                    gncl.Connection.Open();
                    gncl.ExecuteNonQuery();
                    conn.Close();
                 
                    MessageBox.Show("Güncelleme İşlemi Tamamlandı");
                  
                    
                    uye_getir();
                    textinaktif();
                    radioButton6.Checked = true;
                    temizle_sil_güncelle();
                }
                catch { }
            }
        }
     
       
        private void textBox6_KeyPress(object sender, KeyPressEventArgs e) 
            //Error Provider İle Girilen Değer Sayımı Text Mi Kontrolu Yapıyor
        {
            if (!char.IsNumber(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;//Harf Girişini Engelliyor
                errorProvider1.SetError(textBox6, "Sadece Sayı Girişi");
            }
            else
            {
                errorProvider1.Clear();//Doğru sonuç Girilince Uyarıyı Kaldırır }


            }

        }

        private void textBox7_Enter(object sender, EventArgs e)
        //Error Provider İle Girilen Değer 11 Karakter Mi Kontrolu Yapıyor
        {
            if (textBox6.TextLength < 11)
            {
                errorProvider2.SetError(textBox6, "Telefon Alanı 11 Karakter Olmalı");
            }
        }

       public void UYE_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form ac = new ANA();
            this.Hide();
             ac.Show();
          
           
        
        }

       private void radioButton6_CheckedChanged(object sender, EventArgs e)
       {
           button3.Text = "İşlem Seçiniz";
           textinaktif();
       }
    }
}
