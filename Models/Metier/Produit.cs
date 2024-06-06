namespace E_Comerce.Models.Metier
{
    public class Produit
    {
        private int id_produit;
        private string nom;
        private string description;
        private int prix;

        public int Id_produit { get => id_produit; set => id_produit = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Description { get => description; set => description = value; }
        public int Prix { get => prix; set => prix = value; }
    }
}
