namespace E_Comerce.Models.Metier
{
    public class CommandeDetails
    {
        private int id_detail;
        private int id_commande;
        private int id_produit;
        private int quantite;

        public int Id_detail { get => id_detail; set => id_detail = value; }

        public int Id_commande { get => id_commande; set => id_commande = value; }

        public int Id_produit { get => id_produit; set => id_produit = value; }

        public int Quantite { get => quantite; set => quantite = value; }
    }
}