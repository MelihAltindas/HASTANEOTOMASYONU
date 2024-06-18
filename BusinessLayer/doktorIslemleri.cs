using EntitiyLayer;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using System.IO;
using System.Xml.Linq;
using System.Data.SqlClient;


namespace BusinessLayer
{
    public class doktorIslemleri
    {
        public EntitiyLayer.DoktorTablosu BL_Doktor = new EntitiyLayer.DoktorTablosu();
        public EntitiyLayer.MuayeneTablosu BL_Muayene = new EntitiyLayer.MuayeneTablosu();
        public EntitiyLayer.RandevuTablosu BL_Randevu = new EntitiyLayer.RandevuTablosu();

        private DataAccessLayer.VeriTabani baglanti = new DataAccessLayer.VeriTabani();
        

        // .Doktor adı ve soyadını getir
        public Tuple<string, string, string> GetDoktorAdSoyad(string doktorKimlik)
        {
            string doktorkimlik = "";
            string doktorAd = "";
            string doktorSoyad = "";
            

            using (OleDbConnection connection = baglanti.ConnectionOpen())
            {
                try
                {
                    OleDbCommand command = new OleDbCommand("SELECT DoktorKimlik, DoktorAd, DoktorSoyad FROM DoktorTablosu WHERE DoktorKimlik = @DoktorKimlik", connection);
                    command.Parameters.AddWithValue("@DoktorKimlik", doktorKimlik);
                    OleDbDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        doktorkimlik = reader["DoktorKimlik"].ToString();
                        doktorAd = reader["DoktorAd"].ToString();
                        doktorSoyad = reader["DoktorSoyad"].ToString();
                        
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine("Hata oluştu: " + ex.Message);
                    return null;
                }
            }

            return Tuple.Create(doktorkimlik,doktorAd, doktorSoyad );
        }

        //Hasta Bilgilerini Çekme Ekrana Yazdırma
        public string HastaBilgisi(string kimlik)
        {
            string ad = "";
            string soyad = "";
            string dogumTarihi = "";
            string cinsiyet = "";
            string telefon = "";
            string mail = "";

            OleDbConnection connection = baglanti.ConnectionOpen();
            try
            {
                OleDbCommand komut = new OleDbCommand("SELECT HastaAd, HastaSoyad, HastaDogumTarihi, HastaCinsiyet, HastaTelefon, HastaMail FROM HastaTablosu WHERE HastaKimlik LIKE @Kimlik", connection);
                komut.Parameters.AddWithValue("@Kimlik", kimlik + "%");

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
                        HastaTelefon= reader["HastaTelefon"].ToString(),
                        HastaMail= reader["HastaMail"].ToString(),
                    };
                    ad = hasta.HastaAd;
                    soyad = hasta.HastaSoyad;
                    dogumTarihi = hasta.HastaDogumTarihi;
                    cinsiyet = hasta.HastaCinsiyet;
                    telefon = hasta.HastaTelefon;
                    mail = hasta.HastaMail;
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
            string result = $"Hasta Adı: {ad}       Soyadı: {soyad}       Doğum Tarihi: {dogumTarihi}       Cinsiyeti: {cinsiyet}       Telefon: {telefon}      Mail Adresi: {mail}";
            return result;
        }

        //Muayene Tablosuna yeni kayıt girme
        public void kaydet( string hastakimlik, string doktorkimlik, string tanı, string tedavi, string ilaclar)
        {
            OleDbConnection connection = baglanti.ConnectionOpen();
            try
            {
                OleDbCommand komut = new OleDbCommand("INSERT INTO MuayeneTablosu (HastaKimlik, DoktorKimlik,Tanı, Tedavi, İlaclar) VALUES (@HastaKimlik, @DoktorKimlik,@Tanı, @Tedavi, @İlaclar)", connection);
                
                komut.Parameters.AddWithValue("@HastaKimlik", hastakimlik);
                komut.Parameters.AddWithValue("@DoktorKimlik", doktorkimlik);
                komut.Parameters.AddWithValue("@Tanı", tanı);
                komut.Parameters.AddWithValue("@Tedavi", tedavi);
                komut.Parameters.AddWithValue("@İlaclar", ilaclar);
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
        //Muayene Tablosunu Listeleme
        public DataTable Listele()
        {
            OleDbConnection connection = baglanti.ConnectionOpen();
            DataTable dataTable = new DataTable();

            try
            {
                string query = "SELECT * FROM MuayeneTablosu";
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
        //Muayene Tablosunu Güncelleme
        public void guncelle(  string hkimlik ,string dkimlik ,string tanı, string tedavi, string ilaclar)
        {
            OleDbConnection connection = baglanti.ConnectionOpen();
            try
            {
                OleDbCommand komut = new OleDbCommand("UPDATE MuayeneTablosu SET (HastaKimlik, DoktorKimlik,Tanı, Tedavi, İlaclar) VALUES (@HastaKimlik,@DoktorKimlik,@Tanı, @Tedavi, @İlaclar)", connection);
                komut.Parameters.AddWithValue("@HastaKimlik", hkimlik);
                komut.Parameters.AddWithValue("@DoktorKimlik", dkimlik);
                komut.Parameters.AddWithValue("@Tanı", tanı);
                komut.Parameters.AddWithValue("@Tedavi", tedavi);
                komut.Parameters.AddWithValue("@İlaclar", ilaclar);
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
        public List<RandevuTablosu> GetGelecekTarihliRandevular(string doktorad)
        {
            List<RandevuTablosu> randevular = new List<RandevuTablosu>();

            OleDbConnection connection = baglanti.ConnectionOpen();
            try
            {
                DateTime today = DateTime.Today;

                string query = "SELECT * FROM RandevuTablosu WHERE DoktorAdıveSoyadı = @doktorad AND RandevuTarihi >= @today";
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@doktorad", doktorad);
                    command.Parameters.AddWithValue("@today", today);
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            RandevuTablosu randevu = new RandevuTablosu
                            {
                                // Burada verileri al ve randevu nesnesine ekle
                            };
                            randevular.Add(randevu);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return randevular;
        }

    }
    public class Grafik
    {
        private DataAccessLayer.VeriTabani baglanti = new DataAccessLayer.VeriTabani();
  
        public Dictionary<string, int> GetPatientCountsByDepartment()
        {
            Dictionary<string, int> departmentPatientCounts = new Dictionary<string, int>();

            using (OleDbConnection connection = baglanti.ConnectionOpen())
            {
                string query = @"SELECT d.DoktorDepatman, COUNT(r.HastaKimlik) AS PatientCount
                          FROM RandevuTablosu r
                          INNER JOIN DoktorTablosu d ON r.DoktorKimlik = d.DoctorId
                          GROUP BY d.DoktorDepatman";

                OleDbCommand command = new OleDbCommand(query, connection);
                OleDbDataReader reader = command.ExecuteReader();
                connection.Open();
                
                
                    while (reader.Read())
                    {
                        var DoktorDepatman = reader.GetString(0);
                        var patientCount = reader.GetInt32(1);
                        departmentPatientCounts[DoktorDepatman] = patientCount;
                    }
                
            }

            return departmentPatientCounts;
        }
    }
}
