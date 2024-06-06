using System;
using System.Collections.Generic;

namespace E_Comerce.Models.Metier
{
    public class Commande
    {
        private int id_commande;
        private int id_client;
        private DateTime date_commande;

        public int Id_commande { get => id_commande; set => id_commande = value; }
        public int Id_client { get => id_client; set => id_client = value; }
        public DateTime Date_commande { get => date_commande; set => date_commande = value; }
    }
}
