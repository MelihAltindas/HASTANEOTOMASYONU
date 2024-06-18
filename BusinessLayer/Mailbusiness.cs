using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using EntitiyLayer;

namespace BusinessLayer
{
    public class Mailbusiness
    {
        private DataAccessLayer.VeriTabani baglanti = new DataAccessLayer.VeriTabani();
        private EntitiyLayer.Mail posta = new EntitiyLayer.Mail();

       
            public void SendMail(List<string> list, string hastaKimlik, string baslik, string konu)
            {
                OleDbConnection connection = baglanti.ConnectionOpen();

                try
                {
                    string hastaMail = "";

                    // Hasta mail adresini al
                    string hastaMailQuery = "SELECT HastaMail FROM HastaTablosu WHERE HastaKimlik = @hastaKimlik";
                    using (OleDbCommand hastaMailCommand = new OleDbCommand(hastaMailQuery, connection))
                    {
                        hastaMailCommand.Parameters.AddWithValue("@hastaKimlik", hastaKimlik);
                        hastaMailCommand.ExecuteNonQuery();
                        using (OleDbDataReader hastaMailReader = hastaMailCommand.ExecuteReader())
                        {
                            while (hastaMailReader.Read())
                            {
                                hastaMail = hastaMailReader["HastaMail"].ToString();
                            }
                        }
                    }

                    // Doktor bilgilerini çek
                    string doktorAdSoyad = "";

                    string doktorKimlik = "";
                    string doktorBilgiQuery = "SELECT DoktorAd, DoktorSoyad,  DoktorKimlik FROM DoktorTablosu WHERE DoktorKimlik IN (SELECT DoktorKimlik FROM MuayeneTablosu WHERE HastaKimlik = @hastaKimlik)";
                    using (OleDbCommand doktorBilgiCommand = new OleDbCommand(doktorBilgiQuery, connection))
                    {
                        doktorBilgiCommand.Parameters.AddWithValue("@hastaKimlik", hastaKimlik);
                        doktorBilgiCommand.ExecuteNonQuery();
                        using (OleDbDataReader doktorBilgiReader = doktorBilgiCommand.ExecuteReader())
                        {
                            while (doktorBilgiReader.Read())
                            {
                                doktorAdSoyad = doktorBilgiReader["DoktorAd"].ToString() + " " + doktorBilgiReader["DoktorSoyad"].ToString();

                                doktorKimlik = doktorBilgiReader["DoktorKimlik"].ToString();
                            }
                        }
                    }

                    // E-posta gönderme işlemi
                    SmtpClient smtp = new SmtpClient();
                    smtp.Credentials = new NetworkCredential(posta.senderEmail, posta.senderpassword);
                    smtp.Host = posta.smtphost;
                    smtp.Port = posta.smtpport;
                    smtp.EnableSsl = true;


                    MailMessage message = new MailMessage(posta.senderEmail, hastaMail, baslik, konu);


                    smtp.Send(message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        
    }
}
