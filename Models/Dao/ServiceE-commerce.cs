using System.Data;
using E_Comerce.Models.Persistance;
using E_Comerce.Models.MesExceptions;
using E_Comerce.Models.Metier;

namespace E_Comerce.Models.Dao
{
    public class ServiceE_commerce
    {
        public static List<Produit> GetTousLesProduits()
        {
            List<Produit> produits = new List<Produit>();
            Serreurs er = new Serreurs("Erreur sur lecture des produits.", "E-Comerce.getProduits()");
            try
            {
                String mysql = "SELECT * FROM Produits";
                DataTable dataTable = DBInterface.Lecture(mysql, er);

                // Convert DataTable to List<Produit>
                foreach (DataRow row in dataTable.Rows)
                {
                    Produit produit = new Produit
                    {
                        Id_produit = Convert.ToInt32(row["Id_produit"]),
                        Nom = row["Nom"].ToString(),
                        Description = row["Description"].ToString(),
                        Prix = Convert.ToInt32(row["Prix"])
                    };
                    produits.Add(produit);
                }

                return produits;
            }
            catch (MonException e)
            {
                // Log or handle the exception appropriately
                throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), e.Message);
            }
            catch (Exception ex)
            {
                // Log or handle other exceptions
                throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), ex.Message);
            }
        }

        // Method to get a specific product by ID
        public static Produit GetProduitById(int id)
        {
            Serreurs er = new Serreurs("Erreur sur lecture du produit.", "E-Comerce.getProduitById()");
            try
            {
                String mysql = $"SELECT * FROM Produits WHERE Id_produit = {id}";
                DataTable dataTable = DBInterface.Lecture(mysql, er);

                if (dataTable.Rows.Count > 0)
                {
                    DataRow row = dataTable.Rows[0];
                    Produit produit = new Produit
                    {
                        Id_produit = Convert.ToInt32(row["Id_produit"]),
                        Nom = row["Nom"].ToString(),
                        Description = row["Description"].ToString(),
                        Prix = Convert.ToInt32(row["Prix"])
                    };
                    return produit;
                }

                return null; // Product not found
            }
            catch (MonException e)
            {
                throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), e.Message);
            }
        }

        // Method to add a new product
        public static void AjouterProduit(Produit produit)
        {
            Serreurs er = new Serreurs("Erreur lors de l'ajout du produit.", "E-Comerce.ajouterProduit()");
            try
            {
                String mysql = $"INSERT INTO Produits (Id_produit, Nom, Description, Prix) VALUES ('{produit.Id_produit}', '{produit.Nom}', '{produit.Description}', {produit.Prix})";
                DBInterface.Execute_Transaction(mysql);
            }
            catch (MonException e)
            {
                throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), e.Message);
            }
        }

        // Method to update an existing product
        public static void ModifierProduit(Produit produit)
        {
            Serreurs er = new Serreurs("Erreur lors de la modification du produit.", "E-Comerce.modifierProduit()");
            try
            {
                String mysql = $"UPDATE Produits SET Nom = '{produit.Nom}', Description = '{produit.Description}', Prix = {produit.Prix} WHERE Id_produit = {produit.Id_produit}";
                DBInterface.Execute_Transaction(mysql);
            }
            catch (MonException e)
            {
                throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), e.Message);
            }
        }

        // Method to delete a product by ID
        public static void SupprimerProduit(int id)
        {
            Serreurs er = new Serreurs("Erreur lors de la suppression du produit.", "E-Comerce.supprimerProduit()");
            try
            {
                String mysql = $"DELETE FROM Produits WHERE Id_produit = {id}";
                DBInterface.Execute_Transaction(mysql);
            }
            catch (MonException e)
            {
                throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), e.Message);
            }
        }

        public static List<Client> GetTousLesClients()
        {
            List<Client> clients = new List<Client>();
            Serreurs er = new Serreurs("Erreur sur lecture des clients.", "E-Comerce.GetTousLesClients()");

            try
            {
                String mysql = "SELECT * FROM Clients";
                DataTable dataTable = DBInterface.Lecture(mysql, er);

                // Convert DataTable to List<Client>
                foreach (DataRow row in dataTable.Rows)
                {
                    Client client = new Client
                    {
                        Id_client = Convert.ToInt32(row["Id_client"]),
                        Nom = row["Nom"].ToString(),
                        Email = row["Email"].ToString(),
                        Adresse = row["Adresse"].ToString()
                    };
                    clients.Add(client);
                }

                return clients;
            }
            catch (MonException e)
            {
                // Log or handle the exception appropriately
                throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), e.Message);
            }
            catch (Exception ex)
            {
                // Log or handle other exceptions
                throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), ex.Message);
            }
        }

        public static Commande GetCommandeById(int idCommande)
        {
            Serreurs er = new Serreurs("Erreur sur lecture de la commande.", "E-Comerce.GetCommandeById()");

            try
            {
                String mysql = $"SELECT * FROM Commandes WHERE Id_commande = {idCommande}";
                DataTable dataTable = DBInterface.Lecture(mysql, er);

                if (dataTable.Rows.Count > 0)
                {
                    DataRow row = dataTable.Rows[0];
                    Commande commande = new Commande
                    {
                        Id_commande = Convert.ToInt32(row["Id_commande"]),
                        Id_client = Convert.ToInt32(row["Id_client"]),
                        Date_commande = Convert.ToDateTime(row["Date_commande"])
                    };

                    return commande;
                }

                return null; // Commande non trouvée
            }
            catch (MonException e)
            {
                // Log or handle the exception appropriately
                throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), e.Message);
            }
            catch (Exception ex)
            {
                // Log or handle other exceptions
                throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), ex.Message);
            }
        }


        public static List<Commande> GetCommandesParClient(int idClient)
        {
            List<Commande> commandes = new List<Commande>();
            Serreurs er = new Serreurs("Erreur sur lecture des commandes.", "E-Comerce.GetCommandesParClient()");

            try
            {
                String mysql = $"SELECT * FROM Commandes WHERE Id_client = {idClient}";
                DataTable dataTable = DBInterface.Lecture(mysql, er);

                foreach (DataRow row in dataTable.Rows)
                {
                    Commande commande = new Commande
                    {
                        Id_commande = Convert.ToInt32(row["Id_commande"]),
                        Id_client = Convert.ToInt32(row["Id_client"]),
                        Date_commande = Convert.ToDateTime(row["Date_commande"])
                    };
                    commandes.Add(commande);
                }

                return commandes;
            }
            catch (MonException e)
            {
                // Log or handle the exception appropriately
                throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), e.Message);
            }
            catch (Exception ex)
            {
                // Log or handle other exceptions
                throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), ex.Message);
            }
        }

        public static List<CommandeDetails> GetDetailsCommande(int idCommande)
        {
            List<CommandeDetails> detailsCommande = new List<CommandeDetails>();
            Serreurs er = new Serreurs("Erreur sur lecture des détails de la commande.", "E-Comerce.GetDetailsCommande()");

            try
            {
                String mysql = $"SELECT * FROM CommandeDetails WHERE Id_commande = {idCommande}";
                DataTable dataTable = DBInterface.Lecture(mysql, er);

                foreach (DataRow row in dataTable.Rows)
                {
                    CommandeDetails detail = new CommandeDetails
                    {
                        Id_detail = Convert.ToInt32(row["Id_detail"]),
                        Id_commande = Convert.ToInt32(row["Id_commande"]),
                        Id_produit = Convert.ToInt32(row["Id_produit"]),
                        Quantite = Convert.ToInt32(row["Quantite"])
                    };

                    detailsCommande.Add(detail);
                }

                return detailsCommande;
            }
            catch (MonException e)
            {
                // Log or handle the exception appropriately
                throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), e.Message);
            }
            catch (Exception ex)
            {
                // Log or handle other exceptions
                throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), ex.Message);
            }
        }



        public static void SupprimerCommande(int idCommande)
        {
            Serreurs er = new Serreurs("Erreur lors de la suppression de la commande.", "E-Comerce.SupprimerCommande()");
            try
            {
                // Supprimer la commande dans la table Commandes
                String mysqlCommande = $"DELETE FROM Commandes WHERE Id_commande = {idCommande}";
                DBInterface.Execute_Transaction(mysqlCommande);

                // Supprimer les détails de la commande dans la table CommandeDetails
                String mysqlDetails = $"DELETE FROM CommandeDetails WHERE Id_commande = {idCommande}";
                DBInterface.Execute_Transaction(mysqlDetails);
            }
            catch (MonException e)
            {
                throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), e.Message);
            }
        }

        public static List<Commande> GetToutesLesCommandes()
        {
            List<Commande> commandes = new List<Commande>();
            Serreurs er = new Serreurs("Erreur sur lecture des commandes.", "E-Comerce.GetToutesLesCommandes()");

            try
            {
                String mysql = "SELECT * FROM Commandes";
                DataTable dataTable = DBInterface.Lecture(mysql, er);

                // Convert DataTable to List<Commande>
                foreach (DataRow row in dataTable.Rows)
                {
                    Commande commande = new Commande
                    {
                        Id_commande = Convert.ToInt32(row["Id_commande"]),
                        Id_client = Convert.ToInt32(row["Id_client"]),
                        Date_commande = Convert.ToDateTime(row["Date_commande"])
                    };
                    commandes.Add(commande);
                }

                return commandes;
            }
            catch (MonException e)
            {
                // Log or handle the exception appropriately
                throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), e.Message);
            }
            catch (Exception ex)
            {
                // Log or handle other exceptions
                throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), ex.Message);
            }
        }
    }
}
