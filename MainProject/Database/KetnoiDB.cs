
using MainProject.Models;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MainProject.Database
{

    public class KetnoiDB
    {
        private readonly string dbpath = Path.Combine(FileSystem.AppDataDirectory, "chitieu.db");
        private readonly string connect;


        public KetnoiDB()
        {
            connect = $"Data Source={dbpath}";

            Console.WriteLine("==================================================");
            Console.WriteLine(" ĐƯỜNG DẪN DATABASE FILE SQLite NẰM Ở ĐÂY: ");// lấy đường dẫn db ra để mở lên xem có chạy được k
            Console.WriteLine(dbpath);                                       // nhớ xoá đi nếu cbi nộp
            Console.WriteLine("==================================================");

        }

        public void Taodb()
        {
            using (var connection = new SqliteConnection(connect))
            {
                connection.Open();
                var truyvan = connection.CreateCommand();
                truyvan.CommandText =
                    @"CREATE TABLE IF NOT EXISTS taikhoan (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    user TEXT,
                    password TEXT,
                    sotien REAL NOT NULL);

                    CREATE TABLE IF NOT EXISTS danhmuc (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    ten TEXT NOT NULL,
                    LoaiGD TEXT NOT NULL);

                    CREATE TABLE IF NOT EXISTS giaodich (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    sotien REAL NOT NULL,
                    LoaiGD TEXT NOT NULL,
                    IDtk INTEGER,
                    IDdanhmuc INTEGER,
                    ngay TEXT,
                    ghichu TEXT,
                    FOREIGN KEY (IDtk) REFERENCES taikhoan(Id),
                    FOREIGN KEY (IDdanhmuc) REFERENCES danhmuc(Id));";

                truyvan.ExecuteNonQuery();
            }

        }
        public void themTK(string user, string pass)
        {
            using (var connection = new SqliteConnection(connect))
            {
                connection.Open();
                var truyvan = connection.CreateCommand();
                truyvan.CommandText =
                    @"INSERT INTO taikhoan(user, password , sotien) VALUES ($user , $password, $sotien);";
                truyvan.Parameters.AddWithValue("$user", user);
                truyvan.Parameters.AddWithValue("$password", pass);
                truyvan.Parameters.AddWithValue("$sotien", 0);
                truyvan.ExecuteNonQuery();
            }
        }

        public void ThemGd(double soTien, string loaiGD, int idVi, int idDanhMuc, DateTime ngayGD, string ghiChu)
        {
            using (var connection = new SqliteConnection(connect))
            {
                connection.Open();
                var truyvan = connection.CreateCommand();

                truyvan.CommandText = @"
            INSERT INTO giaodich (sotien, LoaiGD, IDtk, IDdanhmuc, ngay, ghichu) 
            VALUES ($sotien, $loai, $idtk, $iddanhmuc, $ngay, $ghichu)";

                truyvan.Parameters.AddWithValue("$sotien", soTien);
                truyvan.Parameters.AddWithValue("$loai", loaiGD);
                truyvan.Parameters.AddWithValue("$idtk", idVi);
                truyvan.Parameters.AddWithValue("$iddanhmuc", idDanhMuc);
                truyvan.Parameters.AddWithValue("$ngay", ngayGD.ToString("s"));
                truyvan.Parameters.AddWithValue("$ghichu", ghiChu);
                truyvan.ExecuteNonQuery();
            }
        }
        public ObservableCollection<taikhoan> laytk()
        {
            using (var connection = new SqliteConnection(connect))
            {
                connection.Open();
                var ds = new ObservableCollection<taikhoan>();
                var truyvan = connection.CreateCommand();
                truyvan.CommandText = @"SELECT Id, user, password , sotien FROM taikhoan";
                using (var doc = truyvan.ExecuteReader())
                {
                    while (doc.Read())
                    {
                        ds.Add(new taikhoan
                        {
                            Id = doc.GetInt32(0),
                            user = doc.GetString(1),
                            password = doc.GetString(2),
                            sotien = doc.GetDouble(3)
                        });
                    }
                }
                return ds;
            }
        }

        public ObservableCollection<danhmuc> layDanhmuc()
        {
            using (var connection = new SqliteConnection(connect))
            {
                connection.Open();
                var ds = new ObservableCollection<danhmuc>();
                var truyvan = connection.CreateCommand();
                truyvan.CommandText = @"SELECT Id, ten, LoaiGD FROM danhmuc";
                using (var doc = truyvan.ExecuteReader())
                {
                    while (doc.Read())
                    {
                        ds.Add(new danhmuc
                        {
                            Id = doc.GetInt32(0),
                            ten = doc.GetString(1),
                            LoaiGD = doc.GetString(2)
                        });
                    }
                }
                return ds;
            }
        }

        public void ThemDanhmuc(string ten, string loaigd)
        {
            using (var connection = new SqliteConnection(connect))
            {
                connection.Open();
                var truyvan = connection.CreateCommand();
                truyvan.CommandText = @"INSERT INTO danhmuc(ten, LoaiGD) VALUES ($name , $GD);";
                truyvan.Parameters.AddWithValue("$name", ten);
                truyvan.Parameters.AddWithValue("$GD", loaigd);
                truyvan.ExecuteNonQuery();
            }
        }

        public bool XoaDanhmuc(int Id)
        {
            using (var connection = new SqliteConnection(connect))
            {
                connection.Open();
                var check = connection.CreateCommand();
                check.CommandText = "SELECT COUNT(*) FROM giaodich WHERE IDdanhmuc = @Id";
                check.Parameters.AddWithValue("@Id", Id);
                long count = (long)check.ExecuteScalar();
                if (count > 0) { return false; }
                var truyvan = connection.CreateCommand();

                truyvan.CommandText = "DELETE FROM danhmuc WHERE Id = @Id";
                truyvan.Parameters.AddWithValue("@Id", Id);

                truyvan.ExecuteNonQuery();
                return true;
            }
        }

        public void SuaDanhmuc(int Id, string ten, string loaigd)
        {
            using (var connection = new SqliteConnection(connect))
            {
                connection.Open();
                var truyvan = connection.CreateCommand();
                truyvan.CommandText = @"UPDATE danhmuc SET ten = $name, LoaiGD = $GD WHERE Id = $Id;";
                truyvan.Parameters.AddWithValue("$name", ten);
                truyvan.Parameters.AddWithValue("$GD", loaigd);
                truyvan.Parameters.AddWithValue("$Id", Id);
                truyvan.ExecuteNonQuery();
            }
        }
        public ObservableCollection<GiaoDichDisplay> hthidg()
        {
            using (var connection = new SqliteConnection(connect))
            {
                connection.Open();
                var ds = new ObservableCollection<GiaoDichDisplay>();
                var truyvan = connection.CreateCommand();
                truyvan.CommandText = @"
            SELECT 
                g.Id, 
                g.sotien, 
                g.LoaiGD, 
                d.ten AS TenDanhMuc, 
                g.ngay, 
                g.ghichu 
            FROM giaodich g
            INNER JOIN danhmuc d ON g.IDdanhmuc = d.Id
            ORDER BY g.ngay DESC";

                using (var doc = truyvan.ExecuteReader())
                {
                    while (doc.Read())
                    {
                        ds.Add(new GiaoDichDisplay
                        {
                            Id = doc.GetInt32(0),
                            SoTien = doc.GetDouble(1),
                            LoaiGD = doc.GetString(2),
                            TenDanhMuc = doc.GetString(3),
                            Ngay = doc.GetString(4),
                            GhiChu = doc.IsDBNull(5) ? "" : doc.GetString(5)
                        });
                    }
                }
                return ds;
            }
        }
    }
}