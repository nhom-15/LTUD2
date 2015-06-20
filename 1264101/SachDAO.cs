using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace De01.DAO
{
    class SachDAO
    {
        public List<Sach> LayHet()
        {
            return new QLThuVienEntities().Saches.ToList();
        }

        public List<Sach> LayTonTai()
        {
            return new QLThuVienEntities().Saches.Where(i => i.State == true).ToList();
        }
    }
}
