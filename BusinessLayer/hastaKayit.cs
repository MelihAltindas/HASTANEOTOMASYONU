using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class hastaKayit
    {
        public EntitiyLayer.HastaTablosu BL_Hasta = new EntitiyLayer.HastaTablosu();

        private DataAccessLayer.VeriTabani baglanti = new DataAccessLayer.VeriTabani();

        public void yeni_kayit(string kimlik,string ad,string soyad, DateTime dogum, string cinsiyet,string tel,string mail,string adres)
        {
            OleDbConnection connection = baglanti.ConnectionOpen();
            try
            {
                OleDbCommand komut = new OleDbCommand("INSERT INTO HastaTablosu (HastaKimlik,HastaAd,HastaSoyad,HastaDogumTarihi,HastaCinsiyet,HastaTelefon,HastaMail,HastaAdres) VALUES (@HastaKimlik,@HastaAd,@HastaSoyad,@HastaDogumTarihi,@HastaCinsiyet,@HastaTelefon,@HastaMail,@HastaAdres)" ,connection);  
                komut.Parameters.AddWithValue("@HastaKimlik", kimlik);
                komut.Parameters.AddWithValue("@HastaAd", ad);
                komut.Parameters.AddWithValue("@HastaSoyad", soyad);
                komut.Parameters.AddWithValue("@HastaDogumTarihi", dogum);
                komut.Parameters.AddWithValue("@HastaCinsiyet", cinsiyet);
                komut.Parameters.AddWithValue("@HastaTelefon", tel);
                komut.Parameters.AddWithValue("@HastaMail", mail);
                komut.Parameters.AddWithValue("@HastaAdres", adres);
                komut.ExecuteNonQuery();
                connection.Close();

            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Hata oluştu: " + ex.Message);
            }
            connection.Close();
        }
    
        public DataTable Listele()
        {
            OleDbConnection connection = baglanti.ConnectionOpen();
            DataTable dataTable = new DataTable();

            try
            {
                string query = "SELECT * FROM HastaTablosu";
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
                OleDbCommand komut = new OleDbCommand("DELETE FROM HastaTablosu WHERE HastaKimlik = @HastaKimlik", connection);
                komut.Parameters.AddWithValue("@HastaKimlik", kimlik);
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
        public void guncelle(string kimlik, string ad, string soyad, DateTime dogum, string cinsiyet, string tel, string mail, string adres)
        {
            OleDbConnection connection = baglanti.ConnectionOpen();
            try
            {
                OleDbCommand komut = new OleDbCommand("UPDATE HastaTablosu SET(HastaKimlik, HastaAd, HastaSoyad, HastaDogumTarihi, HastaCinsiyet, HastaTelefon, HastaMail, HastaAdres) VALUES (@HastaKimlik, @HastaAd, @HastaSoyad, @HastaDogumTarihi, @HastaCinsiyet, @HastaTelefon, @HastaMail, @HastaAdres)" ,connection);
                komut.Parameters.AddWithValue("@HastaKimlik", kimlik);
                komut.Parameters.AddWithValue("@HastaAd", ad);
                komut.Parameters.AddWithValue("@HastaSoyad", soyad);
                komut.Parameters.AddWithValue("@HastaDogumTarihi", dogum);
                komut.Parameters.AddWithValue("@HastaCinsiyet", cinsiyet);
                komut.Parameters.AddWithValue("@HastaTelefon", tel);
                komut.Parameters.AddWithValue("@HastaMail", mail);
                komut.Parameters.AddWithValue("@HastaAdres", adres);
                komut.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Hata oluştu: " + ex.Message);
            }
            connection.Close();
        }
    }
}
