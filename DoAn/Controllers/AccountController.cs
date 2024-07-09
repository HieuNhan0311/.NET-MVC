using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn.Models;
using DoAn.Identity;
using System.Web.Helpers;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using DoAn.ViewModel;
namespace DoAn.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterVM rmv)
        {
            if (ModelState.IsValid)
            {
                var appDbContext = new AppDbContext();
                var userStore = new AppUserStore(appDbContext);
                var userManager = new AppUserManager(userStore);
                var passwdHash = Crypto.HashPassword(rmv.Password);
                var user = new AppUser()
                {
                    Email = rmv.Email,
                    UserName = rmv.User,
                    PasswordHash = passwdHash,
                    City = rmv.City,
                    BirthDay = rmv.DateOfBirth,
                    Address = rmv.Address,
                    PhoneNumber = rmv.Mobile
                };
                IdentityResult identityResult = userManager.Create(user);
                if(identityResult.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Customer");
                    var authenManager = HttpContext.GetOwinContext().Authentication;
                    var userIdentity = userManager.CreateIdentity(user,DefaultAuthenticationTypes.ApplicationCookie);
                    authenManager.SignIn(new AuthenticationProperties(), userIdentity);
                }
                return RedirectToAction("Index", "SanPham");
            }
            else
            {
                ModelState.AddModelError("New Error", "Invalid Data");
                return View();
            }
            View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginVM lvm)
        {
            var appDbContext = new AppDbContext();
            var userStore = new AppUserStore(appDbContext);
            var userManager = new AppUserManager(userStore);
            var user = userManager.Find(lvm.User, lvm.Password);
            //if(user != null)
            //{
            //    var authenManager = HttpContext.GetOwinContext().Authentication;
            //    var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
            //    authenManager.SignIn(new AuthenticationProperties(), userIdentity);
            //    return RedirectToAction("Index", "SanPham");
            //}
            //else
            //{
            //    ModelState.AddModelError("myError", "Invalid username and password");
            //    return View();
            //}

            if (user != null)
            {
                var authenManager = HttpContext.GetOwinContext().Authentication;
                var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                authenManager.SignIn(new AuthenticationProperties(), userIdentity);
                if (userManager.IsInRole(user.Id, "Admin"))
                {
                    return RedirectToAction("Index", "Products", new { area = "Admin" });
                }
                else
                    return RedirectToAction("index", "SanPham");
            }
            else
            {
                ModelState.AddModelError("Error", "Tên đăng nhập hoặc mật khẩu không đúng");
                return View();
            }
        }
        public ActionResult Logout()
        {
            var authenManager = HttpContext.GetOwinContext().Authentication;
            authenManager.SignOut();
            return RedirectToAction("Index", "SanPham");
        }
    }
}