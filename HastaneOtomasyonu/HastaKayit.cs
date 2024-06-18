using BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace HastaneOtomasyonu
{
    public partial class HastaKayit : Form
    {
        public BusinessLayer.hastaKayit HastaTablosu = new BusinessLayer.hastaKayit();
        public HastaKayit()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SekreterIslemleri anasayfa = new SekreterIslemleri();

            // Form1'i göster
            anasayfa.Show();

            // Form2'yi kapat
            this.Close();
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
            textBox5.Clear();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            try
            {
               
                HastaTablosu.yeni_kayit(textBox1.Text, textBox2.Text, textBox3.Text, dateTimePicker1.Value, comboBox1.Text, maskedTextBox1.Text, textBox4.Text, textBox5.Text);
                MessageBox.Show("Başarıyla Eklendi");
                button3_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message);
            }
            temizle();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            hastaKayit veritabaniIslemleri = new hastaKayit();
            dataGridView1.DataSource = veritabaniIslemleri.Listele();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
             if (e.RowIndex >= 0)
                {
                    DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                    textBox1.Text = selectedRow.Cells[0].Value.ToString();
                    textBox2.Text = selectedRow.Cells[1].Value.ToString();
                    textBox3.Text = selectedRow.Cells[2].Value.ToString();
                    dateTimePicker1.Text = selectedRow.Cells[3].Value.ToString();
                    comboBox1.Text = selectedRow.Cells[4].Value.ToString();
                    maskedTextBox1.Text = selectedRow.Cells[5].Value.ToString();
                    textBox4.Text = selectedRow.Cells[6].Value.ToString();
                    textBox5.Text= selectedRow.Cells[7].Value.ToString();
                    

                }
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // TextBox'ta 11 karakterden fazla metin varsa
            if (textBox1.TextLength > 11)
            {
                // Kullanıcıya uygun bir uyarı verin
                MessageBox.Show("Lütfen 11 karakterden fazla olmayan bir tam sayı giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                // TextBox içeriğini temizleyin
                textBox1.Clear();
            }
           
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                hastaKayit HastaTablosu = new hastaKayit();
                // Ekle metodunun çağrılması
                HastaTablosu.guncelle(textBox1.Text, textBox2.Text, textBox3.Text, dateTimePicker1.Value, comboBox1.Text, maskedTextBox1.Text, textBox4.Text, textBox5.Text);
                MessageBox.Show("Başarıyla Güncellendi");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message);
            }

            // Metodun çağrılmasından sonra kontrol temizleniyor
            temizle();
        }
    }
}
