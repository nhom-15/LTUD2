using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using De01.DAO;

namespace De01.BUS
{
    class SachBUS
    {
        public static List<Sach> LayHet()
        {
            return new SachDAO().LayHet();
        }

        public static List<Sach> LayTonTai()
        {
            return new SachDAO().LayTonTai();
        }
    }
}
