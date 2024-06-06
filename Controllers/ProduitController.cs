using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using E_Comerce.Models.Dao;
using E_Comerce.Models.MesExceptions;
using E_Comerce.Models.Metier;

namespace E_Comerce.Controllers
{
    public class ProduitController : Controller
    {
        // GET: ProduitController
        public IActionResult Index()
        {
            List<Produit> produits = null;
            try
            {
                produits = ServiceE_commerce.GetTousLesProduits();
            }
            catch (MonException e)
            {
                ModelState.AddModelError("Erreur", "Erreur lors du chargement des produits : " + e.Message);
            }
            return View(produits);
        }

        public IActionResult List()
        {
            List<Produit> produits = null;
            try
            {
                produits = ServiceE_commerce.GetTousLesProduits();
            }
            catch (MonException e)
            {
                ModelState.AddModelError("Erreur", "Erreur lors du chargement des produits : " + e.Message);
            }
            return View(produits);
        }

        // GET: ProduitController/Details/5
        public ActionResult Details(int id)
        {
            Produit produit = null;
            try
            {
                produit = ServiceE_commerce.GetProduitById(id);
            }
            catch (MonException e)
            {
                ModelState.AddModelError("Erreur", "Erreur lors de la récupération du produit : " + e.Message);
            }
            return View(produit);
        }

        // GET: ProduitController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProduitController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // Extract form data and create a new Produit
                Produit newProduit = new Produit
                {
                    // Set properties based on the form fields
                    Id_produit = Convert.ToInt32(collection["Id_produit"]),
                    Nom = collection["Nom"],
                    Description = collection["Description"],
                    Prix = Convert.ToInt32(collection["Prix"])
                    // Set other properties as needed
                };

                // Call the service method to add the new product
                ServiceE_commerce.AjouterProduit(newProduit);

                return RedirectToAction(nameof(Index));
            }
            catch (MonException e)
            {
                ModelState.AddModelError("Erreur", "Erreur lors de la création du produit : " + e.Message);
                return View();
            }
        }

        // GET: ProduitController/Edit/5
        public ActionResult Edit(int id)
        {
            Produit produit = null;
            try
            {
                produit = ServiceE_commerce.GetProduitById(id);
            }
            catch (MonException e)
            {
                ModelState.AddModelError("Erreur", "Erreur lors de la récupération du produit : " + e.Message);
            }
            return View(produit);
        }

        // POST: ProduitController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)    
        {
            try
            {
                // Extract form data and update the existing Produit
                Produit updatedProduit = new Produit
                {
                    Id_produit = id,
                    Nom = collection["Nom"],
                    Description = collection["Description"],    // ON NE PEUT PAS METTRE D'APOSTROPHE DANS LA DESCRIPTION
                    Prix = Convert.ToInt32(collection["Prix"])
                    // Set other properties as needed
                };

                // Call the service method to update the product
                ServiceE_commerce.ModifierProduit(updatedProduit);

                return RedirectToAction(nameof(Index));
            }
            catch (MonException e)
            {
                ModelState.AddModelError("Erreur", "Erreur lors de la modification du produit : " + e.Message);
                return View();
            }
        }

        // GET: ProduitController/Delete/5
        public ActionResult Delete(int id)
        {
            Produit produit = null;
            try
            {
                produit = ServiceE_commerce.GetProduitById(id);
            }
            catch (MonException e)
            {
                ModelState.AddModelError("Erreur", "Erreur lors de la récupération du produit : " + e.Message);
            }
            return View(produit);
        }

        // POST: ProduitController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // Call the service method to delete the product
                ServiceE_commerce.SupprimerProduit(id);

                return RedirectToAction(nameof(Index));
            }
            catch (MonException e)
            {
                ModelState.AddModelError("Erreur", "Erreur lors de la suppression du produit : " + e.Message);
                return View();
            }
        }
    }
}
