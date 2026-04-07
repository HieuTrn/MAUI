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
}