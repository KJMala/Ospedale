using Ospedale.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Ospedale.Controllers
{
    public class LoginController : Controller
    {
        ModelDbContext DbContext = new ModelDbContext();

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(User u)
        {

            User user = DbContext.User.Where(x => x.Username == u.Username && x.Password == u.Password).FirstOrDefault();
            if (user != null )
            {
                if (user.Ruolo == "Farmacia")
                {
                    HttpCookie cookiePropID = new HttpCookie("USER_COOKIE");
                    cookiePropID.Values["ID"] = user.Ruolo;
                    Response.Cookies.Add(cookiePropID);
                    FormsAuthentication.SetAuthCookie(u.Username, false);
                    return RedirectToAction("ListaFarmaci", "Farmacia");
                }
                else
                {
                    HttpCookie cookiePropID = new HttpCookie("USER_COOKIE");
                    cookiePropID.Values["ID"] = user.Ruolo;
                    Response.Cookies.Add(cookiePropID);
                    FormsAuthentication.SetAuthCookie(u.Username, false);
                    return RedirectToAction("ListaVisite", "Ospedale");
                }
            }

            else
            {
                ViewBag.Errore = "Autenticazione non riuscita";
                return View();
            }

        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect(FormsAuthentication.DefaultUrl);
        }
    }
}