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



namespace HastaneOtomasyonu
{
    public partial class DoktorKayit: Form
    {
        public BusinessLayer.doktorKayit DoktorTablosu = new BusinessLayer.doktorKayit();
        public DoktorKayit()
        {
            InitializeComponent();
        }

               
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                DoktorTablosu.ekle(textBox1.Text, textBox2.Text, textBox3.Text, dateTimePicker1.Value, comboBox1.Text, comboBox2.Text, maskedTextBox1.Text,textBox4.Text, textBox5.Text,textBox6.Text);
                MessageBox.Show("Başarıyla Eklendi");
                button5_Click(sender, e);


            }
            catch(Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message);
            }
            temizle();
        }
        private void temizle()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            // DateTimePicker kontrolünün değeri varsayılan olarak zaten günümüz tarihi olacak
            comboBox1.ResetText();
            comboBox2.ResetText();
            maskedTextBox1.Clear();
            dateTimePicker1.ResetText();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            doktorKayit veritabaniIslemleri = new doktorKayit();
            dataGridView1.DataSource = veritabaniIslemleri.Listele();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            SekreterIslemleri anasayfa = new SekreterIslemleri();

            // Form1'i göster
            anasayfa.Show();

            // Form2'yi kapat
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                doktorKayit DoktorTablosu = new doktorKayit();
                // Ekle metodunun çağrılması
                DoktorTablosu.guncelle(textBox1.Text, textBox2.Text, textBox3.Text, dateTimePicker1.Value, comboBox1.Text, comboBox2.Text, maskedTextBox1.Text, textBox4.Text, textBox5.Text, textBox6.Text);
                MessageBox.Show("Başarıyla Güncellendi");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message);
            }

            // Metodun çağrılmasından sonra kontrol temizleniyor
            temizle();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                doktorKayit DoktorTablosu = new doktorKayit();
                // Ekle metodunun çağrılması
                DoktorTablosu.sil(textBox1.Text);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message);
            }
            button5_Click(sender, e);
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
                comboBox2.Text = selectedRow.Cells[5].Value.ToString();
                maskedTextBox1.Text = selectedRow.Cells[6].Value.ToString();

                textBox4.Text = selectedRow.Cells[7].Value.ToString();
                textBox5.Text = selectedRow.Cells[8].Value.ToString();
                textBox6.Text = selectedRow.Cells[9].Value.ToString();
            }
        }
    }
}
