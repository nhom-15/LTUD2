using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace De01.TacVu
{
    class BCDocGia
    {
        int _STT;

        public int STT
        {
            get { return _STT; }
            set { _STT = value; }
        }
        string _TenDG;

        public string TenDG
        {
            get { return _TenDG; }
            set { _TenDG = value; }
        }

        string _TienNo;

        public string TienNo
        {
            get { return _TienNo; }
            set { _TienNo = value; }
        }
    }
}
