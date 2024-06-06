using Microsoft.Data.SqlClient;
using System;
using System.Data;
using E_Comerce.Models.MesExceptions;

namespace E_Comerce.Models.Persistance
{
    public class DBInterface
    {
        public static DataTable Lecture(string req, Serreurs er=null)
        {
            SqlConnection cnx = null;
            try
            {
                cnx = Connexion.GetInstance().GetConnection();
                SqlCommand cmd = new SqlCommand(req, cnx);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                // Construct the DataSet
                DataSet ds = new DataSet();
                da.Fill(ds, "resultat");

                return ds.Tables["resultat"];
            }
            catch (MonException me)
            {
                throw me;
            }
            catch (Exception e)
            {
                throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), e.Message);
            }
            finally
            {
                if (cnx != null)
                    cnx.Close();
            }
        }

        public static void Execute_Transaction(string requete)
        {
            SqlConnection cnx = null;
            try
            {
                cnx = Connexion.GetInstance().GetConnection();
                using (SqlTransaction trans = cnx.BeginTransaction())
                {
                    using (SqlCommand cmd = new SqlCommand(requete, cnx, trans))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    trans.Commit();
                }
            }
            catch (SqlException uneException)
            {
                throw new MonException(uneException.Message, "Insertion", "SQL");
            }
            finally
            {
                if (cnx != null)
                    cnx.Close();
            }
        }
    }
}
