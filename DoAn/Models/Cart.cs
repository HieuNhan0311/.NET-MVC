using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoAn.Models
{
    public class Cart
    {
        [Key]
        public int CartID { get; set; }
        public int MaSp { get; set; }
        public int Quantity { get; set; }
        public virtual SanPham SanPham { get; set; }

    }
}