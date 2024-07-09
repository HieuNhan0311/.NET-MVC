using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn.Models;

namespace DoAn.Controllers
{
    public class CartController : Controller
    {
        //GET: Cart
        public ActionResult Index()
        {
            MyDBConText db = new MyDBConText();
            List<Cart> Carts = db.Cart.ToList();
            foreach (var cart in Carts)
            {
                var sanPham = db.SanPham.FirstOrDefault(s => s.MaSp == cart.MaSp);
                if (sanPham != null)
                {
                    ViewBag.Id = sanPham.MaSp;
                }
            }
            return View(Carts);
        }

        public ActionResult AddToCart(int productId = 0)
        {
            if (productId > 0)
            {
                MyDBConText db = new MyDBConText();
                Cart cartItem = db.Cart.Where(c => c.MaSp == productId).FirstOrDefault();
                if (cartItem != null)
                {
                    cartItem.Quantity += 1;
                }
                else
                {
                    Cart cart = new Cart();
                    cart.MaSp = productId ;
                    cart.Quantity = 1;
                    db.Cart.Add(cart);
                }
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Cart");

        }

        public ActionResult DeletePro(int id)
        {
            if (id > 0)
            {
                MyDBConText db = new MyDBConText();
                Cart cartItem = db.Cart.FirstOrDefault(c => c.MaSp == id);

                if (cartItem != null)
                {
                    db.Cart.Remove(cartItem);
                    db.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }
        public ActionResult UpdateQuantity(int quan = 1, int proid = 0)
        {
            MyDBConText db = new MyDBConText();
            if (quan > 0)
            {
                Cart cartItem = db.Cart.Where(c => c.MaSp == proid).FirstOrDefault();
                if (cartItem != null)
                {
                    cartItem.Quantity = quan;
                    db.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }
    }
}