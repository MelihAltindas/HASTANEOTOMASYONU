using BusinessLayer;
using EntitiyLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static HastaneOtomasyonu.GirisSayfa;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using iTextSharp.text;
using iTextSharp.text.pdf;
using ZedGraph;

namespace HastaneOtomasyonu
{
    public partial class DoktorIslemleri : Form
    {
        public DoktorIslemleri()
        {
            InitializeComponent();

        }
        public BusinessLayer.doktorIslemleri DoktorTablosu = new BusinessLayer.doktorIslemleri();
        public BusinessLayer.doktorIslemleri MuayeneTablosu = new BusinessLayer.doktorIslemleri();
        public BusinessLayer.Mailbusiness mail = new BusinessLayer.Mailbusiness();
        public BusinessLayer.doktorIslemleri RandevuTablosu = new BusinessLayer.doktorIslemleri();

        private void DoktorIslemleri_Load(object sender, EventArgs e)
        {
            string doktorKimlik = GlobalVariables.DoktorKimlik;

            // Doktor adı ve soyadını getir
            var doktorBilgileri = DoktorTablosu.GetDoktorAdSoyad(doktorKimlik);

            // Label'lara doktor adı ve soyadını yaz
            label8.Text = doktorBilgileri.Item1;
            label1.Text = doktorBilgileri.Item2 + " " + doktorBilgileri.Item3;

        }
        private void CikisYap()
        {
            // Kullanıcıyı tekrar giriş sayfasına yönlendir
            GirisSayfa girisFormu = new GirisSayfa();
            girisFormu.Show();

            // Mevcut formu kapat
            this.Close();
        }
        private void temizle()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            richTextBox1.Clear();

        }
        //seçilen satırı yerlerine yerleştirme
        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                textBox1.Text = selectedRow.Cells[0].Value.ToString();
                textBox2.Text = selectedRow.Cells[2].Value.ToString();
                richTextBox1.Text = selectedRow.Cells[3].Value.ToString();
                textBox3.Text = selectedRow.Cells[4].Value.ToString();

            }
        }
        //ekleme işlemini gerçekleştirme
        private void button4_Click_1(object sender, EventArgs e)
        {
            try
            {
                doktorIslemleri MuayeneTablosu = new doktorIslemleri();
                // Ekle metodunun çağrılması
                MuayeneTablosu.kaydet(textBox1.Text, label8.Text, textBox2.Text, richTextBox1.Text, textBox3.Text);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message);
            }
        }

        //listeleme işlemini gerçekleştirme
        private void button5_Click_1(object sender, EventArgs e)
        {

            doktorIslemleri veritabaniIslemleri = new doktorIslemleri();
            dataGridView1.DataSource = veritabaniIslemleri.Listele();
        }

        //güncelleme işlemini gerçekleştirme
        private void button6_Click_1(object sender, EventArgs e)
        {
            try
            {
                doktorIslemleri MuayeneTablosu = new doktorIslemleri();
                // Ekle metodunun çağrılması
                MuayeneTablosu.guncelle(textBox1.Text,label8.Text,textBox2.Text, richTextBox1.Text, textBox3.Text);
                MessageBox.Show("Başarıyla Güncellendi");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message);
            }

            // Metodun çağrılmasından sonra kontrol temizleniyor
            temizle();
        }
        //hastabilgisini getirme
        private void button1_Click(object sender, EventArgs e)
        {
            string hastaKimlik = textBox1.Text;
            label5.Text = DoktorTablosu.HastaBilgisi(textBox1.Text);
        }
        //anaforma dönüş
        private void button3_Click(object sender, EventArgs e)
        {
            CikisYap();
        }
        
        //mail gönderme butonu
        private void button7_Click(object sender, EventArgs e)
        {
            List<string> liste = new List<string>();
            try
            {
                string gorus = $"Tanı: \n{textBox2.Text}\n\nİlaçlar: \n{textBox3.Text}\n\nGörüş ve Tedavi: \n{richTextBox1.Text}\n\nDoktor Adı Soyadı: \n{label1.Text}";
                mail.SendMail(liste, textBox1.Text, "Özel Altın Hastanesi", gorus);
                MessageBox.Show("Mail Gönderildi");
            }
            catch
            {
                MessageBox.Show("Hata");

            }
        }

        //pdf işlemi
        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                // Font tanımlaması yapın
                BaseFont font = BaseFont.CreateFont("c:\\windows\\fonts\\arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                iTextSharp.text.Font turkishFont = new iTextSharp.text.Font(font, 11, iTextSharp.text.Font.NORMAL);

                // Seçilen hastanın bilgilerini alın
                string hastaBilgileri = $"{label5.Text}";
                string birlesikMetin = $"Tanı: {textBox2.Text}" + Environment.NewLine + $"İlaçlar:{textBox3.Text}" + Environment.NewLine + $"Doktor Görüşleri ve Tedavi: {richTextBox1.Text}";

                // PDF tablosu oluşturun
                PdfPTable pdfTablosu = new PdfPTable(1); // Sadece bir sütun var
                pdfTablosu.DefaultCell.Padding = 10;
                pdfTablosu.WidthPercentage = 100;
                pdfTablosu.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfTablosu.DefaultCell.BorderWidth = 2;

                // Hastanın bilgilerini PDF'e ekleyin
                PdfPCell hastaBilgisiHucresi = new PdfPCell(new Phrase("Hasta Bilgileri", turkishFont));
                pdfTablosu.AddCell(hastaBilgisiHucresi);
                PdfPCell hastaBilgisiIcerikHucresi = new PdfPCell(new Phrase(hastaBilgileri, turkishFont));
                pdfTablosu.AddCell(hastaBilgisiIcerikHucresi);

                // RichTextBox'lerdeki verileri PDF'e ekleyin              
                PdfPCell birlesikMetinIcerikHucresi = new PdfPCell(new Phrase(birlesikMetin, turkishFont));
                pdfTablosu.AddCell(birlesikMetinIcerikHucresi);

                // PDF dosyasını kaydedin
                SaveFileDialog pdfkaydetme = new SaveFileDialog();
                pdfkaydetme.Filter = "PDF Dosyaları|*.pdf";
                pdfkaydetme.Title = "PDF Olarak Kaydet";
                if (pdfkaydetme.ShowDialog() == DialogResult.OK)
                {
                    using (FileStream stream = new FileStream(pdfkaydetme.FileName, FileMode.Create))
                    {
                        iTextSharp.text.Document pdfDosya = new iTextSharp.text.Document(PageSize.A4, 5f, 5f, 11f, 10f);
                        iTextSharp.text.pdf.PdfWriter.GetInstance(pdfDosya, stream);
                        pdfDosya.Open();
                        pdfDosya.Add(pdfTablosu);
                        pdfDosya.Close();
                        stream.Close();
                        MessageBox.Show("PDF dosyası başarıyla oluşturuldu!\n" + "Dosya Konumu: " + pdfkaydetme.FileName, "İşlem Tamam");
                    }
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message);
            }
        }
        private void RandevulariGoster()
        {
            try
            {
                // Label içeriğini al
                string doktorad = label1.Text;

                // İş katmanı sınıfını oluştur
                DoktorIslemleri doktorIslemleri = new DoktorIslemleri();

                // Gelecek tarihli randevuları al
                List<RandevuTablosu> randevular = RandevuTablosu.GetGelecekTarihliRandevular(doktorad);

                // DataGridView'e verileri yükle
                dataGridView2.DataSource = randevular;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }
 
        private void DrawBarGraph(Dictionary<string, int> data)
        {

            GraphPane graphPane = zedGraphControl1.GraphPane;
            graphPane.Title.Text = "Hastalık Bölümlerine Göre Hasta Sayısı";
            graphPane.XAxis.Title.Text = "Hastalık Bölümü";
            graphPane.YAxis.Title.Text = "Hasta Sayısı";

            // Veri serisi oluştur
            string[] labels = data.Keys.ToArray();
            double[] values = data.Values.Select(x => (double)x).ToArray();


            // Sütunların konumunu ve genişliğini belirleyin
            double[] positions = new double[data.Count];
            for (int i = 0; i < data.Count; i++)
            {
                positions[i] = i + 1; // Sütunun x konumu
            }

            // Sütunları oluşturun
            BarItem bar = graphPane.AddBar("Hasta Sayısı", positions, values, Color.DarkSeaGreen);

            BarItem.CreateBarLabels(graphPane, false, "F0");

            // Sütunların genişliğini ayarlayın
            graphPane.BarSettings.ClusterScaleWidth = 0.5;
            graphPane.XAxis.Scale.FontSpec.Size = 10;


            // X ekseninde etiketleri ayarlayın
            graphPane.XAxis.Scale.TextLabels = labels;
            graphPane.XAxis.Type = AxisType.Text;

            // Eksenleri yeniden çiz
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();


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

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                // Label içeriğini al
                string doktorad = label1.Text;

                // İş katmanı sınıfını oluştur
                DoktorIslemleri doktorIslemleri = new DoktorIslemleri();

                // Gelecek tarihli randevuları al
                List<RandevuTablosu> randevular = RandevuTablosu.GetGelecekTarihliRandevular(doktorad);

                // DataGridView'e verileri yükle
                dataGridView2.DataSource = randevular;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }
    }
}
