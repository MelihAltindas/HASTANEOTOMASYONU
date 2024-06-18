using BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HastaneOtomasyonu
{
    public partial class RandevuKayit : Form
    {
        public RandevuKayit()
        {
            InitializeComponent();
        }
        public BusinessLayer.randevuKayıt RandevuTablosu = new BusinessLayer.randevuKayıt();
        private void temizle()
        {
            textBox1.Clear();
            textBox2.Clear();
            comboBox1.ResetText();
            dateTimePicker1.ResetText();
            comboBox2.ResetText();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool kayıt;
            randevuKayıt RandevuTablosu = new randevuKayıt();
            kayıt =RandevuTablosu.RandevuEkle(textBox1.Text, textBox2.Text, comboBox1.Text, dateTimePicker1.Value, checkedListBox1.SelectedItem.ToString(), comboBox2.Text);
            if (kayıt)
            {
                MessageBox.Show("Randevu başarıyla oluşturuldu.");
            }
            else
            {
                MessageBox.Show("Seçilen saatte doktorun randevusu bulunmaktadır. Lütfen başka bir saat seçiniz.");
            };
            randevuKayıt veritabaniIslemleri = new randevuKayıt();
            dataGridView1.DataSource = veritabaniIslemleri.Listele();

            // Metodun çağrılmasından sonra kontrol temizleniyor
            temizle();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem != null)
            {
                string selectedDepartment = comboBox2.SelectedItem.ToString();
                List<string> doctorIDs = RandevuTablosu.GetDoctorsByDepartment(selectedDepartment);
                comboBox1.Items.Clear(); // Her seferinde mevcut doktor kimliklerini temizle
                foreach (string doctorID in doctorIDs)
                {
                    comboBox1.Items.Add(doctorID);
                }
            
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

            // TextBox'ta 11 karakterden fazla metin varsa
            if (textBox1.TextLength > 11)
            {
                // Kullanıcıya uygun bir uyarı verin
                MessageBox.Show("Lütfen 11 karakterden fazla olmayan bir tam sayı giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                // TextBox içeriğini temizleyin
                textBox1.Clear();
            }

            label7.Text = RandevuTablosu.HastaBilgileri(textBox2.Text);

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                    textBox1.Text = selectedRow.Cells[0].Value.ToString();
                    textBox2.Text = selectedRow.Cells[1].Value.ToString();
                    comboBox1.Text = selectedRow.Cells[2].Value.ToString();
                    dateTimePicker1.Text = selectedRow.Cells[3].Value.ToString();    
                }
            
        }
        private void button2_Click(object sender, EventArgs e)
        {
            SekreterIslemleri anasayfa = new SekreterIslemleri();

            // Form1'i göster
            anasayfa.Show();

            // Form2'yi kapat
            this.Close();
        }
        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.CurrentValue == CheckState.Checked)
            {
                checkedListBox1.ItemCheck -= checkedListBox1_ItemCheck;
                checkedListBox1.SetItemChecked(e.Index, false);
                checkedListBox1.ItemCheck += checkedListBox1_ItemCheck;
            }
            // Eğer yeni bir öğe seçiliyorsa, diğer öğelerin işaretlerini kaldır
            else
            {
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    if (i != e.Index)
                    {
                        checkedListBox1.SetItemChecked(i, false);
                    }
                }
            }
        }

        private void RandevuKayit_Load(object sender, EventArgs e)
        {

        }
    }
}
