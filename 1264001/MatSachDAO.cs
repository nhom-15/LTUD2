using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace De01.DAO
{
    class MatSachDAO
    {
        public List<MatSach> LayHet()
        {
            return new QLThuVienEntities().MatSaches.ToList();
        }

        public List<MatSach> LayTonTai()
        {
            return new QLThuVienEntities().MatSaches.Where(i => i.State == true).ToList();
        }
    }
}
