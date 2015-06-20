using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using De01.DAO;

namespace De01.BUS
{
    class MatSachBUS
    {
        public static List<MatSach> LayHet()
        {
            return new MatSachDAO().LayHet();
        }

        public static List<MatSach> LayTonTai()
        {
            return new MatSachDAO().LayTonTai();
        }
    }
}
