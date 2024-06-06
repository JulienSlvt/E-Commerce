using System;
using Microsoft.Data.SqlClient; // Use Microsoft.Data.SqlClient for MSSQL
using Microsoft.Extensions.Configuration;
using E_Comerce.Models.MesExceptions;

namespace E_Comerce.Models.Persistance
{
    public class Connexion
    {
        private static SqlConnection connection;
        private static Connexion instance;

        private Connexion() { }

        public SqlConnection GetConnection()
        {
            try
            {
                var builder = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json");

                IConfiguration configuration = builder.Build();
                string connectionString = configuration.GetConnectionString("bddCourante");

                Console.WriteLine($"Connecting to database with connection string: {connectionString}");

                connection = new SqlConnection(connectionString);
                connection.Open();

                Console.WriteLine("Successfully connected to the database.");

                return connection;
            }
            catch (SqlException err)
            {
                Console.WriteLine($"Error connecting to the database: {err.Message}");
                throw new MonException("", "Erreur d'accès à la base.", err.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unknown error connecting to the database: {e.Message}");
                throw new MonException("", "Erreur d'accès", e.Message);
            }
        }

        public static Connexion GetInstance()
        {
            if (Connexion.instance == null)
                Connexion.instance = new Connexion();
            return Connexion.instance;
        }

        public static void CloseConnection()
        {
            if (instance != null && connection != null)
                connection.Close();
        }
    }
}
