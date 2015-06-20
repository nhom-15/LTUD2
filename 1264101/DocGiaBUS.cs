using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using De01.DAO;

namespace De01.BUS
{
    class DocGiaBUS
    {
        public static List<TheDocGia> LayHet()
        {
            return new DocGiaDAO().LayHet();
        }

        public static List<TheDocGia> LayTonTai()
        {
            return new DocGiaDAO().LayTonTai();
        }
    }
}
