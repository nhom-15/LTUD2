using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace De01.TacVu
{
    class BCTraTre
    {
        int _STT;

        public int STT
        {
            get { return _STT; }
            set { _STT = value; }
        }
        Sach _S;

        public Sach S
        {
            get { return _S; }
            set { _S = value; }
        }
        string _NgayMuon;

        public string NgayMuon
        {
            get { return _NgayMuon; }
            set { _NgayMuon = value; }
        }
        int _SoNgay;

        public int SoNgay
        {
            get { return _SoNgay; }
            set { _SoNgay = value; }
        }
    }
}
