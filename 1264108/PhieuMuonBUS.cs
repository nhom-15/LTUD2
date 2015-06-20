using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using De01.DAO;

namespace De01.BUS
{
    class PhieuMuonBUS
    {
        public static List<PhieuMuon> LayHet()
        {
            return new PhieuMuonDAO().LayHet();
        }

        public static List<PhieuMuon> LayTonTai()
        {
            return new PhieuMuonDAO().LayTonTai();
        }

        public static PhieuMuon LayTheoID(string id)
        {
            return new PhieuMuonDAO().LayTheoID(id);
        }
    }
}
