using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using De01.DAO;

namespace De01.BUS
{
    class LoaiDocGiaBUS
    {
        public static List<LoaiDocGia> LayHet()
        {
            return new LoaiDocGiaDAO().LayHet();
        }
    }
}
