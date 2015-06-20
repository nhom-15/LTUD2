using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace De01.DAO
{
    class PhieuMuonDAO
    {
        public List<PhieuMuon> LayHet()
        {
            return new QLThuVienEntities().PhieuMuons.ToList();
        }

        public List<PhieuMuon> LayTonTai()
        {
            return new QLThuVienEntities().PhieuMuons.Where(i => i.State == true).ToList();
        }

        public PhieuMuon LayTheoID(string id)
        {
            return new QLThuVienEntities().PhieuMuons.Where(i => i.ID == id).FirstOrDefault();
        }
    }
}
