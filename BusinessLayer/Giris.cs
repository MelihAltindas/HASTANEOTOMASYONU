using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace BusinessLayer
{
    public class Giris
    {
        private DataAccessLayer.VeriTabani baglanti = new DataAccessLayer.VeriTabani();

        public Boolean SekreterGiris(string kimlik, string sifre)
        {

            using (OleDbConnection connection = baglanti.ConnectionOpen())
            {
                OleDbCommand komt = new OleDbCommand("SELECT COUNT(*) FROM SekreterTablosu WHERE SekreterKimlik=@kimlik AND SekreterSifre=@sifre", connection);
                komt.Parameters.AddWithValue("@kimlik", kimlik);
                komt.Parameters.AddWithValue("@sifre", sifre);
                int count = (int)komt.ExecuteScalar();
                if (count > 0)
                    return true;
                else
                    return false;
            }

        }
        //doktor giriş
        public Boolean DoktorGiris(string kimlik, string sifre)
        {

            using (OleDbConnection connection = baglanti.ConnectionOpen())
            {
                OleDbCommand komt2 = new OleDbCommand("SELECT COUNT(*) FROM DoktorTablosu WHERE DoktorKimlik=@kimlik AND DoktorSifre=@sifre", connection);
                komt2.Parameters.AddWithValue("@kimlik", kimlik);
                komt2.Parameters.AddWithValue("@sifre", sifre);
                int count = (int)komt2.ExecuteScalar();
                if (count > 0)
                    return true;
                else
                    return false;
            }

        }
    }

    
}
