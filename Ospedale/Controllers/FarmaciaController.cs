using Ospedale.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ospedale.Controllers
{
    public class FarmaciaController : Controller
    {
        ModelDbContext DbContext = new ModelDbContext();
        public List<SelectListItem> ListaReparto
        {
            get
            {
                List<SelectListItem> _templist = new List<SelectListItem>();
                foreach (Reparto r in DbContext.Reparto.ToList())
                {
                    SelectListItem selectListItem = new SelectListItem
                    {
                        Text = r.NomeReparto,
                        Value = r.IdReparto.ToString()
                    };
                    _templist.Add(selectListItem);
                }
                return _templist;
            }
        }
        // FARMACI
        public ActionResult ListaFarmaci()
        {
            
            if (User.Identity.IsAuthenticated)
            {
                string farmacia = Request.Cookies["USER_COOKIE"]["ID"];
                if (farmacia == "Farmacia")
                {
                    return View(DbContext.Farmaco.ToList());
                }
                else
                {
                    ViewBag.Errore = "Non hai i privilegi per accedere a questa pagina, accedi con le credenziali opportune";
                    return RedirectToAction("Login", "Login");
                }
            }
            else
            {
                ViewBag.Errore = "Non hai i privilegi per accedere a questa pagina, accedi con le credenziali opportune";
                return RedirectToAction("Login", "Login");
            }
        }

        public ActionResult CreaFarmaco()
        {
            ViewBag.ListaReparto = ListaReparto;
            if (User.Identity.IsAuthenticated)
            {
                string farmacia = Request.Cookies["USER_COOKIE"]["ID"];
                if (farmacia == "Farmacia")
                {
                    return View();
                }
                else
                {
                    ViewBag.Errore = "Non hai i privilegi per accedere a questa pagina, accedi con le credenziali opportune";
                    return RedirectToAction("Login", "Login");
                }
            }
            else
            {
                ViewBag.Errore = "Non hai i privilegi per accedere a questa pagina, accedi con le credenziali opportune";
                return RedirectToAction("Login", "Login");
            }

        }

        [HttpPost]
        public ActionResult CreaFarmaco(Farmaco f)
        {
            
            if (User.Identity.IsAuthenticated)
            {
                string farmacia = Request.Cookies["USER_COOKIE"]["ID"];
                if (farmacia == "Farmacia")
                {
                    if (ModelState.IsValid)
                    {
                        try
                        {
                            DbContext.Farmaco.Add(f);
                            DbContext.SaveChanges();
                            return RedirectToAction("ListaFarmaci");
                        }
                        catch (Exception ex)
                        {
                            ViewBag.Errore = "Errore nell'aggiunta del farmaco";
                            return View();
                        }
                    }
                    else
                    {
                        ViewBag.Errore = "Errore nella compilazione";
                        return View();
                    }
                }
                else
                {
                    ViewBag.Errore = "Non hai i privilegi per accedere a questa pagina, accedi con le credenziali opportune";
                    return RedirectToAction("Login", "Login");
                }
            }
            else
            {
                ViewBag.Errore = "Non hai i privilegi per accedere a questa pagina, accedi con le credenziali opportune";
                return RedirectToAction("Login", "Login");
            }
        }

        public ActionResult ModificaFarmaco(int id)
        {

            string farmacia = Request.Cookies["USER_COOKIE"]["ID"];
            if (User.Identity.IsAuthenticated)
            {
                if (farmacia == "Farmacia")
                {

                    Farmaco f = DbContext.Farmaco.Find(id);
                    ViewBag.ListaReparto = ListaReparto;
                    return View(f);
                }
                else
                {
                    ViewBag.Errore = "Non hai i privilegi per accedere a questa pagina, accedi con le credenziali opportune";
                    return RedirectToAction("Login", "Login");
                }
            }
            else
            {
                ViewBag.Errore = "Non hai i privilegi per accedere a questa pagina, accedi con le credenziali opportune";
                return RedirectToAction("Login", "Login");
            }
        }

        [HttpPost]
        public ActionResult ModificaFarmaco(Farmaco f)
        {
            
            if (User.Identity.IsAuthenticated)
            {
                string farmacia = Request.Cookies["USER_COOKIE"]["ID"];

                if (farmacia == "Farmacia")
                {
                    if (ModelState.IsValid)
                    {
                        try
                        {
                            DbContext.Entry(f).State = System.Data.Entity.EntityState.Modified;
                            DbContext.SaveChanges();

                            return RedirectToAction("ListaFarmaci");
                        }
                        catch (Exception ex)
                        {
                            ViewBag.Errore = "Errore nella modifca del farmaco";
                            return View();
                        }
                    }
                    else
                    {
                        ViewBag.Errore = "Errore nella modifca del farmaco";
                        return View();
                    }
                }
                else
                {
                    ViewBag.Errore = "Non hai i privilegi per accedere a questa pagina, accedi con le credenziali opportune";
                    return RedirectToAction("Login", "Login");
                }
            }
            else
            {
                ViewBag.Errore = "Non hai i privilegi per accedere a questa pagina, accedi con le credenziali opportune";
                return RedirectToAction("Login", "Login");
            }
        }

        public ActionResult CancellaFarmaco(int id)
        {
            try
            {
                
                if (User.Identity.IsAuthenticated)
                {
                    string farmacia = Request.Cookies["USER_COOKIE"]["ID"];

                    if (farmacia == "Farmacia")
                    {
                        Farmaco f = DbContext.Farmaco.Find(id);
                        return View(f);
                    }
                    else
                    {
                        ViewBag.Errore = "Non hai i privilegi per accedere a questa pagina, accedi con le credenziali opportune";
                        return RedirectToAction("Login", "Login");
                    }
                }
                else
                {
                    ViewBag.Errore = "Non hai i privilegi per accedere a questa pagina, accedi con le credenziali opportune";
                    return RedirectToAction("Login", "Login");
                }
            }
            catch (Exception)
            {
                ViewBag.Errore = "Non hai i privilegi per accedere a questa pagina, accedi con le credenziali opportune";
                return RedirectToAction("Login", "Login");
            }

            
        }

        [HttpPost]
        [ActionName("CancellaFarmaco")]
        public ActionResult CancellaFarmacoPost(int id)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    string farmacia = Request.Cookies["USER_COOKIE"]["ID"];

                    if (farmacia == "Farmacia")
                    {
                       
                        Farmaco f = DbContext.Farmaco.Find(id);
                        if (id == f.IdFarmaco)
                        {
                            DbContext.Farmaco.Remove(f);
                            DbContext.SaveChanges();
                        }
                        else
                        {
                            ViewBag.Errore = "Si è verificato un errore nella eliminazione";
                            return View();
                        }
                        return RedirectToAction("ListaFarmaci");
                    }
                    else
                    {
                        ViewBag.Errore = "Non hai i privilegi per accedere a questa pagina, accedi con le credenziali opportune";
                        return RedirectToAction("Login", "Login");
                    }
                }
                else
                {
                    ViewBag.Errore = "Non hai i privilegi per accedere a questa pagina, accedi con le credenziali opportune";
                    return RedirectToAction("Login", "Login");
                }
            }
            catch (Exception)
            {

                ViewBag.Errore = "Non hai i privilegi per accedere a questa pagina, accedi con le credenziali opportune";
                return RedirectToAction("Login", "Login");
            }
  
        }

    }
}