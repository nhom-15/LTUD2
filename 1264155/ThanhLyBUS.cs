using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using De01.DAO;

namespace De01.BUS
{
    class ThanhLyBUS
    {
        public static List<ThanhLy> LayHet()
        {
            return new ThanhLyDAO().LayHet();
        }

        public static List<ThanhLy> LayTonTai()
        {
            return new ThanhLyDAO().LayTonTai();
        }
    }
}
