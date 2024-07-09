using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using DoAn.Models;
namespace DoAn.Controllers
{
    public class SanPhamController : Controller
    {
        // GET: SanPham
        public ActionResult Index(string sortOrder = "", string search = "", int Page = 1)
        {
            MyDBConText db = new MyDBConText();
            ViewBag.search = search;
            ViewBag.sortOrder = sortOrder;
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



        public ActionResult Detail(int? id)
        {
            MyDBConText db = new MyDBConText();
            SanPham ps = db.SanPham.Where(row => row.MaSp == id).FirstOrDefault();
            return View(ps);
        }
        public ActionResult ProductPhanLoai(int? id, string sortOrder = "")
        {
            MyDBConText db = new MyDBConText();
            List<SanPham> sp = db.SanPham.Where(row => row.MaLoai == id).ToList();
            Loai loais = db.Loai.Where(x => x.MaLoai == id).FirstOrDefault();
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
            //ViewBag.Loai = loais.TenLoai;
            return View(sp);
        }


    }
}