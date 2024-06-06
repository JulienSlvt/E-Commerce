namespace E_Comerce.Models.Metier
{
    public class Client
    {
        private int id_client;
        private string nom;
        private string email;
        private string adresse;

        public int Id_client { get => id_client; set => id_client = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Email { get => email; set => email = value; }
        public string Adresse { get => adresse; set => adresse = value; }
    }
}
