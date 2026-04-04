
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
                    password TEXT);

                   CREATE TABLE IF NOT EXISTS vi (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    ten TEXT NOT NULL,
                    sotien REAL NOT NULL,
                    IDtk INTEGER,
                    FOREIGN KEY (IDtk) REFERENCES taikhoan(Id));
                   CREATE TABLE IF NOT EXISTS danhmuc (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        ten TEXT NOT NULL,
                        LoaiGD TEXT NOT NULL
);
CREATE TABLE IF NOT EXISTS giaodich (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    sotien REAL NOT NULL,
    LoaiGD TEXT NOT NULL,
    IDvi INTEGER,
    IDdanhmuc INTEGER,
    ngay TEXT,
    ghichu TEXT,
    FOREIGN KEY (IDvi) REFERENCES vi(Id),
    FOREIGN KEY (IDdanhmuc) REFERENCES danhmuc(Id)
);
";
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
                    @"INSERT INTO taikhoan(user, password) VALUES ($user , $password);";
                truyvan.Parameters.AddWithValue("$user", user);
                truyvan.Parameters.AddWithValue("$password", pass);
                truyvan.ExecuteNonQuery();
            }
        }
        public void themVi(string ten, double tien, int idtk)
        {
            using (var connection = new SqliteConnection(connect))
            {
                connection.Open();
                var truyvan = connection.CreateCommand();
                truyvan.CommandText =
                    @"INSERT INTO vi(ten, sotien , IDtk) VALUES ($name , $stien , $id);";
                truyvan.Parameters.AddWithValue("$name", ten);
                truyvan.Parameters.AddWithValue("$stien", tien);
                truyvan.Parameters.AddWithValue("$id", idtk);
                truyvan.ExecuteNonQuery();
            }
        }
        public void themDanhmuc(string ten, string loaigd)
        {
            using (var connection = new SqliteConnection(connect))
            {
                connection.Open();
                var truyvan = connection.CreateCommand();
                truyvan.CommandText =
                    @"INSERT INTO danhmuc(ten, LoaiGD) VALUES ($name , $GD);";
                truyvan.Parameters.AddWithValue("$name", ten);
                truyvan.Parameters.AddWithValue("$GD", loaigd);
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
            INSERT INTO giaodich (sotien, LoaiGD, IDvi, IDdanhmuc, ngay, ghichu) 
            VALUES (@sotien, @loai, @idvi, @iddanhmuc, @ngay, @ghichu)";

                truyvan.Parameters.AddWithValue("@sotien", soTien);
                truyvan.Parameters.AddWithValue("@loai", loaiGD);
                truyvan.Parameters.AddWithValue("@idvi", idVi);
                truyvan.Parameters.AddWithValue("@iddanhmuc", idDanhMuc);
                truyvan.Parameters.AddWithValue("@ngay", ngayGD.ToString("s"));
                truyvan.Parameters.AddWithValue("@ghichu", ghiChu);
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
                truyvan.CommandText = @"SELECT Id, user, password FROM taikhoan";
                using (var doc = truyvan.ExecuteReader())
                {
                    while (doc.Read())
                    {
                        ds.Add(new taikhoan
                        {
                            Id = doc.GetInt32(0),
                            user = doc.GetString(1),
                            password = doc.GetString(2)
                        });
                    }
                }
                return ds;
            }
        }
        public ObservableCollection<vi> layVi(int idtk)
        {
            using (var connection = new SqliteConnection(connect))
            {
                connection.Open();
                var ds = new ObservableCollection<vi>();
                var truyvan = connection.CreateCommand();
                truyvan.CommandText = @"SELECT Id, ten, sotien, IDtk FROM vi WHERE IDtk = $idtk";
                truyvan.Parameters.AddWithValue("$idtk", idtk);
                using (var doc = truyvan.ExecuteReader())
                {
                    while (doc.Read())
                    {
                        ds.Add(new vi
                        {
                            Id = doc.GetInt32(0),
                            ten = doc.GetString(1),
                            sotien = doc.GetDouble(2),
                            IDtk = doc.GetInt32(3)
                        });
                    }
                }
                return ds;
            }
        }

        // chưa viết xong....
        
    }
}
