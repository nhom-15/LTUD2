using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using De01.DAO;

namespace De01.BUS
{
    class NhanVienBUS
    {
        public static List<NhanVien> LayHet()
        {
            return new NhanVienDAO().LayHet();
        }

        public static List<NhanVien> LayTonTai()
        {
            return new NhanVienDAO().LayTonTai();
        }
        public static List<NhanVien> LayUsers()
        {
            return new NhanVienDAO().LayUsers();
        }
        public static List<NhanVien> LayBoPhan(int id)
        {
            return new NhanVienDAO().LayBoPhan(id);
        }
    }
}
