using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using EntitiyLayer;
using System.Security.Policy;

namespace BusinessLayer
{
    
    public class randevuKayıt
    {
        public EntitiyLayer.RandevuTablosu BL_Randevu = new EntitiyLayer.RandevuTablosu(); //entitylayerdan sektereter clasının içine ulaşabilmek için bl_doktor olarak oluşturulur.
        //bl_sektreter bussinestaki sekreter classını temsil eder

        private DataAccessLayer.VeriTabani baglanti = new DataAccessLayer.VeriTabani();

        public bool RandevuEkle(string numara, string kimlik, string doktorad, DateTime randevutarihi, string saat, string departman)
        {
            OleDbConnection connection = baglanti.ConnectionOpen();
            try
            {
                

                // Randevu zamanının uygun olup olmadığını kontrol edin
                OleDbCommand kontrolKomutu = new OleDbCommand("SELECT COUNT(*) FROM RandevuTablosu WHERE DoktorAdıveSoyadı = @DoktorAdıveSoyadı AND RandevuTarihi = @RandevuTarihi AND RandevuSaati = @RandevuSaati", connection);
                kontrolKomutu.Parameters.AddWithValue("@DoktorAdıveSoyadı", doktorad);
                kontrolKomutu.Parameters.AddWithValue("@RandevuTarihi", randevutarihi.Date);
                kontrolKomutu.Parameters.AddWithValue("@RandevuSaati", saat);

                if (connection.State != ConnectionState.Open)
                    connection.Open();

                int mevcutRandevuSayisi = (int)kontrolKomutu.ExecuteScalar();

                if (mevcutRandevuSayisi == 0)
                {
                    // Eğer uygunsa, randevuyu planlamaya devam edin
                    OleDbCommand komut = new OleDbCommand("INSERT INTO RandevuTablosu (RandevuNumarası,HastaKimlik, DoktorAdıveSoyadı,RandevuTarihi, RandevuSaati, DoktorDepatman) VALUES (@RandevuNumarası,@HastaKimlik,@DoktorAdıveSoyadı,@RandevuTarihi, @RandevuSaati,@DoktorDepatman)", connection);
                    komut.Parameters.AddWithValue("@RandevuNumarası", numara);
                    komut.Parameters.AddWithValue("@HastaKimlik", kimlik);
                    komut.Parameters.AddWithValue("@DoktorAdıveSoyadı", doktorad);
                    komut.Parameters.AddWithValue("@RandevuTarihi", randevutarihi);
                    komut.Parameters.AddWithValue("@RandevuSaati", saat);
                    komut.Parameters.AddWithValue("@DoktorDepatman", departman);


                    komut.ExecuteNonQuery();

                    Console.WriteLine("Randevu başarıyla eklendi.");
                    return true;
                }
                else
                {
                    Console.WriteLine("Seçilen saatte doktorun randevusu bulunmaktadır. Lütfen başka bir saat seçiniz.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
                return false;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }
        public List<string> GetDoctorsByDepartment(string department)
        {
            List<string> doctorIDs = new List<string>();
            OleDbConnection connection = baglanti.ConnectionOpen();
            try
            {
                OleDbCommand command = new OleDbCommand("SELECT DoktorAd, DoktorSoyad FROM DoktorTablosu WHERE DoktorDepatman = @Department", connection);
                command.Parameters.AddWithValue("@Department", department);
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    doctorIDs.Add(reader["DoktorAd"].ToString() +" "+ reader["DoktorSoyad"].ToString());
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Hata oluştu: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return doctorIDs;
        }
        public DataTable Listele()
        {
            OleDbConnection connection = baglanti.ConnectionOpen();
            DataTable dataTable = new DataTable();

            try
            {
                string query = "SELECT * FROM RandevuTablosu";
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

        public string HastaBilgileri(string kimlik)
        {
            string ad = "";
            string soyad = "";
            string dogumTarihi = "";
            string cinsiyet = "";

            OleDbConnection connection = baglanti.ConnectionOpen();
            try
            {
                OleDbCommand komut = new OleDbCommand("SELECT HastaAd, HastaSoyad, HastaDogumTarihi, HastaCinsiyet FROM HastaTablosu WHERE HastaKimlik LIKE @kimlik", connection);
                komut.Parameters.AddWithValue("@kimlik", kimlik + "%");

                if (connection.State != ConnectionState.Open)
                    connection.Open();

                OleDbDataReader reader = komut.ExecuteReader();

                if (reader.Read())
                {
                    HastaTablosu hasta = new HastaTablosu()
                    {
                        HastaAd = reader["HastaAd"].ToString(),
                        HastaSoyad = reader["HastaSoyad"].ToString(),
                        HastaDogumTarihi = reader["HastaDogumTarihi"].ToString(),
                        HastaCinsiyet = reader["HastaCinsiyet"].ToString(),
                    };
                    ad = hasta.HastaAd;
                    soyad = hasta.HastaSoyad;
                    dogumTarihi = hasta.HastaDogumTarihi;
                    cinsiyet = hasta.HastaCinsiyet;


                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }

            // Combine all retrieved information into a single string
            string result = $"Hasta Adı: {ad}       Soyadı: {soyad}       Doğum Tarihi: {dogumTarihi}       Cinsiyeti: {cinsiyet}";
            return result;
        }


    }
}
