using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace De01.DAO
{
    class PhieuTraDAO
    {
        public List<PhieuTra> LayHet()
        {
            return new QLThuVienEntities().PhieuTras.ToList();
        }

        public List<PhieuTra> LayTonTai()
        {
            return new QLThuVienEntities().PhieuTras.Where(i => i.State == true).ToList();
        }
    }
}
