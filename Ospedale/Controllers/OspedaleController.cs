using Ospedale.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace OspedaleProgettoFinale.Controllers
{
    public class OspedaleController : Controller
    {
        ModelDbContext DbContext = new ModelDbContext();
        public List<SelectListItem> ListaMedico
        {
            get
            {
                List<SelectListItem> _templist = new List<SelectListItem>();
                foreach (Medico m in DbContext.Medico.ToList())
                {
                    SelectListItem selectListItem = new SelectListItem
                    {
                        Text = m.Cognome,
                        Value = m.IdMedico.ToString()
                    };
                    _templist.Add(selectListItem);
                }
                return _templist;
            }
        }
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
        public List<SelectListItem> ListaPaziente
        {
            get
            {
                List<SelectListItem> _templist = new List<SelectListItem>();
                foreach (Paziente p in DbContext.Paziente.ToList())
                {
                    SelectListItem selectListItem = new SelectListItem
                    {
                        Text = p.CodiceFiscale,
                        Value = p.IdPaziente.ToString()
                    };
                    _templist.Add(selectListItem);
                }
                return _templist;
            }
        }
        public List<SelectListItem> ListaFarmaco
        {
            get
            {
                List<SelectListItem> _templist = new List<SelectListItem>();
                foreach (Farmaco f in DbContext.Farmaco.ToList())
                {
                    SelectListItem selectListItem = new SelectListItem
                    {
                        Text = f.NomeFarmaco,
                        Value = f.IdFarmaco.ToString()
                    };
                    _templist.Add(selectListItem);
                }
                return _templist;
            }
        }

        // PAZIENTE

        public ActionResult ListaPazienti()
        {

            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    string admin = Request.Cookies["USER_COOKIE"]["ID"];

                    if (admin == "Admin")
                    {
                        return View(DbContext.Paziente.ToList());
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

                throw;
            }
        }

        public ActionResult CreaPaziente()
        {
            
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    string admin = Request.Cookies["USER_COOKIE"]["ID"];

                    User u = DbContext.User.Where(x => x.Ruolo == "Admin").FirstOrDefault();
                    if (u.Ruolo == "Admin")
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
            catch (Exception)
            {
                ViewBag.Errore = "Non hai i privilegi per accedere a questa pagina, accedi con le credenziali opportune";
                return RedirectToAction("Login", "Login");
            }
        }

        [HttpPost]
        public ActionResult CreaPaziente(Paziente p)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    string admin = Request.Cookies["USER_COOKIE"]["ID"];

                    if (admin == "Admin")
                    {
                        Paziente p1 = new Paziente();
                        Visita v = new Visita();
                        if (ModelState.IsValid)
                        {
                            p1 = DbContext.Paziente.Where(x => x.CodiceFiscale == p.CodiceFiscale).FirstOrDefault();
                            if (p1 != null)
                            {
                                p1 = p;
                                TempData["Errore"] = "Paziente già esistente!";
                                return RedirectToAction("ListaPazienti");
                            }
                            else
                            {
                                DbContext.Paziente.Add(p);
                                DbContext.SaveChanges();
                                return RedirectToAction("ListaPazienti");
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
            catch (Exception)
            {

                ViewBag.Errore = "Non hai i privilegi per accedere a questa pagina, accedi con le credenziali opportune";
                return RedirectToAction("Login", "Login");
            }
        }

        public ActionResult ModificaPaziente(int id)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    string admin = Request.Cookies["USER_COOKIE"]["ID"];

                    if (admin == "Admin")
                    {
                        Paziente p = DbContext.Paziente.Find(id);
                        return View(p);
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
        public ActionResult ModificaPaziente(Paziente p)
        {

            if (User.Identity.IsAuthenticated)
            {
                string admin = Request.Cookies["USER_COOKIE"]["ID"];

                if (admin == "Admin")
                {
                    if (ModelState.IsValid)
                    {
                        try
                        {
                            DbContext.Entry(p).State = System.Data.Entity.EntityState.Modified;
                            DbContext.SaveChanges();

                            return RedirectToAction("ListaPazienti");
                        }
                        catch (Exception ex)
                        {
                            ViewBag.Errore = "Errore nella modifca del paziente";
                            return View();
                        }
                    }
                    else
                    {
                        ViewBag.Errore = "Errore nella modifca del paziente";
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

        public ActionResult CancellaPaziente(int id)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    string admin = Request.Cookies["USER_COOKIE"]["ID"];

                    if (admin == "Admin")
                    {
                        Paziente p = DbContext.Paziente.Find(id);
                        return View(p);
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
        [ActionName("CancellaPaziente")]
        public ActionResult CancellaPazientePost(int id)
        {
            string admin = Request.Cookies["USER_COOKIE"]["ID"];
            if (User.Identity.IsAuthenticated)
            {
                if (admin == "Admin")
                {

                    Paziente p = DbContext.Paziente.Find(id);
                    if (id == p.IdPaziente)
                    {
                        DbContext.Paziente.Remove(p);
                        DbContext.SaveChanges();
                    }
                    else
                    {
                        ViewBag.Errore = "Si è verificato un errore nella eliminazione";
                        return View();
                    }

                    return RedirectToAction("ListaPazienti");

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

        public ActionResult DettagliPaziente(int id)
        {
            string admin = Request.Cookies["USER_COOKIE"]["ID"];

            if (User.Identity.IsAuthenticated)
            {
                if (admin == "Admin")
                {
                    Paziente p = DbContext.Paziente.Find(id);
                    return View(p);
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

        // VISITA

        public ActionResult ListaVisite()
        {
            

            if (User.Identity.IsAuthenticated)
            {
                string admin = Request.Cookies["USER_COOKIE"]["ID"];

                if (admin == "Admin")
                {
                    return View(DbContext.Visita.ToList());
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

        public ActionResult CreaVisita()
        {

            
            ViewBag.ListaReparto = ListaReparto;
            ViewBag.ListaMedico = ListaMedico;
            ViewBag.ListaPaziente = ListaPaziente;
            ViewBag.ListaFarmaco = ListaFarmaco;

            if (User.Identity.IsAuthenticated)
            {

                string admin = Request.Cookies["USER_COOKIE"]["ID"];

                if (admin == "Admin")
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
        public ActionResult CreaVisita(Visita v)
        {
            

            if (User.Identity.IsAuthenticated)
            {
                string admin = Request.Cookies["USER_COOKIE"]["ID"];

                if (admin == "Admin")
                {
                    if (ModelState.IsValid)
                    {
                        try
                        {
                            DbContext.Visita.Add(v);
                            DbContext.SaveChanges();
                            return RedirectToAction("ListaVisite");

                        }
                        catch (Exception)
                        {
                            ViewBag.Errore = "Errore nell'aggiunta della visita";
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

        [HttpPost]
        public ActionResult ModificaVisita(Visita v)
        {
            if (User.Identity.IsAuthenticated)
            {

                string admin = Request.Cookies["USER_COOKIE"]["ID"];

                if (admin == "Admin")
                {
                    if (ModelState.IsValid)
                    {
                        try
                        {
                            DbContext.Entry(v).State = System.Data.Entity.EntityState.Modified;
                            DbContext.SaveChanges();

                            return RedirectToAction("ListaVisite");
                        }
                        catch (Exception ex)
                        {
                            ViewBag.Errore = "Errore nella modifca della visita";
                            return View();
                        }
                    }
                    else
                    {
                        ViewBag.Errore = "Errore nella modifca della visita";
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

        public ActionResult ModificaVisita(int id)
        {
            

            ViewBag.ListaReparto = ListaReparto;
            ViewBag.ListaMedico = ListaMedico;
            ViewBag.ListaPaziente = ListaPaziente;
            ViewBag.ListaFarmaco = ListaFarmaco;
            if (User.Identity.IsAuthenticated)
            {

                string admin = Request.Cookies["USER_COOKIE"]["ID"];
                if (admin == "Admin")
                {
                    Visita v = DbContext.Visita.Find(id);
                    return View(v);
                }
                else
                {
                    ViewBag.Errore = "Non hai i privilegi per accedere a questa pagina, accedi con le credenziali opportune";
                    return RedirectToAction("Login");
                }
            }
            else
            {
                ViewBag.Errore = "Non hai i privilegi per accedere a questa pagina, accedi con le credenziali opportune";
                return RedirectToAction("Login", "Login");
            }
        }

        public ActionResult CancellaVisita(int id)
        {
            

            if (User.Identity.IsAuthenticated)
            {
                string admin = Request.Cookies["USER_COOKIE"]["ID"];

                if (admin == "Admin")
                {
                    Visita v = DbContext.Visita.Find(id);
                    return View(v);
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
        [ActionName("CancellaVisita")]
        public ActionResult CancellaVisitaPost(int id)
        {

            if (User.Identity.IsAuthenticated)
            {

                string admin = Request.Cookies["USER_COOKIE"]["ID"];

                if (admin == "Admin")
                {
                    Visita v = DbContext.Visita.Find(id);
                    if (id == v.IdVisita)
                    {
                        DbContext.Visita.Remove(v);
                        DbContext.SaveChanges();
                    }
                    else
                    {
                        ViewBag.Errore = "Si è verificato un errore nella eliminazione";
                        return View();
                    }

                    return RedirectToAction("ListaVisite");
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

        //JSON
        public ActionResult FunzioniAsincrone()
        {

            if (User.Identity.IsAuthenticated)
            {

                string admin = Request.Cookies["USER_COOKIE"]["ID"];

                if (admin == "Admin")
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

        public JsonResult RicercaPerData(DateTime s)
        {
            List<Visita> _listaVisite = new List<Visita>();
            Visita v = new Visita();
            try
            {
                _listaVisite = DbContext.Visita.Where(x => x.DataVisita < s).ToList();
                List<DataToJson> listatoReturn = new List<DataToJson>();
                foreach (Visita visita in _listaVisite)
                {
                    DataToJson dateToJson = new DataToJson
                    {
                        Medico = visita.Medico.Cognome,
                        Reparto = visita.Reparto.NomeReparto,
                        Paziente = visita.Paziente.CodiceFiscale,
                        DataVisita = visita.DataVisita.ToString(),
                        StatoPaziente = visita.StatoPaziente,
                        PressioneMassima = visita.PressioneMassima,
                        PressioneMinima = visita.PressioneMinima,
                        Temperatura = visita.Temperatura,
                        Farmaco = visita.Farmaco.NomeFarmaco,
                        Posologia = visita.Posologia.ToString()
                    };
                    listatoReturn.Add(dateToJson);
                }

                return Json(listatoReturn, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json("KO", JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult RicercaPerReparto(string s)
        {
            int count = 0;
            List<Visita> visita = new List<Visita>();
            try
            {
                visita = DbContext.Visita.Where(x => x.Reparto.NomeReparto == s).ToList();
                for(int i = 0; i < visita.Count();)
                {
                    i++;
                    count = i;
                }

                return Json(count, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                return Json("KO", JsonRequestBehavior.AllowGet);
            }
        }

        public class DataToJson
        {
            public string Medico { get; set; }
            public string Reparto { get; set; }
            public string Paziente { get; set; }
            public string DataVisita { get; set; }
            public string StatoPaziente { get; set; }

            public int PressioneMinima { get; set; }

            public int PressioneMassima { get; set; }

            public decimal Temperatura { get; set; }

            public int FrequenzaCardiaca { get; set; }
            public string Farmaco { get; set; }

            public string Posologia { get; set; }
        }

        //MEDICI

        public ActionResult ListaMedici()
        {
            ViewBag.ListaReparto = ListaReparto;

            if (User.Identity.IsAuthenticated)
            {
                string admin = Request.Cookies["USER_COOKIE"]["ID"];

                if (admin == "Admin")
                {
                    return View(DbContext.Medico.ToList());
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

        public ActionResult CreaMedico()
        {
            ViewBag.ListaReparto = ListaReparto;

            if (User.Identity.IsAuthenticated)
            {

                string admin = Request.Cookies["USER_COOKIE"]["ID"];

                if (admin == "Admin")
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
        public ActionResult CreaMedico(Medico m)
        {


            if (User.Identity.IsAuthenticated)
            {
                string admin = Request.Cookies["USER_COOKIE"]["ID"];

                if (admin == "Admin")
                {
                    if (ModelState.IsValid)
                    {
                        try
                        {
                            DbContext.Medico.Add(m);
                            DbContext.SaveChanges();
                            return RedirectToAction("ListaMedici");

                        }
                        catch (Exception)
                        {
                            ViewBag.Errore = "Errore nell'aggiunta del medico";
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

        public ActionResult ModificaMedico(int id)
        {
            ViewBag.ListaReparto = ListaReparto;

            if (User.Identity.IsAuthenticated)
            {

                string admin = Request.Cookies["USER_COOKIE"]["ID"];
                if (admin == "Admin")
                {
                    Medico m = DbContext.Medico.Find(id);
                    return View(m);
                }
                else
                {
                    ViewBag.Errore = "Non hai i privilegi per accedere a questa pagina, accedi con le credenziali opportune";
                    return RedirectToAction("Login");
                }
            }
            else
            {
                ViewBag.Errore = "Non hai i privilegi per accedere a questa pagina, accedi con le credenziali opportune";
                return RedirectToAction("Login", "Login");
            }
        }

        [HttpPost]
        public ActionResult ModificaMedico(Medico m)
        {
            if (User.Identity.IsAuthenticated)
            {

                string admin = Request.Cookies["USER_COOKIE"]["ID"];

                if (admin == "Admin")
                {
                    if (ModelState.IsValid)
                    {
                        try
                        {
                            DbContext.Entry(m).State = System.Data.Entity.EntityState.Modified;
                            DbContext.SaveChanges();

                            return RedirectToAction("ListaMedici");
                        }
                        catch (Exception ex)
                        {
                            ViewBag.Errore = "Errore nella modifca del medico";
                            return View();
                        }
                    }
                    else
                    {
                        ViewBag.Errore = "Errore nella modifca della visita";
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

        public ActionResult CancellaMedico(int id)
        {
            ViewBag.ListaReparto = ListaReparto;

            if (User.Identity.IsAuthenticated)
            {
                string admin = Request.Cookies["USER_COOKIE"]["ID"];

                if (admin == "Admin")
                {
                    Medico m = DbContext.Medico.Find(id);
                    return View(m);
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
        [ActionName("CancellaMedico")]
        public ActionResult CancellaMedicoPost(int id)
        {

            if (User.Identity.IsAuthenticated)
            {

                string admin = Request.Cookies["USER_COOKIE"]["ID"];

                if (admin == "Admin")
                {
                    Medico m = DbContext.Medico.Find(id);
                    if (id == m.IdMedico)
                    {
                        DbContext.Medico.Remove(m);
                        DbContext.SaveChanges();
                    }
                    else
                    {
                        ViewBag.Errore = "Si è verificato un errore nella eliminazione";
                        return View();
                    }

                    return RedirectToAction("ListaMedici");
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

    }
}