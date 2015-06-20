using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using De01.DAO;

namespace De01.BUS
{
    class PhieuTraBUS
    {
        public static List<PhieuTra> LayHet()
        {
            return new PhieuTraDAO().LayHet();
        }

        public static List<PhieuTra> LayTonTai()
        {
            return new PhieuTraDAO().LayTonTai();
        }
    }
}
