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

namespace HastaneOtomasyonu
{
    public partial class SekreterKayit : Form
    {
        public SekreterKayit()
        {
            InitializeComponent();
        }

       
        public BusinessLayer.sekreterKayit SekreterTablosu = new BusinessLayer.sekreterKayit();
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                sekreterKayit SekreterTablosu = new sekreterKayit();
                // Ekle metodunun çağrılması
                SekreterTablosu.ekle(textBox1.Text, textBox2.Text, textBox3.Text, dateTimePicker1.Value, comboBox1.Text, maskedTextBox1.Text,textBox4.Text);
                MessageBox.Show("Başarıyla Eklendi");

                sekreterKayit veritabaniIslemleri = new sekreterKayit();//eklendikten sonra dataGridView de görünmesi için işlem sonrası çağırma
                dataGridView1.DataSource = veritabaniIslemleri.Listele();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message);
            }

            // Metodun çağrılmasından sonra kontrol temizleniyor
            temizle();
        }
        private void temizle()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            // DateTimePicker kontrolünün değeri varsayılan olarak zaten günümüz tarihi olacak
            comboBox1.ResetText();
            maskedTextBox1.Clear();
            dateTimePicker1.ResetText();
            textBox4.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            sekreterKayit veritabaniIslemleri = new sekreterKayit();
            dataGridView1.DataSource = veritabaniIslemleri.Listele();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                sekreterKayit SekreterTablosu = new sekreterKayit();
                // Ekle metodunun çağrılması
                SekreterTablosu.guncelle(textBox1.Text, textBox2.Text, textBox3.Text, dateTimePicker1.Value, comboBox1.Text, maskedTextBox1.Text, textBox4.Text);
                MessageBox.Show("Başarıyla Güncellendi");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message);
            }

            // Metodun çağrılmasından sonra kontrol temizleniyor
            temizle();
        }

        private void button3_Click(object sender, EventArgs e)
        {
                try
                {
                    sekreterKayit SekreterTablosu = new sekreterKayit();
                    // Ekle metodunun çağrılması
                    SekreterTablosu.sil(textBox1.Text);
                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata oluştu: " + ex.Message);
                }
            button4_Click(sender, e);
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SekreterIslemleri anasayfa = new SekreterIslemleri();

            // Form1'i göster
            anasayfa.Show();

            // Form2'yi kapat
            this.Close();
        }

        

        private void SekreterKayit_Load(object sender, EventArgs e)
        {

        }
    }
}
