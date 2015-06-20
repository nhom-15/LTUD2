using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace De01.TacVu
{
    class BCTheLoai
    {
        int soluong;

        public int Soluong
        {
            get { return soluong; }
            set { soluong = value; }
        }
        double tile;

        public double Tile
        {
            get { return tile; }
            set { tile = value; }
        }

        TheLoai tl;

        public TheLoai Tl
        {
            get { return tl; }
            set { tl = value; }
        }
        int _STT;

        public int STT
        {
            get { return _STT; }
            set { _STT = value; }
        }
    }
}
