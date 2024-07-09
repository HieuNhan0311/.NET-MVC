using DoAn.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using DoAn.Filters;

namespace DoAn.Areas.Admin.Controllers
{
    [AdminAuthorization]
    public class ProductsController : Controller
    {
        // GET: Admin/Products
        [MyAuthenFilter]
        public ActionResult Index(string sortOrder = "", string search = "", int Page = 1)
        {
            MyDBConText db = new MyDBConText();
            ViewBag.search = search;
            ViewBag.sortOrder = sortOrder;
            List<Loai> l = db.Loai.ToList();
            ViewBag.Loai = l;

            List<SanPham> sp = db.SanPham.ToList();
            if (!string.IsNullOrEmpty(search))
            {
                sp = db.SanPham.Where(row => row.TenSP.Contains(search)).ToList();
            }
            else
            {
                sp = db.SanPham.ToList();
            }
            switch (sortOrder)
            {
                case "namesp":
                    sp = sp.OrderBy(row => row.TenSP).ToList();
                    break;
                case "masp":
                    sp = sp.OrderBy(row => row.MaSp).ToList();
                    break;
                case "slt":
                    sp = sp.OrderBy(row => row.SLTon).ToList();
                    break;
            }
            //paging
            int NoOfRecordPerPage = 6;
            int NoOfPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(sp.Count) / Convert.ToDouble(NoOfRecordPerPage)));
            int NoOfRecordToSkip = (Page - 1) * NoOfRecordPerPage;
            ViewBag.Page = Page;
            ViewBag.NoOfPage = NoOfPage;
            sp = sp.Skip(NoOfRecordToSkip).Take(NoOfRecordPerPage).ToList();
            return View(sp);
        }
        public ActionResult add(int id=0)
        {
            MyDBConText db = new MyDBConText();
            ViewBag.Loai = db.Loai.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult add(SanPham pro)
        {
            MyDBConText db = new MyDBConText();
            if (ModelState.IsValid)
            {
                db.SanPham.Add(pro); 

                db.SaveChanges();
                int categoryID = pro.MaLoai;
                return RedirectToAction("index");
            }
            else
            {
                ViewBag.Loai = db.Loai.ToList();
                return RedirectToAction("Create");
            }
        }
        public ActionResult Edit(int id)
        {
            MyDBConText db = new MyDBConText();
            SanPham pro = db.SanPham.Where(row => row.MaSp == id).FirstOrDefault();
            //navigation
            ViewBag.Loai = db.Loai.ToList();
            return View(pro);
        }
        [HttpPost]
        public ActionResult Edit(int id, SanPham pro)
        {
            MyDBConText db = new MyDBConText();
            // Check if the product with the specified ID exists
            SanPham Product = db.SanPham.Where(row => row.MaSp == id).FirstOrDefault();
            if (Product != null)
            {
                // Update the product's properties
                Product.TenSP = pro.TenSP;
                //Product.Anh = pro.Anh;
                Product.MaLoai = pro.MaLoai;
                Product.MoTa = pro.MoTa;
                Product.GiaBan = pro.GiaBan;
                int categoryID = pro.MaLoai;
                // Update the product in the database
                db.SaveChanges();
                return RedirectToAction("index");


            }
            else
            {

                return RedirectToAction("Edit");
            }

        }
        public ActionResult Delete(int id)
        {
            MyDBConText db = new MyDBConText();
            SanPham pro = db.SanPham.Where(row => row.MaSp == id).FirstOrDefault();
            return View(pro);
        }
        [HttpPost]
        public ActionResult Delete(int id, SanPham p)
        {
            MyDBConText db = new MyDBConText();
            SanPham pro = db.SanPham.Where(row => row.MaSp == id).FirstOrDefault();
            db.SanPham.Remove(pro);
            db.SaveChanges();
            return RedirectToAction("index");

        }



    }
}