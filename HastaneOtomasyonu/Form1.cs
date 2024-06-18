using EntitiyLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLayer;
using DataAccessLayer;

namespace HastaneOtomasyonu
{
    public partial class GirisSayfa : Form
    {
        public GirisSayfa()
        {
            InitializeComponent();
            textBox2.PasswordChar = '*';
        }
        public static class GlobalVariables
        {
            public static string DoktorKimlik { get; set; }
        }
        public BusinessLayer.Giris SekreterTablosu = new BusinessLayer.Giris();
        public BusinessLayer.Giris DoktorTablosu = new BusinessLayer.Giris();
        private void button1_Click(object sender, EventArgs e)
        {
            string Kimlik = textBox1.Text;
            string Sifre = textBox2.Text;

            bool isSekreter = SekreterTablosu.SekreterGiris(Kimlik, Sifre);
            bool isDoktor = DoktorTablosu.DoktorGiris(Kimlik, Sifre);

            if (isSekreter)
            {
                // Sekreter girişi başarılıysa yapılacak işlemler
                this.Hide();
                SekreterIslemleri sekreterForm = new SekreterIslemleri();
                sekreterForm.Show();

            }
            else if (isDoktor)
            {
                GlobalVariables.DoktorKimlik = Kimlik;
                // Doktor girişi başarılıysa yapılacak işlemler
                this.Hide();
                DoktorIslemleri doktorForm = new DoktorIslemleri();
                doktorForm.Show();
            }
            else
            {
                MessageBox.Show("Kullanıcı Numarası veya Şifre Hatalı");
            }




            //string SekreterKimlik = textBox1.Text;
            //string SekreterSifre = textBox2.Text;
            //bool loggedIn = SekreterTablosu.SekreterGiris(SekreterKimlik, SekreterSifre);
            //if (loggedIn)
            //{
              //  MessageBox.Show("Giriş Başarılı");
                // Giriş başarılı ise yapılacak işlemler
                //this.Hide();
            //}

            //else
            //{
              //  MessageBox.Show("Kullanıcı Numarası veya Şifre Hatalı");

            //}
          
        }

        private void GirisSayfa_Load(object sender, EventArgs e)
        {

        }
    }
}
