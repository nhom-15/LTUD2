using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace De01.DAO
{
    class ThanhLyDAO
    {
        public List<ThanhLy> LayHet()
        {
            return new QLThuVienEntities().ThanhLies.ToList();
        }

        public List<ThanhLy> LayTonTai()
        {
            return new QLThuVienEntities().ThanhLies.Where(i => i.State == true).ToList();
        }
    }
}
