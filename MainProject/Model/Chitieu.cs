using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MainProject.Models
{
    public class taikhoan
    {
        public int Id { get; set; }
        public string user { get; set; }
        public string password { get; set; }
        public double sotien { get; set; }
    }

    public class giaodich
    {
        public int Id { get; set; }
        public double sotien { get; set; }
        public string LoaiGD { get; set; } 

        public int IDtk { get; set; }
        public int IDdanhmuc { get; set; }
        public DateTime ngay { get; set; }
        public string ghichu { get; set; }
    }

    public class danhmuc
    {
        public int Id { get; set; }
        public string ten { get; set; }
        public string LoaiGD { get; set; } 
    }
    public class GiaoDichDisplay
    {
        public int Id { get; set; }
        public double SoTien { get; set; }
        public string LoaiGD { get; set; } 
        public string TenDanhMuc { get; set; } 
        public string Ngay { get; set; }
        public string GhiChu { get; set; }

        
        public Color MauSoTien => LoaiGD == "Thu" ? Colors.Green : Colors.Red;
        public string DinhDangSoTien => LoaiGD == "Thu" ? $"+{SoTien:N0} đ" : $"-{SoTien:N0} đ";
    }
}