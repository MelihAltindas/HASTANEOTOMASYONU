using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class sekreterKayit
    {
        public EntitiyLayer.SekreterTablosu BL_Sekreter = new EntitiyLayer.SekreterTablosu(); //entitylayerdan sektereter clasının içine ulaşabilmek için bl_doktor olarak oluşturulur.
        //bl_sektreter bussinestaki sekreter classını temsil eder

        private DataAccessLayer.VeriTabani baglanti = new DataAccessLayer.VeriTabani(); //veritabanına bağlanmak için

        public void ekle(string kimlik, string ad, string soyad, DateTime dogum, string cinsiyet, string tel, string sifre)
        {
            OleDbConnection connection = baglanti.ConnectionOpen();
           
            try
            {
                OleDbCommand komut = new OleDbCommand("INSERT INTO SekreterTablosu (SekreterKimlik, SekreterAd, SekreterSoyad, SekreterDogumTarihi,SekreterCinsiyet,SekreterTelefon,  SekreterSifre) VALUES (@SekreterKimlik, @SekreterAd, @SekreterSoyad,@SekreterDogumTarihi, @SekreterCinsiyet,@SekreterTelefon , @SekreterSifre)" ,connection);

                komut.Parameters.AddWithValue("@SekreterKimlik", kimlik);
                komut.Parameters.AddWithValue("@SekreterAd", ad);
                komut.Parameters.AddWithValue("@SekreterSoyad", soyad);
                komut.Parameters.AddWithValue("@SekreterDogumTarihi", dogum);
                komut.Parameters.AddWithValue("@SekreterCinsiyet", cinsiyet);
                komut.Parameters.AddWithValue("@SekreterTelefon", tel);
                komut.Parameters.AddWithValue("@SekreterSifre", sifre);

               
                komut.ExecuteNonQuery();
               
                connection.Close();
               
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Hata oluştu: " + ex.Message);
            }
            connection.Close();
        }
        public void guncelle(string kimlik, string ad, string soyad, DateTime dogum, string tel, string cinsiyet, string sifre)
        {
            OleDbConnection connection = baglanti.ConnectionOpen();
            try
            {
                OleDbCommand komut = new OleDbCommand("UPDATE SekreterTablosu SET (SekreterKimlik, SekreterAd, SekreterSoyad, SekreterDogumTarihi, SekreterCinsiyet, SekreterTelefon, SekreterSifre) VALUES(@SekreterKimlik, @SekreterAd, @SekreterSoyad, @SekreterDogumTarihi, @SekreterCinsiyet, @SekreterTelefon, @SekreterSifre)" ,connection);

                komut.Parameters.AddWithValue("@SekreterKimlik", kimlik);
                komut.Parameters.AddWithValue("@SekreterAd", ad);
                komut.Parameters.AddWithValue("@SekreterSoyad", soyad);
                komut.Parameters.AddWithValue("@SekreterDogumTarihi", dogum);
                komut.Parameters.AddWithValue("@SekreterCinsiyet", cinsiyet);
                komut.Parameters.AddWithValue("@SekreterTelefon", tel);
                komut.Parameters.AddWithValue("@SekreterSifre", sifre);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Hata oluştu: " + ex.Message);
            }
            connection.Close();
        }
        public void sil(string kimlik)
        {
            OleDbConnection connection = baglanti.ConnectionOpen();
            try
            {
                OleDbCommand komut = new OleDbCommand("DELETE FROM SekreterTablosu WHERE SekreterKimlik = @Sekreterkimlik", connection);
                komut.Parameters.AddWithValue("@Sekreterkimlik", kimlik);
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
        public DataTable Listele()
        {
            OleDbConnection connection = baglanti.ConnectionOpen();
            DataTable dataTable = new DataTable();

            try
            {
                string query = "SELECT * FROM SekreterTablosu";
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


    }
}
