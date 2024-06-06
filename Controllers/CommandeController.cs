using E_Comerce.Models.Dao;
using E_Comerce.Models.MesExceptions;
using E_Comerce.Models.Metier;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Comerce.Controllers
{
    public class CommandeController : Controller
    {
        // GET: CommandeController
        public ActionResult Index()
        {

            List<Commande> commandes = null;
            try
            {
                commandes = ServiceE_commerce.GetToutesLesCommandes();
            }
            catch (MonException e)
            {
                ModelState.AddModelError("Erreur", "Erreur lors du chargement des commandes : " + e.Message);
            }


            return View(commandes);
        }

        // GET: CommandeController/Details/5
        public ActionResult Details(int id)
        {
            // Utiliser ServiceE_commerce.GetDetailsCommande(id) pour récupérer les détails d'une commande par ID
            List<CommandeDetails> detailsCommande = ServiceE_commerce.GetDetailsCommande(id);
            return View(detailsCommande);
        }

        // GET: CommandeController/Delete/5
        public ActionResult Delete(int id)
        {
            // Utiliser ServiceE_commerce.GetCommandeById(id) pour récupérer une commande par ID
            Commande commande = ServiceE_commerce.GetCommandeById(id);
            return View(commande);
        }

        // POST: CommandeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // Utiliser ServiceE_commerce.SupprimerCommande(id) pour supprimer une commande par ID
                ServiceE_commerce.SupprimerCommande(id);
                return RedirectToAction(nameof(Index));
            }
            catch (MonException e)
            {
                ModelState.AddModelError("Erreur", "Erreur lors de la suppression de la commande : " + e.Message);
                return View();
            }
        }
    }
}
