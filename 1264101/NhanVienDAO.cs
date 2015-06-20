using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace De01.DAO
{
    class NhanVienDAO
    {
        public List<NhanVien> LayHet()
        {
            return new QLThuVienEntities().NhanViens.ToList();
        }

        public List<NhanVien> LayTonTai()
        {
            return new QLThuVienEntities().NhanViens.Where(i => i.State == true).ToList();
        }

        public List<NhanVien> LayUsers()
        {
            return new QLThuVienEntities().NhanViens.Where(i => i.State == true && i.ID_BoPhan!=0).ToList();
        }
        public List<NhanVien> LayBoPhan(int id)
        {
            return new QLThuVienEntities().NhanViens.Where(i => i.State == true && i.ID_BoPhan != 0 && i.ID_BoPhan==id).ToList();
        }
    }
}
