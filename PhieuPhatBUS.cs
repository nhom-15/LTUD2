using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using De01.DAO;

namespace De01.BUS
{
    class PhieuPhatBUS
    {
        public static List<PhieuPhat> LayHet()
        {
            return new PhieuPhatDAO().LayHet();
        }

        public static List<PhieuPhat> LayTonTai()
        {
            return new PhieuPhatDAO().LayTonTai();
        }
    }
}
