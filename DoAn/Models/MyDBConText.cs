using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace DoAn.Models
{
    public class MyDBConText:DbContext
    {
        public MyDBConText() : base("myConnect") { }
        public DbSet<SanPham> SanPham { get; set; }
        public DbSet<Loai> Loai { get; set; }
        public DbSet<Cart> Cart { get; set; }
    }
}