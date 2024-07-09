using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn.Identity;
using DoAn.Filters;

namespace DoAn.Areas.Admin.Controllers
{
    [AdminAuthorization]
    public class UserController : Controller
    {
        // GET: Admin/User
        public ActionResult Index()
        {
            AppDbContext db = new AppDbContext();
            List<AppUser> users = db.Users.ToList();
            return View(users);
        }
        public ActionResult Delete(string id)
        {
            AppDbContext db = new AppDbContext();
            var itemdel = db.Users.Where(n => n.Id == id).FirstOrDefault();
            return View(itemdel);
        }

        [HttpPost]
        public ActionResult Delete(AppUser u)
        {
            AppDbContext db = new AppDbContext();
            var user = db.Users.Where(n => n.Id == u.Id).FirstOrDefault();
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(AppUser usr)
        {
            AppDbContext db = new AppDbContext();
            db.Users.Add(usr);
            db.SaveChanges();
            return RedirectToAction("Index");
        }




    }
}