using CDA.Models;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static System.Net.WebRequestMethods;

namespace CDA.Controllers
{
    public class AuteurController : Controller
    {



        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "lYpEw9gzq9Pvwe3z2QkIpPQkBit7KVQCPSUSuMyt",
            BasePath = "https://cdaprojet2-default-rtdb.firebaseio.com"
        };

        IFirebaseClient? Client;

        // GET: AuteurController : Permet de récuperer tout les auteurs 
        public ActionResult Index()
        {

            Client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = Client.Get("Auteurs");
            dynamic? data = JsonConvert.DeserializeObject<dynamic>(response.Body);
            var list = new List<Auteur>();

            if (data != null)
            {   

                foreach (var item in data)
                {
                    list.Add(JsonConvert.DeserializeObject<Auteur>(((JProperty)item).Value.ToString()));
                }

            }
            return View(list);
        }

        // GET: AuteurController/Details/5 Permet de récuprepr 1 seul auteur en fonction de son ID
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AuteurController/Create : Afficher la vue de création 
        public ActionResult Create()
        {
            return View();
        }

        // POST: AuteurController/Create Pour enregistrer un nouveau Auteur
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Auteur aut)
        {
            try
            {
                Client = new FireSharp.FirebaseClient(config);
                PushResponse response = Client.Push("Auteurs/", aut);
                aut.IdAuteur = response.Result.name;
                SetResponse setresponse = Client.Set("Auteurs/" + aut.IdAuteur, aut);

                if (setresponse.StatusCode == System.Net.HttpStatusCode.OK)
                    ModelState.AddModelError(string.Empty, "OK");
                else
                    ModelState.AddModelError(string.Empty, "KO!");

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: AuteurController/Edit/5 Editer un auteur en fonction de son id
        public ActionResult Edit(string id)
        {

            Client = new FirebaseClient(config);
            FirebaseResponse response = Client.Get("Auteurs/" + id);
            Auteur aut = JsonConvert.DeserializeObject<Auteur>(response.Body);
            return View(aut);
           
        }

        // POST: AuteurController/Edit/5  Pour modifier un auteur 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Auteur aut)
        {
            Client = new FireSharp.FirebaseClient(config);
            SetResponse response = Client.Set("Auteurs/" + aut.IdAuteur, aut);
            return RedirectToAction("Index");
        }

        // GET: AuteurController/Delete/5 Affichager vue suppression  un auteur en fonction de son id
        public ActionResult Delete(string id)
        {
            Client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = Client.Get("Auteurs/" + id);
            Auteur aut = JsonConvert.DeserializeObject<Auteur>(response.Body);
            return View(aut);
        }

        // POST: AuteurController/Delete/5 Supprimer un Auteur de la bdd
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, Auteur aut)
        {
            Client = new FireSharp.FirebaseClient(config);

            FirebaseResponse response = Client.Delete("Auteurs/" + id);
            return RedirectToAction("Index");
        }
    }
}
