using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace De01.DAO
{
    class LoaiDocGiaDAO
    {
        public List<LoaiDocGia> LayHet()
        {
            return new QLThuVienEntities().LoaiDocGias.ToList();
        }


    }
}
