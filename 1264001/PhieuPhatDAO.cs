using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace De01.DAO
{
    class PhieuPhatDAO
    {
        public List<PhieuPhat> LayHet()
        {
            return new QLThuVienEntities().PhieuPhats.ToList();
        }

        public List<PhieuPhat> LayTonTai()
        {
            return new QLThuVienEntities().PhieuPhats.Where(i => i.State == true).ToList();
        }
    }
}
