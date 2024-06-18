using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessLayer
{
    public class doktorKayit
    {
        public EntitiyLayer.DoktorTablosu BL_Doktor = new EntitiyLayer.DoktorTablosu();

        private DataAccessLayer.VeriTabani baglanti = new DataAccessLayer.VeriTabani();

        public void ekle(string kimlik,string ad, string soyad, DateTime dogum, string cinsiyet,string dep,string tel,string adres,string sifre,string kim)
        {
            OleDbConnection connection = baglanti.ConnectionOpen();
            try
            {
                OleDbCommand komut = new OleDbCommand("INSERT INTO DoktorTablosu (DoktorKimlik,DoktorAd,DoktorSoyad,DoktorDogumTarihi,DoktorCinsiyet,DoktorDepatman,DoktorTelefon,DoktorAdres,DoktorSifre,SekreterKimlik) VALUES (@DoktorKimlik,@DoktorAd,@DoktorSoyad,@DoktorDogumTarihi,@DoktorCinsiyet,@DoktorDepatman,@DoktorTelefon,@DoktorAdres,@DoktorSifre,@SekreterKimlik)" ,connection);
                komut.Parameters.AddWithValue("@DoktorKimlik", kimlik);
                komut.Parameters.AddWithValue("@DoktorAd", ad);
                komut.Parameters.AddWithValue("@DoktorSoyad", soyad);
                komut.Parameters.AddWithValue("@DoktorDogumTarihi", dogum);
                komut.Parameters.AddWithValue("@DoktorCinsiyet", cinsiyet);
                komut.Parameters.AddWithValue("@DoktorDepatman", dep);
                komut.Parameters.AddWithValue("@DoktorTelefon", tel);
                komut.Parameters.AddWithValue("@DoktorAdres", adres);
                komut.Parameters.AddWithValue("@DoktorSifre", sifre);
                komut.Parameters.AddWithValue("@SekreterKimlik", kim);
                komut.ExecuteNonQuery();

                //connection.Close();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Hata oluştu: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

        }
        public void guncelle(string kimlik, string ad, string soyad, DateTime dogum, string cinsiyet, string dep, string tel, string adres, string sifre, string kim)
        {
            OleDbConnection connection = baglanti.ConnectionOpen();
            try
            {
                OleDbCommand komut = new OleDbCommand("UPDATE DoktorTablosu SET (DoktorKimlik,DoktorAd,DoktorSoyad,DoktorDogumTarihi,DoktorCinsiyet,DoktorDepatman,DoktorTelefon,DoktorAdres,DoktorSifre,SekreterKimlik) VALUES (@DoktorKimlik,@DoktorAd,@DoktorSoyad,@DoktorDogumTarihi,@DoktorCinsiyet,@DoktorDepatman,@DoktorTelefon,@DoktorAdres,@DoktorSifre,@SekreterKimlik)", connection);
                komut.Parameters.AddWithValue("@DoktorKimlik", kimlik);
                komut.Parameters.AddWithValue("@DoktorAd", ad);
                komut.Parameters.AddWithValue("@DoktorSoyad", soyad);
                komut.Parameters.AddWithValue("@DoktorDogumTarihi", dogum);
                komut.Parameters.AddWithValue("@DoktorCinsiyet", cinsiyet);
                komut.Parameters.AddWithValue("@DoktorDepatman", dep);
                komut.Parameters.AddWithValue("@DoktorTelefon", tel);
                komut.Parameters.AddWithValue("@DoktorAdres", adres);
                komut.Parameters.AddWithValue("@DoktorSifre", sifre);
                komut.Parameters.AddWithValue("@SekreterKimlik", kim);
                komut.ExecuteNonQuery();

                //connection.Close();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Hata oluştu: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

        }
        public DataTable Listele()
        {
            OleDbConnection connection = baglanti.ConnectionOpen();
            DataTable dataTable = new DataTable();

            try
            {
                string query = "SELECT * FROM DoktorTablosu";
                OleDbDataAdapter adapter = new OleDbDataAdapter(query, connection);
                adapter.Fill(dataTable);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Hata oluştu: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return dataTable;
        }
        public void sil(string kimlik)
        {
            OleDbConnection connection = baglanti.ConnectionOpen();
            try
            {
                OleDbCommand komut = new OleDbCommand("DELETE FROM DoktorTablosu WHERE DoktorKimlik = @DoktorKimlik", connection);
                komut.Parameters.AddWithValue("@DoktorKimlik", kimlik);
                komut.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Hata oluştu: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
