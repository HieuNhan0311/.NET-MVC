using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoAn.Models
{
    public class SanPham
    {
        [Key]
        public int MaSp { get; set; }
        public int MaLoai { get; set; }
        public string TenSP { get; set; }
        public string DVT { get; set; }
        public string MoTa { get; set; }
        public int SLTon { get; set; }
        public string Anh { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public decimal GiaBan { get; set; }
        public string KichThuoc { get; set; }


        public virtual Loai Loai { get; set; }
    }
}