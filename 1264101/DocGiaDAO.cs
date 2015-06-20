using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace De01.DAO
{
    class DocGiaDAO
    {
        public List<TheDocGia> LayHet()
        {
            return new QLThuVienEntities().TheDocGias.ToList();
        }

        public List<TheDocGia> LayTonTai()
        {
            return new QLThuVienEntities().TheDocGias.Where(i => i.State == true).ToList();
        }
    }
}
