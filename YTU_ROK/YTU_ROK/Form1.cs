using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace YTU_ROK
{
    public partial class ANA : Form
    {
        public ANA()
        {
            InitializeComponent();
        }
     public bool cıkıs = false;
        private void button1_Click(object sender, EventArgs e)
        {
            Form uye = new UYE();

            this.Dispose();
            uye.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form mail = new E_mail();           
            this.Dispose();
            mail.Show();
        }

        private void ANA_FormClosing(object sender, FormClosingEventArgs e)
        {

        
                if (MessageBox.Show("Çıkmak İstediğinizden Emin misiniz ?", "Çıkış", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }
                else
                { 
                    e.Cancel = false;

                }
           
        }
        private void ANA_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void ANA_Load(object sender, EventArgs e)
        {
          
        }
    }
}
